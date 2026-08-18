using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000386 RID: 902
	internal sealed class ScalarType : SchemaType
	{
		// Token: 0x060020A7 RID: 8359 RVA: 0x0009A1A6 File Offset: 0x000983A6
		internal ScalarType(Schema parentElement, string typeName, PrimitiveType primitiveType) : base(parentElement)
		{
			this.Name = typeName;
			this._primitiveType = primitiveType;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0009A1C0 File Offset: 0x000983C0
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
				throw new NotSupportedException(this._primitiveType.FullName);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0009A2A5 File Offset: 0x000984A5
		public PrimitiveTypeKind TypeKind
		{
			get
			{
				return this._primitiveType.PrimitiveTypeKind;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0009A2B2 File Offset: 0x000984B2
		public PrimitiveType Type
		{
			get
			{
				return this._primitiveType;
			}
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0009A2BC File Offset: 0x000984BC
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

		// Token: 0x060020AC RID: 8364 RVA: 0x0009A2E4 File Offset: 0x000984E4
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

		// Token: 0x060020AD RID: 8365 RVA: 0x0009A310 File Offset: 0x00098510
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

		// Token: 0x060020AE RID: 8366 RVA: 0x0009A33C File Offset: 0x0009853C
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

		// Token: 0x060020AF RID: 8367 RVA: 0x0009A368 File Offset: 0x00098568
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

		// Token: 0x060020B0 RID: 8368 RVA: 0x0009A394 File Offset: 0x00098594
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

		// Token: 0x060020B1 RID: 8369 RVA: 0x0009A3C0 File Offset: 0x000985C0
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

		// Token: 0x060020B2 RID: 8370 RVA: 0x0009A3F0 File Offset: 0x000985F0
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

		// Token: 0x060020B3 RID: 8371 RVA: 0x0009A420 File Offset: 0x00098620
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

		// Token: 0x060020B4 RID: 8372 RVA: 0x0009A454 File Offset: 0x00098654
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

		// Token: 0x060020B5 RID: 8373 RVA: 0x0009A490 File Offset: 0x00098690
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

		// Token: 0x060020B6 RID: 8374 RVA: 0x0009A4B5 File Offset: 0x000986B5
		private static bool TryParseGuid(string text, out object value)
		{
			if (!ScalarType._guidValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			value = new Guid(text);
			return true;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0009A4D7 File Offset: 0x000986D7
		private static bool TryParseString(string text, out object value)
		{
			value = text;
			return true;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0009A4E0 File Offset: 0x000986E0
		private static bool TryParseBinary(string text, out object value)
		{
			if (!ScalarType._binaryValueValidator.IsMatch(text))
			{
				value = null;
				return false;
			}
			string text2 = text.Substring(2);
			value = ScalarType.ConvertToByteArray(text2);
			return true;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0009A510 File Offset: 0x00098710
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

		// Token: 0x060020BA RID: 8378 RVA: 0x0009A578 File Offset: 0x00098778
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

		// Token: 0x04000B8F RID: 2959
		internal const string DateTimeFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffZ";

		// Token: 0x04000B90 RID: 2960
		internal const string TimeFormat = "HH\\:mm\\:ss.fffffffZ";

		// Token: 0x04000B91 RID: 2961
		internal const string DateTimeOffsetFormat = "yyyy-MM-dd HH\\:mm\\:ss.fffffffz";

		// Token: 0x04000B92 RID: 2962
		private static readonly Regex _binaryValueValidator = new Regex("^0[xX][0-9a-fA-F]+$", RegexOptions.Compiled);

		// Token: 0x04000B93 RID: 2963
		private static readonly Regex _guidValueValidator = new Regex("[0-9a-fA-F]{8,8}(-[0-9a-fA-F]{4,4}){3,3}-[0-9a-fA-F]{12,12}", RegexOptions.Compiled);

		// Token: 0x04000B94 RID: 2964
		private readonly PrimitiveType _primitiveType;
	}
}
