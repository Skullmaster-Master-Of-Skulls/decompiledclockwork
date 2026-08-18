using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FB RID: 1275
	public sealed class ReferentialConstraint : MetadataItem
	{
		// Token: 0x06002F6F RID: 12143 RVA: 0x000E4280 File Offset: 0x000E2480
		public ReferentialConstraint(RelationshipEndMember fromRole, RelationshipEndMember toRole, IEnumerable<EdmProperty> fromProperties, IEnumerable<EdmProperty> toProperties)
		{
			Check.NotNull<RelationshipEndMember>(fromRole, "fromRole");
			Check.NotNull<RelationshipEndMember>(toRole, "toRole");
			Check.NotNull<IEnumerable<EdmProperty>>(fromProperties, "fromProperties");
			Check.NotNull<IEnumerable<EdmProperty>>(toProperties, "toProperties");
			this._fromRole = fromRole;
			this._toRole = toRole;
			this._fromProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(fromProperties));
			this._toProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(toProperties));
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000E42F5 File Offset: 0x000E24F5
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ReferentialConstraint;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002F71 RID: 12145 RVA: 0x000E42F9 File Offset: 0x000E24F9
		internal override string Identity
		{
			get
			{
				return this.FromRole.Name + "_" + this.ToRole.Name;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000E431B File Offset: 0x000E251B
		// (set) Token: 0x06002F73 RID: 12147 RVA: 0x000E4323 File Offset: 0x000E2523
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromRole
		{
			get
			{
				return this._fromRole;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._fromRole = value;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000E4332 File Offset: 0x000E2532
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x000E433A File Offset: 0x000E253A
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToRole
		{
			get
			{
				return this._toRole;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._toRole = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x000E4349 File Offset: 0x000E2549
		internal AssociationEndMember PrincipalEnd
		{
			get
			{
				return (AssociationEndMember)this.FromRole;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000E4356 File Offset: 0x000E2556
		internal AssociationEndMember DependentEnd
		{
			get
			{
				return (AssociationEndMember)this.ToRole;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000E437C File Offset: 0x000E257C
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> FromProperties
		{
			get
			{
				if (!base.IsReadOnly && this._fromProperties.Count == 0)
				{
					this._fromRole.GetEntityType().KeyMembers.Each(delegate(EdmMember p)
					{
						this._fromProperties.Source.Add((EdmProperty)p);
					});
				}
				return this._fromProperties;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000E43CC File Offset: 0x000E25CC
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> ToProperties
		{
			get
			{
				return this._toProperties;
			}
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000E43D4 File Offset: 0x000E25D4
		public override string ToString()
		{
			return this.FromRole.Name + "_" + this.ToRole.Name;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000E43F8 File Offset: 0x000E25F8
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				this.FromProperties.Source.SetReadOnly();
				this.ToProperties.Source.SetReadOnly();
				base.SetReadOnly();
				RelationshipEndMember fromRole = this.FromRole;
				if (fromRole != null)
				{
					fromRole.SetReadOnly();
				}
				RelationshipEndMember toRole = this.ToRole;
				if (toRole != null)
				{
					toRole.SetReadOnly();
				}
			}
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000E4458 File Offset: 0x000E2658
		internal string BuildConstraintExceptionMessage()
		{
			string name = this.FromProperties.First<EdmProperty>().DeclaringType.Name;
			string name2 = this.ToProperties.First<EdmProperty>().DeclaringType.Name;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < this.FromProperties.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(", ");
					stringBuilder2.Append(", ");
				}
				stringBuilder.Append(name).Append('.').Append(this.FromProperties[i]);
				stringBuilder2.Append(name2).Append('.').Append(this.ToProperties[i]);
			}
			return Strings.RelationshipManager_InconsistentReferentialConstraintProperties(stringBuilder.ToString(), stringBuilder2.ToString());
		}

		// Token: 0x04001221 RID: 4641
		private RelationshipEndMember _fromRole;

		// Token: 0x04001222 RID: 4642
		private RelationshipEndMember _toRole;

		// Token: 0x04001223 RID: 4643
		private readonly ReadOnlyMetadataCollection<EdmProperty> _fromProperties;

		// Token: 0x04001224 RID: 4644
		private readonly ReadOnlyMetadataCollection<EdmProperty> _toProperties;
	}
}
