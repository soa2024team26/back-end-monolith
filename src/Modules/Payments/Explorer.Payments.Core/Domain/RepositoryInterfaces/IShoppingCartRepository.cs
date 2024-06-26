﻿using Explorer.Payments.API.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface IShoppingCartRepository
    {
        public ShoppingCart GetById(int shoppingCartId);
        public void Update(ShoppingCart shoppingCart);
        public ShoppingCart GetShoppingCartByUserId(int userId);
        public double GetTotalPriceByUserId(int userId);
    }
}
