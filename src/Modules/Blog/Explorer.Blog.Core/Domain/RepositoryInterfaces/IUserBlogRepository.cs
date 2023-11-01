﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IUserBlogRepository
    {
        List<UserBlog> GetByUserId(int userId);
    }
}
