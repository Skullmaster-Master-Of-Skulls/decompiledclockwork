using System;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000307 RID: 775
	internal sealed class ScalarType : SchemaType
	{
		// Token: 0x06002DE1 RID: 11745 RVA: 0x000ADA12 File Offset: 0x000ABC12
		internal ScalarType(Schema parentElement, string typeName, PrimitiveType primitiveType) : base(parentElement)
		{
			this.Name = typeName;
			this._primitiveType = primitiveType;
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000ADA2C File Offset: 0x000ABC2C
		public bool TryParse(string text, out object value)
		{
			switch (this._primitiveType.PrimitiveTypeKind)
			{
			case PrimitiveTypeKind.Binary:
				return ScalarType.TryParseBinary(text, out value);
			case PrimitiveTypeKind.Boolean:
				return ScalarType.TryParseBoolean(text, out value);
			case PrimitiveTypeKind.Byte:
				return ScalarType.TryParseByte(text, out value);
			case PrimitiveTypeKind.DateTime:
				return ScalarType.TryParseDateTime(text, out value);
			case PrimitiveTypeKind.Decimal:
				return ScalarType.TryParseDecimal(text, out value);
			case PrimitiveTypeKind.Double:
				return ScalarType.TryParseDouble(text, out value);
			case PrimitiveTypeKind.Guid:
				return ScalarType.TryParseGuid(text, out value);
			case PrimitiveTypeKind.Single:
				return ScalarType.TryParseSingle(text, out value);
			case PrimitiveTypeKind.SByte:
				return ScalarType.TryParseSByte(text, out value);
			case PrimitiveTypeKind.Int16:
				return ScalarType.TryParseInt16(text, out value);
			case PrimitiveTypeKind.Int32:
				return ScalarType.TryParseInt32(text, out value);
			case PrimitiveTypeKind.Int64:
				return ScalarType.TryParseInt64(text, out value);
			case PrimitiveTypeKind.String:
				return ScalarType.TryParseString(text, out value);
			case PrimitiveTypeKind.Time:
				return ScalarType.TryParseTime(text, out value);
			case PrimitiveTypeKind.DateTimeOffset:
				return ScalarType.TryParseDateTimeOffset(text, out value);
			default:
				throw EntityUtil.NotSupported(this._primitiveType.FullName);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000ADB11 File Offset: 0x000ABD11
		public PrimitiveTypeKind TypeKind
		{
			get
			{
				return this._primitiveType.PrimitiveTypeKind;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000ADB1E File Offset: 0x000ABD1E
		public PrimitiveType Type
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000ADB28 File Offset: 0x000ABD28
		private static bool TryParseBoolean(string text, out object value)
		{
			bool flag;
			if (!bool.TryParse(text, out flag))
			{
				value = null;
				return false;
			}
			value = flag;
			return true;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000ADB50 File Offset: 0x000ABD50
		private static bool TryParseByte(string text, out object value)
		{
			byte b;
			if (!byte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out b))
			{
				value = null;
				return false;
			}
			value = b;
			return true;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000ADB7C File Offset: 0x000ABD7C
		private static bool TryParseSByte(string text, out object value)
		{
			sbyte b;
			if (!sbyte.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out b))
			{
				value = null;
				return false;
			}
			value = b;
			return true;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000ADBA8 File Offset: 0x000ABDA8
		private static bool TryParseInt16(string text, out object value)
		{
			short num;
			if (!short.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000ADBD4 File Offset: 0x000ABDD4
		private static bool TryParseInt32(string text, out object value)
		{
			int num;
			if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000ADC00 File Offset: 0x000ABE00
		private static bool TryParseInt64(string text, out object value)
		{
			long num;
			if (!long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000ADC2C File Offset: 0x000ABE2C
		private static bool TryParseDouble(string text, out object value)
		{
			double num;
			if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000ADC5C File Offset: 0x000ABE5C
		private static bool TryParseDecimal(string text, out object value)
		{
			decimal num;
			if (!decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000ADC8C File Offset: 0x000ABE8C
		private static bool TryParseDateTime(string text, out object value)
		{
			DateTime dateTime;
			if (!DateTime.TryParseExact(text, "yyyy-MM-dd HH\\:mm\\:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				value = null;
				return false;
			}
			value = dateTime;
			return true;
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000ADCC0 File Offset: 0x000ABEC0
		private static bool TryParseTime(string text, out object value)
		{
			DateTime dateTime;
			if (!DateTime.TryParseExact(text, "HH\\:mm\\:ss.fffffffZ", CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				value = null;
				return false;
			}
			value = new TimeSpan(dateTime.Ticks);
			return true;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000ADCFC File Offset: 0x000ABEFC
		private static bool TryParseDateTimeOffset(string text, out object value)
		{
			DateTimeOffset dateTimeOffset;
			if (!DateTimeOffset.TryParse(text, out dateTimeOffset))
			{
				value = null;
				return false;
			}
			value = dateTimeOffset;
			return true;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000ADD21 File Offset: 0x000ABF21
		private static bool TryParseGuid(string text, out object value)
		{
			if (!ScalarType._GuidValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			value = new Guid(text);
			return true;
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000ADD43 File Offset: 0x000ABF43
		private static bool TryParseString(string text, out object value)
		{
			value = text;
			return true;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x000ADD4C File Offset: 0x000ABF4C
		private static bool TryParseBinary(string text, out object value)
		{
			if (!ScalarType._BinaryValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			string text2 = text.Substring(2);
			value = ScalarType.ConvertToByteArray(text2);
			return true;
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000ADD7C File Offset: 0x000ABF7C
		internal static byte[] ConvertToByteArray(string text)
		{
			int num = 2;
			int num2 = text.Length / 2;
			if (text.Length % 2 == 1)
			{
				num = 1;
				num2++;
			}
			byte[] array = new byte[num2];
			int i = 0;
			int num3 = 0;
			while (i < text.Length)
			{
				array[num3] = byte.Parse(text.Substring(i, num), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				i += num;
				num = 2;
				num3++;
			}
			return array;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000ADDE4 File Offset: 0x000ABFE4
		private static bool TryParseSingle(string text, out object value)
		{
			float num;
			if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
			{
				value = null;
				return false;
			}
			value = num;
			return true;
		}

		// Token: 0x040013F9 RID: 5113
		internal const string DateTimeFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffZ";

		// Token: 0x040013FA RID: 5114
		internal const string TimeFormat = "HH\\:mm\\:ss.fffffffZ";

		// Token: 0x040013FB RID: 5115
		internal const string DateTimeOffsetFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffffffz";

		// Token: 0x040013FC RID: 5116
		private static readonly Regex _BinaryValueValidator = new Regex("^0[xX][0-9a-fA-F]+$", RegexOptions.Compiled);

		// Token: 0x040013FD RID: 5117
		private static readonly Regex _GuidValueValidator = new Regex("[0-9a-fA-F]{8,8}(-[0-9a-fA-F]{4,4}){3,3}-[0-9a-fA-F]{12,12}", RegexOptions.Compiled);

		// Token: 0x040013FE RID: 5118
		private PrimitiveType _primitiveType;
	}
}
