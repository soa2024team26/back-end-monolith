﻿using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain.Blog;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class UserBlogService : CrudService<UserBlogDto, UserBlog>, IUserBlogService
    {
        private readonly IUserBlogRepository _blogRepository;
        private readonly IBlogCommentService _blogCommentService;
        public UserBlogService(IUserBlogRepository blogRepository,  IBlogCommentService blogCommentService, ICrudRepository<UserBlog> repository, IMapper mapper) : base(repository, mapper) 
        {
            _blogRepository = blogRepository;
            _blogCommentService = blogCommentService;
        }

        public Result<UserBlogDto> Create(UserBlogDto blogDto)
        {
            blogDto.CreationTime = DateTime.UtcNow;

            return base.Create(blogDto);
        }

        public List<UserBlogDto> GetByUserId(int userId)
        {
            var blogs = _blogRepository.GetByUserId(userId);

            var blogDtos = blogs.Select(blog => new UserBlogDto
            {
                Id = (int)blog.Id,
                UserId = blog.UserId,
                Username = blog.Username,
                Title = blog.Title,
                Description = blog.Description,
                CreationTime = blog.CreationTime,
                Status = (API.Dtos.BlogStatus)blog.Status,
                Image = blog.Image
               
            }).ToList();

            return blogDtos;
        }

        public Result DeleteAll(int blogId)
        {
            var result = _blogCommentService.DeleteCommentsByBlogId(blogId);

            if (result.IsSuccess)
            {
               
                var deleteResult = Delete(blogId);

                if (deleteResult.IsSuccess)
                {
                    return Result.Ok();
                }
                else
                {
                    
                    return deleteResult;
                }
            }
            else
            {
               
                return result;
            }
        }



        public Result AddRating(RatingDto ratingDto)
        {
            Rating rating = new Rating(ratingDto.isUpvote, ratingDto.UserId, DateTime.UtcNow);

            UserBlog userBlog = _blogRepository.GetById((int)ratingDto.BlogId);
            userBlog.AddRating(rating);

            _blogRepository.Update(userBlog);
           

            return Result.Ok();

        }

        public Result<RatingCount> GetRatingsCount(int blogId)
        {
            UserBlog blog = _blogRepository.GetById(blogId);
            RatingCount count = new RatingCount { Count = blog.GetRatingsCount()};
            return count;
        }
        
        public List<UserBlogDto> GetByStatus(API.Dtos.BlogStatus status)
        {
            var blogs = _blogRepository.GetByStatus((Domain.Blog.BlogStatus)status);

            var blogDtos = blogs.Select(blog => new UserBlogDto
            {
                Id = (int)blog.Id,
                UserId = blog.UserId,
                Username = blog.Username,
                Title = blog.Title,
                Description = blog.Description,
                CreationTime = blog.CreationTime,
                Status = (API.Dtos.BlogStatus)blog.Status,
                Image = blog.Image

            }).ToList();

            return blogDtos;
        }

    }
}
