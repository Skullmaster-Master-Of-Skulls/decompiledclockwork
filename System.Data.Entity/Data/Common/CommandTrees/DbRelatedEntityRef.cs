using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200041F RID: 1055
	internal sealed class DbRelatedEntityRef
	{
		// Token: 0x06003709 RID: 14089 RVA: 0x000D17AC File Offset: 0x000CF9AC
		internal DbRelatedEntityRef(RelationshipEndMember sourceEnd, RelationshipEndMember targetEnd, DbExpression targetEntityRef)
		{
			EntityUtil.CheckArgumentNull<RelationshipEndMember>(sourceEnd, "sourceEnd");
			EntityUtil.CheckArgumentNull<RelationshipEndMember>(targetEnd, "targetEnd");
			EntityUtil.CheckArgumentNull<DbExpression>(targetEntityRef, "targetEntityRef");
			if (sourceEnd.DeclaringType != targetEnd.DeclaringType)
			{
				throw EntityUtil.Argument(Strings.Cqt_RelatedEntityRef_TargetEndFromDifferentRelationship, "targetEnd");
			}
			if (sourceEnd == targetEnd)
			{
				throw EntityUtil.Argument(Strings.Cqt_RelatedEntityRef_TargetEndSameAsSourceEnd, "targetEnd");
			}
			if (targetEnd.RelationshipMultiplicity != RelationshipMultiplicity.One && targetEnd.RelationshipMultiplicity != RelationshipMultiplicity.ZeroOrOne)
			{
				throw EntityUtil.Argument(Strings.Cqt_RelatedEntityRef_TargetEndMustBeAtMostOne, "targetEnd");
			}
			if (!TypeSemantics.IsReferenceType(targetEntityRef.ResultType))
			{
				throw EntityUtil.Argument(Strings.Cqt_RelatedEntityRef_TargetEntityNotRef, "targetEntityRef");
			}
			EntityTypeBase elementType = TypeHelpers.GetEdmType<RefType>(targetEnd.TypeUsage).ElementType;
			EntityTypeBase elementType2 = TypeHelpers.GetEdmType<RefType>(targetEntityRef.ResultType).ElementType;
			if (!elementType.EdmEquals(elementType2) && !TypeSemantics.IsSubTypeOf(elementType2, elementType))
			{
				throw EntityUtil.Argument(Strings.Cqt_RelatedEntityRef_TargetEntityNotCompatible, "targetEntityRef");
			}
			this._targetEntityRef = targetEntityRef;
			this._targetEnd = targetEnd;
			this._sourceEnd = sourceEnd;
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000D18AC File Offset: 0x000CFAAC
		internal RelationshipEndMember SourceEnd
		{
			get
			{
				return this._sourceEnd;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600370B RID: 14091 RVA: 0x000D18B4 File Offset: 0x000CFAB4
		internal RelationshipEndMember TargetEnd
		{
			get
			{
				return this._targetEnd;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000D18BC File Offset: 0x000CFABC
		internal DbExpression TargetEntityReference
		{
			get
			{
				return this._targetEntityRef;
			}
		}

		// Token: 0x04001832 RID: 6194
		private readonly RelationshipEndMember _sourceEnd;

		// Token: 0x04001833 RID: 6195
		private readonly RelationshipEndMember _targetEnd;

		// Token: 0x04001834 RID: 6196
		private readonly DbExpression _targetEntityRef;
	}
}
