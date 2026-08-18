using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200031B RID: 795
	internal class TypeUsageBuilder
	{
		// Token: 0x06002EF0 RID: 12016 RVA: 0x000B14C4 File Offset: 0x000AF6C4
		internal TypeUsageBuilder(SchemaElement element)
		{
			this._element = element;
			this._facetValues = new Dictionary<string, object>();
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000B14DE File Offset: 0x000AF6DE
		internal TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000B14E6 File Offset: 0x000AF6E6
		internal bool Nullable
		{
			get
			{
				return this._nullable == null || this._nullable.Value;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000B1502 File Offset: 0x000AF702
		internal string Default
		{
			get
			{
				return this._default;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000B150A File Offset: 0x000AF70A
		internal object DefaultAsObject
		{
			get
			{
				return this._defaultObject;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000B1512 File Offset: 0x000AF712
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._hasUserDefinedFacets;
			}
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000B151C File Offset: 0x000AF71C
		private bool TryGetFacets(EdmType edmType, bool complainOnMissingFacet, out Dictionary<string, Facet> calculatedFacets)
		{
			bool result = true;
			Dictionary<string, Facet> dictionary = edmType.GetAssociatedFacetDescriptions().ToDictionary((FacetDescription f) => f.FacetName, (FacetDescription f) => f.DefaultValueFacet);
			calculatedFacets = new Dictionary<string, Facet>();
			foreach (Facet facet in dictionary.Values)
			{
				object value;
				if (this._facetValues.TryGetValue(facet.Name, out value))
				{
					if (facet.Description.IsConstant)
					{
						this._element.AddError(ErrorCode.ConstantFacetSpecifiedInSchema, EdmSchemaErrorSeverity.Error, this._element, Strings.ConstantFacetSpecifiedInSchema(facet.Name, edmType.Name));
						result = false;
					}
					else
					{
						calculatedFacets.Add(facet.Name, Facet.Create(facet.Description, value));
					}
					this._facetValues.Remove(facet.Name);
				}
				else if (complainOnMissingFacet && facet.Description.IsRequired)
				{
					this._element.AddError(ErrorCode.RequiredFacetMissing, EdmSchemaErrorSeverity.Error, Strings.RequiredFacetMissing(facet.Name, edmType.Name));
					result = false;
				}
				else
				{
					calculatedFacets.Add(facet.Name, facet);
				}
			}
			foreach (KeyValuePair<string, object> keyValuePair in this._facetValues)
			{
				if (keyValuePair.Key == "StoreGeneratedPattern")
				{
					Facet facet2 = Facet.Create(Converter.StoreGeneratedPatternFacet, keyValuePair.Value);
					calculatedFacets.Add(facet2.Name, facet2);
				}
				else if (keyValuePair.Key == "ConcurrencyMode")
				{
					Facet facet3 = Facet.Create(Converter.ConcurrencyModeFacet, keyValuePair.Value);
					calculatedFacets.Add(facet3.Name, facet3);
				}
				else if (edmType is PrimitiveType && (edmType as PrimitiveType).PrimitiveTypeKind == PrimitiveTypeKind.String && keyValuePair.Key == "Collation")
				{
					Facet facet4 = Facet.Create(Converter.CollationFacet, keyValuePair.Value);
					calculatedFacets.Add(facet4.Name, facet4);
				}
				else
				{
					this._element.AddError(ErrorCode.FacetNotAllowedByType, EdmSchemaErrorSeverity.Error, Strings.FacetNotAllowed(keyValuePair.Key, edmType.Name));
				}
			}
			return result;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000B17A4 File Offset: 0x000AF9A4
		internal void ValidateAndSetTypeUsage(EdmType edmType, bool complainOnMissingFacet)
		{
			Dictionary<string, Facet> dictionary;
			this.TryGetFacets(edmType, complainOnMissingFacet, out dictionary);
			this._typeUsage = TypeUsage.Create(edmType, dictionary.Values);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000B17D0 File Offset: 0x000AF9D0
		internal void ValidateAndSetTypeUsage(ScalarType scalar, bool complainOnMissingFacet)
		{
			Trace.Assert(this._element != null);
			Trace.Assert(scalar != null);
			if (Helper.IsSpatialType(scalar.Type) && !this._facetValues.ContainsKey("IsStrict") && !this._element.Schema.UseStrongSpatialTypes)
			{
				this._facetValues.Add("IsStrict", false);
			}
			Dictionary<string, Facet> dictionary;
			bool flag = this.TryGetFacets(scalar.Type, complainOnMissingFacet, out dictionary);
			if (flag)
			{
				switch (scalar.TypeKind)
				{
				case PrimitiveTypeKind.Binary:
					this.ValidateAndSetBinaryFacets(scalar.Type, dictionary);
					break;
				case PrimitiveTypeKind.DateTime:
				case PrimitiveTypeKind.Time:
				case PrimitiveTypeKind.DateTimeOffset:
					this.ValidatePrecisionFacetsForDateTimeFamily(scalar.Type, dictionary);
					break;
				case PrimitiveTypeKind.Decimal:
					this.ValidateAndSetDecimalFacets(scalar.Type, dictionary);
					break;
				case PrimitiveTypeKind.String:
					this.ValidateAndSetStringFacets(scalar.Type, dictionary);
					break;
				case PrimitiveTypeKind.Geometry:
				case PrimitiveTypeKind.Geography:
				case PrimitiveTypeKind.GeometryPoint:
				case PrimitiveTypeKind.GeometryLineString:
				case PrimitiveTypeKind.GeometryPolygon:
				case PrimitiveTypeKind.GeometryMultiPoint:
				case PrimitiveTypeKind.GeometryMultiLineString:
				case PrimitiveTypeKind.GeometryMultiPolygon:
				case PrimitiveTypeKind.GeometryCollection:
				case PrimitiveTypeKind.GeographyPoint:
				case PrimitiveTypeKind.GeographyLineString:
				case PrimitiveTypeKind.GeographyPolygon:
				case PrimitiveTypeKind.GeographyMultiPoint:
				case PrimitiveTypeKind.GeographyMultiLineString:
				case PrimitiveTypeKind.GeographyMultiPolygon:
				case PrimitiveTypeKind.GeographyCollection:
					this.ValidateSpatialFacets(scalar.Type, dictionary);
					break;
				}
			}
			this._typeUsage = TypeUsage.Create(scalar.Type, dictionary.Values);
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000B193C File Offset: 0x000AFB3C
		internal void ValidateEnumFacets(SchemaEnumType schemaEnumType)
		{
			foreach (KeyValuePair<string, object> keyValuePair in this._facetValues)
			{
				if (keyValuePair.Key != "Nullable" && keyValuePair.Key != "StoreGeneratedPattern" && keyValuePair.Key != "ConcurrencyMode")
				{
					this._element.AddError(ErrorCode.FacetNotAllowedByType, EdmSchemaErrorSeverity.Error, Strings.FacetNotAllowed(keyValuePair.Key, schemaEnumType.FQName));
				}
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000B19E4 File Offset: 0x000AFBE4
		internal bool HandleAttribute(XmlReader reader)
		{
			bool flag = this.InternalHandleAttribute(reader);
			this._hasUserDefinedFacets = (this._hasUserDefinedFacets || flag);
			return flag;
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000B1A08 File Offset: 0x000AFC08
		private bool InternalHandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Nullable"))
			{
				this.HandleNullableAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "DefaultValue"))
			{
				this.HandleDefaultAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Precision"))
			{
				this.HandlePrecisionAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Scale"))
			{
				this.HandleScaleAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "StoreGeneratedPattern"))
			{
				this.HandleStoreGeneratedPatternAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ConcurrencyMode"))
			{
				this.HandleConcurrencyModeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "MaxLength"))
			{
				this.HandleMaxLengthAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Unicode"))
			{
				this.HandleUnicodeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Collation"))
			{
				this.HandleCollationAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "FixedLength"))
			{
				this.HandleIsFixedLengthAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Nullable"))
			{
				this.HandleNullableAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "SRID"))
			{
				this.HandleSridAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000B1B1E File Offset: 0x000AFD1E
		private void ValidateAndSetBinaryFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000B1B28 File Offset: 0x000AFD28
		private void ValidateAndSetDecimalFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			byte? b = null;
			Facet facet;
			if (facets.TryGetValue("Precision", out facet) && facet.Value != null)
			{
				b = new byte?((byte)facet.Value);
				FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "Precision");
				byte? b2 = b;
				int? num = (b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null;
				int num2 = facet2.MinValue.Value;
				if (!(num.GetValueOrDefault() < num2 & num != null))
				{
					b2 = b;
					num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
					num2 = facet2.MaxValue.Value;
					if (!(num.GetValueOrDefault() > num2 & num != null))
					{
						goto IL_133;
					}
				}
				this._element.AddError(ErrorCode.PrecisionOutOfRange, EdmSchemaErrorSeverity.Error, Strings.PrecisionOutOfRange(b, facet2.MinValue.Value, facet2.MaxValue.Value, primitiveType.Name));
			}
			IL_133:
			Facet facet3;
			if (facets.TryGetValue("Scale", out facet3) && facet3.Value != null)
			{
				byte b3 = (byte)facet3.Value;
				FacetDescription facet4 = Helper.GetFacet(primitiveType.FacetDescriptions, "Scale");
				if ((int)b3 < facet4.MinValue.Value || (int)b3 > facet4.MaxValue.Value)
				{
					this._element.AddError(ErrorCode.ScaleOutOfRange, EdmSchemaErrorSeverity.Error, Strings.ScaleOutOfRange(b3, facet4.MinValue.Value, facet4.MaxValue.Value, primitiveType.Name));
					return;
				}
				if (b != null)
				{
					byte? b2 = b;
					int? num = (b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null;
					int num2 = (int)b3;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						this._element.AddError(ErrorCode.BadPrecisionAndScale, EdmSchemaErrorSeverity.Error, Strings.BadPrecisionAndScale(b, b3));
					}
				}
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000B1D80 File Offset: 0x000AFF80
		private void ValidatePrecisionFacetsForDateTimeFamily(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			byte? b = null;
			Facet facet;
			if (facets.TryGetValue("Precision", out facet) && facet.Value != null)
			{
				b = new byte?((byte)facet.Value);
				FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "Precision");
				byte? b2 = b;
				int? num = (b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null;
				int value = facet2.MinValue.Value;
				if (!(num.GetValueOrDefault() < value & num != null))
				{
					b2 = b;
					num = ((b2 != null) ? new int?((int)b2.GetValueOrDefault()) : null);
					value = facet2.MaxValue.Value;
					if (!(num.GetValueOrDefault() > value & num != null))
					{
						return;
					}
				}
				this._element.AddError(ErrorCode.PrecisionOutOfRange, EdmSchemaErrorSeverity.Error, Strings.PrecisionOutOfRange(b, facet2.MinValue.Value, facet2.MaxValue.Value, primitiveType.Name));
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000B1B1E File Offset: 0x000AFD1E
		private void ValidateAndSetStringFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000B1EBC File Offset: 0x000B00BC
		private void ValidateLengthFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			Facet facet;
			if (!facets.TryGetValue("MaxLength", out facet) || facet.Value == null)
			{
				return;
			}
			if (Helper.IsUnboundedFacetValue(facet))
			{
				return;
			}
			int num = (int)facet.Value;
			FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength");
			int value = facet2.MaxValue.Value;
			int value2 = facet2.MinValue.Value;
			if (num < value2 || num > value)
			{
				this._element.AddError(ErrorCode.InvalidSize, EdmSchemaErrorSeverity.Error, Strings.InvalidSize(num, value2, value, primitiveType.Name));
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000B1F68 File Offset: 0x000B0168
		private void ValidateSpatialFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			if (this._facetValues.ContainsKey("ConcurrencyMode"))
			{
				this._element.AddError(ErrorCode.FacetNotAllowedByType, EdmSchemaErrorSeverity.Error, Strings.FacetNotAllowed("ConcurrencyMode", type.FullName));
			}
			Facet facet;
			if (this._element.Schema.DataModel == SchemaDataModelOption.EntityDataModel && (!facets.TryGetValue("IsStrict", out facet) || (bool)facet.Value))
			{
				this._element.AddError(ErrorCode.UnexpectedSpatialType, EdmSchemaErrorSeverity.Error, Strings.SpatialWithUseStrongSpatialTypesFalse);
			}
			Facet facet2;
			if (!facets.TryGetValue("SRID", out facet2) || facet2.Value == null)
			{
				return;
			}
			if (Helper.IsVariableFacetValue(facet2))
			{
				return;
			}
			int num = (int)facet2.Value;
			FacetDescription facet3 = Helper.GetFacet(primitiveType.FacetDescriptions, "SRID");
			int value = facet3.MaxValue.Value;
			int value2 = facet3.MinValue.Value;
			if (num < value2 || num > value)
			{
				this._element.AddError(ErrorCode.InvalidSystemReferenceId, EdmSchemaErrorSeverity.Error, Strings.InvalidSystemReferenceId(num, value2, value, primitiveType.Name));
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000B2090 File Offset: 0x000B0290
		internal void HandleMaxLengthAttribute(XmlReader reader)
		{
			string value = reader.Value;
			if (value.Trim() == "Max")
			{
				this._facetValues.Add("MaxLength", EdmConstants.UnboundedValue);
				return;
			}
			int num = 0;
			if (!this._element.HandleIntAttribute(reader, ref num))
			{
				return;
			}
			this._facetValues.Add("MaxLength", num);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000B20F8 File Offset: 0x000B02F8
		internal void HandleSridAttribute(XmlReader reader)
		{
			string value = reader.Value;
			if (value.Trim() == "Variable")
			{
				this._facetValues.Add("SRID", EdmConstants.VariableValue);
				return;
			}
			int num = 0;
			if (!this._element.HandleIntAttribute(reader, ref num))
			{
				return;
			}
			this._facetValues.Add("SRID", num);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000B2160 File Offset: 0x000B0360
		private void HandleNullableAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Nullable", flag);
				this._nullable = new bool?(flag);
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000B21A4 File Offset: 0x000B03A4
		internal void HandleStoreGeneratedPatternAttribute(XmlReader reader)
		{
			string value = reader.Value;
			StoreGeneratedPattern storeGeneratedPattern;
			if (value == "None")
			{
				storeGeneratedPattern = StoreGeneratedPattern.None;
			}
			else if (value == "Identity")
			{
				storeGeneratedPattern = StoreGeneratedPattern.Identity;
			}
			else
			{
				if (!(value == "Computed"))
				{
					return;
				}
				storeGeneratedPattern = StoreGeneratedPattern.Computed;
			}
			this._facetValues.Add("StoreGeneratedPattern", storeGeneratedPattern);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000B2204 File Offset: 0x000B0404
		internal void HandleConcurrencyModeAttribute(XmlReader reader)
		{
			string value = reader.Value;
			ConcurrencyMode concurrencyMode;
			if (value == "None")
			{
				concurrencyMode = ConcurrencyMode.None;
			}
			else
			{
				if (!(value == "Fixed"))
				{
					return;
				}
				concurrencyMode = ConcurrencyMode.Fixed;
			}
			this._facetValues.Add("ConcurrencyMode", concurrencyMode);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000B2251 File Offset: 0x000B0451
		private void HandleDefaultAttribute(XmlReader reader)
		{
			this._default = reader.Value;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000B2260 File Offset: 0x000B0460
		private void HandlePrecisionAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Precision", b);
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000B2298 File Offset: 0x000B0498
		private void HandleScaleAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Scale", b);
			}
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000B22D0 File Offset: 0x000B04D0
		private void HandleUnicodeAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Unicode", flag);
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000B2305 File Offset: 0x000B0505
		private void HandleCollationAttribute(XmlReader reader)
		{
			if (string.IsNullOrEmpty(reader.Value))
			{
				return;
			}
			this._facetValues.Add("Collation", reader.Value);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000B232C File Offset: 0x000B052C
		private void HandleIsFixedLengthAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("FixedLength", flag);
			}
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000B2364 File Offset: 0x000B0564
		internal void ValidateDefaultValue(SchemaType type)
		{
			if (this._default == null)
			{
				return;
			}
			ScalarType scalarType = type as ScalarType;
			if (scalarType != null)
			{
				this.ValidateScalarMemberDefaultValue(scalarType);
				return;
			}
			this._element.AddError(ErrorCode.DefaultNotAllowed, EdmSchemaErrorSeverity.Error, Strings.DefaultNotAllowed);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000B23A0 File Offset: 0x000B05A0
		private void ValidateScalarMemberDefaultValue(ScalarType scalar)
		{
			if (scalar != null)
			{
				switch (scalar.TypeKind)
				{
				case PrimitiveTypeKind.Binary:
					this.ValidateBinaryDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Boolean:
					this.ValidateBooleanDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Byte:
					this.ValidateIntegralDefaultValue(scalar, 0L, 255L);
					return;
				case PrimitiveTypeKind.DateTime:
					this.ValidateDateTimeDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Decimal:
					this.ValidateDecimalDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Double:
					this.ValidateFloatingPointDefaultValue(scalar, double.MinValue, double.MaxValue);
					return;
				case PrimitiveTypeKind.Guid:
					this.ValidateGuidDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.Single:
					this.ValidateFloatingPointDefaultValue(scalar, -3.4028234663852886E+38, 3.4028234663852886E+38);
					return;
				case PrimitiveTypeKind.Int16:
					this.ValidateIntegralDefaultValue(scalar, -32768L, 32767L);
					return;
				case PrimitiveTypeKind.Int32:
					this.ValidateIntegralDefaultValue(scalar, -2147483648L, 2147483647L);
					return;
				case PrimitiveTypeKind.Int64:
					this.ValidateIntegralDefaultValue(scalar, long.MinValue, long.MaxValue);
					return;
				case PrimitiveTypeKind.String:
					this._defaultObject = this._default;
					return;
				case PrimitiveTypeKind.Time:
					this.ValidateTimeDefaultValue(scalar);
					return;
				case PrimitiveTypeKind.DateTimeOffset:
					this.ValidateDateTimeOffsetDefaultValue(scalar);
					return;
				}
				this._element.AddError(ErrorCode.DefaultNotAllowed, EdmSchemaErrorSeverity.Error, Strings.DefaultNotAllowed);
				return;
			}
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000B24E0 File Offset: 0x000B06E0
		private void ValidateBinaryDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			string message = Strings.InvalidDefaultBinaryWithNoMaxLength(this._default);
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, message);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000B251D File Offset: 0x000B071D
		private void ValidateBooleanDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultBoolean(this._default));
			}
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000B254C File Offset: 0x000B074C
		private void ValidateIntegralDefaultValue(ScalarType scalar, long minValue, long maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultIntegral(this._default, minValue, maxValue));
			}
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000B2588 File Offset: 0x000B0788
		private void ValidateDateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTime(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffZ".Replace("\\", "")));
			}
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000B25D8 File Offset: 0x000B07D8
		private void ValidateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultTime(this._default, "HH\\:mm\\:ss.fffffffZ".Replace("\\", "")));
			}
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000B2628 File Offset: 0x000B0828
		private void ValidateDateTimeOffsetDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTimeOffset(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffffffz".Replace("\\", "")));
			}
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000B2676 File Offset: 0x000B0876
		private void ValidateDecimalDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDecimal(this._default, 38, 38));
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000B26B4 File Offset: 0x000B08B4
		private void ValidateFloatingPointDefaultValue(ScalarType scalar, double minValue, double maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultFloatingPoint(this._default, minValue, maxValue));
			}
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000B26EF File Offset: 0x000B08EF
		private void ValidateGuidDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultGuid(this._default));
			}
		}

		// Token: 0x0400144B RID: 5195
		private readonly Dictionary<string, object> _facetValues;

		// Token: 0x0400144C RID: 5196
		private readonly SchemaElement _element;

		// Token: 0x0400144D RID: 5197
		private string _default;

		// Token: 0x0400144E RID: 5198
		private object _defaultObject;

		// Token: 0x0400144F RID: 5199
		private bool? _nullable;

		// Token: 0x04001450 RID: 5200
		private TypeUsage _typeUsage;

		// Token: 0x04001451 RID: 5201
		private bool _hasUserDefinedFacets;
	}
}
