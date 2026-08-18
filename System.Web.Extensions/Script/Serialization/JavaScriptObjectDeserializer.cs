using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Resources;
using System.Web.Util;

namespace System.Web.Script.Serialization
{
	// Token: 0x020000FE RID: 254
	internal class JavaScriptObjectDeserializer
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x0002EA54 File Offset: 0x0002CC54
		internal static object BasicDeserialize(string input, int depthLimit, JavaScriptSerializer serializer)
		{
			JavaScriptObjectDeserializer javaScriptObjectDeserializer = new JavaScriptObjectDeserializer(input, depthLimit, serializer);
			object result = javaScriptObjectDeserializer.DeserializeInternal(0);
			if (javaScriptObjectDeserializer._s.GetNextNonEmptyChar() != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.JSON_IllegalPrimitive, new object[]
				{
					javaScriptObjectDeserializer._s.ToString()
				}));
			}
			return result;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002EAB1 File Offset: 0x0002CCB1
		private JavaScriptObjectDeserializer(string input, int depthLimit, JavaScriptSerializer serializer)
		{
			this._s = new JavaScriptString(input);
			this._depthLimit = depthLimit;
			this._serializer = serializer;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		private object DeserializeInternal(int depth)
		{
			if (++depth > this._depthLimit)
			{
				throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_DepthLimitExceeded));
			}
			char? nextNonEmptyChar = this._s.GetNextNonEmptyChar();
			if (nextNonEmptyChar == null)
			{
				return null;
			}
			this._s.MovePrev();
			if (this.IsNextElementDateTime())
			{
				return this.DeserializeStringIntoDateTime();
			}
			if (JavaScriptObjectDeserializer.IsNextElementObject(nextNonEmptyChar))
			{
				IDictionary<string, object> dictionary = this.DeserializeDictionary(depth);
				if (dictionary.ContainsKey("__type"))
				{
					return ObjectConverter.ConvertObjectToType(dictionary, null, this._serializer);
				}
				return dictionary;
			}
			else
			{
				if (JavaScriptObjectDeserializer.IsNextElementArray(nextNonEmptyChar))
				{
					return this.DeserializeList(depth);
				}
				if (JavaScriptObjectDeserializer.IsNextElementString(nextNonEmptyChar))
				{
					return this.DeserializeString();
				}
				return this.DeserializePrimitiveObject();
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002EB88 File Offset: 0x0002CD88
		private IList DeserializeList(int depth)
		{
			IList list = new ArrayList();
			char? c = this._s.MoveNext();
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 91;
			if (!(num.GetValueOrDefault() == num2 & num != null))
			{
				throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidArrayStart));
			}
			bool flag = false;
			do
			{
				c = (c2 = this._s.GetNextNonEmptyChar());
				if (c2 == null)
				{
					goto IL_188;
				}
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 93;
				if (num.GetValueOrDefault() == num2 & num != null)
				{
					goto IL_188;
				}
				this._s.MovePrev();
				object value = this.DeserializeInternal(depth);
				list.Add(value);
				flag = false;
				c = this._s.GetNextNonEmptyChar();
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 93;
				if (num.GetValueOrDefault() == num2 & num != null)
				{
					goto IL_188;
				}
				flag = true;
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 44;
			}
			while (num.GetValueOrDefault() == num2 & num != null);
			throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidArrayExpectComma));
			IL_188:
			if (flag)
			{
				throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidArrayExtraComma));
			}
			c2 = c;
			num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
			num2 = 93;
			if (!(num.GetValueOrDefault() == num2 & num != null))
			{
				throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidArrayEnd));
			}
			return list;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002ED8C File Offset: 0x0002CF8C
		private IDictionary<string, object> DeserializeDictionary(int depth)
		{
			IDictionary<string, object> dictionary = null;
			char? c = this._s.MoveNext();
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 123;
			if (!(num.GetValueOrDefault() == num2 & num != null))
			{
				throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_ExpectedOpenBrace));
			}
			for (;;)
			{
				c = (c2 = this._s.GetNextNonEmptyChar());
				if (c2 == null)
				{
					goto IL_257;
				}
				this._s.MovePrev();
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 58;
				if (num.GetValueOrDefault() == num2 & num != null)
				{
					break;
				}
				string text = null;
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 125;
				if (!(num.GetValueOrDefault() == num2 & num != null))
				{
					text = this.DeserializeMemberName();
					c = this._s.GetNextNonEmptyChar();
					c2 = c;
					num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					num2 = 58;
					if (!(num.GetValueOrDefault() == num2 & num != null))
					{
						goto Block_8;
					}
				}
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, object>();
					if (text == null)
					{
						goto Block_10;
					}
				}
				this.ThrowIfMaxJsonDeserializerMembersExceeded(dictionary.Count);
				object value = this.DeserializeInternal(depth);
				dictionary[text] = value;
				c = this._s.GetNextNonEmptyChar();
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 125;
				if (num.GetValueOrDefault() == num2 & num != null)
				{
					goto IL_257;
				}
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 44;
				if (!(num.GetValueOrDefault() == num2 & num != null))
				{
					goto Block_14;
				}
			}
			throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidMemberName));
			Block_8:
			throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidObject));
			Block_10:
			c = this._s.GetNextNonEmptyChar();
			goto IL_257;
			Block_14:
			throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidObject));
			IL_257:
			c2 = c;
			num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
			num2 = 125;
			if (!(num.GetValueOrDefault() == num2 & num != null))
			{
				throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_InvalidObject));
			}
			return dictionary;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0002F043 File Offset: 0x0002D243
		private void ThrowIfMaxJsonDeserializerMembersExceeded(int count)
		{
			if (count >= AppSettings.MaxJsonDeserializerMembers)
			{
				throw new InvalidOperationException(SR.GetString("CollectionCountExceeded_JavaScriptObjectDeserializer", new object[]
				{
					AppSettings.MaxJsonDeserializerMembers
				}));
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002F070 File Offset: 0x0002D270
		private string DeserializeMemberName()
		{
			char? nextNonEmptyChar = this._s.GetNextNonEmptyChar();
			if (nextNonEmptyChar == null)
			{
				return null;
			}
			this._s.MovePrev();
			if (JavaScriptObjectDeserializer.IsNextElementString(nextNonEmptyChar))
			{
				return this.DeserializeString();
			}
			return this.DeserializePrimitiveToken();
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002F0B4 File Offset: 0x0002D2B4
		private object DeserializePrimitiveObject()
		{
			string text = this.DeserializePrimitiveToken();
			if (text.Equals("null"))
			{
				return null;
			}
			if (text.Equals("true"))
			{
				return true;
			}
			if (text.Equals("false"))
			{
				return false;
			}
			bool flag = text.IndexOf('.') >= 0;
			if (text.LastIndexOf("e", StringComparison.OrdinalIgnoreCase) < 0)
			{
				if (!flag)
				{
					int num;
					if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
					{
						return num;
					}
					long num2;
					if (long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
					{
						return num2;
					}
				}
				decimal num3;
				if (decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out num3))
				{
					return num3;
				}
			}
			double num4;
			if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out num4))
			{
				return num4;
			}
			throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.JSON_IllegalPrimitive, new object[]
			{
				text
			}));
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002F1AC File Offset: 0x0002D3AC
		private string DeserializePrimitiveToken()
		{
			StringBuilder stringBuilder = new StringBuilder();
			char? c = null;
			for (;;)
			{
				char? c2;
				c = (c2 = this._s.MoveNext());
				if (c2 == null)
				{
					goto IL_7E;
				}
				if (!char.IsLetterOrDigit(c.Value) && c.Value != '.' && c.Value != '-' && c.Value != '_' && c.Value != '+')
				{
					break;
				}
				stringBuilder.Append(c.Value);
			}
			this._s.MovePrev();
			IL_7E:
			return stringBuilder.ToString();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0002F240 File Offset: 0x0002D440
		private string DeserializeString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			char? c = this._s.MoveNext();
			char c2 = this.CheckQuoteChar(c);
			for (;;)
			{
				char? c3;
				c = (c3 = this._s.MoveNext());
				if (c3 == null)
				{
					goto Block_7;
				}
				c3 = c;
				int? num = (c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null;
				int num2 = 92;
				if (num.GetValueOrDefault() == num2 & num != null)
				{
					if (flag)
					{
						stringBuilder.Append('\\');
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				else if (flag)
				{
					this.AppendCharToBuilder(c, stringBuilder);
					flag = false;
				}
				else
				{
					c3 = c;
					num = ((c3 != null) ? new int?((int)c3.GetValueOrDefault()) : null);
					num2 = (int)c2;
					if (num.GetValueOrDefault() == num2 & num != null)
					{
						break;
					}
					stringBuilder.Append(c.Value);
				}
			}
			return Utf16StringValidator.ValidateString(stringBuilder.ToString());
			Block_7:
			throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_UnterminatedString));
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0002F358 File Offset: 0x0002D558
		private void AppendCharToBuilder(char? c, StringBuilder sb)
		{
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 34;
			if (!(num.GetValueOrDefault() == num2 & num != null))
			{
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 39;
				if (!(num.GetValueOrDefault() == num2 & num != null))
				{
					c2 = c;
					num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					num2 = 47;
					if (!(num.GetValueOrDefault() == num2 & num != null))
					{
						c2 = c;
						num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						num2 = 98;
						if (num.GetValueOrDefault() == num2 & num != null)
						{
							sb.Append('\b');
							return;
						}
						c2 = c;
						num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						num2 = 102;
						if (num.GetValueOrDefault() == num2 & num != null)
						{
							sb.Append('\f');
							return;
						}
						c2 = c;
						num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						num2 = 110;
						if (num.GetValueOrDefault() == num2 & num != null)
						{
							sb.Append('\n');
							return;
						}
						c2 = c;
						num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						num2 = 114;
						if (num.GetValueOrDefault() == num2 & num != null)
						{
							sb.Append('\r');
							return;
						}
						c2 = c;
						num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						num2 = 116;
						if (num.GetValueOrDefault() == num2 & num != null)
						{
							sb.Append('\t');
							return;
						}
						c2 = c;
						num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
						num2 = 117;
						if (num.GetValueOrDefault() == num2 & num != null)
						{
							sb.Append((char)int.Parse(this._s.MoveNext(4), NumberStyles.HexNumber, CultureInfo.InvariantCulture));
							return;
						}
						throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_BadEscape));
					}
				}
			}
			sb.Append(c.Value);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		private char CheckQuoteChar(char? c)
		{
			char result = '"';
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 39;
			if (num.GetValueOrDefault() == num2 & num != null)
			{
				result = c.Value;
			}
			else
			{
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 34;
				if (!(num.GetValueOrDefault() == num2 & num != null))
				{
					throw new ArgumentException(this._s.GetDebugString(AtlasWeb.JSON_StringNotQuoted));
				}
			}
			return result;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002F690 File Offset: 0x0002D890
		private object DeserializeStringIntoDateTime()
		{
			if (AppSettings.JsonDeserializerLimitedDate)
			{
				int num = this._s.LimitedIndexOf("\\/\"", 36);
				if (num >= 0)
				{
					Match match = Regex.Match(this._s.Substring(num + 3), "^\"\\\\/Date\\((?<ticks>-?[0-9]+)(?:[a-zA-Z]|(?:\\+|-)[0-9]{4})?\\)\\\\/\"");
					long num2;
					if (long.TryParse(match.Groups["ticks"].Value, out num2))
					{
						this._s.MoveNext(match.Length);
						return new DateTime(num2 * 10000L + JavaScriptSerializer.DatetimeMinTimeTicks, DateTimeKind.Utc);
					}
				}
			}
			else
			{
				int num3 = this._s.IndexOf("\\/\"");
				Match match2 = Regex.Match(this._s.Substring(num3 + 3), "^\"\\\\/Date\\((?<ticks>-?[0-9]+)(?:[a-zA-Z]|(?:\\+|-)[0-9]{4})?\\)\\\\/\"");
				string value = match2.Groups["ticks"].Value;
				long num4;
				if (long.TryParse(value, out num4))
				{
					this._s.MoveNext(match2.Length);
					DateTime dateTime = new DateTime(num4 * 10000L + JavaScriptSerializer.DatetimeMinTimeTicks, DateTimeKind.Utc);
					return dateTime;
				}
			}
			return this.DeserializeString();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002F7B0 File Offset: 0x0002D9B0
		private static bool IsNextElementArray(char? c)
		{
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 91;
			return num.GetValueOrDefault() == num2 & num != null;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002F7F8 File Offset: 0x0002D9F8
		private bool IsNextElementDateTime()
		{
			string text = this._s.MoveNext(8);
			if (text != null)
			{
				this._s.MovePrev(8);
				return string.Equals(text, "\"\\/Date(", StringComparison.Ordinal);
			}
			return false;
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002F830 File Offset: 0x0002DA30
		private static bool IsNextElementObject(char? c)
		{
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 123;
			return num.GetValueOrDefault() == num2 & num != null;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002F878 File Offset: 0x0002DA78
		private static bool IsNextElementString(char? c)
		{
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 34;
			if (!(num.GetValueOrDefault() == num2 & num != null))
			{
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 39;
				return num.GetValueOrDefault() == num2 & num != null;
			}
			return true;
		}

		// Token: 0x040003CC RID: 972
		private const string DateTimePrefix = "\"\\/Date(";

		// Token: 0x040003CD RID: 973
		private const int DateTimePrefixLength = 8;

		// Token: 0x040003CE RID: 974
		private const string DateTimeSuffix = "\\/\"";

		// Token: 0x040003CF RID: 975
		private const int DateTimeSuffixLength = 3;

		// Token: 0x040003D0 RID: 976
		private const int DateTimeMaxLength = 36;

		// Token: 0x040003D1 RID: 977
		internal JavaScriptString _s;

		// Token: 0x040003D2 RID: 978
		private JavaScriptSerializer _serializer;

		// Token: 0x040003D3 RID: 979
		private int _depthLimit;
	}
}
