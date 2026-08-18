using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CA RID: 1226
	[SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
	public abstract class RelationshipType : EntityTypeBase
	{
		// Token: 0x06002D46 RID: 11590 RVA: 0x000DB63F File Offset: 0x000D983F
		internal RelationshipType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x000DB64C File Offset: 0x000D984C
		public ReadOnlyMetadataCollection<RelationshipEndMember> RelationshipEndMembers
		{
			get
			{
				if (this._relationshipEndMembers == null)
				{
					FilteredReadOnlyMetadataCollection<RelationshipEndMember, EdmMember> value = new FilteredReadOnlyMetadataCollection<RelationshipEndMember, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsRelationshipEndMember));
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<RelationshipEndMember>>(ref this._relationshipEndMembers, value, null);
				}
				return this._relationshipEndMembers;
			}
		}

		// Token: 0x0400109B RID: 4251
		private ReadOnlyMetadataCollection<RelationshipEndMember> _relationshipEndMembers;
	}
}
