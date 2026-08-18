using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x0200000F RID: 15
	public class BatchEmail
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00022F88 File Offset: 0x00021188
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00022F90 File Offset: 0x00021190
		public virtual BatchEmail.BatchEmailSendMode SendMode { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00022F9C File Offset: 0x0002119C
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00022FB4 File Offset: 0x000211B4
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00022FC0 File Offset: 0x000211C0
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00022FD8 File Offset: 0x000211D8
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00022FE4 File Offset: 0x000211E4
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00022FFC File Offset: 0x000211FC
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00023008 File Offset: 0x00021208
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00023020 File Offset: 0x00021220
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0002302C File Offset: 0x0002122C
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00023044 File Offset: 0x00021244
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00023050 File Offset: 0x00021250
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00023068 File Offset: 0x00021268
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00023074 File Offset: 0x00021274
		public SmtpSettings SmtpSettings
		{
			get
			{
				bool flag = this.smtpSettings == null;
				if (flag)
				{
					this.smtpSettings = new SmtpSettings(0, "");
				}
				return this.smtpSettings;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000230AC File Offset: 0x000212AC
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000230C4 File Offset: 0x000212C4
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000230D0 File Offset: 0x000212D0
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000230E8 File Offset: 0x000212E8
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000230F4 File Offset: 0x000212F4
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0002310C File Offset: 0x0002130C
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00023118 File Offset: 0x00021318
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00023130 File Offset: 0x00021330
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0002313C File Offset: 0x0002133C
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00023154 File Offset: 0x00021354
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00023160 File Offset: 0x00021360
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00023178 File Offset: 0x00021378
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00023184 File Offset: 0x00021384
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0002319C File Offset: 0x0002139C
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000231A8 File Offset: 0x000213A8
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000231C0 File Offset: 0x000213C0
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000231CC File Offset: 0x000213CC
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000231E4 File Offset: 0x000213E4
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000231F0 File Offset: 0x000213F0
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00023208 File Offset: 0x00021408
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00023214 File Offset: 0x00021414
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0002322C File Offset: 0x0002142C
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00023238 File Offset: 0x00021438
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00023250 File Offset: 0x00021450
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

		// Token: 0x06000144 RID: 324 RVA: 0x0002325C File Offset: 0x0002145C
		public BatchEmail(string xml)
		{
			this.SendMode = BatchEmail.BatchEmailSendMode.SendEmails;
			this.emailArgs = new Dictionary<string, string>();
			this.smtpSettings = new SmtpSettings(0, "");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(string.IsNullOrEmpty(xml) ? "<batchemails></batchemails>" : xml);
			XmlNode firstChild = xmlDocument.DocumentElement.FirstChild;
			bool flag = firstChild != null;
			if (flag)
			{
				XmlAttributeCollection attributes = firstChild.Attributes;
				this.FromXml(attributes);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0002338C File Offset: 0x0002158C
		private string ReplaceMailMergeCode(string s, string lookup, string replace)
		{
			bool flag = s.IndexOf(lookup) >= 0;
			string result;
			if (flag)
			{
				result = s.Replace(lookup, replace);
			}
			else
			{
				result = Regex.Replace(s, Regex.Escape(lookup), Regex.Escape(replace), RegexOptions.IgnoreCase);
			}
			return result;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000233D0 File Offset: 0x000215D0
		private int ExtractIntFromArgs(Dictionary<string, string> args, string name)
		{
			int num = 0;
			bool flag = args.ContainsKey(name);
			int result;
			if (flag)
			{
				string text = args[name];
				bool flag2 = string.IsNullOrEmpty(text);
				if (flag2)
				{
					result = num;
				}
				else
				{
					int num2;
					bool flag3 = int.TryParse(text, out num2);
					if (flag3)
					{
						result = num2;
					}
					else
					{
						result = num;
					}
				}
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00023424 File Offset: 0x00021624
		public string DefaultAdminEmail
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0002343C File Offset: 0x0002163C
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
				bool flag = text2 == null;
				if (flag)
				{
					text2 = "";
				}
				bool flag2 = string.IsNullOrEmpty(text2);
				string text3 = text;
				string text4 = text3;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text4);
				if (num <= 2300378703U)
				{
					if (num <= 510364466U)
					{
						if (num <= 381759768U)
						{
							if (num != 19793580U)
							{
								if (num == 381759768U)
								{
									if (text4 == "promptuser")
									{
										this.promptUser = BatchEmail.StringToBool(text2, false);
									}
								}
							}
							else if (text4 == "adminemail")
							{
								bool flag3 = !flag2;
								if (flag3)
								{
									this.adminEmail = text2;
								}
							}
						}
						else if (num != 441736833U)
						{
							if (num == 510364466U)
							{
								if (text4 == "testmode")
								{
									this.testMode = BatchEmail.StringToBool(text2, false);
								}
							}
						}
						else if (text4 == "attachments")
						{
							bool flag4 = !flag2;
							if (flag4)
							{
								this.attachments = text2;
							}
						}
					}
					else if (num <= 1445564707U)
					{
						if (num != 1111836708U)
						{
							if (num == 1445564707U)
							{
								if (text4 == "cc")
								{
									bool flag5 = !flag2;
									if (flag5)
									{
										this.cc = text2;
									}
								}
							}
						}
						else if (text4 == "to")
						{
							bool flag6 = !flag2;
							if (flag6)
							{
								this.to = text2;
							}
						}
					}
					else if (num != 1874587459U)
					{
						if (num != 2174115206U)
						{
							if (num == 2300378703U)
							{
								if (text4 == "subject")
								{
									bool flag7 = !flag2;
									if (flag7)
									{
										this.subject = text2;
									}
								}
							}
						}
						else if (text4 == "templateid")
						{
							this.templateId = BatchEmail.StringToInt(text2, 0);
						}
					}
					else if (text4 == "bcc")
					{
						bool flag8 = !flag2;
						if (flag8)
						{
							this.bcc = text2;
						}
					}
				}
				else if (num <= 3685382517U)
				{
					if (num <= 2513272949U)
					{
						if (num != 2498028297U)
						{
							if (num == 2513272949U)
							{
								if (text4 == "from")
								{
									bool flag9 = !flag2;
									if (flag9)
									{
										this.from = text2;
									}
								}
							}
						}
						else if (text4 == "priority")
						{
							bool flag10 = !this.emailArgs.ContainsKey("priority");
							if (flag10)
							{
								this.emailArgs.Add("priority", text2);
							}
						}
					}
					else if (num != 2556802313U)
					{
						if (num != 3410127167U)
						{
							if (num == 3685382517U)
							{
								if (text4 == "body")
								{
									bool flag11 = !flag2;
									if (flag11)
									{
										this.body = text2;
									}
								}
							}
						}
						else if (text4 == "isactive")
						{
							this.isActive = BatchEmail.StringToBool(text2, false);
						}
					}
					else if (text4 == "title")
					{
						bool flag12 = !flag2;
						if (flag12)
						{
							this.title = text2;
						}
					}
				}
				else if (num <= 3842741008U)
				{
					if (num != 3773915440U)
					{
						if (num == 3842741008U)
						{
							if (text4 == "iconnum")
							{
								this.iconNum = BatchEmail.StringToInt(text2, -1);
							}
						}
					}
					else if (text4 == "bodyishtml")
					{
						this.bodyIsHtml = BatchEmail.StringToBool(text2, true);
					}
				}
				else if (num != 4144906025U)
				{
					if (num != 4154231416U)
					{
						if (num == 4225941681U)
						{
							if (text4 == "sendreport")
							{
								this.sendReport = BatchEmail.StringToBool(text2, false);
							}
						}
					}
					else if (text4 == "emailhistorytypecode")
					{
						bool flag13 = !flag2;
						if (flag13)
						{
							this.emailHistoryTypeCode = text2;
						}
						bool flag14 = string.IsNullOrEmpty(this.emailHistoryTypeCode);
						if (flag14)
						{
							this.emailHistoryTypeCode = "UNKNOWN";
						}
					}
				}
				else if (text4 == "delaybetweenemails")
				{
					this.delayBetweenEmails = BatchEmail.StringToInt(text2, 0);
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000239B4 File Offset: 0x00021BB4
		private static bool StringToBool(string s, bool defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				bool flag2 = s.Equals("1");
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag4;
					bool flag3 = bool.TryParse(s, out flag4);
					if (flag3)
					{
						result = flag4;
					}
					else
					{
						result = defaultValue;
					}
				}
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000239FC File Offset: 0x00021BFC
		private static int StringToInt(string s, int defaultValue)
		{
			bool flag = string.IsNullOrEmpty(s);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num;
				bool flag2 = int.TryParse(s, out num);
				if (flag2)
				{
					result = num;
				}
				else
				{
					result = defaultValue;
				}
			}
			return result;
		}

		// Token: 0x0400002F RID: 47
		private string to = "";

		// Token: 0x04000030 RID: 48
		private string from = "";

		// Token: 0x04000031 RID: 49
		private string cc = "";

		// Token: 0x04000032 RID: 50
		private string bcc = "";

		// Token: 0x04000033 RID: 51
		private string attachments = "";

		// Token: 0x04000034 RID: 52
		private string body = "";

		// Token: 0x04000035 RID: 53
		private string subject = "";

		// Token: 0x04000036 RID: 54
		private SmtpSettings smtpSettings;

		// Token: 0x04000037 RID: 55
		private string title = "";

		// Token: 0x04000038 RID: 56
		private string emailHistoryTypeCode = "";

		// Token: 0x04000039 RID: 57
		private bool testMode = true;

		// Token: 0x0400003A RID: 58
		private string adminEmail = "";

		// Token: 0x0400003B RID: 59
		private bool isActive = true;

		// Token: 0x0400003C RID: 60
		private bool sendReport = true;

		// Token: 0x0400003D RID: 61
		private int templateId = 0;

		// Token: 0x0400003E RID: 62
		private int iconNum = -1;

		// Token: 0x0400003F RID: 63
		private bool bodyIsHtml = true;

		// Token: 0x04000040 RID: 64
		private int delayBetweenEmails = 0;

		// Token: 0x04000041 RID: 65
		private bool promptUser = false;

		// Token: 0x04000042 RID: 66
		private Dictionary<string, string> emailArgs;

		// Token: 0x04000043 RID: 67
		private const string pre = "#~";

		// Token: 0x04000044 RID: 68
		private const string post = "~#";

		// Token: 0x04000045 RID: 69
		private object mailManObj = null;

		// Token: 0x04000046 RID: 70
		private string defaultAdminEmail = null;

		// Token: 0x02000053 RID: 83
		public enum BatchEmailSendMode
		{
			// Token: 0x04000151 RID: 337
			SendEmails,
			// Token: 0x04000152 RID: 338
			SendFirstEmail,
			// Token: 0x04000153 RID: 339
			DontSendEmails,
			// Token: 0x04000154 RID: 340
			PreviewEmails
		}
	}
}
