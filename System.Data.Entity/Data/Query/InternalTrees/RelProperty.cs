using System;
using System.Data.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000095 RID: 149
	internal sealed class RelProperty
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x00035AF0 File Offset: 0x00033CF0
		internal RelProperty(RelationshipType relationshipType, RelationshipEndMember fromEnd, RelationshipEndMember toEnd)
		{
			this.m_relationshipType = relationshipType;
			this.m_fromEnd = fromEnd;
			this.m_toEnd = toEnd;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00035B0D File Offset: 0x00033D0D
		public RelationshipType Relationship
		{
			get
			{
				return this.m_relationshipType;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00035B15 File Offset: 0x00033D15
		public RelationshipEndMember FromEnd
		{
			get
			{
				return this.m_fromEnd;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00035B1D File Offset: 0x00033D1D
		public RelationshipEndMember ToEnd
		{
			get
			{
				return this.m_toEnd;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00035B28 File Offset: 0x00033D28
		public override bool Equals(object obj)
		{
			RelProperty relProperty = obj as RelProperty;
			return relProperty != null && this.Relationship.EdmEquals(relProperty.Relationship) && this.FromEnd.EdmEquals(relProperty.FromEnd) && this.ToEnd.EdmEquals(relProperty.ToEnd);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00035B78 File Offset: 0x00033D78
		public override int GetHashCode()
		{
			return this.ToEnd.Identity.GetHashCode();
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00035B8C File Offset: 0x00033D8C
		[DebuggerNonUserCode]
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.m_relationshipType.ToString(),
				":",
				this.m_fromEnd.ToString(),
				":",
				this.m_toEnd.ToString()
			});
		}

		// Token: 0x040008A7 RID: 2215
		private readonly RelationshipType m_relationshipType;

		// Token: 0x040008A8 RID: 2216
		private readonly RelationshipEndMember m_fromEnd;

		// Token: 0x040008A9 RID: 2217
		private readonly RelationshipEndMember m_toEnd;
	}
}
