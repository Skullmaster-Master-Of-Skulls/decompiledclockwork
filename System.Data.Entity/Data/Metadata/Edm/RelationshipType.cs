using System;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F5 RID: 501
	public abstract class RelationshipType : EntityTypeBase
	{
		// Token: 0x0600211B RID: 8475 RVA: 0x0007485F File Offset: 0x00072A5F
		internal RelationshipType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x0007486C File Offset: 0x00072A6C
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

		// Token: 0x04000EA8 RID: 3752
		private ReadOnlyMetadataCollection<RelationshipEndMember> _relationshipEndMembers;
	}
}
