using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CC RID: 1228
	[SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
	public class EntityType : EntityTypeBase
	{
		// Token: 0x06002D58 RID: 11608 RVA: 0x000DB8C3 File Offset: 0x000D9AC3
		internal EntityType(string name, string namespaceName, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000DB8E4 File Offset: 0x000D9AE4
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

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000DB91D File Offset: 0x000D9B1D
		internal IEnumerable<ForeignKeyBuilder> ForeignKeyBuilders
		{
			get
			{
				return this._foreignKeyBuilders;
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000DB925 File Offset: 0x000D9B25
		internal void RemoveForeignKey(ForeignKeyBuilder foreignKeyBuilder)
		{
			Util.ThrowIfReadOnly(this);
			foreignKeyBuilder.SetOwner(null);
			this._foreignKeyBuilders.Remove(foreignKeyBuilder);
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000DB941 File Offset: 0x000D9B41
		internal void AddForeignKey(ForeignKeyBuilder foreignKeyBuilder)
		{
			Util.ThrowIfReadOnly(this);
			foreignKeyBuilder.SetOwner(this);
			this._foreignKeyBuilders.Add(foreignKeyBuilder);
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000DB95C File Offset: 0x000D9B5C
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityType;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000DB960 File Offset: 0x000D9B60
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000DB962 File Offset: 0x000D9B62
		public ReadOnlyMetadataCollection<NavigationProperty> DeclaredNavigationProperties
		{
			get
			{
				return base.GetDeclaredOnlyMembers<NavigationProperty>();
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x000DB96C File Offset: 0x000D9B6C
		public ReadOnlyMetadataCollection<NavigationProperty> NavigationProperties
		{
			get
			{
				ReadOnlyMetadataCollection<NavigationProperty> navigationPropertiesCache = this._navigationPropertiesCache;
				if (navigationPropertiesCache == null)
				{
					lock (this._navigationPropertiesCacheLock)
					{
						if (this._navigationPropertiesCache == null)
						{
							base.Members.SourceAccessed += this.ResetNavigationProperties;
							this._navigationPropertiesCache = new FilteredReadOnlyMetadataCollection<NavigationProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsNavigationProperty));
						}
						navigationPropertiesCache = this._navigationPropertiesCache;
					}
				}
				return navigationPropertiesCache;
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000DB9F4 File Offset: 0x000D9BF4
		private void ResetNavigationProperties(object sender, EventArgs e)
		{
			if (this._navigationPropertiesCache != null)
			{
				lock (this._navigationPropertiesCacheLock)
				{
					if (this._navigationPropertiesCache != null)
					{
						this._navigationPropertiesCache = null;
						base.Members.SourceAccessed -= this.ResetNavigationProperties;
					}
				}
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x000DBA5C File Offset: 0x000D9C5C
		public ReadOnlyMetadataCollection<EdmProperty> DeclaredProperties
		{
			get
			{
				return base.GetDeclaredOnlyMembers<EdmProperty>();
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000DBA64 File Offset: 0x000D9C64
		public ReadOnlyMetadataCollection<EdmMember> DeclaredMembers
		{
			get
			{
				return base.GetDeclaredOnlyMembers<EdmMember>();
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x000DBA6C File Offset: 0x000D9C6C
		public virtual ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (!base.IsReadOnly)
				{
					return new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty));
				}
				if (this._properties == null)
				{
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty)), null);
				}
				return this._properties;
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000DBACB File Offset: 0x000D9CCB
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public RefType GetReferenceType()
		{
			if (this._referenceType == null)
			{
				Interlocked.CompareExchange<RefType>(ref this._referenceType, new RefType(this), null);
			}
			return this._referenceType;
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000DBB04 File Offset: 0x000D9D04
		internal RowType GetKeyRowType()
		{
			if (this._keyRow == null)
			{
				List<EdmProperty> list = new List<EdmProperty>(this.KeyMembers.Count);
				list.AddRange(from keyMember in this.KeyMembers
				select new EdmProperty(keyMember.Name, Helper.GetModelTypeUsage(keyMember)));
				Interlocked.CompareExchange<RowType>(ref this._keyRow, new RowType(list), null);
			}
			return this._keyRow;
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000DBB74 File Offset: 0x000D9D74
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

		// Token: 0x06002D68 RID: 11624 RVA: 0x000DBC08 File Offset: 0x000D9E08
		public static EntityType Create(string name, string namespaceName, DataSpace dataSpace, IEnumerable<string> keyMemberNames, IEnumerable<EdmMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			EntityType entityType = new EntityType(name, namespaceName, dataSpace, keyMemberNames, members);
			if (metadataProperties != null)
			{
				entityType.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			entityType.SetReadOnly();
			return entityType;
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000DBC54 File Offset: 0x000D9E54
		public static EntityType Create(string name, string namespaceName, DataSpace dataSpace, EntityType baseType, IEnumerable<string> keyMemberNames, IEnumerable<EdmMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotNull<EntityType>(baseType, "baseType");
			EntityType entityType = new EntityType(name, namespaceName, dataSpace, keyMemberNames, members)
			{
				BaseType = baseType
			};
			if (metadataProperties != null)
			{
				entityType.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			entityType.SetReadOnly();
			return entityType;
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000DBCB3 File Offset: 0x000D9EB3
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public void AddNavigationProperty(NavigationProperty property)
		{
			Check.NotNull<NavigationProperty>(property, "property");
			base.AddMember(property, true);
		}

		// Token: 0x040010A0 RID: 4256
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x040010A1 RID: 4257
		private RefType _referenceType;

		// Token: 0x040010A2 RID: 4258
		private RowType _keyRow;

		// Token: 0x040010A3 RID: 4259
		private readonly List<ForeignKeyBuilder> _foreignKeyBuilders = new List<ForeignKeyBuilder>();

		// Token: 0x040010A4 RID: 4260
		private readonly object _navigationPropertiesCacheLock = new object();

		// Token: 0x040010A5 RID: 4261
		private ReadOnlyMetadataCollection<NavigationProperty> _navigationPropertiesCache;
	}
}
