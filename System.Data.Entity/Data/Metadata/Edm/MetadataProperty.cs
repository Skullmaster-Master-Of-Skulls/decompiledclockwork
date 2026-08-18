using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E3 RID: 483
	public sealed class MetadataProperty : MetadataItem
	{
		// Token: 0x0600209F RID: 8351 RVA: 0x00072356 File Offset: 0x00070556
		internal MetadataProperty(string name, TypeUsage typeUsage, object value)
		{
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._value = value;
			this._typeUsage = typeUsage;
			this._propertyKind = PropertyKind.Extended;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00072388 File Offset: 0x00070588
		internal MetadataProperty(string name, EdmType edmType, bool isCollectionType, object value)
		{
			EntityUtil.CheckArgumentNull<EdmType>(edmType, "edmType");
			this._name = name;
			this._value = value;
			if (isCollectionType)
			{
				this._typeUsage = TypeUsage.Create(edmType.GetCollectionType());
			}
			else
			{
				this._typeUsage = TypeUsage.Create(edmType);
			}
			this._propertyKind = PropertyKind.System;
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x000723DF File Offset: 0x000705DF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataProperty;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x000723E3 File Offset: 0x000705E3
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x000723EB File Offset: 0x000705EB
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x000723F4 File Offset: 0x000705F4
		[MetadataProperty(typeof(object), false)]
		public object Value
		{
			get
			{
				MetadataPropertyValue metadataPropertyValue = this._value as MetadataPropertyValue;
				if (metadataPropertyValue != null)
				{
					return metadataPropertyValue.GetValue();
				}
				return this._value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0007241D File Offset: 0x0007061D
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000702E2 File Offset: 0x0006E4E2
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x00072425 File Offset: 0x00070625
		public PropertyKind PropertyKind
		{
			get
			{
				return this._propertyKind;
			}
		}

		// Token: 0x04000E50 RID: 3664
		private string _name;

		// Token: 0x04000E51 RID: 3665
		private PropertyKind _propertyKind;

		// Token: 0x04000E52 RID: 3666
		private object _value;

		// Token: 0x04000E53 RID: 3667
		private TypeUsage _typeUsage;
	}
}
