using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000619 RID: 1561
	internal sealed class RelProperty
	{
		// Token: 0x06003D2E RID: 15662 RVA: 0x0011AE2A File Offset: 0x0011902A
		internal RelProperty(RelationshipType relationshipType, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			this.m_relationshipType = relationshipType;
			this.m_fromEnd = fromEnd;
			this.m_toEnd = toEnd;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x0011AE47 File Offset: 0x00119047
		public RelationshipType Relationship
		{
			get
			{
				return this.m_relationshipType;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x0011AE4F File Offset: 0x0011904F
		public RelationshipEndMember FromEnd
		{
			get
			{
				return this.m_fromEnd;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x0011AE57 File Offset: 0x00119057
		public RelationshipEndMember ToEnd
		{
			get
			{
				return this.m_toEnd;
			}
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x0011AE60 File Offset: 0x00119060
		public override bool Equals(object obj)
		{
			RelProperty relProperty = obj as RelProperty;
			return relProperty != null && this.Relationship.EdmEquals(relProperty.Relationship) && this.FromEnd.EdmEquals(relProperty.FromEnd) && this.ToEnd.EdmEquals(relProperty.ToEnd);
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x0011AEB0 File Offset: 0x001190B0
		public override int GetHashCode()
		{
			return this.ToEnd.Identity.GetHashCode();
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x0011AEC4 File Offset: 0x001190C4
		[DebuggerNonUserCode]
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.m_relationshipType,
				":",
				this.m_fromEnd,
				":",
				this.m_toEnd
			});
		}

		// Token: 0x0400171F RID: 5919
		private readonly RelationshipType m_relationshipType;

		// Token: 0x04001720 RID: 5920
		private readonly RelationshipEndMember m_fromEnd;

		// Token: 0x04001721 RID: 5921
		private readonly RelationshipEndMember m_toEnd;
	}
}
