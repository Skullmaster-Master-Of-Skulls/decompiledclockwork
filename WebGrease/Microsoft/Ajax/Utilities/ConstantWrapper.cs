using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200007F RID: 127
	public class ConstantWrapper : Expression
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00023D45 File Offset: 0x00021F45
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00023D4D File Offset: 0x00021F4D
		public bool MayHaveIssues { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00023D56 File Offset: 0x00021F56
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00023D5E File Offset: 0x00021F5E
		public object Value { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00023D67 File Offset: 0x00021F67
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x00023D6F File Offset: 0x00021F6F
		public PrimitiveType PrimitiveType { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00023D78 File Offset: 0x00021F78
		public override bool IsConstant
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00023D7B File Offset: 0x00021F7B
		public bool IsNumericLiteral
		{
			get
			{
				return this.PrimitiveType == PrimitiveType.Number;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00023D86 File Offset: 0x00021F86
		public bool IsFiniteNumericLiteral
		{
			get
			{
				return this.IsNumericLiteral && !double.IsNaN((double)this.Value) && !double.IsInfinity((double)this.Value);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00023DBC File Offset: 0x00021FBC
		public bool IsIntegerLiteral
		{
			get
			{
				bool result;
				try
				{
					result = (this.IsFiniteNumericLiteral && this.ToInteger() == (double)this.Value);
				}
				catch (InvalidCastException)
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00023E00 File Offset: 0x00022000
		public bool IsExactInteger
		{
			get
			{
				return this.IsIntegerLiteral && Math.Abs((double)this.Value) <= 9007199254740991.0;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00023E2A File Offset: 0x0002202A
		public bool IsNaN
		{
			get
			{
				return this.IsNumericLiteral && double.IsNaN((double)this.Value);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00023E46 File Offset: 0x00022046
		public bool IsInfinity
		{
			get
			{
				return this.IsNumericLiteral && double.IsInfinity((double)this.Value);
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00023E62 File Offset: 0x00022062
		public bool IsZero
		{
			get
			{
				return this.IsNumericLiteral && (double)this.Value == 0.0;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00023E84 File Offset: 0x00022084
		public bool IsBooleanLiteral
		{
			get
			{
				return this.PrimitiveType == PrimitiveType.Boolean;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00023E8F File Offset: 0x0002208F
		public bool IsStringLiteral
		{
			get
			{
				return this.PrimitiveType == PrimitiveType.String;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00023E9A File Offset: 0x0002209A
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00023EA2 File Offset: 0x000220A2
		public bool IsParameterToRegExp { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00023EAC File Offset: 0x000220AC
		public bool IsSpecialNumeric
		{
			get
			{
				bool result = false;
				if (this.IsNumericLiteral)
				{
					double d = (double)this.Value;
					result = (double.IsNaN(d) || double.IsInfinity(d));
				}
				return result;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00023EE2 File Offset: 0x000220E2
		public bool IsOtherDecimal
		{
			get
			{
				return this.PrimitiveType == PrimitiveType.Other && this.Value != null && ConstantWrapper.IsOnlyDecimalDigits(this.Value.ToString());
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00023F07 File Offset: 0x00022107
		public bool StringContainsAspNetReplacement
		{
			get
			{
				return this.IsStringLiteral && ConstantWrapper.s_aspNetSubstitution.IsMatch((string)this.Value);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00023F28 File Offset: 0x00022128
		public ConstantWrapper(object value, PrimitiveType primitiveType, Context context) : base(context)
		{
			this.PrimitiveType = primitiveType;
			this.Value = ((primitiveType == PrimitiveType.Number) ? Convert.ToDouble(value, CultureInfo.InvariantCulture) : value);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00023F58 File Offset: 0x00022158
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			ConstantWrapper constantWrapper = otherNode as ConstantWrapper;
			if (constantWrapper != null && this.PrimitiveType == constantWrapper.PrimitiveType)
			{
				switch (this.PrimitiveType)
				{
				case PrimitiveType.Null:
					return true;
				case PrimitiveType.Boolean:
					return this.ToBoolean() == constantWrapper.ToBoolean();
				case PrimitiveType.Number:
					return this.ToNumber() == constantWrapper.ToNumber();
				case PrimitiveType.String:
					return string.CompareOrdinal(this.Value.ToString(), constantWrapper.ToString()) == 0;
				case PrimitiveType.Other:
					return false;
				}
			}
			return false;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00023FDD File Offset: 0x000221DD
		public override PrimitiveType FindPrimitiveType()
		{
			return this.PrimitiveType;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00023FE5 File Offset: 0x000221E5
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00023FF1 File Offset: 0x000221F1
		private static void AddEscape(string unescapedRun, string escapedText, ref StringBuilder sb)
		{
			if (sb == null)
			{
				sb = new StringBuilder();
			}
			sb.Append(unescapedRun);
			sb.Append(escapedText);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00024010 File Offset: 0x00022210
		public static string EscapeString(string text, bool isRegularExpression, bool useW3Strict, bool useStrict)
		{
			char c = ConstantWrapper.OkayToDoubleQuote(text) ? '"' : '\'';
			StringBuilder stringBuilder = null;
			int num = 0;
			if (!string.IsNullOrEmpty(text))
			{
				int i = 0;
				while (i < text.Length)
				{
					char c2 = text[i];
					char c3 = c2;
					if (c3 <= '"')
					{
						switch (c3)
						{
						case '\b':
							ConstantWrapper.AddEscape(text.Substring(num, i - num), isRegularExpression ? "\\x08" : "\\b", ref stringBuilder);
							num = i + 1;
							break;
						case '\t':
							ConstantWrapper.AddEscape(text.Substring(num, i - num), isRegularExpression ? "\\x09" : "\\t", ref stringBuilder);
							num = i + 1;
							break;
						case '\n':
							ConstantWrapper.AddEscape(text.Substring(num, i - num), isRegularExpression ? "\\x0a" : "\\n", ref stringBuilder);
							num = i + 1;
							break;
						case '\v':
							if (!useW3Strict)
							{
								goto IL_223;
							}
							ConstantWrapper.AddEscape(text.Substring(num, i - num), isRegularExpression ? "\\x0b" : "\\v", ref stringBuilder);
							num = i + 1;
							break;
						case '\f':
							ConstantWrapper.AddEscape(text.Substring(num, i - num), isRegularExpression ? "\\x0c" : "\\f", ref stringBuilder);
							num = i + 1;
							break;
						case '\r':
							ConstantWrapper.AddEscape(text.Substring(num, i - num), isRegularExpression ? "\\x0d" : "\\r", ref stringBuilder);
							num = i + 1;
							break;
						default:
							if (c3 != '"')
							{
								goto IL_223;
							}
							goto IL_1AE;
						}
					}
					else
					{
						if (c3 == '\'')
						{
							goto IL_1AE;
						}
						if (c3 != '\\')
						{
							switch (c3)
							{
							case '\u2028':
							case '\u2029':
								ConstantWrapper.AddEscape(text.Substring(num, i - num), "\\u", ref stringBuilder);
								stringBuilder.Append("{0:x}".FormatInvariant(new object[]
								{
									(int)c2
								}));
								num = i + 1;
								break;
							default:
								goto IL_223;
							}
						}
						else
						{
							ConstantWrapper.AddEscape(text.Substring(num, i - num), "\\\\", ref stringBuilder);
							num = i + 1;
						}
					}
					IL_2CA:
					i++;
					continue;
					IL_1AE:
					if (c == c2)
					{
						ConstantWrapper.AddEscape(text.Substring(num, i - num), "\\", ref stringBuilder);
						stringBuilder.Append(c2);
						num = i + 1;
						goto IL_2CA;
					}
					goto IL_2CA;
					IL_223:
					if ((' ' <= c2 && c2 <= '~') || c2 >= ' ')
					{
						goto IL_2CA;
					}
					if (isRegularExpression || useStrict)
					{
						ConstantWrapper.AddEscape(text.Substring(num, i - num), "\\x{0:x2}".FormatInvariant(new object[]
						{
							(int)c2
						}), ref stringBuilder);
						num = i + 1;
						goto IL_2CA;
					}
					ConstantWrapper.AddEscape(text.Substring(num, i - num), "\\", ref stringBuilder);
					int num2 = (int)c2;
					if (num2 < 8)
					{
						stringBuilder.Append(num2.ToStringInvariant());
					}
					else
					{
						stringBuilder.Append((num2 / 8).ToStringInvariant());
						stringBuilder.Append((num2 % 8).ToStringInvariant());
					}
					num = i + 1;
					goto IL_2CA;
				}
			}
			string arg;
			if (stringBuilder == null || string.IsNullOrEmpty(text))
			{
				arg = (text ?? string.Empty);
			}
			else
			{
				if (num < text.Length)
				{
					stringBuilder.Append(text.Substring(num));
				}
				arg = stringBuilder.ToString();
			}
			return c + arg + c;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00024344 File Offset: 0x00022544
		private static bool OkayToDoubleQuote(string text)
		{
			int num = 0;
			int num2 = 0;
			foreach (char c in text)
			{
				if (c != '"')
				{
					if (c == '\'')
					{
						num2++;
					}
				}
				else
				{
					num++;
				}
			}
			return num <= num2;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002438C File Offset: 0x0002258C
		public double ToNumber()
		{
			switch (this.PrimitiveType)
			{
			case PrimitiveType.Null:
				return 0.0;
			case PrimitiveType.Boolean:
				return (double)(((bool)this.Value) ? 1 : 0);
			case PrimitiveType.Number:
				return (double)this.Value;
			case PrimitiveType.Other:
				throw new InvalidCastException("Cannot convert 'other' primitives to number");
			}
			double result;
			try
			{
				string text = this.Value.ToString();
				if (text == null || string.IsNullOrEmpty(text.Trim()))
				{
					result = 0.0;
				}
				else
				{
					if (this.MayHaveIssues)
					{
						throw new InvalidCastException("cross-browser conversion issues");
					}
					Match match;
					if ((match = ConstantWrapper.s_hexNumberFormat.Match(text)).Success)
					{
						if (!string.IsNullOrEmpty(match.Result("${sign}")))
						{
							throw new InvalidCastException("Cross-browser error converting signed hex string to number");
						}
						double num = 0.0;
						string text2 = match.Result("${hex}");
						int num2 = 0;
						while (num2 < text2.Length && !double.IsInfinity(num))
						{
							char c = text2[num2];
							num = num * 16.0 + (double)((c <= '9') ? (c & '\u000f') : ((c & '\u000f') + '\t'));
							num2++;
						}
						result = num;
					}
					else
					{
						result = double.Parse(text, NumberStyles.Float, CultureInfo.InvariantCulture);
					}
				}
			}
			catch (FormatException)
			{
				result = double.NaN;
			}
			catch (OverflowException)
			{
				Regex regex = new Regex("^\\s*-");
				result = (regex.IsMatch(this.Value.ToString()) ? double.NegativeInfinity : double.PositiveInfinity);
			}
			return result;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00024544 File Offset: 0x00022744
		public bool IsOkayToCombine
		{
			get
			{
				bool flag = (!this.IsStringLiteral && !this.IsNumericLiteral) || (this.IsNumericLiteral && !this.MayHaveIssues && ConstantWrapper.NumberIsOkayToCombine((double)this.Value)) || (this.IsStringLiteral && !this.MayHaveIssues);
				if (flag && this.IsStringLiteral && ConstantWrapper.s_aspNetSubstitution.IsMatch((string)this.Value))
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000245C0 File Offset: 0x000227C0
		public static bool NumberIsOkayToCombine(double numericValue)
		{
			return double.IsNaN(numericValue) || double.IsInfinity(numericValue) || (-9007199254740992.0 <= numericValue && numericValue <= 9007199254740992.0 && Math.Floor(numericValue) == numericValue);
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000245F8 File Offset: 0x000227F8
		public bool IsNotOneOrPositiveZero
		{
			get
			{
				if (this.IsNumericLiteral)
				{
					double num = (double)this.Value;
					if (num == 1.0 || (num == 0.0 && !this.IsNegativeZero))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0002463C File Offset: 0x0002283C
		public bool IsNegativeZero
		{
			get
			{
				return this.IsNumericLiteral && (double)this.Value == 0.0 && 1.0 / (double)this.Value < 0.0;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002468C File Offset: 0x0002288C
		internal double ToInteger()
		{
			double num = this.ToNumber();
			if (double.IsNaN(num))
			{
				return 0.0;
			}
			if (num == 0.0 || double.IsInfinity(num))
			{
				return num;
			}
			return (double)Math.Sign(num) * Math.Floor(Math.Abs(num));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000246DC File Offset: 0x000228DC
		internal int ToInt32()
		{
			double num = this.ToNumber();
			if (Math.Floor(num) != num || num < -2147483648.0 || 2147483647.0 < num)
			{
				throw new InvalidCastException("Not an integer in the appropriate range; cross-browser issue");
			}
			if (num == 0.0 || double.IsNaN(num) || double.IsInfinity(num))
			{
				return 0;
			}
			long num2 = Convert.ToInt64(num) % 4294967296L;
			return Convert.ToInt32((num2 >= (long)((ulong)int.MinValue)) ? (num2 - 4294967296L) : num2);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00024768 File Offset: 0x00022968
		internal uint ToUInt32()
		{
			double num = this.ToNumber();
			if (Math.Floor(num) != num || num < 0.0 || 4294967295.0 < num)
			{
				throw new InvalidCastException("Not an integer in the appropriate range; cross-browser issue");
			}
			if (num == 0.0 || double.IsNaN(num) || double.IsInfinity(num))
			{
				return 0U;
			}
			long num2 = Convert.ToInt64(num);
			return (uint)(num2 & (long)((ulong)-1));
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000247D4 File Offset: 0x000229D4
		public bool ToBoolean()
		{
			switch (this.PrimitiveType)
			{
			case PrimitiveType.Null:
				return false;
			case PrimitiveType.Boolean:
				return (bool)this.Value;
			case PrimitiveType.Number:
			{
				double num = (double)this.Value;
				return num != 0.0 && !double.IsNaN(num);
			}
			case PrimitiveType.Other:
				throw new InvalidCastException("Cannot convert 'other' primitive types to boolean");
			}
			return !string.IsNullOrEmpty(this.Value.ToString());
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00024854 File Offset: 0x00022A54
		public override string ToString()
		{
			switch (this.PrimitiveType)
			{
			case PrimitiveType.Null:
				return "null";
			case PrimitiveType.Boolean:
				if (!(bool)this.Value)
				{
					return "false";
				}
				return "true";
			case PrimitiveType.Number:
			{
				double num = (double)this.Value;
				if (num == 0.0)
				{
					return "0";
				}
				if (double.IsNaN(num))
				{
					return "NaN";
				}
				if (double.IsPositiveInfinity(num))
				{
					return "Infinity";
				}
				if (double.IsNegativeInfinity(num))
				{
					return "-Infinity";
				}
				return num.ToStringInvariant("R");
			}
			default:
				return this.Value.ToString();
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002492D File Offset: 0x00022B2D
		private static bool IsOnlyDecimalDigits(string text)
		{
			return text.IfNotNull((string s) => !s.Any((char c) => !JSScanner.IsDigit(c)));
		}

		// Token: 0x040002EC RID: 748
		private static Regex s_hexNumberFormat = new Regex("^\\s*(?<sign>[-+])?0X(?<hex>[0-9a-f]+)\\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040002ED RID: 749
		private static Regex s_aspNetSubstitution = new Regex("\\<%.*%\\>", RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
