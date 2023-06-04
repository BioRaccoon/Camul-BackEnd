using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public class FeedbackData : IEntityTypeConfiguration<Feedback>
    {
        Dictionary<string, Feedback> feedbacks = new Dictionary<string, Feedback>();

        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            Bootstrap();
            foreach (var feedback in feedbacks)
            {
                builder.HasData(feedback.Value);
            }
        }

        public void Bootstrap()
        {
            try
            {
                feedbacks.Add("I cant see the building!",
                new Feedback
                {
                    FeedbackID = Guid.NewGuid(),
                    ClientID = ClientData.clients.ToList()[0].Value.UserID,
                    CloudFolderURL = ("https://www.google.com"),
                    FeedbackDate = DateTime.UtcNow,
                    FeedbackDescription = "I cant see the building!",
                    FeedbackLocationID = ClientData.clients.ToList()[0].Value.UserLocationID
                }
                );

                feedbacks.Add("It is in construction work!",
                new Feedback
                {
                    FeedbackID = Guid.NewGuid(),
                    ClientID = ClientData.clients.ToList()[1].Value.UserID,
                    CloudFolderURL = ("https://www.google.com"),
                    FeedbackDate = DateTime.UtcNow,
                    FeedbackDescription = "It is in construction work!",
                    FeedbackLocationID = ClientData.clients.ToList()[1].Value.UserLocationID
                }
                );

                feedbacks.Add("The building is different.",
                new Feedback
                {
                    FeedbackID = Guid.NewGuid(),
                    ClientID = ClientData.clients.ToList()[2].Value.UserID,
                    CloudFolderURL = ("https://www.google.com"),
                    FeedbackDate = DateTime.UtcNow,
                    FeedbackDescription = "The building is different.",
                    FeedbackLocationID = ClientData.clients.ToList()[0].Value.UserLocationID
                }
                );

                feedbacks.Add("The sight is awful!",
                new Feedback
                {
                    FeedbackID = Guid.NewGuid(),
                    ClientID = ClientData.clients.ToList()[3].Value.UserID,
                    CloudFolderURL = ("https://www.google.com"),
                    FeedbackDate = DateTime.UtcNow,
                    FeedbackDescription = "The sight is awful!",
                    FeedbackLocationID = ClientData.clients.ToList()[0].Value.UserLocationID
                }
                );

                feedbacks.Add("The place smells bad!",
                new Feedback
                {
                    FeedbackID = Guid.NewGuid(),
                    ClientID = ClientData.clients.ToList()[2].Value.UserID,
                    CloudFolderURL = ("https://www.google.com"),
                    FeedbackDate = DateTime.UtcNow,
                    FeedbackDescription = "The place smells bad!",
                    FeedbackLocationID = ClientData.clients.ToList()[0].Value.UserLocationID
                }
                );

                feedbacks.Add("Good path with awesome view!",
                new Feedback
                {
                    FeedbackID = Guid.NewGuid(),
                    ClientID = ClientData.clients.ToList()[1].Value.UserID,
                    CloudFolderURL = ("https://www.google.com"),
                    FeedbackDate = DateTime.UtcNow,
                    FeedbackDescription = "Good path with awesome view!",
                    FeedbackLocationID = ClientData.clients.ToList()[0].Value.UserLocationID
                }
                );

                feedbacks.Add("Thank you for the reception from ISEP!",
                new Feedback
                {
                    FeedbackID = Guid.NewGuid(),
                    ClientID = ClientData.clients.ToList()[0].Value.UserID,
                    CloudFolderURL = "https://www.google.com",
                    FeedbackDate = DateTime.UtcNow,
                    FeedbackDescription = "Thank you for the reception from ISEP!",
                    FeedbackLocationID = ClientData.clients.ToList()[0].Value.UserLocationID
                }
                );
            } catch (Exception) { }
        }
    }
}
