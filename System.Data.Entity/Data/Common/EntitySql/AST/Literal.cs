using System;
using System.Data.Entity;
using System.Globalization;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200035D RID: 861
	internal sealed class Literal : Node
	{
		// Token: 0x060031CE RID: 12750 RVA: 0x000C3E3B File Offset: 0x000C203B
		internal Literal(string originalValue, LiteralKind kind, string query, int inputPos) : base(query, inputPos)
		{
			this._originalValue = originalValue;
			this._literalKind = kind;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000C3E54 File Offset: 0x000C2054
		internal static Literal NewBooleanLiteral(bool value)
		{
			return new Literal(value);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000C3E5C File Offset: 0x000C205C
		private Literal(bool boolLiteral) : base(null, 0)
		{
			this._wasValueComputed = true;
			this._originalValue = string.Empty;
			this._computedValue = boolLiteral;
			this._type = typeof(bool);
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000C3E94 File Offset: 0x000C2094
		internal bool IsNumber
		{
			get
			{
				return this._literalKind == LiteralKind.Number;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000C3E9F File Offset: 0x000C209F
		internal bool IsSignedNumber
		{
			get
			{
				return this.IsNumber && (this._originalValue[0] == '-' || this._originalValue[0] == '+');
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000C3ECD File Offset: 0x000C20CD
		internal bool IsString
		{
			get
			{
				return this._literalKind == LiteralKind.String || this._literalKind == LiteralKind.UnicodeString;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000C3EE3 File Offset: 0x000C20E3
		internal bool IsUnicodeString
		{
			get
			{
				return this._literalKind == LiteralKind.UnicodeString;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000C3EEE File Offset: 0x000C20EE
		internal bool IsNullLiteral
		{
			get
			{
				return this._literalKind == LiteralKind.Null;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000C3EFA File Offset: 0x000C20FA
		internal string OriginalValue
		{
			get
			{
				return this._originalValue;
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000C3F02 File Offset: 0x000C2102
		internal void PrefixSign(string sign)
		{
			this._originalValue = sign + this._originalValue;
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000C3F16 File Offset: 0x000C2116
		internal object Value
		{
			get
			{
				this.ComputeValue();
				return this._computedValue;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x000C3F24 File Offset: 0x000C2124
		internal Type Type
		{
			get
			{
				this.ComputeValue();
				return this._type;
			}
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000C3F34 File Offset: 0x000C2134
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
					this._computedValue = Literal.ConvertBinaryLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.DateTime:
					this._computedValue = Literal.ConvertDateTimeLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.Time:
					this._computedValue = Literal.ConvertTimeLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.DateTimeOffset:
					this._computedValue = Literal.ConvertDateTimeOffsetLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.Guid:
					this._computedValue = Literal.ConvertGuidLiteralValue(base.ErrCtx, this._originalValue);
					break;
				case LiteralKind.Null:
					this._computedValue = null;
					break;
				default:
					throw EntityUtil.NotSupported(Strings.LiteralTypeNotSupported(this._literalKind.ToString()));
				}
				this._type = (this.IsNullLiteral ? null : this._computedValue.GetType());
			}
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000C40D4 File Offset: 0x000C22D4
		private static object ConvertNumericLiteral(ErrorContext errCtx, string numericString)
		{
			int num = numericString.IndexOfAny(Literal.numberSuffixes);
			if (-1 != num)
			{
				string text = numericString.Substring(num).ToUpperInvariant();
				string s = numericString.Substring(0, numericString.Length - text.Length);
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num2 <= 3238785555U)
				{
					if (num2 != 2078435802U)
					{
						if (num2 != 2129901492U)
						{
							if (num2 != 3238785555U)
							{
								goto IL_232;
							}
							if (!(text == "D"))
							{
								goto IL_232;
							}
							double num3;
							if (!double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
							{
								throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "double"));
							}
							return num3;
						}
						else if (!(text == "UL"))
						{
							goto IL_232;
						}
					}
					else if (!(text == "LU"))
					{
						goto IL_232;
					}
					ulong num4;
					if (!ulong.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num4))
					{
						throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "unsigned long"));
					}
					return num4;
				}
				else if (num2 <= 3356228888U)
				{
					if (num2 != 3272340793U)
					{
						if (num2 == 3356228888U)
						{
							if (text == "M")
							{
								decimal num5;
								if (!decimal.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num5))
								{
									throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "decimal"));
								}
								return num5;
							}
						}
					}
					else if (text == "F")
					{
						float num6;
						if (!float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out num6))
						{
							throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "float"));
						}
						return num6;
					}
				}
				else if (num2 != 3373006507U)
				{
					if (num2 == 3490449840U)
					{
						if (text == "U")
						{
							uint num7;
							if (!uint.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num7))
							{
								throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "unsigned int"));
							}
							return num7;
						}
					}
				}
				else if (text == "L")
				{
					long num8;
					if (!long.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num8))
					{
						throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "long"));
					}
					return num8;
				}
			}
			IL_232:
			return Literal.DefaultNumericConversion(numericString, errCtx);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000C431C File Offset: 0x000C251C
		private static object DefaultNumericConversion(string numericString, ErrorContext errCtx)
		{
			if (-1 != numericString.IndexOfAny(Literal.floatTokens))
			{
				double num;
				if (!double.TryParse(numericString, NumberStyles.Float, CultureInfo.InvariantCulture, out num))
				{
					throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "double"));
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
					throw EntityUtil.EntitySqlError(errCtx, Strings.CannotConvertNumericLiteral(numericString, "long"));
				}
				return num3;
			}
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000C43A4 File Offset: 0x000C25A4
		private static bool ConvertBooleanLiteralValue(ErrorContext errCtx, string booleanLiteralValue)
		{
			bool result = false;
			if (!bool.TryParse(booleanLiteralValue, out result))
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.InvalidLiteralFormat("Boolean", booleanLiteralValue));
			}
			return result;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000C43D0 File Offset: 0x000C25D0
		private static string GetStringLiteralValue(string stringLiteralValue, bool isUnicode)
		{
			int num = isUnicode ? 2 : 1;
			char c = stringLiteralValue[num - 1];
			if (c != '\'' && c != '"')
			{
				throw EntityUtil.EntitySqlError(Strings.MalformedStringLiteralPayload);
			}
			int num2 = stringLiteralValue.Split(new char[]
			{
				c
			}).Length - 1;
			if (num2 % 2 != 0)
			{
				throw EntityUtil.EntitySqlError(Strings.MalformedStringLiteralPayload);
			}
			string text = stringLiteralValue.Substring(num, stringLiteralValue.Length - (1 + num));
			text = text.Replace(new string(c, 2), new string(c, 1));
			int num3 = text.Split(new char[]
			{
				c
			}).Length - 1;
			if (num3 != (num2 - 2) / 2)
			{
				throw EntityUtil.EntitySqlError(Strings.MalformedStringLiteralPayload);
			}
			return text;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000C4484 File Offset: 0x000C2684
		private static byte[] ConvertBinaryLiteralValue(ErrorContext errCtx, string binaryLiteralValue)
		{
			if (string.IsNullOrEmpty(binaryLiteralValue))
			{
				return Literal._emptyByteArray;
			}
			int i = 0;
			int num = binaryLiteralValue.Length - 1;
			int num2 = num - i + 1;
			int num3 = num2 / 2;
			bool flag = num2 % 2 != 0;
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

		// Token: 0x060031E0 RID: 12768 RVA: 0x000C4524 File Offset: 0x000C2724
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
			throw EntityUtil.ArgumentOutOfRange("hexadecimal digit is not valid");
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000C4564 File Offset: 0x000C2764
		private static DateTime ConvertDateTimeLiteralValue(ErrorContext errCtx, string datetimeLiteralValue)
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

		// Token: 0x060031E2 RID: 12770 RVA: 0x000C45BC File Offset: 0x000C27BC
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
				throw EntityUtil.EntitySqlError(errCtx, Strings.InvalidDateTimeOffsetLiteral(datetimeLiteralValue), innerException);
			}
			return result;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000C46A4 File Offset: 0x000C28A4
		private static TimeSpan ConvertTimeLiteralValue(ErrorContext errCtx, string datetimeLiteralValue)
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

		// Token: 0x060031E4 RID: 12772 RVA: 0x000C46EC File Offset: 0x000C28EC
		private static void GetTimeParts(string datetimeLiteralValue, string[] datetimeParts, int timePartStartIndex, out int hour, out int minute, out int second, out int ticks)
		{
			hour = int.Parse(datetimeParts[timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (hour > 23)
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidHour(datetimeParts[timePartStartIndex], datetimeLiteralValue));
			}
			minute = int.Parse(datetimeParts[++timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (minute > 59)
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidMinute(datetimeParts[timePartStartIndex], datetimeLiteralValue));
			}
			second = 0;
			ticks = 0;
			timePartStartIndex++;
			if (datetimeParts.Length > timePartStartIndex)
			{
				second = int.Parse(datetimeParts[timePartStartIndex], NumberStyles.Integer, CultureInfo.InvariantCulture);
				if (second > 59)
				{
					throw EntityUtil.EntitySqlError(Strings.InvalidSecond(datetimeParts[timePartStartIndex], datetimeLiteralValue));
				}
				timePartStartIndex++;
				if (datetimeParts.Length > timePartStartIndex)
				{
					string s = datetimeParts[timePartStartIndex].PadRight(7, '0');
					ticks = int.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000C47AC File Offset: 0x000C29AC
		private static void GetDateParts(string datetimeLiteralValue, string[] datetimeParts, out int year, out int month, out int day)
		{
			year = int.Parse(datetimeParts[0], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (year < 1 || year > 9999)
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidYear(datetimeParts[0], datetimeLiteralValue));
			}
			month = int.Parse(datetimeParts[1], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (month < 1 || month > 12)
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidMonth(datetimeParts[1], datetimeLiteralValue));
			}
			day = int.Parse(datetimeParts[2], NumberStyles.Integer, CultureInfo.InvariantCulture);
			if (day < 1)
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidDay(datetimeParts[2], datetimeLiteralValue));
			}
			if (day > DateTime.DaysInMonth(year, month))
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidDayInMonth(datetimeParts[2], datetimeParts[1], datetimeLiteralValue));
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000C4856 File Offset: 0x000C2A56
		private static Guid ConvertGuidLiteralValue(ErrorContext errCtx, string guidLiteralValue)
		{
			return new Guid(guidLiteralValue);
		}

		// Token: 0x040015AF RID: 5551
		private readonly LiteralKind _literalKind;

		// Token: 0x040015B0 RID: 5552
		private string _originalValue;

		// Token: 0x040015B1 RID: 5553
		private bool _wasValueComputed;

		// Token: 0x040015B2 RID: 5554
		private object _computedValue;

		// Token: 0x040015B3 RID: 5555
		private Type _type;

		// Token: 0x040015B4 RID: 5556
		private static readonly byte[] _emptyByteArray = new byte[0];

		// Token: 0x040015B5 RID: 5557
		private static char[] numberSuffixes = new char[]
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

		// Token: 0x040015B6 RID: 5558
		private static char[] floatTokens = new char[]
		{
			'.',
			'E',
			'e'
		};

		// Token: 0x040015B7 RID: 5559
		private static readonly char[] _datetimeSeparators = new char[]
		{
			' ',
			':',
			'-',
			'.'
		};

		// Token: 0x040015B8 RID: 5560
		private static readonly char[] _dateSeparators = new char[]
		{
			'-'
		};

		// Token: 0x040015B9 RID: 5561
		private static readonly char[] _timeSeparators = new char[]
		{
			':',
			'.'
		};

		// Token: 0x040015BA RID: 5562
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
