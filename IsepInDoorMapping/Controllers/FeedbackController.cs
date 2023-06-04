using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Filters;
using ServiceLayer.Interfaces;

namespace IsepInDoorMapping.Controllers
{
    [Route("api/feedbacks")]
    public class FeedbackController : BaseApiController
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(ILoggerManager logger,
            IFeedbackService _feedbackService) : base(logger)
        {
            this._feedbackService = _feedbackService;
        }

        /// <summary>
        /// Get all Feedbacks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var feedbackDto = await _feedbackService.GetAllFeedbacks(false);
                return Ok(feedbackDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllFeedbacks)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get a Feedback by Id
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        [HttpGet("{feedbackID}", Name = "GetFeedbackById")]
        public async Task<IActionResult> GetFeedbackById(string feedbackID)
        {
            var feedbackDto = await _feedbackService.GetFeedbackById(
                                            new Guid(feedbackID), trackChanges: false);

            if (feedbackDto is null)
            {
                _logger.LogInfo($"Feedback with id: {feedbackID} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(feedbackDto);
            }
        }

        [HttpGet("user/{userID}", Name = "GetUserFeedbacks")]
        public async Task<IActionResult> GetUserFeedbacks(string userID)
        {
            var feedbackDto = await _feedbackService.GetUserFeedbacks(
                                            new Guid(userID), trackChanges: false);

            if (feedbackDto is null)
            {
                _logger.LogInfo($"The user with ID {userID} doesn´t have feedbacks associated!");
                return NotFound();
            }
            else
            {
                return Ok(feedbackDto);
            }
        }

        /// <summary>
        /// Add a new Feedback
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackCreationDto feedback)
        {
            var feedbackReturn = await _feedbackService.CreateFeedback(feedback);

            return CreatedAtRoute("GetFeedbackById",
                new
                {
                    feedbackID = feedbackReturn.FeedbackID
                },
                feedbackReturn);
        }

        /// <summary>
        /// Update an Feedback
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPut("{feedbackID}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateFeedbackExists))]
        public async Task<IActionResult> UpdateFeedback(string feedbackID, [FromBody] FeedbackCreationDto feedback)
        {
            var feedbackData = HttpContext.Items["feedback"] as Feedback;

            await _feedbackService.UpdateFeedback(feedback, feedbackData);

            return NoContent();
        }

        /// <summary>
        /// Remove an Feedback
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        [HttpDelete("{feedbackID}")]
        public async Task<IActionResult> RemoveFeedback(string feedbackID)
        {
            var feedback = await _feedbackService.GetFeedbackById(new Guid(feedbackID), false);

            if (feedback is null)
            {
                _logger.LogInfo($"Feedback with id: {feedbackID} doesn't exist in the database. No need to delete it.");
                return NotFound();
            }
            else
            {
                await _feedbackService.RemoveFeedback(feedback);
                return Ok();
            }
        }

        [HttpDelete(Name ="DeleteAllFeedbacks")]
        public async Task<IActionResult> DeleteAllFeedback()
        {
            try
            {
                await _feedbackService.DeleteAllFeedbacks(false);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(DeleteAllFeedback)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
            finally
            {
            }
        }

    }
}