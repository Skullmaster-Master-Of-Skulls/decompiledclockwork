using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.ClockWorkServer;
using TechnoPro.Common.Core.FileStorages;
using TechnoPro.Common.Core.Institution;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.Core.Updates.Adapters;
using TechnoPro.Common.Core.Updates.ExternalLogsProvider;
using TechnoPro.Common.Core.UserAccount;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.ClockWorkServer;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.ICore.UserAccount;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.UserAccount.LoginTracking;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x0200000D RID: 13
	public class UpdateExecutiveManager : IExecuteUpdateManager
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00004790 File Offset: 0x00002990
		public void ExecuteUpdates()
		{
			IServerInstanceInfoManager serverInstanceInfoManager = new ServerInstanceInfoManager();
			foreach (ServerInstanceInfo serverInstanceInfo in serverInstanceInfoManager.GetServerInstancesInfo())
			{
				bool flag = !serverInstanceInfo.ContainsPatchCredentials;
				if (!flag)
				{
					serverInstanceInfo.Version = serverInstanceInfo.Version.FormatVersion();
					this.ExecuteUpdate(serverInstanceInfo);
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000480C File Offset: 0x00002A0C
		private void ExecuteUpdate(ServerInstanceInfo instance)
		{
			CWLogger.Logger.Info("UpdateExecutiveManager::ExecuteUpdates:: /~~~~~~~~~~~~~~~~ Started for {0}... ~~~~~~~~~~~~~~~~~~~~/", instance.InstanceName);
			try
			{
				instance.SetupDatabaseLayerFactory();
				OperationContext opContext = new OperationContext
				{
					WhoAmI = 0,
					AppContext = new ApplicationContext
					{
						ExecutingPath = UpdateExecutiveManager.GetUpdatingSystemPath()
					}
				};
				InstitutionManager institutionManager = new InstitutionManager
				{
					OpContext = opContext
				};
				string institutionUniqueName = institutionManager.GetInstitutionUniqueName();
				string text = Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, institutionUniqueName);
				IExternalLogManager externalLogManager = new AzureUpdatesLogManager(institutionUniqueName);
				bool flag = Directory.Exists(text);
				if (flag)
				{
					this.ImportLicense(text, externalLogManager);
					this.ImportReports(instance, text, externalLogManager);
				}
				IExecuteUpdateManager executeUpdateManager = new ExecuteUpdateManager(instance)
				{
					ExternalLogManager = externalLogManager
				};
				executeUpdateManager.ExecuteUpdates();
				this.LoggingUserLogins(externalLogManager);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("UpdateExecutiveManager::ExecuteUpdates:: Instance Name={1}, {0}", ex.ToString(), instance.InstanceName), ex);
			}
			CWLogger.Logger.Info("UpdateExecutiveManager::ExecuteUpdates:: /~~~~~~~~~~~~~~~~ Finished for {0} ~~~~~~~~~~~~~~~~~~~~/", instance.InstanceName);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004920 File Offset: 0x00002B20
		private void LoggingUserLogins(IExternalLogManager logManager)
		{
			try
			{
				IUserLoginTrackingManager userLoginTrackingManager = new UserLoginTrackingManager(new OperationContext
				{
					WhoAmI = 0
				});
				IList<LoginInfo> list = userLoginTrackingManager.LoadLoginInfosByDateRange(DateTime.Now.AddHours(-24.0), DateTime.Now);
				logManager.Log("UserLoginInfo: ************ Begin last 24h user login info **************");
				foreach (LoginInfo loginInfo in list)
				{
					string format = "UserLoginInfo: PersonId={0}, IP={1}, ClockWorkClientVersion={2}, DotNetVersionsOnClient={3}, LogonOn={4}";
					object[] array = new object[5];
					array[0] = loginInfo.PersonId;
					array[1] = (loginInfo.Ip ?? "NULL");
					array[2] = ((loginInfo.ClockWorkVersion != null) ? loginInfo.ClockWorkVersion.ToString() : "NULL");
					int num = 3;
					object obj;
					if (loginInfo.NetVersions == null)
					{
						obj = "NULL";
					}
					else
					{
						obj = (from v in loginInfo.NetVersions
						select v.ToString().Substring(1).Replace("_", ".")).ToList<string>().CommaSeparatedValues<string>();
					}
					array[num] = obj;
					array[4] = loginInfo.LoginDate;
					logManager.Log(string.Format(format, array));
				}
				logManager.Log("UserLoginInfo: ************ End login info **************");
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("UpdateExecutiveManager::LoggingUserLogins:: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004ABC File Offset: 0x00002CBC
		private void ImportLicense(string clientFolder, IExternalLogManager logManager)
		{
			try
			{
				string searchPattern = string.Format("ClockWork license.*.cwk", Array.Empty<object>());
				List<string> list = Directory.GetFiles(clientFolder, searchPattern).ToList<string>();
				bool flag = list.Count == 0;
				if (!flag)
				{
					string text = list[0];
					bool flag2 = !File.Exists(text);
					if (!flag2)
					{
						string fileName = Path.GetFileName(text);
						IMiscSafeManager miscSafeManager = new MiscSafeManager();
						string value = miscSafeManager.GetValue("LastClockWorkLicenseImported");
						bool flag3 = fileName != null && (string.IsNullOrEmpty(value) || !fileName.Equals(value, StringComparison.OrdinalIgnoreCase));
						if (flag3)
						{
							CWLogger.Logger.Info("UpdateExecutiveManager::ImportLicense: License '{0}' was found", fileName);
							ILicensingManager licensingManager = new LicensingManager();
							licensingManager.ImportLicenseFromFile(text);
							CWLogger.Logger.Info("UpdateExecutiveManager::ImportLicense: License '{0}' was successfully imported", fileName);
							logManager.Log(string.Format("LicenseSystem: ********** License '{0}' was successfully imported ***********", fileName));
							miscSafeManager.Save("LastClockWorkLicenseImported", fileName);
						}
						else
						{
							CWLogger.Logger.Trace("UpdateExecutiveManager::No new license package found", fileName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("UpdateExecutiveManager::ImportLicense: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004BF8 File Offset: 0x00002DF8
		private void ImportReports(ServerInstanceInfo instanceInfo, string clientFolder, IExternalLogManager logManager)
		{
			try
			{
				string searchPattern = string.Format("*.cwr", Array.Empty<object>());
				List<string> list = Directory.GetFiles(clientFolder, searchPattern).ToList<string>();
				bool flag = list.Count == 0;
				if (!flag)
				{
					string executingPath = Path.Combine(instanceInfo.InstallationPath, "bin");
					IReportManager reportManager = new ReportManager(new OperationContext
					{
						AppContext = new ApplicationContext
						{
							ExecutingPath = executingPath
						},
						WhoAmI = 0
					});
					IFileSignManager fileSignManager = new FileSignManager();
					List<int> list2 = new List<int>();
					foreach (string text in list)
					{
						bool flag2 = File.Exists(text);
						if (flag2)
						{
							string text2 = Path.Combine(FileSystem.GetTemporalFolderInTechnoPro(), Path.GetFileName(text));
							try
							{
								fileSignManager.DecryptAndVerifyUsingFileSystem(text, text2);
								bool flag3 = File.Exists(text2);
								if (flag3)
								{
									string xml = File.ReadAllText(text2);
									IDictionary<string, int> dictionary = reportManager.ImportReportsFromXmlForUpdatingSystem(xml, 2000000033);
									list2.AddRange(from id in dictionary.Values
									where id > 0
									select id);
								}
							}
							catch (Exception ex)
							{
								CWLogger.Logger.ErrorException(string.Format("UpdateExecutiveManager::ImportReports: Report '{0}' failed to import : {1}", text, ex.ToString()), ex);
							}
						}
					}
					bool flag4 = list2.Count > 0;
					if (flag4)
					{
						this.SendNotificationEmailsAsync(Setting.AUTOMATICUPDATING_Email_SuccessfullyImportedReports, new Dictionary<string, string>
						{
							{
								"importeddatetime",
								DateTime.Now.ToString("MMM dd, yyyy hh:mm tt")
							},
							{
								"reportidlist",
								list2.CommaSeparatedValues<int>()
							}
						});
						logManager.Log(string.Format("ReportSystem: Successfully imported {0} report(s) into their system. Report Ids = '{1}'", list2.Count, list2.CommaSeparatedValues<int>()));
					}
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException(string.Format("UpdateExecutiveManager::ImportReports: {0}", ex2.ToString()), ex2);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004E58 File Offset: 0x00003058
		private static string GetUpdatingSystemPath()
		{
			return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004E7C File Offset: 0x0000307C
		[DebuggerStepThrough]
		private Task SendNotificationEmailsAsync(Setting emailSetting, Dictionary<string, string> customDictionary = null)
		{
			UpdateExecutiveManager.<SendNotificationEmailsAsync>d__6 <SendNotificationEmailsAsync>d__ = new UpdateExecutiveManager.<SendNotificationEmailsAsync>d__6();
			<SendNotificationEmailsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SendNotificationEmailsAsync>d__.<>4__this = this;
			<SendNotificationEmailsAsync>d__.emailSetting = emailSetting;
			<SendNotificationEmailsAsync>d__.customDictionary = customDictionary;
			<SendNotificationEmailsAsync>d__.<>1__state = -1;
			<SendNotificationEmailsAsync>d__.<>t__builder.Start<UpdateExecutiveManager.<SendNotificationEmailsAsync>d__6>(ref <SendNotificationEmailsAsync>d__);
			return <SendNotificationEmailsAsync>d__.<>t__builder.Task;
		}
	}
}
