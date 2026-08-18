using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DC RID: 1244
	public class EdmProperty : EdmMember
	{
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000DCE6F File Offset: 0x000DB06F
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static EdmProperty CreatePrimitive(string name, PrimitiveType primitiveType)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			return EdmProperty.CreateProperty(name, primitiveType);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000DCE90 File Offset: 0x000DB090
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static EdmProperty CreateEnum(string name, EnumType enumType)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<EnumType>(enumType, "enumType");
			return EdmProperty.CreateProperty(name, enumType);
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000DCEB4 File Offset: 0x000DB0B4
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		public static EdmProperty CreateComplex(string name, ComplexType complexType)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<ComplexType>(complexType, "complexType");
			EdmProperty edmProperty = EdmProperty.CreateProperty(name, complexType);
			edmProperty.Nullable = false;
			return edmProperty;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000DCEEC File Offset: 0x000DB0EC
		public static EdmProperty Create(string name, TypeUsage typeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			EdmType edmType = typeUsage.EdmType;
			if (!Helper.IsPrimitiveType(edmType) && !Helper.IsEnumType(edmType) && !Helper.IsComplexType(edmType))
			{
				throw new ArgumentException(Strings.EdmProperty_InvalidPropertyType(edmType.FullName));
			}
			return new EdmProperty(name, typeUsage);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000DCF48 File Offset: 0x000DB148
		private static EdmProperty CreateProperty(string name, EdmType edmType)
		{
			TypeUsage typeUsage = TypeUsage.Create(edmType, new FacetValues());
			return new EdmProperty(name, typeUsage);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000DCF6A File Offset: 0x000DB16A
		internal EdmProperty(string name, TypeUsage typeUsage) : base(name, typeUsage)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000DCF8C File Offset: 0x000DB18C
		internal EdmProperty(string name, TypeUsage typeUsage, PropertyInfo propertyInfo, Type entityDeclaringType) : this(name, typeUsage)
		{
			this._propertyInfo = propertyInfo;
			this._entityDeclaringType = entityDeclaringType;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000DCFA5 File Offset: 0x000DB1A5
		internal EdmProperty(string name) : this(name, TypeUsage.Create(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.String)))
		{
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002DDC RID: 11740 RVA: 0x000DCFBA File Offset: 0x000DB1BA
		internal PropertyInfo PropertyInfo
		{
			get
			{
				return this._propertyInfo;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x000DCFC2 File Offset: 0x000DB1C2
		internal Type EntityDeclaringType
		{
			get
			{
				return this._entityDeclaringType;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000DCFCA File Offset: 0x000DB1CA
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmProperty;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002DDF RID: 11743 RVA: 0x000DCFCE File Offset: 0x000DB1CE
		// (set) Token: 0x06002DE0 RID: 11744 RVA: 0x000DCFF0 File Offset: 0x000DB1F0
		public bool Nullable
		{
			get
			{
				return (bool)this.TypeUsage.Facets["Nullable"].Value;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
				{
					Nullable = new bool?(value)
				});
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x000DD02C File Offset: 0x000DB22C
		public string TypeName
		{
			get
			{
				return this.TypeUsage.EdmType.Name;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000DD03E File Offset: 0x000DB23E
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x000DD05C File Offset: 0x000DB25C
		public object DefaultValue
		{
			get
			{
				return this.TypeUsage.Facets["DefaultValue"].Value;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
				{
					DefaultValue = value
				});
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000DD08E File Offset: 0x000DB28E
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x000DD096 File Offset: 0x000DB296
		internal Func<object, object> ValueGetter
		{
			get
			{
				return this._memberGetter;
			}
			set
			{
				Interlocked.CompareExchange<Func<object, object>>(ref this._memberGetter, value, null);
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000DD0A6 File Offset: 0x000DB2A6
		// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x000DD0AE File Offset: 0x000DB2AE
		internal Action<object, object> ValueSetter
		{
			get
			{
				return this._memberSetter;
			}
			set
			{
				Interlocked.CompareExchange<Action<object, object>>(ref this._memberSetter, value, null);
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000DD0C0 File Offset: 0x000DB2C0
		internal bool IsKeyMember
		{
			get
			{
				EntityType entityType = this.DeclaringType as EntityType;
				return entityType != null && entityType.KeyMembers.Contains(this);
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000DD0EA File Offset: 0x000DB2EA
		public bool IsCollectionType
		{
			get
			{
				return this.TypeUsage.EdmType is CollectionType;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x000DD0FF File Offset: 0x000DB2FF
		public bool IsComplexType
		{
			get
			{
				return this.TypeUsage.EdmType is ComplexType;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x000DD114 File Offset: 0x000DB314
		public bool IsPrimitiveType
		{
			get
			{
				return this.TypeUsage.EdmType is PrimitiveType;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x000DD129 File Offset: 0x000DB329
		public bool IsEnumType
		{
			get
			{
				return this.TypeUsage.EdmType is EnumType;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x000DD13E File Offset: 0x000DB33E
		public bool IsUnderlyingPrimitiveType
		{
			get
			{
				return this.IsPrimitiveType || this.IsEnumType;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000DD150 File Offset: 0x000DB350
		public ComplexType ComplexType
		{
			get
			{
				return this.TypeUsage.EdmType as ComplexType;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000DD162 File Offset: 0x000DB362
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x000DD174 File Offset: 0x000DB374
		public PrimitiveType PrimitiveType
		{
			get
			{
				return this.TypeUsage.EdmType as PrimitiveType;
			}
			internal set
			{
				Check.NotNull<PrimitiveType>(value, "value");
				Util.ThrowIfReadOnly(this);
				StoreGeneratedPattern storeGeneratedPattern = this.StoreGeneratedPattern;
				ConcurrencyMode concurrencyMode = this.ConcurrencyMode;
				List<Facet> list = new List<Facet>();
				foreach (FacetDescription facetDescription in value.GetAssociatedFacetDescriptions())
				{
					Facet facet;
					if (this.TypeUsage.Facets.TryGetValue(facetDescription.FacetName, false, out facet) && ((facet.Value == null && facet.Description.DefaultValue != null) || (facet.Value != null && !facet.Value.Equals(facet.Description.DefaultValue))))
					{
						list.Add(facet);
					}
				}
				this.TypeUsage = TypeUsage.Create(value, FacetValues.Create(list));
				if (storeGeneratedPattern != StoreGeneratedPattern.None)
				{
					this.StoreGeneratedPattern = storeGeneratedPattern;
				}
				if (concurrencyMode != ConcurrencyMode.None)
				{
					this.ConcurrencyMode = concurrencyMode;
				}
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000DD268 File Offset: 0x000DB468
		public EnumType EnumType
		{
			get
			{
				return this.TypeUsage.EdmType as EnumType;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000DD27A File Offset: 0x000DB47A
		public PrimitiveType UnderlyingPrimitiveType
		{
			get
			{
				if (!this.IsUnderlyingPrimitiveType)
				{
					return null;
				}
				if (!this.IsEnumType)
				{
					return this.PrimitiveType;
				}
				return this.EnumType.UnderlyingType;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x000DD2A0 File Offset: 0x000DB4A0
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x000DD2A8 File Offset: 0x000DB4A8
		public ConcurrencyMode ConcurrencyMode
		{
			get
			{
				return MetadataHelper.GetConcurrencyMode(this);
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[]
				{
					Facet.Create(Converter.ConcurrencyModeFacet, value)
				});
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x000DD2E7 File Offset: 0x000DB4E7
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x000DD2F0 File Offset: 0x000DB4F0
		public StoreGeneratedPattern StoreGeneratedPattern
		{
			get
			{
				return MetadataHelper.GetStoreGeneratedPattern(this);
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[]
				{
					Facet.Create(Converter.StoreGeneratedPatternFacet, value)
				});
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000DD330 File Offset: 0x000DB530
		// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x000DD364 File Offset: 0x000DB564
		public CollectionKind CollectionKind
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("CollectionKind", false, out facet))
				{
					return CollectionKind.None;
				}
				return (CollectionKind)facet.Value;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this.TypeUsage = this.TypeUsage.ShallowCopy(new Facet[]
				{
					Facet.Create(MetadataItem.CollectionKindFacetDescription, value)
				});
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x000DD3A4 File Offset: 0x000DB5A4
		public bool IsMaxLengthConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000DD3D8 File Offset: 0x000DB5D8
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x000DD41C File Offset: 0x000DB61C
		public int? MaxLength
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet))
				{
					return null;
				}
				return facet.Value as int?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.MaxLength != value)
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						MaxLength = value
					});
				}
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x000DD484 File Offset: 0x000DB684
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x000DD4B4 File Offset: 0x000DB6B4
		public bool IsMaxLength
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.IsUnbounded;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (value)
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						MaxLength = EdmConstants.UnboundedValue
					});
				}
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000DD4F4 File Offset: 0x000DB6F4
		public bool IsFixedLengthConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("FixedLength", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06002DFF RID: 11775 RVA: 0x000DD528 File Offset: 0x000DB728
		// (set) Token: 0x06002E00 RID: 11776 RVA: 0x000DD56C File Offset: 0x000DB76C
		public bool? IsFixedLength
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("FixedLength", false, out facet))
				{
					return null;
				}
				return facet.Value as bool?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.IsFixedLength != value)
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						FixedLength = value
					});
				}
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000DD5D4 File Offset: 0x000DB7D4
		public bool IsUnicodeConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Unicode", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000DD608 File Offset: 0x000DB808
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x000DD64C File Offset: 0x000DB84C
		public bool? IsUnicode
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Unicode", false, out facet))
				{
					return null;
				}
				return facet.Value as bool?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.IsUnicode != value)
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						Unicode = value
					});
				}
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000DD6B4 File Offset: 0x000DB8B4
		public bool IsPrecisionConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Precision", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000DD6E8 File Offset: 0x000DB8E8
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x000DD72C File Offset: 0x000DB92C
		public byte? Precision
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Precision", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.Precision != value)
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						Precision = value
					});
				}
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x000DD794 File Offset: 0x000DB994
		public bool IsScaleConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Scale", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000DD7C8 File Offset: 0x000DB9C8
		// (set) Token: 0x06002E09 RID: 11785 RVA: 0x000DD80C File Offset: 0x000DBA0C
		public byte? Scale
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Scale", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				if (this.Scale != value)
				{
					this.TypeUsage = this.TypeUsage.ShallowCopy(new FacetValues
					{
						Scale = value
					});
				}
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000DD874 File Offset: 0x000DBA74
		public void SetMetadataProperties(IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotNull<IEnumerable<MetadataProperty>>(metadataProperties, "metadataProperties");
			Util.ThrowIfReadOnly(this);
			base.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
		}

		// Token: 0x04001194 RID: 4500
		private readonly PropertyInfo _propertyInfo;

		// Token: 0x04001195 RID: 4501
		private readonly Type _entityDeclaringType;

		// Token: 0x04001196 RID: 4502
		private Func<object, object> _memberGetter;

		// Token: 0x04001197 RID: 4503
		private Action<object, object> _memberSetter;
	}
}
