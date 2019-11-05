using DDDCrashCourse.SharedKernel.Types;
using DDDCrashCourse.Social.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.PostAggregate
{
    public abstract class BasePost : Entity<Guid>, IAggregateRoot
    {
        #region Constructors and properties
        protected BasePost(Guid id, PostType type) : base(id)
        {
            Type = type;
        }

        protected BasePost(PostType type) : base(Guid.NewGuid())
        {
            Type = type;
        }

        public PostType Type { get; private set; }
        public Guid Author { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime LastModified { get; protected set; }
        public List<Comment> Comments { get; protected set; }
        public List<Interaction> Interactions { get; protected set; }
        public int NumberOfInteractions { get; protected set; }
        public int NumberOfComments { get; protected set; }
        public int NumberOfLikes { get; protected set; }
        public int NumberOfLoves { get; protected set; }
        public int NumberOfLaughs { get; protected set; }

        #endregion

        #region Public methods
        public void AddComment(Guid commentAuthor, string message)
        {
            var comment = Comment.Create(commentAuthor, message);
            Comments.Add(comment);
        }

        public void DeleteComment(Guid commentId)
        {
            var comment = Comments.Find(c => c.Id == commentId);
            Comments.Remove(comment);
        }

        public void EditComment(Guid commentId, string message)
        {
            var comment = Comments.Find(c => c.Id == commentId);
            comment.Message = message;
            comment.LastModified = DateTime.UtcNow;
        }

        public void AddInteraction(Guid interactionAuthor, InteractionType type)
        {
            Interactions.Add(new Interaction(interactionAuthor, type));
        }

        public void RemoveInteraction(Guid interactionAuthor)
        {
            var interaction = Interactions.Find(i => i.InteractionAuthor == interactionAuthor);
            Interactions.Remove(interaction);
        }
        #endregion
    }
}
