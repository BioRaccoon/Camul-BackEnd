using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> CreateFeedback(FeedbackCreationDto feedback);
        Task DeleteAllFeedbacks(bool trackChanges);
        Task<IEnumerable<FeedbackDto>> GetAllFeedbacks(bool trackChanges);
        Task<FeedbackDto> GetFeedbackById(Guid guid, bool trackChanges);
        Task <IEnumerable<FeedbackDto>>GetUserFeedbacks(Guid guid, bool trackChanges);
        Task RemoveFeedback(FeedbackDto feedback);
        Task UpdateFeedback(FeedbackCreationDto feedback, Feedback? feedbackData);
    }
}
