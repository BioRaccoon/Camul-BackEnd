using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{

    public class FeedbackService : IFeedbackService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public FeedbackService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FeedbackDto> CreateFeedback(FeedbackCreationDto feedback)
        {
            var feedbackData = _mapper.Map<Feedback>(feedback);

            await _repository.Feedback.AddFeedback(feedbackData);
            await _repository.SaveAsync();

            var feedbackReturn = _mapper.Map<FeedbackDto>(feedbackData);

            return feedbackReturn;
        }

        public async Task DeleteAllFeedbacks(bool trackChanges)
        {
            await _repository.Feedback.DeleteAll(false);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<FeedbackDto>> GetAllFeedbacks(bool trackChanges)
        {
            var feedbacks = await _repository.Feedback.GetAllFeedbacks(trackChanges: false);

            var feedbackDto = _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);

            return feedbackDto;
        }

        public async Task<FeedbackDto> GetFeedbackById(Guid guid, bool trackChanges)
        {
            var feedback = await _repository.Feedback.GetFeedbackById(guid, trackChanges: false);

            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

            return feedbackDto;
        }

        public async Task<IEnumerable<FeedbackDto>> GetUserFeedbacks(Guid userID, bool trackChanges)
        {
            var feedbacks = await _repository.Feedback.GetUserFeedbacks(userID,false);

            var feedbackDto = _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);

            return feedbackDto;
        }

        public async Task RemoveFeedback(FeedbackDto feedbackDto)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDto);

            await _repository.Feedback.RemoveFeedback(feedback);
            await _repository.SaveAsync();
        }

        public async Task UpdateFeedback(FeedbackCreationDto feedback, Feedback? feedbackData)
        {
            _mapper.Map(feedback, feedbackData);
            await _repository.SaveAsync();
        }
    }
}
