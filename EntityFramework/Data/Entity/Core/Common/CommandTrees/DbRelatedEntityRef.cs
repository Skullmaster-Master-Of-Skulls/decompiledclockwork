using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000104 RID: 260
	internal sealed class DbRelatedEntityRef
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x00025EF0 File Offset: 0x000240F0
		internal DbRelatedEntityRef(RelationshipEndMember sourceEnd, RelationshipEndMember targetEnd, DbExpression targetEntityRef)
		{
			if (!object.ReferenceEquals(sourceEnd.DeclaringType, targetEnd.DeclaringType))
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship, "targetEnd");
			}
			if (object.ReferenceEquals(sourceEnd, targetEnd))
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd, "targetEnd");
			}
			if (targetEnd.RelationshipMultiplicity != RelationshipMultiplicity.One && targetEnd.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne, "targetEnd");
			}
			if (!TypeSemantics.IsReferenceType(targetEntityRef.ResultType))
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEntityNotRef, "targetEntityRef");
			}
			EntityTypeBase elementType = TypeHelpers.GetEdmType<RefType>(targetEnd.TypeUsage).ElementType;
			EntityTypeBase elementType2 = TypeHelpers.GetEdmType<RefType>(targetEntityRef.ResultType).ElementType;
			if (!elementType.EdmEquals(elementType2) && !TypeSemantics.IsSubTypeOf(elementType2, elementType))
			{
				throw new ArgumentException(Strings.Cqt_RelatedEntityRef_TargetEntityNotCompatible, "targetEntityRef");
			}
			this._targetEntityRef = targetEntityRef;
			this._targetEnd = targetEnd;
			this._sourceEnd = sourceEnd;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00025FD6 File Offset: 0x000241D6
		internal RelationshipEndMember SourceEnd
		{
			get
			{
				return this._sourceEnd;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00025FDE File Offset: 0x000241DE
		internal RelationshipEndMember TargetEnd
		{
			get
			{
				return this._targetEnd;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00025FE6 File Offset: 0x000241E6
		internal DbExpression TargetEntityReference
		{
			get
			{
				return this._targetEntityRef;
			}
		}

		// Token: 0x040001F1 RID: 497
		private readonly RelationshipEndMember _sourceEnd;

		// Token: 0x040001F2 RID: 498
		private readonly RelationshipEndMember _targetEnd;

		// Token: 0x040001F3 RID: 499
		private readonly DbExpression _targetEntityRef;
	}
}
