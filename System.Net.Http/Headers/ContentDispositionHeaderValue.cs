using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x02000028 RID: 40
	[__DynamicallyInvokable]
	public class ContentDispositionHeaderValue : ICloneable
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000080BA File Offset: 0x000062BA
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000080C2 File Offset: 0x000062C2
		[__DynamicallyInvokable]
		public string DispositionType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dispositionType;
			}
			[__DynamicallyInvokable]
			set
			{
				ContentDispositionHeaderValue.CheckDispositionTypeFormat(value, "value");
				this.dispositionType = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000080D6 File Offset: 0x000062D6
		[__DynamicallyInvokable]
		public ICollection<NameValueHeaderValue> Parameters
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ObjectCollection<NameValueHeaderValue>();
				}
				return this.parameters;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000080F1 File Offset: 0x000062F1
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000080FE File Offset: 0x000062FE
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetName("name");
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetName("name", value);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000810C File Offset: 0x0000630C
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00008119 File Offset: 0x00006319
		[__DynamicallyInvokable]
		public string FileName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetName("filename");
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetName("filename", value);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00008127 File Offset: 0x00006327
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00008134 File Offset: 0x00006334
		[__DynamicallyInvokable]
		public string FileNameStar
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetName("filename*");
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetName("filename*", value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008142 File Offset: 0x00006342
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000814F File Offset: 0x0000634F
		[__DynamicallyInvokable]
		public DateTimeOffset? CreationDate
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDate("creation-date");
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetDate("creation-date", value);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000815D File Offset: 0x0000635D
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000816A File Offset: 0x0000636A
		[__DynamicallyInvokable]
		public DateTimeOffset? ModificationDate
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDate("modification-date");
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetDate("modification-date", value);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008178 File Offset: 0x00006378
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00008185 File Offset: 0x00006385
		[__DynamicallyInvokable]
		public DateTimeOffset? ReadDate
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDate("read-date");
			}
			[__DynamicallyInvokable]
			set
			{
				this.SetDate("read-date", value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008194 File Offset: 0x00006394
		// (set) Token: 0x060001ED RID: 493 RVA: 0x000081DC File Offset: 0x000063DC
		[__DynamicallyInvokable]
		public long? Size
		{
			[__DynamicallyInvokable]
			get
			{
				NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, "size");
				if (nameValueHeaderValue != null)
				{
					string value = nameValueHeaderValue.Value;
					ulong value2;
					if (ulong.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out value2))
					{
						return new long?((long)value2);
					}
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, "size");
				if (value == null)
				{
					if (nameValueHeaderValue != null)
					{
						this.parameters.Remove(nameValueHeaderValue);
						return;
					}
				}
				else
				{
					long? num = value;
					long num2 = 0L;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					if (nameValueHeaderValue != null)
					{
						nameValueHeaderValue.Value = value.Value.ToString(CultureInfo.InvariantCulture);
						return;
					}
					string value2 = value.Value.ToString(CultureInfo.InvariantCulture);
					this.Parameters.Add(new NameValueHeaderValue("size", value2));
				}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008281 File Offset: 0x00006481
		internal ContentDispositionHeaderValue()
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000828C File Offset: 0x0000648C
		[__DynamicallyInvokable]
		protected ContentDispositionHeaderValue(ContentDispositionHeaderValue source)
		{
			this.dispositionType = source.dispositionType;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add((NameValueHeaderValue)((ICloneable)nameValueHeaderValue).Clone());
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008304 File Offset: 0x00006504
		[__DynamicallyInvokable]
		public ContentDispositionHeaderValue(string dispositionType)
		{
			ContentDispositionHeaderValue.CheckDispositionTypeFormat(dispositionType, "dispositionType");
			this.dispositionType = dispositionType;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000831E File Offset: 0x0000651E
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.dispositionType + NameValueHeaderValue.ToString(this.parameters, ';', true);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000833C File Offset: 0x0000653C
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			ContentDispositionHeaderValue contentDispositionHeaderValue = obj as ContentDispositionHeaderValue;
			return contentDispositionHeaderValue != null && string.Compare(this.dispositionType, contentDispositionHeaderValue.dispositionType, StringComparison.OrdinalIgnoreCase) == 0 && HeaderUtilities.AreEqualCollections<NameValueHeaderValue>(this.parameters, contentDispositionHeaderValue.parameters);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000837C File Offset: 0x0000657C
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.dispositionType.ToLowerInvariant().GetHashCode() ^ NameValueHeaderValue.GetHashCode(this.parameters);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000839A File Offset: 0x0000659A
		object ICloneable.Clone()
		{
			return new ContentDispositionHeaderValue(this);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000083A4 File Offset: 0x000065A4
		[__DynamicallyInvokable]
		public static ContentDispositionHeaderValue Parse(string input)
		{
			int num = 0;
			return (ContentDispositionHeaderValue)GenericHeaderParser.ContentDispositionParser.ParseValue(input, null, ref num);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000083C8 File Offset: 0x000065C8
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out ContentDispositionHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (GenericHeaderParser.ContentDispositionParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (ContentDispositionHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000083F8 File Offset: 0x000065F8
		internal static int GetDispositionTypeLength(string input, int startIndex, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			string text = null;
			int dispositionTypeExpressionLength = ContentDispositionHeaderValue.GetDispositionTypeExpressionLength(input, startIndex, out text);
			if (dispositionTypeExpressionLength == 0)
			{
				return 0;
			}
			int num = startIndex + dispositionTypeExpressionLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			ContentDispositionHeaderValue contentDispositionHeaderValue = new ContentDispositionHeaderValue();
			contentDispositionHeaderValue.dispositionType = text;
			if (num >= input.Length || input[num] != ';')
			{
				parsedValue = contentDispositionHeaderValue;
				return num - startIndex;
			}
			num++;
			int nameValueListLength = NameValueHeaderValue.GetNameValueListLength(input, num, ';', contentDispositionHeaderValue.Parameters);
			if (nameValueListLength == 0)
			{
				return 0;
			}
			parsedValue = contentDispositionHeaderValue;
			return num + nameValueListLength - startIndex;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008488 File Offset: 0x00006688
		private static int GetDispositionTypeExpressionLength(string input, int startIndex, out string dispositionType)
		{
			dispositionType = null;
			int tokenLength = HttpRuleParser.GetTokenLength(input, startIndex);
			if (tokenLength == 0)
			{
				return 0;
			}
			dispositionType = input.Substring(startIndex, tokenLength);
			return tokenLength;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000084B0 File Offset: 0x000066B0
		private static void CheckDispositionTypeFormat(string dispositionType, string parameterName)
		{
			if (string.IsNullOrEmpty(dispositionType))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, parameterName);
			}
			string text;
			int dispositionTypeExpressionLength = ContentDispositionHeaderValue.GetDispositionTypeExpressionLength(dispositionType, 0, out text);
			if (dispositionTypeExpressionLength == 0 || text.Length != dispositionType.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					dispositionType
				}));
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000850C File Offset: 0x0000670C
		private DateTimeOffset? GetDate(string parameter)
		{
			NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, parameter);
			if (nameValueHeaderValue != null)
			{
				string text = nameValueHeaderValue.Value;
				if (this.IsQuoted(text))
				{
					text = text.Substring(1, text.Length - 2);
				}
				DateTimeOffset value;
				if (HttpRuleParser.TryStringToDate(text, out value))
				{
					return new DateTimeOffset?(value);
				}
			}
			return null;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008564 File Offset: 0x00006764
		private void SetDate(string parameter, DateTimeOffset? date)
		{
			NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, parameter);
			if (date == null)
			{
				if (nameValueHeaderValue != null)
				{
					this.parameters.Remove(nameValueHeaderValue);
					return;
				}
			}
			else
			{
				string value = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
				{
					HttpRuleParser.DateToString(date.Value)
				});
				if (nameValueHeaderValue != null)
				{
					nameValueHeaderValue.Value = value;
					return;
				}
				this.Parameters.Add(new NameValueHeaderValue(parameter, value));
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000085DC File Offset: 0x000067DC
		private string GetName(string parameter)
		{
			NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, parameter);
			if (nameValueHeaderValue == null)
			{
				return null;
			}
			if (parameter.EndsWith("*", StringComparison.Ordinal))
			{
				string result;
				if (this.TryDecode5987(nameValueHeaderValue.Value, out result))
				{
					return result;
				}
				return null;
			}
			else
			{
				string result;
				if (this.TryDecodeMime(nameValueHeaderValue.Value, out result))
				{
					return result;
				}
				return nameValueHeaderValue.Value;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008638 File Offset: 0x00006838
		private void SetName(string parameter, string value)
		{
			NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, parameter);
			if (string.IsNullOrEmpty(value))
			{
				if (nameValueHeaderValue != null)
				{
					this.parameters.Remove(nameValueHeaderValue);
					return;
				}
			}
			else
			{
				string value2 = string.Empty;
				if (parameter.EndsWith("*", StringComparison.Ordinal))
				{
					value2 = this.Encode5987(value);
				}
				else
				{
					value2 = this.EncodeAndQuoteMime(value);
				}
				if (nameValueHeaderValue != null)
				{
					nameValueHeaderValue.Value = value2;
					return;
				}
				this.Parameters.Add(new NameValueHeaderValue(parameter, value2));
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000086B0 File Offset: 0x000068B0
		private string EncodeAndQuoteMime(string input)
		{
			string text = input;
			bool flag = false;
			if (this.IsQuoted(text))
			{
				text = text.Substring(1, text.Length - 2);
				flag = true;
			}
			if (text.IndexOf("\"", 0, StringComparison.Ordinal) >= 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					input
				}));
			}
			if (this.RequiresEncoding(text))
			{
				flag = true;
				text = this.EncodeMime(text);
			}
			else if (!flag && HttpRuleParser.GetTokenLength(text, 0) != text.Length)
			{
				flag = true;
			}
			if (flag)
			{
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
				{
					text
				});
			}
			return text;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008752 File Offset: 0x00006952
		private bool IsQuoted(string value)
		{
			return value.Length > 1 && value.StartsWith("\"", StringComparison.Ordinal) && value.EndsWith("\"", StringComparison.Ordinal);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000877C File Offset: 0x0000697C
		private bool RequiresEncoding(string input)
		{
			foreach (char c in input)
			{
				if (c > '\u007f')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000087AC File Offset: 0x000069AC
		private string EncodeMime(string input)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			string text = Convert.ToBase64String(bytes);
			return string.Format(CultureInfo.InvariantCulture, "=?utf-8?B?{0}?=", new object[]
			{
				text
			});
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000087E8 File Offset: 0x000069E8
		private bool TryDecodeMime(string input, out string output)
		{
			output = null;
			if (!this.IsQuoted(input) || input.Length < 10)
			{
				return false;
			}
			string[] array = input.Split(new char[]
			{
				'?'
			});
			if (array.Length != 5 || array[0] != "\"=" || array[4] != "=\"" || array[2].ToLowerInvariant() != "b")
			{
				return false;
			}
			try
			{
				Encoding encoding = Encoding.GetEncoding(array[1]);
				byte[] bytes = Convert.FromBase64String(array[3]);
				output = encoding.GetString(bytes);
				return true;
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			return false;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000088A4 File Offset: 0x00006AA4
		private string Encode5987(string input)
		{
			StringBuilder stringBuilder = new StringBuilder("utf-8''");
			foreach (char c in input)
			{
				if (c > '\u007f')
				{
					byte[] bytes = Encoding.UTF8.GetBytes(c.ToString());
					foreach (byte character in bytes)
					{
						stringBuilder.Append(Uri.HexEscape((char)character));
					}
				}
				else if (!HttpRuleParser.IsTokenChar(c) || c == '*' || c == '\'' || c == '%')
				{
					stringBuilder.Append(Uri.HexEscape(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008958 File Offset: 0x00006B58
		private bool TryDecode5987(string input, out string output)
		{
			output = null;
			string[] array = input.Split(new char[]
			{
				'\''
			});
			if (array.Length != 3)
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				Encoding encoding = Encoding.GetEncoding(array[0]);
				string text = array[2];
				byte[] array2 = new byte[text.Length];
				int num = 0;
				for (int i = 0; i < text.Length; i++)
				{
					if (Uri.IsHexEncoding(text, i))
					{
						array2[num++] = (byte)Uri.HexUnescape(text, ref i);
						i--;
					}
					else
					{
						if (num > 0)
						{
							stringBuilder.Append(encoding.GetString(array2, 0, num));
							num = 0;
						}
						stringBuilder.Append(text[i]);
					}
				}
				if (num > 0)
				{
					stringBuilder.Append(encoding.GetString(array2, 0, num));
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			output = stringBuilder.ToString();
			return true;
		}

		// Token: 0x040000F7 RID: 247
		private const string fileName = "filename";

		// Token: 0x040000F8 RID: 248
		private const string name = "name";

		// Token: 0x040000F9 RID: 249
		private const string fileNameStar = "filename*";

		// Token: 0x040000FA RID: 250
		private const string creationDate = "creation-date";

		// Token: 0x040000FB RID: 251
		private const string modificationDate = "modification-date";

		// Token: 0x040000FC RID: 252
		private const string readDate = "read-date";

		// Token: 0x040000FD RID: 253
		private const string size = "size";

		// Token: 0x040000FE RID: 254
		private ICollection<NameValueHeaderValue> parameters;

		// Token: 0x040000FF RID: 255
		private string dispositionType;
	}
}
