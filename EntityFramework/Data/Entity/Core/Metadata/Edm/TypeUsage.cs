using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000501 RID: 1281
	[DebuggerDisplay("EdmType={EdmType}, Facets.Count={Facets.Count}")]
	public class TypeUsage : MetadataItem
	{
		// Token: 0x06002F97 RID: 12183 RVA: 0x000E4966 File Offset: 0x000E2B66
		internal TypeUsage()
		{
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000E496E File Offset: 0x000E2B6E
		private TypeUsage(EdmType edmType) : base(MetadataItem.MetadataFlags.Readonly)
		{
			Check.NotNull<EdmType>(edmType, "edmType");
			this._edmType = edmType;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000E498C File Offset: 0x000E2B8C
		private TypeUsage(EdmType edmType, IEnumerable<Facet> facets) : this(edmType)
		{
			MetadataCollection<Facet> metadataCollection = MetadataCollection<Facet>.Wrap(facets.ToList<Facet>());
			metadataCollection.SetReadOnly();
			this._facets = metadataCollection.AsReadOnlyMetadataCollection();
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000E49BF File Offset: 0x000E2BBF
		internal static TypeUsage Create(EdmType edmType)
		{
			return new TypeUsage(edmType);
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000E49C7 File Offset: 0x000E2BC7
		internal static TypeUsage Create(EdmType edmType, FacetValues values)
		{
			return new TypeUsage(edmType, TypeUsage.GetDefaultFacetDescriptionsAndOverrideFacetValues(edmType, values));
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000E49D6 File Offset: 0x000E2BD6
		public static TypeUsage Create(EdmType edmType, IEnumerable<Facet> facets)
		{
			return new TypeUsage(edmType, facets);
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000E49DF File Offset: 0x000E2BDF
		internal TypeUsage ShallowCopy(FacetValues facetValues)
		{
			return TypeUsage.Create(this._edmType, TypeUsage.OverrideFacetValues(this.Facets, facetValues));
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000E49F8 File Offset: 0x000E2BF8
		internal TypeUsage ShallowCopy(params Facet[] facetValues)
		{
			return TypeUsage.Create(this._edmType, TypeUsage.OverrideFacetValues(this.Facets, facetValues));
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000E4A1A File Offset: 0x000E2C1A
		private static IEnumerable<Facet> OverrideFacetValues(IEnumerable<Facet> facets, IEnumerable<Facet> facetValues)
		{
			return facets.Except(facetValues, (Facet f1, Facet f2) => f1.EdmEquals(f2)).Union(facetValues);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000E4A46 File Offset: 0x000E2C46
		public static TypeUsage CreateDefaultTypeUsage(EdmType edmType)
		{
			Check.NotNull<EdmType>(edmType, "edmType");
			return TypeUsage.Create(edmType);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000E4A5C File Offset: 0x000E2C5C
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength, int maxLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw new ArgumentException(Strings.NotStringTypeForTypeUsage);
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000E4AD4 File Offset: 0x000E2CD4
		public static TypeUsage CreateStringTypeUsage(PrimitiveType primitiveType, bool isUnicode, bool isFixedLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.String)
			{
				throw new ArgumentException(Strings.NotStringTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				Unicode = new bool?(isUnicode),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000E4B44 File Offset: 0x000E2D44
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength, int maxLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw new ArgumentException(Strings.NotBinaryTypeForTypeUsage);
			}
			TypeUsage.ValidateMaxLength(maxLength);
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = new int?(maxLength),
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000E4BA8 File Offset: 0x000E2DA8
		public static TypeUsage CreateBinaryTypeUsage(PrimitiveType primitiveType, bool isFixedLength)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Binary)
			{
				throw new ArgumentException(Strings.NotBinaryTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				MaxLength = TypeUsage.DefaultMaxLengthFacetValue,
				FixedLength = new bool?(isFixedLength)
			});
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000E4C04 File Offset: 0x000E2E04
		public static TypeUsage CreateDateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTime)
			{
				throw new ArgumentException(Strings.NotDateTimeTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000E4C4C File Offset: 0x000E2E4C
		public static TypeUsage CreateDateTimeOffsetTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.DateTimeOffset)
			{
				throw new ArgumentException(Strings.NotDateTimeOffsetTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000E4C98 File Offset: 0x000E2E98
		public static TypeUsage CreateTimeTypeUsage(PrimitiveType primitiveType, byte? precision)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Time)
			{
				throw new ArgumentException(Strings.NotTimeTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = precision
			});
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000E4CE4 File Offset: 0x000E2EE4
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType, byte precision, byte scale)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw new ArgumentException(Strings.NotDecimalTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = new byte?(precision),
				Scale = new byte?(scale)
			});
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000E4D44 File Offset: 0x000E2F44
		public static TypeUsage CreateDecimalTypeUsage(PrimitiveType primitiveType)
		{
			Check.NotNull<PrimitiveType>(primitiveType, "primitiveType");
			if (primitiveType.PrimitiveTypeKind != PrimitiveTypeKind.Decimal)
			{
				throw new ArgumentException(Strings.NotDecimalTypeForTypeUsage);
			}
			return TypeUsage.Create(primitiveType, new FacetValues
			{
				Precision = TypeUsage.DefaultPrecisionFacetValue,
				Scale = TypeUsage.DefaultScaleFacetValue
			});
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x000E4DA0 File Offset: 0x000E2FA0
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.TypeUsage;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000E4DA4 File Offset: 0x000E2FA4
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public virtual EdmType EdmType
		{
			get
			{
				return this._edmType;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000E4DAC File Offset: 0x000E2FAC
		[MetadataProperty(BuiltInTypeKind.Facet, true)]
		public virtual ReadOnlyMetadataCollection<Facet> Facets
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

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000E4DF0 File Offset: 0x000E2FF0
		[SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		public TypeUsage ModelTypeUsage
		{
			get
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
							TypeUsage modelTypeUsage = edmProperty.TypeUsage.ModelTypeUsage;
							array[i] = new EdmProperty(edmProperty.Name, modelTypeUsage);
						}
						RowType edmType2 = new RowType(array, rowType.InitializerMetadata);
						typeUsage = TypeUsage.Create(edmType2, this.Facets);
					}
					else if (Helper.IsCollectionType(edmType))
					{
						CollectionType collectionType = (CollectionType)edmType;
						TypeUsage modelTypeUsage2 = collectionType.TypeUsage.ModelTypeUsage;
						typeUsage = TypeUsage.Create(new CollectionType(modelTypeUsage2), this.Facets);
					}
					else if (Helper.IsPrimitiveType(edmType))
					{
						typeUsage = ((PrimitiveType)edmType).ProviderManifest.GetEdmType(this);
						if (typeUsage == null)
						{
							throw new ProviderIncompatibleException(Strings.Mapping_ProviderReturnsNullType(this.ToString()));
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
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000E4F6E File Offset: 0x000E316E
		public bool IsSubtypeOf(TypeUsage typeUsage)
		{
			return this.EdmType != null && typeUsage != null && this.EdmType.IsSubtypeOf(typeUsage.EdmType);
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000E4F96 File Offset: 0x000E3196
		private IEnumerable<Facet> GetFacets()
		{
			return from facetDescription in this._edmType.GetAssociatedFacetDescriptions()
			select facetDescription.DefaultValueFacet;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000E4FC5 File Offset: 0x000E31C5
		internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000E4FD0 File Offset: 0x000E31D0
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

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000E5038 File Offset: 0x000E3238
		private static IEnumerable<Facet> GetDefaultFacetDescriptionsAndOverrideFacetValues(EdmType type, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<FacetDescription>(type.GetAssociatedFacetDescriptions(), (FacetDescription fd) => fd, (FacetDescription fd) => fd.DefaultValueFacet, values);
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000E5098 File Offset: 0x000E3298
		private static IEnumerable<Facet> OverrideFacetValues(IEnumerable<Facet> facets, FacetValues values)
		{
			return TypeUsage.OverrideFacetValues<Facet>(facets, (Facet f) => f.Description, (Facet f) => f, values);
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000E5308 File Offset: 0x000E3508
		private static IEnumerable<Facet> OverrideFacetValues<T>(IEnumerable<T> facetThings, Func<T, FacetDescription> getDescription, Func<T, Facet> getFacet, FacetValues values)
		{
			foreach (T thing in facetThings)
			{
				FacetDescription description = getDescription(thing);
				Facet facet;
				if (!description.IsConstant && values.TryGetFacet(description, out facet))
				{
					yield return facet;
				}
				else
				{
					yield return getFacet(thing);
				}
			}
			yield break;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000E533C File Offset: 0x000E353C
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
				if (0 <= Array.BinarySearch<string>(TypeUsage._identityFacets, facet.Name, StringComparer.Ordinal))
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

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000E5408 File Offset: 0x000E3608
		public override string ToString()
		{
			return this.EdmType.ToString();
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000E5418 File Offset: 0x000E3618
		internal override bool EdmEquals(MetadataItem item)
		{
			if (object.ReferenceEquals(this, item))
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

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000E54FC File Offset: 0x000E36FC
		private static void ValidateMaxLength(int maxLength)
		{
			if (maxLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxLength", Strings.InvalidMaxLengthSize);
			}
		}

		// Token: 0x0400122D RID: 4653
		internal const bool DefaultUnicodeFacetValue = true;

		// Token: 0x0400122E RID: 4654
		internal const bool DefaultFixedLengthFacetValue = false;

		// Token: 0x0400122F RID: 4655
		private TypeUsage _modelTypeUsage;

		// Token: 0x04001230 RID: 4656
		private readonly EdmType _edmType;

		// Token: 0x04001231 RID: 4657
		private ReadOnlyMetadataCollection<Facet> _facets;

		// Token: 0x04001232 RID: 4658
		private string _identity;

		// Token: 0x04001233 RID: 4659
		private static readonly string[] _identityFacets = new string[]
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

		// Token: 0x04001234 RID: 4660
		internal static readonly EdmConstants.Unbounded DefaultMaxLengthFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04001235 RID: 4661
		internal static readonly EdmConstants.Unbounded DefaultPrecisionFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04001236 RID: 4662
		internal static readonly EdmConstants.Unbounded DefaultScaleFacetValue = EdmConstants.UnboundedValue;

		// Token: 0x04001237 RID: 4663
		internal static readonly byte? DefaultDateTimePrecisionFacetValue = null;
	}
}
