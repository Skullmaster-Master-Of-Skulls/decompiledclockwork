using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200005B RID: 91
	public sealed class SqlMetaData
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x00043BD4 File Offset: 0x00042FD4
		public SqlMetaData(string name, SqlDbType dbType)
		{
			this.Construct(name, dbType, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00043BF4 File Offset: 0x00042FF4
		public SqlMetaData(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00043C18 File Offset: 0x00043018
		public SqlMetaData(string name, SqlDbType dbType, long maxLength)
		{
			this.Construct(name, dbType, maxLength, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00043C38 File Offset: 0x00043038
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00043C5C File Offset: 0x0004305C
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType)
		{
			this.Construct(name, dbType, userDefinedType, null, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00043C80 File Offset: 0x00043080
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00043CA4 File Offset: 0x000430A4
		public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, userDefinedType, serverTypeName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00043CCC File Offset: 0x000430CC
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale)
		{
			this.Construct(name, dbType, precision, scale, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00043CF0 File Offset: 0x000430F0
		public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00043D18 File Offset: 0x00043118
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00043D3C File Offset: 0x0004313C
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, maxLength, locale, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00043D64 File Offset: 0x00043164
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00043D8C File Offset: 0x0004318C
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long locale, SqlCompareOptions compareOptions, Type userDefinedType) : this(name, dbType, maxLength, precision, scale, locale, compareOptions, userDefinedType, false, false, SortOrder.Unspecified, -1)
		{
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00043DB0 File Offset: 0x000431B0
		public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			switch (dbType)
			{
			case SqlDbType.BigInt:
			case SqlDbType.Bit:
			case SqlDbType.DateTime:
			case SqlDbType.Float:
			case SqlDbType.Image:
			case SqlDbType.Int:
			case SqlDbType.Money:
			case SqlDbType.Real:
			case SqlDbType.UniqueIdentifier:
			case SqlDbType.SmallDateTime:
			case SqlDbType.SmallInt:
			case SqlDbType.SmallMoney:
			case SqlDbType.Timestamp:
			case SqlDbType.TinyInt:
			case SqlDbType.Xml:
			case SqlDbType.Date:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this.Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this.Construct(name, dbType, maxLength, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Decimal:
			case SqlDbType.Time:
			case SqlDbType.DateTime2:
			case SqlDbType.DateTimeOffset:
				this.Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this.Construct(name, dbType, SqlMetaData.Max, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Variant:
				this.Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			case SqlDbType.Udt:
				this.Construct(name, dbType, userDefinedType, "", useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
				return;
			}
			SQL.InvalidSqlDbTypeForConstructor(dbType);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00043EF4 File Offset: 0x000432F4
		public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName)
		{
			this.Construct(name, dbType, database, owningSchema, objectName, false, false, SortOrder.Unspecified, -1);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00043F18 File Offset: 0x00043318
		internal SqlMetaData(string name, SqlDbType sqlDBType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName, bool partialLength, Type udtType)
		{
			this.AssertNameIsValid(name);
			this.m_strName = name;
			this.m_sqlDbType = sqlDBType;
			this.m_lMaxLength = maxLength;
			this.m_bPrecision = precision;
			this.m_bScale = scale;
			this.m_lLocale = localeId;
			this.m_eCompareOptions = compareOptions;
			this.m_XmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this.m_XmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this.m_XmlSchemaCollectionName = xmlSchemaCollectionName;
			this.m_bPartialLength = partialLength;
			this.m_udttype = udtType;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00043F90 File Offset: 0x00043390
		private SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool partialLength)
		{
			this.AssertNameIsValid(name);
			this.m_strName = name;
			this.m_sqlDbType = sqlDbType;
			this.m_lMaxLength = maxLength;
			this.m_bPrecision = precision;
			this.m_bScale = scale;
			this.m_lLocale = localeId;
			this.m_eCompareOptions = compareOptions;
			this.m_bPartialLength = partialLength;
			this.m_udttype = null;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00043FF0 File Offset: 0x000433F0
		public SqlCompareOptions CompareOptions
		{
			get
			{
				return this.m_eCompareOptions;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00044004 File Offset: 0x00043404
		public DbType DbType
		{
			get
			{
				return SqlMetaData.sxm_rgSqlDbTypeToDbType[(int)this.m_sqlDbType];
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x00044020 File Offset: 0x00043420
		public bool IsUniqueKey
		{
			get
			{
				return this.m_isUniqueKey;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00044034 File Offset: 0x00043434
		public long LocaleId
		{
			get
			{
				return this.m_lLocale;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00044048 File Offset: 0x00043448
		public static long Max
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x00044058 File Offset: 0x00043458
		public long MaxLength
		{
			get
			{
				return this.m_lMaxLength;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0004406C File Offset: 0x0004346C
		public string Name
		{
			get
			{
				return this.m_strName;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00044080 File Offset: 0x00043480
		public byte Precision
		{
			get
			{
				return this.m_bPrecision;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00044094 File Offset: 0x00043494
		public byte Scale
		{
			get
			{
				return this.m_bScale;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000440A8 File Offset: 0x000434A8
		public SortOrder SortOrder
		{
			get
			{
				return this.m_columnSortOrder;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000440BC File Offset: 0x000434BC
		public int SortOrdinal
		{
			get
			{
				return this.m_sortOrdinal;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000440D0 File Offset: 0x000434D0
		public SqlDbType SqlDbType
		{
			get
			{
				return this.m_sqlDbType;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x000440E4 File Offset: 0x000434E4
		public Type Type
		{
			get
			{
				return this.m_udttype;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000440F8 File Offset: 0x000434F8
		public string TypeName
		{
			get
			{
				if (this.m_serverTypeName != null)
				{
					return this.m_serverTypeName;
				}
				if (this.SqlDbType == SqlDbType.Udt)
				{
					return this.UdtTypeName;
				}
				return SqlMetaData.sxm_rgDefaults[(int)this.SqlDbType].Name;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00044138 File Offset: 0x00043538
		internal string ServerTypeName
		{
			get
			{
				return this.m_serverTypeName;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0004414C File Offset: 0x0004354C
		public bool UseServerDefault
		{
			get
			{
				return this.m_useServerDefault;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00044160 File Offset: 0x00043560
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this.m_XmlSchemaCollectionDatabase;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00044174 File Offset: 0x00043574
		public string XmlSchemaCollectionName
		{
			get
			{
				return this.m_XmlSchemaCollectionName;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00044188 File Offset: 0x00043588
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this.m_XmlSchemaCollectionOwningSchema;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0004419C File Offset: 0x0004359C
		internal bool IsPartialLength
		{
			get
			{
				return this.m_bPartialLength;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000441B0 File Offset: 0x000435B0
		internal string UdtTypeName
		{
			get
			{
				if (this.SqlDbType != SqlDbType.Udt)
				{
					return null;
				}
				if (this.m_udttype == null)
				{
					return null;
				}
				return this.m_udttype.FullName;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000441E4 File Offset: 0x000435E4
		private void Construct(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (dbType != SqlDbType.BigInt && SqlDbType.Bit != dbType && SqlDbType.DateTime != dbType && SqlDbType.Date != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType && SqlDbType.Decimal != dbType && SqlDbType.Float != dbType && SqlDbType.Image != dbType && SqlDbType.Int != dbType && SqlDbType.Money != dbType && SqlDbType.NText != dbType && SqlDbType.Real != dbType && SqlDbType.SmallDateTime != dbType && SqlDbType.SmallInt != dbType && SqlDbType.SmallMoney != dbType && SqlDbType.Text != dbType && SqlDbType.Time != dbType && SqlDbType.Timestamp != dbType && SqlDbType.TinyInt != dbType && SqlDbType.UniqueIdentifier != dbType && SqlDbType.Variant != dbType && SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			this.SetDefaultsForType(dbType);
			if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				this.m_lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			this.m_strName = name;
			this.m_useServerDefault = useServerDefault;
			this.m_isUniqueKey = isUniqueKey;
			this.m_columnSortOrder = columnSortOrder;
			this.m_sortOrdinal = sortOrdinal;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000442BC File Offset: 0x000436BC
		private void Construct(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			long lLocale = 0L;
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
			{
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
				lLocale = (long)CultureInfo.CurrentCulture.LCID;
			}
			else if (SqlDbType.Binary == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.VarBinary == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.Image != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			this.SetDefaultsForType(dbType);
			this.m_strName = name;
			this.m_lMaxLength = maxLength;
			this.m_lLocale = lLocale;
			this.m_useServerDefault = useServerDefault;
			this.m_isUniqueKey = isUniqueKey;
			this.m_columnSortOrder = columnSortOrder;
			this.m_sortOrdinal = sortOrdinal;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00044570 File Offset: 0x00043970
		private void Construct(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Char == dbType)
			{
				if (maxLength > 8000L || maxLength < 0L)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.VarChar == dbType)
			{
				if ((maxLength > 8000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.NChar == dbType)
			{
				if (maxLength > 4000L || maxLength < 0L)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else if (SqlDbType.NVarChar == dbType)
			{
				if ((maxLength > 4000L || maxLength < 0L) && maxLength != SqlMetaData.Max)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			else
			{
				if (SqlDbType.NText != dbType && SqlDbType.Text != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (SqlMetaData.Max != maxLength)
				{
					throw ADP.Argument(Res.GetString("ADP_InvalidDataLength2", new object[]
					{
						maxLength.ToString(CultureInfo.InvariantCulture)
					}), "maxLength");
				}
			}
			if (SqlCompareOptions.BinarySort != compareOptions && (~(SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth) & compareOptions) != SqlCompareOptions.None)
			{
				throw ADP.InvalidEnumerationValue(typeof(SqlCompareOptions), (int)compareOptions);
			}
			this.SetDefaultsForType(dbType);
			this.m_strName = name;
			this.m_lMaxLength = maxLength;
			this.m_lLocale = locale;
			this.m_eCompareOptions = compareOptions;
			this.m_useServerDefault = useServerDefault;
			this.m_isUniqueKey = isUniqueKey;
			this.m_columnSortOrder = columnSortOrder;
			this.m_sortOrdinal = sortOrdinal;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0004474C File Offset: 0x00043B4C
		private void Construct(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Decimal == dbType)
			{
				if (precision > SqlDecimal.MaxPrecision || scale > precision)
				{
					throw SQL.PrecisionValueOutOfRange(precision);
				}
				if (scale > SqlDecimal.MaxScale)
				{
					throw SQL.ScaleValueOutOfRange(scale);
				}
			}
			else
			{
				if (SqlDbType.Time != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType)
				{
					throw SQL.InvalidSqlDbTypeForConstructor(dbType);
				}
				if (scale > 7)
				{
					throw SQL.TimeScaleValueOutOfRange(scale);
				}
			}
			this.SetDefaultsForType(dbType);
			this.m_strName = name;
			this.m_bPrecision = precision;
			this.m_bScale = scale;
			if (SqlDbType.Decimal == dbType)
			{
				this.m_lMaxLength = (long)((ulong)SqlMetaData.__maxLenFromPrecision[(int)(precision - 1)]);
			}
			else
			{
				this.m_lMaxLength -= (long)((ulong)SqlMetaData.__maxVarTimeLenOffsetFromScale[(int)scale]);
			}
			this.m_useServerDefault = useServerDefault;
			this.m_isUniqueKey = isUniqueKey;
			this.m_columnSortOrder = columnSortOrder;
			this.m_sortOrdinal = sortOrdinal;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00044820 File Offset: 0x00043C20
		private void Construct(string name, SqlDbType dbType, Type userDefinedType, string serverTypeName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Udt != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if (null == userDefinedType)
			{
				throw ADP.ArgumentNull("userDefinedType");
			}
			this.SetDefaultsForType(SqlDbType.Udt);
			this.m_strName = name;
			this.m_lMaxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
			this.m_udttype = userDefinedType;
			this.m_serverTypeName = serverTypeName;
			this.m_useServerDefault = useServerDefault;
			this.m_isUniqueKey = isUniqueKey;
			this.m_columnSortOrder = columnSortOrder;
			this.m_sortOrdinal = sortOrdinal;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000448AC File Offset: 0x00043CAC
		private void Construct(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
		{
			this.AssertNameIsValid(name);
			this.ValidateSortOrder(columnSortOrder, sortOrdinal);
			if (SqlDbType.Xml != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if ((database != null || owningSchema != null) && objectName == null)
			{
				throw ADP.ArgumentNull("objectName");
			}
			this.SetDefaultsForType(SqlDbType.Xml);
			this.m_strName = name;
			this.m_XmlSchemaCollectionDatabase = database;
			this.m_XmlSchemaCollectionOwningSchema = owningSchema;
			this.m_XmlSchemaCollectionName = objectName;
			this.m_useServerDefault = useServerDefault;
			this.m_isUniqueKey = isUniqueKey;
			this.m_columnSortOrder = columnSortOrder;
			this.m_sortOrdinal = sortOrdinal;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00044934 File Offset: 0x00043D34
		private void AssertNameIsValid(string name)
		{
			if (name == null)
			{
				throw ADP.ArgumentNull("name");
			}
			if (128L < (long)name.Length)
			{
				throw SQL.NameTooLong("name");
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0004496C File Offset: 0x00043D6C
		private void ValidateSortOrder(SortOrder columnSortOrder, int sortOrdinal)
		{
			if (SortOrder.Unspecified != columnSortOrder && columnSortOrder != SortOrder.Ascending && SortOrder.Descending != columnSortOrder)
			{
				throw SQL.InvalidSortOrder(columnSortOrder);
			}
			if (SortOrder.Unspecified == columnSortOrder != (-1 == sortOrdinal))
			{
				throw SQL.MustSpecifyBothSortOrderAndOrdinal(columnSortOrder, sortOrdinal);
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000449A0 File Offset: 0x00043DA0
		public short Adjust(short value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000449C0 File Offset: 0x00043DC0
		public int Adjust(int value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000449DC File Offset: 0x00043DDC
		public long Adjust(long value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000449F8 File Offset: 0x00043DF8
		public float Adjust(float value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00044A18 File Offset: 0x00043E18
		public double Adjust(double value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00044A34 File Offset: 0x00043E34
		public string Adjust(string value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					value = value.PadRight((int)this.MaxLength);
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = value.Remove((int)this.MaxLength, (int)((long)value.Length - this.MaxLength));
			}
			return value;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00044AE4 File Offset: 0x00043EE4
		public decimal Adjust(decimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType && SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				this.VerifyMoneyRange(new SqlMoney(value));
				return value;
			}
			return this.InternalAdjustSqlDecimal(new SqlDecimal(value)).Value;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00044B40 File Offset: 0x00043F40
		public DateTime Adjust(DateTime value)
		{
			if (SqlDbType.DateTime == this.SqlDbType || SqlDbType.SmallDateTime == this.SqlDbType)
			{
				this.VerifyDateTimeRange(value);
			}
			else
			{
				if (SqlDbType.DateTime2 == this.SqlDbType)
				{
					return new DateTime(this.InternalAdjustTimeTicks(value.Ticks));
				}
				if (SqlDbType.Date == this.SqlDbType)
				{
					return value.Date;
				}
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00044BA0 File Offset: 0x00043FA0
		public Guid Adjust(Guid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00044BC0 File Offset: 0x00043FC0
		public SqlBoolean Adjust(SqlBoolean value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00044BDC File Offset: 0x00043FDC
		public SqlByte Adjust(SqlByte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00044BFC File Offset: 0x00043FFC
		public SqlInt16 Adjust(SqlInt16 value)
		{
			if (SqlDbType.SmallInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00044C1C File Offset: 0x0004401C
		public SqlInt32 Adjust(SqlInt32 value)
		{
			if (SqlDbType.Int != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00044C38 File Offset: 0x00044038
		public SqlInt64 Adjust(SqlInt64 value)
		{
			if (this.SqlDbType != SqlDbType.BigInt)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00044C54 File Offset: 0x00044054
		public SqlSingle Adjust(SqlSingle value)
		{
			if (SqlDbType.Real != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00044C74 File Offset: 0x00044074
		public SqlDouble Adjust(SqlDouble value)
		{
			if (SqlDbType.Float != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00044C90 File Offset: 0x00044090
		public SqlMoney Adjust(SqlMoney value)
		{
			if (SqlDbType.Money != this.SqlDbType && SqlDbType.SmallMoney != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyMoneyRange(value);
			}
			return value;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00044CC8 File Offset: 0x000440C8
		public SqlDateTime Adjust(SqlDateTime value)
		{
			if (SqlDbType.DateTime != this.SqlDbType && SqlDbType.SmallDateTime != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (!value.IsNull)
			{
				this.VerifyDateTimeRange(value.Value);
			}
			return value;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00044D04 File Offset: 0x00044104
		public SqlDecimal Adjust(SqlDecimal value)
		{
			if (SqlDbType.Decimal != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return this.InternalAdjustSqlDecimal(value);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00044D28 File Offset: 0x00044128
		public SqlString Adjust(SqlString value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Value.Length < this.MaxLength)
				{
					return new SqlString(value.Value.PadRight((int)this.MaxLength));
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value = new SqlString(value.Value.Remove((int)this.MaxLength, (int)((long)value.Value.Length - this.MaxLength)));
			}
			return value;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00044E0C File Offset: 0x0004420C
		public SqlBinary Adjust(SqlBinary value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (!value.IsNull && (long)value.Length < this.MaxLength)
				{
					byte[] value2 = value.Value;
					byte[] array = new byte[this.MaxLength];
					Array.Copy(value2, array, value2.Length);
					Array.Clear(array, value2.Length, array.Length - value2.Length);
					return new SqlBinary(array);
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value.IsNull)
			{
				return value;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				byte[] value3 = value.Value;
				byte[] array2 = new byte[this.MaxLength];
				Array.Copy(value3, array2, (int)this.MaxLength);
				value = new SqlBinary(array2);
			}
			return value;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00044EE8 File Offset: 0x000442E8
		public SqlGuid Adjust(SqlGuid value)
		{
			if (SqlDbType.UniqueIdentifier != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00044F08 File Offset: 0x00044308
		public SqlChars Adjust(SqlChars value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					long length = value.Length;
					if (length < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							char[] array = new char[(int)this.MaxLength];
							Array.Copy(value.Buffer, array, (int)length);
							value = new SqlChars(array);
						}
						char[] buffer = value.Buffer;
						for (long num = length; num < this.MaxLength; num += 1L)
						{
							buffer[(int)(checked((IntPtr)num))] = ' ';
						}
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00045010 File Offset: 0x00044410
		public SqlBytes Adjust(SqlBytes value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && !value.IsNull)
				{
					int num = (int)value.Length;
					if ((long)num < this.MaxLength)
					{
						if (value.MaxLength < this.MaxLength)
						{
							byte[] array = new byte[this.MaxLength];
							Array.Copy(value.Buffer, array, num);
							value = new SqlBytes(array);
						}
						byte[] buffer = value.Buffer;
						Array.Clear(buffer, num, buffer.Length - num);
						value.SetLength(this.MaxLength);
						return value;
					}
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null || value.IsNull)
			{
				return value;
			}
			if (value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				value.SetLength(this.MaxLength);
			}
			return value;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000450F4 File Offset: 0x000444F4
		public SqlXml Adjust(SqlXml value)
		{
			if (SqlDbType.Xml != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00045114 File Offset: 0x00044514
		public TimeSpan Adjust(TimeSpan value)
		{
			if (SqlDbType.Time != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			this.VerifyTimeRange(value);
			return new TimeSpan(this.InternalAdjustTimeTicks(value.Ticks));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0004514C File Offset: 0x0004454C
		public DateTimeOffset Adjust(DateTimeOffset value)
		{
			if (SqlDbType.DateTimeOffset != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return new DateTimeOffset(this.InternalAdjustTimeTicks(value.Ticks), value.Offset);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00045184 File Offset: 0x00044584
		public object Adjust(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					return this.Adjust((byte[])value);
				}
				if (type == typeof(char[]))
				{
					return this.Adjust((char[])value);
				}
				if (type == typeof(Guid))
				{
					return this.Adjust((Guid)value);
				}
				if (type == typeof(object))
				{
					throw ADP.InvalidDataType(TypeCode.UInt64);
				}
				if (type == typeof(SqlBinary))
				{
					return this.Adjust((SqlBinary)value);
				}
				if (type == typeof(SqlBoolean))
				{
					return this.Adjust((SqlBoolean)value);
				}
				if (type == typeof(SqlByte))
				{
					return this.Adjust((SqlByte)value);
				}
				if (type == typeof(SqlDateTime))
				{
					return this.Adjust((SqlDateTime)value);
				}
				if (type == typeof(SqlDouble))
				{
					return this.Adjust((SqlDouble)value);
				}
				if (type == typeof(SqlGuid))
				{
					return this.Adjust((SqlGuid)value);
				}
				if (type == typeof(SqlInt16))
				{
					return this.Adjust((SqlInt16)value);
				}
				if (type == typeof(SqlInt32))
				{
					return this.Adjust((SqlInt32)value);
				}
				if (type == typeof(SqlInt64))
				{
					return this.Adjust((SqlInt64)value);
				}
				if (type == typeof(SqlMoney))
				{
					return this.Adjust((SqlMoney)value);
				}
				if (type == typeof(SqlDecimal))
				{
					return this.Adjust((SqlDecimal)value);
				}
				if (type == typeof(SqlSingle))
				{
					return this.Adjust((SqlSingle)value);
				}
				if (type == typeof(SqlString))
				{
					return this.Adjust((SqlString)value);
				}
				if (type == typeof(SqlChars))
				{
					return this.Adjust((SqlChars)value);
				}
				if (type == typeof(SqlBytes))
				{
					return this.Adjust((SqlBytes)value);
				}
				if (type == typeof(SqlXml))
				{
					return this.Adjust((SqlXml)value);
				}
				if (type == typeof(TimeSpan))
				{
					return this.Adjust((TimeSpan)value);
				}
				if (type == typeof(DateTimeOffset))
				{
					return this.Adjust((DateTimeOffset)value);
				}
				throw ADP.UnknownDataType(type);
			case TypeCode.DBNull:
				return value;
			case TypeCode.Boolean:
				return this.Adjust((bool)value);
			case TypeCode.Char:
				return this.Adjust((char)value);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return this.Adjust((byte)value);
			case TypeCode.Int16:
				return this.Adjust((short)value);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return this.Adjust((int)value);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return this.Adjust((long)value);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return this.Adjust((float)value);
			case TypeCode.Double:
				return this.Adjust((double)value);
			case TypeCode.Decimal:
				return this.Adjust((decimal)value);
			case TypeCode.DateTime:
				return this.Adjust((DateTime)value);
			case TypeCode.String:
				return this.Adjust((string)value);
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000456A4 File Offset: 0x00044AA4
		public static SqlMetaData InferFromValue(object value, string name)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull("value");
			}
			Type type = value.GetType();
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Empty:
				throw ADP.InvalidDataType(TypeCode.Empty);
			case TypeCode.Object:
				if (type == typeof(byte[]))
				{
					long num = (long)((byte[])value).Length;
					if (num < 1L)
					{
						num = 1L;
					}
					if (8000L < num)
					{
						num = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num);
				}
				if (type == typeof(char[]))
				{
					long num2 = (long)((char[])value).Length;
					if (num2 < 1L)
					{
						num2 = 1L;
					}
					if (4000L < num2)
					{
						num2 = SqlMetaData.Max;
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, num2);
				}
				if (type == typeof(Guid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(object))
				{
					return new SqlMetaData(name, SqlDbType.Variant);
				}
				if (type == typeof(SqlBinary))
				{
					SqlBinary sqlBinary = (SqlBinary)value;
					long num3;
					if (!sqlBinary.IsNull)
					{
						num3 = (long)sqlBinary.Length;
						if (num3 < 1L)
						{
							num3 = 1L;
						}
						if (8000L < num3)
						{
							num3 = SqlMetaData.Max;
						}
					}
					else
					{
						num3 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
					}
					return new SqlMetaData(name, SqlDbType.VarBinary, num3);
				}
				if (type == typeof(SqlBoolean))
				{
					return new SqlMetaData(name, SqlDbType.Bit);
				}
				if (type == typeof(SqlByte))
				{
					return new SqlMetaData(name, SqlDbType.TinyInt);
				}
				if (type == typeof(SqlDateTime))
				{
					return new SqlMetaData(name, SqlDbType.DateTime);
				}
				if (type == typeof(SqlDouble))
				{
					return new SqlMetaData(name, SqlDbType.Float);
				}
				if (type == typeof(SqlGuid))
				{
					return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
				}
				if (type == typeof(SqlInt16))
				{
					return new SqlMetaData(name, SqlDbType.SmallInt);
				}
				if (type == typeof(SqlInt32))
				{
					return new SqlMetaData(name, SqlDbType.Int);
				}
				if (type == typeof(SqlInt64))
				{
					return new SqlMetaData(name, SqlDbType.BigInt);
				}
				if (type == typeof(SqlMoney))
				{
					return new SqlMetaData(name, SqlDbType.Money);
				}
				if (type == typeof(SqlDecimal))
				{
					SqlDecimal sqlDecimal = (SqlDecimal)value;
					byte precision;
					byte scale;
					if (!sqlDecimal.IsNull)
					{
						precision = sqlDecimal.Precision;
						scale = sqlDecimal.Scale;
					}
					else
					{
						precision = SqlMetaData.sxm_rgDefaults[5].Precision;
						scale = SqlMetaData.sxm_rgDefaults[5].Scale;
					}
					return new SqlMetaData(name, SqlDbType.Decimal, precision, scale);
				}
				if (type == typeof(SqlSingle))
				{
					return new SqlMetaData(name, SqlDbType.Real);
				}
				if (type == typeof(SqlString))
				{
					SqlString sqlString = (SqlString)value;
					if (!sqlString.IsNull)
					{
						long num4 = (long)sqlString.Value.Length;
						if (num4 < 1L)
						{
							num4 = 1L;
						}
						if (num4 > 4000L)
						{
							num4 = SqlMetaData.Max;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num4, (long)sqlString.LCID, sqlString.SqlCompareOptions);
					}
					return new SqlMetaData(name, SqlDbType.NVarChar, SqlMetaData.sxm_rgDefaults[12].MaxLength);
				}
				else
				{
					if (type == typeof(SqlChars))
					{
						SqlChars sqlChars = (SqlChars)value;
						long num5;
						if (!sqlChars.IsNull)
						{
							num5 = sqlChars.Length;
							if (num5 < 1L)
							{
								num5 = 1L;
							}
							if (num5 > 4000L)
							{
								num5 = SqlMetaData.Max;
							}
						}
						else
						{
							num5 = SqlMetaData.sxm_rgDefaults[12].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.NVarChar, num5);
					}
					if (type == typeof(SqlBytes))
					{
						SqlBytes sqlBytes = (SqlBytes)value;
						long num6;
						if (!sqlBytes.IsNull)
						{
							num6 = sqlBytes.Length;
							if (num6 < 1L)
							{
								num6 = 1L;
							}
							else if (8000L < num6)
							{
								num6 = SqlMetaData.Max;
							}
						}
						else
						{
							num6 = SqlMetaData.sxm_rgDefaults[21].MaxLength;
						}
						return new SqlMetaData(name, SqlDbType.VarBinary, num6);
					}
					if (type == typeof(SqlXml))
					{
						return new SqlMetaData(name, SqlDbType.Xml);
					}
					if (type == typeof(TimeSpan))
					{
						return new SqlMetaData(name, SqlDbType.Time, 0, SqlMetaData.InferScaleFromTimeTicks(((TimeSpan)value).Ticks));
					}
					if (type == typeof(DateTimeOffset))
					{
						return new SqlMetaData(name, SqlDbType.DateTimeOffset, 0, SqlMetaData.InferScaleFromTimeTicks(((DateTimeOffset)value).Ticks));
					}
					throw ADP.UnknownDataType(type);
				}
				break;
			case TypeCode.DBNull:
				throw ADP.InvalidDataType(TypeCode.DBNull);
			case TypeCode.Boolean:
				return new SqlMetaData(name, SqlDbType.Bit);
			case TypeCode.Char:
				return new SqlMetaData(name, SqlDbType.NVarChar, 1L);
			case TypeCode.SByte:
				throw ADP.InvalidDataType(TypeCode.SByte);
			case TypeCode.Byte:
				return new SqlMetaData(name, SqlDbType.TinyInt);
			case TypeCode.Int16:
				return new SqlMetaData(name, SqlDbType.SmallInt);
			case TypeCode.UInt16:
				throw ADP.InvalidDataType(TypeCode.UInt16);
			case TypeCode.Int32:
				return new SqlMetaData(name, SqlDbType.Int);
			case TypeCode.UInt32:
				throw ADP.InvalidDataType(TypeCode.UInt32);
			case TypeCode.Int64:
				return new SqlMetaData(name, SqlDbType.BigInt);
			case TypeCode.UInt64:
				throw ADP.InvalidDataType(TypeCode.UInt64);
			case TypeCode.Single:
				return new SqlMetaData(name, SqlDbType.Real);
			case TypeCode.Double:
				return new SqlMetaData(name, SqlDbType.Float);
			case TypeCode.Decimal:
			{
				SqlDecimal sqlDecimal2 = new SqlDecimal((decimal)value);
				return new SqlMetaData(name, SqlDbType.Decimal, sqlDecimal2.Precision, sqlDecimal2.Scale);
			}
			case TypeCode.DateTime:
				return new SqlMetaData(name, SqlDbType.DateTime);
			case TypeCode.String:
			{
				long num7 = (long)((string)value).Length;
				if (num7 < 1L)
				{
					num7 = 1L;
				}
				if (4000L < num7)
				{
					num7 = SqlMetaData.Max;
				}
				return new SqlMetaData(name, SqlDbType.NVarChar, num7);
			}
			}
			throw ADP.UnknownDataTypeCode(type, Type.GetTypeCode(type));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00045CEC File Offset: 0x000450EC
		public bool Adjust(bool value)
		{
			if (SqlDbType.Bit != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00045D08 File Offset: 0x00045108
		public byte Adjust(byte value)
		{
			if (SqlDbType.TinyInt != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00045D28 File Offset: 0x00045128
		public byte[] Adjust(byte[] value)
		{
			if (SqlDbType.Binary == this.SqlDbType || SqlDbType.Timestamp == this.SqlDbType)
			{
				if (value != null && (long)value.Length < this.MaxLength)
				{
					byte[] array = new byte[this.MaxLength];
					Array.Copy(value, array, value.Length);
					Array.Clear(array, value.Length, array.Length - value.Length);
					return array;
				}
			}
			else if (SqlDbType.VarBinary != this.SqlDbType && SqlDbType.Image != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				byte[] array2 = new byte[this.MaxLength];
				Array.Copy(value, array2, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00045DD8 File Offset: 0x000451D8
		public char Adjust(char value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (1L != this.MaxLength)
				{
					SqlMetaData.ThrowInvalidType();
				}
			}
			else if (1L > this.MaxLength || (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType))
			{
				SqlMetaData.ThrowInvalidType();
			}
			return value;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00045E44 File Offset: 0x00045244
		public char[] Adjust(char[] value)
		{
			if (SqlDbType.Char == this.SqlDbType || SqlDbType.NChar == this.SqlDbType)
			{
				if (value != null)
				{
					long num = (long)value.Length;
					if (num < this.MaxLength)
					{
						char[] array = new char[(int)this.MaxLength];
						Array.Copy(value, array, (int)num);
						for (long num2 = num; num2 < (long)array.Length; num2 += 1L)
						{
							array[(int)(checked((IntPtr)num2))] = ' ';
						}
						return array;
					}
				}
			}
			else if (SqlDbType.VarChar != this.SqlDbType && SqlDbType.NVarChar != this.SqlDbType && SqlDbType.Text != this.SqlDbType && SqlDbType.NText != this.SqlDbType)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (value == null)
			{
				return null;
			}
			if ((long)value.Length > this.MaxLength && SqlMetaData.Max != this.MaxLength)
			{
				char[] array2 = new char[this.MaxLength];
				Array.Copy(value, array2, (int)this.MaxLength);
				value = array2;
			}
			return value;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00045F10 File Offset: 0x00045310
		internal static SqlMetaData GetPartialLengthMetaData(SqlMetaData md)
		{
			if (md.IsPartialLength)
			{
				return md;
			}
			if (md.SqlDbType == SqlDbType.Xml)
			{
				SqlMetaData.ThrowInvalidType();
			}
			if (md.SqlDbType == SqlDbType.NVarChar || md.SqlDbType == SqlDbType.VarChar || md.SqlDbType == SqlDbType.VarBinary)
			{
				return new SqlMetaData(md.Name, md.SqlDbType, SqlMetaData.Max, 0, 0, md.LocaleId, md.CompareOptions, null, null, null, true, md.Type);
			}
			return md;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00045F88 File Offset: 0x00045388
		private static void ThrowInvalidType()
		{
			throw ADP.InvalidMetaDataValue();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00045F9C File Offset: 0x0004539C
		private void VerifyDateTimeRange(DateTime value)
		{
			if (SqlDbType.SmallDateTime == this.SqlDbType && (SqlMetaData.x_dtSmallMax < value || SqlMetaData.x_dtSmallMin > value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00045FD4 File Offset: 0x000453D4
		private void VerifyMoneyRange(SqlMoney value)
		{
			if (SqlDbType.SmallMoney == this.SqlDbType && ((SqlMetaData.x_smSmallMax < value).Value || (SqlMetaData.x_smSmallMin > value).Value))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0004601C File Offset: 0x0004541C
		private SqlDecimal InternalAdjustSqlDecimal(SqlDecimal value)
		{
			if (!value.IsNull && (value.Precision != this.Precision || value.Scale != this.Scale))
			{
				if (value.Scale != this.Scale)
				{
					value = SqlDecimal.AdjustScale(value, (int)(this.Scale - value.Scale), false);
				}
				return SqlDecimal.ConvertToPrecScale(value, (int)this.Precision, (int)this.Scale);
			}
			return value;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0004608C File Offset: 0x0004548C
		private void VerifyTimeRange(TimeSpan value)
		{
			if (SqlDbType.Time == this.SqlDbType && (SqlMetaData.x_timeMin > value || value > SqlMetaData.x_timeMax))
			{
				SqlMetaData.ThrowInvalidType();
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000460C4 File Offset: 0x000454C4
		private long InternalAdjustTimeTicks(long ticks)
		{
			return ticks / SqlMetaData.__unitTicksFromScale[(int)this.Scale] * SqlMetaData.__unitTicksFromScale[(int)this.Scale];
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000460EC File Offset: 0x000454EC
		private static byte InferScaleFromTimeTicks(long ticks)
		{
			for (byte b = 0; b < 7; b += 1)
			{
				if (ticks / SqlMetaData.__unitTicksFromScale[(int)b] * SqlMetaData.__unitTicksFromScale[(int)b] == ticks)
				{
					return b;
				}
			}
			return 7;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00046120 File Offset: 0x00045520
		private void SetDefaultsForType(SqlDbType dbType)
		{
			if (SqlDbType.BigInt <= dbType && SqlDbType.DateTimeOffset >= dbType)
			{
				SqlMetaData sqlMetaData = SqlMetaData.sxm_rgDefaults[(int)dbType];
				this.m_sqlDbType = dbType;
				this.m_lMaxLength = sqlMetaData.MaxLength;
				this.m_bPrecision = sqlMetaData.Precision;
				this.m_bScale = sqlMetaData.Scale;
				this.m_lLocale = sqlMetaData.LocaleId;
				this.m_eCompareOptions = sqlMetaData.CompareOptions;
			}
		}

		// Token: 0x040001B5 RID: 437
		private string m_strName;

		// Token: 0x040001B6 RID: 438
		private long m_lMaxLength;

		// Token: 0x040001B7 RID: 439
		private SqlDbType m_sqlDbType;

		// Token: 0x040001B8 RID: 440
		private byte m_bPrecision;

		// Token: 0x040001B9 RID: 441
		private byte m_bScale;

		// Token: 0x040001BA RID: 442
		private long m_lLocale;

		// Token: 0x040001BB RID: 443
		private SqlCompareOptions m_eCompareOptions;

		// Token: 0x040001BC RID: 444
		private string m_XmlSchemaCollectionDatabase;

		// Token: 0x040001BD RID: 445
		private string m_XmlSchemaCollectionOwningSchema;

		// Token: 0x040001BE RID: 446
		private string m_XmlSchemaCollectionName;

		// Token: 0x040001BF RID: 447
		private string m_serverTypeName;

		// Token: 0x040001C0 RID: 448
		private bool m_bPartialLength;

		// Token: 0x040001C1 RID: 449
		private Type m_udttype;

		// Token: 0x040001C2 RID: 450
		private bool m_useServerDefault;

		// Token: 0x040001C3 RID: 451
		private bool m_isUniqueKey;

		// Token: 0x040001C4 RID: 452
		private SortOrder m_columnSortOrder;

		// Token: 0x040001C5 RID: 453
		private int m_sortOrdinal;

		// Token: 0x040001C6 RID: 454
		private const long x_lMax = -1L;

		// Token: 0x040001C7 RID: 455
		private const long x_lServerMaxUnicode = 4000L;

		// Token: 0x040001C8 RID: 456
		private const long x_lServerMaxANSI = 8000L;

		// Token: 0x040001C9 RID: 457
		private const long x_lServerMaxBinary = 8000L;

		// Token: 0x040001CA RID: 458
		private const bool x_defaultUseServerDefault = false;

		// Token: 0x040001CB RID: 459
		private const bool x_defaultIsUniqueKey = false;

		// Token: 0x040001CC RID: 460
		private const SortOrder x_defaultColumnSortOrder = SortOrder.Unspecified;

		// Token: 0x040001CD RID: 461
		private const int x_defaultSortOrdinal = -1;

		// Token: 0x040001CE RID: 462
		private const SqlCompareOptions x_eDefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x040001CF RID: 463
		private static byte[] __maxLenFromPrecision = new byte[]
		{
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			9,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			13,
			17,
			17,
			17,
			17,
			17,
			17,
			17,
			17,
			17,
			17
		};

		// Token: 0x040001D0 RID: 464
		private const byte MaxTimeScale = 7;

		// Token: 0x040001D1 RID: 465
		private static byte[] __maxVarTimeLenOffsetFromScale = new byte[]
		{
			2,
			2,
			2,
			1,
			1,
			0,
			0,
			0
		};

		// Token: 0x040001D2 RID: 466
		private static readonly DateTime x_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x040001D3 RID: 467
		private static readonly DateTime x_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x040001D4 RID: 468
		private static readonly SqlMoney x_smSmallMax = new SqlMoney(214748.3647m);

		// Token: 0x040001D5 RID: 469
		private static readonly SqlMoney x_smSmallMin = new SqlMoney(-214748.3648m);

		// Token: 0x040001D6 RID: 470
		private static readonly TimeSpan x_timeMin = TimeSpan.Zero;

		// Token: 0x040001D7 RID: 471
		private static readonly TimeSpan x_timeMax = new TimeSpan(863999999999L);

		// Token: 0x040001D8 RID: 472
		private static readonly long[] __unitTicksFromScale = new long[]
		{
			10000000L,
			1000000L,
			100000L,
			10000L,
			1000L,
			100L,
			10L,
			1L
		};

		// Token: 0x040001D9 RID: 473
		private static DbType[] sxm_rgSqlDbTypeToDbType = new DbType[]
		{
			DbType.Int64,
			DbType.Binary,
			DbType.Boolean,
			DbType.AnsiString,
			DbType.DateTime,
			DbType.Decimal,
			DbType.Double,
			DbType.Binary,
			DbType.Int32,
			DbType.Currency,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Single,
			DbType.Guid,
			DbType.DateTime,
			DbType.Int16,
			DbType.Currency,
			DbType.AnsiString,
			DbType.Binary,
			DbType.Byte,
			DbType.Binary,
			DbType.AnsiString,
			DbType.Object,
			DbType.Object,
			DbType.Xml,
			DbType.String,
			DbType.String,
			DbType.String,
			DbType.Object,
			DbType.Object,
			DbType.Date,
			DbType.Time,
			DbType.DateTime2,
			DbType.DateTimeOffset
		};

		// Token: 0x040001DA RID: 474
		internal static SqlMetaData[] sxm_rgDefaults = new SqlMetaData[]
		{
			new SqlMetaData("bigint", SqlDbType.BigInt, 8L, 19, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("binary", SqlDbType.Binary, 1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("bit", SqlDbType.Bit, 1L, 1, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("char", SqlDbType.Char, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("datetime", SqlDbType.DateTime, 8L, 23, 3, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("decimal", SqlDbType.Decimal, 9L, 18, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("float", SqlDbType.Float, 8L, 53, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("image", SqlDbType.Image, -1L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("int", SqlDbType.Int, 4L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("money", SqlDbType.Money, 8L, 19, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nchar", SqlDbType.NChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("ntext", SqlDbType.NText, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("real", SqlDbType.Real, 4L, 24, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("uniqueidentifier", SqlDbType.UniqueIdentifier, 16L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smalldatetime", SqlDbType.SmallDateTime, 4L, 16, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallint", SqlDbType.SmallInt, 2L, 5, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("smallmoney", SqlDbType.SmallMoney, 4L, 10, 4, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("text", SqlDbType.Text, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("timestamp", SqlDbType.Timestamp, 8L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("tinyint", SqlDbType.TinyInt, 1L, 3, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varbinary", SqlDbType.VarBinary, 8000L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("varchar", SqlDbType.VarChar, 8000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("sql_variant", SqlDbType.Variant, 8016L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("xml", SqlDbType.Xml, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, true),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, false),
			new SqlMetaData("udt", SqlDbType.Udt, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("table", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("date", SqlDbType.Date, 3L, 10, 0, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("time", SqlDbType.Time, 5L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetime2", SqlDbType.DateTime2, 8L, 0, 7, 0L, SqlCompareOptions.None, false),
			new SqlMetaData("datetimeoffset", SqlDbType.DateTimeOffset, 10L, 0, 7, 0L, SqlCompareOptions.None, false)
		};
	}
}
