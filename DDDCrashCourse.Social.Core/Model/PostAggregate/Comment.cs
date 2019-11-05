using DDDCrashCourse.SharedKernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.PostAggregate
{
    public class Comment : Entity<Guid>
    {
        #region Constructor and properties
        private Comment(Guid id, Guid authorId, string message, DateTime utcDateCreated, DateTime utcLastModified) : base(id)
        {
            Message = message;
            AuthorId = authorId;
            DateCreated = utcDateCreated;
            LastModified = utcLastModified;
        }

        private Comment(Guid authorId, string message) : base(Guid.NewGuid())
        {
            AuthorId = authorId;
            Message = message;
            DateCreated = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }

        public Guid AuthorId { get; private set; }
        public string Message { get; internal set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; internal set; }
        #endregion

        #region Factories
        public static Comment Create(Guid id, Guid authorId, string message,
            DateTime utcDateCreated, DateTime utcLastModified)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Comment id can't be an empty GUID", nameof(Comment.Id));

            if (authorId == Guid.Empty)
                throw new ArgumentException("Comment authorI can't be an empty GUID", nameof(Comment.AuthorId));

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Comment message can't be null or empty", nameof(Comment.Message));

            if (utcLastModified > utcDateCreated)
                throw new ArgumentException("Last modified date can't be greater than the creation date'");

            return new Comment(id, authorId, message, utcDateCreated, utcLastModified);
        }

        public static Comment Create(Guid authorId, string message)
        {
            if (authorId == Guid.Empty)
                throw new ArgumentException("Comment authorId can't be an empty GUID", nameof(Comment.AuthorId));

            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Comment message can't be null or empty", nameof(Comment.Message));

            return new Comment(authorId, message);
        }
        #endregion

        #region Publi methods

        /// <summary>
        ///     Edit a comment message
        /// </summary>
        /// <param name="message">New message</param>
        /// <exception cref="ArgumentException"></exception>
        public void EditComment(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Comment message can't be null or empty", nameof(Comment.Message));

            Message = message;
            LastModified = DateTime.UtcNow;
        }
        #endregion
    }
}
