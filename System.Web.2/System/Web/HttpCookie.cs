using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000091 RID: 145
	public sealed class HttpCookie
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x00015C67 File Offset: 0x00013E67
		internal HttpCookie()
		{
			this._changed = true;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00015C81 File Offset: 0x00013E81
		public HttpCookie(string name)
		{
			this._name = name;
			this.SetDefaultsFromConfig();
			this._changed = true;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00015CA8 File Offset: 0x00013EA8
		public HttpCookie(string name, string value)
		{
			this._name = name;
			this._stringValue = value;
			this.SetDefaultsFromConfig();
			this._changed = true;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00015CD8 File Offset: 0x00013ED8
		internal void SetDefaultsFromConfig()
		{
			HttpCookiesSection httpCookiesSection = (HttpContext.Current != null) ? RuntimeConfig.GetLKGConfig(HttpContext.Current).HttpCookies : RuntimeConfig.GetAppLKGConfig().HttpCookies;
			this._secure = httpCookiesSection.RequireSSL;
			this._httpOnly = httpCookiesSection.HttpOnlyCookies;
			this._sameSite = httpCookiesSection.SameSite;
			if (httpCookiesSection.Domain != null && httpCookiesSection.Domain.Length > 0)
			{
				this._domain = httpCookiesSection.Domain;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00015D4E File Offset: 0x00013F4E
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00015D56 File Offset: 0x00013F56
		internal bool Changed
		{
			get
			{
				return this._changed;
			}
			set
			{
				this._changed = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00015D5F File Offset: 0x00013F5F
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00015D67 File Offset: 0x00013F67
		internal bool Added
		{
			get
			{
				return this._added;
			}
			set
			{
				this._added = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00015D70 File Offset: 0x00013F70
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00015D78 File Offset: 0x00013F78
		internal bool IsInResponseHeader { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00015D81 File Offset: 0x00013F81
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00015D89 File Offset: 0x00013F89
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
				this._changed = true;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00015D99 File Offset: 0x00013F99
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00015DA1 File Offset: 0x00013FA1
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
				this._changed = true;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00015DB1 File Offset: 0x00013FB1
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00015DB9 File Offset: 0x00013FB9
		public bool Secure
		{
			get
			{
				return this._secure;
			}
			set
			{
				this._secure = value;
				this._changed = true;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00015DC9 File Offset: 0x00013FC9
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x00015DD1 File Offset: 0x00013FD1
		public bool Shareable { get; set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00015DDA File Offset: 0x00013FDA
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x00015DE2 File Offset: 0x00013FE2
		public bool HttpOnly
		{
			get
			{
				return this._httpOnly;
			}
			set
			{
				this._httpOnly = value;
				this._changed = true;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00015DF2 File Offset: 0x00013FF2
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x00015DFA File Offset: 0x00013FFA
		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				this._domain = value;
				this._changed = true;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00015E0A File Offset: 0x0001400A
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x00015E20 File Offset: 0x00014020
		public DateTime Expires
		{
			get
			{
				if (!this._expirationSet)
				{
					return DateTime.MinValue;
				}
				return this._expires;
			}
			set
			{
				this._expires = value;
				this._expirationSet = true;
				this._changed = true;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00015E37 File Offset: 0x00014037
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x00015E54 File Offset: 0x00014054
		public string Value
		{
			get
			{
				if (this._multiValue != null)
				{
					return this._multiValue.ToString(false);
				}
				return this._stringValue;
			}
			set
			{
				if (this._multiValue != null)
				{
					this._multiValue.Reset();
					this._multiValue.Add(null, value);
				}
				else
				{
					this._stringValue = value;
				}
				this._changed = true;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00015E86 File Offset: 0x00014086
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00015E8E File Offset: 0x0001408E
		public SameSiteMode SameSite
		{
			get
			{
				return this._sameSite;
			}
			set
			{
				this._sameSite = value;
				this._changed = true;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00015E9E File Offset: 0x0001409E
		public bool HasKeys
		{
			get
			{
				return this.Values.HasKeys();
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00015EAC File Offset: 0x000140AC
		private bool SupportsHttpOnly(HttpContext context)
		{
			if (context != null && context.Request != null)
			{
				HttpBrowserCapabilities browser = context.Request.Browser;
				return browser != null && (browser.Type != "IE5" || browser.Platform != "MacPPC");
			}
			return false;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00015EFC File Offset: 0x000140FC
		public NameValueCollection Values
		{
			get
			{
				if (this._multiValue == null)
				{
					this._multiValue = new HttpValueCollection();
					if (this._stringValue != null)
					{
						if (this._stringValue.IndexOf('&') >= 0 || this._stringValue.IndexOf('=') >= 0)
						{
							this._multiValue.FillFromString(this._stringValue);
						}
						else
						{
							this._multiValue.Add(null, this._stringValue);
						}
						this._stringValue = null;
					}
				}
				this._changed = true;
				return this._multiValue;
			}
		}

		// Token: 0x170003D5 RID: 981
		public string this[string key]
		{
			get
			{
				return this.Values[key];
			}
			set
			{
				this.Values[key] = value;
				this._changed = true;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00015FA4 File Offset: 0x000141A4
		public static bool TryParse(string input, out HttpCookie result)
		{
			result = null;
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			int num = input.IndexOf(';');
			string text = (num >= 0) ? input.Substring(0, num) : input;
			HttpCookie httpCookie = HttpRequest.CreateCookieFromString(text.Trim(), false);
			if (string.IsNullOrEmpty(httpCookie.Name))
			{
				return false;
			}
			HttpCookie.TryParseFlags(input, num, httpCookie);
			result = httpCookie;
			return true;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00016000 File Offset: 0x00014200
		internal static void TryParseFlags(string input, int dividerIndex, HttpCookie cookie)
		{
			while (dividerIndex >= 0 && dividerIndex < input.Length - 1)
			{
				int num = dividerIndex + 1;
				dividerIndex = input.IndexOf(';', num);
				string text = (dividerIndex >= 0) ? input.Substring(num, dividerIndex - num).Trim() : input.Substring(num).Trim();
				int num2 = text.IndexOf('=');
				string s = (num2 >= 0) ? text.Substring(0, num2).Trim() : text;
				string text2 = (num2 >= 0 && num2 < text.Length - 1) ? text.Substring(num2 + 1).Trim() : null;
				if (StringUtil.EqualsIgnoreCase(s, "Expires"))
				{
					DateTime expires;
					if (DateTime.TryParse(text2, out expires))
					{
						cookie.Expires = expires;
					}
				}
				else if (text2 != null && StringUtil.EqualsIgnoreCase(s, "Domain"))
				{
					cookie.Domain = text2;
				}
				else if (text2 != null && StringUtil.EqualsIgnoreCase(s, "Path"))
				{
					cookie.Path = text2;
				}
				else if (StringUtil.EqualsIgnoreCase(s, "Secure"))
				{
					cookie.Secure = true;
				}
				else if (StringUtil.EqualsIgnoreCase(s, "HttpOnly"))
				{
					cookie.HttpOnly = true;
				}
				else if (StringUtil.EqualsIgnoreCase(s, "SameSite"))
				{
					SameSiteMode sameSite = (SameSiteMode)(-1);
					if (Enum.TryParse<SameSiteMode>(text2, true, out sameSite))
					{
						cookie.SameSite = sameSite;
					}
				}
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00016144 File Offset: 0x00014344
		internal HttpResponseHeader GetSetCookieHeader(HttpContext context)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this._name))
			{
				stringBuilder.Append(this._name);
				stringBuilder.Append('=');
			}
			if (this._multiValue != null)
			{
				stringBuilder.Append(this._multiValue.ToString(false));
			}
			else if (this._stringValue != null)
			{
				stringBuilder.Append(this._stringValue);
			}
			if (!string.IsNullOrEmpty(this._domain))
			{
				stringBuilder.Append("; domain=");
				stringBuilder.Append(this._domain);
			}
			if (this._expirationSet && this._expires != DateTime.MinValue)
			{
				stringBuilder.Append("; expires=");
				stringBuilder.Append(HttpUtility.FormatHttpCookieDateTime(this._expires));
			}
			if (!string.IsNullOrEmpty(this._path))
			{
				stringBuilder.Append("; path=");
				stringBuilder.Append(this._path);
			}
			if (this._secure)
			{
				stringBuilder.Append("; secure");
			}
			if (this._httpOnly && this.SupportsHttpOnly(context))
			{
				stringBuilder.Append("; HttpOnly");
			}
			if (this._sameSite > (AppSettings.SuppressSameSiteNone ? SameSiteMode.None : ((SameSiteMode)(-1))))
			{
				stringBuilder.Append("; SameSite=");
				stringBuilder.Append(this._sameSite);
			}
			return new HttpResponseHeader(27, stringBuilder.ToString());
		}

		// Token: 0x04000385 RID: 901
		private string _name;

		// Token: 0x04000386 RID: 902
		private string _path = "/";

		// Token: 0x04000387 RID: 903
		private bool _secure;

		// Token: 0x04000388 RID: 904
		private bool _httpOnly;

		// Token: 0x04000389 RID: 905
		private string _domain;

		// Token: 0x0400038A RID: 906
		private bool _expirationSet;

		// Token: 0x0400038B RID: 907
		private DateTime _expires;

		// Token: 0x0400038C RID: 908
		private string _stringValue;

		// Token: 0x0400038D RID: 909
		private HttpValueCollection _multiValue;

		// Token: 0x0400038E RID: 910
		private bool _changed;

		// Token: 0x0400038F RID: 911
		private bool _added;

		// Token: 0x04000390 RID: 912
		private SameSiteMode _sameSite;
	}
}
