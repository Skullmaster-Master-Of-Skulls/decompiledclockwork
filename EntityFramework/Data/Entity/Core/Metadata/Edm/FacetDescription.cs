using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E3 RID: 1251
	public class FacetDescription
	{
		// Token: 0x06002E7C RID: 11900 RVA: 0x000DEA6E File Offset: 0x000DCC6E
		internal FacetDescription()
		{
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000DEA78 File Offset: 0x000DCC78
		internal FacetDescription(string facetName, EdmType facetType, int? minValue, int? maxValue, object defaultValue, bool isConstant, string declaringTypeName)
		{
			this._facetName = facetName;
			this._facetType = facetType;
			this._minValue = minValue;
			this._maxValue = maxValue;
			if (defaultValue != null)
			{
				this._defaultValue = defaultValue;
			}
			else
			{
				this._defaultValue = FacetDescription._notInitializedSentinel;
			}
			this._isConstant = isConstant;
			this.Validate(declaringTypeName);
			if (this._isConstant)
			{
				FacetDescription.UpdateMinMaxValueForConstant(this._facetName, this._facetType, ref this._minValue, ref this._maxValue, this._defaultValue);
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000DEAFC File Offset: 0x000DCCFC
		internal FacetDescription(string facetName, EdmType facetType, int? minValue, int? maxValue, object defaultValue)
		{
			Check.NotEmpty(facetName, "facetName");
			Check.NotNull<EdmType>(facetType, "facetType");
			if ((minValue != null || maxValue != null) && minValue != null)
			{
				bool flag = maxValue != null;
			}
			this._facetName = facetName;
			this._facetType = facetType;
			this._minValue = minValue;
			this._maxValue = maxValue;
			this._defaultValue = defaultValue;
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000DEB6F File Offset: 0x000DCD6F
		public virtual string FacetName
		{
			get
			{
				return this._facetName;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000DEB77 File Offset: 0x000DCD77
		public EdmType FacetType
		{
			get
			{
				return this._facetType;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000DEB7F File Offset: 0x000DCD7F
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x000DEB87 File Offset: 0x000DCD87
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000DEB8F File Offset: 0x000DCD8F
		public object DefaultValue
		{
			get
			{
				if (this._defaultValue == FacetDescription._notInitializedSentinel)
				{
					return null;
				}
				return this._defaultValue;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000DEBA6 File Offset: 0x000DCDA6
		public virtual bool IsConstant
		{
			get
			{
				return this._isConstant;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000DEBAE File Offset: 0x000DCDAE
		public bool IsRequired
		{
			get
			{
				return this._defaultValue == FacetDescription._notInitializedSentinel;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x000DEBC0 File Offset: 0x000DCDC0
		internal Facet DefaultValueFacet
		{
			get
			{
				if (this._defaultValueFacet == null)
				{
					Facet value = Facet.Create(this, this.DefaultValue, true);
					Interlocked.CompareExchange<Facet>(ref this._defaultValueFacet, value, null);
				}
				return this._defaultValueFacet;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000DEBF8 File Offset: 0x000DCDF8
		internal Facet NullValueFacet
		{
			get
			{
				if (this._nullValueFacet == null)
				{
					Facet value = Facet.Create(this, null, true);
					Interlocked.CompareExchange<Facet>(ref this._nullValueFacet, value, null);
				}
				return this._nullValueFacet;
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000DEC2A File Offset: 0x000DCE2A
		public override string ToString()
		{
			return this.FacetName;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000DEC34 File Offset: 0x000DCE34
		internal Facet GetBooleanFacet(bool value)
		{
			if (this._valueCache == null)
			{
				Interlocked.CompareExchange<Facet[]>(ref this._valueCache, new Facet[]
				{
					Facet.Create(this, true, true),
					Facet.Create(this, false, true)
				}, null);
			}
			if (!value)
			{
				return this._valueCache[1];
			}
			return this._valueCache[0];
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000DEC94 File Offset: 0x000DCE94
		internal static bool IsNumericType(EdmType facetType)
		{
			if (Helper.IsPrimitiveType(facetType))
			{
				PrimitiveType primitiveType = (PrimitiveType)facetType;
				return primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Byte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.SByte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int16 || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int32;
			}
			return false;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000DECDC File Offset: 0x000DCEDC
		private static void UpdateMinMaxValueForConstant(string facetName, EdmType facetType, ref int? minValue, ref int? maxValue, object defaultValue)
		{
			if (FacetDescription.IsNumericType(facetType))
			{
				if (facetName == "Precision" || facetName == "Scale")
				{
					byte? b = (byte?)defaultValue;
					minValue = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
					byte? b2 = (byte?)defaultValue;
					maxValue = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
					return;
				}
				minValue = (int?)defaultValue;
				maxValue = (int?)defaultValue;
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000DED88 File Offset: 0x000DCF88
		private void Validate(string declaringTypeName)
		{
			if (this._defaultValue == FacetDescription._notInitializedSentinel)
			{
				if (this._isConstant)
				{
					throw new ArgumentException(Strings.MissingDefaultValueForConstantFacet(this._facetName, declaringTypeName));
				}
			}
			else if (FacetDescription.IsNumericType(this._facetType))
			{
				if (this._isConstant)
				{
					if (this._minValue != null != (this._maxValue != null) || (this._minValue != null && this._minValue.Value != this._maxValue.Value))
					{
						throw new ArgumentException(Strings.MinAndMaxValueMustBeSameForConstantFacet(this._facetName, declaringTypeName));
					}
				}
				else
				{
					if (this._minValue == null || this._maxValue == null)
					{
						throw new ArgumentException(Strings.BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(this._facetName, declaringTypeName));
					}
					if (this._minValue.Value == this._maxValue)
					{
						throw new ArgumentException(Strings.MinAndMaxValueMustBeDifferentForNonConstantFacet(this._facetName, declaringTypeName));
					}
					if (this._minValue < 0 || this._maxValue < 0)
					{
						throw new ArgumentException(Strings.MinAndMaxMustBePositive(this._facetName, declaringTypeName));
					}
					if (this._minValue > this._maxValue)
					{
						throw new ArgumentException(Strings.MinMustBeLessThanMax(this._minValue.ToString(), this._facetName, declaringTypeName));
					}
				}
			}
		}

		// Token: 0x040011AC RID: 4524
		private readonly string _facetName;

		// Token: 0x040011AD RID: 4525
		private readonly EdmType _facetType;

		// Token: 0x040011AE RID: 4526
		private readonly int? _minValue;

		// Token: 0x040011AF RID: 4527
		private readonly int? _maxValue;

		// Token: 0x040011B0 RID: 4528
		private readonly object _defaultValue;

		// Token: 0x040011B1 RID: 4529
		private readonly bool _isConstant;

		// Token: 0x040011B2 RID: 4530
		private Facet _defaultValueFacet;

		// Token: 0x040011B3 RID: 4531
		private Facet _nullValueFacet;

		// Token: 0x040011B4 RID: 4532
		private Facet[] _valueCache;

		// Token: 0x040011B5 RID: 4533
		private static readonly object _notInitializedSentinel = new object();
	}
}
