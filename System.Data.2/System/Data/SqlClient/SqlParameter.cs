using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001EE RID: 494
	[TypeConverter(typeof(SqlParameter.SqlParameterConverter))]
	public sealed class SqlParameter : DbParameter, IDbDataParameter, IDataParameter, ICloneable
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000D4B40 File Offset: 0x000D3F40
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x000D4B54 File Offset: 0x000D3F54
		internal SqlCipherMetadata CipherMetadata
		{
			get
			{
				return this._columnEncryptionCipherMetadata;
			}
			set
			{
				this._columnEncryptionCipherMetadata = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x000D4B68 File Offset: 0x000D3F68
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x000D4B7C File Offset: 0x000D3F7C
		internal bool HasReceivedMetadata { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x000D4B90 File Offset: 0x000D3F90
		internal byte NormalizationRuleVersion
		{
			get
			{
				if (this._columnEncryptionCipherMetadata != null)
				{
					return this._columnEncryptionCipherMetadata.NormalizationRuleVersion;
				}
				return 0;
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000D4BB4 File Offset: 0x000D3FB4
		public SqlParameter()
		{
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000D4BD8 File Offset: 0x000D3FD8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.IsNullable = isNullable;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.Value = value;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000D4C38 File Offset: 0x000D4038
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.Direction = direction;
			this.PrecisionInternal = precision;
			this.ScaleInternal = scale;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
			this.Value = value;
			this._xmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this._xmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this._xmlSchemaCollectionName = xmlSchemaCollectionName;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000D4CC0 File Offset: 0x000D40C0
		public SqlParameter(string parameterName, SqlDbType dbType) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000D4CE4 File Offset: 0x000D40E4
		public SqlParameter(string parameterName, object value) : this()
		{
			this.ParameterName = parameterName;
			this.Value = value;
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000D4D08 File Offset: 0x000D4108
		public SqlParameter(string parameterName, SqlDbType dbType, int size) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000D4D30 File Offset: 0x000D4130
		public SqlParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn) : this()
		{
			this.ParameterName = parameterName;
			this.SqlDbType = dbType;
			this.Size = size;
			this.SourceColumn = sourceColumn;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x000D4D60 File Offset: 0x000D4160
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x000D4D74 File Offset: 0x000D4174
		internal SqlCollation Collation
		{
			get
			{
				return this._collation;
			}
			set
			{
				this._collation = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000D4D88 File Offset: 0x000D4188
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x000D4DA8 File Offset: 0x000D41A8
		[Browsable(false)]
		public SqlCompareOptions CompareInfo
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.SqlCompareOptions;
				}
				return SqlCompareOptions.None;
			}
			set
			{
				SqlCollation sqlCollation = this._collation;
				if (sqlCollation == null)
				{
					sqlCollation = (this._collation = new SqlCollation());
				}
				if ((value & SqlString.x_iValidSqlCompareOptionMask) != value)
				{
					throw ADP.ArgumentOutOfRange("CompareInfo");
				}
				sqlCollation.SqlCompareOptions = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000D4DE8 File Offset: 0x000D41E8
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x000D4E08 File Offset: 0x000D4208
		[ResDescription("SqlParameter_XmlSchemaCollectionDatabase")]
		[ResCategory("DataCategory_Xml")]
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				string xmlSchemaCollectionDatabase = this._xmlSchemaCollectionDatabase;
				if (xmlSchemaCollectionDatabase == null)
				{
					return ADP.StrEmpty;
				}
				return xmlSchemaCollectionDatabase;
			}
			set
			{
				this._xmlSchemaCollectionDatabase = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x000D4E1C File Offset: 0x000D421C
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x000D4E3C File Offset: 0x000D423C
		[ResCategory("DataCategory_Xml")]
		[ResDescription("SqlParameter_XmlSchemaCollectionOwningSchema")]
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				string xmlSchemaCollectionOwningSchema = this._xmlSchemaCollectionOwningSchema;
				if (xmlSchemaCollectionOwningSchema == null)
				{
					return ADP.StrEmpty;
				}
				return xmlSchemaCollectionOwningSchema;
			}
			set
			{
				this._xmlSchemaCollectionOwningSchema = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000D4E50 File Offset: 0x000D4250
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x000D4E70 File Offset: 0x000D4270
		[ResCategory("DataCategory_Xml")]
		[ResDescription("SqlParameter_XmlSchemaCollectionName")]
		public string XmlSchemaCollectionName
		{
			get
			{
				string xmlSchemaCollectionName = this._xmlSchemaCollectionName;
				if (xmlSchemaCollectionName == null)
				{
					return ADP.StrEmpty;
				}
				return xmlSchemaCollectionName;
			}
			set
			{
				this._xmlSchemaCollectionName = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000D4E84 File Offset: 0x000D4284
		// (set) Token: 0x06001E71 RID: 7793 RVA: 0x000D4E98 File Offset: 0x000D4298
		[ResCategory("DataCategory_Data")]
		[ResDescription("TCE_SqlParameter_ForceColumnEncryption")]
		[DefaultValue(false)]
		public bool ForceColumnEncryption { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000D4EAC File Offset: 0x000D42AC
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000D4EC4 File Offset: 0x000D42C4
		public override DbType DbType
		{
			get
			{
				return this.GetMetaTypeOnly().DbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if (metaType == null || metaType.DbType != value || value == DbType.Date || value == DbType.Time)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromDbType(value);
				}
			}
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000D4F00 File Offset: 0x000D4300
		public override void ResetDbType()
		{
			this.ResetSqlDbType();
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x000D4F14 File Offset: 0x000D4314
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x000D4F28 File Offset: 0x000D4328
		internal MetaType InternalMetaType
		{
			get
			{
				return this._internalMetaType;
			}
			set
			{
				this._internalMetaType = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x000D4F3C File Offset: 0x000D433C
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x000D4F5C File Offset: 0x000D435C
		[Browsable(false)]
		public int LocaleId
		{
			get
			{
				SqlCollation collation = this._collation;
				if (collation != null)
				{
					return collation.LCID;
				}
				return 0;
			}
			set
			{
				SqlCollation sqlCollation = this._collation;
				if (sqlCollation == null)
				{
					sqlCollation = (this._collation = new SqlCollation());
				}
				if ((long)value != (1048575L & (long)value))
				{
					throw ADP.ArgumentOutOfRange("LocaleId");
				}
				sqlCollation.LCID = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x000D4FA0 File Offset: 0x000D43A0
		private SqlMetaData MetaData
		{
			get
			{
				MetaType metaTypeOnly = this.GetMetaTypeOnly();
				long maxLength;
				if (metaTypeOnly.IsFixed)
				{
					maxLength = (long)metaTypeOnly.FixedLength;
				}
				else if (this.Size > 0 || this.Size < 0)
				{
					maxLength = (long)this.Size;
				}
				else
				{
					maxLength = SmiMetaData.GetDefaultForType(metaTypeOnly.SqlDbType).MaxLength;
				}
				return new SqlMetaData(this.ParameterName, metaTypeOnly.SqlDbType, maxLength, this.GetActualPrecision(), this.GetActualScale(), (long)this.LocaleId, this.CompareInfo, this.XmlSchemaCollectionDatabase, this.XmlSchemaCollectionOwningSchema, this.XmlSchemaCollectionName, metaTypeOnly.IsPlp, this._udtType);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x000D503C File Offset: 0x000D443C
		internal bool SizeInferred
		{
			get
			{
				return this._size == 0;
			}
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000D5054 File Offset: 0x000D4454
		internal SmiParameterMetaData GetMetadataForTypeInfo()
		{
			ParameterPeekAheadValue parameterPeekAheadValue = null;
			if (this._internalMetaType == null)
			{
				this._internalMetaType = this.GetMetaTypeOnly();
			}
			return this.MetaDataForSmi(out parameterPeekAheadValue);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000D5080 File Offset: 0x000D4480
		internal SmiParameterMetaData MetaDataForSmi(out ParameterPeekAheadValue peekAhead)
		{
			peekAhead = null;
			MetaType metaType = this.ValidateTypeLengths(true);
			long num = (long)this.GetActualSize();
			long num2 = (long)this.Size;
			if (!metaType.IsLong)
			{
				if (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)
				{
					num /= 2L;
				}
				if (num > num2)
				{
					num2 = num;
				}
			}
			if (num2 == 0L)
			{
				if (SqlDbType.Binary == metaType.SqlDbType || SqlDbType.VarBinary == metaType.SqlDbType)
				{
					num2 = 8000L;
				}
				else if (SqlDbType.Char == metaType.SqlDbType || SqlDbType.VarChar == metaType.SqlDbType)
				{
					num2 = 8000L;
				}
				else if (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)
				{
					num2 = 4000L;
				}
			}
			else if ((num2 > 8000L && (SqlDbType.Binary == metaType.SqlDbType || SqlDbType.VarBinary == metaType.SqlDbType)) || (num2 > 8000L && (SqlDbType.Char == metaType.SqlDbType || SqlDbType.VarChar == metaType.SqlDbType)) || (num2 > 4000L && (SqlDbType.NChar == metaType.SqlDbType || SqlDbType.NVarChar == metaType.SqlDbType)))
			{
				num2 = -1L;
			}
			int num3 = this.LocaleId;
			if (num3 == 0 && metaType.IsCharType)
			{
				object coercedValue = this.GetCoercedValue();
				if (coercedValue is SqlString && !((SqlString)coercedValue).IsNull)
				{
					num3 = ((SqlString)coercedValue).LCID;
				}
				else
				{
					num3 = CultureInfo.CurrentCulture.LCID;
				}
			}
			SqlCompareOptions sqlCompareOptions = this.CompareInfo;
			if (sqlCompareOptions == SqlCompareOptions.None && metaType.IsCharType)
			{
				object coercedValue2 = this.GetCoercedValue();
				if (coercedValue2 is SqlString && !((SqlString)coercedValue2).IsNull)
				{
					sqlCompareOptions = ((SqlString)coercedValue2).SqlCompareOptions;
				}
				else
				{
					sqlCompareOptions = SmiMetaData.GetDefaultForType(metaType.SqlDbType).CompareOptions;
				}
			}
			string text = null;
			string text2 = null;
			string text3 = null;
			if (SqlDbType.Xml == metaType.SqlDbType)
			{
				text = this.XmlSchemaCollectionDatabase;
				text2 = this.XmlSchemaCollectionOwningSchema;
				text3 = this.XmlSchemaCollectionName;
			}
			else if (SqlDbType.Udt == metaType.SqlDbType || (SqlDbType.Structured == metaType.SqlDbType && !ADP.IsEmpty(this.TypeName)))
			{
				string[] array;
				if (SqlDbType.Udt == metaType.SqlDbType)
				{
					array = SqlParameter.ParseTypeName(this.UdtTypeName, true);
				}
				else
				{
					array = SqlParameter.ParseTypeName(this.TypeName, false);
				}
				if (1 == array.Length)
				{
					text3 = array[0];
				}
				else if (2 == array.Length)
				{
					text2 = array[0];
					text3 = array[1];
				}
				else
				{
					if (3 != array.Length)
					{
						throw ADP.ArgumentOutOfRange("names");
					}
					text = array[0];
					text2 = array[1];
					text3 = array[2];
				}
				if ((!ADP.IsEmpty(text) && 255 < text.Length) || (!ADP.IsEmpty(text2) && 255 < text2.Length) || (!ADP.IsEmpty(text3) && 255 < text3.Length))
				{
					throw ADP.ArgumentOutOfRange("names");
				}
			}
			byte b = this.GetActualPrecision();
			byte actualScale = this.GetActualScale();
			if (SqlDbType.Decimal == metaType.SqlDbType && b == 0)
			{
				b = 29;
			}
			List<SmiExtendedMetaData> fieldMetaData = null;
			SmiMetaDataPropertyCollection extendedProperties = null;
			if (SqlDbType.Structured == metaType.SqlDbType)
			{
				this.GetActualFieldsAndProperties(out fieldMetaData, out extendedProperties, out peekAhead);
			}
			return new SmiParameterMetaData(metaType.SqlDbType, num2, b, actualScale, (long)num3, sqlCompareOptions, null, SqlDbType.Structured == metaType.SqlDbType, fieldMetaData, extendedProperties, this.ParameterNameFixed, text, text2, text3, this.Direction);
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x000D53B8 File Offset: 0x000D47B8
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x000D53CC File Offset: 0x000D47CC
		internal bool ParamaterIsSqlType
		{
			get
			{
				return this._isSqlParameterSqlType;
			}
			set
			{
				this._isSqlParameterSqlType = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x000D53E0 File Offset: 0x000D47E0
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x000D5400 File Offset: 0x000D4800
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlParameter_ParameterName")]
		public override string ParameterName
		{
			get
			{
				string parameterName = this._parameterName;
				if (parameterName == null)
				{
					return ADP.StrEmpty;
				}
				return parameterName;
			}
			set
			{
				if (!ADP.IsEmpty(value) && value.Length >= 128 && ('@' != value[0] || value.Length > 128))
				{
					throw SQL.InvalidParameterNameLength(value);
				}
				if (this._parameterName != value)
				{
					this.PropertyChanging();
					this._parameterName = value;
					return;
				}
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x000D5460 File Offset: 0x000D4860
		internal string ParameterNameFixed
		{
			get
			{
				string text = this.ParameterName;
				if (0 < text.Length && '@' != text[0])
				{
					text = "@" + text;
				}
				return text;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x000D5498 File Offset: 0x000D4898
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x000D54AC File Offset: 0x000D48AC
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbDataParameter_Precision")]
		[DefaultValue(0)]
		public new byte Precision
		{
			get
			{
				return this.PrecisionInternal;
			}
			set
			{
				this.PrecisionInternal = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x000D54C0 File Offset: 0x000D48C0
		// (set) Token: 0x06001E85 RID: 7813 RVA: 0x000D54F0 File Offset: 0x000D48F0
		internal byte PrecisionInternal
		{
			get
			{
				byte b = this._precision;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && SqlDbType.Decimal == metaSqlDbTypeOnly)
				{
					b = this.ValuePrecision(this.SqlValue);
				}
				return b;
			}
			set
			{
				SqlDbType sqlDbType = this.SqlDbType;
				if (sqlDbType == SqlDbType.Decimal && value > 38)
				{
					throw SQL.PrecisionValueOutOfRange(value);
				}
				if (this._precision != value)
				{
					this.PropertyChanging();
					this._precision = value;
				}
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000D552C File Offset: 0x000D492C
		private bool ShouldSerializePrecision()
		{
			return this._precision > 0;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x000D5544 File Offset: 0x000D4944
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x000D5558 File Offset: 0x000D4958
		[DefaultValue(0)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbDataParameter_Scale")]
		public new byte Scale
		{
			get
			{
				return this.ScaleInternal;
			}
			set
			{
				this.ScaleInternal = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000D556C File Offset: 0x000D496C
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x000D559C File Offset: 0x000D499C
		internal byte ScaleInternal
		{
			get
			{
				byte b = this._scale;
				SqlDbType metaSqlDbTypeOnly = this.GetMetaSqlDbTypeOnly();
				if (b == 0 && SqlDbType.Decimal == metaSqlDbTypeOnly)
				{
					b = this.ValueScale(this.SqlValue);
				}
				return b;
			}
			set
			{
				if (this._scale != value || !this._hasScale)
				{
					this.PropertyChanging();
					this._scale = value;
					this._hasScale = true;
					this._actualSize = -1;
				}
			}
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000D55D8 File Offset: 0x000D49D8
		private bool ShouldSerializeScale()
		{
			return this._scale > 0;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x000D55F0 File Offset: 0x000D49F0
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x000D5608 File Offset: 0x000D4A08
		[ResDescription("SqlParameter_SqlDbType")]
		[DbProviderSpecificTypeProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		public SqlDbType SqlDbType
		{
			get
			{
				return this.GetMetaTypeOnly().SqlDbType;
			}
			set
			{
				MetaType metaType = this._metaType;
				if ((SqlDbType)24 == value)
				{
					throw SQL.InvalidSqlDbType(value);
				}
				if (metaType == null || metaType.SqlDbType != value)
				{
					this.PropertyTypeChanging();
					this._metaType = MetaType.GetMetaTypeFromSqlDbType(value, value == SqlDbType.Structured);
				}
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x000D564C File Offset: 0x000D4A4C
		private bool ShouldSerializeSqlDbType()
		{
			return this._metaType != null;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x000D5664 File Offset: 0x000D4A64
		public void ResetSqlDbType()
		{
			if (this._metaType != null)
			{
				this.PropertyTypeChanging();
				this._metaType = null;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000D5688 File Offset: 0x000D4A88
		// (set) Token: 0x06001E91 RID: 7825 RVA: 0x000D572C File Offset: 0x000D4B2C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object SqlValue
		{
			get
			{
				if (this._udtLoadError != null)
				{
					throw this._udtLoadError;
				}
				if (this._value != null)
				{
					if (this._value == DBNull.Value)
					{
						return MetaType.GetNullSqlValue(this.GetMetaTypeOnly().SqlType);
					}
					if (this._value is INullable)
					{
						return this._value;
					}
					if (this._value is DateTime)
					{
						SqlDbType sqlDbType = this.GetMetaTypeOnly().SqlDbType;
						if (sqlDbType == SqlDbType.Date || sqlDbType == SqlDbType.DateTime2)
						{
							return this._value;
						}
					}
					return MetaType.GetSqlValueFromComVariant(this._value);
				}
				else
				{
					if (this._sqlBufferReturnValue != null)
					{
						return this._sqlBufferReturnValue.SqlValue;
					}
					return null;
				}
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x000D5740 File Offset: 0x000D4B40
		// (set) Token: 0x06001E93 RID: 7827 RVA: 0x000D5760 File Offset: 0x000D4B60
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public string UdtTypeName
		{
			get
			{
				string udtTypeName = this._udtTypeName;
				if (udtTypeName == null)
				{
					return ADP.StrEmpty;
				}
				return udtTypeName;
			}
			set
			{
				this._udtTypeName = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x000D5774 File Offset: 0x000D4B74
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x000D5794 File Offset: 0x000D4B94
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public string TypeName
		{
			get
			{
				string typeName = this._typeName;
				if (typeName == null)
				{
					return ADP.StrEmpty;
				}
				return typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x000D57A8 File Offset: 0x000D4BA8
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x000D57FC File Offset: 0x000D4BFC
		[ResDescription("DbParameter_Value")]
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(StringConverter))]
		public override object Value
		{
			get
			{
				if (this._udtLoadError != null)
				{
					throw this._udtLoadError;
				}
				if (this._value != null)
				{
					return this._value;
				}
				if (this._sqlBufferReturnValue == null)
				{
					return null;
				}
				if (this.ParamaterIsSqlType)
				{
					return this._sqlBufferReturnValue.SqlValue;
				}
				return this._sqlBufferReturnValue.Value;
			}
			set
			{
				this._value = value;
				this._sqlBufferReturnValue = null;
				this._coercedValue = null;
				this._valueAsINullable = (this._value as INullable);
				this._isSqlParameterSqlType = (this._valueAsINullable != null);
				this._isNull = (this._value == null || this._value == DBNull.Value || (this._isSqlParameterSqlType && this._valueAsINullable.IsNull));
				this._udtLoadError = null;
				this._actualSize = -1;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000D5880 File Offset: 0x000D4C80
		internal INullable ValueAsINullable
		{
			get
			{
				return this._valueAsINullable;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x000D5894 File Offset: 0x000D4C94
		internal bool IsNull
		{
			get
			{
				if (this._internalMetaType.SqlDbType == SqlDbType.Udt)
				{
					this._isNull = (this._value == null || this._value == DBNull.Value || (this._isSqlParameterSqlType && this._valueAsINullable.IsNull));
				}
				return this._isNull;
			}
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000D58EC File Offset: 0x000D4CEC
		internal int GetActualSize()
		{
			MetaType metaType = this.InternalMetaType;
			SqlDbType sqlDbType = metaType.SqlDbType;
			if (this._actualSize == -1 || sqlDbType == SqlDbType.Udt)
			{
				this._actualSize = 0;
				object coercedValue = this.GetCoercedValue();
				bool flag = false;
				if (this.IsNull && !metaType.IsVarTime)
				{
					return 0;
				}
				if (sqlDbType == SqlDbType.Variant)
				{
					metaType = MetaType.GetMetaTypeFromValue(coercedValue, false);
					sqlDbType = MetaType.GetSqlDataType((int)metaType.TDSType, 0U, 0).SqlDbType;
					flag = true;
				}
				if (metaType.IsFixed)
				{
					this._actualSize = metaType.FixedLength;
				}
				else
				{
					int num = 0;
					if (sqlDbType <= SqlDbType.Char)
					{
						if (sqlDbType == SqlDbType.Binary)
						{
							goto IL_1E2;
						}
						if (sqlDbType != SqlDbType.Char)
						{
							goto IL_2AF;
						}
					}
					else
					{
						if (sqlDbType != SqlDbType.Image)
						{
							if (sqlDbType - SqlDbType.NChar > 2)
							{
								switch (sqlDbType)
								{
								case SqlDbType.Text:
								case SqlDbType.VarChar:
									goto IL_172;
								case SqlDbType.Timestamp:
								case SqlDbType.VarBinary:
									goto IL_1E2;
								case SqlDbType.TinyInt:
								case SqlDbType.Variant:
								case (SqlDbType)24:
								case (SqlDbType)26:
								case (SqlDbType)27:
								case (SqlDbType)28:
								case SqlDbType.Date:
									goto IL_2AF;
								case SqlDbType.Xml:
									break;
								case SqlDbType.Udt:
									if (!this.IsNull)
									{
										num = AssemblyCache.GetLength(coercedValue);
										goto IL_2AF;
									}
									goto IL_2AF;
								case SqlDbType.Structured:
									num = -1;
									goto IL_2AF;
								case SqlDbType.Time:
									this._actualSize = (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_2AF;
								case SqlDbType.DateTime2:
									this._actualSize = 3 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_2AF;
								case SqlDbType.DateTimeOffset:
									this._actualSize = 5 + (flag ? 5 : MetaType.GetTimeSizeFromScale(this.GetActualScale()));
									goto IL_2AF;
								default:
									goto IL_2AF;
								}
							}
							num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.StringSize(coercedValue, this._coercedValueIsSqlType) : 0);
							this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
							this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
							if (this._actualSize == -1)
							{
								this._actualSize = num;
							}
							this._actualSize <<= 1;
							goto IL_2AF;
						}
						goto IL_1E2;
					}
					IL_172:
					num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.StringSize(coercedValue, this._coercedValueIsSqlType) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
						goto IL_2AF;
					}
					goto IL_2AF;
					IL_1E2:
					num = ((!this._isNull && !this._coercedValueIsDataFeed) ? SqlParameter.BinarySize(coercedValue, this._coercedValueIsSqlType) : 0);
					this._actualSize = (this.ShouldSerializeSize() ? this.Size : 0);
					this._actualSize = ((this.ShouldSerializeSize() && this._actualSize <= num) ? this._actualSize : num);
					if (this._actualSize == -1)
					{
						this._actualSize = num;
					}
					IL_2AF:
					if (flag && num > 8000)
					{
						throw SQL.ParameterInvalidVariant(this.ParameterName);
					}
				}
			}
			return this._actualSize;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x000D5BC8 File Offset: 0x000D4FC8
		object ICloneable.Clone()
		{
			return new SqlParameter(this);
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x000D5BDC File Offset: 0x000D4FDC
		internal static object CoerceValue(object value, MetaType destinationType, out bool coercedToDataFeed, out bool typeChanged, bool allowStreaming = true)
		{
			coercedToDataFeed = false;
			typeChanged = false;
			Type type = value.GetType();
			if (typeof(object) != destinationType.ClassType && type != destinationType.ClassType && (type != destinationType.SqlType || SqlDbType.Xml == destinationType.SqlDbType))
			{
				try
				{
					typeChanged = true;
					if (typeof(string) == destinationType.ClassType)
					{
						if (typeof(SqlXml) == type)
						{
							value = MetaType.GetStringFromXml(((SqlXml)value).CreateReader());
						}
						else if (typeof(SqlString) == type)
						{
							typeChanged = false;
						}
						else if (typeof(XmlReader).IsAssignableFrom(type))
						{
							if (allowStreaming)
							{
								coercedToDataFeed = true;
								value = new XmlDataFeed((XmlReader)value);
							}
							else
							{
								value = MetaType.GetStringFromXml((XmlReader)value);
							}
						}
						else if (typeof(char[]) == type)
						{
							value = new string((char[])value);
						}
						else if (typeof(SqlChars) == type)
						{
							value = new string(((SqlChars)value).Value);
						}
						else if (value is TextReader && allowStreaming)
						{
							coercedToDataFeed = true;
							value = new TextDataFeed((TextReader)value);
						}
						else
						{
							value = Convert.ChangeType(value, destinationType.ClassType, null);
						}
					}
					else if (DbType.Currency == destinationType.DbType && typeof(string) == type)
					{
						value = decimal.Parse((string)value, NumberStyles.Currency, null);
					}
					else if (typeof(SqlBytes) == type && typeof(byte[]) == destinationType.ClassType)
					{
						typeChanged = false;
					}
					else if (typeof(string) == type && SqlDbType.Time == destinationType.SqlDbType)
					{
						value = TimeSpan.Parse((string)value);
					}
					else if (typeof(string) == type && SqlDbType.DateTimeOffset == destinationType.SqlDbType)
					{
						value = DateTimeOffset.Parse((string)value, null);
					}
					else if (typeof(DateTime) == type && SqlDbType.DateTimeOffset == destinationType.SqlDbType)
					{
						value = new DateTimeOffset((DateTime)value);
					}
					else if (243 == destinationType.TDSType && (value is DataTable || value is DbDataReader || value is IEnumerable<SqlDataRecord>))
					{
						typeChanged = false;
					}
					else if (destinationType.ClassType == typeof(byte[]) && value is Stream && allowStreaming)
					{
						coercedToDataFeed = true;
						value = new StreamDataFeed((Stream)value);
					}
					else
					{
						value = Convert.ChangeType(value, destinationType.ClassType, null);
					}
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					throw ADP.ParameterConversionFailed(value, destinationType.ClassType, ex);
				}
			}
			return value;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000D5F0C File Offset: 0x000D530C
		internal void FixStreamDataForNonPLP()
		{
			object coercedValue = this.GetCoercedValue();
			if (!this._coercedValueIsDataFeed)
			{
				return;
			}
			this._coercedValueIsDataFeed = false;
			if (coercedValue is TextDataFeed)
			{
				if (this.Size > 0)
				{
					char[] array = new char[this.Size];
					int length = ((TextDataFeed)coercedValue)._source.ReadBlock(array, 0, this.Size);
					this.CoercedValue = new string(array, 0, length);
					return;
				}
				this.CoercedValue = ((TextDataFeed)coercedValue)._source.ReadToEnd();
				return;
			}
			else if (coercedValue is StreamDataFeed)
			{
				if (this.Size > 0)
				{
					byte[] array2 = new byte[this.Size];
					int i = 0;
					Stream source = ((StreamDataFeed)coercedValue)._source;
					while (i < this.Size)
					{
						int num = source.Read(array2, i, this.Size - i);
						if (num == 0)
						{
							break;
						}
						i += num;
					}
					if (i < this.Size)
					{
						Array.Resize<byte>(ref array2, i);
					}
					this.CoercedValue = array2;
					return;
				}
				MemoryStream memoryStream = new MemoryStream();
				((StreamDataFeed)coercedValue)._source.CopyTo(memoryStream);
				this.CoercedValue = memoryStream.ToArray();
				return;
			}
			else
			{
				if (coercedValue is XmlDataFeed)
				{
					this.CoercedValue = MetaType.GetStringFromXml(((XmlDataFeed)coercedValue)._source);
					return;
				}
				return;
			}
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000D6044 File Offset: 0x000D5444
		private void CloneHelper(SqlParameter destination)
		{
			this.CloneHelperCore(destination);
			destination._metaType = this._metaType;
			destination._collation = this._collation;
			destination._xmlSchemaCollectionDatabase = this._xmlSchemaCollectionDatabase;
			destination._xmlSchemaCollectionOwningSchema = this._xmlSchemaCollectionOwningSchema;
			destination._xmlSchemaCollectionName = this._xmlSchemaCollectionName;
			destination._udtTypeName = this._udtTypeName;
			destination._typeName = this._typeName;
			destination._udtLoadError = this._udtLoadError;
			destination._parameterName = this._parameterName;
			destination._precision = this._precision;
			destination._scale = this._scale;
			destination._sqlBufferReturnValue = this._sqlBufferReturnValue;
			destination._isSqlParameterSqlType = this._isSqlParameterSqlType;
			destination._internalMetaType = this._internalMetaType;
			destination.CoercedValue = this.CoercedValue;
			destination._valueAsINullable = this._valueAsINullable;
			destination._isNull = this._isNull;
			destination._coercedValueIsDataFeed = this._coercedValueIsDataFeed;
			destination._coercedValueIsSqlType = this._coercedValueIsSqlType;
			destination._actualSize = this._actualSize;
			destination.ForceColumnEncryption = this.ForceColumnEncryption;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000D6154 File Offset: 0x000D5554
		internal byte GetActualPrecision()
		{
			if (!this.ShouldSerializePrecision())
			{
				return this.ValuePrecision(this.CoercedValue);
			}
			return this.PrecisionInternal;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000D617C File Offset: 0x000D557C
		internal byte GetActualScale()
		{
			if (this.ShouldSerializeScale())
			{
				return this.ScaleInternal;
			}
			if (this.GetMetaTypeOnly().IsVarTime)
			{
				return 7;
			}
			return this.ValueScale(this.CoercedValue);
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000D61B4 File Offset: 0x000D55B4
		internal int GetParameterSize()
		{
			if (!this.ShouldSerializeSize())
			{
				return this.ValueSize(this.CoercedValue);
			}
			return this.Size;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000D61DC File Offset: 0x000D55DC
		private void GetActualFieldsAndProperties(out List<SmiExtendedMetaData> fields, out SmiMetaDataPropertyCollection props, out ParameterPeekAheadValue peekAhead)
		{
			fields = null;
			props = null;
			peekAhead = null;
			object coercedValue = this.GetCoercedValue();
			if (coercedValue is DataTable)
			{
				DataTable dataTable = coercedValue as DataTable;
				if (dataTable.Columns.Count <= 0)
				{
					throw SQL.NotEnoughColumnsInStructuredType();
				}
				fields = new List<SmiExtendedMetaData>(dataTable.Columns.Count);
				bool[] array = new bool[dataTable.Columns.Count];
				bool flag = false;
				if (dataTable.PrimaryKey != null && dataTable.PrimaryKey.Length != 0)
				{
					foreach (DataColumn dataColumn in dataTable.PrimaryKey)
					{
						array[dataColumn.Ordinal] = true;
						flag = true;
					}
				}
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					fields.Add(MetaDataUtilsSmi.SmiMetaDataFromDataColumn(dataTable.Columns[j], dataTable));
					if (!flag && dataTable.Columns[j].Unique)
					{
						array[j] = true;
						flag = true;
					}
				}
				if (flag)
				{
					props = new SmiMetaDataPropertyCollection();
					props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array));
					return;
				}
			}
			else if (coercedValue is SqlDataReader)
			{
				fields = new List<SmiExtendedMetaData>(((SqlDataReader)coercedValue).GetInternalSmiMetaData());
				if (fields.Count <= 0)
				{
					throw SQL.NotEnoughColumnsInStructuredType();
				}
				bool[] array2 = new bool[fields.Count];
				bool flag2 = false;
				for (int k = 0; k < fields.Count; k++)
				{
					SmiQueryMetaData smiQueryMetaData = fields[k] as SmiQueryMetaData;
					if (smiQueryMetaData != null && !smiQueryMetaData.IsKey.IsNull && smiQueryMetaData.IsKey.Value)
					{
						array2[k] = true;
						flag2 = true;
					}
				}
				if (flag2)
				{
					props = new SmiMetaDataPropertyCollection();
					props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array2));
					return;
				}
			}
			else
			{
				if (coercedValue is IEnumerable<SqlDataRecord>)
				{
					IEnumerator<SqlDataRecord> enumerator = ((IEnumerable<SqlDataRecord>)coercedValue).GetEnumerator();
					try
					{
						if (!enumerator.MoveNext())
						{
							throw SQL.IEnumerableOfSqlDataRecordHasNoRows();
						}
						SqlDataRecord sqlDataRecord = enumerator.Current;
						int fieldCount = sqlDataRecord.FieldCount;
						if (0 < fieldCount)
						{
							bool[] array3 = new bool[fieldCount];
							bool[] array4 = new bool[fieldCount];
							bool[] array5 = new bool[fieldCount];
							int num = -1;
							bool flag3 = false;
							bool flag4 = false;
							int num2 = 0;
							SmiOrderProperty.SmiColumnOrder[] array6 = new SmiOrderProperty.SmiColumnOrder[fieldCount];
							fields = new List<SmiExtendedMetaData>(fieldCount);
							for (int l = 0; l < fieldCount; l++)
							{
								SqlMetaData sqlMetaData = sqlDataRecord.GetSqlMetaData(l);
								fields.Add(MetaDataUtilsSmi.SqlMetaDataToSmiExtendedMetaData(sqlMetaData));
								if (sqlMetaData.IsUniqueKey)
								{
									array3[l] = true;
									flag3 = true;
								}
								if (sqlMetaData.UseServerDefault)
								{
									array4[l] = true;
									flag4 = true;
								}
								array6[l].Order = sqlMetaData.SortOrder;
								if (SortOrder.Unspecified != sqlMetaData.SortOrder)
								{
									if (fieldCount <= sqlMetaData.SortOrdinal)
									{
										throw SQL.SortOrdinalGreaterThanFieldCount(l, sqlMetaData.SortOrdinal);
									}
									if (array5[sqlMetaData.SortOrdinal])
									{
										throw SQL.DuplicateSortOrdinal(sqlMetaData.SortOrdinal);
									}
									array6[l].SortOrdinal = sqlMetaData.SortOrdinal;
									array5[sqlMetaData.SortOrdinal] = true;
									if (sqlMetaData.SortOrdinal > num)
									{
										num = sqlMetaData.SortOrdinal;
									}
									num2++;
								}
							}
							if (flag3)
							{
								props = new SmiMetaDataPropertyCollection();
								props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array3));
							}
							if (flag4)
							{
								if (props == null)
								{
									props = new SmiMetaDataPropertyCollection();
								}
								props[SmiPropertySelector.DefaultFields] = new SmiDefaultFieldsProperty(new List<bool>(array4));
							}
							if (0 < num2)
							{
								if (num >= num2)
								{
									int num3 = 0;
									while (num3 < num2 && array5[num3])
									{
										num3++;
									}
									throw SQL.MissingSortOrdinal(num3);
								}
								if (props == null)
								{
									props = new SmiMetaDataPropertyCollection();
								}
								props[SmiPropertySelector.SortOrder] = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>(array6));
							}
							peekAhead = new ParameterPeekAheadValue();
							peekAhead.Enumerator = enumerator;
							peekAhead.FirstRecord = sqlDataRecord;
							enumerator = null;
							return;
						}
						throw SQL.NotEnoughColumnsInStructuredType();
					}
					finally
					{
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
				if (coercedValue is DbDataReader)
				{
					DataTable schemaTable = ((DbDataReader)coercedValue).GetSchemaTable();
					if (schemaTable.Rows.Count <= 0)
					{
						throw SQL.NotEnoughColumnsInStructuredType();
					}
					int count = schemaTable.Rows.Count;
					fields = new List<SmiExtendedMetaData>(count);
					bool[] array7 = new bool[count];
					bool flag5 = false;
					int ordinal = schemaTable.Columns[SchemaTableColumn.IsKey].Ordinal;
					int ordinal2 = schemaTable.Columns[SchemaTableColumn.ColumnOrdinal].Ordinal;
					for (int m = 0; m < count; m++)
					{
						DataRow dataRow = schemaTable.Rows[m];
						SmiExtendedMetaData smiExtendedMetaData = MetaDataUtilsSmi.SmiMetaDataFromSchemaTableRow(dataRow);
						int n = m;
						if (!dataRow.IsNull(ordinal2))
						{
							n = (int)dataRow[ordinal2];
						}
						if (n >= count || n < 0)
						{
							throw SQL.InvalidSchemaTableOrdinals();
						}
						while (n > fields.Count)
						{
							fields.Add(null);
						}
						if (fields.Count == n)
						{
							fields.Add(smiExtendedMetaData);
						}
						else
						{
							if (fields[n] != null)
							{
								throw SQL.InvalidSchemaTableOrdinals();
							}
							fields[n] = smiExtendedMetaData;
						}
						if (!dataRow.IsNull(ordinal) && (bool)dataRow[ordinal])
						{
							array7[n] = true;
							flag5 = true;
						}
					}
					if (flag5)
					{
						props = new SmiMetaDataPropertyCollection();
						props[SmiPropertySelector.UniqueKey] = new SmiUniqueKeyProperty(new List<bool>(array7));
					}
				}
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000D6744 File Offset: 0x000D5B44
		internal object GetCoercedValue()
		{
			if (this._coercedValue == null || this._internalMetaType.SqlDbType == SqlDbType.Udt)
			{
				bool flag = this.Value is DataFeed;
				if (this.IsNull || flag)
				{
					this._coercedValue = this.Value;
					this._coercedValueIsSqlType = (this._coercedValue != null && this._isSqlParameterSqlType);
					this._coercedValueIsDataFeed = flag;
					this._actualSize = (this.IsNull ? 0 : -1);
				}
				else
				{
					bool flag2;
					this._coercedValue = SqlParameter.CoerceValue(this.Value, this._internalMetaType, out this._coercedValueIsDataFeed, out flag2, true);
					this._coercedValueIsSqlType = (this._isSqlParameterSqlType && !flag2);
					this._actualSize = -1;
				}
			}
			return this._coercedValue;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x000D6804 File Offset: 0x000D5C04
		internal bool CoercedValueIsSqlType
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this._coercedValueIsSqlType;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x000D6828 File Offset: 0x000D5C28
		internal bool CoercedValueIsDataFeed
		{
			get
			{
				if (this._coercedValue == null)
				{
					this.GetCoercedValue();
				}
				return this._coercedValueIsDataFeed;
			}
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000D684C File Offset: 0x000D5C4C
		[Conditional("DEBUG")]
		internal void AssertCachedPropertiesAreValid()
		{
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000D685C File Offset: 0x000D5C5C
		[Conditional("DEBUG")]
		internal void AssertPropertiesAreValid(object value, bool? isSqlType = null, bool? isDataFeed = null, bool? isNull = null)
		{
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000D686C File Offset: 0x000D5C6C
		private SqlDbType GetMetaSqlDbTypeOnly()
		{
			MetaType metaType = this._metaType;
			if (metaType == null)
			{
				metaType = MetaType.GetDefaultMetaType();
			}
			return metaType.SqlDbType;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000D6890 File Offset: 0x000D5C90
		private MetaType GetMetaTypeOnly()
		{
			if (this._metaType != null)
			{
				return this._metaType;
			}
			if (this._value != null && DBNull.Value != this._value)
			{
				Type type = this._value.GetType();
				if (typeof(char) == type)
				{
					this._value = this._value.ToString();
					type = typeof(string);
				}
				else if (typeof(char[]) == type)
				{
					this._value = new string((char[])this._value);
					type = typeof(string);
				}
				return MetaType.GetMetaTypeFromType(type);
			}
			if (this._sqlBufferReturnValue != null)
			{
				Type typeFromStorageType = this._sqlBufferReturnValue.GetTypeFromStorageType(this._isSqlParameterSqlType);
				if (null != typeFromStorageType)
				{
					return MetaType.GetMetaTypeFromType(typeFromStorageType);
				}
			}
			return MetaType.GetDefaultMetaType();
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x000D696C File Offset: 0x000D5D6C
		internal void Prepare(SqlCommand cmd)
		{
			if (this._metaType == null)
			{
				throw ADP.PrepareParameterType(cmd);
			}
			if (!this.ShouldSerializeSize() && !this._metaType.IsFixed)
			{
				throw ADP.PrepareParameterSize(cmd);
			}
			if (!this.ShouldSerializePrecision() && !this.ShouldSerializeScale() && this._metaType.SqlDbType == SqlDbType.Decimal)
			{
				throw ADP.PrepareParameterScale(cmd, this.SqlDbType.ToString());
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x000D69E0 File Offset: 0x000D5DE0
		private void PropertyChanging()
		{
			this._internalMetaType = null;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x000D69F4 File Offset: 0x000D5DF4
		private void PropertyTypeChanging()
		{
			this.PropertyChanging();
			this.CoercedValue = null;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000D6A10 File Offset: 0x000D5E10
		internal void SetSqlBuffer(SqlBuffer buff)
		{
			this._sqlBufferReturnValue = buff;
			this._value = null;
			this._coercedValue = null;
			this._isNull = this._sqlBufferReturnValue.IsNull;
			this._coercedValueIsDataFeed = false;
			this._coercedValueIsSqlType = false;
			this._udtLoadError = null;
			this._actualSize = -1;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000D6A60 File Offset: 0x000D5E60
		internal void SetUdtLoadError(Exception e)
		{
			this._udtLoadError = e;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000D6A74 File Offset: 0x000D5E74
		internal void Validate(int index, bool isCommandProc)
		{
			MetaType metaTypeOnly = this.GetMetaTypeOnly();
			this._internalMetaType = metaTypeOnly;
			if (ADP.IsDirection(this, ParameterDirection.Output) && !ADP.IsDirection(this, ParameterDirection.ReturnValue) && !metaTypeOnly.IsFixed && !this.ShouldSerializeSize() && (this._value == null || Convert.IsDBNull(this._value)) && this.SqlDbType != SqlDbType.Timestamp && this.SqlDbType != SqlDbType.Udt && this.SqlDbType != SqlDbType.Xml && !metaTypeOnly.IsVarTime)
			{
				throw ADP.UninitializedParameterSize(index, metaTypeOnly.ClassType);
			}
			if (metaTypeOnly.SqlDbType != SqlDbType.Udt && this.Direction != ParameterDirection.Output)
			{
				this.GetCoercedValue();
			}
			if (metaTypeOnly.SqlDbType == SqlDbType.Udt)
			{
				if (ADP.IsEmpty(this.UdtTypeName))
				{
					throw SQL.MustSetUdtTypeNameForUdtParams();
				}
			}
			else if (!ADP.IsEmpty(this.UdtTypeName))
			{
				throw SQL.UnexpectedUdtTypeNameForNonUdtParams();
			}
			if (metaTypeOnly.SqlDbType == SqlDbType.Structured)
			{
				if (!isCommandProc && ADP.IsEmpty(this.TypeName))
				{
					throw SQL.MustSetTypeNameForParam(metaTypeOnly.TypeName, this.ParameterName);
				}
				if (ParameterDirection.Input != this.Direction)
				{
					throw SQL.UnsupportedTVPOutputParameter(this.Direction, this.ParameterName);
				}
				if (DBNull.Value == this.GetCoercedValue())
				{
					throw SQL.DBNullNotSupportedForTVPValues(this.ParameterName);
				}
			}
			else if (!ADP.IsEmpty(this.TypeName))
			{
				throw SQL.UnexpectedTypeNameForNonStructParams(this.ParameterName);
			}
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000D6BBC File Offset: 0x000D5FBC
		internal MetaType ValidateTypeLengths(bool yukonOrNewer)
		{
			MetaType metaType = this.InternalMetaType;
			if (SqlDbType.Udt != metaType.SqlDbType && !metaType.IsFixed && !metaType.IsLong)
			{
				long num = (long)this.GetActualSize();
				long num2 = (long)this.Size;
				long num3;
				if (metaType.IsNCharType && yukonOrNewer)
				{
					num3 = ((num2 * 2L > num) ? (num2 * 2L) : num);
				}
				else
				{
					num3 = ((num2 > num) ? num2 : num);
				}
				if (num3 > 8000L || this._coercedValueIsDataFeed || num2 == -1L || num == -1L)
				{
					if (yukonOrNewer)
					{
						metaType = MetaType.GetMaxMetaTypeFromMetaType(metaType);
						this._metaType = metaType;
						this.InternalMetaType = metaType;
						if (!metaType.IsPlp)
						{
							if (metaType.SqlDbType == SqlDbType.Xml)
							{
								throw ADP.InvalidMetaDataValue();
							}
							if (metaType.SqlDbType == SqlDbType.NVarChar || metaType.SqlDbType == SqlDbType.VarChar || metaType.SqlDbType == SqlDbType.VarBinary)
							{
								this.Size = -1;
							}
						}
					}
					else
					{
						SqlDbType sqlDbType = metaType.SqlDbType;
						if (sqlDbType <= SqlDbType.NChar)
						{
							if (sqlDbType != SqlDbType.Binary)
							{
								if (sqlDbType == SqlDbType.Char)
								{
									goto IL_127;
								}
								if (sqlDbType != SqlDbType.NChar)
								{
									return metaType;
								}
								goto IL_140;
							}
						}
						else
						{
							if (sqlDbType == SqlDbType.NVarChar)
							{
								goto IL_140;
							}
							if (sqlDbType != SqlDbType.VarBinary)
							{
								if (sqlDbType != SqlDbType.VarChar)
								{
									return metaType;
								}
								goto IL_127;
							}
						}
						metaType = MetaType.GetMetaTypeFromSqlDbType(SqlDbType.Image, false);
						this._metaType = metaType;
						this.InternalMetaType = metaType;
						return metaType;
						IL_127:
						metaType = MetaType.GetMetaTypeFromSqlDbType(SqlDbType.Text, false);
						this._metaType = metaType;
						this.InternalMetaType = metaType;
						return metaType;
						IL_140:
						metaType = MetaType.GetMetaTypeFromSqlDbType(SqlDbType.NText, false);
						this._metaType = metaType;
						this.InternalMetaType = metaType;
					}
				}
			}
			return metaType;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000D6D24 File Offset: 0x000D6124
		private byte ValuePrecision(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValuePrecisionCore(value);
			}
			if (((SqlDecimal)value).IsNull)
			{
				return 0;
			}
			return ((SqlDecimal)value).Precision;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x000D6D64 File Offset: 0x000D6164
		private byte ValueScale(object value)
		{
			if (!(value is SqlDecimal))
			{
				return this.ValueScaleCore(value);
			}
			if (((SqlDecimal)value).IsNull)
			{
				return 0;
			}
			return ((SqlDecimal)value).Scale;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000D6DA4 File Offset: 0x000D61A4
		private static int StringSize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlString)
				{
					return ((SqlString)value).Value.Length;
				}
				if (value is SqlChars)
				{
					return ((SqlChars)value).Value.Length;
				}
			}
			else
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				char[] array = value as char[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x000D6E10 File Offset: 0x000D6210
		private static int BinarySize(object value, bool isSqlType)
		{
			if (isSqlType)
			{
				if (value is SqlBinary)
				{
					return ((SqlBinary)value).Length;
				}
				if (value is SqlBytes)
				{
					return ((SqlBytes)value).Value.Length;
				}
			}
			else
			{
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				if (value is byte)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x000D6E68 File Offset: 0x000D6268
		private int ValueSize(object value)
		{
			if (value is SqlString)
			{
				if (((SqlString)value).IsNull)
				{
					return 0;
				}
				return ((SqlString)value).Value.Length;
			}
			else if (value is SqlChars)
			{
				if (((SqlChars)value).IsNull)
				{
					return 0;
				}
				return ((SqlChars)value).Value.Length;
			}
			else if (value is SqlBinary)
			{
				if (((SqlBinary)value).IsNull)
				{
					return 0;
				}
				return ((SqlBinary)value).Length;
			}
			else if (value is SqlBytes)
			{
				if (((SqlBytes)value).IsNull)
				{
					return 0;
				}
				return (int)((SqlBytes)value).Length;
			}
			else
			{
				if (value is DataFeed)
				{
					return 0;
				}
				return this.ValueSizeCore(value);
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x000D6F28 File Offset: 0x000D6328
		internal static string[] ParseTypeName(string typeName, bool isUdtTypeName)
		{
			string[] result;
			try
			{
				string property;
				if (isUdtTypeName)
				{
					property = "SQL_UDTTypeName";
				}
				else
				{
					property = "SQL_TypeName";
				}
				result = MultipartIdentifier.ParseMultipartIdentifier(typeName, "[\"", "]\"", '.', 3, true, property, true);
			}
			catch (ArgumentException)
			{
				if (isUdtTypeName)
				{
					throw SQL.InvalidUdt3PartNameFormat();
				}
				throw SQL.InvalidParameterTypeNameFormat();
			}
			return result;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000D6F90 File Offset: 0x000D6390
		private SqlParameter(SqlParameter source) : this()
		{
			ADP.CheckArgumentNull(source, "source");
			source.CloneHelper(this);
			ICloneable cloneable = this._value as ICloneable;
			if (cloneable != null)
			{
				this._value = cloneable.Clone();
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000D6FD0 File Offset: 0x000D63D0
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x000D6FE4 File Offset: 0x000D63E4
		private object CoercedValue
		{
			get
			{
				return this._coercedValue;
			}
			set
			{
				this._coercedValue = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x000D6FF8 File Offset: 0x000D63F8
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x000D7014 File Offset: 0x000D6414
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbParameter_Direction")]
		[ResCategory("DataCategory_Data")]
		public override ParameterDirection Direction
		{
			get
			{
				ParameterDirection direction = this._direction;
				if (direction == (ParameterDirection)0)
				{
					return ParameterDirection.Input;
				}
				return direction;
			}
			set
			{
				if (this._direction == value)
				{
					return;
				}
				if (value - ParameterDirection.Input <= 2 || value == ParameterDirection.ReturnValue)
				{
					this.PropertyChanging();
					this._direction = value;
					return;
				}
				throw ADP.InvalidParameterDirection(value);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x000D704C File Offset: 0x000D644C
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x000D7060 File Offset: 0x000D6460
		public override bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x000D7074 File Offset: 0x000D6474
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x000D7088 File Offset: 0x000D6488
		[ResDescription("DbParameter_Offset")]
		[Browsable(false)]
		[ResCategory("DataCategory_Data")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				if (value < 0)
				{
					throw ADP.InvalidOffsetValue(value);
				}
				this._offset = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x000D70A8 File Offset: 0x000D64A8
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x000D70D0 File Offset: 0x000D64D0
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbParameter_Size")]
		public override int Size
		{
			get
			{
				int num = this._size;
				if (num == 0)
				{
					num = this.ValueSize(this.Value);
				}
				return num;
			}
			set
			{
				if (this._size != value)
				{
					if (value < -1)
					{
						throw ADP.InvalidSizeValue(value);
					}
					this.PropertyChanging();
					this._size = value;
				}
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000D7100 File Offset: 0x000D6500
		private void ResetSize()
		{
			if (this._size != 0)
			{
				this.PropertyChanging();
				this._size = 0;
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000D7124 File Offset: 0x000D6524
		private bool ShouldSerializeSize()
		{
			return this._size != 0;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x000D713C File Offset: 0x000D653C
		// (set) Token: 0x06001EC5 RID: 7877 RVA: 0x000D715C File Offset: 0x000D655C
		[ResDescription("DbParameter_SourceColumn")]
		[ResCategory("DataCategory_Update")]
		public override string SourceColumn
		{
			get
			{
				string sourceColumn = this._sourceColumn;
				if (sourceColumn == null)
				{
					return ADP.StrEmpty;
				}
				return sourceColumn;
			}
			set
			{
				this._sourceColumn = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x000D7170 File Offset: 0x000D6570
		// (set) Token: 0x06001EC7 RID: 7879 RVA: 0x000D7184 File Offset: 0x000D6584
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this._sourceColumnNullMapping;
			}
			set
			{
				this._sourceColumnNullMapping = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x000D7198 File Offset: 0x000D6598
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x000D71B8 File Offset: 0x000D65B8
		[ResDescription("DbParameter_SourceVersion")]
		[ResCategory("DataCategory_Update")]
		public override DataRowVersion SourceVersion
		{
			get
			{
				DataRowVersion sourceVersion = this._sourceVersion;
				if (sourceVersion == (DataRowVersion)0)
				{
					return DataRowVersion.Current;
				}
				return sourceVersion;
			}
			set
			{
				if (value <= DataRowVersion.Current)
				{
					if (value != DataRowVersion.Original && value != DataRowVersion.Current)
					{
						goto IL_32;
					}
				}
				else if (value != DataRowVersion.Proposed && value != DataRowVersion.Default)
				{
					goto IL_32;
				}
				this._sourceVersion = value;
				return;
				IL_32:
				throw ADP.InvalidDataRowVersion(value);
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x000D7200 File Offset: 0x000D6600
		private void CloneHelperCore(SqlParameter destination)
		{
			destination._value = this._value;
			destination._direction = this._direction;
			destination._size = this._size;
			destination._offset = this._offset;
			destination._sourceColumn = this._sourceColumn;
			destination._sourceVersion = this._sourceVersion;
			destination._sourceColumnNullMapping = this._sourceColumnNullMapping;
			destination._isNullable = this._isNullable;
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000D7270 File Offset: 0x000D6670
		internal void CopyTo(DbParameter destination)
		{
			ADP.CheckArgumentNull(destination, "destination");
			this.CloneHelper((SqlParameter)destination);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x000D7294 File Offset: 0x000D6694
		internal object CompareExchangeParent(object value, object comparand)
		{
			object parent = this._parent;
			if (comparand == parent)
			{
				this._parent = value;
			}
			return parent;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x000D72B4 File Offset: 0x000D66B4
		internal void ResetParent()
		{
			this._parent = null;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x000D72C8 File Offset: 0x000D66C8
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x000D72DC File Offset: 0x000D66DC
		private byte ValuePrecisionCore(object value)
		{
			if (value is decimal)
			{
				return ((decimal)value).Precision;
			}
			return 0;
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x000D7308 File Offset: 0x000D6708
		private byte ValueScaleCore(object value)
		{
			if (value is decimal)
			{
				return (byte)((decimal.GetBits((decimal)value)[3] & 16711680) >> 16);
			}
			return 0;
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x000D7338 File Offset: 0x000D6738
		private int ValueSizeCore(object value)
		{
			if (!ADP.IsNull(value))
			{
				string text = value as string;
				if (text != null)
				{
					return text.Length;
				}
				byte[] array = value as byte[];
				if (array != null)
				{
					return array.Length;
				}
				char[] array2 = value as char[];
				if (array2 != null)
				{
					return array2.Length;
				}
				if (value is byte || value is char)
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x04001175 RID: 4469
		private MetaType _metaType;

		// Token: 0x04001176 RID: 4470
		private SqlCollation _collation;

		// Token: 0x04001177 RID: 4471
		private string _xmlSchemaCollectionDatabase;

		// Token: 0x04001178 RID: 4472
		private string _xmlSchemaCollectionOwningSchema;

		// Token: 0x04001179 RID: 4473
		private string _xmlSchemaCollectionName;

		// Token: 0x0400117A RID: 4474
		private string _udtTypeName;

		// Token: 0x0400117B RID: 4475
		private string _typeName;

		// Token: 0x0400117C RID: 4476
		private Type _udtType;

		// Token: 0x0400117D RID: 4477
		private Exception _udtLoadError;

		// Token: 0x0400117E RID: 4478
		private string _parameterName;

		// Token: 0x0400117F RID: 4479
		private byte _precision;

		// Token: 0x04001180 RID: 4480
		private byte _scale;

		// Token: 0x04001181 RID: 4481
		private bool _hasScale;

		// Token: 0x04001182 RID: 4482
		private MetaType _internalMetaType;

		// Token: 0x04001183 RID: 4483
		private SqlBuffer _sqlBufferReturnValue;

		// Token: 0x04001184 RID: 4484
		private INullable _valueAsINullable;

		// Token: 0x04001185 RID: 4485
		private bool _isSqlParameterSqlType;

		// Token: 0x04001186 RID: 4486
		private bool _isNull = true;

		// Token: 0x04001187 RID: 4487
		private bool _coercedValueIsSqlType;

		// Token: 0x04001188 RID: 4488
		private bool _coercedValueIsDataFeed;

		// Token: 0x04001189 RID: 4489
		private int _actualSize = -1;

		// Token: 0x0400118A RID: 4490
		private SqlCipherMetadata _columnEncryptionCipherMetadata;

		// Token: 0x0400118D RID: 4493
		private object _value;

		// Token: 0x0400118E RID: 4494
		private object _parent;

		// Token: 0x0400118F RID: 4495
		private ParameterDirection _direction;

		// Token: 0x04001190 RID: 4496
		private int _size;

		// Token: 0x04001191 RID: 4497
		private int _offset;

		// Token: 0x04001192 RID: 4498
		private string _sourceColumn;

		// Token: 0x04001193 RID: 4499
		private DataRowVersion _sourceVersion;

		// Token: 0x04001194 RID: 4500
		private bool _sourceColumnNullMapping;

		// Token: 0x04001195 RID: 4501
		private bool _isNullable;

		// Token: 0x04001196 RID: 4502
		private object _coercedValue;

		// Token: 0x020003CF RID: 975
		internal sealed class SqlParameterConverter : ExpandableObjectConverter
		{
			// Token: 0x06003548 RID: 13640 RVA: 0x00144720 File Offset: 0x00143B20
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06003549 RID: 13641 RVA: 0x0014474C File Offset: 0x00143B4C
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == null)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is SqlParameter)
				{
					return this.ConvertToInstanceDescriptor(value as SqlParameter);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}

			// Token: 0x0600354A RID: 13642 RVA: 0x001447A4 File Offset: 0x00143BA4
			private InstanceDescriptor ConvertToInstanceDescriptor(SqlParameter p)
			{
				int num = 0;
				if (p.ShouldSerializeSqlDbType())
				{
					num |= 1;
				}
				if (p.ShouldSerializeSize())
				{
					num |= 2;
				}
				if (!ADP.IsEmpty(p.SourceColumn))
				{
					num |= 4;
				}
				if (p.Value != null)
				{
					num |= 8;
				}
				if (ParameterDirection.Input != p.Direction || p.IsNullable || p.ShouldSerializePrecision() || p.ShouldSerializeScale() || DataRowVersion.Current != p.SourceVersion)
				{
					num |= 16;
				}
				if (p.SourceColumnNullMapping || !ADP.IsEmpty(p.XmlSchemaCollectionDatabase) || !ADP.IsEmpty(p.XmlSchemaCollectionOwningSchema) || !ADP.IsEmpty(p.XmlSchemaCollectionName))
				{
					num |= 32;
				}
				Type[] types;
				object[] arguments;
				switch (num)
				{
				case 0:
				case 1:
					types = new Type[]
					{
						typeof(string),
						typeof(SqlDbType)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.SqlDbType
					};
					break;
				case 2:
				case 3:
					types = new Type[]
					{
						typeof(string),
						typeof(SqlDbType),
						typeof(int)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.SqlDbType,
						p.Size
					};
					break;
				case 4:
				case 5:
				case 6:
				case 7:
					types = new Type[]
					{
						typeof(string),
						typeof(SqlDbType),
						typeof(int),
						typeof(string)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.SqlDbType,
						p.Size,
						p.SourceColumn
					};
					break;
				case 8:
					types = new Type[]
					{
						typeof(string),
						typeof(object)
					};
					arguments = new object[]
					{
						p.ParameterName,
						p.Value
					};
					break;
				default:
					if ((32 & num) == 0)
					{
						types = new Type[]
						{
							typeof(string),
							typeof(SqlDbType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(bool),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(object)
						};
						arguments = new object[]
						{
							p.ParameterName,
							p.SqlDbType,
							p.Size,
							p.Direction,
							p.IsNullable,
							p.PrecisionInternal,
							p.ScaleInternal,
							p.SourceColumn,
							p.SourceVersion,
							p.Value
						};
					}
					else
					{
						types = new Type[]
						{
							typeof(string),
							typeof(SqlDbType),
							typeof(int),
							typeof(ParameterDirection),
							typeof(byte),
							typeof(byte),
							typeof(string),
							typeof(DataRowVersion),
							typeof(bool),
							typeof(object),
							typeof(string),
							typeof(string),
							typeof(string)
						};
						arguments = new object[]
						{
							p.ParameterName,
							p.SqlDbType,
							p.Size,
							p.Direction,
							p.PrecisionInternal,
							p.ScaleInternal,
							p.SourceColumn,
							p.SourceVersion,
							p.SourceColumnNullMapping,
							p.Value,
							p.XmlSchemaCollectionDatabase,
							p.XmlSchemaCollectionOwningSchema,
							p.XmlSchemaCollectionName
						};
					}
					break;
				}
				ConstructorInfo constructor = typeof(SqlParameter).GetConstructor(types);
				return new InstanceDescriptor(constructor, arguments);
			}
		}
	}
}
