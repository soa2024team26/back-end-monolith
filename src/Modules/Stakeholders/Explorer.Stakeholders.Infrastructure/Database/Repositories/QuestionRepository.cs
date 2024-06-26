﻿using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly StakeholdersContext _dbContext;

        public QuestionRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Question Create(Question entity)
        {
            _dbContext.Question.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<Question> GetAllUnanswered()
        {
            return _dbContext.Question.Where(q => !q.isAnswered).ToList();
        }

        public IEnumerable<Question> GetAllAnswered()
        {
            return _dbContext.Question.Where(q => q.isAnswered).ToList();
        }

        public Question GetQuestionById(int questionId)
        {
            return _dbContext.Question.FirstOrDefault(q => q.Id == questionId);
        }
    }
}
