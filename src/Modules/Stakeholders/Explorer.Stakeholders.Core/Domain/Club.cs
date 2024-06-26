﻿using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public long OwnerId { get; init; }
        public List<long> MemberIds { get; init; }

        public Club(string name, string description, string imageUrl, long ownerId, List<long> memberIds)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            OwnerId = ownerId;
            //MemberIds = new List<long>();
            MemberIds = memberIds;
            Validate();
        }

        private void Validate()
        {
            if (OwnerId == 0) throw new ArgumentException("Invalid UserId");
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        }
    }
}
