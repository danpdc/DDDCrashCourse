using DDDCrashCourse.SharedKernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDDCrashCourse.Social.Core.Model.PostAggregate
{
    public class Interaction : ValueObject<Interaction>
    {
        #region Constructor and properties
        public Interaction(Guid interactionAuthor, InteractionType type)
        {
            InteractionAuthor = interactionAuthor;
            Type = type;
        }

        public Guid InteractionAuthor { get; private set; }
        public InteractionType Type { get; private set; }
        #endregion

        #region Overrides
        public override bool Equals(Interaction other)
        {
            return InteractionAuthor == other.InteractionAuthor && Type == other.Type;
        }
        #endregion
    }
}
