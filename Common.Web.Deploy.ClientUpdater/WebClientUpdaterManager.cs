using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Updates;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.Startup;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Startup;
using TechnoPro.Common.Compression;
using TechnoPro.Common.Core.ConnectionString;
using TechnoPro.Common.Core.FileStorages;
using TechnoPro.Common.Core.InstanceInfo;
using TechnoPro.Common.Core.Updates.Adapters;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.ConnectionString;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Security.Hashing;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Web.Deploy.ClientUpdater
{
	// Token: 0x02000002 RID: 2
	public class WebClientUpdaterManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		private string WebAppName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public TimeSpan RunningTime { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public bool IsRunning { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002083 File Offset: 0x00000283
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000208B File Offset: 0x0000028B
		public bool TestMode { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002094 File Offset: 0x00000294
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000209C File Offset: 0x0000029C
		public bool WaitTimeBeforeCheckingUpdates { get; set; }

		// Token: 0x0600000B RID: 11 RVA: 0x000020A5 File Offset: 0x000002A5
		public WebClientUpdaterManager(string webAppName)
		{
			this.WebAppName = webAppName;
			this.RunningTime = TimeSpan.FromHours(4.0);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020C8 File Offset: 0x000002C8
		public void Init()
		{
			CWLogger.Logger.Info("WebClientUpdaterManager:Init: ClockWorkWeb App vDir='{0}'", this.WebAppName ?? string.Empty);
			this.GetValuesFromRegistry();
			this.GetClockWorkServerConnectionInfo();
			bool flag = ((IClientStartupClientManager)new ClientStartupClientManager()).CheckConnectivityToServer();
			CWLogger.Logger.Info("WebClientUpdaterManager:Init: Is connected to ClockWorkServer={0}", flag);
			if (flag)
			{
				this.InstallClockWorkServerCertificate();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002124 File Offset: 0x00000324
		public void Run()
		{
			try
			{
				CWLogger.Logger.Info("WebClientUpdaterManager::Run: Started for '{0}'...", this.WebAppName);
				if (((IClientStartupClientManager)new ClientStartupClientManager()).CheckConnectivityToServer())
				{
					this.RunUpdateChecks();
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("WebClientUpdaterManager::Running scheduled update failed: {0}", ex.ToString()), ex);
			}
			finally
			{
				CWLogger.Logger.Info("WebClientUpdaterManager::Run: Ended for '{0}'...", this.WebAppName);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021AC File Offset: 0x000003AC
		private void RunUpdateChecks()
		{
			if (this.IsRunning)
			{
				CWLogger.Logger.Warn("WebClientUpdaterManager::Updating process abort because previous one it is still running ...");
				return;
			}
			if (string.IsNullOrEmpty(this.WebAppName))
			{
				CWLogger.Logger.Error("WebClientUpdaterManager::WebAppName is NULL or empty");
				return;
			}
			WebInstanceInfo webInstanceInfo = null;
			try
			{
				this.IsRunning = true;
				webInstanceInfo = new WebInstanceInfoManager().GetWebInstanceInfo(this.WebAppName);
				if (webInstanceInfo == null)
				{
					CWLogger.Logger.Error("WebClientUpdaterManager:: web app instance is NULL");
				}
				else
				{
					CWLogger.Logger.Info("WebClientUpdaterManager:: Checking if there is an update available for '{0}', currentversion='{1}'", webInstanceInfo.InstanceName, webInstanceInfo.Version);
					UpdateRequiredRequest updateRequiredRequest = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequiredRequest>();
					updateRequiredRequest.ClientVersion = webInstanceInfo.Version;
					updateRequiredRequest.FileType = "ClockWorkWeb update";
					UpdateRequiredResponse updateRequiredResponse = new ClientStartupClientManager().IsUpdateRequired(updateRequiredRequest);
					if (updateRequiredResponse.IsUpdateRequired)
					{
						CWLogger.Logger.Info("WebClientUpdaterManager:: Version '{0}' for app '{1}' is available on the server", updateRequiredResponse.CurrentVersionOnServer, webInstanceInfo.InstanceName);
						ClockWorkHashAuthenticationDTO clockWorkHashingAuth = this.GetClockWorkHashingAuth();
						CWLogger.Logger.Info("WebClientUpdaterManager:: Getting '{1}' version '{0}' from the server", updateRequiredResponse.CurrentVersionOnServer, webInstanceInfo.InstanceName);
						IClientUpdateClientManager clientUpdateClientManager = new ClientUpdateClientManager();
						GetClientUpdateReq getClientUpdateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetClientUpdateReq>();
						getClientUpdateReq.ClientVersion = webInstanceInfo.Version;
						getClientUpdateReq.FileType = "ClockWorkWeb update";
						getClientUpdateReq.HashAuthentication = clockWorkHashingAuth;
						GetClientUpdateResp clientUpdate = clientUpdateClientManager.GetClientUpdate(getClientUpdateReq);
						FileSystemStructure file = clientUpdate.File;
						if (file != null)
						{
							CWLogger.Logger.Info("WebClientUpdaterManager:: Version '{0}' was successfully download from the server", updateRequiredResponse.CurrentVersionOnServer);
							string temporalFolderInTechnoPro = FileSystem.GetTemporalFolderInTechnoPro();
							string text = Path.Combine(temporalFolderInTechnoPro, webInstanceInfo.InstanceName + "_backup");
							if (!FileSystem.CopyDirectory(webInstanceInfo.InstallationPath, text, true))
							{
								CWLogger.Logger.Error("WebClientUpdaterManager::RunUpdateChecks: Failed while creating {0} files backup", webInstanceInfo.InstanceName);
							}
							else
							{
								CWLogger.Logger.Info("WebClientUpdaterManager:: Backup of '{0}' folder was successfully created on '{1}'", webInstanceInfo.InstallationPath, text);
								if (webInstanceInfo != null)
								{
									webInstanceInfo.StopApplicationPool();
								}
								string text2;
								if (file.BinaryData != null && file.BinaryData.Length != 0)
								{
									text2 = Path.Combine(FileSystem.GetTemporalFolderInTechnoPro(), string.Format("{0}.{1}", file.Filename, file.Extension));
									File.WriteAllBytes(text2, file.BinaryData);
								}
								else
								{
									text2 = file.Filename;
								}
								IFileSignManager fileSignManager = new FileSignManager();
								string tempFileName = FileSystem.GetTempFileName(Path.GetExtension(text2));
								try
								{
									fileSignManager.DecryptAndVerifyUsingFileSystem(text2, tempFileName);
								}
								catch (Exception ex)
								{
									CWLogger.Logger.ErrorException(string.Format("WebClientUpdaterManager::RunUpdateChecks:: Verify file signature failed. {0}", ex.ToString()), ex);
									throw;
								}
								string text3 = Path.Combine(temporalFolderInTechnoPro, webInstanceInfo.InstanceName);
								CompressDataAdapter.expandFolder(tempFileName, text3);
								webInstanceInfo.ExecutePreUpdateCustomAction(text3);
								this.UpdateClockWorkWebWizard(webInstanceInfo, text3);
								if (!FileSystem.CopyDirectory(text3, webInstanceInfo.InstallationPath, true))
								{
									CWLogger.Logger.Error("WebClientUpdaterManager::RunUpdateChecks:: Failed when copying new files, rolling back installation ...");
									FileSystem.CopyDirectoryAndContinueIfFailing(text, webInstanceInfo.InstallationPath, true);
								}
								else
								{
									CWLogger.Logger.Info("WebClientUpdaterManager:: Version '{0}' for app '{1}' was successfully applied on '{2}'", updateRequiredResponse.CurrentVersionOnServer, webInstanceInfo.InstanceName, webInstanceInfo.InstallationPath);
									this.UpdateService(webInstanceInfo.ProgramFilesFolder);
									new RegistryHelper().WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clientUpdate.File.Version, new string[]
									{
										"ClockWorkWeb",
										webInstanceInfo.InstanceName,
										"Version"
									});
									webInstanceInfo.ExecutePostUpdateCustomAction(text3);
								}
							}
						}
					}
					else
					{
						CWLogger.Logger.Info("WebClientUpdaterManager:: No new updates available for app '{0}' on the server, currentversion='{1}'", webInstanceInfo.InstanceName, webInstanceInfo.Version);
					}
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException(string.Format("WebClientUpdaterManager::RunUpdateChecks:: Getting new updates failed: {0}", ex2.ToString()), ex2);
			}
			finally
			{
				if (webInstanceInfo != null)
				{
					webInstanceInfo.StartApplicationPool();
				}
				this.IsRunning = false;
				FileSystem.CleanTechnoProTempFolder();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002580 File Offset: 0x00000780
		private void UpdateService(string startupPath)
		{
			try
			{
				string text = Path.Combine(startupPath, "UpdaterService_upd");
				string text2 = Path.Combine(startupPath, "UpdaterService_upd2");
				if (Directory.Exists(text))
				{
					FileSystem.CopyDirectory(text, text2, true);
					FileSystem.DeleteDirectory(text, true);
					CWLogger.Logger.Info("WebClientUpdaterManager::UpdateService:: Updating service ...");
					foreach (string text3 in Directory.GetFiles(text2))
					{
						string newFilename = Path.Combine(startupPath, Path.Combine("UpdaterService", Path.GetFileName(text3)));
						FileSystem.MoveFile(text3, newFilename, MoveFileFlags.MOVEFILE_REPLACE_EXISTING | MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
						CWLogger.Logger.Info("WebClientUpdaterManager::UpdateService:: File '{0}' was marked for updating after the system reboot", text3);
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("WebClientUpdaterManager::UpdateService:: {0}", ex), ex);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000264C File Offset: 0x0000084C
		private ClockWorkHashAuthenticationDTO GetClockWorkHashingAuth()
		{
			DateTime now = DateTime.Now;
			int num = new Random().Next();
			string password = string.Concat(new object[]
			{
				"ClockWorkWebUpdatingSystem",
				now.ToString("yyyy-MM-dd hh:mm:ss.fff"),
				num,
				"$Ys5+TBS!)yV~XW|B%>+\\S2zBY'^sKx,j~7zJj95#<G%l4)A'wnV^6d/=M=;UK#%x+%$SQ#F';v|3Ty_~?/|kY!.NK|eyXT6I}.L~|0_FgfK]\\!6o/9,/HpE~De93}uB"
			});
			string hashValue = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).CreateHash(password, null);
			return new ClockWorkHashAuthenticationDTO
			{
				HashValue = hashValue,
				Seed = num,
				StampTime = now,
				Username = "ClockWorkWebUpdatingSystem"
			};
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000026D4 File Offset: 0x000008D4
		private void InstallClockWorkServerCertificate()
		{
			CertificateInfo clockWorkServerCertificate = ((IClientStartupClientManager)new ClientStartupClientManager()).GetClockWorkServerCertificate();
			ObjectFactory.Resolve<ClientCache>().ClientClockWorkServerConnectionInfo.Certificate = clockWorkServerCertificate;
			if (!string.IsNullOrEmpty(clockWorkServerCertificate.CertificatePublicKey))
			{
				X509Certificate2 x509Certificate = new X509Certificate2(Convert.FromBase64String(clockWorkServerCertificate.CertificatePublicKey));
				CWLogger.Logger.Info("WebClientUpdaterManager:InstallClockWorkServerCertificate: Thumbprint='{0}'", x509Certificate.Thumbprint ?? string.Empty);
				X509CertificateAdapter.RemoveByThumbprint(StoreName.TrustedPeople, StoreLocation.LocalMachine, x509Certificate.Thumbprint);
				X509CertificateAdapter.RemoveByThumbprint(StoreName.Root, StoreLocation.LocalMachine, x509Certificate.Thumbprint);
				X509CertificateAdapter.RemoveByThumbprint(StoreName.TrustedPeople, StoreLocation.CurrentUser, x509Certificate.Thumbprint);
				x509Certificate.Install(StoreName.TrustedPeople, StoreLocation.LocalMachine);
				x509Certificate.Install(StoreName.Root, StoreLocation.LocalMachine);
				x509Certificate.Install(StoreName.TrustedPeople, StoreLocation.CurrentUser);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002778 File Offset: 0x00000978
		private void GetClockWorkServerConnectionInfo()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			ClockWorkConnectionString connectionString = new ClockWorkConnectionStringManager(new OperationContext
			{
				WhoAmI = 0
			}).GetConnectionString(string.Format("{0}.{1}", eTechnoProProductNames.ClockWorkWeb, this.WebAppName));
			if (connectionString == null)
			{
				string text = new RegistryHelper().ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkWeb",
					this.WebAppName,
					"ClockWorkServer",
					"DiscoveryServiceEndpoints",
					"PreferedEndpointConnection"
				});
				if (!string.IsNullOrEmpty(text))
				{
					CWLogger.Logger.Info("WebClientUpdaterManager::GetClockWorkServerConnectionInfo: {0}", text);
					Uri uri = new Uri(text);
					clientCache.ClientClockWorkServerConnectionInfo = new ClockWorkServerPreferredConnectionInfo
					{
						ExternalHostname = uri.Host,
						Hostname = uri.Host,
						Port = uri.Port,
						ExternalPort = uri.Port,
						VirtualDirectory = uri.PathAndQuery.Split(new string[]
						{
							"/"
						}, StringSplitOptions.RemoveEmptyEntries).First<string>(),
						BindingType = uri.GetBindingType(),
						IISVersion = InternetInformationServicesVersion.IIS7
					};
					return;
				}
			}
			else
			{
				CWLogger.Logger.Info("WebClientUpdaterManager::GetClockWorkServerConnectionInfo: {0}", connectionString.ToString());
				clientCache.ClientClockWorkServerConnectionInfo = new ClockWorkServerPreferredConnectionInfo
				{
					Hostname = connectionString.Server,
					ExternalHostname = connectionString.Server,
					Port = connectionString.Port,
					ExternalPort = connectionString.Port,
					IISVersion = InternetInformationServicesVersion.IIS7,
					IdentityDNS = null,
					VirtualDirectory = connectionString.InstanceName,
					BindingType = connectionString.BindingType,
					Certificate = null
				};
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000291C File Offset: 0x00000B1C
		private void GetValuesFromRegistry()
		{
			RegistryHelper registryHelper = new RegistryHelper();
			this.TestMode = (registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				this.WebAppName,
				"TestMode"
			}) == 1);
			this.WaitTimeBeforeCheckingUpdates = (registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				this.WebAppName,
				"SkipWaitingTimeBeforeUpdating"
			}) == 0);
			int num = registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				this.WebAppName,
				"WebUpdaterService_RunningTimeHours"
			});
			int num2 = registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				this.WebAppName,
				"WebUpdaterService_RunningTimeMinutes"
			});
			if (num != 0 || num2 != 0)
			{
				this.RunningTime = new TimeSpan(num, num2, 0);
			}
			CWLogger.Logger.Info("WebClientUpdaterManager::TestMode = {0}", this.TestMode);
			CWLogger.Logger.Info("WebClientUpdaterManager::SkipWaitingTimeBeforeUpdating = {0}", !this.WaitTimeBeforeCheckingUpdates);
			CWLogger.Logger.Info("WebClientUpdaterManager::RunningTime = {0}:{1}:{2}", this.RunningTime.Hours, this.RunningTime.Minutes, this.RunningTime.Seconds);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002A74 File Offset: 0x00000C74
		private void UpdateClockWorkWebWizard(WebInstanceInfo webInstanceInfo, string tempFolder)
		{
			string text = Path.Combine(tempFolder, "ClockWorkWebWizard");
			try
			{
				if (!string.IsNullOrEmpty(text) && Directory.Exists(text) && !string.IsNullOrEmpty(webInstanceInfo.ProgramFilesFolder) && Directory.Exists(webInstanceInfo.ProgramFilesFolder))
				{
					FileSystem.CopyDirectory(text, webInstanceInfo.ProgramFilesFolder, true);
				}
			}
			catch
			{
			}
			finally
			{
				if (Directory.Exists(text))
				{
					FileSystem.DeleteDirectory(text, true);
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const string PRIVATE_CLOCKWORK_HASHING_AUTH_KEY = "$Ys5+TBS!)yV~XW|B%>+\\S2zBY'^sKx,j~7zJj95#<G%l4)A'wnV^6d/=M=;UK#%x+%$SQ#F';v|3Ty_~?/|kY!.NK|eyXT6I}.L~|0_FgfK]\\!6o/9,/HpE~De93}uB";
	}
}
