using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using ClockWorkLogger;
using ClockWorkWebAPI;
using ClockWorkWebAPI.AuthenticationAuthorization;
using ClockWorkWebAPI.Settings;
using ClockWorkWebAPI.Templates;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000007 RID: 7
	public class ClockWorkWebCore
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00003370 File Offset: 0x00001570
		public static int GetUrlVariableInt2(HttpRequest Request, string varName, string varNameCombined, int combinedIndex, bool encrypted, IEncryption tripleDES)
		{
			int num = ClockWorkWebCore.GetUrlVariableInt(Request, varName, encrypted, tripleDES);
			bool flag = num <= 0;
			if (flag)
			{
				object obj = Request.QueryString[varNameCombined];
				bool flag2 = obj != null;
				if (flag2)
				{
					string text = obj.ToString();
					string[] array = text.Split(new char[]
					{
						','
					});
					bool flag3 = combinedIndex < array.Length;
					if (flag3)
					{
						text = ClockWorkWebCore.DecodeUrlVariable(array[combinedIndex], encrypted, tripleDES);
					}
					else
					{
						bool flag4 = array.Length != 0;
						if (flag4)
						{
							text = ClockWorkWebCore.DecodeUrlVariable(array[0], encrypted, tripleDES);
						}
						else
						{
							text = "0";
						}
					}
					try
					{
						num = int.Parse(text);
					}
					catch
					{
						num = 0;
					}
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003444 File Offset: 0x00001644
		public static string GetUsersEmail(db conn, Cache Cache, int pid)
		{
			return ClockWorkWebCore.GetUsersEmail(Cache, pid);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003460 File Offset: 0x00001660
		public static string GetUsersEmail(Cache Cache, int pid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.GENERAL_EmailCid);
			bool settingValue2 = webSettingsClientManager.GetSettingValue<bool>(Setting.GENERAL_EmailEncrypted);
			string query = "SELECT controlvalue FROM otherinfops WHERE personid=@pid AND controlid=@cid";
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@cid", DbType.Int32, settingValue)
			};
			DataTable dataTable = clockWork.ExecuteQuery(query, parameters);
			bool flag = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
			string result;
			if (flag)
			{
				result = Core.BytesToString((byte[])dataTable.Rows[0][0], settingValue2, clockWork.Encryption);
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003544 File Offset: 0x00001744
		[Obsolete]
		public static int GetUrlVariableInt(HttpRequest Request, string varName, bool encrypted, IEncryption tripleDES)
		{
			object obj = Request.QueryString[varName];
			bool flag = obj != null;
			int result;
			if (flag)
			{
				string text = obj.ToString();
				text = ClockWorkWebCore.DecodeUrlVariable(text, encrypted, tripleDES);
				try
				{
					result = int.Parse(text);
				}
				catch
				{
					result = 0;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000035A8 File Offset: 0x000017A8
		[Obsolete]
		public static string GetUrlVariableString(HttpRequest Request, string varName, bool encrypted, IEncryption tripleDES)
		{
			object obj = Request.QueryString[varName];
			bool flag = obj != null;
			string result;
			if (flag)
			{
				string text = obj.ToString();
				text = ClockWorkWebCore.DecodeUrlVariable(text, encrypted, tripleDES);
				result = text;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000035EC File Offset: 0x000017EC
		[Obsolete]
		public static string EncodeUrlVariable(string varValue, bool encrypted)
		{
			return ClockWorkWebCore.EncodeUrlVariable(varValue, encrypted, DatabaseLayerFactory.ClockWork.Encryption);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003610 File Offset: 0x00001810
		[Obsolete]
		public static string EncodeUrlVariable(string varValue, bool encrypted, IEncryption tripleDES)
		{
			string result;
			if (encrypted)
			{
				string plainText = varValue + "~`~" + DateTime.Now.ToString("yyyyMMdd.h:mm.ss");
				result = ClockWorkWebCore.UrlEncodeByteArray(tripleDES.Encrypt(plainText));
			}
			else
			{
				result = varValue;
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003658 File Offset: 0x00001858
		[Obsolete]
		public static string UrlEncodeByteArray(byte[] bytes)
		{
			string str = ClockWorkWebCore.ByteArrayToHexString(bytes);
			return HttpUtility.UrlEncode(str);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003678 File Offset: 0x00001878
		[Obsolete]
		public static string DecodeUrlVariable(string varValue, bool encrypted)
		{
			return ClockWorkWebCore.DecodeUrlVariable(varValue, encrypted, DatabaseLayerFactory.ClockWork.Encryption);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000369C File Offset: 0x0000189C
		[Obsolete]
		public static string DecodeUrlVariable(string varValue, bool encrypted, IEncryption tripleDES)
		{
			string result;
			if (encrypted)
			{
				byte[] encryptedText = ClockWorkWebCore.UrlDecodeByteArray(varValue);
				string text = tripleDES.Decrypt(encryptedText);
				int num = text.IndexOf("~`~");
				bool flag = num == 0;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = num > 0;
					if (flag2)
					{
						result = text.Substring(0, num);
					}
					else
					{
						result = text;
					}
				}
			}
			else
			{
				result = varValue;
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003700 File Offset: 0x00001900
		public static bool IsStudentActivated(HttpSessionState Session, int pid, db conn)
		{
			bool flag = Session["activated"] != null;
			bool result;
			if (flag)
			{
				result = (bool)Session["activated"];
			}
			else
			{
				DateTime now = DateTime.Now;
				DateTime dateTime = (now.Month >= 5) ? new DateTime(now.Year, 5, 1) : new DateTime(now.Year - 1, 5, 1);
				DateTime dateTime2 = (now.Month >= 5) ? new DateTime(now.Year + 1, 4, 30) : new DateTime(now.Year, 4, 30);
				conn.Da.SelectCommand.CommandText = "SELECT personid FROM people WHERE personid=@pid AND dateadded>=@sd AND dateadded<=@ed UNION SELECT personid FROM peoplepreviousyears WHERE personid=@pid AND dateactive>=@sd AND dateactive<=@ed";
				conn.Da.SelectCommand.Parameters.Clear();
				conn.Da.SelectCommand.Parameters.AddWithValue("@pid", pid);
				conn.Da.SelectCommand.Parameters.AddWithValue("@sd", dateTime);
				conn.Da.SelectCommand.Parameters.AddWithValue("@ed", dateTime2);
				DataTable dataTable = new DataTable();
				conn.Da.Fill(dataTable);
				bool flag2 = dataTable.Rows.Count > 0;
				Session["activated"] = flag2;
				result = flag2;
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003868 File Offset: 0x00001A68
		public static void ClearAuthenticationInformationFromSession(HttpSessionState Session)
		{
			string[] array = new string[]
			{
				"authenticated",
				"username",
				"userinfo",
				"clockworkpid",
				"clockworknid",
				"clockworkiid"
			};
			foreach (string name in array)
			{
				bool flag = Session[name] != null;
				if (flag)
				{
					Session.Remove(name);
				}
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000038DC File Offset: 0x00001ADC
		public static void WarnUserBeforeSessionTimeout(HttpSessionState Session, Page Page, Type ThisDotGetType)
		{
			string text = "Warning: Within next 3 minutes, if you do not do anything,  our system will redirect to the login page. Please save changed data.";
			int num = Session.Timeout * 60000 - 180000;
			int num2 = Session.Timeout * 60000 - 5;
			string text2 = Page.ResolveClientUrl("~/custom/login/LoginS.aspx");
			string script = string.Concat(new string[]
			{
				"\r\n            var myTimeReminder, myTimeOut; \r\n            clearTimeout(myTimeReminder); \r\n            clearTimeout(myTimeOut); var sessionTimeReminder = ",
				num.ToString(),
				"; var sessionTimeout = ",
				num2.ToString(),
				";function doReminder(){ alert('",
				text,
				"'); }function doRedirect(){ window.location.href='",
				text2,
				"'; }\r\n            myTimeReminder=setTimeout('doReminder()', sessionTimeReminder); \r\n            myTimeOut=setTimeout('doRedirect()', sessionTimeout); "
			});
			ScriptManager.RegisterClientScriptBlock(Page, ThisDotGetType, "CheckSessionOut", script, true);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003980 File Offset: 0x00001B80
		[Obsolete]
		public static string GetUsersIpAddress(HttpRequest Request)
		{
			string text = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			bool flag = text == string.Empty;
			if (flag)
			{
				text = Request.ServerVariables["REMOTE_ADDR"];
			}
			return text;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000039C8 File Offset: 0x00001BC8
		[Obsolete]
		public static void NotAllowed(Setting setting, HttpSessionState Session, HttpResponse Response, HttpRequest Request)
		{
			UserInfo userInfo = ClockWorkWebCore.GetUserInfo(Session);
			string text = (userInfo == null) ? "ui is null" : string.Format("pid={0},iid={1},atlcontact={2},nid={3},display={4}", new object[]
			{
				userInfo.ClockworkPid.ToString(),
				userInfo.ClockworkIid.ToString(),
				userInfo.ClockworkAltContactId.ToString(),
				userInfo.ClockworkNid.ToString(),
				userInfo.DisplayName
			});
			CWLogger logger = CWLogger.Logger;
			string message = "NOTALLOWED:UserTurnedAwayBecauseNotAllowed:pid={0}:page={1}:code={2}";
			object arg = text;
			object arg2 = (Request == null) ? "NULLREQUEST" : Request.Url.ToString();
			int num = (int)setting;
			logger.Info(message, arg, arg2, num.ToString());
			ClockWorkWebCore.SetReturnUrl(Session, Response, Request);
			string str = "~/User/misc/NotAllowed.aspx?code=";
			num = (int)setting;
			Response.Redirect(str + num.ToString(), true);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003A98 File Offset: 0x00001C98
		[Obsolete]
		public static void NotAllowed(int setting, HttpSessionState Session, HttpResponse Response, HttpRequest Request)
		{
			UserInfo userInfo = ClockWorkWebCore.GetUserInfo(Session);
			string arg = (userInfo == null) ? "ui is null" : string.Format("pid={0},iid={1},atlcontact={2},nid={3},display={4}", new object[]
			{
				userInfo.ClockworkPid.ToString(),
				userInfo.ClockworkIid.ToString(),
				userInfo.ClockworkAltContactId.ToString(),
				userInfo.ClockworkNid.ToString(),
				userInfo.DisplayName
			});
			CWLogger.Logger.Info("NOTALLOWED:UserTurnedAwayBecauseNotAllowed:pid={0}:page={1}:code={2}", arg, (Request == null) ? "NULLREQUEST" : Request.Url.ToString(), setting.ToString());
			ClockWorkWebCore.SetReturnUrl(Session, Response, Request);
			Response.Redirect("~/User/misc/NotAllowed.aspx?code=" + setting.ToString(), true);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003B64 File Offset: 0x00001D64
		[Obsolete]
		public static void ReturnToSender(HttpSessionState Session, HttpResponse Response)
		{
			string text = ClockWorkWebCore.GetReturnUrl(Session);
			bool flag = text.Length <= 0;
			if (flag)
			{
				text = "~/custom/misc/home.aspx";
			}
			Response.Redirect(text, true);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003B9A File Offset: 0x00001D9A
		[Obsolete]
		public static void SetReturnUrl(HttpSessionState Session, HttpResponse Response, HttpRequest Request)
		{
			ClockWorkWebCore.SetReturnUrl(Session, Request.Url.ToString());
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003BB0 File Offset: 0x00001DB0
		[Obsolete]
		public static void SetReturnUrl(HttpSessionState Session, string url)
		{
			bool flag = url.ToLower().IndexOf("login.aspx") < 0;
			if (flag)
			{
				Session["gotourl"] = url;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003BE4 File Offset: 0x00001DE4
		private static string GetUrlFirstLevelFolder(string url)
		{
			int num = url.LastIndexOf('/');
			bool flag = num >= 0;
			string result;
			if (flag)
			{
				string text = url.Substring(0, num);
				num = text.LastIndexOf('/');
				bool flag2 = num >= 0;
				if (flag2)
				{
					result = text.Substring(num + 1);
				}
				else
				{
					result = text;
				}
			}
			else
			{
				result = url;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003C40 File Offset: 0x00001E40
		[Obsolete]
		public static string GotoLastReturnUrl2(HttpSessionState Session, HttpResponse response, string FolderEnforce, string defaultUrl)
		{
			string returnUrl = ClockWorkWebCore.GetReturnUrl(Session);
			bool flag = string.IsNullOrEmpty(returnUrl);
			string result;
			if (flag)
			{
				response.Redirect(defaultUrl, true);
				result = defaultUrl;
			}
			else
			{
				bool flag2 = returnUrl.IndexOf(FolderEnforce, StringComparison.OrdinalIgnoreCase) >= 0;
				if (flag2)
				{
					response.Redirect(returnUrl);
					result = returnUrl;
				}
				else
				{
					response.Redirect(defaultUrl, true);
					result = defaultUrl;
				}
			}
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003C9C File Offset: 0x00001E9C
		[Obsolete]
		public static void SetReturnUrl2(HttpSessionState Session, string url)
		{
			string returnUrl = ClockWorkWebCore.GetReturnUrl(Session);
			bool flag = url.ToLower().IndexOf("login.aspx") < 0;
			if (flag)
			{
				string text = ClockWorkWebCore.GetUrlFirstLevelFolder(returnUrl).ToLower();
				string value = ClockWorkWebCore.GetUrlFirstLevelFolder(url).ToLower();
				bool flag2 = !text.Equals(value);
				if (flag2)
				{
					Session["gotourl"] = url;
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003D04 File Offset: 0x00001F04
		public static string GetReturnUrl(HttpSessionState Session)
		{
			object obj = Session["gotourl"];
			return (obj == null) ? "" : ((string)obj);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003D34 File Offset: 0x00001F34
		private static int StringToIntSafe(string s, int defaultInt)
		{
			bool flag = s.Trim().Length < 1;
			int result;
			if (flag)
			{
				result = defaultInt;
			}
			else
			{
				try
				{
					result = int.Parse(s);
				}
				catch
				{
					result = defaultInt;
				}
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003D7C File Offset: 0x00001F7C
		[Obsolete]
		public static string GetCurrentFullUrl(HttpRequest Request)
		{
			return Request.Url.AbsoluteUri;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003D9C File Offset: 0x00001F9C
		[Obsolete]
		public static void EnsurePageNotCached(HttpResponse Response)
		{
			Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1.0));
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
			Response.Cache.SetNoStore();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public static string ForceLogoutUrl
		{
			get
			{
				string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("forcelogoutlink");
				return string.IsNullOrEmpty(appSettingsByNameUsingProtection) ? "" : appSettingsByNameUsingProtection;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003E18 File Offset: 0x00002018
		public static void DisableNoCache(MasterPage Master)
		{
			bool flag = Master is ClockWorkMasterPage;
			if (flag)
			{
				ClockWorkMasterPage clockWorkMasterPage = (ClockWorkMasterPage)Master;
				clockWorkMasterPage.DisableNoCache = true;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003E44 File Offset: 0x00002044
		private static string GetCurrentDirectory(HttpRequest Request)
		{
			string text = Request.Url.ToString();
			int length = text.LastIndexOf('/');
			return text.Substring(0, length);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003E74 File Offset: 0x00002074
		private static string GetCurrentUrl(HttpRequest Request)
		{
			return Request.Url.ToString();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003E93 File Offset: 0x00002093
		[Obsolete]
		public static void Logout(Cache cache, db conn, HttpSessionState Session, HttpResponse Response, HttpRequest Request)
		{
			ClockWorkWebCore.Logout(cache, Session, Response, Request, true);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003EA4 File Offset: 0x000020A4
		[Obsolete]
		public static void Logout(Cache cache, HttpSessionState Session, HttpResponse Response, HttpRequest Request, bool redirect)
		{
			Session.Remove("authenticated");
			Session.Remove("username");
			Session.Remove("userinfo");
			Session.Remove("pid");
			Session.Clear();
			string forceLogoutUrl = ClockWorkWebCore.ForceLogoutUrl;
			if (redirect)
			{
				bool flag = !string.IsNullOrEmpty(forceLogoutUrl);
				if (flag)
				{
					bool flag2 = forceLogoutUrl.Equals("javascript: self.close()");
					if (flag2)
					{
						Response.Write("<script language='javascript'> { window.close();}</script>");
					}
					else
					{
						Response.Redirect(forceLogoutUrl, true);
					}
				}
				else
				{
					string text = ClockWorkWebCore.GetCurrentDirectory(Request);
					text += "/default.aspx";
					Response.Redirect(text, true);
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003F50 File Offset: 0x00002150
		[Obsolete("Use WebAuthenticationAuthorizationWebClientManager.CurrentInstance instead")]
		public static string GetAuthenticatedUsername(HttpSessionState Session)
		{
			object obj = Session["authenticated"];
			bool flag = obj != null && obj is bool;
			string result;
			if (flag)
			{
				result = ((Session["username"] == null) ? "" : ((string)Session["username"]));
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003FB0 File Offset: 0x000021B0
		public static string GetContactUsString(db conn, Cache cache)
		{
			string settingValueString = AppSettingsV2.GetSettingValueString(Setting.INSTRUCTOR_contactInfo, conn, cache);
			bool flag = settingValueString.Length > 0;
			string result;
			if (flag)
			{
				result = "contact us at " + settingValueString;
			}
			else
			{
				result = "contact us";
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003FF0 File Offset: 0x000021F0
		private static Person FindInstructor(Cache cache, db conn, UserInfo userInfo)
		{
			string username = userInfo.Username;
			string text = "";
			string student_no = "";
			string name = "";
			int num = 0;
			string settingValueString = AppSettingsV2.GetSettingValueString(Setting.GENERAL_EmailSuffix, conn, cache);
			string settingValueString2 = AppSettingsV2.GetSettingValueString(Setting.GENERAL_EmailSuffix2, conn, cache);
			for (int i = 0; i < 2; i++)
			{
				int num2 = i;
				int num3 = num2;
				if (num3 != 0)
				{
					if (num3 == 1)
					{
						text = username + settingValueString2;
					}
				}
				else
				{
					text = username + settingValueString;
				}
				text = text.ToLower();
				conn.Da.SelectCommand.CommandText = "SELECT lucd.lucoursedataid AS instructorid,lucd.altlookupstring,lucd.lookupstring,lucd.email FROM lucoursedata lucd WHERE lucd.lookuplisttype=1 AND lucd.email=@email";
				conn.Da.SelectCommand.Parameters.Clear();
				conn.Da.SelectCommand.Parameters.AddWithValue("@email", text);
				DataTable dataTable = new DataTable();
				conn.Da.Fill(dataTable);
				bool flag = dataTable.Rows.Count > 0;
				if (flag)
				{
					DataRow dataRow = dataTable.Rows[0];
					num = (int)dataRow[0];
					name = dataRow["altlookupstring"].ToString();
					text = dataRow["email"].ToString();
					break;
				}
			}
			bool flag2 = num > 0;
			Person result;
			if (flag2)
			{
				Person person = new Person(num, name, text, student_no);
				result = person;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004164 File Offset: 0x00002364
		public static string UrlVariable_GetUrlString(db conn, int var)
		{
			double totalSeconds = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
			byte[] bytes = conn.TripleDES.Encrypt(var.ToString() + "_" + totalSeconds.ToString());
			return ClockWorkWebCore.ByteArrayToHexString(bytes);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000041C4 File Offset: 0x000023C4
		public static int UrlVariable_GetIntVariable(db conn, string urlVal)
		{
			byte[] encryptedText = ClockWorkWebCore.HexStringToByteArray(urlVal);
			string text = conn.TripleDES.Decrypt(encryptedText);
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
			return 0;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000422C File Offset: 0x0000242C
		public static UserInfo GetUserInfoAfterAuthenticated(db conn, string username, Cache cache)
		{
			string settingValueString = AppSettingsV2.GetSettingValueString(Setting.LOGIN_UsernameType, conn, cache);
			bool flag = settingValueString.CompareTo("email") == 0;
			string email;
			if (flag)
			{
				string settingValueString2 = AppSettingsV2.GetSettingValueString(Setting.GENERAL_EmailSuffix, conn, cache);
				bool flag2 = username.ToLower().IndexOf(settingValueString2.ToLower()) <= 0;
				if (flag2)
				{
					email = username + settingValueString2;
				}
				else
				{
					email = username;
				}
			}
			else
			{
				email = "";
			}
			return new UserInfo(username, username, email, new GroupMembership[1]);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000042B4 File Offset: 0x000024B4
		public static UserInfo GetUserInfo(HttpSessionState Session)
		{
			object obj = Session["userinfo"];
			bool flag = obj != null;
			UserInfo result;
			if (flag)
			{
				result = (UserInfo)obj;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000042E4 File Offset: 0x000024E4
		public static byte[] UrlDecodeByteArray(string s)
		{
			string hex = HttpUtility.UrlDecode(s);
			return ClockWorkWebCore.HexStringToByteArray(hex);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004308 File Offset: 0x00002508
		public static string ByteArrayToHexString(byte[] Bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "0123456789ABCDEF";
			foreach (byte b in Bytes)
			{
				stringBuilder.Append(text[b >> 4]);
				stringBuilder.Append(text[(int)(b & 15)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000436C File Offset: 0x0000256C
		public static bool IsUserAuthenticated(HttpSessionState session, Page page)
		{
			return session["authenticated"] != null && (bool)session["authenticated"];
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000043A0 File Offset: 0x000025A0
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
				while (i < Hex.Length - 1)
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

		// Token: 0x0600005F RID: 95 RVA: 0x00004440 File Offset: 0x00002640
		public static void GoHome(NameValueCollection appSettings, HttpSessionState Session, HttpRequest Request, HttpResponse Response)
		{
			string url = "~/custom/misc/home.aspx";
			Response.Redirect(url, true);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004460 File Offset: 0x00002660
		public static void GoHome(HttpSessionState Session, HttpRequest Request, HttpResponse Response)
		{
			string url = "~/custom/misc/home.aspx";
			Response.Redirect(url, true);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004480 File Offset: 0x00002680
		public static string GetUpcomingMasterPageFile(HttpSessionState Session)
		{
			object obj = Session["UpcomingAppsMasterPage"];
			bool flag = obj != null && obj is string;
			if (flag)
			{
				string text = (string)obj;
				bool flag2 = text.Length > 0;
				if (flag2)
				{
					return text.Trim();
				}
			}
			return "";
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000044D9 File Offset: 0x000026D9
		public static void SetUpcomingMasterPageFile(HttpSessionState Session, string masterPageFile)
		{
			Session["UpcomingAppsMasterPage"] = masterPageFile;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000044EC File Offset: 0x000026EC
		public static void SetFocus(Control control)
		{
			bool flag = control == null;
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("\r\n<script language='JavaScript'>\r\n");
				stringBuilder.Append("<!--\r\n");
				stringBuilder.Append("function SetFocus()\r\n");
				stringBuilder.Append("{\r\n");
				stringBuilder.Append("try {");
				stringBuilder.Append("\tdocument.");
				Control parent = control.Parent;
				while (!(parent is HtmlForm))
				{
					parent = parent.Parent;
				}
				stringBuilder.Append(parent.ClientID);
				stringBuilder.Append("['");
				stringBuilder.Append(control.UniqueID);
				bool flag2 = control is CheckBoxList || control is RadioButtonList;
				if (flag2)
				{
					stringBuilder.Append("_0");
				}
				stringBuilder.Append("'].focus();\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("catch ( e ) {\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("}\r\n");
				stringBuilder.Append("window.onload = SetFocus;\r\n");
				stringBuilder.Append("// -->\r\n");
				stringBuilder.Append("</script>");
				control.Page.RegisterClientScriptBlock("SetFocus", stringBuilder.ToString());
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004638 File Offset: 0x00002838
		public static EmailTemplate CreateEmailTemplate(db conn, Cache cache, string language, Setting emailSetting)
		{
			string settingValueString = AppSettingsV2.GetSettingValueString(emailSetting, conn, cache);
			return ClockWorkWebCore.CreateEmailTemplate(settingValueString, cache, language);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000465C File Offset: 0x0000285C
		public static EmailTemplate CreateEmailTemplate(string emailXml, Cache cache, string language)
		{
			string strB = (language == null || language.Length < 1) ? "EN" : language;
			bool flag = emailXml.Length > 0;
			EmailTemplate result;
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(emailXml);
				XmlNode xmlNode = xmlDocument.ChildNodes[0];
				foreach (object obj in xmlNode.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					XmlElement xmlElement = xmlNode2["language"];
					string text = (((xmlElement != null) ? xmlElement.InnerText : null) ?? "").Trim();
					bool flag2 = text.CompareTo(strB) == 0;
					if (flag2)
					{
						string innerText = xmlNode2["from"].InnerText;
						string innerText2 = xmlNode2["to"].InnerText;
						string innerText3 = xmlNode2["cc"].InnerText;
						string innerText4 = xmlNode2["bcc"].InnerText;
						string innerText5 = xmlNode2["subject"].InnerText;
						string innerText6 = xmlNode2["attachment"].InnerText;
						string innerText7 = xmlNode2["body"].InnerText;
						return new EmailTemplate(innerText2, innerText, innerText3, innerText4, innerText6, innerText5, innerText7);
					}
				}
				result = null;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
