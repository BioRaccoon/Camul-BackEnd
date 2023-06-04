using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.GenericRepository.Service;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks(bool trackChanges)
        {
            return await FindAllAsync(trackChanges).Result.OrderBy(c => c.FeedbackID.ToString()).ToListAsync();
        }

        public async Task<Feedback?> GetFeedbackById(Guid feedbackID, bool trackChanges)
            => await FindByConditionAsync(c => c.FeedbackID.Equals(feedbackID), trackChanges).Result.SingleOrDefaultAsync();

        public async Task AddFeedback(Feedback feedback) => await CreateAsync(feedback);

        public async Task RemoveFeedback(Feedback feedback) => await RemoveAsync(feedback);

        public async Task DeleteAll(bool trackChanges)
        {
            var feedbackList = await FindAllAsync(trackChanges);

            foreach (var feedback in feedbackList)
            {
                await RemoveFeedback(feedback);
            }
        }

        public async Task<IEnumerable<Feedback>> GetUserFeedbacks(Guid userID, bool trackChanges)
            => await FindByConditionAsync(
                c => c.ClientID.Equals(userID), trackChanges
                ).Result.OrderBy(c => c.FeedbackID.ToString()).ToListAsync();
    }
}