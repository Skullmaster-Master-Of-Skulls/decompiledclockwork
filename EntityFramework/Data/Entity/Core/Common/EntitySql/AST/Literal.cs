using System;
using System.Data.Entity.Resources;
using System.Globalization;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200022B RID: 555
	internal sealed class Literal : Node
	{
		// Token: 0x06001397 RID: 5015 RVA: 0x0005086C File Offset: 0x0004EA6C
		internal Literal(string originalValue, LiteralKind kind, string query, int inputPos) : base(query, inputPos)
		{
			this._originalValue = originalValue;
			this._literalKind = kind;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00050885 File Offset: 0x0004EA85
		internal static Literal NewBooleanLiteral(bool value)
		{
			return new Literal(value);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0005088D File Offset: 0x0004EA8D
		private Literal(bool boolLiteral) : base(null, 0)
		{
			this._wasValueComputed = true;
			this._originalValue = string.Empty;
			this._computedValue = boolLiteral;
			this._type = typeof(bool);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x000508C5 File Offset: 0x0004EAC5
		internal bool IsNumber
		{
			get
			{
				return this._literalKind == LiteralKind.Number;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x000508D0 File Offset: 0x0004EAD0
		internal bool IsSignedNumber
		{
			get
			{
				return this.IsNumber && (this._originalValue[0] == '-' || this._originalValue[0] == '+');
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x000508FE File Offset: 0x0004EAFE
		internal bool IsString
		{
			get
			{
				return this._literalKind == LiteralKind.String || this._literalKind == LiteralKind.UnicodeString;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x00050914 File Offset: 0x0004EB14
		internal bool IsUnicodeString
		{
			get
			{
				return this._literalKind == LiteralKind.UnicodeString;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0005091F File Offset: 0x0004EB1F
		internal bool IsNullLiteral
		{
			get
			{
				return this._literalKind == LiteralKind.Null;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0005092B File Offset: 0x0004EB2B
		internal string OriginalValue
		{
			get
			{
				return this._originalValue;
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00050933 File Offset: 0x0004EB33
		internal void PrefixSign(string sign)
		{
			this._originalValue = sign + this._originalValue;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00050947 File Offset: 0x0004EB47
		internal object Value
		{
			get
			{
				this.ComputeValue();
				return this._computedValue;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00050955 File Offset: 0x0004EB55
		internal Type Type
		{
			get
			{
				this.ComputeValue();
				return this._type;
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00050964 File Offset: 0x0004EB64
		private void ComputeValue()
		{
			if (!this._wasValueComputed)
			{
				this._wasValueComputed = true;
				switch (this._literalKind)
				{
				case LiteralKind.Number:
					this._computedValue = Literal.ConvertNumericLiteral(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.String:
					this._computedValue = Literal.GetStringLiteralValue(this._originalValue, false);
					break;
				case LiteralKind.UnicodeString:
					this._computedValue = Literal.GetStringLiteralValue(this._originalValue, true);
					break;
				case LiteralKind.Boolean:
					this._computedValue = Literal.ConvertBooleanLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.Binary:
					this._computedValue = Literal.ConvertBinaryLiteralValue(this._originalValue);
					break;
				case LiteralKind.DateTime:
					this._computedValue = Literal.ConvertDateTimeLiteralValue(this._originalValue);
					break;
				case LiteralKind.Time:
					this._computedValue = Literal.ConvertTimeLiteralValue(this._originalValue);
					break;
				case LiteralKind.DateTimeOffset:
					this._computedValue = Literal.ConvertDateTimeOffsetLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.Guid:
					this._computedValue = Literal.ConvertGuidLiteralValue(this._originalValue);
					break;
				case LiteralKind.Null:
					this._computedValue = null;
					break;
				default:
					throw new NotSupportedException(Strings.LiteralTypeNotSupported(this._literalKind.ToString()));
				}
				this._type = (this.IsNullLiteral ? null : this._computedValue.GetType());
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00050AE4 File Offset: 0x0004ECE4
		private static object ConvertNumericLiteral(ErrorContext errCtx, string numericString)
		{
			int num = numericString.IndexOfAny(Literal._numberSuffixes);
			if (-1 != num)
			{
				string text = numericString.Substring(num).ToUpperInvariant();
				string s = numericString.Substring(0, numericString.Length - text.Length);
				string key;
				switch (key = text)
				{
				case "U":
				{
					uint num3;
					if (!uint.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num3))
					{
						string errorMessage = Strings.CannotConvertNumericLiteral(numericString, "unsigned int");
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
					return num3;
				}
				case "L":
				{
					long num4;
					if (!long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4))
					{
						string errorMessage2 = Strings.CannotConvertNumericLiteral(numericString, "long");
						throw EntitySqlException.Create(errCtx, errorMessage2, null);
					}
					return num4;
				}
				case "UL":
				case "LU":
				{
					ulong num5;
					if (!ulong.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num5))
					{
						string errorMessage3 = Strings.CannotConvertNumericLiteral(numericString, "unsigned long");
						throw EntitySqlException.Create(errCtx, errorMessage3, null);
					}
					return num5;
				}
				case "F":
				{
					float num6;
					if (!float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out num6))
					{
						string errorMessage4 = Strings.CannotConvertNumericLiteral(numericString, "float");
						throw EntitySqlException.Create(errCtx, errorMessage4, null);
					}
					return num6;
				}
				case "M":
				{
					decimal num7;
					if (!decimal.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num7))
					{
						string errorMessage5 = Strings.CannotConvertNumericLiteral(numericString, "decimal");
						throw EntitySqlException.Create(errCtx, errorMessage5, null);
					}
					return num7;
				}
				case "D":
				{
					double num8;
					if (!double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out num8))
					{
						string errorMessage6 = Strings.CannotConvertNumericLiteral(numericString, "double");
						throw EntitySqlException.Create(errCtx, errorMessage6, null);
					}
					return num8;
				}
				}
			}
			return Literal.DefaultNumericConversion(numericString, errCtx);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00050D00 File Offset: 0x0004EF00
		private static object DefaultNumericConversion(string numericString, ErrorContext errCtx)
		{
			if (-1 != numericString.IndexOfAny(Literal._floatTokens))
			{
				double num;
				if (!double.TryParse(numericString, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
				{
					string errorMessage = Strings.CannotConvertNumericLiteral(numericString, "double");
					throw EntitySqlException.Create(errCtx, errorMessage, null);
				}
				return num;
			}
			else
			{
				int num2;
				if (int.TryParse(numericString, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
				{
					return num2;
				}
				long num3;
				if (!long.TryParse(numericString, NumberStyles.Integer, CultureInfo.InvariantCulture, out num3))
				{
					string errorMessage2 = Strings.CannotConvertNumericLiteral(numericString, "long");
					throw EntitySqlException.Create(errCtx, errorMessage2, null);
				}
				return num3;
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00050D90 File Offset: 0x0004EF90
		private static bool ConvertBooleanLiteralValue(ErrorContext errCtx, string booleanLiteralValue)
		{
			bool result = false;
			if (!bool.TryParse(booleanLiteralValue, out result))
			{
				string errorMessage = Strings.InvalidLiteralFormat("Boolean", booleanLiteralValue);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			return result;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00050DC0 File Offset: 0x0004EFC0
		private static string GetStringLiteralValue(string stringLiteralValue, bool isUnicode)
		{
			int num = isUnicode ? 2 : 1;
			char c = stringLiteralValue[num - 1];
			if (c != '\'' && c != '"')
			{
				string malformedStringLiteralPayload = Strings.MalformedStringLiteralPayload;
				throw new EntitySqlException(malformedStringLiteralPayload);
			}
			int num2 = stringLiteralValue.Split(new char[]
			{
				c
			}).Length - 1;
			if (num2 % 2 != 0)
			{
				string malformedStringLiteralPayload2 = Strings.MalformedStringLiteralPayload;
				throw new EntitySqlException(malformedStringLiteralPayload2);
			}
			string text = stringLiteralValue.Substring(num, stringLiteralValue.Length - (1 + num));
			text = text.Replace(new string(c, 2), new string(c, 1));
			int num3 = text.Split(new char[]
			{
				c
			}).Length - 1;
			if (num3 != (num2 - 2) / 2)
			{
				string malformedStringLiteralPayload3 = Strings.MalformedStringLiteralPayload;
				throw new EntitySqlException(malformedStringLiteralPayload3);
			}
			return text;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00050E88 File Offset: 0x0004F088
		private static byte[] ConvertBinaryLiteralValue(string binaryLiteralValue)
		{
			if (string.IsNullOrEmpty(binaryLiteralValue))
			{
				return Literal._emptyByteArray;
			}
			int i = 0;
			int num = binaryLiteralValue.Length - 1;
			int num2 = num - i + 1;
			int num3 = num2 / 2;
			bool flag = 0 != num2 % 2;
			if (flag)
			{
				num3++;
			}
			byte[] array = new byte[num3];
			int num4 = 0;
			if (flag)
			{
				array[num4++] = (byte)Literal.HexDigitToBinaryValue(binaryLiteralValue[i++]);
			}
			while (i < num)
			{
				array[num4++] = (byte)(Literal.HexDigitToBinaryValue(binaryLiteralValue[i++]) << 4 | Literal.HexDigitToBinaryValue(binaryLiteralValue[i++]));
			}
			return array;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00050F2B File Offset: 0x0004F12B
		private static int HexDigitToBinaryValue(char hexChar)
		{
			if (hexChar >= '0' && hexChar <= '9')
			{
				return (int)(hexChar - '0');
			}
			if (hexChar >= 'A' && hexChar <= 'F')
			{
				return (int)(hexChar - 'A' + '\n');
			}
			if (hexChar >= 'a' && hexChar <= 'f')
			{
				return (int)(hexChar - 'a' + '\n');
			}
			throw new ArgumentOutOfRangeException("hexChar");
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00050F6C File Offset: 0x0004F16C
		private static DateTime ConvertDateTimeLiteralValue(string datetimeLiteralValue)
		{
			string[] datetimeParts = datetimeLiteralValue.Split(Literal._datetimeSeparators, StringSplitOptions.RemoveEmptyEntries);
			int year;
			int month;
			int day;
			Literal.GetDateParts(datetimeLiteralValue, datetimeParts, out year, out month, out day);
			int hour;
			int minute;
			int second;
			int num;
			Literal.GetTimeParts(datetimeLiteralValue, datetimeParts, 3, out hour, out minute, out second, out num);
			DateTime result = new DateTime(year, month, day, hour, minute, second, 0);
			result = result.AddTicks((long)num);
			return result;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00050FC4 File Offset: 0x0004F1C4
		private static DateTimeOffset ConvertDateTimeOffsetLiteralValue(ErrorContext errCtx, string datetimeLiteralValue)
		{
			string[] array = datetimeLiteralValue.Split(Literal._datetimeOffsetSeparators, StringSplitOptions.RemoveEmptyEntries);
			int year;
			int month;
			int day;
			Literal.GetDateParts(datetimeLiteralValue, array, out year, out month, out day);
			string[] array2 = new string[array.Length - 2];
			Array.Copy(array, array2, array.Length - 2);
			int hour;
			int minute;
			int second;
			int num;
			Literal.GetTimeParts(datetimeLiteralValue, array2, 3, out hour, out minute, out second, out num);
			int hours = int.Parse(array[array.Length - 2], NumberStyles.Integer, CultureInfo.InvariantCulture);
			int minutes = int.Parse(array[array.Length - 1], NumberStyles.Integer, CultureInfo.InvariantCulture);
			TimeSpan offset = new TimeSpan(hours, minutes, 0);
			if (datetimeLiteralValue.IndexOf('+') == -1)
			{
				offset = offset.Negate();
			}
			DateTime dateTime = new DateTime(year, month, day, hour, minute, second, 0);
			dateTime = dateTime.AddTicks((long)num);
			DateTimeOffset result;
			try
			{
				result = new DateTimeOffset(dateTime, offset);
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				string errorMessage = Strings.InvalidDateTimeOffsetLiteral(datetimeLiteralValue);
				throw EntitySqlException.Create(errCtx, errorMessage, innerException);
			}
			return result;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000510B0 File Offset: 0x0004F2B0
		private static TimeSpan ConvertTimeLiteralValue(string datetimeLiteralValue)
		{
			string[] datetimeParts = datetimeLiteralValue.Split(Literal._datetimeSeparators, StringSplitOptions.RemoveEmptyEntries);
			int hours;
			int minutes;
			int seconds;
			int num;
			Literal.GetTimeParts(datetimeLiteralValue, datetimeParts, 0, out hours, out minutes, out seconds, out num);
			TimeSpan result = new TimeSpan(hours, minutes, seconds);
			result = result.Add(new TimeSpan((long)num));
			return result;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000510F8 File Offset: 0x0004F2F8
		private static void GetTimeParts(string datetimeLiteralValue, string[] datetimeParts, int timePartStartIndex, out int hour, out int minute, out int second, out int ticks)
		{
			hour = int.Parse(datetimeParts[timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (hour > 23)
			{
				string message = Strings.InvalidHour(datetimeParts[timePartStartIndex], datetimeLiteralValue);
				throw new EntitySqlException(message);
			}
			minute = int.Parse(datetimeParts[++timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (minute > 59)
			{
				string message2 = Strings.InvalidMinute(datetimeParts[timePartStartIndex], datetimeLiteralValue);
				throw new EntitySqlException(message2);
			}
			second = 0;
			ticks = 0;
			timePartStartIndex++;
			if (datetimeParts.Length > timePartStartIndex)
			{
				second = int.Parse(datetimeParts[timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
				if (second > 59)
				{
					string message3 = Strings.InvalidSecond(datetimeParts[timePartStartIndex], datetimeLiteralValue);
					throw new EntitySqlException(message3);
				}
				timePartStartIndex++;
				if (datetimeParts.Length > timePartStartIndex)
				{
					string s = datetimeParts[timePartStartIndex].PadRight(7, '0');
					ticks = int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000511BC File Offset: 0x0004F3BC
		private static void GetDateParts(string datetimeLiteralValue, string[] datetimeParts, out int year, out int month, out int day)
		{
			year = int.Parse(datetimeParts[0], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (year < 1 || year > 9999)
			{
				string message = Strings.InvalidYear(datetimeParts[0], datetimeLiteralValue);
				throw new EntitySqlException(message);
			}
			month = int.Parse(datetimeParts[1], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (month < 1 || month > 12)
			{
				string message2 = Strings.InvalidMonth(datetimeParts[1], datetimeLiteralValue);
				throw new EntitySqlException(message2);
			}
			day = int.Parse(datetimeParts[2], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (day < 1)
			{
				string message3 = Strings.InvalidDay(datetimeParts[2], datetimeLiteralValue);
				throw new EntitySqlException(message3);
			}
			if (day > DateTime.DaysInMonth(year, month))
			{
				string message4 = Strings.InvalidDayInMonth(datetimeParts[2], datetimeParts[1], datetimeLiteralValue);
				throw new EntitySqlException(message4);
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0005126E File Offset: 0x0004F46E
		private static Guid ConvertGuidLiteralValue(string guidLiteralValue)
		{
			return new Guid(guidLiteralValue);
		}

		// Token: 0x04000605 RID: 1541
		private readonly LiteralKind _literalKind;

		// Token: 0x04000606 RID: 1542
		private string _originalValue;

		// Token: 0x04000607 RID: 1543
		private bool _wasValueComputed;

		// Token: 0x04000608 RID: 1544
		private object _computedValue;

		// Token: 0x04000609 RID: 1545
		private Type _type;

		// Token: 0x0400060A RID: 1546
		private static readonly byte[] _emptyByteArray = new byte[0];

		// Token: 0x0400060B RID: 1547
		private static readonly char[] _numberSuffixes = new char[]
		{
			'U',
			'u',
			'L',
			'l',
			'F',
			'f',
			'M',
			'm',
			'D',
			'd'
		};

		// Token: 0x0400060C RID: 1548
		private static readonly char[] _floatTokens = new char[]
		{
			'.',
			'E',
			'e'
		};

		// Token: 0x0400060D RID: 1549
		private static readonly char[] _datetimeSeparators = new char[]
		{
			' ',
			':',
			'-',
			'.'
		};

		// Token: 0x0400060E RID: 1550
		private static readonly char[] _datetimeOffsetSeparators = new char[]
		{
			' ',
			':',
			'-',
			'.',
			'+',
			'-'
		};
	}
}
