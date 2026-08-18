using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using ClockWorkLogger;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.AuthenticationCAS
{
	// Token: 0x02000002 RID: 2
	public class CASAuthManager : ICASAuthManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CASAuthManager()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public CASAuthManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206C File Offset: 0x0000026C
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public OperationContext OpContext { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x00002080 File Offset: 0x00000280
		[Obsolete("Use AuthenticateCAS(ticket,Args) instead; args should include 'baseweburl'")]
		public CASAuthenticationResult AuthenticateCAS(string ticket)
		{
			string text = string.Empty;
			string text2 = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.LOGIN_CollectCredentialsUrl);
			int num = text2.IndexOf("?");
			bool flag = num > 0;
			if (flag)
			{
				text2 = text2.Substring(num + 1);
				string[] array = text2.Split(new char[]
				{
					'&'
				});
				foreach (string text3 in array)
				{
					num = text3.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						string text4 = text3.Substring(0, num);
						bool flag3 = text4.Equals("service", StringComparison.OrdinalIgnoreCase);
						if (flag3)
						{
							text = text3.Substring(num + 1);
							break;
						}
					}
				}
			}
			bool flag4 = text.Length < 1;
			if (flag4)
			{
				text = "/custom/login/logins.aspx";
				CWLogger.Logger.Warn("CASAuthManager:AuthenticateCAS:Can't find returnUrl:LOGIN_CollectCredentialsUrl={0}", text2 ?? "NULL");
			}
			CASAuthenticationOptions authenticationOptions = new CASAuthenticationOptions
			{
				CASLoginUrl = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.LOGIN_CollectCredentialsUrl),
				CASServiceValidateUrl = SettingManager.CurrentInstance.GetSettingValue<string>(Setting.LOGIN_CAS_ServiceValidateUrl),
				ClockWorkLoginSuccessUrl = text
			};
			return this.AuthenticateCAS(authenticationOptions, ticket);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021B8 File Offset: 0x000003B8
		public CASAuthenticationResult AuthenticateCAS(CASAuthenticationOptions AuthenticationOptions, string ticket)
		{
			string text = AuthenticationOptions.CASServiceValidateUrl.Contains("?") ? string.Format("{0}&service={1}&ticket={2}", AuthenticationOptions.CASServiceValidateUrl, AuthenticationOptions.ClockWorkLoginSuccessUrl, ticket) : string.Format("{0}?service={1}&ticket={2}", AuthenticationOptions.CASServiceValidateUrl, AuthenticationOptions.ClockWorkLoginSuccessUrl, ticket);
			WebRequest webRequest = WebRequest.Create(text);
			webRequest.Method = "GET";
			webRequest.ContentType = "text/xml";
			webRequest.ContentLength = 0L;
			string text2;
			using (WebResponse response = webRequest.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.Default))
				{
					text2 = streamReader.ReadToEnd().Trim();
					streamReader.Close();
					response.Close();
				}
			}
			bool flag = false;
			string text3 = "";
			string text4 = "";
			bool flag2 = string.IsNullOrEmpty(text2);
			if (flag2)
			{
				CWLogger.Logger.Warn("CASAuthManager:AuthenticateCAS:Fail:WebServiceValidateCallReturnedEmptyString:url={0}", text ?? "NULL");
			}
			else
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(text2);
					XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("cas:authenticationFailure");
					XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("cas:authenticationSuccess");
					bool flag3 = elementsByTagName != null && elementsByTagName.Count > 0;
					if (flag3)
					{
						XmlNode xmlNode = elementsByTagName[0];
						string innerText = xmlNode.InnerText;
						bool flag4 = xmlNode.Attributes != null && xmlNode.Attributes.Count > 0;
						if (flag4)
						{
							XmlAttribute xmlAttribute = xmlNode.Attributes["code"];
							CWLogger.Logger.Info("CASAuthManager:AuthenticateCAS:FailedLogin:code={0}:msg={1}:url={2}", (xmlAttribute == null) ? "NULL" : (xmlAttribute.Value ?? "NULLVALUE"), innerText, text ?? "NULL");
						}
					}
					else
					{
						bool flag5 = elementsByTagName2 != null && elementsByTagName2.Count > 0;
						if (flag5)
						{
							XmlNode xmlNode2 = elementsByTagName2[0];
							bool hasChildNodes = xmlNode2.HasChildNodes;
							if (hasChildNodes)
							{
								foreach (object obj in xmlNode2.ChildNodes)
								{
									XmlNode xmlNode3 = (XmlNode)obj;
									bool flag6 = xmlNode3.Name.Equals("cas:user", StringComparison.OrdinalIgnoreCase);
									if (flag6)
									{
										text3 = xmlNode3.InnerText;
									}
									else
									{
										bool flag7 = xmlNode3.Name.Equals("cas:authtype", StringComparison.OrdinalIgnoreCase);
										if (flag7)
										{
											text4 = xmlNode3.InnerText;
										}
									}
								}
								bool flag8 = !string.IsNullOrEmpty(text3);
								if (flag8)
								{
									flag = true;
								}
							}
						}
						else
						{
							CWLogger.Logger.Warn("CASAuthManager:AuthenticateCAS:FailedLogin:UnrecognizableXml:xml={0}:url={1}", text2 ?? "NULL", text ?? "NULL");
						}
					}
				}
				catch (Exception exception)
				{
					text2 = "";
					CWLogger.Logger.ErrorException(string.Format("CASAuthManager:AuthenticateCAS:ParseXmlException:xml={0}:url={1}", text2 ?? "NULL", text ?? "NULL"), exception);
				}
			}
			bool flag9 = !flag || string.IsNullOrEmpty(text3);
			CASAuthenticationResult result;
			if (flag9)
			{
				result = new CASAuthenticationResult
				{
					IsAuthenticated = false,
					ReturnAttributes = null,
					UserName = text3
				};
			}
			else
			{
				CWLogger.Logger.Info("LoginS:SuccessfulAuthentication:username={0}:authtype={1}", (text3 == null) ? "NULL" : text3, (text4 == null) ? "NULL" : text4);
				Dictionary<string, string> returnAttributes = new Dictionary<string, string>
				{
					{
						"authtype",
						text4 ?? ""
					}
				};
				result = new CASAuthenticationResult
				{
					IsAuthenticated = true,
					UserName = text3,
					ReturnAttributes = returnAttributes
				};
			}
			return result;
		}
	}
}
