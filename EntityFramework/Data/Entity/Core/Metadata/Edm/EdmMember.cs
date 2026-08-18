using System;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C0 RID: 1216
	public abstract class EdmMember : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06002CC8 RID: 11464 RVA: 0x000DA494 File Offset: 0x000D8694
		internal EdmMember()
		{
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000DA49C File Offset: 0x000D869C
		internal EdmMember(string name, TypeUsage memberTypeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(memberTypeUsage, "memberTypeUsage");
			this._name = name;
			this._typeUsage = memberTypeUsage;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x000DA4CA File Offset: 0x000D86CA
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000DA4D2 File Offset: 0x000D86D2
		internal override string Identity
		{
			get
			{
				return this._identity ?? this.Name;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x000DA4E4 File Offset: 0x000D86E4
		// (set) Token: 0x06002CCD RID: 11469 RVA: 0x000DA508 File Offset: 0x000D8708
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotEmpty(value, "value");
				Util.ThrowIfReadOnly(this);
				if (!string.Equals(this._name, value, StringComparison.Ordinal))
				{
					string identity = this.Identity;
					this._name = value;
					if (this._declaringType != null)
					{
						if (this._declaringType.Members.Except(new EdmMember[]
						{
							this
						}).Any((EdmMember c) => string.Equals(this.Identity, c.Identity, StringComparison.Ordinal)))
						{
							this._identity = (from i in this._declaringType.Members
							select i.Identity).Uniquify(this.Identity);
						}
						this._declaringType.NotifyItemIdentityChanged(this, identity);
					}
				}
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x000DA5D0 File Offset: 0x000D87D0
		public virtual StructuralType DeclaringType
		{
			get
			{
				return this._declaringType;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000DA5D8 File Offset: 0x000D87D8
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x000DA5E0 File Offset: 0x000D87E0
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
			protected set
			{
				Check.NotNull<TypeUsage>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._typeUsage = value;
			}
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x000DA5FB File Offset: 0x000D87FB
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x000DA604 File Offset: 0x000D8804
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				string identity = this._identity;
				this._identity = this.Name;
				if (this._declaringType != null && identity != null && !string.Equals(identity, this._identity, StringComparison.Ordinal))
				{
					this._declaringType.NotifyItemIdentityChanged(this, identity);
				}
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x000DA659 File Offset: 0x000D8859
		internal void ChangeDeclaringTypeWithoutCollectionFixup(StructuralType newDeclaringType)
		{
			this._declaringType = newDeclaringType;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000DA664 File Offset: 0x000D8864
		public bool IsStoreGeneratedComputed
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && (StoreGeneratedPattern)facet.Value == StoreGeneratedPattern.Computed;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x000DA69C File Offset: 0x000D889C
		public bool IsStoreGeneratedIdentity
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && (StoreGeneratedPattern)facet.Value == StoreGeneratedPattern.Identity;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000DA6D4 File Offset: 0x000D88D4
		internal virtual bool IsPrimaryKeyColumn
		{
			get
			{
				EntityTypeBase entityTypeBase = this._declaringType as EntityTypeBase;
				return entityTypeBase != null && entityTypeBase.KeyMembers.Contains(this);
			}
		}

		// Token: 0x0400107E RID: 4222
		private StructuralType _declaringType;

		// Token: 0x0400107F RID: 4223
		private TypeUsage _typeUsage;

		// Token: 0x04001080 RID: 4224
		private string _name;

		// Token: 0x04001081 RID: 4225
		private string _identity;
	}
}
