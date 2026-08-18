using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E5 RID: 1253
	internal class FacetValues
	{
		// Token: 0x170006DA RID: 1754
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x000DEFF1 File Offset: 0x000DD1F1
		internal FacetValueContainer<bool?> Nullable
		{
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x000DEFFA File Offset: 0x000DD1FA
		internal FacetValueContainer<int?> MaxLength
		{
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x000DF003 File Offset: 0x000DD203
		internal FacetValueContainer<bool?> Unicode
		{
			set
			{
				this._unicode = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x000DF00C File Offset: 0x000DD20C
		internal FacetValueContainer<bool?> FixedLength
		{
			set
			{
				this._fixedLength = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (set) Token: 0x06002E98 RID: 11928 RVA: 0x000DF015 File Offset: 0x000DD215
		internal FacetValueContainer<byte?> Precision
		{
			set
			{
				this._precision = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (set) Token: 0x06002E99 RID: 11929 RVA: 0x000DF01E File Offset: 0x000DD21E
		internal FacetValueContainer<byte?> Scale
		{
			set
			{
				this._scale = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x000DF027 File Offset: 0x000DD227
		internal object DefaultValue
		{
			set
			{
				this._defaultValue = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (set) Token: 0x06002E9B RID: 11931 RVA: 0x000DF030 File Offset: 0x000DD230
		internal FacetValueContainer<string> Collation
		{
			set
			{
				this._collation = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (set) Token: 0x06002E9C RID: 11932 RVA: 0x000DF039 File Offset: 0x000DD239
		internal FacetValueContainer<int?> Srid
		{
			set
			{
				this._srid = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (set) Token: 0x06002E9D RID: 11933 RVA: 0x000DF042 File Offset: 0x000DD242
		internal FacetValueContainer<bool?> IsStrict
		{
			set
			{
				this._isStrict = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x000DF04B File Offset: 0x000DD24B
		internal FacetValueContainer<StoreGeneratedPattern?> StoreGeneratedPattern
		{
			set
			{
				this._storeGeneratedPattern = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (set) Token: 0x06002E9F RID: 11935 RVA: 0x000DF054 File Offset: 0x000DD254
		internal FacetValueContainer<ConcurrencyMode?> ConcurrencyMode
		{
			set
			{
				this._concurrencyMode = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000DF05D File Offset: 0x000DD25D
		internal FacetValueContainer<CollectionKind?> CollectionKind
		{
			set
			{
				this._collectionKind = value;
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000DF068 File Offset: 0x000DD268
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal bool TryGetFacet(FacetDescription description, out Facet facet)
		{
			string facetName;
			switch (facetName = description.FacetName)
			{
			case "Nullable":
				if (this._nullable.HasValue)
				{
					facet = Facet.Create(description, this._nullable.GetValueAsObject());
					return true;
				}
				break;
			case "MaxLength":
				if (this._maxLength.HasValue)
				{
					facet = Facet.Create(description, this._maxLength.GetValueAsObject());
					return true;
				}
				break;
			case "Unicode":
				if (this._unicode.HasValue)
				{
					facet = Facet.Create(description, this._unicode.GetValueAsObject());
					return true;
				}
				break;
			case "FixedLength":
				if (this._fixedLength.HasValue)
				{
					facet = Facet.Create(description, this._fixedLength.GetValueAsObject());
					return true;
				}
				break;
			case "Precision":
				if (this._precision.HasValue)
				{
					facet = Facet.Create(description, this._precision.GetValueAsObject());
					return true;
				}
				break;
			case "Scale":
				if (this._scale.HasValue)
				{
					facet = Facet.Create(description, this._scale.GetValueAsObject());
					return true;
				}
				break;
			case "DefaultValue":
				if (this._defaultValue != null)
				{
					facet = Facet.Create(description, this._defaultValue);
					return true;
				}
				break;
			case "Collation":
				if (this._collation.HasValue)
				{
					facet = Facet.Create(description, this._collation.GetValueAsObject());
					return true;
				}
				break;
			case "SRID":
				if (this._srid.HasValue)
				{
					facet = Facet.Create(description, this._srid.GetValueAsObject());
					return true;
				}
				break;
			case "IsStrict":
				if (this._isStrict.HasValue)
				{
					facet = Facet.Create(description, this._isStrict.GetValueAsObject());
					return true;
				}
				break;
			case "StoreGeneratedPattern":
				if (this._storeGeneratedPattern.HasValue)
				{
					facet = Facet.Create(description, this._storeGeneratedPattern.GetValueAsObject());
					return true;
				}
				break;
			case "ConcurrencyMode":
				if (this._concurrencyMode.HasValue)
				{
					facet = Facet.Create(description, this._concurrencyMode.GetValueAsObject());
					return true;
				}
				break;
			case "CollectionKind":
				if (this._collectionKind.HasValue)
				{
					facet = Facet.Create(description, this._collectionKind.GetValueAsObject());
					return true;
				}
				break;
			}
			facet = null;
			return false;
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000DF360 File Offset: 0x000DD560
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public static FacetValues Create(IEnumerable<Facet> facets)
		{
			FacetValues facetValues = new FacetValues();
			foreach (Facet facet in facets)
			{
				FacetDescription description = facet.Description;
				string facetName;
				switch (facetName = description.FacetName)
				{
				case "Nullable":
					facetValues.Nullable = (bool?)facet.Value;
					break;
				case "MaxLength":
				{
					EdmConstants.Unbounded unbounded = facet.Value as EdmConstants.Unbounded;
					if (unbounded != null)
					{
						facetValues.MaxLength = unbounded;
					}
					else
					{
						facetValues.MaxLength = (int?)facet.Value;
					}
					break;
				}
				case "Unicode":
					facetValues.Unicode = (bool?)facet.Value;
					break;
				case "FixedLength":
					facetValues.FixedLength = (bool?)facet.Value;
					break;
				case "Precision":
				{
					EdmConstants.Unbounded unbounded2 = facet.Value as EdmConstants.Unbounded;
					if (unbounded2 != null)
					{
						facetValues.Precision = unbounded2;
					}
					else
					{
						facetValues.Precision = (byte?)facet.Value;
					}
					break;
				}
				case "Scale":
				{
					EdmConstants.Unbounded unbounded3 = facet.Value as EdmConstants.Unbounded;
					if (unbounded3 != null)
					{
						facetValues.Scale = unbounded3;
					}
					else
					{
						facetValues.Scale = (byte?)facet.Value;
					}
					break;
				}
				case "DefaultValue":
					facetValues.DefaultValue = facet.Value;
					break;
				case "Collation":
					facetValues.Collation = (string)facet.Value;
					break;
				case "SRID":
					facetValues.Srid = (int?)facet.Value;
					break;
				case "IsStrict":
					facetValues.IsStrict = (bool?)facet.Value;
					break;
				case "StoreGeneratedPattern":
					facetValues.StoreGeneratedPattern = (StoreGeneratedPattern?)facet.Value;
					break;
				case "ConcurrencyMode":
					facetValues.ConcurrencyMode = (ConcurrencyMode?)facet.Value;
					break;
				case "CollectionKind":
					facetValues.CollectionKind = (CollectionKind?)facet.Value;
					break;
				}
			}
			return facetValues;
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000DF68C File Offset: 0x000DD88C
		internal static FacetValues NullFacetValues
		{
			get
			{
				return new FacetValues
				{
					FixedLength = null,
					MaxLength = null,
					Precision = null,
					Scale = null,
					Unicode = null,
					Collation = null,
					Srid = null,
					IsStrict = null,
					ConcurrencyMode = null,
					StoreGeneratedPattern = null,
					CollectionKind = null
				};
			}
		}

		// Token: 0x040011B9 RID: 4537
		private FacetValueContainer<bool?> _nullable;

		// Token: 0x040011BA RID: 4538
		private FacetValueContainer<int?> _maxLength;

		// Token: 0x040011BB RID: 4539
		private FacetValueContainer<bool?> _unicode;

		// Token: 0x040011BC RID: 4540
		private FacetValueContainer<bool?> _fixedLength;

		// Token: 0x040011BD RID: 4541
		private FacetValueContainer<byte?> _precision;

		// Token: 0x040011BE RID: 4542
		private FacetValueContainer<byte?> _scale;

		// Token: 0x040011BF RID: 4543
		private object _defaultValue;

		// Token: 0x040011C0 RID: 4544
		private FacetValueContainer<string> _collation;

		// Token: 0x040011C1 RID: 4545
		private FacetValueContainer<int?> _srid;

		// Token: 0x040011C2 RID: 4546
		private FacetValueContainer<bool?> _isStrict;

		// Token: 0x040011C3 RID: 4547
		private FacetValueContainer<StoreGeneratedPattern?> _storeGeneratedPattern;

		// Token: 0x040011C4 RID: 4548
		private FacetValueContainer<ConcurrencyMode?> _concurrencyMode;

		// Token: 0x040011C5 RID: 4549
		private FacetValueContainer<CollectionKind?> _collectionKind;
	}
}
