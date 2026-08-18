using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Authentication;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;
using TechnoPro.Common.UI.Web.Entity.AuthenticationAuthorization;
using TechnoPro.Common.UI.Web.Entity.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x02000010 RID: 16
	public class NavigatorClientManager : INavigatorClientManager
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003180 File Offset: 0x00001380
		public static NavigatorClientManager CurrentInstance
		{
			get
			{
				NavigatorClientManager result;
				if ((result = NavigatorClientManager._instance) == null)
				{
					result = (NavigatorClientManager._instance = new NavigatorClientManager());
				}
				return result;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000031A6 File Offset: 0x000013A6
		public NavigatorClientManager()
		{
			this.encryption = DatabaseLayerFactory.ClockWork.Encryption;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000031C0 File Offset: 0x000013C0
		private Uri GetLastReturnUri()
		{
			HttpContext httpContext = HttpContext.Current;
			HttpSessionState session = httpContext.Session;
			object obj = session["gotourl"];
			string text = (obj == null) ? "" : (((string)obj) ?? "").Trim();
			try
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					return new Uri(text);
				}
			}
			catch (Exception ex)
			{
			}
			obj = session["gotoUri"];
			bool flag2 = obj != null && obj is Uri;
			Uri result;
			if (flag2)
			{
				result = (Uri)obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000326C File Offset: 0x0000146C
		public Uri GetCurrentUri()
		{
			return HttpContext.Current.Request.Url;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003290 File Offset: 0x00001490
		public string GetStringFromUrlParameter(string urlParameterName)
		{
			HttpRequest request = HttpContext.Current.Request;
			string s = request.QueryString[urlParameterName] ?? "";
			byte[] encryptedText = Convert.FromBase64String(s);
			string text = this.encryption.Decrypt(encryptedText);
			int num = text.IndexOf("`");
			bool flag = num <= 0;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text2 = text.Substring(0, num);
				string s2 = text.Substring(num + 1);
				DateTime d;
				bool flag2 = !DateTime.TryParse(s2, out d);
				if (flag2)
				{
					result = string.Empty;
				}
				else
				{
					bool flag3 = (DateTime.Now - d).TotalDays <= 2.0;
					if (flag3)
					{
						result = (text2 ?? "");
					}
					else
					{
						result = string.Empty;
					}
				}
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000336C File Offset: 0x0000156C
		public string GetUrlParameterFromString(string s)
		{
			string arg = DateTime.Now.ToString("yyyy-MM-dd H:mm");
			string plainText = string.Format("{0}`{1}", s, arg);
			byte[] inArray = this.encryption.Encrypt(plainText);
			string str = Convert.ToBase64String(inArray);
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000033BC File Offset: 0x000015BC
		public int GetIntFromUrlParameter(string s)
		{
			return this.ConvertUrlStringToIntParameter(s);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000033D8 File Offset: 0x000015D8
		public string GetUrlParameterFromString(int num)
		{
			return this.ConvertIntParameterToUrlString(num);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000033F4 File Offset: 0x000015F4
		public string ConvertIntParameterToUrlString(int parameter)
		{
			string arg = DateTime.Now.ToString("yyyy-MM-dd H:mm");
			string plainText = string.Format("{0}`{1}", parameter.ToString(), arg);
			byte[] inArray = this.encryption.Encrypt(plainText);
			string str = Convert.ToBase64String(inArray);
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003450 File Offset: 0x00001650
		public string ConvertIntParameterToLongtermUrlString(int parameter)
		{
			string arg = DateTime.Now.ToString("yyyy-MM-dd H:mm");
			string plainText = string.Format("{0}`{1}`{2}", parameter.ToString(), arg, "LNG");
			byte[] inArray = DatabaseLayerFactory.ClockWork.Encryption.Encrypt(plainText);
			string str = Convert.ToBase64String(inArray);
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000034B4 File Offset: 0x000016B4
		public int ConvertUrlStringToIntParameter(string urlParameter)
		{
			bool flag;
			return this.ConvertUrlStringToIntParameter(urlParameter, out flag);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000034D0 File Offset: 0x000016D0
		public int ConvertUrlStringToIntParameter(string urlParameter, out bool wasLongTermUrl)
		{
			wasLongTermUrl = false;
			bool flag = string.IsNullOrEmpty(urlParameter);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				urlParameter = urlParameter.Replace(" ", "+");
				try
				{
					byte[] encryptedText;
					try
					{
						encryptedText = Convert.FromBase64String(urlParameter);
					}
					catch
					{
						urlParameter = HttpUtility.UrlDecode(urlParameter);
						encryptedText = Convert.FromBase64String(urlParameter);
					}
					string text = this.encryption.Decrypt(encryptedText);
					CWLogger.Logger.Debug("NavigatorClientManager:ConvertUrlStringToIntParameter:plainText={0}", text ?? "NULL");
					int num = text.IndexOf("`");
					string[] array = text.Split(new char[]
					{
						'`'
					});
					bool flag2 = num <= 0 || array.Length < 2;
					if (flag2)
					{
						return this.UrlVariable_GetIntVariable(urlParameter);
					}
					string s = array[0].Trim();
					string s2 = array[1].Trim();
					string text2 = (array.Length > 2) ? array[2].Trim().ToLower() : "";
					wasLongTermUrl = (text2.Length > 0);
					DateTime d;
					int num2;
					bool flag3 = !DateTime.TryParse(s2, out d) || !int.TryParse(s, out num2);
					if (flag3)
					{
						return this.UrlVariable_GetIntVariable(urlParameter);
					}
					TimeSpan timeSpan = DateTime.Now - d;
					double totalDays = timeSpan.TotalDays;
					bool flag4 = totalDays > 2.0;
					if (flag4)
					{
						CWLogger.Logger.Warn("NavigatorClientManager:ConvertUrlStringToIntParameter:ParameterIsOlderThan2Days:ageInDays={0}:lng={1}", totalDays.ToString(), text2);
						bool flag5 = text2 != "lng";
						if (flag5)
						{
							return 0;
						}
					}
					return (timeSpan.TotalDays <= 365.0) ? num2 : 0;
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Debug("NavigatorClientManager:ConvertUrlStringToIntParameter:Error={0}", ex.ToString());
				}
				result = this.UrlVariable_GetIntVariable(urlParameter);
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000036CC File Offset: 0x000018CC
		public string GetStudentUrlWithParameters(string url, Dictionary<string, int> args)
		{
			StringBuilder stringBuilder = new StringBuilder(url + "?");
			int num = 0;
			foreach (string text in args.Keys)
			{
				string str = this.ConvertIntParameterToUrlString(args[text]);
				bool flag = num > 0;
				if (flag)
				{
					stringBuilder.Append("&");
				}
				stringBuilder.Append(text + "=" + str);
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000377C File Offset: 0x0000197C
		public string GetStudentUrlWithIntParameter(string url, string pname, int pvalue)
		{
			string text = this.ConvertIntParameterToUrlString(pvalue);
			return string.Concat(new string[]
			{
				url,
				"?",
				pname,
				"=",
				text
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000037C0 File Offset: 0x000019C0
		public void SetReturnUrlSpecific(string relativeUrl)
		{
			try
			{
				string baseUrlNoUserCustomStaffAdmin = this.GetBaseUrlNoUserCustomStaffAdmin();
				Uri value = new Uri(baseUrlNoUserCustomStaffAdmin + relativeUrl);
				HttpContext httpContext = HttpContext.Current;
				HttpSessionState session = httpContext.Session;
				session.Remove("gotourl");
				session.Remove("gotoUri");
				session.Add("gotoUri", value);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("NavigationClientManager:SetReturnUrlSpecific:relativeUrl={0}:err={1}", relativeUrl ?? "NULL", ex.ToString());
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003850 File Offset: 0x00001A50
		private string GetBaseUrlNoUserCustomStaffAdmin()
		{
			Uri url = HttpContext.Current.Request.Url;
			string text = url.ToString();
			string[] array = new string[]
			{
				"/user/",
				"/custom/",
				"/staff/",
				"/admin/"
			};
			foreach (string value in array)
			{
				int num = text.IndexOf(value, StringComparison.OrdinalIgnoreCase);
				bool flag = num < 0;
				if (!flag)
				{
					return text.Substring(0, num);
				}
			}
			string absolutePath = url.AbsolutePath;
			int length = absolutePath.IndexOf("/", absolutePath.StartsWith("/") ? 1 : 0, StringComparison.Ordinal);
			return url.Scheme + "://" + url.Authority + absolutePath.Substring(0, length);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000392C File Offset: 0x00001B2C
		private string GetBaseUrl()
		{
			Uri url = HttpContext.Current.Request.Url;
			string text = url.ToString();
			string[] array = new string[]
			{
				"/user/",
				"/custom/",
				"/staff/",
				"/admin/"
			};
			foreach (string text2 in array)
			{
				int num = text.IndexOf(text2, StringComparison.OrdinalIgnoreCase);
				bool flag = num < 0;
				if (!flag)
				{
					return text.Substring(0, num + text2.Length - 1);
				}
			}
			string absolutePath = url.AbsolutePath;
			int length = absolutePath.IndexOf("/", absolutePath.StartsWith("/") ? 1 : 0, StringComparison.Ordinal);
			return url.Scheme + "://" + url.Authority + absolutePath.Substring(0, length);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003A14 File Offset: 0x00001C14
		public void SetReturnUrl()
		{
			try
			{
				Uri currentUri = this.GetCurrentUri();
				HttpContext httpContext = HttpContext.Current;
				HttpSessionState session = httpContext.Session;
				session.Remove("gotourl");
				session.Remove("gotoUri");
				session.Add("gotoUri", currentUri);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("NavigationClientManager:SetReturnUrl:err={0}", ex.ToString());
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003A8C File Offset: 0x00001C8C
		public void GotoLastReturnUrl()
		{
			string lastReturnUrl = this.GetLastReturnUrl("~/custom/misc/home.aspx");
			bool flag = !string.IsNullOrEmpty(lastReturnUrl);
			if (flag)
			{
				try
				{
					HttpContext.Current.Response.Redirect(lastReturnUrl, true);
				}
				catch
				{
				}
			}
			else
			{
				this.GotoHomePage();
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003AEC File Offset: 0x00001CEC
		public void GotoLastReturnUrl(string folderEnforce, string defaultPage)
		{
			string lastReturnUrl = this.GetLastReturnUrl(folderEnforce, defaultPage);
			bool flag = !string.IsNullOrEmpty(lastReturnUrl);
			if (flag)
			{
				try
				{
					HttpContext.Current.Response.Redirect(lastReturnUrl, false);
					HttpContext.Current.ApplicationInstance.CompleteRequest();
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("GotoLastReturnUrl:folderEnforce={0}:DefaultPage={1}:error={2}", folderEnforce ?? "NULL", defaultPage ?? "NULL", ex.ToString());
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003B78 File Offset: 0x00001D78
		public string GetLastReturnUrl(string defaultUrl)
		{
			Uri lastReturnUri = this.GetLastReturnUri();
			bool flag = lastReturnUri != null && lastReturnUri.ToString().Trim().Length >= 1;
			string result;
			if (flag)
			{
				result = lastReturnUri.ToString();
			}
			else
			{
				result = defaultUrl;
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public string GetLastReturnUrl(string folderEnforce, string defaultPage)
		{
			Uri lastReturnUri = this.GetLastReturnUri();
			bool flag = lastReturnUri != null && lastReturnUri.ToString().Trim().Length >= 1;
			string result;
			if (flag)
			{
				result = lastReturnUri.ToString();
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(defaultPage);
				if (flag2)
				{
					defaultPage = "Default.aspx";
				}
				string text = string.IsNullOrEmpty(folderEnforce) ? defaultPage : (folderEnforce + "/" + defaultPage);
				result = (text ?? lastReturnUri.ToString());
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C44 File Offset: 0x00001E44
		public void NotAllowed(eNotAllowedCode notAllowedCode, IDictionary<string, string> args, object currentPageObj)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			HttpResponse response = httpContext.Response;
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(currentPageObj);
			string text = (currentClockWorkIdentity == null) ? "identity is null" : string.Format("pid={0},iid={1},atlcontact={2},nid={3},display={4}", new object[]
			{
				currentClockWorkIdentity.PersonId.ToString(),
				currentClockWorkIdentity.InstructorId.ToString(),
				currentClockWorkIdentity.AlternateContactId.ToString(),
				currentClockWorkIdentity.NotetakerId.ToString(),
				currentClockWorkIdentity.UserName ?? ""
			});
			CWLogger logger = CWLogger.Logger;
			string message = "NOTALLOWED:UserTurnedAwayBecauseNotAllowed:pid={0}:page={1}:notAllowedCode={2}:args={3}";
			object arg = text;
			object arg2 = (request == null) ? "NULLREQUEST" : request.Url.ToString();
			object arg3;
			if (args != null)
			{
				arg3 = string.Join(",", (from g in args
				select g.Key + "=" + (g.Value ?? "NULL")).ToArray<string>());
			}
			else
			{
				arg3 = "NULL";
			}
			logger.Info(message, arg, arg2, arg3);
			this.SetReturnUrl();
			HttpResponse httpResponse = response;
			string str = "~/User/misc/NotAllowed.aspx?notAllowedCode=";
			int num = (int)notAllowedCode;
			string str2 = num.ToString();
			string str3 = (args != null && args.Count > 0) ? "&" : "";
			string str4;
			if (args == null || args.Count <= 0)
			{
				str4 = "";
			}
			else
			{
				str4 = string.Join("&", (from g in args
				select g.Key + "=" + (g.Value ?? "")).ToArray<string>());
			}
			httpResponse.Redirect(str + str2 + str3 + str4, true);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public void NotAllowed(Setting setting, object currentPageObj)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest request = httpContext.Request;
			HttpResponse response = httpContext.Response;
			IWebAuthenticationAuthorizationWebClientManager webAuthenticationAuthorizationWebClientManager = new WebAuthenticationAuthorizationWebClientManager();
			ClockWorkIdentity currentClockWorkIdentity = webAuthenticationAuthorizationWebClientManager.GetCurrentClockWorkIdentity(currentPageObj);
			string arg = (currentClockWorkIdentity == null) ? "ui is null" : string.Format("pid={0},iid={1},atlcontact={2},nid={3},display={4}", new object[]
			{
				currentClockWorkIdentity.PersonId.ToString(),
				currentClockWorkIdentity.InstructorId.ToString(),
				currentClockWorkIdentity.AlternateContactId.ToString(),
				currentClockWorkIdentity.NotetakerId.ToString(),
				currentClockWorkIdentity.UserName ?? ""
			});
			CWLogger.Logger.Info("NOTALLOWED:UserTurnedAwayBecauseNotAllowed:pid={0}:page={1}:code={2}", arg, (request == null) ? "NULLREQUEST" : request.Url.ToString(), setting.ToString());
			this.SetReturnUrl();
			response.Redirect("~/User/misc/NotAllowed.aspx?code=" + setting.ToString(), true);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003EE0 File Offset: 0x000020E0
		public void GotoModuleNotLicensedWarningPage(TechnoPro.Common.Public.Entities.Settings.Group Group)
		{
			string baseUrl = this.GetBaseUrl();
			string url = baseUrl + "/misc/licensing.aspx";
			HttpContext httpContext = HttpContext.Current;
			HttpResponse response = httpContext.Response;
			response.Redirect(url, true);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003F18 File Offset: 0x00002118
		public void EnsurePageNotCached()
		{
			HttpResponse response = HttpContext.Current.Response;
			response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1.0));
			response.Cache.SetCacheability(HttpCacheability.NoCache);
			response.Cache.SetNoStore();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003F6C File Offset: 0x0000216C
		public void GotoHomePage()
		{
			HttpContext.Current.Response.Redirect("~/custom/misc/home.aspx", true);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003F88 File Offset: 0x00002188
		private int UrlVariable_GetIntVariable(string urlVal)
		{
			try
			{
				byte[] encryptedText = NavigatorClientManager.HexStringToByteArray(urlVal);
				string text = this.encryption.Decrypt(encryptedText);
				int num = text.IndexOf('_');
				bool flag = num > 0;
				if (flag)
				{
					text = text.Substring(0, num);
					try
					{
						return int.Parse(text);
					}
					catch
					{
						return 0;
					}
				}
			}
			catch
			{
			}
			return 0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004004 File Offset: 0x00002204
		public static byte[] HexStringToByteArray(string Hex)
		{
			byte[] result;
			try
			{
				byte[] array = new byte[Hex.Length / 2];
				int[] array2 = new int[]
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					7,
					8,
					9,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					10,
					11,
					12,
					13,
					14,
					15
				};
				int num = 0;
				int i = 0;
				while (i < Hex.Length)
				{
					array[num] = (byte)(array2[(int)(char.ToUpper(Hex[i]) - '0')] << 4 | array2[(int)(char.ToUpper(Hex[i + 1]) - '0')]);
					i += 2;
					num++;
				}
				result = array;
			}
			catch
			{
				result = new byte[0];
			}
			return result;
		}

		// Token: 0x04000012 RID: 18
		private static NavigatorClientManager _instance;

		// Token: 0x04000013 RID: 19
		private IEncryption encryption;
	}
}
