﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class PaymentNotificationService : CrudService<PaymentNotificationDto, PaymentNotification>, IPaymentNotificationService
    {
        private readonly ICrudRepository<PaymentNotification> _paymentNotificationRepository;
        public PaymentNotificationService(ICrudRepository<PaymentNotification> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
            _paymentNotificationRepository = crudRepository;
        }

        

    }
}
