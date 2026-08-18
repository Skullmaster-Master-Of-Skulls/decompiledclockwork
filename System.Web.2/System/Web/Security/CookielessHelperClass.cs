using System;
using System.Web.Util;

namespace System.Web.Security
{
	// Token: 0x020005D6 RID: 1494
	internal sealed class CookielessHelperClass
	{
		// Token: 0x06004B9D RID: 19357 RVA: 0x00101156 File Offset: 0x000FF356
		internal CookielessHelperClass(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x00101168 File Offset: 0x000FF368
		private void Init()
		{
			if (this._Headers != null)
			{
				return;
			}
			if (this._Headers == null)
			{
				this.GetCookielessValuesFromHeader();
			}
			if (this._Headers == null)
			{
				this.RemoveCookielessValuesFromPath();
			}
			if (this._Headers == null)
			{
				this._Headers = string.Empty;
			}
			this._OriginalHeaders = this._Headers;
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x001011BC File Offset: 0x000FF3BC
		private void GetCookielessValuesFromHeader()
		{
			this._Headers = this._Context.Request.Headers["AspFilterSessionId"];
			this._OriginalHeaders = this._Headers;
			if (!string.IsNullOrEmpty(this._Headers))
			{
				if (this._Headers.Length == 24 && !this._Headers.Contains("("))
				{
					this._Headers = null;
					return;
				}
				this._Context.Response.SetAppPathModifier("(" + this._Headers + ")");
			}
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x00101250 File Offset: 0x000FF450
		internal void RemoveCookielessValuesFromPath()
		{
			string text = this._Context.Request.ClientFilePath.VirtualPathString;
			if (text.IndexOf('(') == -1)
			{
				return;
			}
			int num;
			int num2;
			if (AppSettings.RestoreAggressiveCookielessPathRemoval)
			{
				num = text.LastIndexOf(")/", StringComparison.Ordinal);
				num2 = ((num > 2) ? text.LastIndexOf("/(", num - 1, num, StringComparison.Ordinal) : -1);
			}
			else
			{
				num2 = text.IndexOf("/(", StringComparison.Ordinal);
				num = ((num2 >= 0) ? text.IndexOf(")/", num2 + 2, StringComparison.Ordinal) : -1);
			}
			if (num2 < 0)
			{
				return;
			}
			if (this._Headers == null)
			{
				this.GetCookielessValuesFromHeader();
			}
			if (CookielessHelperClass.IsValidHeader(text, num2 + 2, num))
			{
				if (this._Headers == null)
				{
					this._Headers = text.Substring(num2 + 2, num - num2 - 2);
				}
				text = text.Substring(0, num2) + text.Substring(num + 1);
				this._Context.Request.ClientFilePath = VirtualPath.CreateAbsolute(text);
				string rawUrl = this._Context.Request.RawUrl;
				int num3 = rawUrl.IndexOf('?');
				if (num3 > -1)
				{
					text += rawUrl.Substring(num3);
				}
				this._Context.Request.RawUrl = text;
				if (!string.IsNullOrEmpty(this._Headers))
				{
					this._Context.Request.ValidateCookielessHeaderIfRequiredByConfig(this._Headers);
					this._Context.Response.SetAppPathModifier("(" + this._Headers + ")");
					string filePath = this._Context.Request.FilePath;
					if (string.IsNullOrEmpty(this._Context.Request.Headers["AspFilterSessionId"]) || AppSettings.RestoreAggressiveCookielessPathRemoval)
					{
						string text2 = this._Context.Response.RemoveAppPathModifier(filePath);
						if (filePath != text2)
						{
							this._Context.RewritePath(VirtualPath.CreateAbsolute(text2), this._Context.Request.PathInfoObject, null, false);
						}
					}
				}
			}
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x00101440 File Offset: 0x000FF640
		internal string GetCookieValue(char identifier)
		{
			int num = 0;
			int num2 = 0;
			this.Init();
			if (!CookielessHelperClass.GetValueStartAndEnd(this._Headers, identifier, out num, out num2))
			{
				return null;
			}
			return this._Headers.Substring(num, num2 - num);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0010147C File Offset: 0x000FF67C
		internal bool DoesCookieValueExistInOriginal(char identifier)
		{
			int num = 0;
			int num2 = 0;
			this.Init();
			return CookielessHelperClass.GetValueStartAndEnd(this._OriginalHeaders, identifier, out num, out num2);
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x001014A4 File Offset: 0x000FF6A4
		internal void SetCookieValue(char identifier, string cookieValue)
		{
			int num = 0;
			int num2 = 0;
			this.Init();
			while (CookielessHelperClass.GetValueStartAndEnd(this._Headers, identifier, out num, out num2))
			{
				this._Headers = this._Headers.Substring(0, num - 2) + this._Headers.Substring(num2 + 1);
			}
			if (!string.IsNullOrEmpty(cookieValue))
			{
				this._Headers = this._Headers + new string(new char[]
				{
					identifier,
					'('
				}) + cookieValue + ")";
			}
			if (this._Headers.Length > 0)
			{
				this._Context.Response.SetAppPathModifier("(" + this._Headers + ")");
				return;
			}
			this._Context.Response.SetAppPathModifier(null);
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x00101570 File Offset: 0x000FF770
		private static bool GetValueStartAndEnd(string headers, char identifier, out int startPos, out int endPos)
		{
			if (string.IsNullOrEmpty(headers))
			{
				startPos = (endPos = -1);
				return false;
			}
			string value = new string(new char[]
			{
				identifier,
				'('
			});
			startPos = headers.IndexOf(value, StringComparison.Ordinal);
			if (startPos < 0)
			{
				startPos = (endPos = -1);
				return false;
			}
			startPos += 2;
			endPos = headers.IndexOf(')', startPos);
			if (endPos < 0)
			{
				startPos = (endPos = -1);
				return false;
			}
			return true;
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x001015E0 File Offset: 0x000FF7E0
		internal static bool UseCookieless(HttpContext context, bool doRedirect, HttpCookieMode cookieMode)
		{
			switch (cookieMode)
			{
			case HttpCookieMode.UseUri:
				return true;
			case HttpCookieMode.UseCookies:
				return false;
			case HttpCookieMode.AutoDetect:
			{
				if (context == null)
				{
					context = HttpContext.Current;
				}
				if (context == null)
				{
					return false;
				}
				if (!context.Request.Browser.Cookies || !context.Request.Browser.SupportsRedirectWithCookie)
				{
					return true;
				}
				string cookieValue = context.CookielessHelper.GetCookieValue('X');
				if (cookieValue != null && cookieValue == "1")
				{
					return true;
				}
				string value = context.Request.Headers["Cookie"];
				if (!string.IsNullOrEmpty(value))
				{
					return false;
				}
				string text = context.Request.QueryString["AspxAutoDetectCookieSupport"];
				if (text != null && text == "1")
				{
					context.CookielessHelper.SetCookieValue('X', "1");
					return true;
				}
				if (doRedirect)
				{
					context.CookielessHelper.RedirectWithDetection(null);
				}
				return false;
			}
			case HttpCookieMode.UseDeviceProfile:
				if (context == null)
				{
					context = HttpContext.Current;
				}
				return context != null && (!context.Request.Browser.Cookies || !context.Request.Browser.SupportsRedirectWithCookie);
			default:
				return false;
			}
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00101708 File Offset: 0x000FF908
		internal void RedirectWithDetection(string redirectPath)
		{
			this.Init();
			if (string.IsNullOrEmpty(redirectPath))
			{
				redirectPath = this._Context.Request.RawUrl;
			}
			if (redirectPath.IndexOf("?", StringComparison.Ordinal) > 0)
			{
				redirectPath += "&AspxAutoDetectCookieSupport=1";
			}
			else
			{
				redirectPath += "?AspxAutoDetectCookieSupport=1";
			}
			this._Context.Response.Cookies.Add(new HttpCookie("AspxAutoDetectCookieSupport", "1"));
			this._Context.Response.Redirect(redirectPath, true);
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x00101798 File Offset: 0x000FF998
		internal void RedirectWithDetectionIfRequired(string redirectPath, HttpCookieMode cookieMode)
		{
			this.Init();
			if (cookieMode != HttpCookieMode.AutoDetect)
			{
				return;
			}
			if (!this._Context.Request.Browser.Cookies || !this._Context.Request.Browser.SupportsRedirectWithCookie)
			{
				return;
			}
			string cookieValue = this.GetCookieValue('X');
			if (cookieValue != null && cookieValue == "1")
			{
				return;
			}
			string value = this._Context.Request.Headers["Cookie"];
			if (!string.IsNullOrEmpty(value))
			{
				return;
			}
			string text = this._Context.Request.QueryString["AspxAutoDetectCookieSupport"];
			if (text != null && text == "1")
			{
				this.SetCookieValue('X', "1");
				return;
			}
			this.RedirectWithDetection(redirectPath);
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x00101860 File Offset: 0x000FFA60
		private static bool IsValidHeader(string path, int startPos, int endPos)
		{
			if (endPos - startPos < 3)
			{
				return false;
			}
			while (startPos <= endPos - 3)
			{
				if (path[startPos] < 'A' || path[startPos] > 'Z')
				{
					return false;
				}
				if (path[startPos + 1] != '(')
				{
					return false;
				}
				startPos += 2;
				bool flag = false;
				while (startPos < endPos)
				{
					if (path[startPos] == ')')
					{
						startPos++;
						flag = true;
						break;
					}
					if (AppSettings.RestoreAggressiveCookielessPathRemoval)
					{
						if (path[startPos] == '/')
						{
							return false;
						}
					}
					else
					{
						char c = path[startPos];
						if ((c < 'A' || c > 'Z') && (c < 'a' || c > 'z') && (c < '0' || c > '9') && c != '-' && c != '_')
						{
							return false;
						}
					}
					startPos++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return startPos >= endPos;
		}

		// Token: 0x040028B8 RID: 10424
		internal const string COOKIELESS_SESSION_FILTER_HEADER = "AspFilterSessionId";

		// Token: 0x040028B9 RID: 10425
		private const string s_AutoDetectName = "AspxAutoDetectCookieSupport";

		// Token: 0x040028BA RID: 10426
		private const string s_AutoDetectValue = "1";

		// Token: 0x040028BB RID: 10427
		private HttpContext _Context;

		// Token: 0x040028BC RID: 10428
		private string _Headers;

		// Token: 0x040028BD RID: 10429
		private string _OriginalHeaders;
	}
}
