using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F9 RID: 505
	[DebuggerDisplay("EdmType={EdmType}, Facets.Count={Facets.Count}")]
	public sealed class TypeUsage : MetadataItem
	{
		// Token: 0x06002130 RID: 8496 RVA: 0x00074C56 File Offset: 0x00072E56
		private TypeUsage(EdmType edmType) : base(MetadataItem.MetadataFlags.Readonly)
		{
			EntityUtil.GenericCheckArgumentNull<EdmType>(edmType, "edmType");
			this._edmType = edmType;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00074C74 File Offset: 0x00072E74
		private TypeUsage(EdmType edmType, IEnumerable<Facet> facets) : this(edmType)
		{
			MetadataCollection<Facet> metadataCollection = new MetadataCollection<Facet>(facets);
			metadataCollection.SetReadOnly();
			this._facets = metadataCollection.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x00074CA2 File Offset: 0x00072EA2
		internal static TypeUsage Create(EdmType edmType)
		{
			return new TypeUsage(edmType);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00074CAA File Offset: 0x00072EAA
		internal static TypeUsage Create(EdmType edmType, FacetValues values)
		{
			return new TypeUsage(edmType, TypeUsage.GetDefaultFacetDescriptionsAndOverrideFacetValues(edmType, values));
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00074CB9 File Offset: 0x00072EB9
		internal static TypeUsage Create(EdmType edmType, IEnumerable<Facet> facets)
		{
			return new TypeUsage(edmType, facets);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00074CC2 File Offset: 0x00072EC2
		internal TypeUsage ShallowCopy(FacetValues facetValues)
		{
			return TypeUsage.Create(this._edmType, TypeUsage.OverrideFacetValues(this.Facets, facetValues));
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00074CDC File Offset: 0x00072EDC
		public static TypeUsage CreateDefaultTypeUsage(EdmType edmType)
		{
			EntityUtil.CheckArgumentNull<EdmType>(edmType, "edmType");
			return TypeUsage.Create(edmType);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00074D00 File Offset: 0x00072F00
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength, int maxLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw EntityUtil.NotStringTypeForTypeUsage();
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00074D70 File Offset: 0x00072F70
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw EntityUtil.NotStringTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00074DD8 File Offset: 0x00072FD8
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength, int maxLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw EntityUtil.NotBinaryTypeForTypeUsage();
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00074E34 File Offset: 0x00073034
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw EntityUtil.NotBinaryTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00074E8C File Offset: 0x0007308C
		public static TypeUsage CreateDateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTime)
			{
				throw EntityUtil.NotDateTimeTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00074ED0 File Offset: 0x000730D0
		public static TypeUsage CreateDateTimeOffsetTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTimeOffset)
			{
				throw EntityUtil.NotDateTimeOffsetTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00074F14 File Offset: 0x00073114
		public static TypeUsage CreateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Time)
			{
				throw EntityUtil.NotTimeTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00074F58 File Offset: 0x00073158
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType, byte precision, byte scale)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw EntityUtil.NotDecimalTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = new byte?(precision),
				Scale = new byte?(scale)
			});
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00074FB0 File Offset: 0x000731B0
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType)
		{
			EntityUtil.CheckArgumentNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw EntityUtil.NotDecimalTypeForTypeUsage();
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = TypeUsage.DefaultPrecisionFacetValue,
				Scale = TypeUsage.DefaultScaleFacetValue
			});
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x00075005 File Offset: 0x00073205
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.TypeUsage;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00075009 File Offset: 0x00073209
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public EdmType EdmType
		{
			get
			{
				return this._edmType;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00075014 File Offset: 0x00073214
		[MetadataProperty(BuiltInTypeKind.Facet, true)]
		public ReadOnlyMetadataCollection<Facet> Facets
		{
			get
			{
				if (this._facets == null)
				{
					MetadataCollection<Facet> metadataCollection = new MetadataCollection<Facet>(this.GetFacets());
					metadataCollection.SetReadOnly();
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<Facet>>(ref this._facets, metadataCollection.AsReadOnlyMetadataCollection(), null);
				}
				return this._facets;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00075058 File Offset: 0x00073258
		internal TypeUsage GetModelTypeUsage()
		{
			if (this._modelTypeUsage == null)
			{
				EdmType edmType = this.EdmType;
				if (edmType.DataSpace == DataSpace.CSpace || edmType.DataSpace == DataSpace.OSpace)
				{
					return this;
				}
				TypeUsage typeUsage;
				if (Helper.IsRowType(edmType))
				{
					RowType rowType = (RowType)edmType;
					EdmProperty[] array = new EdmProperty[rowType.Properties.Count];
					for (int i = 0; i < array.Length; i++)
					{
						EdmProperty edmProperty = rowType.Properties[i];
						TypeUsage modelTypeUsage = edmProperty.TypeUsage.GetModelTypeUsage();
						array[i] = new EdmProperty(edmProperty.Name, modelTypeUsage);
					}
					RowType edmType2 = new RowType(array, rowType.InitializerMetadata);
					typeUsage = TypeUsage.Create(edmType2, this.Facets);
				}
				else if (Helper.IsCollectionType(edmType))
				{
					CollectionType collectionType = (CollectionType)edmType;
					TypeUsage modelTypeUsage2 = collectionType.TypeUsage.GetModelTypeUsage();
					typeUsage = TypeUsage.Create(new CollectionType(modelTypeUsage2), this.Facets);
				}
				else if (Helper.IsRefType(edmType))
				{
					typeUsage = this;
				}
				else if (Helper.IsPrimitiveType(edmType))
				{
					typeUsage = ((PrimitiveType)edmType).ProviderManifest.GetEdmType(this);
					if (typeUsage == null)
					{
						throw EntityUtil.ProviderIncompatible(Strings.Mapping_ProviderReturnsNullType(this.ToString()));
					}
					if (!TypeSemantics.IsNullable(this))
					{
						typeUsage = TypeUsage.Create(typeUsage.EdmType, TypeUsage.OverrideFacetValues(typeUsage.Facets, new FacetValues
						{
							Nullable = new bool?(false)
						}));
					}
				}
				else
				{
					if (!Helper.IsEntityTypeBase(edmType) && !Helper.IsComplexType(edmType))
					{
						return null;
					}
					typeUsage = this;
				}
				Interlocked.CompareExchange<TypeUsage>(ref this._modelTypeUsage, typeUsage, null);
			}
			return this._modelTypeUsage;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000751DD File Offset: 0x000733DD
		public bool IsSubtypeOf(TypeUsage typeUsage)
		{
			return this.EdmType != null && typeUsage != null && this.EdmType.IsSubtypeOf(typeUsage.EdmType);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000751FD File Offset: 0x000733FD
		private IEnumerable<Facet> GetFacets()
		{
			foreach (FacetDescription facetDescription in this._edmType.GetAssociatedFacetDescriptions())
			{
				yield return facetDescription.DefaultValueFacet;
			}
			IEnumerator<FacetDescription> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0007520D File Offset: 0x0007340D
		internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x00075218 File Offset: 0x00073418
		internal override string Identity
		{
			get
			{
				if (this.Facets.Count == 0)
				{
					return this.EdmType.Identity;
				}
				if (this._identity == null)
				{
					StringBuilder stringBuilder = new StringBuilder(128);
					this.BuildIdentity(stringBuilder);
					string value = stringBuilder.ToString();
					Interlocked.CompareExchange<string>(ref this._identity, value, null);
				}
				return this._identity;
			}
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00075274 File Offset: 0x00073474
		private static IEnumerable<Facet> GetDefaultFacetDescriptionsAndOverrideFacetValues(EdmType type, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<FacetDescription>(type.GetAssociatedFacetDescriptions(), (FacetDescription fd) => fd, (FacetDescription fd) => fd.DefaultValueFacet, values);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000752CC File Offset: 0x000734CC
		private static IEnumerable<Facet> OverrideFacetValues(IEnumerable<Facet> facets, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<Facet>(facets, (Facet f) => f.Description, (Facet f) => f, values);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0007531E File Offset: 0x0007351E
		private static IEnumerable<Facet> OverrideFacetValues<T>(IEnumerable<T> facetThings, Func<T, FacetDescription> getDescription, Func<T, Facet> getFacet, FacetValues values)
		{
			foreach (T arg in facetThings)
			{
				FacetDescription facetDescription = getDescription(arg);
				Facet facet;
				if (!facetDescription.IsConstant && values.TryGetFacet(facetDescription, out facet))
				{
					yield return facet;
				}
				else
				{
					yield return getFacet(arg);
				}
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x00075344 File Offset: 0x00073544
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (this._identity != null)
			{
				builder.Append(this._identity);
				return;
			}
			builder.Append(this.EdmType.Identity);
			builder.Append("(");
			bool flag = true;
			for (int i = 0; i < this.Facets.Count; i++)
			{
				Facet facet = this.Facets[i];
				if (0 <= Array.BinarySearch<string>(TypeUsage.s_identityFacets, facet.Name, StringComparer.Ordinal))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						builder.Append(",");
					}
					builder.Append(facet.Name);
					builder.Append("=");
					builder.Append(facet.Value ?? string.Empty);
				}
			}
			builder.Append(")");
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x00075410 File Offset: 0x00073610
		public override string ToString()
		{
			return this.EdmType.ToString();
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00075420 File Offset: 0x00073620
		internal override bool EdmEquals(MetadataItem item)
		{
			if (this == item)
			{
				return true;
			}
			if (item == null || BuiltInTypeKind.TypeUsage != item.BuiltInTypeKind)
			{
				return false;
			}
			TypeUsage typeUsage = (TypeUsage)item;
			if (!this.EdmType.EdmEquals(typeUsage.EdmType))
			{
				return false;
			}
			if (this._facets == null && typeUsage._facets == null)
			{
				return true;
			}
			if (this.Facets.Count != typeUsage.Facets.Count)
			{
				return false;
			}
			foreach (Facet facet in this.Facets)
			{
				Facet facet2;
				if (!typeUsage.Facets.TryGetValue(facet.Name, false, out facet2))
				{
					return false;
				}
				if (!object.Equals(facet.Value, facet2.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00075500 File Offset: 0x00073700
		private static void ValidateMaxLength(int maxLength)
		{
			if (maxLength <= 0)
			{
				throw EntityUtil.ArgumentOutOfRange(Strings.InvalidMaxLengthSize, "maxLength");
			}
		}

		// Token: 0x04000EAD RID: 3757
		private TypeUsage _modelTypeUsage;

		// Token: 0x04000EAE RID: 3758
		private readonly EdmType _edmType;

		// Token: 0x04000EAF RID: 3759
		private ReadOnlyMetadataCollection<Facet> _facets;

		// Token: 0x04000EB0 RID: 3760
		private string _identity;

		// Token: 0x04000EB1 RID: 3761
		private static readonly string[] s_identityFacets = new string[]
		{
			"DefaultValue",
			"FixedLength",
			"MaxLength",
			"Nullable",
			"Precision",
			"Scale",
			"Unicode",
			"SRID"
		};

		// Token: 0x04000EB2 RID: 3762
		internal static readonly EdmConstants.Unbounded DefaultMaxLengthFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04000EB3 RID: 3763
		internal static readonly EdmConstants.Unbounded DefaultPrecisionFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04000EB4 RID: 3764
		internal static readonly EdmConstants.Unbounded DefaultScaleFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04000EB5 RID: 3765
		internal static readonly bool DefaultUnicodeFacetValue = true;

		// Token: 0x04000EB6 RID: 3766
		internal static readonly bool DefaultFixedLengthFacetValue = false;

		// Token: 0x04000EB7 RID: 3767
		internal static readonly byte? DefaultDateTimePrecisionFacetValue = null;
	}
}
