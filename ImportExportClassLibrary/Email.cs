using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SettingsPermissions;

namespace ImportExportClassLibrary
{
	// Token: 0x02000034 RID: 52
	public class Email
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000C970 File Offset: 0x0000B970
		public static string GetEmailTemplateFromUser(Form parentForm, string emailTemplatesDirectory)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Please choose the email template file to use:";
			openFileDialog.InitialDirectory = emailTemplatesDirectory;
			DialogResult dialogResult = openFileDialog.ShowDialog(parentForm);
			if (dialogResult == DialogResult.OK)
			{
				return openFileDialog.FileName;
			}
			return null;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000C9AC File Offset: 0x0000B9AC
		public static void EmailItemsOneEmail(Form parentForm, ArrayList items, string StartDirectory, Settings settings)
		{
			string settingString = settings.GetSettingString(22, Path.Combine(StartDirectory, "templates\\email"));
			string emailTemplateFromUser = Email.GetEmailTemplateFromUser(parentForm, settingString);
			if (emailTemplateFromUser != null)
			{
				string text = TemplatesClass.FillTemplate(items, emailTemplateFromUser);
				if (File.Exists(text))
				{
					Email.LaunchEmailer(text, settings, StartDirectory, settingString);
				}
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C9F4 File Offset: 0x0000B9F4
		public static void LaunchEmailer(string tempFilename, Settings settings, string StartDirectory, string emailTemplatesDirectory)
		{
			string text = "emailtemplatesdirectory=" + emailTemplatesDirectory;
			string settingString = settings.GetSettingString(101);
			if (settingString.Length > 0)
			{
				text = text + " smtpserverout=" + settingString;
			}
			int setting = settings.GetSetting(100);
			text = text + " usedefaultemailsoftware=" + (setting == 1).ToString();
			string settingString2 = settings.GetSettingString(102);
			if (settingString2.Length > 0)
			{
				int num;
				try
				{
					num = int.Parse(settingString2);
				}
				catch
				{
					num = 25;
				}
				if (num != 25)
				{
					text = text + " smtpportout=" + num.ToString();
				}
			}
			int setting2 = settings.GetSetting(103);
			text = text + " usessl=" + (setting2 == 1).ToString();
			string settingString3 = settings.GetSettingString(104);
			if (settingString3 != null && settingString3.Length > 0)
			{
				text = text + " username=" + settingString3;
			}
			string settingString4 = settings.GetSettingString(105);
			if (settingString4 != null && settingString4.Length > 0)
			{
				text = text + " userpassword=" + settingString4;
			}
			int setting3 = settings.GetSetting(106);
			text = text + " bodyhtml=" + (setting3 == 1).ToString();
			string settingString5 = settings.GetSettingString(107);
			if (settingString5.Length > 0)
			{
				text = text + " defaultfromaddress=" + settingString5;
			}
			text = text + " file=\"" + tempFilename + "\"";
			Process.Start(Path.Combine(StartDirectory, "tpemailer.exe"), text);
		}
	}
}
