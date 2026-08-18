using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Http.Headers
{
	// Token: 0x02000038 RID: 56
	[__DynamicallyInvokable]
	public class MediaTypeHeaderValue : ICloneable
	{
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000C584 File Offset: 0x0000A784
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		[__DynamicallyInvokable]
		public string CharSet
		{
			[__DynamicallyInvokable]
			get
			{
				NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, "charset");
				if (nameValueHeaderValue != null)
				{
					return nameValueHeaderValue.Value;
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				NameValueHeaderValue nameValueHeaderValue = NameValueHeaderValue.Find(this.parameters, "charset");
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
					if (nameValueHeaderValue != null)
					{
						nameValueHeaderValue.Value = value;
						return;
					}
					this.Parameters.Add(new NameValueHeaderValue("charset", value));
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000C608 File Offset: 0x0000A808
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000C623 File Offset: 0x0000A823
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000C62B File Offset: 0x0000A82B
		[__DynamicallyInvokable]
		public string MediaType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.mediaType;
			}
			[__DynamicallyInvokable]
			set
			{
				MediaTypeHeaderValue.CheckMediaTypeFormat(value, "value");
				this.mediaType = value;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C63F File Offset: 0x0000A83F
		internal MediaTypeHeaderValue()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C648 File Offset: 0x0000A848
		[__DynamicallyInvokable]
		protected MediaTypeHeaderValue(MediaTypeHeaderValue source)
		{
			this.mediaType = source.mediaType;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue nameValueHeaderValue in source.parameters)
				{
					this.Parameters.Add((NameValueHeaderValue)((ICloneable)nameValueHeaderValue).Clone());
				}
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		[__DynamicallyInvokable]
		public MediaTypeHeaderValue(string mediaType)
		{
			MediaTypeHeaderValue.CheckMediaTypeFormat(mediaType, "mediaType");
			this.mediaType = mediaType;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C6DA File Offset: 0x0000A8DA
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.mediaType + NameValueHeaderValue.ToString(this.parameters, ';', true);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = obj as MediaTypeHeaderValue;
			return mediaTypeHeaderValue != null && string.Compare(this.mediaType, mediaTypeHeaderValue.mediaType, StringComparison.OrdinalIgnoreCase) == 0 && HeaderUtilities.AreEqualCollections<NameValueHeaderValue>(this.parameters, mediaTypeHeaderValue.parameters);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C738 File Offset: 0x0000A938
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.mediaType.ToLowerInvariant().GetHashCode() ^ NameValueHeaderValue.GetHashCode(this.parameters);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C758 File Offset: 0x0000A958
		[__DynamicallyInvokable]
		public static MediaTypeHeaderValue Parse(string input)
		{
			int num = 0;
			return (MediaTypeHeaderValue)MediaTypeHeaderParser.SingleValueParser.ParseValue(input, null, ref num);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C77C File Offset: 0x0000A97C
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out MediaTypeHeaderValue parsedValue)
		{
			int num = 0;
			parsedValue = null;
			object obj;
			if (MediaTypeHeaderParser.SingleValueParser.TryParseValue(input, null, ref num, out obj))
			{
				parsedValue = (MediaTypeHeaderValue)obj;
				return true;
			}
			return false;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		internal static int GetMediaTypeLength(string input, int startIndex, Func<MediaTypeHeaderValue> mediaTypeCreator, out MediaTypeHeaderValue parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input) || startIndex >= input.Length)
			{
				return 0;
			}
			string text = null;
			int mediaTypeExpressionLength = MediaTypeHeaderValue.GetMediaTypeExpressionLength(input, startIndex, out text);
			if (mediaTypeExpressionLength == 0)
			{
				return 0;
			}
			int num = startIndex + mediaTypeExpressionLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			MediaTypeHeaderValue mediaTypeHeaderValue;
			if (num >= input.Length || input[num] != ';')
			{
				mediaTypeHeaderValue = mediaTypeCreator();
				mediaTypeHeaderValue.mediaType = text;
				parsedValue = mediaTypeHeaderValue;
				return num - startIndex;
			}
			mediaTypeHeaderValue = mediaTypeCreator();
			mediaTypeHeaderValue.mediaType = text;
			num++;
			int nameValueListLength = NameValueHeaderValue.GetNameValueListLength(input, num, ';', mediaTypeHeaderValue.Parameters);
			if (nameValueListLength == 0)
			{
				return 0;
			}
			parsedValue = mediaTypeHeaderValue;
			return num + nameValueListLength - startIndex;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C84C File Offset: 0x0000AA4C
		private static int GetMediaTypeExpressionLength(string input, int startIndex, out string mediaType)
		{
			mediaType = null;
			int tokenLength = HttpRuleParser.GetTokenLength(input, startIndex);
			if (tokenLength == 0)
			{
				return 0;
			}
			int num = startIndex + tokenLength;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			if (num >= input.Length || input[num] != '/')
			{
				return 0;
			}
			num++;
			num += HttpRuleParser.GetWhitespaceLength(input, num);
			int tokenLength2 = HttpRuleParser.GetTokenLength(input, num);
			if (tokenLength2 == 0)
			{
				return 0;
			}
			int num2 = num + tokenLength2 - startIndex;
			if (tokenLength + tokenLength2 + 1 == num2)
			{
				mediaType = input.Substring(startIndex, num2);
			}
			else
			{
				mediaType = input.Substring(startIndex, tokenLength) + "/" + input.Substring(num, tokenLength2);
			}
			return num2;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
		private static void CheckMediaTypeFormat(string mediaType, string parameterName)
		{
			if (string.IsNullOrEmpty(mediaType))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, parameterName);
			}
			string text;
			int mediaTypeExpressionLength = MediaTypeHeaderValue.GetMediaTypeExpressionLength(mediaType, 0, out text);
			if (mediaTypeExpressionLength == 0 || text.Length != mediaType.Length)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_invalid_value, new object[]
				{
					mediaType
				}));
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C93B File Offset: 0x0000AB3B
		object ICloneable.Clone()
		{
			return new MediaTypeHeaderValue(this);
		}

		// Token: 0x0400015F RID: 351
		private const string charSet = "charset";

		// Token: 0x04000160 RID: 352
		private ICollection<NameValueHeaderValue> parameters;

		// Token: 0x04000161 RID: 353
		private string mediaType;
	}
}
