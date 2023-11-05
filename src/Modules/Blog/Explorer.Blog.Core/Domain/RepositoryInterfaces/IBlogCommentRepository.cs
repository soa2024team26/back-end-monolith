﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogCommentRepository
    {

        List<BlogComment> GetCommentsByBlogId(int blogId);


    }
}
