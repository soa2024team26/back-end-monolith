﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourPurchaseToken : Entity
    {
        public long UserId { get; init; }
        public int TourId { get; init; }
        public DateTime PurchaseDate { get; init; }

        public TourPurchaseToken(long userId, int tourId, DateTime purchaseDate)
        {
            UserId = userId;
            TourId = tourId;
            PurchaseDate = purchaseDate;
        }
    }
}
