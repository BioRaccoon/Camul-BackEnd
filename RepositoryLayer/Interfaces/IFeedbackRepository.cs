using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks(bool trackChanges);
        Task<Feedback> GetFeedbackById(Guid feedbackID, bool trackChanges);
        Task AddFeedback(Feedback feedback);
        Task RemoveFeedback(Feedback feedback);
        Task DeleteAll(bool trackChanges);
        Task<IEnumerable<Feedback>> GetUserFeedbacks(Guid userID, bool trackChanges);
    }
}
