using System;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D7 RID: 471
	public sealed class FacetDescription
	{
		// Token: 0x06001FDE RID: 8158 RVA: 0x0006F624 File Offset: 0x0006D824
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

		// Token: 0x06001FDF RID: 8159 RVA: 0x0006F6A8 File Offset: 0x0006D8A8
		internal FacetDescription(string facetName, EdmType facetType, int? minValue, int? maxValue, object defaultValue)
		{
			EntityUtil.CheckStringArgument(facetName, "facetName");
			EntityUtil.GenericCheckArgumentNull<EdmType>(facetType, "facetType");
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

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x0006F71A File Offset: 0x0006D91A
		public string FacetName
		{
			get
			{
				return this._facetName;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0006F722 File Offset: 0x0006D922
		public EdmType FacetType
		{
			get
			{
				return this._facetType;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0006F72A File Offset: 0x0006D92A
		public int? MinValue
		{
			get
			{
				return this._minValue;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0006F732 File Offset: 0x0006D932
		public int? MaxValue
		{
			get
			{
				return this._maxValue;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x0006F73A File Offset: 0x0006D93A
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

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0006F751 File Offset: 0x0006D951
		public bool IsConstant
		{
			get
			{
				return this._isConstant;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x0006F759 File Offset: 0x0006D959
		public bool IsRequired
		{
			get
			{
				return this._defaultValue == FacetDescription._notInitializedSentinel;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0006F768 File Offset: 0x0006D968
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

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x0006F7A0 File Offset: 0x0006D9A0
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

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0006F7D2 File Offset: 0x0006D9D2
		public override string ToString()
		{
			return this.FacetName;
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0006F7DC File Offset: 0x0006D9DC
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

		// Token: 0x06001FEB RID: 8171 RVA: 0x0006F83C File Offset: 0x0006DA3C
		internal static bool IsNumericType(EdmType facetType)
		{
			if (Helper.IsPrimitiveType(facetType))
			{
				PrimitiveType primitiveType = (PrimitiveType)facetType;
				return primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Byte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.SByte || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int16 || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Int32;
			}
			return false;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0006F884 File Offset: 0x0006DA84
		private static void UpdateMinMaxValueForConstant(string facetName, EdmType facetType, ref int? minValue, ref int? maxValue, object defaultValue)
		{
			if (FacetDescription.IsNumericType(facetType))
			{
				if (facetName == "Precision" || facetName == "Scale")
				{
					byte? b = (byte?)defaultValue;
					minValue = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
					b = (byte?)defaultValue;
					maxValue = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
					return;
				}
				minValue = (int?)defaultValue;
				maxValue = (int?)defaultValue;
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0006F930 File Offset: 0x0006DB30
		private void Validate(string declaringTypeName)
		{
			if (this._defaultValue == FacetDescription._notInitializedSentinel)
			{
				if (this._isConstant)
				{
					throw EntityUtil.MissingDefaultValueForConstantFacet(this._facetName, declaringTypeName);
				}
			}
			else if (FacetDescription.IsNumericType(this._facetType))
			{
				if (this._isConstant)
				{
					if (this._minValue != null != (this._maxValue != null) || (this._minValue != null && this._minValue.Value != this._maxValue.Value))
					{
						throw EntityUtil.MinAndMaxValueMustBeSameForConstantFacet(this._facetName, declaringTypeName);
					}
				}
				else
				{
					if (this._minValue == null || this._maxValue == null)
					{
						throw EntityUtil.BothMinAndMaxValueMustBeSpecifiedForNonConstantFacet(this._facetName, declaringTypeName);
					}
					int value = this._minValue.Value;
					int? num = this._maxValue;
					if (value == num.GetValueOrDefault() & num != null)
					{
						throw EntityUtil.MinAndMaxValueMustBeDifferentForNonConstantFacet(this._facetName, declaringTypeName);
					}
					num = this._minValue;
					int num2 = 0;
					if (!(num.GetValueOrDefault() < num2 & num != null))
					{
						num = this._maxValue;
						num2 = 0;
						if (!(num.GetValueOrDefault() < num2 & num != null))
						{
							num = this._minValue;
							int? maxValue = this._maxValue;
							if (num.GetValueOrDefault() > maxValue.GetValueOrDefault() & (num != null & maxValue != null))
							{
								throw EntityUtil.MinMustBeLessThanMax(this._minValue.ToString(), this._facetName, declaringTypeName);
							}
							return;
						}
					}
					throw EntityUtil.MinAndMaxMustBePositive(this._facetName, declaringTypeName);
				}
			}
		}

		// Token: 0x04000E12 RID: 3602
		private readonly string _facetName;

		// Token: 0x04000E13 RID: 3603
		private readonly EdmType _facetType;

		// Token: 0x04000E14 RID: 3604
		private readonly int? _minValue;

		// Token: 0x04000E15 RID: 3605
		private readonly int? _maxValue;

		// Token: 0x04000E16 RID: 3606
		private readonly object _defaultValue;

		// Token: 0x04000E17 RID: 3607
		private readonly bool _isConstant;

		// Token: 0x04000E18 RID: 3608
		private Facet _defaultValueFacet;

		// Token: 0x04000E19 RID: 3609
		private Facet _nullValueFacet;

		// Token: 0x04000E1A RID: 3610
		private Facet[] _valueCache;

		// Token: 0x04000E1B RID: 3611
		private static object _notInitializedSentinel = new object();
	}
}
