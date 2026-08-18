using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync.Notetaking;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.Core.Authentication;
using TechnoPro.Common.ClientManager.Core.Email;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.ClientManager.ICore.Email;
using TechnoPro.Common.UI.ClientManager.Web.Auth;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Notetaking;
using TechnoPro.Common.UI.ClientManager.Web.Core.Notetaking;
using TechnoPro.Common.UI.Web.Entity.Adapters;
using TechnoPro.Common.UI.Web.Entity.Notetaking;

namespace TechnoPro.ClockWorkWeb.admin.settings
{
	// Token: 0x02000190 RID: 400
	public class admin_settings_InstallCheck : Page
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x00004120 File Offset: 0x00002320
		private void Page_Init(object sender, EventArgs e)
		{
			WebAuthenticationAuthorizationWebClientManager.CurrentInstance.ExemptThisPageFromAuthentication(this.Page, false);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0004BF60 File Offset: 0x0004A160
		protected void Page_Load(object sender, EventArgs e)
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			bool flag = !base.Request.IsLocal;
			if (flag)
			{
				base.Response.Redirect("~/custom/misc/home.aspx");
			}
			bool flag2 = !this.Page.IsPostBack;
			if (flag2)
			{
				string[] names = Enum.GetNames(typeof(AuthenticationTypes));
				foreach (string text in names)
				{
					this.cmb_ldapAuthType.Items.Add(new ListItem(text, text));
				}
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0004BFFD File Offset: 0x0004A1FD
		protected void btn_logToTrace_Click(object sender, EventArgs e)
		{
			CWLogger.Logger.Trace("InstallCheck:SampleMessageAtUserRequest");
			this.lbl_logToTrace.Text = "Done - check logs to ensure log entry was successfully entered.";
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0004C024 File Offset: 0x0004A224
		protected void btn_testDbConnection_Click(object sender, EventArgs e)
		{
			try
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				string connectionString = clockWork.ConnectionString;
				this.lbl_connectionString.Text = ((connectionString.Length > 10) ? (connectionString.Substring(0, 10) + "...") : connectionString);
				IEncryption encryption = clockWork.Encryption;
				DataTable dataTable = clockWork.ExecuteQuery("SELECT * FROM people");
				this.lbl_testDbConnectionResults.Text = "Retrieved " + dataTable.Rows.Count.ToString() + " row(s) from the people table";
				bool flag = encryption != null;
				if (flag)
				{
					try
					{
						bool flag2 = dataTable.Rows.Count > 0;
						if (flag2)
						{
							byte[] encryptedText = (byte[])dataTable.Rows[0]["firstname"];
							string str = encryption.Decrypt(encryptedText);
							this.lbl_pwdCheck.Text = "ClockWork Db Password Check seems to have worked, the first name in the db is: " + str;
						}
						else
						{
							this.lbl_pwdCheck.Text = "ClockWork Db Password Check seems to have worked, but there are no people in the database to try it out on.";
						}
					}
					catch (Exception ex)
					{
						this.lbl_pwdCheck.Text = "ClockWork Db Password Check Failed: " + ex.Message;
					}
				}
			}
			catch (Exception ex2)
			{
				this.lbl_testDbConnectionResults.Text = ex2.Message;
				this.lbl_pwdCheck.Text = "n/a";
			}
			finally
			{
				this.p_dbchecks.Visible = true;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0004C1DC File Offset: 0x0004A3DC
		protected void btn_checkLogDb_Click(object sender, EventArgs e)
		{
			try
			{
				CWLogger.Logger.Error("Test error log entry initiated by user.");
				this.lbl_checkLogDbResults.Text = "Done. check the logs.";
			}
			catch (Exception ex)
			{
				this.lbl_checkLogDbResults.Text = ex.Message;
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0004C238 File Offset: 0x0004A438
		public void btn_encrypt_click(object sender, EventArgs e)
		{
			string text = "";
			foreach (string section in this.sections)
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					text += " <br /> ";
				}
				text += this.EncryptConfigData(section);
			}
			this.lbl_encryptMsg.Text = text;
			this.p_encryptMsg.Visible = true;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0004C2AC File Offset: 0x0004A4AC
		public void btn_decrypt_click(object sender, EventArgs e)
		{
			string text = "";
			foreach (string section in this.sections)
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					text += " <br /> ";
				}
				text += this.DecryptConfigData(section);
			}
			this.lbl_encryptMsg.Text = text;
			this.p_encryptMsg.Visible = true;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0004C320 File Offset: 0x0004A520
		private string EncryptConfigData(string section)
		{
			string result;
			try
			{
				Configuration configuration = WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
				ConfigurationSection section2 = configuration.GetSection(section);
				bool flag = section2 != null;
				if (flag)
				{
					section2.SectionInformation.ProtectSection(this.provider);
					configuration.Save();
				}
				result = "Configuration Section '" + section + "' is encrypted";
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0004C39C File Offset: 0x0004A59C
		private string DecryptConfigData(string section)
		{
			string result;
			try
			{
				Configuration configuration = WebConfigurationManager.OpenWebConfiguration(base.Request.ApplicationPath);
				ConfigurationSection section2 = configuration.GetSection(section);
				bool flag = section2 != null && section2.SectionInformation.IsProtected;
				if (flag)
				{
					section2.SectionInformation.UnprotectSection();
					configuration.Save();
				}
				result = "Configuration Section '" + section + "' is decrypted";
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			return result;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0004C420 File Offset: 0x0004A620
		public void btn_causeError_click(object sender, EventArgs e)
		{
			int num = 0;
			num = 5 / num;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0004C434 File Offset: 0x0004A634
		protected void btn_testEmail_Click(object sender, EventArgs e)
		{
			try
			{
				IEmailClientManager emailClientManager = new EmailClientManager();
				SendEmailsResp sendEmailsResp = emailClientManager.SendEmail(new TPMailMessageDTO
				{
					From = new TPMailAddressDTO
					{
						EmailAddress = this.txt_adminEmailFrom.Text
					},
					To = new List<TPMailAddressDTO>
					{
						new TPMailAddressDTO
						{
							EmailAddress = this.txt_adminEmailTo.Text
						}
					},
					Subject = "ClockWork Web Test Email",
					Body = "This email was sent because the 'Test email' button was clicked on the ClockWork InstallCheck page."
				}, "InstallCheck_TestEmail");
				bool flag = sendEmailsResp == null || sendEmailsResp.SendEmailResult == null;
				if (flag)
				{
					this.lbl_emailRes.Text = "There may have been an error - please check the logs for clarification.";
				}
				else
				{
					bool flag2 = sendEmailsResp.SendEmailResult.Status == eTPMailResultStatusDTO.CompletedSuccess;
					if (flag2)
					{
						this.lbl_emailRes.Text = "Sent successfully; please check your email.";
					}
					else
					{
						this.lbl_emailRes.Text = "Failed: " + sendEmailsResp.SendEmailResult.ToString() + "; ex=" + (sendEmailsResp.SendEmailResult.ErrorMessage ?? "NULL");
					}
				}
			}
			catch (Exception ex)
			{
				this.lbl_emailRes.Text = ex.Message;
			}
			this.p_emailRes.Visible = true;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0004C584 File Offset: 0x0004A784
		private LdapConnectionInfoDTO GetLdapConnectionInfoFromScreen()
		{
			int port;
			bool flag = !int.TryParse(this.txt_ldapPort.Text.Trim(), out port);
			if (flag)
			{
				port = 0;
			}
			int protocolVersion;
			bool flag2 = !int.TryParse(this.txt_protocolVersion.Text.Trim(), out protocolVersion);
			if (flag2)
			{
				protocolVersion = 0;
			}
			return new LdapConnectionInfoDTO
			{
				AuthType = this.cmb_ldapAuthType.SelectedValue,
				Domain = this.txt_ldapDC.Text,
				IsDoubleBinding = this.rbtns_ldaptype.SelectedValue.Equals("double"),
				IsActiveDirectory = this.rbtns_ldaptype.SelectedValue.Equals("ad"),
				LookupAttribute = this.txt_ldapLookupAttr.Text,
				Port = port,
				PreDomain = this.txt_ldapPreDC.Text,
				PreLookupAttribute = this.txt_ldapPreLookupAttr.Text,
				PreUsername = this.txt_ldapPreUsername.Text,
				PrePassword = this.txt_ldapPrePassword.Text,
				ProtocolVersion = protocolVersion,
				ReturnAttributes = this.txt_ldapReturnAttr.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries),
				ServerName = this.txt_ldapServerName.Text,
				SSL = this.chk_ldapDoubleBinding_useSsl.Checked,
				TLS = this.chk_ldapUseTls.Checked,
				DontVerifyServerCertificate = this.chk_dontVerifyServerCertificate.Checked
			};
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0004C710 File Offset: 0x0004A910
		protected void btn_showLdapSettings_Click(object sender, EventArgs e)
		{
			LdapConnectionInfoDTO ldapConnectionInfoFromScreen = this.GetLdapConnectionInfoFromScreen();
			StringDictionary stringDictionary = ldapConnectionInfoFromScreen.CreateConnectionInfoStringDictionary();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in stringDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				bool flag = stringBuilder.Length > 0;
				if (flag)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.AppendFormat("{0}={1}", dictionaryEntry.Key.ToString(), (dictionaryEntry.Value == null) ? "NULL" : dictionaryEntry.Value.ToString());
			}
			this.p_showLdapSettings.Visible = true;
			this.lbl_ldapSettings.Text = stringBuilder.ToString();
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0004C7E8 File Offset: 0x0004A9E8
		protected void btn_testLdap_Click(object sender, EventArgs e)
		{
			LdapConnectionInfoDTO ldapConnectionInfoFromScreen = this.GetLdapConnectionInfoFromScreen();
			ILdapClientManager ldapClientManager = new LdapClientManager();
			LdapAuthenticationResultDTO ldapAuthenticationResultDTO = ldapClientManager.LdapLogin(ldapConnectionInfoFromScreen, this.txt_ldapUsername.Text, this.txt_ldapPassword.Text);
			this.lbl_ldapResults.Visible = true;
			bool isAuthenticated = ldapAuthenticationResultDTO.IsAuthenticated;
			if (isAuthenticated)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = ldapAuthenticationResultDTO.ReturnAttributes != null;
				if (flag)
				{
					foreach (KeyValuePair<string, string> keyValuePair in ldapAuthenticationResultDTO.ReturnAttributes)
					{
						bool flag2 = stringBuilder.Length > 0;
						if (flag2)
						{
							stringBuilder.Append("<br />");
						}
						stringBuilder.AppendFormat("{0}={1}", keyValuePair.Key, keyValuePair.Value ?? "NULL");
					}
				}
				bool flag3 = stringBuilder.Length < 1;
				if (flag3)
				{
					stringBuilder.Append("None.");
				}
				this.lbl_ldapResults.Text = string.Format("Success<br />Return attributes<br />==================<br />{0}", stringBuilder.ToString());
			}
			else
			{
				this.lbl_ldapResults.Text = string.Format("FAIL: Message={0}:ReturnAttributesCount={1}", ldapAuthenticationResultDTO.ErrorMessage ?? "NULL", (ldapAuthenticationResultDTO.ReturnAttributes == null) ? "NULL" : ldapAuthenticationResultDTO.ReturnAttributes.Count.ToString());
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0004C964 File Offset: 0x0004AB64
		protected void btn_generateError_Click(object sender, EventArgs e)
		{
			double num = 0.0;
			base.Response.Write((5.0 / num).ToString());
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0004C99C File Offset: 0x0004AB9C
		protected void btn_testLogging_Click(object sender, EventArgs e)
		{
			try
			{
				CWLogger.Logger.Info("InstallCheck:This is a test of the logging system");
				this.lbl_misc_results.Text = "Done.";
			}
			catch (Exception ex)
			{
				this.lbl_misc_results.Text = "Error: " + ex.ToString();
			}
			finally
			{
				this.p_misc_results.Visible = true;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0004CA20 File Offset: 0x0004AC20
		protected void btn_notetakerDataSyncTest_Click(object sender, EventArgs e)
		{
			INotetakingClientDataSyncWebClientManager notetakingClientDataSyncWebClientManager = new NotetakingClientDataSyncWebClientManager();
			GetNotetakerInfoAndCoursesInfo getNotetakerInfoAndCoursesInfo;
			NotetakerWithExternalCoursesDTO notetakerAndCourseInfo = notetakingClientDataSyncWebClientManager.GetNotetakerAndCourseInfo(true, this.Page, out getNotetakerInfoAndCoursesInfo);
			this.lbl_notetakerDataSync_name.Text = "Name: " + ((notetakerAndCourseInfo == null || notetakerAndCourseInfo.Notetaker == null) ? "NULL" : notetakerAndCourseInfo.Notetaker.Person.GetName());
			bool flag = notetakerAndCourseInfo != null && notetakerAndCourseInfo.ExternalCourses != null;
			string text;
			if (flag)
			{
				text = string.Join(" ", notetakerAndCourseInfo.ExternalCourses.ToList<DataSyncExternalCourseDTO>().ConvertAll<string>((DataSyncExternalCourseDTO g) => (g.Subject ?? "") + " " + (g.Course ?? "")).ToArray());
			}
			else
			{
				text = "NULL";
			}
			this.lbl_notetakerDataSync_courses.Text = "Courses: <ul>" + ((notetakerAndCourseInfo == null || notetakerAndCourseInfo.ExternalCourses == null) ? "<li>NULL</li>" : text) + "</ul>";
			this.lbl_notetakerDataSync_source.Text = getNotetakerInfoAndCoursesInfo.Source.ToString();
			this.lbl_notetakerDataSync_snum.Text = (getNotetakerInfoAndCoursesInfo.StudentNumber ?? "NULL");
			this.lbl_notetakerDataSync_username.Text = (getNotetakerInfoAndCoursesInfo.Username ?? "NULL");
			this.p_notetakerDataSync_results.Visible = true;
		}

		// Token: 0x04000867 RID: 2151
		private string provider = "RSAProtectedConfigurationProvider";

		// Token: 0x04000868 RID: 2152
		private string[] sections = new string[]
		{
			"connectionStrings",
			"system.net/mailSettings/smtp",
			"appSettings"
		};

		// Token: 0x04000869 RID: 2153
		protected Label lblTitle;

		// Token: 0x0400086A RID: 2154
		protected Panel p_info;

		// Token: 0x0400086B RID: 2155
		protected ScriptManager ToolkitScriptManager1;

		// Token: 0x0400086C RID: 2156
		protected Panel p_disabled;

		// Token: 0x0400086D RID: 2157
		protected Label lbl_disabled;

		// Token: 0x0400086E RID: 2158
		protected Accordion acc1;

		// Token: 0x0400086F RID: 2159
		protected AccordionPane pane_cwdb;

		// Token: 0x04000870 RID: 2160
		protected Panel p_checkDbConnection;

		// Token: 0x04000871 RID: 2161
		protected Button btn_testDbConnection;

		// Token: 0x04000872 RID: 2162
		protected Label lbl_connectionString;

		// Token: 0x04000873 RID: 2163
		protected Panel p_dbchecks;

		// Token: 0x04000874 RID: 2164
		protected Label lbl_testDbConnectionResults;

		// Token: 0x04000875 RID: 2165
		protected Label lbl_pwdCheck;

		// Token: 0x04000876 RID: 2166
		protected AccordionPane pane_outgoingemail;

		// Token: 0x04000877 RID: 2167
		protected Panel Panel2;

		// Token: 0x04000878 RID: 2168
		protected Label lbl_emailfrom;

		// Token: 0x04000879 RID: 2169
		protected TextBox txt_adminEmailFrom;

		// Token: 0x0400087A RID: 2170
		protected Label lbl_emailto;

		// Token: 0x0400087B RID: 2171
		protected TextBox txt_adminEmailTo;

		// Token: 0x0400087C RID: 2172
		protected Button btn_testEmail;

		// Token: 0x0400087D RID: 2173
		protected Panel p_emailRes;

		// Token: 0x0400087E RID: 2174
		protected Label lbl_emailRes;

		// Token: 0x0400087F RID: 2175
		protected AccordionPane pane_logging;

		// Token: 0x04000880 RID: 2176
		protected Panel Panel1;

		// Token: 0x04000881 RID: 2177
		protected Button btn_checkLogDb;

		// Token: 0x04000882 RID: 2178
		protected Button btn_causeError;

		// Token: 0x04000883 RID: 2179
		protected Label lbl_checkLogDbConnectionString;

		// Token: 0x04000884 RID: 2180
		protected Label lbl_checkLogDbResults;

		// Token: 0x04000885 RID: 2181
		protected Button btn_logToTrace;

		// Token: 0x04000886 RID: 2182
		protected Label lbl_logToTrace;

		// Token: 0x04000887 RID: 2183
		protected AccordionPane pane_ldap;

		// Token: 0x04000888 RID: 2184
		protected Label lbl_ldapResults;

		// Token: 0x04000889 RID: 2185
		protected Panel p_ldap2;

		// Token: 0x0400088A RID: 2186
		protected RadioButtonList rbtns_ldaptype;

		// Token: 0x0400088B RID: 2187
		protected CheckBox chk_ldapDoubleBinding_useSsl;

		// Token: 0x0400088C RID: 2188
		protected CheckBox chk_ldapUseTls;

		// Token: 0x0400088D RID: 2189
		protected CheckBox chk_dontVerifyServerCertificate;

		// Token: 0x0400088E RID: 2190
		protected Label Label5;

		// Token: 0x0400088F RID: 2191
		protected TextBox txt_protocolVersion;

		// Token: 0x04000890 RID: 2192
		protected Label lbl_protocolInfo;

		// Token: 0x04000891 RID: 2193
		protected Label lbl_ldapServerName;

		// Token: 0x04000892 RID: 2194
		protected TextBox txt_ldapServerName;

		// Token: 0x04000893 RID: 2195
		protected Label lbl_ldapPort;

		// Token: 0x04000894 RID: 2196
		protected TextBox txt_ldapPort;

		// Token: 0x04000895 RID: 2197
		protected Label lbl_ldapDC;

		// Token: 0x04000896 RID: 2198
		protected TextBox txt_ldapDC;

		// Token: 0x04000897 RID: 2199
		protected Label lbl_ldapLookupAttr;

		// Token: 0x04000898 RID: 2200
		protected TextBox txt_ldapLookupAttr;

		// Token: 0x04000899 RID: 2201
		protected Label lbl_ldapReturnAttr;

		// Token: 0x0400089A RID: 2202
		protected TextBox txt_ldapReturnAttr;

		// Token: 0x0400089B RID: 2203
		protected Label lbl_ldapAuthType;

		// Token: 0x0400089C RID: 2204
		protected DropDownList cmb_ldapAuthType;

		// Token: 0x0400089D RID: 2205
		protected Label lbl_ldapUsername;

		// Token: 0x0400089E RID: 2206
		protected TextBox txt_ldapUsername;

		// Token: 0x0400089F RID: 2207
		protected Label lbl_ldapPassword;

		// Token: 0x040008A0 RID: 2208
		protected TextBox txt_ldapPassword;

		// Token: 0x040008A1 RID: 2209
		protected Panel p_ldapDoubleBinding;

		// Token: 0x040008A2 RID: 2210
		protected Label Label1;

		// Token: 0x040008A3 RID: 2211
		protected TextBox txt_ldapPreDC;

		// Token: 0x040008A4 RID: 2212
		protected Label Label2;

		// Token: 0x040008A5 RID: 2213
		protected TextBox txt_ldapPreLookupAttr;

		// Token: 0x040008A6 RID: 2214
		protected Label Label3;

		// Token: 0x040008A7 RID: 2215
		protected TextBox txt_ldapPreUsername;

		// Token: 0x040008A8 RID: 2216
		protected Label Label4;

		// Token: 0x040008A9 RID: 2217
		protected TextBox txt_ldapPrePassword;

		// Token: 0x040008AA RID: 2218
		protected Button btn_testLdap;

		// Token: 0x040008AB RID: 2219
		protected Button btn_showLdapSettings;

		// Token: 0x040008AC RID: 2220
		protected Panel p_showLdapSettings;

		// Token: 0x040008AD RID: 2221
		protected TextBox lbl_ldapSettings;

		// Token: 0x040008AE RID: 2222
		protected AccordionPane pane_configencrypt;

		// Token: 0x040008AF RID: 2223
		protected Panel p_configEncrypt;

		// Token: 0x040008B0 RID: 2224
		protected Button btn_encrypt;

		// Token: 0x040008B1 RID: 2225
		protected Button btn_decrypt;

		// Token: 0x040008B2 RID: 2226
		protected Panel p_encryptMsg;

		// Token: 0x040008B3 RID: 2227
		protected Label lbl_encryptMsg;

		// Token: 0x040008B4 RID: 2228
		protected AccordionPane AccordionPane1;

		// Token: 0x040008B5 RID: 2229
		protected Panel Panel3;

		// Token: 0x040008B6 RID: 2230
		protected Button btn_notetakerDataSyncTest;

		// Token: 0x040008B7 RID: 2231
		protected Panel p_notetakerDataSync_results;

		// Token: 0x040008B8 RID: 2232
		protected Label lbl_notetakerDataSync_name;

		// Token: 0x040008B9 RID: 2233
		protected Label lbl_notetakerDataSync_courses;

		// Token: 0x040008BA RID: 2234
		protected Label lbl_notetakerDataSync_source;

		// Token: 0x040008BB RID: 2235
		protected Label lbl_notetakerDataSync_snum;

		// Token: 0x040008BC RID: 2236
		protected Label lbl_notetakerDataSync_username;

		// Token: 0x040008BD RID: 2237
		protected AccordionPane pane_misc;

		// Token: 0x040008BE RID: 2238
		protected Panel p_misc;

		// Token: 0x040008BF RID: 2239
		protected Button btn_generateError;

		// Token: 0x040008C0 RID: 2240
		protected Button btn_testLogging;

		// Token: 0x040008C1 RID: 2241
		protected Panel p_misc_results;

		// Token: 0x040008C2 RID: 2242
		protected Label lbl_misc_results;
	}
}
