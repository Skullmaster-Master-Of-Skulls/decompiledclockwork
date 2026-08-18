using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000399 RID: 921
	internal class TypeUsageBuilder
	{
		// Token: 0x06002139 RID: 8505 RVA: 0x0009C129 File Offset: 0x0009A329
		internal TypeUsageBuilder(SchemaElement element)
		{
			this._element = element;
			this._facetValues = new Dictionary<string, object>();
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x0009C143 File Offset: 0x0009A343
		internal TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x0009C14B File Offset: 0x0009A34B
		internal bool Nullable
		{
			get
			{
				return this._nullable == null || this._nullable.Value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0009C167 File Offset: 0x0009A367
		internal string Default
		{
			get
			{
				return this._default;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0009C16F File Offset: 0x0009A36F
		internal object DefaultAsObject
		{
			get
			{
				return this._defaultObject;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0009C177 File Offset: 0x0009A377
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._hasUserDefinedFacets;
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0009C190 File Offset: 0x0009A390
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
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
				else if (edmType is PrimitiveType && ((PrimitiveType)edmType).PrimitiveTypeKind == PrimitiveTypeKind.String && keyValuePair.Key == "Collation")
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

		// Token: 0x06002140 RID: 8512 RVA: 0x0009C414 File Offset: 0x0009A614
		internal void ValidateAndSetTypeUsage(EdmType edmType, bool complainOnMissingFacet)
		{
			Dictionary<string, Facet> dictionary;
			this.TryGetFacets(edmType, complainOnMissingFacet, out dictionary);
			this._typeUsage = TypeUsage.Create(edmType, dictionary.Values);
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0009C440 File Offset: 0x0009A640
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

		// Token: 0x06002142 RID: 8514 RVA: 0x0009C5B4 File Offset: 0x0009A7B4
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

		// Token: 0x06002143 RID: 8515 RVA: 0x0009C65C File Offset: 0x0009A85C
		internal bool HandleAttribute(XmlReader reader)
		{
			bool flag = this.InternalHandleAttribute(reader);
			this._hasUserDefinedFacets = (this._hasUserDefinedFacets || flag);
			return flag;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0009C680 File Offset: 0x0009A880
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

		// Token: 0x06002145 RID: 8517 RVA: 0x0009C796 File Offset: 0x0009A996
		private void ValidateAndSetBinaryFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0009C7A0 File Offset: 0x0009A9A0
		private void ValidateAndSetDecimalFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			byte? b = null;
			Facet facet;
			if (facets.TryGetValue("Precision", out facet) && facet.Value != null)
			{
				b = new byte?((byte)facet.Value);
				FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "Precision");
				if ((int)b < facet2.MinValue.Value || (int)b > facet2.MaxValue.Value)
				{
					this._element.AddError(ErrorCode.PrecisionOutOfRange, EdmSchemaErrorSeverity.Error, Strings.PrecisionOutOfRange(b, facet2.MinValue.Value, facet2.MaxValue.Value, primitiveType.Name));
				}
			}
			Facet facet3;
			if (facets.TryGetValue("Scale", out facet3) && facet3.Value != null)
			{
				byte b2 = (byte)facet3.Value;
				FacetDescription facet4 = Helper.GetFacet(primitiveType.FacetDescriptions, "Scale");
				if ((int)b2 < facet4.MinValue.Value || (int)b2 > facet4.MaxValue.Value)
				{
					this._element.AddError(ErrorCode.ScaleOutOfRange, EdmSchemaErrorSeverity.Error, Strings.ScaleOutOfRange(b2, facet4.MinValue.Value, facet4.MaxValue.Value, primitiveType.Name));
					return;
				}
				if (b != null && b < b2)
				{
					this._element.AddError(ErrorCode.BadPrecisionAndScale, EdmSchemaErrorSeverity.Error, Strings.BadPrecisionAndScale(b, b2));
				}
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0009C994 File Offset: 0x0009AB94
		private void ValidatePrecisionFacetsForDateTimeFamily(EdmType type, Dictionary<string, Facet> facets)
		{
			PrimitiveType primitiveType = (PrimitiveType)type;
			byte? b = null;
			Facet facet;
			if (facets.TryGetValue("Precision", out facet) && facet.Value != null)
			{
				b = new byte?((byte)facet.Value);
				FacetDescription facet2 = Helper.GetFacet(primitiveType.FacetDescriptions, "Precision");
				if ((int)b < facet2.MinValue.Value || (int)b > facet2.MaxValue.Value)
				{
					this._element.AddError(ErrorCode.PrecisionOutOfRange, EdmSchemaErrorSeverity.Error, Strings.PrecisionOutOfRange(b, facet2.MinValue.Value, facet2.MaxValue.Value, primitiveType.Name));
				}
			}
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0009CA8D File Offset: 0x0009AC8D
		private void ValidateAndSetStringFacets(EdmType type, Dictionary<string, Facet> facets)
		{
			this.ValidateLengthFacets(type, facets);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0009CA98 File Offset: 0x0009AC98
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

		// Token: 0x0600214A RID: 8522 RVA: 0x0009CB44 File Offset: 0x0009AD44
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

		// Token: 0x0600214B RID: 8523 RVA: 0x0009CC6C File Offset: 0x0009AE6C
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

		// Token: 0x0600214C RID: 8524 RVA: 0x0009CCD4 File Offset: 0x0009AED4
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

		// Token: 0x0600214D RID: 8525 RVA: 0x0009CD3C File Offset: 0x0009AF3C
		private void HandleNullableAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Nullable", flag);
				this._nullable = new bool?(flag);
			}
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0009CD80 File Offset: 0x0009AF80
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

		// Token: 0x0600214F RID: 8527 RVA: 0x0009CDE0 File Offset: 0x0009AFE0
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

		// Token: 0x06002150 RID: 8528 RVA: 0x0009CE2D File Offset: 0x0009B02D
		private void HandleDefaultAttribute(XmlReader reader)
		{
			this._default = reader.Value;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0009CE3C File Offset: 0x0009B03C
		private void HandlePrecisionAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Precision", b);
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0009CE74 File Offset: 0x0009B074
		private void HandleScaleAttribute(XmlReader reader)
		{
			byte b = 0;
			if (this._element.HandleByteAttribute(reader, ref b))
			{
				this._facetValues.Add("Scale", b);
			}
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0009CEAC File Offset: 0x0009B0AC
		private void HandleUnicodeAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("Unicode", flag);
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0009CEE1 File Offset: 0x0009B0E1
		private void HandleCollationAttribute(XmlReader reader)
		{
			if (string.IsNullOrEmpty(reader.Value))
			{
				return;
			}
			this._facetValues.Add("Collation", reader.Value);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0009CF08 File Offset: 0x0009B108
		private void HandleIsFixedLengthAttribute(XmlReader reader)
		{
			bool flag = false;
			if (this._element.HandleBoolAttribute(reader, ref flag))
			{
				this._facetValues.Add("FixedLength", flag);
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0009CF40 File Offset: 0x0009B140
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

		// Token: 0x06002157 RID: 8535 RVA: 0x0009CF7C File Offset: 0x0009B17C
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
			}
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0009D0BC File Offset: 0x0009B2BC
		private void ValidateBinaryDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			string message = Strings.InvalidDefaultBinaryWithNoMaxLength(this._default);
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, message);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0009D0F9 File Offset: 0x0009B2F9
		private void ValidateBooleanDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultBoolean(this._default));
			}
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0009D128 File Offset: 0x0009B328
		private void ValidateIntegralDefaultValue(ScalarType scalar, long minValue, long maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultIntegral(this._default, minValue, maxValue));
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0009D164 File Offset: 0x0009B364
		private void ValidateDateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTime(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffZ".Replace("\\", "")));
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0009D1B4 File Offset: 0x0009B3B4
		private void ValidateTimeDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultTime(this._default, "HH\\:mm\\:ss.fffffffZ".Replace("\\", "")));
			}
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0009D204 File Offset: 0x0009B404
		private void ValidateDateTimeOffsetDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDateTimeOffset(this._default, "yyyy-MM-dd HH\\:mm\\:ss.fffffffz".Replace("\\", "")));
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0009D252 File Offset: 0x0009B452
		private void ValidateDecimalDefaultValue(ScalarType scalar)
		{
			if (scalar.TryParse(this._default, out this._defaultObject))
			{
				return;
			}
			this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultDecimal(this._default, 38, 38));
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0009D290 File Offset: 0x0009B490
		private void ValidateFloatingPointDefaultValue(ScalarType scalar, double minValue, double maxValue)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultFloatingPoint(this._default, minValue, maxValue));
			}
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0009D2CB File Offset: 0x0009B4CB
		private void ValidateGuidDefaultValue(ScalarType scalar)
		{
			if (!scalar.TryParse(this._default, out this._defaultObject))
			{
				this._element.AddError(ErrorCode.InvalidDefault, EdmSchemaErrorSeverity.Error, Strings.InvalidDefaultGuid(this._default));
			}
		}

		// Token: 0x04000BC6 RID: 3014
		private readonly Dictionary<string, object> _facetValues;

		// Token: 0x04000BC7 RID: 3015
		private readonly SchemaElement _element;

		// Token: 0x04000BC8 RID: 3016
		private string _default;

		// Token: 0x04000BC9 RID: 3017
		private object _defaultObject;

		// Token: 0x04000BCA RID: 3018
		private bool? _nullable;

		// Token: 0x04000BCB RID: 3019
		private TypeUsage _typeUsage;

		// Token: 0x04000BCC RID: 3020
		private bool _hasUserDefinedFacets;
	}
}
