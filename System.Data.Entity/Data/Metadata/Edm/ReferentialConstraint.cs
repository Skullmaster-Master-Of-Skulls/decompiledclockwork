using System;
using System.Collections.Generic;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F0 RID: 496
	public sealed class ReferentialConstraint : MetadataItem
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x0007464C File Offset: 0x0007284C
		internal ReferentialConstraint(RelationshipEndMember fromRole, RelationshipEndMember toRole, IEnumerable<EdmProperty> fromProperties, IEnumerable<EdmProperty> toProperties)
		{
			this._fromRole = EntityUtil.GenericCheckArgumentNull<RelationshipEndMember>(fromRole, "fromRole");
			this._toRole = EntityUtil.GenericCheckArgumentNull<RelationshipEndMember>(toRole, "toRole");
			this._fromProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(EntityUtil.GenericCheckArgumentNull<IEnumerable<EdmProperty>>(fromProperties, "fromProperties")));
			this._toProperties = new ReadOnlyMetadataCollection<EdmProperty>(new MetadataCollection<EdmProperty>(EntityUtil.GenericCheckArgumentNull<IEnumerable<EdmProperty>>(toProperties, "toProperties")));
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x000746B8 File Offset: 0x000728B8
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ReferentialConstraint;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x000746BC File Offset: 0x000728BC
		internal override string Identity
		{
			get
			{
				return this.FromRole.Name + "_" + this.ToRole.Name;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x000746DE File Offset: 0x000728DE
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember FromRole
		{
			get
			{
				return this._fromRole;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x000746E6 File Offset: 0x000728E6
		[MetadataProperty(BuiltInTypeKind.RelationshipEndMember, false)]
		public RelationshipEndMember ToRole
		{
			get
			{
				return this._toRole;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x000746EE File Offset: 0x000728EE
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> FromProperties
		{
			get
			{
				return this._fromProperties;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x000746F6 File Offset: 0x000728F6
		[MetadataProperty(BuiltInTypeKind.EdmProperty, true)]
		public ReadOnlyMetadataCollection<EdmProperty> ToProperties
		{
			get
			{
				return this._toProperties;
			}
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000746BC File Offset: 0x000728BC
		public override string ToString()
		{
			return this.FromRole.Name + "_" + this.ToRole.Name;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00074700 File Offset: 0x00072900
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
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
				this.FromProperties.Source.SetReadOnly();
				this.ToProperties.Source.SetReadOnly();
			}
		}

		// Token: 0x04000E9D RID: 3741
		private RelationshipEndMember _fromRole;

		// Token: 0x04000E9E RID: 3742
		private RelationshipEndMember _toRole;

		// Token: 0x04000E9F RID: 3743
		private readonly ReadOnlyMetadataCollection<EdmProperty> _fromProperties;

		// Token: 0x04000EA0 RID: 3744
		private readonly ReadOnlyMetadataCollection<EdmProperty> _toProperties;
	}
}
