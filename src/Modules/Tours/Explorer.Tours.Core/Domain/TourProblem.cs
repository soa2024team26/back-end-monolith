﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Core.Domain;


public class TourProblem : Entity
{
    public string? ProblemCategory { get; init; }
    public string? ProblemPriority { get; init; }
    public string? Description { get; init; }
    public DateTime TimeStamp { get; init; }
    public long TourId { get; init; }
    public bool IsClosed { get; init; }
    public bool IsResolved { get; init; }
    public long TouristId { get;init; }
    public DateTime? DeadlineTimeStamp {get; init; }
    public TourProblem(string? problemCategory, string? problemPriority,
        string? description, DateTime timeStamp, long tourId, bool isClosed, bool isResolved, long touristId, DateTime? deadlineTimeStamp)
    {
        ProblemCategory = problemCategory;
        ProblemPriority = problemPriority;
        Description = description;
        TimeStamp = timeStamp;
        TourId = tourId;
        IsClosed = isClosed;
        IsResolved = isResolved;
        TouristId = touristId;
        DeadlineTimeStamp = deadlineTimeStamp;
        Validate();
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description.");
        if (string.IsNullOrWhiteSpace(ProblemCategory)) throw new ArgumentNullException("Problem Category is empty");
        if (string.IsNullOrWhiteSpace(ProblemPriority)) throw new ArgumentException("Problem Priority is empty");
        if (TimeStamp == null) throw new ArgumentException("Time Stamp is empty");
        if (IsClosed == null) throw new ArgumentException("IsClosed is null");
        if (IsResolved == null) throw new ArgumentException("IsResolved is null");
        if (TourId == 0) throw new ArgumentException("Invalid TourId");
        if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
    }
}