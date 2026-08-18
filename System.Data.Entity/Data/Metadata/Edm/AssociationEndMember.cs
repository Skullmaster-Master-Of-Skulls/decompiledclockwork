using System;
using System.Data.Objects.DataClasses;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C0 RID: 448
	public sealed class AssociationEndMember : RelationshipEndMember
	{
		// Token: 0x06001F28 RID: 7976 RVA: 0x0006E010 File Offset: 0x0006C210
		internal AssociationEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity) : base(name, endRefType, multiplicity)
		{
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x000173E2 File Offset: 0x000155E2
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationEndMember;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x0006E01B File Offset: 0x0006C21B
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x0006E023 File Offset: 0x0006C223
		internal Func<RelationshipManager, RelatedEnd, RelatedEnd> GetRelatedEnd
		{
			get
			{
				return this._getRelatedEndMethod;
			}
			set
			{
				Interlocked.CompareExchange<Func<RelationshipManager, RelatedEnd, RelatedEnd>>(ref this._getRelatedEndMethod, value, null);
			}
		}

		// Token: 0x04000D14 RID: 3348
		private Func<RelationshipManager, RelatedEnd, RelatedEnd> _getRelatedEndMethod;
	}
}
