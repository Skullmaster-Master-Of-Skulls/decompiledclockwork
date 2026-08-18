using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Headers
{
	// Token: 0x02000035 RID: 53
	public class CookieHeaderValue : ICloneable
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00007230 File Offset: 0x00005430
		public CookieHeaderValue(string name, string value)
		{
			CookieState item = new CookieState(name, value);
			this.Cookies.Add(item);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007258 File Offset: 0x00005458
		public CookieHeaderValue(string name, NameValueCollection values)
		{
			CookieState item = new CookieState(name, values);
			this.Cookies.Add(item);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000727F File Offset: 0x0000547F
		protected CookieHeaderValue()
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007288 File Offset: 0x00005488
		private CookieHeaderValue(CookieHeaderValue source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this.Expires = source.Expires;
			this.MaxAge = source.MaxAge;
			this.Domain = source.Domain;
			this.Path = source.Path;
			this.Secure = source.Secure;
			this.HttpOnly = source.HttpOnly;
			foreach (CookieState value in source.Cookies)
			{
				this.Cookies.Add(value.Clone<CookieState>());
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000733C File Offset: 0x0000553C
		public Collection<CookieState> Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					this._cookies = new Collection<CookieState>();
				}
				return this._cookies;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007357 File Offset: 0x00005557
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0000735F File Offset: 0x0000555F
		public DateTimeOffset? Expires { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007368 File Offset: 0x00005568
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00007370 File Offset: 0x00005570
		public TimeSpan? MaxAge { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007379 File Offset: 0x00005579
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00007381 File Offset: 0x00005581
		public string Domain { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000738A File Offset: 0x0000558A
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00007392 File Offset: 0x00005592
		public string Path { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000739B File Offset: 0x0000559B
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000073A3 File Offset: 0x000055A3
		public bool Secure { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000073AC File Offset: 0x000055AC
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000073B4 File Offset: 0x000055B4
		public bool HttpOnly { get; set; }

		// Token: 0x17000045 RID: 69
		public CookieState this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					return null;
				}
				CookieState cookieState = this.Cookies.FirstOrDefault((CookieState c) => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
				if (cookieState == null)
				{
					cookieState = new CookieState(name, string.Empty);
					this.Cookies.Add(cookieState);
				}
				return cookieState;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007440 File Offset: 0x00005640
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool first = true;
			foreach (CookieState cookieState in this.Cookies)
			{
				first = CookieHeaderValue.AppendSegment(stringBuilder, first, cookieState.ToString(), null);
			}
			if (this.Expires != null)
			{
				first = CookieHeaderValue.AppendSegment(stringBuilder, first, "expires", FormattingUtilities.DateToString(this.Expires.Value));
			}
			if (this.MaxAge != null)
			{
				first = CookieHeaderValue.AppendSegment(stringBuilder, first, "max-age", ((int)this.MaxAge.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.Domain != null)
			{
				first = CookieHeaderValue.AppendSegment(stringBuilder, first, "domain", this.Domain);
			}
			if (this.Path != null)
			{
				first = CookieHeaderValue.AppendSegment(stringBuilder, first, "path", this.Path);
			}
			if (this.Secure)
			{
				first = CookieHeaderValue.AppendSegment(stringBuilder, first, "secure", null);
			}
			if (this.HttpOnly)
			{
				first = CookieHeaderValue.AppendSegment(stringBuilder, first, "httponly", null);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000757C File Offset: 0x0000577C
		public object Clone()
		{
			return new CookieHeaderValue(this);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007584 File Offset: 0x00005784
		public static bool TryParse(string input, out CookieHeaderValue parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			string[] array = input.Split(CookieHeaderValue.segmentSeparator);
			CookieHeaderValue cookieHeaderValue = new CookieHeaderValue();
			foreach (string segment in array)
			{
				if (!CookieHeaderValue.ParseCookieSegment(cookieHeaderValue, segment))
				{
					return false;
				}
			}
			if (cookieHeaderValue.Cookies.Count == 0)
			{
				return false;
			}
			parsedValue = cookieHeaderValue;
			return true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000075EE File Offset: 0x000057EE
		private static bool AppendSegment(StringBuilder builder, bool first, string name, string value)
		{
			if (first)
			{
				first = false;
			}
			else
			{
				builder.Append("; ");
			}
			builder.Append(name);
			if (value != null)
			{
				builder.Append("=");
				builder.Append(value);
			}
			return first;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007624 File Offset: 0x00005824
		private static bool ParseCookieSegment(CookieHeaderValue instance, string segment)
		{
			if (string.IsNullOrWhiteSpace(segment))
			{
				return true;
			}
			string[] array = segment.Split(CookieHeaderValue.nameValueSeparator, 2);
			if (array.Length < 1 || string.IsNullOrWhiteSpace(array[0]))
			{
				return false;
			}
			string text = array[0].Trim();
			if (string.Equals(text, "expires", StringComparison.OrdinalIgnoreCase))
			{
				string segmentValue = CookieHeaderValue.GetSegmentValue(array, null);
				DateTimeOffset value;
				if (FormattingUtilities.TryParseDate(segmentValue, out value))
				{
					instance.Expires = new DateTimeOffset?(value);
					return true;
				}
				return false;
			}
			else if (string.Equals(text, "max-age", StringComparison.OrdinalIgnoreCase))
			{
				string segmentValue2 = CookieHeaderValue.GetSegmentValue(array, null);
				int seconds;
				if (FormattingUtilities.TryParseInt32(segmentValue2, out seconds))
				{
					instance.MaxAge = new TimeSpan?(new TimeSpan(0, 0, seconds));
					return true;
				}
				return false;
			}
			else
			{
				if (string.Equals(text, "domain", StringComparison.OrdinalIgnoreCase))
				{
					instance.Domain = CookieHeaderValue.GetSegmentValue(array, null);
					return true;
				}
				if (string.Equals(text, "path", StringComparison.OrdinalIgnoreCase))
				{
					instance.Path = CookieHeaderValue.GetSegmentValue(array, "/");
					return true;
				}
				if (string.Equals(text, "secure", StringComparison.OrdinalIgnoreCase))
				{
					string segmentValue3 = CookieHeaderValue.GetSegmentValue(array, null);
					if (!string.IsNullOrWhiteSpace(segmentValue3))
					{
						return false;
					}
					instance.Secure = true;
					return true;
				}
				else
				{
					if (!string.Equals(text, "httponly", StringComparison.OrdinalIgnoreCase))
					{
						string segmentValue4 = CookieHeaderValue.GetSegmentValue(array, null);
						bool result;
						try
						{
							FormDataCollection formDataCollection = new FormDataCollection(segmentValue4);
							NameValueCollection values = formDataCollection.ReadAsNameValueCollection();
							CookieState item = new CookieState(text, values);
							instance.Cookies.Add(item);
							result = true;
						}
						catch
						{
							result = false;
						}
						return result;
					}
					string segmentValue5 = CookieHeaderValue.GetSegmentValue(array, null);
					if (!string.IsNullOrWhiteSpace(segmentValue5))
					{
						return false;
					}
					instance.HttpOnly = true;
					return true;
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000077B0 File Offset: 0x000059B0
		private static string GetSegmentValue(string[] nameValuePair, string defaultValue)
		{
			if (nameValuePair.Length <= 1)
			{
				return defaultValue;
			}
			return FormattingUtilities.UnquoteToken(nameValuePair[1]);
		}

		// Token: 0x04000072 RID: 114
		private const string ExpiresToken = "expires";

		// Token: 0x04000073 RID: 115
		private const string MaxAgeToken = "max-age";

		// Token: 0x04000074 RID: 116
		private const string DomainToken = "domain";

		// Token: 0x04000075 RID: 117
		private const string PathToken = "path";

		// Token: 0x04000076 RID: 118
		private const string SecureToken = "secure";

		// Token: 0x04000077 RID: 119
		private const string HttpOnlyToken = "httponly";

		// Token: 0x04000078 RID: 120
		private const string DefaultPath = "/";

		// Token: 0x04000079 RID: 121
		private static readonly char[] segmentSeparator = new char[]
		{
			';'
		};

		// Token: 0x0400007A RID: 122
		private static readonly char[] nameValueSeparator = new char[]
		{
			'='
		};

		// Token: 0x0400007B RID: 123
		private Collection<CookieState> _cookies;
	}
}
