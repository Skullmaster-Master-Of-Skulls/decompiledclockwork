using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003D RID: 61
	internal class SmiMetaData
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000216 RID: 534 RVA: 0x001DD558 File Offset: 0x001DC958
		internal static SmiMetaData DefaultChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultChar_NoCollation.SqlDbType, SmiMetaData.DefaultChar_NoCollation.MaxLength, SmiMetaData.DefaultChar_NoCollation.Precision, SmiMetaData.DefaultChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000217 RID: 535 RVA: 0x001DD5A8 File Offset: 0x001DC9A8
		internal static SmiMetaData DefaultNChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNChar_NoCollation.SqlDbType, SmiMetaData.DefaultNChar_NoCollation.MaxLength, SmiMetaData.DefaultNChar_NoCollation.Precision, SmiMetaData.DefaultNChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000218 RID: 536 RVA: 0x001DD5F8 File Offset: 0x001DC9F8
		internal static SmiMetaData DefaultNText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNText_NoCollation.SqlDbType, SmiMetaData.DefaultNText_NoCollation.MaxLength, SmiMetaData.DefaultNText_NoCollation.Precision, SmiMetaData.DefaultNText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000219 RID: 537 RVA: 0x001DD648 File Offset: 0x001DCA48
		internal static SmiMetaData DefaultNVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultNVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultNVarChar_NoCollation.MaxLength, SmiMetaData.DefaultNVarChar_NoCollation.Precision, SmiMetaData.DefaultNVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600021A RID: 538 RVA: 0x001DD698 File Offset: 0x001DCA98
		internal static SmiMetaData DefaultText
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultText_NoCollation.SqlDbType, SmiMetaData.DefaultText_NoCollation.MaxLength, SmiMetaData.DefaultText_NoCollation.Precision, SmiMetaData.DefaultText_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600021B RID: 539 RVA: 0x001DD6E8 File Offset: 0x001DCAE8
		internal static SmiMetaData DefaultVarChar
		{
			get
			{
				return new SmiMetaData(SmiMetaData.DefaultVarChar_NoCollation.SqlDbType, SmiMetaData.DefaultVarChar_NoCollation.MaxLength, SmiMetaData.DefaultVarChar_NoCollation.Precision, SmiMetaData.DefaultVarChar_NoCollation.Scale, (long)CultureInfo.CurrentCulture.LCID, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, null);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x001DD738 File Offset: 0x001DCB38
		[Obsolete("Not supported as of SMI v2.  Will be removed when v1 support dropped. Use ctor without columns param.")]
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, SmiMetaData[] columns) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x001DD758 File Offset: 0x001DCB58
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, false, null, null)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x001DD788 File Offset: 0x001DCB88
		internal SmiMetaData(SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool isMultiValued, IList<SmiExtendedMetaData> fieldTypes, SmiMetaDataPropertyCollection extendedProperties) : this(dbType, maxLength, precision, scale, localeId, compareOptions, userDefinedType, null, isMultiValued, fieldTypes, extendedProperties)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x001DD7B8 File Offset: 0x001DCBB8
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
				if (userDefinedType != null)
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

		// Token: 0x06000220 RID: 544 RVA: 0x001DD998 File Offset: 0x001DCD98
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000221 RID: 545 RVA: 0x001DDAE8 File Offset: 0x001DCEE8
		internal SqlCompareOptions CompareOptions
		{
			get
			{
				return this._compareOptions;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000222 RID: 546 RVA: 0x001DDB08 File Offset: 0x001DCF08
		internal long LocaleId
		{
			get
			{
				return this._localeId;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000223 RID: 547 RVA: 0x001DDB28 File Offset: 0x001DCF28
		internal long MaxLength
		{
			get
			{
				return this._maxLength;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000224 RID: 548 RVA: 0x001DDB48 File Offset: 0x001DCF48
		internal byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000225 RID: 549 RVA: 0x001DDB68 File Offset: 0x001DCF68
		internal byte Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000226 RID: 550 RVA: 0x001DDB88 File Offset: 0x001DCF88
		internal SqlDbType SqlDbType
		{
			get
			{
				return this._databaseType;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000227 RID: 551 RVA: 0x001DDBA8 File Offset: 0x001DCFA8
		internal Type Type
		{
			get
			{
				if (this._clrType == null && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, true);
				}
				return this._clrType;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000228 RID: 552 RVA: 0x001DDBE8 File Offset: 0x001DCFE8
		internal Type TypeWithoutThrowing
		{
			get
			{
				if (this._clrType == null && SqlDbType.Udt == this._databaseType && this._udtAssemblyQualifiedName != null)
				{
					this._clrType = Type.GetType(this._udtAssemblyQualifiedName, false);
				}
				return this._clrType;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000229 RID: 553 RVA: 0x001DDC28 File Offset: 0x001DD028
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600022A RID: 554 RVA: 0x001DDC68 File Offset: 0x001DD068
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600022B RID: 555 RVA: 0x001DDCB8 File Offset: 0x001DD0B8
		internal bool IsMultiValued
		{
			get
			{
				return this._isMultiValued;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600022C RID: 556 RVA: 0x001DDCD8 File Offset: 0x001DD0D8
		internal IList<SmiExtendedMetaData> FieldMetaData
		{
			get
			{
				return this._fieldMetaData;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600022D RID: 557 RVA: 0x001DDCF8 File Offset: 0x001DD0F8
		internal SmiMetaDataPropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x001DDD18 File Offset: 0x001DD118
		internal static bool IsSupportedDbType(SqlDbType dbType)
		{
			return (SqlDbType.BigInt <= dbType && SqlDbType.Xml >= dbType) || (SqlDbType.Udt <= dbType && SqlDbType.DateTimeOffset >= dbType);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x001DDD48 File Offset: 0x001DD148
		internal static SmiMetaData GetDefaultForType(SqlDbType dbType)
		{
			return SmiMetaData.__defaultValues[(int)dbType];
		}

		// Token: 0x06000230 RID: 560 RVA: 0x001DDD68 File Offset: 0x001DD168
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

		// Token: 0x06000231 RID: 561 RVA: 0x001DDDD8 File Offset: 0x001DD1D8
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

		// Token: 0x06000232 RID: 562 RVA: 0x001DDE68 File Offset: 0x001DD268
		internal string TraceString()
		{
			return this.TraceString(0);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x001DDE88 File Offset: 0x001DD288
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
				(this.Type != null) ? this.Type.ToString() : "<null>",
				this.IsMultiValued,
				text2,
				text3
			});
		}

		// Token: 0x040005A0 RID: 1440
		internal const long UnlimitedMaxLengthIndicator = -1L;

		// Token: 0x040005A1 RID: 1441
		internal const long MaxUnicodeCharacters = 4000L;

		// Token: 0x040005A2 RID: 1442
		internal const long MaxANSICharacters = 8000L;

		// Token: 0x040005A3 RID: 1443
		internal const long MaxBinaryLength = 8000L;

		// Token: 0x040005A4 RID: 1444
		internal const int MinPrecision = 1;

		// Token: 0x040005A5 RID: 1445
		internal const int MinScale = 0;

		// Token: 0x040005A6 RID: 1446
		internal const int MaxTimeScale = 7;

		// Token: 0x040005A7 RID: 1447
		internal const SqlCompareOptions DefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x040005A8 RID: 1448
		internal const long MaxNameLength = 128L;

		// Token: 0x040005A9 RID: 1449
		private SqlDbType _databaseType;

		// Token: 0x040005AA RID: 1450
		private long _maxLength;

		// Token: 0x040005AB RID: 1451
		private byte _precision;

		// Token: 0x040005AC RID: 1452
		private byte _scale;

		// Token: 0x040005AD RID: 1453
		private long _localeId;

		// Token: 0x040005AE RID: 1454
		private SqlCompareOptions _compareOptions;

		// Token: 0x040005AF RID: 1455
		private Type _clrType;

		// Token: 0x040005B0 RID: 1456
		private string _udtAssemblyQualifiedName;

		// Token: 0x040005B1 RID: 1457
		private bool _isMultiValued;

		// Token: 0x040005B2 RID: 1458
		private IList<SmiExtendedMetaData> _fieldMetaData;

		// Token: 0x040005B3 RID: 1459
		private SmiMetaDataPropertyCollection _extendedProperties;

		// Token: 0x040005B4 RID: 1460
		internal static readonly DateTime MaxSmallDateTime = new DateTime(2079, 6, 6, 23, 59, 29, 998);

		// Token: 0x040005B5 RID: 1461
		internal static readonly DateTime MinSmallDateTime = new DateTime(1899, 12, 31, 23, 59, 29, 999);

		// Token: 0x040005B6 RID: 1462
		internal static readonly SqlMoney MaxSmallMoney = new SqlMoney(214748.3647m);

		// Token: 0x040005B7 RID: 1463
		internal static readonly SqlMoney MinSmallMoney = new SqlMoney(-214748.3648m);

		// Token: 0x040005B8 RID: 1464
		private static readonly IList<SmiExtendedMetaData> __emptyFieldList = new List<SmiExtendedMetaData>().AsReadOnly();

		// Token: 0x040005B9 RID: 1465
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

		// Token: 0x040005BA RID: 1466
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

		// Token: 0x040005BB RID: 1467
		internal static readonly SmiMetaData DefaultBigInt = new SmiMetaData(SqlDbType.BigInt, 8L, 19, 0, SqlCompareOptions.None);

		// Token: 0x040005BC RID: 1468
		internal static readonly SmiMetaData DefaultBinary = new SmiMetaData(SqlDbType.Binary, 1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005BD RID: 1469
		internal static readonly SmiMetaData DefaultBit = new SmiMetaData(SqlDbType.Bit, 1L, 1, 0, SqlCompareOptions.None);

		// Token: 0x040005BE RID: 1470
		internal static readonly SmiMetaData DefaultChar_NoCollation = new SmiMetaData(SqlDbType.Char, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x040005BF RID: 1471
		internal static readonly SmiMetaData DefaultDateTime = new SmiMetaData(SqlDbType.DateTime, 8L, 23, 3, SqlCompareOptions.None);

		// Token: 0x040005C0 RID: 1472
		internal static readonly SmiMetaData DefaultDecimal = new SmiMetaData(SqlDbType.Decimal, 9L, 18, 0, SqlCompareOptions.None);

		// Token: 0x040005C1 RID: 1473
		internal static readonly SmiMetaData DefaultFloat = new SmiMetaData(SqlDbType.Float, 8L, 53, 0, SqlCompareOptions.None);

		// Token: 0x040005C2 RID: 1474
		internal static readonly SmiMetaData DefaultImage = new SmiMetaData(SqlDbType.Image, -1L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005C3 RID: 1475
		internal static readonly SmiMetaData DefaultInt = new SmiMetaData(SqlDbType.Int, 4L, 10, 0, SqlCompareOptions.None);

		// Token: 0x040005C4 RID: 1476
		internal static readonly SmiMetaData DefaultMoney = new SmiMetaData(SqlDbType.Money, 8L, 19, 4, SqlCompareOptions.None);

		// Token: 0x040005C5 RID: 1477
		internal static readonly SmiMetaData DefaultNChar_NoCollation = new SmiMetaData(SqlDbType.NChar, 1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x040005C6 RID: 1478
		internal static readonly SmiMetaData DefaultNText_NoCollation = new SmiMetaData(SqlDbType.NText, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x040005C7 RID: 1479
		internal static readonly SmiMetaData DefaultNVarChar_NoCollation = new SmiMetaData(SqlDbType.NVarChar, 4000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x040005C8 RID: 1480
		internal static readonly SmiMetaData DefaultReal = new SmiMetaData(SqlDbType.Real, 4L, 24, 0, SqlCompareOptions.None);

		// Token: 0x040005C9 RID: 1481
		internal static readonly SmiMetaData DefaultUniqueIdentifier = new SmiMetaData(SqlDbType.UniqueIdentifier, 16L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005CA RID: 1482
		internal static readonly SmiMetaData DefaultSmallDateTime = new SmiMetaData(SqlDbType.SmallDateTime, 4L, 16, 0, SqlCompareOptions.None);

		// Token: 0x040005CB RID: 1483
		internal static readonly SmiMetaData DefaultSmallInt = new SmiMetaData(SqlDbType.SmallInt, 2L, 5, 0, SqlCompareOptions.None);

		// Token: 0x040005CC RID: 1484
		internal static readonly SmiMetaData DefaultSmallMoney = new SmiMetaData(SqlDbType.SmallMoney, 4L, 10, 4, SqlCompareOptions.None);

		// Token: 0x040005CD RID: 1485
		internal static readonly SmiMetaData DefaultText_NoCollation = new SmiMetaData(SqlDbType.Text, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x040005CE RID: 1486
		internal static readonly SmiMetaData DefaultTimestamp = new SmiMetaData(SqlDbType.Timestamp, 8L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005CF RID: 1487
		internal static readonly SmiMetaData DefaultTinyInt = new SmiMetaData(SqlDbType.TinyInt, 1L, 3, 0, SqlCompareOptions.None);

		// Token: 0x040005D0 RID: 1488
		internal static readonly SmiMetaData DefaultVarBinary = new SmiMetaData(SqlDbType.VarBinary, 8000L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005D1 RID: 1489
		internal static readonly SmiMetaData DefaultVarChar_NoCollation = new SmiMetaData(SqlDbType.VarChar, 8000L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x040005D2 RID: 1490
		internal static readonly SmiMetaData DefaultVariant = new SmiMetaData(SqlDbType.Variant, 8016L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005D3 RID: 1491
		internal static readonly SmiMetaData DefaultXml = new SmiMetaData(SqlDbType.Xml, -1L, 0, 0, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth);

		// Token: 0x040005D4 RID: 1492
		internal static readonly SmiMetaData DefaultUdt_NoType = new SmiMetaData(SqlDbType.Udt, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005D5 RID: 1493
		internal static readonly SmiMetaData DefaultStructured = new SmiMetaData(SqlDbType.Structured, 0L, 0, 0, SqlCompareOptions.None);

		// Token: 0x040005D6 RID: 1494
		internal static readonly SmiMetaData DefaultDate = new SmiMetaData(SqlDbType.Date, 3L, 10, 0, SqlCompareOptions.None);

		// Token: 0x040005D7 RID: 1495
		internal static readonly SmiMetaData DefaultTime = new SmiMetaData(SqlDbType.Time, 5L, 0, 7, SqlCompareOptions.None);

		// Token: 0x040005D8 RID: 1496
		internal static readonly SmiMetaData DefaultDateTime2 = new SmiMetaData(SqlDbType.DateTime2, 8L, 0, 7, SqlCompareOptions.None);

		// Token: 0x040005D9 RID: 1497
		internal static readonly SmiMetaData DefaultDateTimeOffset = new SmiMetaData(SqlDbType.DateTimeOffset, 10L, 0, 7, SqlCompareOptions.None);

		// Token: 0x040005DA RID: 1498
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

		// Token: 0x040005DB RID: 1499
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
