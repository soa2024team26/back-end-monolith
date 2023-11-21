﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Payments.Core.UseCases
{
    public class ShoppingCartService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingCartService
    {

        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICrudRepository<Tour> _tourRepository;
        private readonly ICrudRepository<OrderItem> _crudOrderItemRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICrudRepository<TourPurchaseToken> _tourPurchaseTokenRepository;


        public ShoppingCartService(ICrudRepository<ShoppingCart> repository, IMapper mapper, IShoppingCartRepository shoppingCartRepository, ICrudRepository<Tour> tourRepository, ICrudRepository<OrderItem> crudOrderItemRepository, IOrderItemRepository orderItemRepository, ICrudRepository<TourPurchaseToken> tourPurchaseTokenRepository) : base(repository, mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _tourRepository = tourRepository;
            _crudOrderItemRepository = crudOrderItemRepository;
            _orderItemRepository = orderItemRepository;
            _tourRepository = tourRepository;
            _tourPurchaseTokenRepository = tourPurchaseTokenRepository;
        }


        public Result<ShoppingCartDto> AddItem(ShoppingCartDto shoppingCartDto, int tourId)
        {
            try
            {
                Tours.Core.Domain.Tour tour = _tourRepository.Get(tourId);
                if (shoppingCartDto != null)
                {
                    OrderItem orderItem = new OrderItem(tourId, tour.Name, tour.Price, shoppingCartDto.Id, false);
                    _crudOrderItemRepository.Create(orderItem);

                    ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartDto.Id);

                    shoppingCart.AddItem((int)orderItem.Id);

                    shoppingCart.CalculateTotalPrice(shoppingCart.TotalPrice, orderItem.Price, true);
                    _shoppingCartRepository.Update(shoppingCart);
                    return Result.Ok(shoppingCartDto);
                }
                else
                {
                    return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
                }

            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }           

        }


        public Result<ShoppingCartDto> GetShoppingCartByUserId(int userId)
        {

            try
            {
                var shoppingCart = _shoppingCartRepository.GetShoppingCartByUserId(userId);
                ShoppingCartDto shoppingCartDto = MapToDto(shoppingCart);
                return Result.Ok(shoppingCartDto);
            }
            catch (Exception e)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<ShoppingCartDto> RemoveItem(int shoppingCartId, int itemId)
        {
            try
            {
                ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartId);
                OrderItem orderItem = GetOrderItemById(itemId);
                shoppingCart.RemoveItem(itemId);

                shoppingCart.CalculateTotalPrice(shoppingCart.TotalPrice, orderItem.Price, false);
                _shoppingCartRepository.Update(shoppingCart);
                _crudOrderItemRepository.Delete(itemId);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }


        public Result<ShoppingCartDto> RemoveAllItems(int shoppingCartId)
        {
            try
            {
                ShoppingCart shoppingCart = _shoppingCartRepository.GetById(shoppingCartId);
                shoppingCart.RemoveAllItems();
                _orderItemRepository.RemoveAllItemsByShoppingCartId(shoppingCartId);
                _shoppingCartRepository.Update(shoppingCart);

                return Result.Ok();
            }
            catch (ArgumentException e)
            {
                return Result.Fail<ShoppingCartDto>(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        private OrderItem GetOrderItemById(int id)
        {
            OrderItem orderItem = _crudOrderItemRepository.Get(id);
            return orderItem;
        }

        public Result<double> GetTotalPriceByUserId(int userId)
        {
            try
            {
                double TotalPrice = _shoppingCartRepository.GetTotalPriceByUserId(userId);
                return Result.Ok(TotalPrice);
            }
            catch (ArgumentException e)
            {
                return Result.Fail<double>(FailureCode.InvalidArgument).WithError(e.Message);
            }

        }


        public Result<String> CreateTourPurchaseToken(List<OrderItemDto> orderItems, int userId)
        {
            ShoppingCart shoppingCart = _shoppingCartRepository.GetShoppingCartByUserId(userId);
            foreach (OrderItemDto item in orderItems)
            {
                TourPurchaseToken purchaseToken = new TourPurchaseToken(userId, item.TourId, DateTime.UtcNow);
                _tourPurchaseTokenRepository.Create(purchaseToken);
                shoppingCart.RemoveItem(item.Id);
            }
            shoppingCart.TotalPrice = 0;
            _shoppingCartRepository.Update(shoppingCart);

            return Result.Ok();
        }
    }
}