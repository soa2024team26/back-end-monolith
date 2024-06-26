﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using Profile = Explorer.Stakeholders.Core.Domain.Users.Profile;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ProfileService : CrudService<ProfileDto, Profile>, IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly IFollowRepository _followRepository;
        private readonly IMessageRepository _messageRepository;
        public ProfileService(ICrudRepository<Profile> repository, IMapper mapper, IProfileRepository profileRepository, IFollowRepository followRepository, IMessageRepository messageRepository) : base(repository, mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
            _followRepository = followRepository;
            _messageRepository = messageRepository;
        }

        // PROFILE
        public Result<ProfileDto> GetByUserId(int userId)
        {
            foreach (var profile in _profileRepository.GetAll())
            {
                if (profile.UserId == userId)
                {
                    var profileDto = _mapper.Map<ProfileDto>(profile);

                    return Result.Ok(profileDto);
                }
            }

            return Result.Fail("Profile not found for the given userId.");
        }

        public Result AddFollow(FollowDto followDto)
        {
            Follow follow = new Follow(followDto.ProfileId, followDto.FollowerId);

            Profile profile = _profileRepository.Get((int)followDto.ProfileId);
            profile.AddFollow(follow);

            _profileRepository.Update(profile);

            return Result.Ok();
        }


        public Result<PagedResult<ProfileDto>> GetAllFollowers(int page, int pageSize, long profileId)

        {
            Profile profile = _profileRepository.Get(Convert.ToInt32(profileId));

            var followers = profile.Follows
                .Where(follow => follow.ProfileId == profileId)
                .Select(follow =>
                {
                    var profileResult = Get((int)follow.FollowerId);

                    if (profileResult.IsSuccess)
                    {
                        var profile = profileResult.Value;

                        return new ProfileDto
                        {
                            Id = profile.Id,
                            FirstName = profile.FirstName,
                            LastName = profile.LastName,
                            ProfilePicture = profile.ProfilePicture,
                            Biography = profile.Biography,
                            Motto = profile.Motto,
                            UserId = profile.UserId,
                            IsActive = profile.IsActive
                        };
                    }

                    return new ProfileDto();
                })
                .ToList();

            var pagedResult = new PagedResult<ProfileDto>(followers, followers.Count());
            return Result.Ok(pagedResult);
        }

        public bool AlreadyFollows(long profileId, long followerId)
        {
            Profile profile = _profileRepository.Get((int)profileId);

            foreach (Follow follow in profile.Follows)
            {
                if ((follow.ProfileId == profileId) && (follow.FollowerId == followerId))
                {
                    return true;
                }
            }
            return false;
        }

        public Result<PagedResult<MessageDto>> GetUnreadMessages(int page, int pageSize, long profileId)
        {
            var unreadMessages = _messageRepository.GetAll()
                .Where(message => message.ReceiverId == profileId && message.Status == 0)
                .ToList();

            var unreadMessageDtos = _mapper.Map<List<MessageDto>>(unreadMessages);

            var pagedResult = Result.Ok(new PagedResult<MessageDto>(unreadMessageDtos, unreadMessageDtos.Count));

            return pagedResult;

        }


    }
}
