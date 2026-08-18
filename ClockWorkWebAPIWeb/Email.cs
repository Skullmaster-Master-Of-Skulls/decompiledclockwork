using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Web.UI;
using System.Xml;
using ClockWorkLogger;
using ClockWorkWebAPI;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPIWeb
{
	// Token: 0x02000014 RID: 20
	public class Email
	{
		// Token: 0x0600011F RID: 287 RVA: 0x0000E714 File Offset: 0x0000C914
		[Obsolete]
		public static void ParseEmailXml(string xml, string preferredLanguage, out string from, out string to, out string[] cc, out string[] bcc, out string subject, out string body, out string attachments, out bool active)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string xml2 = "<?xml version=\"1.0\"?>" + xml;
			xmlDocument.LoadXml(xml2);
			XmlNode nextSibling = xmlDocument.FirstChild.NextSibling;
			XmlNode firstChild = nextSibling.FirstChild;
			XmlNode nextSibling2 = firstChild.NextSibling;
			from = "";
			to = "";
			cc = null;
			bcc = null;
			subject = "";
			body = "";
			attachments = "";
			active = false;
			bool flag = nextSibling2.ChildNodes.Count == 1;
			if (flag)
			{
				XmlNode firstChild2 = nextSibling2.FirstChild;
				foreach (object obj in nextSibling2.FirstChild.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text = xmlNode.Name.ToLower();
					string innerText = xmlNode.InnerText;
					string text2 = text;
					string text3 = text2;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
					if (num <= 1874587459U)
					{
						if (num <= 1111836708U)
						{
							if (num != 441736833U)
							{
								if (num == 1111836708U)
								{
									if (text3 == "to")
									{
										to = innerText;
									}
								}
							}
							else if (text3 == "attachments")
							{
								attachments = innerText;
							}
						}
						else if (num != 1445564707U)
						{
							if (num == 1874587459U)
							{
								if (text3 == "bcc")
								{
									bcc = innerText.Split(new char[]
									{
										','
									});
								}
							}
						}
						else if (text3 == "cc")
						{
							cc = innerText.Split(new char[]
							{
								','
							});
						}
					}
					else if (num <= 2433117216U)
					{
						if (num != 2300378703U)
						{
							if (num == 2433117216U)
							{
								if (text3 == "bodyhtml")
								{
									body = innerText;
								}
							}
						}
						else if (text3 == "subject")
						{
							subject = innerText;
						}
					}
					else if (num != 2513272949U)
					{
						if (num == 3648362799U)
						{
							if (text3 == "active")
							{
								active = innerText.Equals("1");
							}
						}
					}
					else if (text3 == "from")
					{
						from = innerText;
					}
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		[Obsolete]
		public static Exception SendEmailFromTemplate(db conn, Page page, Setting setting, StringDictionary args)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return Email.SendEmailFromTemplate(webSettingsClientManager.GetSettingValue<string>(setting), args);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
		[Obsolete]
		public static bool IsEmailTemplateActive(string emailXml)
		{
			string text = emailXml.Trim();
			bool flag = string.IsNullOrEmpty(text);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				XmlDocument xmlDocument;
				try
				{
					xmlDocument = new XmlDocument();
					string xml = "<?xml version=\"1.0\"?>" + text;
					xmlDocument.LoadXml(xml);
				}
				catch (Exception ex)
				{
					return false;
				}
				foreach (object obj in xmlDocument.LastChild.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string text2 = xmlNode.Name.ToLower();
					string innerText = xmlNode.InnerText;
					string text3 = text2;
					string a = text3;
					if (a == "isactive")
					{
						return innerText.Equals("1");
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		private static void AddKey(ref StringDictionary args, string keyName, Setting setting)
		{
			bool flag = !args.ContainsKey(keyName);
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(setting);
				args.Add(keyName, settingValue);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		[Obsolete]
		public static Exception SendEmailFromTemplate(string emailXml, StringDictionary args)
		{
			string text = emailXml.Trim();
			bool flag = args != null && args.Count > 0;
			string text2;
			if (flag)
			{
				string[] array = new string[args.Count];
				args.Keys.CopyTo(array, 0);
				text2 = string.Join(", ", array);
			}
			else
			{
				text2 = "NONE.";
			}
			bool flag2 = string.IsNullOrEmpty(text);
			Exception result;
			if (flag2)
			{
				CWLogger.Logger.Warn("EMAIL:SendEmailFromTemplate:Email template is missing. Args={0}", text2);
				result = null;
			}
			else
			{
				Email.AddKey(ref args, "testcoordinatoremail", Setting.TESTBOOKING_TestBookingCoordinatorEmail);
				Email.AddKey(ref args, "testcontactinfo", Setting.TESTBOOKING_DepartmentContactInformation);
				Email.AddKey(ref args, "testprofcontactinfo", Setting.INSTRUCTOR_contactInfo);
				foreach (object obj in args)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text3 = (dictionaryEntry.Value == null) ? "" : dictionaryEntry.Value.ToString();
					text3 = SecurityElement.Escape(text3);
					text = text.Replace("#~" + dictionaryEntry.Key.ToString() + "~#", text3);
				}
				XmlDocument xmlDocument;
				try
				{
					xmlDocument = new XmlDocument();
					string xml = "<?xml version=\"1.0\"?>" + text;
					xmlDocument.LoadXml(xml);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("SendEmailFromTemplate:xml={0}", (emailXml == null) ? "NULL" : emailXml), ex);
					return ex;
				}
				string text4 = "";
				string text5 = "";
				string[] cc = null;
				string[] bcc = null;
				string subject = "";
				string body = "";
				bool flag3 = false;
				string overrideTestModeAdminEmail = "";
				bool flag4 = false;
				foreach (object obj2 in xmlDocument.LastChild.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					string text6 = xmlNode.Name.ToLower();
					string innerText = xmlNode.InnerText;
					string text7 = text6;
					string text8 = text7;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text8);
					if (num <= 1445564707U)
					{
						if (num <= 510364466U)
						{
							if (num != 441736833U)
							{
								if (num == 510364466U)
								{
									if (text8 == "testmode")
									{
										foreach (object obj3 in xmlNode.Attributes)
										{
											XmlAttribute xmlAttribute = (XmlAttribute)obj3;
											text6 = xmlAttribute.Name.ToLower();
											string text9 = text6;
											string a = text9;
											if (!(a == "isactive"))
											{
												if (a == "adminemail")
												{
													overrideTestModeAdminEmail = innerText;
												}
											}
											else
											{
												flag4 = innerText.Equals("1");
											}
										}
									}
								}
							}
							else if (!(text8 == "attachments"))
							{
							}
						}
						else if (num != 1111836708U)
						{
							if (num == 1445564707U)
							{
								if (text8 == "cc")
								{
									cc = innerText.Split(new char[]
									{
										','
									});
								}
							}
						}
						else if (text8 == "to")
						{
							text5 = innerText;
						}
					}
					else if (num <= 2300378703U)
					{
						if (num != 1874587459U)
						{
							if (num == 2300378703U)
							{
								if (text8 == "subject")
								{
									subject = innerText;
								}
							}
						}
						else if (text8 == "bcc")
						{
							bcc = innerText.Split(new char[]
							{
								','
							});
						}
					}
					else if (num != 2513272949U)
					{
						if (num != 3410127167U)
						{
							if (num == 3685382517U)
							{
								if (text8 == "body")
								{
									body = innerText;
								}
							}
						}
						else if (text8 == "isactive")
						{
							flag3 = innerText.Equals("1");
						}
					}
					else if (text8 == "from")
					{
						text4 = innerText;
					}
				}
				bool flag5 = !flag4;
				if (flag5)
				{
					overrideTestModeAdminEmail = "";
				}
				bool flag6 = flag3;
				if (flag6)
				{
					IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
					string text10 = string.IsNullOrEmpty(text4) ? webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_FromEmailAddress) : text4;
					bool flag7 = !string.IsNullOrEmpty(text5) && !string.IsNullOrEmpty(text10);
					if (flag7)
					{
						result = Email.SendEmail(text10, text5, cc, bcc, subject, body, overrideTestModeAdminEmail);
					}
					else
					{
						CWLogger.Logger.Error("EMAIL:SendEmailFromTemplate:Missing to and/or from address. To={0}:From={1}:Args={2}", (text5 == null) ? "NULL" : text5, (text4 == null) ? "NULL" : text4, text2);
						result = new Exception("Missing to and/or from address");
					}
				}
				else
				{
					CWLogger.Logger.Warn("EMAIL:SendEmailFromTemplate:Email marked as inactive. To={0}:From={1}:Args={2}", (text5 == null) ? "NULL" : text5, (text4 == null) ? "NULL" : text4, text2);
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		[Obsolete]
		public static Exception SendEmail(string fromEmailAddress, string to, string[] cc, string[] bcc, string subject, string body)
		{
			return Email.SendEmail(fromEmailAddress, to, cc, bcc, subject, body, "");
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000F10C File Offset: 0x0000D30C
		[Obsolete]
		public static Exception SendEmail(string fromEmailAddress, string to, string[] cc, string[] bcc, string subject, string body, string overrideTestModeAdminEmail)
		{
			return Email.SendEmail(fromEmailAddress, to, cc, bcc, subject, body, overrideTestModeAdminEmail, MailPriorityType.Normal);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000F130 File Offset: 0x0000D330
		[Obsolete]
		public static Exception SendEmail(string fromEmailAddress, string to, string[] cc, string[] bcc, string subject, string body, string overrideTestModeAdminEmail, MailPriorityType mailPriority)
		{
			return Email.SendEmail(fromEmailAddress, to, cc, bcc, subject, body, true, overrideTestModeAdminEmail, mailPriority);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000F154 File Offset: 0x0000D354
		[Obsolete]
		public static Exception SendEmail(string fromEmailAddress, string to, string[] cc, string[] bcc, string subject, string body, bool isBodyHtml, string overrideTestModeAdminEmail, MailPriorityType mailPriority)
		{
			bool flag = false;
			bool flag2 = string.IsNullOrEmpty(fromEmailAddress);
			string text;
			if (flag2)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				text = webSettingsClientManager.GetSettingValue<string>(Setting.GENERAL_FromEmailAddress);
			}
			else
			{
				text = fromEmailAddress;
			}
			bool flag3 = string.IsNullOrEmpty(overrideTestModeAdminEmail);
			string text2;
			if (flag3)
			{
				text2 = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("emailtestmodeaddress");
			}
			else
			{
				text2 = overrideTestModeAdminEmail;
			}
			bool flag4 = !string.IsNullOrEmpty(text2);
			if (flag4)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("* Test Mode *\nThis email was redirected to your email address because of the setting in the 'ExternalAppSetings.config' file on the website.  The original intended recipients were:\n");
				stringBuilder.Append(string.Format("To: {0}\n", to));
				bool flag5 = cc != null && cc.Length != 0;
				if (flag5)
				{
					foreach (string arg in cc)
					{
						stringBuilder.Append(string.Format("Cc: {0}\n", arg));
					}
				}
				bool flag6 = bcc != null && bcc.Length != 0;
				if (flag6)
				{
					foreach (string arg2 in bcc)
					{
						stringBuilder.Append(string.Format("Bcc: {0}\n", arg2));
					}
				}
				stringBuilder.Append("\n");
				body = stringBuilder.ToString() + body;
				cc = null;
				bcc = null;
				to = text2;
			}
			Exception result;
			try
			{
				MailMessage mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(text);
				bool flag7 = flag;
				if (flag7)
				{
					mailMessage.To.Add(new MailAddress("mike@tpro.ca"));
				}
				else
				{
					mailMessage.To.Add(new MailAddress(to));
				}
				bool flag8 = cc != null && !flag;
				if (flag8)
				{
					for (int k = 0; k < cc.Length; k++)
					{
						try
						{
							bool flag9 = cc[k].Trim().Length > 0;
							if (flag9)
							{
								mailMessage.CC.Add(new MailAddress(cc[k]));
							}
						}
						catch
						{
						}
					}
				}
				bool flag10 = bcc != null && !flag;
				if (flag10)
				{
					for (int l = 0; l < bcc.Length; l++)
					{
						try
						{
							bool flag11 = bcc[l].Trim().Length > 0;
							if (flag11)
							{
								mailMessage.Bcc.Add(new MailAddress(bcc[l]));
							}
						}
						catch
						{
						}
					}
				}
				mailMessage.IsBodyHtml = isBodyHtml;
				bool flag12 = mailMessage.IsBodyHtml && body.IndexOf("<br") < 0;
				if (flag12)
				{
					mailMessage.Body = body.Replace("\r\n", "<br />");
				}
				else
				{
					mailMessage.Body = body;
				}
				mailMessage.Subject = (flag ? (subject + " (" + to + ")") : subject);
				mailMessage.Priority = (MailPriority)mailPriority;
				SmtpClient smtpClient = new SmtpClient();
				smtpClient.Send(mailMessage);
				CWLogger.Logger.Info("EMAIL:SendEmail:Sent email successfully. To={0}:From={1}:Subject={2}", (to == null) ? "NULL" : to, (text == null) ? "NULL" : text, (subject == null) ? "NULL" : subject);
				result = null;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("EMAIL:SendEmail:To={0}:From={1}:subject={2}:emsg={3}", new object[]
				{
					(to == null) ? "NULL" : to,
					(text == null) ? "NULL" : text,
					(subject == null) ? "NULL" : subject,
					ex.ToString()
				});
				result = ex;
			}
			return result;
		}
	}
}
