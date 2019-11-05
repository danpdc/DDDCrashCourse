using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.PostAggregate
{
    public class TextPost : BasePost
    {
        #region Constructors and properties
        private TextPost(Guid id) : base(id, PostType.Text)
        {
        }

        private TextPost() : base(Guid.NewGuid(), PostType.Text)
        {
        }

        public string Title { get; private set; }
        public string Message { get; private set; }
        #endregion

        #region Factories
        public static TextPost Create(string title, string message)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Post title can't be empty", nameof(TextPost.Title));
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Post text can't be empty", nameof(TextPost.Message));

            var post = new TextPost(Guid.NewGuid());
            post.DateCreated = DateTime.UtcNow;
            post.Title = title;
            post.Message = message;
            post.Comments = new List<Comment>();
            post.Interactions = new List<Interaction>();
            SetListCounts(post);

            return post;
        }

        public static TextPost Create(Guid id, string title, string message, List<Comment> comments,
            List<Interaction> interactions)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Post ID can't be an empty GUID", nameof(TextPost.Id));
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Post title can't be empty", nameof(TextPost.Title));
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Post text can't be empty", nameof(TextPost.Message));
            if (comments == null)
                throw new ArgumentException("Comment list can't be null", nameof(TextPost.Comments));
            if (interactions == null)
                throw new ArgumentException("Interaction list can't be null", nameof(TextPost.Interactions));

            var post = new TextPost(id);
            post.Title = title;
            post.Message = message;
            post.Comments = comments;
            post.Interactions = interactions;
            SetListCounts(post);

            return post;
        }
        #endregion

        #region Public methods
        public void EditPostMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
                LastModified = DateTime.UtcNow;
            }

        }

        public void EditPostTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Title = title;
                LastModified = DateTime.UtcNow;
            }

        }
        #endregion

        #region Private methods
        private static void SetListCounts(TextPost post)
        {
            post.NumberOfInteractions = post.Interactions.Count;
            post.NumberOfComments = post.Comments.Count;
            post.NumberOfLikes = post.Interactions.Count(i => i.Type == InteractionType.Like);
            post.NumberOfLoves = post.Interactions.Count(i => i.Type == InteractionType.Love);
            post.NumberOfLaughs = post.Interactions.Count(i => i.Type == InteractionType.Laugh);
        }
        #endregion
    }
}
