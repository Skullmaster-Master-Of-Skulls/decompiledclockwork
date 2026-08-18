using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001DF RID: 479
	public abstract class EdmMember : MetadataItem
	{
		// Token: 0x06002043 RID: 8259 RVA: 0x0007084C File Offset: 0x0006EA4C
		internal EdmMember(string name, TypeUsage memberTypeUsage)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(memberTypeUsage, "memberTypeUsage");
			this._name = name;
			this._typeUsage = memberTypeUsage;
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x00070879 File Offset: 0x0006EA79
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00070881 File Offset: 0x0006EA81
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x00070889 File Offset: 0x0006EA89
		public StructuralType DeclaringType
		{
			get
			{
				return this._declaringType;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x00070891 File Offset: 0x0006EA91
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00070879 File Offset: 0x0006EA79
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000702E2 File Offset: 0x0006E4E2
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00070899 File Offset: 0x0006EA99
		internal void ChangeDeclaringTypeWithoutCollectionFixup(StructuralType newDeclaringType)
		{
			this._declaringType = newDeclaringType;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x000708A4 File Offset: 0x0006EAA4
		internal bool IsStoreGeneratedComputed
		{
			get
			{
				Facet facet = null;
				return this.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && (StoreGeneratedPattern)facet.Value == StoreGeneratedPattern.Computed;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x000708E0 File Offset: 0x0006EAE0
		internal bool IsStoreGeneratedIdentity
		{
			get
			{
				Facet facet = null;
				return this.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out facet) && (StoreGeneratedPattern)facet.Value == StoreGeneratedPattern.Identity;
			}
		}

		// Token: 0x04000E40 RID: 3648
		private TypeUsage _typeUsage;

		// Token: 0x04000E41 RID: 3649
		private string _name;

		// Token: 0x04000E42 RID: 3650
		private StructuralType _declaringType;
	}
}
