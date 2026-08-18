using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000212 RID: 530
	internal class FacetValues
	{
		// Token: 0x170006E7 RID: 1767
		// (set) Token: 0x060022FF RID: 8959 RVA: 0x0007C645 File Offset: 0x0007A845
		internal FacetValueContainer<bool?> Nullable
		{
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x0007C64E File Offset: 0x0007A84E
		internal FacetValueContainer<int?> MaxLength
		{
			set
			{
				this._maxLength = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (set) Token: 0x06002301 RID: 8961 RVA: 0x0007C657 File Offset: 0x0007A857
		internal FacetValueContainer<bool?> Unicode
		{
			set
			{
				this._unicode = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x0007C660 File Offset: 0x0007A860
		internal FacetValueContainer<bool?> FixedLength
		{
			set
			{
				this._fixedLength = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (set) Token: 0x06002303 RID: 8963 RVA: 0x0007C669 File Offset: 0x0007A869
		internal FacetValueContainer<byte?> Precision
		{
			set
			{
				this._precision = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x0007C672 File Offset: 0x0007A872
		internal FacetValueContainer<byte?> Scale
		{
			set
			{
				this._scale = value;
			}
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0007C67C File Offset: 0x0007A87C
		internal bool TryGetFacet(FacetDescription description, out Facet facet)
		{
			if (description.FacetName == "Nullable")
			{
				if (this._nullable.HasValue)
				{
					facet = Facet.Create(description, this._nullable.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "MaxLength")
			{
				if (this._maxLength.HasValue)
				{
					facet = Facet.Create(description, this._maxLength.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "Unicode")
			{
				if (this._unicode.HasValue)
				{
					facet = Facet.Create(description, this._unicode.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "FixedLength")
			{
				if (this._fixedLength.HasValue)
				{
					facet = Facet.Create(description, this._fixedLength.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "Precision")
			{
				if (this._precision.HasValue)
				{
					facet = Facet.Create(description, this._precision.GetValueAsObject());
					return true;
				}
			}
			else if (description.FacetName == "Scale" && this._scale.HasValue)
			{
				facet = Facet.Create(description, this._scale.GetValueAsObject());
				return true;
			}
			facet = null;
			return false;
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0007C7D0 File Offset: 0x0007A9D0
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
					Unicode = null
				};
			}
		}

		// Token: 0x04000F92 RID: 3986
		private FacetValueContainer<bool?> _nullable;

		// Token: 0x04000F93 RID: 3987
		private FacetValueContainer<int?> _maxLength;

		// Token: 0x04000F94 RID: 3988
		private FacetValueContainer<bool?> _unicode;

		// Token: 0x04000F95 RID: 3989
		private FacetValueContainer<bool?> _fixedLength;

		// Token: 0x04000F96 RID: 3990
		private FacetValueContainer<byte?> _precision;

		// Token: 0x04000F97 RID: 3991
		private FacetValueContainer<byte?> _scale;
	}
}
