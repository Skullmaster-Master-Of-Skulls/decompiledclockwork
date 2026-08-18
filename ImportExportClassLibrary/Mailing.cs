using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using ClockWorkAPI;
using EmailClassLibrary;
using SettingsPermissions;
using TechnoPro.Common.Core;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TPEmailer;
using UnivOleDb;

namespace ImportExportClassLibrary
{
	// Token: 0x0200004E RID: 78
	public class Mailing
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000309 RID: 777 RVA: 0x0001F428 File Offset: 0x0001E428
		// (remove) Token: 0x0600030A RID: 778 RVA: 0x0001F460 File Offset: 0x0001E460
		public event EmailSentEventHandler OnEmailSent;

		// Token: 0x0600030B RID: 779 RVA: 0x0001F4AC File Offset: 0x0001E4AC
		public Mailing()
		{
			this._bgWorker = new BackgroundWorker();
			this._bgWorker.DoWork += this._bgWorker_DoWork;
			this._bgWorker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
			{
				if (this.OnEmailSent != null)
				{
					this.OnEmailSent();
				}
			};
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001F50C File Offset: 0x0001E50C
		private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Dictionary<string, string> dictionary = e.Argument as Dictionary<string, string>;
			if (dictionary != null)
			{
				try
				{
					using (UnivConnection univConnection = UnivOleDbFactory.CreateConnection(this.CWConnectionString))
					{
						UnivDataAdapter da = univConnection.CreateDataAdapter();
						TemplateInDatabase templateInDatabase = TemplateInDatabase.LoadTemplate(da, this.templateName);
						if (templateInDatabase == null || templateInDatabase.IsEmpty)
						{
							e.Cancel = true;
							throw new Exception("Template not found.");
						}
						string filename = templateInDatabase.Filename;
						if (!File.Exists(filename))
						{
							e.Cancel = true;
							throw new Exception(string.Format("Template file not found ({0})", filename));
						}
						string text = File.ReadAllText(filename);
						string arg = "#<";
						string arg2 = ">#";
						if (text.IndexOf("~#") >= 0 && text.IndexOf("#~") >= 0)
						{
							arg = "#~";
							arg2 = "~#";
						}
						foreach (KeyValuePair<string, string> keyValuePair in dictionary)
						{
							string text2 = keyValuePair.Key;
							string arg3;
							if (text2.StartsWith("#<") && text2.Length > 4)
							{
								arg3 = text2.Substring(2, text2.Length - 4);
							}
							else
							{
								arg3 = text2;
							}
							text2 = string.Format("{0}{1}{2}", arg, arg3, arg2);
							text = text.Replace(text2, (keyValuePair.Value == null) ? "" : keyValuePair.Value);
						}
						switch (this.emailSendMethod)
						{
						case EmailMethod.Smtp:
						{
							SmtpSettings smtpSettings = Mailing.GetSmtpSettings(da);
							if (smtpSettings == null)
							{
								e.Cancel = true;
								throw new Exception("Smtp settings not found.");
							}
							Hashtable hashtable = Email.ParseEmailTemplate(text);
							string to = hashtable.ContainsKey("to") ? hashtable["to"].ToString() : "";
							string from = hashtable.ContainsKey("from") ? hashtable["from"].ToString() : "";
							string subject = hashtable.ContainsKey("subject") ? hashtable["subject"].ToString() : "";
							string cc = hashtable.ContainsKey("cc") ? hashtable["cc"].ToString() : "";
							string bcc = hashtable.ContainsKey("bcc") ? hashtable["bcc"].ToString() : "";
							string attachments = hashtable.ContainsKey("attachments") ? hashtable["attachments"].ToString() : "";
							string text3 = hashtable.ContainsKey("body") ? hashtable["body"].ToString() : "";
							IEmailManager emailManager = new EmailManager(new OperationContext
							{
								WhoAmI = 0
							});
							TPMailResult tpmailResult = emailManager.SendEmail(to, from, subject, text3, null, cc, bcc, attachments);
							string errorMessage = tpmailResult.ErrorMessage;
							if (!string.IsNullOrEmpty(errorMessage))
							{
								e.Cancel = true;
								throw new Exception("Smtp send email failed: " + errorMessage);
							}
							break;
						}
						case EmailMethod.Outlook:
						{
							object obj = null;
							Hashtable hashtable = Email.ParseEmailTemplate(text);
							string to = hashtable.ContainsKey("to") ? hashtable["to"].ToString() : "";
							string from = hashtable.ContainsKey("from") ? hashtable["from"].ToString() : "";
							string subject = hashtable.ContainsKey("subject") ? hashtable["subject"].ToString() : "";
							string cc = hashtable.ContainsKey("cc") ? hashtable["cc"].ToString() : "";
							string bcc = hashtable.ContainsKey("bcc") ? hashtable["bcc"].ToString() : "";
							string attachments = hashtable.ContainsKey("attachments") ? hashtable["attachments"].ToString() : "";
							string text3 = hashtable.ContainsKey("body") ? hashtable["body"].ToString() : "";
							EmailResult emailResult = EmailOut.SendEmailOutlook(ref obj, from, to, subject, cc, bcc, attachments, text3, false);
							if (!emailResult.Worked && emailResult.Exception != null)
							{
								throw emailResult.Exception;
							}
							break;
						}
						case EmailMethod.TpEmailer:
						{
							string tempFilename = TemplatesClass.GetTempFilename(".txt");
							File.WriteAllText(tempFilename, text);
							e.Cancel = true;
							throw new Exception("Email send method not supported.");
						}
						default:
							e.Cancel = true;
							throw new Exception("Email send method not supported.");
						}
					}
					return;
				}
				catch (Exception)
				{
					e.Cancel = true;
					return;
				}
			}
			e.Cancel = true;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001FA30 File Offset: 0x0001EA30
		public void Send(string CWConnectionString, EmailMethod emailSendMethod, string templateName, Dictionary<string, string> dtParameters)
		{
			this.CWConnectionString = CWConnectionString;
			this.emailSendMethod = emailSendMethod;
			this.templateName = templateName;
			this._bgWorker.RunWorkerAsync(dtParameters);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001FA54 File Offset: 0x0001EA54
		public void SendSynchronous(string CWConnectionString, EmailMethod emailSendMethod, string templateName, Dictionary<string, string> dtParameters)
		{
			this.CWConnectionString = CWConnectionString;
			this.emailSendMethod = emailSendMethod;
			this.templateName = templateName;
			this._bgWorker_DoWork(this, new DoWorkEventArgs(dtParameters));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001FA79 File Offset: 0x0001EA79
		public static SmtpSettings GetSmtpSettings(UnivDataAdapter da)
		{
			return Mailing.GetSmtpSettings(da, -1);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001FA84 File Offset: 0x0001EA84
		public static SmtpSettings GetSmtpSettings(UnivDataAdapter da, int groupId)
		{
			string commandText = "SELECT settingcode,settingstringvalue,settingvalue FROM settingsgroups WHERE settingcode IN (SELECT orderid AS settingcode FROM splitorderids(@codes,',')) AND groupid=@gid";
			string parameterValue = Utility.ListToString(new List<int>
			{
				101,
				102,
				103,
				104,
				105
			});
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@gid", groupId);
			da.SelectCommand.Parameters.Add("@codes", parameterValue);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			string text = null;
			string username = null;
			string password = null;
			bool useSsl = false;
			int port = 25;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow[0];
				string text2 = dataRow[1].ToString();
				int num2 = (dataRow[2] == DBNull.Value) ? 0 : ((int)dataRow[2]);
				switch (num)
				{
				case 101:
					text = text2;
					break;
				case 102:
					if (text2.Length > 0)
					{
						int.TryParse(text2, out port);
					}
					else if (num2 > 0)
					{
						port = num2;
					}
					break;
				case 103:
					if (text2.Length > 0)
					{
						useSsl = ("yes1true".IndexOf(text2.ToLower()) >= 0);
					}
					else if (num2 > 0)
					{
						useSsl = (num2 == 1);
					}
					break;
				case 104:
					username = text2;
					break;
				case 105:
					password = text2;
					break;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				return new SmtpSettings(port, text, username, password)
				{
					UseSsl = useSsl
				};
			}
			return null;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001FC78 File Offset: 0x0001EC78
		public static Exception ManualEmailOld(EmailSoftware emailSoftware, string emailTemplateFilledIn, Settings settings)
		{
			Exception result2;
			try
			{
				if (emailSoftware == EmailSoftware.Outlook)
				{
					try
					{
						object obj = null;
						Hashtable hashtable = Email.ParseEmailTemplate(emailTemplateFilledIn);
						string to = hashtable.ContainsKey("to") ? hashtable["to"].ToString() : "";
						string from = hashtable.ContainsKey("from") ? hashtable["from"].ToString() : "";
						string subject = hashtable.ContainsKey("subject") ? hashtable["subject"].ToString() : "";
						string cc = hashtable.ContainsKey("cc") ? hashtable["cc"].ToString() : "";
						string bcc = hashtable.ContainsKey("bcc") ? hashtable["bcc"].ToString() : "";
						string attachments = hashtable.ContainsKey("attachments") ? hashtable["attachments"].ToString() : "";
						string body = hashtable.ContainsKey("body") ? hashtable["body"].ToString() : "";
						EmailResult emailResult = EmailOut.SendEmailOutlook(ref obj, from, to, subject, cc, bcc, attachments, body, false);
						if (!emailResult.Worked && emailResult.Exception != null)
						{
							return emailResult.Exception;
						}
						return null;
					}
					catch (Exception result)
					{
						return result;
					}
				}
				string tempFilename = TemplatesClass.GetTempFilename(".txt");
				File.WriteAllText(tempFilename, emailTemplateFilledIn);
				StringDictionary stringDictionary = Mailing.CreateDefaultDictionaryForTpEmailer(settings);
				stringDictionary.Add("file", "\"" + tempFilename + "\"");
				Mailing.LaunchTpEmailer(stringDictionary);
				result2 = null;
			}
			catch (Exception ex)
			{
				result2 = ex;
			}
			return result2;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001FE64 File Offset: 0x0001EE64
		public static void LaunchTpEmailer(StringDictionary dictionary)
		{
			string[] array = new string[dictionary.Keys.Count];
			string[] array2 = new string[dictionary.Keys.Count];
			dictionary.Keys.CopyTo(array2, 0);
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				string text2 = text + "=" + dictionary[text];
				array[i] = text2;
			}
			Form1 form = new Form1(array);
			form.Show();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001FEDC File Offset: 0x0001EEDC
		public static StringDictionary CreateDefaultDictionaryForTpEmailer(Settings settings)
		{
			string startDirectory = ClockWorkCore.GetStartDirectory();
			string settingString = settings.GetSettingString(22, Path.Combine(startDirectory, "templates\\email"));
			settings.GetSettingString(23, "");
			int setting = settings.GetSetting(103);
			string settingString2 = settings.GetSettingString(104);
			string settingString3 = settings.GetSettingString(105);
			int setting2 = settings.GetSetting(106);
			string settingString4 = settings.GetSettingString(107);
			int setting3 = settings.GetSetting(100);
			string settingString5 = settings.GetSettingString(101);
			string settingString6 = settings.GetSettingString(102);
			return Mailing.CreateDefaultDictionaryForTpEmailer(settingString, setting == 1, settingString2, settingString3, setting2 == 1, settingString4, setting3 == 1, settingString5, settingString6);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001FF7C File Offset: 0x0001EF7C
		public static StringDictionary CreateDefaultDictionaryForTpEmailer(string emailTemplatesDirectory, bool useSsl, string username, string userpassword, bool sendBodyAsHtml, string defaultfromaddress, bool useDefaultEmailSoftware, string smtpserver, string smtpportoutstr)
		{
			StringDictionary stringDictionary = new StringDictionary();
			stringDictionary.Add("emailtemplatesdirectory", emailTemplatesDirectory);
			if (!string.IsNullOrEmpty(smtpserver))
			{
				stringDictionary.Add("smtpserverout", smtpserver);
			}
			stringDictionary.Add("usedefaultemailsoftware", useDefaultEmailSoftware.ToString());
			if (!string.IsNullOrEmpty(smtpportoutstr))
			{
				int num;
				try
				{
					num = int.Parse(smtpportoutstr);
				}
				catch
				{
					num = 25;
				}
				if (num != 25)
				{
					stringDictionary.Add("smtpportout", num.ToString());
				}
			}
			stringDictionary.Add("usessl", useSsl.ToString());
			if (!string.IsNullOrEmpty(username))
			{
				stringDictionary.Add("username", username);
			}
			if (!string.IsNullOrEmpty(userpassword))
			{
				stringDictionary.Add("userpassword", userpassword);
			}
			stringDictionary.Add("bodyhtml", sendBodyAsHtml.ToString());
			if (!string.IsNullOrEmpty(defaultfromaddress))
			{
				stringDictionary.Add("defaultfromaddress", defaultfromaddress);
			}
			return stringDictionary;
		}

		// Token: 0x040001AA RID: 426
		private BackgroundWorker _bgWorker;

		// Token: 0x040001AB RID: 427
		private string templateName = "";

		// Token: 0x040001AC RID: 428
		private string CWConnectionString;

		// Token: 0x040001AD RID: 429
		private EmailMethod emailSendMethod;
	}
}
