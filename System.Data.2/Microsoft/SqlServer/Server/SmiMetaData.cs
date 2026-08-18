using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000043 RID: 67
	internal class SmiMetaData
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0003A618 File Offset: 0x00039A18
		internal static SmiMetaData DefaultChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultChar_NoCollation.SqlDbType, SmiMetaData.DefaultChar_NoCollation.MaxLength, SmiMetaData.DefaultChar_NoCollation.Precision, SmiMetaData.DefaultChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0003A660 File Offset: 0x00039A60
		internal static SmiMetaData DefaultNChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNChar_NoCollation.SqlDbType, SmiMetaData.DefaultNChar_NoCollation.MaxLength, SmiMetaData.DefaultNChar_NoCollation.Precision, SmiMetaData.DefaultNChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0003A6A8 File Offset: 0x00039AA8
		internal static SmiMetaData DefaultNText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNText_NoCollation.SqlDbType, SmiMetaData.DefaultNText_NoCollation.MaxLength, SmiMetaData.DefaultNText_NoCollation.Precision, SmiMetaData.DefaultNText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0003A6F0 File Offset: 0x00039AF0
		internal static SmiMetaData DefaultNVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultNVarChar_NoCollation.MaxLength, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0003A738 File Offset: 0x00039B38
		internal static SmiMetaData DefaultText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultText_NoCollation.SqlDbType, SmiMetaData.DefaultText_NoCollation.MaxLength, SmiMetaData.DefaultText_NoCollation.Precision, SmiMetaData.DefaultText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0003A780 File Offset: 0x00039B80
		internal static SmiMetaData DefaultVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultVarChar_NoCollation.MaxLength, SmiMetaData.DefaultVarChar_NoCollation.Precision, SmiMetaData.DefaultVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0003A7C8 File Offset: 0x00039BC8
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType)
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0003A7E8 File Offset: 0x00039BE8
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0003A80C File Offset: 0x00039C0C
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldTypes, extendedProperties)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0003A834 File Offset: 0x00039C34
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, string udtAssemblyQualifiedName, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties)
		{
			this.SetDefaultsForType(dbType);
			switch (dbType)
			{
			case SqlDbType.Binary:
			case SqlDbType.VarBinary:
				this._maxLength = maxLength;
				break;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NVarChar:
			case SqlDbType.VarChar:
				this._maxLength = maxLength;
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Decimal:
				this._precision = precision;
				this._scale = scale;
				this._maxLength = (long)((ulong)SmiMetaData.__maxLenFromPrecision[(int)(precision - 1)]);
				break;
			case SqlDbType.NText:
			case SqlDbType.Text:
				this._localeId = localeId;
				this._compareOptions = compareOptions;
				break;
			case SqlDbType.Udt:
				this._clrType = userDefinedType;
				if (null != userDefinedType)
				{
					this._maxLength = (long)SerializationHelperSql9.GetUdtMaxLength(userDefinedType);
				}
				else
				{
					this._maxLength = maxLength;
				}
				this._udtAssemblyQualifiedName = udtAssemblyQualifiedName;
				break;
			case SqlDbType.Structured:
				if (fieldTypes != null)
				{
					this._fieldMetaData = new List<SmiExtendedMetaData>(fieldTypes).AsReadOnly();
				}
				this._isMultiValued = isMultiValued;
				this._maxLength = (long)this._fieldMetaData.Count;
				break;
			case SqlDbType.Time:
				this._scale = scale;
				this._maxLength = (long)(5 - SmiMetaData.__maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTime2:
				this._scale = scale;
				this._maxLength = (long)(8 - SmiMetaData.__maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			case SqlDbType.DateTimeOffset:
				this._scale = scale;
				this._maxLength = (long)(10 - SmiMetaData.__maxVarTimeLenOffsetFromScale[(int)scale]);
				break;
			}
			if (extendedProperties != null)
			{
				extendedProperties.SetReadOnly();
				this._extendedProperties = extendedProperties;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0003AA14 File Offset: 0x00039E14
		internal bool IsValidMaxLengthForCtorGivenType(SqlDbType dbType, long maxLength)
		{
			bool result = true;
			switch (dbType)
			{
			case SqlDbType.Binary:
				result = (0L < maxLength && 8000L >= maxLength);
				break;
			case SqlDbType.Char:
				result = (0L < maxLength && 8000L >= maxLength);
				break;
			case SqlDbType.NChar:
				result = (0L < maxLength && 4000L >= maxLength);
				break;
			case SqlDbType.NVarChar:
				result = (-1L == maxLength || (0L < maxLength && 4000L >= maxLength));
				break;
			case SqlDbType.VarBinary:
				result = (-1L == maxLength || (0L < maxLength && 8000L >= maxLength));
				break;
			case SqlDbType.VarChar:
				result = (-1L == maxLength || (0L < maxLength && 8000L >= maxLength));
				break;
			}
			return result;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0003AB60 File Offset: 0x00039F60
		internal SqlCompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0003AB74 File Offset: 0x00039F74
		internal long LocaleId
		{
			get
			{
				return this._localeId;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0003AB88 File Offset: 0x00039F88
		internal long MaxLength
		{
			get
			{
				return this._maxLength;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0003AB9C File Offset: 0x00039F9C
		internal byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0003ABB0 File Offset: 0x00039FB0
		internal byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0003ABC4 File Offset: 0x00039FC4
		internal SqlDbType SqlDbType
		{
			get
			{
				return this._databaseType;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0003ABD8 File Offset: 0x00039FD8
		internal Type Type
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, true);
				}
				return this._clrType;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0003AC20 File Offset: 0x0003A020
		internal Type TypeWithoutThrowing
		{
			get
			{
				if (null == this._clrType && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, false);
				}
				return this._clrType;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0003AC68 File Offset: 0x0003A068
		internal string TypeName
		{
			get
			{
				string result;
				if (SqlDbType.Udt == this._databaseType)
				{
					result = this.Type.FullName;
				}
				else
				{
					result = SmiMetaData.__typeNameByDatabaseType[(int)this._databaseType];
				}
				return result;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0003ACA0 File Offset: 0x0003A0A0
		internal string AssemblyQualifiedName
		{
			get
			{
				string result = null;
				if (SqlDbType.Udt == this._databaseType)
				{
					if (this._udtAssemblyQualifiedName == null && this._clrType != null)
					{
						this._udtAssemblyQualifiedName = this._clrType.AssemblyQualifiedName;
					}
					result = this._udtAssemblyQualifiedName;
				}
				return result;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0003ACE8 File Offset: 0x0003A0E8
		internal bool IsMultiValued
		{
			get
			{
				return this._isMultiValued;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0003ACFC File Offset: 0x0003A0FC
		internal IList<SmiExtendedMetaData> FieldMetaData
		{
			get
			{
				return this._fieldMetaData;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0003AD10 File Offset: 0x0003A110
		internal SmiMetaDataPropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0003AD24 File Offset: 0x0003A124
		internal static bool IsSupportedDbType(SqlDbType dbType)
		{
			return (SqlDbType.BigInt <= dbType && SqlDbType.Xml >= dbType) || (SqlDbType.Udt <= dbType && SqlDbType.DateTimeOffset >= dbType);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0003AD4C File Offset: 0x0003A14C
		internal static SmiMetaData GetDefaultForType(SqlDbType dbType)
		{
			return SmiMetaData.__defaultValues[(int)dbType];
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0003AD60 File Offset: 0x0003A160
		private SmiMetaData(SqlDbType sqlDbType, long maxLength, byte precision, byte scale, SqlCompareOptions compareOptions)
		{
			this._databaseType = sqlDbType;
			this._maxLength = maxLength;
			this._precision = precision;
			this._scale = scale;
			this._compareOptions = compareOptions;
			this._localeId = 0L;
			this._clrType = null;
			this._isMultiValued = false;
			this._fieldMetaData = SmiMetaData.__emptyFieldList;
			this._extendedProperties = SmiMetaDataPropertyCollection.EmptyInstance;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0003ADC4 File Offset: 0x0003A1C4
		private void SetDefaultsForType(SqlDbType dbType)
		{
			SmiMetaData defaultForType = SmiMetaData.GetDefaultForType(dbType);
			this._databaseType = dbType;
			this._maxLength = defaultForType.MaxLength;
			this._precision = defaultForType.Precision;
			this._scale = defaultForType.Scale;
			this._localeId = defaultForType.LocaleId;
			this._compareOptions = defaultForType.CompareOptions;
			this._clrType = null;
			this._isMultiValued = defaultForType._isMultiValued;
			this._fieldMetaData = defaultForType._fieldMetaData;
			this._extendedProperties = defaultForType._extendedProperties;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0003AE48 File Offset: 0x0003A248
		internal string TraceString()
		{
			return this.TraceString(0);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0003AE5C File Offset: 0x0003A25C
		internal virtual string TraceString(int indent)
		{
			string text = new string(' ', indent);
			string text2 = string.Empty;
			if (this._fieldMetaData != null)
			{
				foreach (SmiMetaData smiMetaData in this._fieldMetaData)
				{
					text2 = string.Format(CultureInfo.InvariantCulture, "{0}{1}\n\t", new object[]
					{
						text2,
						smiMetaData.TraceString(indent + 5)
					});
				}
			}
			string text3 = string.Empty;
			if (this._extendedProperties != null)
			{
				foreach (SmiMetaDataProperty smiMetaDataProperty in this._extendedProperties.Values)
				{
					text3 = string.Format(CultureInfo.InvariantCulture, "{0}{1}                   {2}\n\t", new object[]
					{
						text3,
						text,
						smiMetaDataProperty.TraceString()
					});
				}
			}
			return string.Format(CultureInfo.InvariantCulture, "\n\t{0}            SqlDbType={1:g}\n\t{0}            MaxLength={2:d}\n\t{0}            Precision={3:d}\n\t{0}                Scale={4:d}\n\t{0}             LocaleId={5:x}\n\t{0}       CompareOptions={6:g}\n\t{0}                 Type={7}\n\t{0}          MultiValued={8}\n\t{0}               fields=\n\t{9}{0}           properties=\n\t{10}", new object[]
			{
				text,
				this.SqlDbType,
				this.MaxLength,
				this.Precision,
				this.Scale,
				this.LocaleId,
				this.CompareOptions,
				(null != this.Type) ? this.Type.ToString() : "<null>",
				this.IsMultiValued,
				text2,
				text3
			});
		}

		// Token: 0x04000112 RID: 274
		private SqlDbType _databaseType;

		// Token: 0x04000113 RID: 275
		private long _maxLength;

		// Token: 0x04000114 RID: 276
		private byte _precision;

		// Token: 0x04000115 RID: 277
		private byte _scale;

		// Token: 0x04000116 RID: 278
		private long _localeId;

		// Token: 0x04000117 RID: 279
		private SqlCompareOptions _compareOptions;

		// Token: 0x04000118 RID: 280
		private Type _clrType;

		// Token: 0x04000119 RID: 281
		private string _udtAssemblyQualifiedName;

		// Token: 0x0400011A RID: 282
		private bool _isMultiValued;

		// Token: 0x0400011B RID: 283
		private IList<SmiExtendedMetaData> _fieldMetaData;

		// Token: 0x0400011C RID: 284
		private SmiMetaDataPropertyCollection _extendedProperties;

		// Token: 0x0400011D RID: 285
		internal const long UnlimitedMaxLengthIndicator = -1L;

		// Token: 0x0400011E RID: 286
		internal const long MaxUnicodeCharacters = 4000L;

		// Token: 0x0400011F RID: 287
		internal const long MaxANSICharacters = 8000L;

		// Token: 0x04000120 RID: 288
		internal const long MaxBinaryLength = 8000L;

		// Token: 0x04000121 RID: 289
		internal const int MinPrecision = 1;

		// Token: 0x04000122 RID: 290
		internal const int MinScale = 0;

		// Token: 0x04000123 RID: 291
		internal const int MaxTimeScale = 7;

		// Token: 0x04000124 RID: 292
		internal static readonly DateTime MaxSmallDateTime = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x04000125 RID: 293
		internal static readonly DateTime MinSmallDateTime = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x04000126 RID: 294
		internal static readonly SqlMoney MaxSmallMoney = new SqlMoney(214748.3647m);

		// Token: 0x04000127 RID: 295
		internal static readonly SqlMoney MinSmallMoney = new SqlMoney(-214748.3648m);

		// Token: 0x04000128 RID: 296
		internal const SqlCompareOptions DefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04000129 RID: 297
		internal const long MaxNameLength = 128L;

		// Token: 0x0400012A RID: 298
		private static readonly IList<SmiExtendedMetaData> __emptyFieldList = new List<SmiExtendedMetaData>().AsReadOnly();

		// Token: 0x0400012B RID: 299
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

		// Token: 0x0400012C RID: 300
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

		// Token: 0x0400012D RID: 301
		internal static readonly SmiMetaData DefaultBigInt = new SmiMetaData(SqlDbType.BigInt, 8L, 19, 0, SqlCompareOptions.None);

		// Token: 0x0400012E RID: 302
		internal static readonly SmiMetaData DefaultBinary = new SmiMetaData(SqlDbType.Binary, 1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400012F RID: 303
		internal static readonly SmiMetaData DefaultBit = new SmiMetaData(SqlDbType.Bit, 1L, 1, 0, SqlCompareOptions.None);

		// Token: 0x04000130 RID: 304
		internal static readonly SmiMetaData DefaultChar_NoCollation = new SmiMetaData(SqlDbType.Char, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000131 RID: 305
		internal static readonly SmiMetaData DefaultDateTime = new SmiMetaData(SqlDbType.DateTime, 8L, 23, 3, SqlCompareOptions.None);

		// Token: 0x04000132 RID: 306
		internal static readonly SmiMetaData DefaultDecimal = new SmiMetaData(SqlDbType.Decimal, 9L, 18, 0, SqlCompareOptions.None);

		// Token: 0x04000133 RID: 307
		internal static readonly SmiMetaData DefaultFloat = new SmiMetaData(SqlDbType.Float, 8L, 53, 0, SqlCompareOptions.None);

		// Token: 0x04000134 RID: 308
		internal static readonly SmiMetaData DefaultImage = new SmiMetaData(SqlDbType.Image, -1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000135 RID: 309
		internal static readonly SmiMetaData DefaultInt = new SmiMetaData(SqlDbType.Int, 4L, 10, 0, SqlCompareOptions.None);

		// Token: 0x04000136 RID: 310
		internal static readonly SmiMetaData DefaultMoney = new SmiMetaData(SqlDbType.Money, 8L, 19, 4, SqlCompareOptions.None);

		// Token: 0x04000137 RID: 311
		internal static readonly SmiMetaData DefaultNChar_NoCollation = new SmiMetaData(SqlDbType.NChar, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000138 RID: 312
		internal static readonly SmiMetaData DefaultNText_NoCollation = new SmiMetaData(SqlDbType.NText, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000139 RID: 313
		internal static readonly SmiMetaData DefaultNVarChar_NoCollation = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x0400013A RID: 314
		internal static readonly SmiMetaData DefaultReal = new SmiMetaData(SqlDbType.Real, 4L, 24, 0, SqlCompareOptions.None);

		// Token: 0x0400013B RID: 315
		internal static readonly SmiMetaData DefaultUniqueIdentifier = new SmiMetaData(SqlDbType.UniqueIdentifier, 16L, 0, 0, SqlCompareOptions.None);

		// Token: 0x0400013C RID: 316
		internal static readonly SmiMetaData DefaultSmallDateTime = new SmiMetaData(SqlDbType.SmallDateTime, 4L, 16, 0, SqlCompareOptions.None);

		// Token: 0x0400013D RID: 317
		internal static readonly SmiMetaData DefaultSmallInt = new SmiMetaData(SqlDbType.SmallInt, 2L, 5, 0, SqlCompareOptions.None);

		// Token: 0x0400013E RID: 318
		internal static readonly SmiMetaData DefaultSmallMoney = new SmiMetaData(SqlDbType.SmallMoney, 4L, 10, 4, SqlCompareOptions.None);

		// Token: 0x0400013F RID: 319
		internal static readonly SmiMetaData DefaultText_NoCollation = new SmiMetaData(SqlDbType.Text, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000140 RID: 320
		internal static readonly SmiMetaData DefaultTimestamp = new SmiMetaData(SqlDbType.Timestamp, 8L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000141 RID: 321
		internal static readonly SmiMetaData DefaultTinyInt = new SmiMetaData(SqlDbType.TinyInt, 1L, 3, 0, SqlCompareOptions.None);

		// Token: 0x04000142 RID: 322
		internal static readonly SmiMetaData DefaultVarBinary = new SmiMetaData(SqlDbType.VarBinary, 8000L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000143 RID: 323
		internal static readonly SmiMetaData DefaultVarChar_NoCollation = new SmiMetaData(SqlDbType.VarChar, 8000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000144 RID: 324
		internal static readonly SmiMetaData DefaultVariant = new SmiMetaData(SqlDbType.Variant, 8016L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000145 RID: 325
		internal static readonly SmiMetaData DefaultXml = new SmiMetaData(SqlDbType.Xml, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x04000146 RID: 326
		internal static readonly SmiMetaData DefaultUdt_NoType = new SmiMetaData(SqlDbType.Udt, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000147 RID: 327
		internal static readonly SmiMetaData DefaultStructured = new SmiMetaData(SqlDbType.Structured, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x04000148 RID: 328
		internal static readonly SmiMetaData DefaultDate = new SmiMetaData(SqlDbType.Date, 3L, 10, 0, SqlCompareOptions.None);

		// Token: 0x04000149 RID: 329
		internal static readonly SmiMetaData DefaultTime = new SmiMetaData(SqlDbType.Time, 5L, 0, 7, SqlCompareOptions.None);

		// Token: 0x0400014A RID: 330
		internal static readonly SmiMetaData DefaultDateTime2 = new SmiMetaData(SqlDbType.DateTime2, 8L, 0, 7, SqlCompareOptions.None);

		// Token: 0x0400014B RID: 331
		internal static readonly SmiMetaData DefaultDateTimeOffset = new SmiMetaData(SqlDbType.DateTimeOffset, 10L, 0, 7, SqlCompareOptions.None);

		// Token: 0x0400014C RID: 332
		private static SmiMetaData[] __defaultValues = new SmiMetaData[]
		{
			SmiMetaData.DefaultBigInt,
			SmiMetaData.DefaultBinary,
			SmiMetaData.DefaultBit,
			SmiMetaData.DefaultChar_NoCollation,
			SmiMetaData.DefaultDateTime,
			SmiMetaData.DefaultDecimal,
			SmiMetaData.DefaultFloat,
			SmiMetaData.DefaultImage,
			SmiMetaData.DefaultInt,
			SmiMetaData.DefaultMoney,
			SmiMetaData.DefaultNChar_NoCollation,
			SmiMetaData.DefaultNText_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultReal,
			SmiMetaData.DefaultUniqueIdentifier,
			SmiMetaData.DefaultSmallDateTime,
			SmiMetaData.DefaultSmallInt,
			SmiMetaData.DefaultSmallMoney,
			SmiMetaData.DefaultText_NoCollation,
			SmiMetaData.DefaultTimestamp,
			SmiMetaData.DefaultTinyInt,
			SmiMetaData.DefaultVarBinary,
			SmiMetaData.DefaultVarChar_NoCollation,
			SmiMetaData.DefaultVariant,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultXml,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultNVarChar_NoCollation,
			SmiMetaData.DefaultUdt_NoType,
			SmiMetaData.DefaultStructured,
			SmiMetaData.DefaultDate,
			SmiMetaData.DefaultTime,
			SmiMetaData.DefaultDateTime2,
			SmiMetaData.DefaultDateTimeOffset
		};

		// Token: 0x0400014D RID: 333
		private static string[] __typeNameByDatabaseType = new string[]
		{
			"bigint",
			"binary",
			"bit",
			"char",
			"datetime",
			"decimal",
			"float",
			"image",
			"int",
			"money",
			"nchar",
			"ntext",
			"nvarchar",
			"real",
			"uniqueidentifier",
			"smalldatetime",
			"smallint",
			"smallmoney",
			"text",
			"timestamp",
			"tinyint",
			"varbinary",
			"varchar",
			"sql_variant",
			null,
			"xml",
			null,
			null,
			null,
			string.Empty,
			string.Empty,
			"date",
			"time",
			"datetime2",
			"datetimeoffset"
		};
	}
}
