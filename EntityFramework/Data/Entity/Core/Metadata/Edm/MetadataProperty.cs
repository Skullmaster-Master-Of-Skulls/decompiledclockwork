using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004EB RID: 1259
	public class MetadataProperty : MetadataItem
	{
		// Token: 0x06002EEB RID: 12011 RVA: 0x000E06D9 File Offset: 0x000DE8D9
		internal MetadataProperty()
		{
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000E06E1 File Offset: 0x000DE8E1
		internal MetadataProperty(string name, TypeUsage typeUsage, object value)
		{
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._value = value;
			this._typeUsage = typeUsage;
			this._propertyKind = PropertyKind.Extended;
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000E0711 File Offset: 0x000DE911
		internal MetadataProperty(string name, EdmType edmType, bool isCollectionType, object value)
		{
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

		// Token: 0x06002EEE RID: 12014 RVA: 0x000E0751 File Offset: 0x000DE951
		private MetadataProperty(string name, object value)
		{
			this._name = name;
			this._value = value;
			this._propertyKind = PropertyKind.Extended;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x000E076E File Offset: 0x000DE96E
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataProperty;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000E0772 File Offset: 0x000DE972
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000E077A File Offset: 0x000DE97A
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000E0784 File Offset: 0x000DE984
		// (set) Token: 0x06002EF3 RID: 12019 RVA: 0x000E07AD File Offset: 0x000DE9AD
		[MetadataProperty(typeof(object), false)]
		public virtual object Value
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
			set
			{
				Check.NotNull<object>(value, "value");
				Util.ThrowIfReadOnly(this);
				this._value = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000E07C8 File Offset: 0x000DE9C8
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000E07D0 File Offset: 0x000DE9D0
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000E07E0 File Offset: 0x000DE9E0
		public virtual PropertyKind PropertyKind
		{
			get
			{
				return this._propertyKind;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000E07E8 File Offset: 0x000DE9E8
		public bool IsAnnotation
		{
			get
			{
				return this.PropertyKind == PropertyKind.Extended && this.TypeUsage == null;
			}
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000E0800 File Offset: 0x000DEA00
		public static MetadataProperty Create(string name, TypeUsage typeUsage, object value)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			MetadataProperty metadataProperty = new MetadataProperty(name, typeUsage, value);
			metadataProperty.SetReadOnly();
			return metadataProperty;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000E0835 File Offset: 0x000DEA35
		public static MetadataProperty CreateAnnotation(string name, object value)
		{
			Check.NotEmpty(name, "name");
			return new MetadataProperty(name, value);
		}

		// Token: 0x040011CF RID: 4559
		private readonly string _name;

		// Token: 0x040011D0 RID: 4560
		private readonly PropertyKind _propertyKind;

		// Token: 0x040011D1 RID: 4561
		private object _value;

		// Token: 0x040011D2 RID: 4562
		private readonly TypeUsage _typeUsage;
	}
}
