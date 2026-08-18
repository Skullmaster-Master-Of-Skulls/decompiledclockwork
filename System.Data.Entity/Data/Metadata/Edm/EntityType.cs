using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D0 RID: 464
	public class EntityType : EntityTypeBase
	{
		// Token: 0x06001FA6 RID: 8102 RVA: 0x0006EB2C File Offset: 0x0006CD2C
		internal EntityType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0006EB42 File Offset: 0x0006CD42
		internal EntityType(string name, string namespaceName, DataSpace dataSpace, IEnumerable<string> keyMemberNames, IEnumerable<EdmMember> members) : base(name, namespaceName, dataSpace)
		{
			if (members != null)
			{
				EntityTypeBase.CheckAndAddMembers(members, this);
			}
			if (keyMemberNames != null)
			{
				base.CheckAndAddKeyMembers(keyMemberNames);
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x0006EB70 File Offset: 0x0006CD70
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityType;
			}
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0006EB74 File Offset: 0x0006CD74
		internal bool TryGetMemberSql(EdmMember member, out string sql)
		{
			sql = null;
			return this._memberSql != null && this._memberSql.TryGetValue(member, out sql);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0006EB90 File Offset: 0x0006CD90
		internal void SetMemberSql(EdmMember member, string sql)
		{
			object memberSqlLock = this._memberSqlLock;
			lock (memberSqlLock)
			{
				if (this._memberSql == null)
				{
					this._memberSql = new Dictionary<EdmMember, string>();
				}
				this._memberSql[member] = sql;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x0006EBEC File Offset: 0x0006CDEC
		public ReadOnlyMetadataCollection<NavigationProperty> NavigationProperties
		{
			get
			{
				return new FilteredReadOnlyMetadataCollection<NavigationProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsNavigationProperty));
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x0006EC05 File Offset: 0x0006CE05
		public ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty)), null);
				}
				return this._properties;
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0006EC39 File Offset: 0x0006CE39
		public RefType GetReferenceType()
		{
			if (this._referenceType == null)
			{
				Interlocked.CompareExchange<RefType>(ref this._referenceType, new RefType(this), null);
			}
			return this._referenceType;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0006EC5C File Offset: 0x0006CE5C
		internal RowType GetKeyRowType(MetadataWorkspace metadataWorkspace)
		{
			if (this._keyRow == null)
			{
				List<EdmProperty> list = new List<EdmProperty>(base.KeyMembers.Count);
				foreach (EdmMember edmMember in base.KeyMembers)
				{
					list.Add(new EdmProperty(edmMember.Name, Helper.GetModelTypeUsage(edmMember)));
				}
				Interlocked.CompareExchange<RowType>(ref this._keyRow, new RowType(list), null);
			}
			return this._keyRow;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0006ECF4 File Offset: 0x0006CEF4
		internal bool TryGetNavigationProperty(string relationshipType, string fromName, string toName, out NavigationProperty navigationProperty)
		{
			foreach (NavigationProperty navigationProperty2 in this.NavigationProperties)
			{
				if (navigationProperty2.RelationshipType.FullName == relationshipType && navigationProperty2.FromEndMember.Name == fromName && navigationProperty2.ToEndMember.Name == toName)
				{
					navigationProperty = navigationProperty2;
					return true;
				}
			}
			navigationProperty = null;
			return false;
		}

		// Token: 0x04000DFD RID: 3581
		private RefType _referenceType;

		// Token: 0x04000DFE RID: 3582
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x04000DFF RID: 3583
		private RowType _keyRow;

		// Token: 0x04000E00 RID: 3584
		private Dictionary<EdmMember, string> _memberSql;

		// Token: 0x04000E01 RID: 3585
		private object _memberSqlLock = new object();
	}
}
