using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using ClockWorkAPI;
using EmailClassLibrary;
using EncryptionClassLibrary;
using SettingsPermissions;
using TechnoPro.Common.Core;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;
using UnivOleDb;

namespace ReportFunctions
{
	// Token: 0x02000039 RID: 57
	public class BatchEmail
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000419C4 File Offset: 0x000409C4
		// (set) Token: 0x06000354 RID: 852 RVA: 0x000419DB File Offset: 0x000409DB
		public virtual BatchEmail.BatchEmailSendMode SendMode { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000419E4 File Offset: 0x000409E4
		// (set) Token: 0x06000356 RID: 854 RVA: 0x000419FC File Offset: 0x000409FC
		public bool PromptUser
		{
			get
			{
				return this.promptUser;
			}
			set
			{
				this.promptUser = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00041A08 File Offset: 0x00040A08
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00041A20 File Offset: 0x00040A20
		public Dictionary<string, string> EmailArgs
		{
			get
			{
				return this.emailArgs;
			}
			set
			{
				this.emailArgs = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00041A2C File Offset: 0x00040A2C
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00041A44 File Offset: 0x00040A44
		public int DelayBetweenEmails
		{
			get
			{
				return this.delayBetweenEmails;
			}
			set
			{
				this.delayBetweenEmails = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00041A50 File Offset: 0x00040A50
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00041A68 File Offset: 0x00040A68
		public bool BodyIsHtml
		{
			get
			{
				return this.bodyIsHtml;
			}
			set
			{
				this.bodyIsHtml = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00041A74 File Offset: 0x00040A74
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00041A8C File Offset: 0x00040A8C
		public bool SendReport
		{
			get
			{
				return this.sendReport;
			}
			set
			{
				this.sendReport = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00041A98 File Offset: 0x00040A98
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00041AB0 File Offset: 0x00040AB0
		public string AdminEmail
		{
			get
			{
				return this.adminEmail;
			}
			set
			{
				this.adminEmail = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00041ABC File Offset: 0x00040ABC
		public SmtpSettings SmtpSettings
		{
			get
			{
				if (this.smtpSettings == null)
				{
					this.smtpSettings = ReportFunction.GetSmtpSettings(this.da);
				}
				return this.smtpSettings;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00041AF8 File Offset: 0x00040AF8
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00041B10 File Offset: 0x00040B10
		public string EmailHistoryTypeCode
		{
			get
			{
				return this.emailHistoryTypeCode;
			}
			set
			{
				this.emailHistoryTypeCode = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00041B1C File Offset: 0x00040B1C
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00041B34 File Offset: 0x00040B34
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00041B40 File Offset: 0x00040B40
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00041B58 File Offset: 0x00040B58
		public string To
		{
			get
			{
				return this.to;
			}
			set
			{
				this.to = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00041B64 File Offset: 0x00040B64
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00041B7C File Offset: 0x00040B7C
		public string From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00041B88 File Offset: 0x00040B88
		// (set) Token: 0x0600036B RID: 875 RVA: 0x00041BA0 File Offset: 0x00040BA0
		public string Cc
		{
			get
			{
				return this.cc;
			}
			set
			{
				this.cc = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00041BAC File Offset: 0x00040BAC
		// (set) Token: 0x0600036D RID: 877 RVA: 0x00041BC4 File Offset: 0x00040BC4
		public string Bcc
		{
			get
			{
				return this.bcc;
			}
			set
			{
				this.bcc = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00041BD0 File Offset: 0x00040BD0
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00041BE8 File Offset: 0x00040BE8
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00041BF4 File Offset: 0x00040BF4
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00041C0C File Offset: 0x00040C0C
		public string Attachments
		{
			get
			{
				return this.attachments;
			}
			set
			{
				this.attachments = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00041C18 File Offset: 0x00040C18
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00041C30 File Offset: 0x00040C30
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00041C3C File Offset: 0x00040C3C
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00041C54 File Offset: 0x00040C54
		public bool IsActive
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00041C60 File Offset: 0x00040C60
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00041C78 File Offset: 0x00040C78
		public bool TestMode
		{
			get
			{
				return this.testMode;
			}
			set
			{
				this.testMode = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00041C84 File Offset: 0x00040C84
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00041C9C File Offset: 0x00040C9C
		public int IconNum
		{
			get
			{
				return this.iconNum;
			}
			set
			{
				this.iconNum = value;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00041CA8 File Offset: 0x00040CA8
		public BatchEmail(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, string xml)
		{
			this.SendMode = BatchEmail.BatchEmailSendMode.SendEmails;
			this.emailArgs = new Dictionary<string, string>();
			this.da = da;
			this.tripleDES = tripleDES;
			this.smtpSettings = ReportFunction.GetSmtpSettings(da);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(string.IsNullOrEmpty(xml) ? "<batchemails></batchemails>" : xml);
			XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
			if (firstChild != null)
			{
				XmlAttributeCollection attributes = firstChild.Attributes;
				this.FromXml(attributes);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00041DE4 File Offset: 0x00040DE4
		private string ReplaceMailMergeCode(string s, string lookup, string replace)
		{
			string result;
			if (s.IndexOf(lookup) >= 0)
			{
				result = s.Replace(lookup, replace);
			}
			else
			{
				result = Regex.Replace(s, Regex.Escape(lookup), Regex.Escape(replace), RegexOptions.IgnoreCase);
			}
			return result;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00041E24 File Offset: 0x00040E24
		public EmailResult SendEmail(Dictionary<string, string> args)
		{
			EmailResult emailResult = new EmailResult();
			emailResult.Worked = false;
			string text = this.to;
			string text2 = this.cc;
			string text3 = this.bcc;
			string text4 = this.body;
			string text5 = this.from;
			string text6 = this.attachments;
			string text7 = this.subject;
			foreach (string text8 in args.Keys)
			{
				string lookup = string.Format("{0}{1}{2}", "#~", text8, "~#");
				text = this.ReplaceMailMergeCode(text, lookup, args[text8]);
				text5 = this.ReplaceMailMergeCode(text5, lookup, args[text8]);
				text2 = this.ReplaceMailMergeCode(text2, lookup, args[text8]);
				text3 = this.ReplaceMailMergeCode(text3, lookup, args[text8]);
				text6 = this.ReplaceMailMergeCode(text6, lookup, args[text8]);
				text7 = this.ReplaceMailMergeCode(text7, lookup, args[text8]);
				text4 = this.ReplaceMailMergeCode(text4, lookup, args[text8]);
			}
			if (this.testMode)
			{
				text4 = string.Format("Currently running in test mode.  Would have sent to: [to]={0}, [cc]={1}, [bcc]={2}.\n\n{3}", new object[]
				{
					text,
					text2,
					text3,
					text4
				});
				text = this.adminEmail;
				text2 = "";
				text3 = "";
			}
			if (this.bodyIsHtml && text4.IndexOf("<br />") < 0 && text4.IndexOf("<br>") < 0)
			{
				text4 = text4.Replace(Environment.NewLine, "<br />");
			}
			emailResult.Email = new EmailTemplate
			{
				To = text,
				From = text5,
				Subject = text7,
				Cc = text2,
				Bcc = text3,
				Attachments = text6,
				Body = text4
			};
			try
			{
				int personId = this.ExtractIntFromArgs(args, "personid");
				int lucid = this.ExtractIntFromArgs(args, "lucourseid");
				int infoPcId = this.ExtractIntFromArgs(args, "infopcid");
				int sentBy = this.ExtractIntFromArgs(args, "sentby");
				string text9;
				if (args.ContainsKey("lucids"))
				{
					text9 = args["lucids"];
				}
				else
				{
					text9 = "";
				}
				List<int> list = new List<int>();
				if (!string.IsNullOrEmpty(text9))
				{
					string[] array = text9.Split(new char[]
					{
						','
					});
					foreach (string s in array)
					{
						int item;
						if (int.TryParse(s, out item))
						{
							list.Add(item);
						}
					}
				}
				string text10 = null;
				if (text5 == null || string.IsNullOrEmpty(text5.Trim()))
				{
					text5 = this.DefaultAdminEmail;
				}
				if (text == null || string.IsNullOrEmpty(text.Trim()))
				{
					text10 = "Missing/Invalid to address";
				}
				else if (text5 == null || string.IsNullOrEmpty(text5.Trim()))
				{
					text10 = "Missing/Invalid from address";
				}
				bool flag = this.isActive && string.IsNullOrEmpty(text10) && (this.SendMode == BatchEmail.BatchEmailSendMode.SendEmails || this.SendMode == BatchEmail.BatchEmailSendMode.SendFirstEmail);
				if (flag)
				{
					IEmailManager emailManager = new EmailManager(new OperationContext
					{
						WhoAmI = 0
					});
					TPMailResult tpmailResult = emailManager.SendEmail(text, text5, text7, text4, this.bodyIsHtml ? text4 : null, text2, text3, text6);
					text10 = tpmailResult.ErrorMessage;
				}
				else if (string.IsNullOrEmpty(text10))
				{
					text10 = "Not active";
				}
				if (!string.IsNullOrEmpty(text10))
				{
					if (flag)
					{
						EmailHistory.AddLogEntry(this.da, this.tripleDES, this.emailHistoryTypeCode, personId, 0, sentBy, "", false, text10, lucid, infoPcId, list);
						emailResult.Message = text10;
						throw new Exception(text10);
					}
				}
				else
				{
					string ebody = string.Format("From: {0}\nTo: {1}\nCc: {2}\nBcc: {3}\nSubject: {4}\nAttach: {5}\nBody: {6}", new object[]
					{
						text5,
						text,
						text2,
						text3,
						text7,
						text6,
						text4
					});
					emailResult.Worked = true;
					if (!this.testMode)
					{
						EmailHistory.AddLogEntry(this.da, this.tripleDES, this.emailHistoryTypeCode, personId, 0, sentBy, ebody, true, "", lucid, infoPcId, list);
						if (this.iconNum > 0)
						{
							if (args.ContainsKey("appointmentids"))
							{
								string text11 = args["appointmentids"];
								if (!string.IsNullOrEmpty(text11))
								{
									string[] array3 = text11.Split(new char[]
									{
										','
									});
									foreach (string s2 in array3)
									{
										int appId;
										if (int.TryParse(s2, out appId))
										{
											Icon.AddIconToAppointment(this.da, appId, this.iconNum);
										}
									}
								}
							}
							else
							{
								int num = this.ExtractIntFromArgs(args, "appointmentid");
								if (num > 0)
								{
									Icon.AddIconToAppointment(this.da, num, this.iconNum);
								}
							}
						}
					}
				}
				emailResult.Message = text10;
			}
			catch (Exception exception)
			{
				emailResult.Exception = exception;
			}
			return emailResult;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0004243C File Offset: 0x0004143C
		private int ExtractIntFromArgs(Dictionary<string, string> args, string name)
		{
			int num = 0;
			int result;
			if (args.ContainsKey(name))
			{
				string text = args[name];
				int num2;
				if (string.IsNullOrEmpty(text))
				{
					result = num;
				}
				else if (int.TryParse(text, out num2))
				{
					result = num2;
				}
				else
				{
					result = num;
				}
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00042498 File Offset: 0x00041498
		public string DefaultAdminEmail
		{
			get
			{
				if (this.defaultAdminEmail == null)
				{
					SettingWithValueCollection settingWithValueCollection = Settings.LoadEveryoneSettings(this.da, new int[]
					{
						107
					});
					if (settingWithValueCollection != null && settingWithValueCollection.Count > 0)
					{
						SettingWithValue settingWithValue = settingWithValueCollection[107];
						if (settingWithValue != null)
						{
							this.defaultAdminEmail = settingWithValue.ValStr;
						}
					}
				}
				return this.defaultAdminEmail;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00042518 File Offset: 0x00041518
		private void FromXml(XmlAttributeCollection attributes)
		{
			this.adminEmail = this.DefaultAdminEmail;
			this.to = "#~email~#";
			this.from = this.DefaultAdminEmail;
			foreach (object obj in attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text = xmlAttribute.Name.ToLower();
				string text2 = xmlAttribute.Value;
				if (text2 == null)
				{
					text2 = "";
				}
				bool flag = string.IsNullOrEmpty(text2);
				string text3 = text;
				switch (text3)
				{
				case "emailhistorytypecode":
					if (!flag)
					{
						this.emailHistoryTypeCode = text2;
					}
					if (string.IsNullOrEmpty(this.emailHistoryTypeCode))
					{
						this.emailHistoryTypeCode = "UNKNOWN";
					}
					break;
				case "title":
					if (!flag)
					{
						this.title = text2;
					}
					break;
				case "to":
					if (!flag)
					{
						this.to = text2;
					}
					break;
				case "from":
					if (!flag)
					{
						this.from = text2;
					}
					break;
				case "cc":
					if (!flag)
					{
						this.cc = text2;
					}
					break;
				case "bcc":
					if (!flag)
					{
						this.bcc = text2;
					}
					break;
				case "subject":
					if (!flag)
					{
						this.subject = text2;
					}
					break;
				case "attachments":
					if (!flag)
					{
						this.attachments = text2;
					}
					break;
				case "body":
					if (!flag)
					{
						this.body = text2;
					}
					break;
				case "isactive":
					this.isActive = BatchEmail.StringToBool(text2, false);
					break;
				case "testmode":
					this.testMode = BatchEmail.StringToBool(text2, false);
					break;
				case "adminemail":
					if (!flag)
					{
						this.adminEmail = text2;
					}
					break;
				case "sendreport":
					this.sendReport = BatchEmail.StringToBool(text2, false);
					break;
				case "templateid":
					this.templateId = BatchEmail.StringToInt(text2, 0);
					break;
				case "iconnum":
					this.iconNum = BatchEmail.StringToInt(text2, -1);
					break;
				case "bodyishtml":
					this.bodyIsHtml = BatchEmail.StringToBool(text2, true);
					break;
				case "delaybetweenemails":
					this.delayBetweenEmails = BatchEmail.StringToInt(text2, 0);
					break;
				case "promptuser":
					this.promptUser = BatchEmail.StringToBool(text2, false);
					break;
				case "priority":
					if (!this.emailArgs.ContainsKey("priority"))
					{
						this.emailArgs.Add("priority", text2);
					}
					break;
				}
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000428E4 File Offset: 0x000418E4
		private static bool StringToBool(string s, bool defaultValue)
		{
			bool result;
			bool flag;
			if (string.IsNullOrEmpty(s))
			{
				result = defaultValue;
			}
			else if (s.Equals("1"))
			{
				result = true;
			}
			else if (bool.TryParse(s, out flag))
			{
				result = flag;
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00042934 File Offset: 0x00041934
		private static int StringToInt(string s, int defaultValue)
		{
			int result;
			int num;
			if (string.IsNullOrEmpty(s))
			{
				result = defaultValue;
			}
			else if (int.TryParse(s, out num))
			{
				result = num;
			}
			else
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x040001A6 RID: 422
		private const string pre = "#~";

		// Token: 0x040001A7 RID: 423
		private const string post = "~#";

		// Token: 0x040001A8 RID: 424
		private string to = "";

		// Token: 0x040001A9 RID: 425
		private string from = "";

		// Token: 0x040001AA RID: 426
		private string cc = "";

		// Token: 0x040001AB RID: 427
		private string bcc = "";

		// Token: 0x040001AC RID: 428
		private string attachments = "";

		// Token: 0x040001AD RID: 429
		private string body = "";

		// Token: 0x040001AE RID: 430
		private string subject = "";

		// Token: 0x040001AF RID: 431
		private SmtpSettings smtpSettings;

		// Token: 0x040001B0 RID: 432
		private UnivDataAdapter da;

		// Token: 0x040001B1 RID: 433
		private TripleDESEncryptionClass tripleDES;

		// Token: 0x040001B2 RID: 434
		private string title = "";

		// Token: 0x040001B3 RID: 435
		private string emailHistoryTypeCode = "";

		// Token: 0x040001B4 RID: 436
		private bool testMode = true;

		// Token: 0x040001B5 RID: 437
		private string adminEmail = "";

		// Token: 0x040001B6 RID: 438
		private bool isActive = true;

		// Token: 0x040001B7 RID: 439
		private bool sendReport = true;

		// Token: 0x040001B8 RID: 440
		private int templateId = 0;

		// Token: 0x040001B9 RID: 441
		private int iconNum = -1;

		// Token: 0x040001BA RID: 442
		private bool bodyIsHtml = true;

		// Token: 0x040001BB RID: 443
		private int delayBetweenEmails = 0;

		// Token: 0x040001BC RID: 444
		private bool promptUser = false;

		// Token: 0x040001BD RID: 445
		private Dictionary<string, string> emailArgs;

		// Token: 0x040001BE RID: 446
		private object mailManObj = null;

		// Token: 0x040001BF RID: 447
		private string defaultAdminEmail = null;

		// Token: 0x0200003A RID: 58
		public enum BatchEmailSendMode
		{
			// Token: 0x040001C2 RID: 450
			SendEmails,
			// Token: 0x040001C3 RID: 451
			SendFirstEmail,
			// Token: 0x040001C4 RID: 452
			DontSendEmails,
			// Token: 0x040001C5 RID: 453
			PreviewEmails
		}
	}
}
