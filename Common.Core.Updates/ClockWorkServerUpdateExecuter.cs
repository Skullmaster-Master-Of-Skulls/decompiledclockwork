using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using ClockWorkLogger;
using TechnoPro.Common.ClientManager.Core.ClockWorkServerConnection;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection;
using TechnoPro.Common.Compression;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Core.ClockWorkServerConnection;
using TechnoPro.Common.Core.Institution;
using TechnoPro.Common.Core.Updates.Adapters;
using TechnoPro.Common.DAO.FileSign.Impl;
using TechnoPro.Common.DAO.Impl.Updates;
using TechnoPro.Common.DAO.Updates;
using TechnoPro.Common.ICore.ClockWorkServerConnection;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;
using TechnoPro.Common.Web.Deploy;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x0200000B RID: 11
	[ExecuterFileType("ClockWorkServer update")]
	internal class ClockWorkServerUpdateExecuter : IUpdateExecuter
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000035C9 File Offset: 0x000017C9
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000035D1 File Offset: 0x000017D1
		private IUpdateDAO UpdateDAO { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000035DA File Offset: 0x000017DA
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000035E2 File Offset: 0x000017E2
		public string Name { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002C91 File Offset: 0x00000E91
		public int ExecutionOrder
		{
			get
			{
				return UpdateFileTypes.UpdateFileTypesList.IndexOf(this.ExecutingFileType());
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000035EB File Offset: 0x000017EB
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000035F3 File Offset: 0x000017F3
		public ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000035FC File Offset: 0x000017FC
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003604 File Offset: 0x00001804
		public IExternalLogManager ExternalLogManager { get; set; }

		// Token: 0x06000050 RID: 80 RVA: 0x0000360D File Offset: 0x0000180D
		public ClockWorkServerUpdateExecuter()
		{
			this.UpdateDAO = new UpdateDAO();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003624 File Offset: 0x00001824
		public ExecuteUpdatesResp ExecuteUpdate()
		{
			ExecuteUpdatesResp result;
			try
			{
				string updates_PATH = ClockWorkUpdateSystemPathVariables.UPDATES_PATH;
				bool flag = !string.IsNullOrEmpty(this.ServerInstance.InstallationPath);
				if (flag)
				{
					string text = Path.Combine(this.ServerInstance.InstallationPath, "bin");
					InstitutionManager institutionManager = new InstitutionManager();
					string institutionUniqueName = institutionManager.GetInstitutionUniqueName();
					IList<UpdateStatus> executionStatus = this.UpdateDAO.GetExecutionStatus();
					UpdateStatus updateStatus = (from u in executionStatus
					where u.Status == eUpdateStatus.OnSchedule.ToString() && u.FileType == "ClockWorkServer update"
					select u).FirstOrDefault<UpdateStatus>();
					bool flag2 = updateStatus == null;
					if (flag2)
					{
						result = new ExecuteUpdatesResp
						{
							ExecuteUpdatesStatus = eExecuteUpdateStatus.UpToDate
						};
					}
					else
					{
						string version = updateStatus.Filename.GetVersion().FormatVersion();
						string text2 = (this.ServerInstance.Version != null) ? this.ServerInstance.Version.FormatVersion() : null;
						Version v = string.IsNullOrEmpty(text2) ? null : new Version(text2);
						Version version2 = new Version(version);
						bool flag3 = v != null && v >= version2;
						if (flag3)
						{
							updateStatus.Status = eUpdateStatus.Dismiss.ToString();
							this.UpdateDAO.SaveExecutionStatus(updateStatus);
							result = new ExecuteUpdatesResp
							{
								ExecuteUpdatesStatus = eExecuteUpdateStatus.UpToDate
							};
						}
						else
						{
							string temporalFolderInTechnoPro = FileSystem.GetTemporalFolderInTechnoPro();
							string text3 = Path.Combine(temporalFolderInTechnoPro, "bin");
							bool flag4 = FileSystem.CopyDirectory(text, text3, true);
							bool flag5 = !flag4;
							if (flag5)
							{
								result = new ExecuteUpdatesResp
								{
									ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
									LastError = "Failed while creating ClockWorkServer files backup"
								};
							}
							else
							{
								string path = Path.Combine(updates_PATH, updateStatus.IsPublic ? "Public" : institutionUniqueName);
								string text4 = Path.Combine(path, updateStatus.Filename);
								IFileSignDAO fileSignDAO = new FileSignDAO();
								string tempFileName = FileSystem.GetTempFileName(Path.GetExtension(text4));
								try
								{
									fileSignDAO.DecryptAndVerifyUsingFileSystem(text4, tempFileName);
								}
								catch (DecryptAndVerifyFailedException ex)
								{
									CWLogger.Logger.ErrorException(string.Format("ClockWorkServerUpdateExecuter::ExecuteUpdate:: {0}", ex.ToString()), ex);
									return new ExecuteUpdatesResp
									{
										ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
										LastError = ex.Message
									};
								}
								string temporalFolder = FileSystem.GetTemporalFolder();
								CompressDataAdapter.expandFolder(tempFileName, temporalFolder);
								this.ServerInstance.StopApplicationPool();
								this.ServerInstance.StopClockWorkServerJobsService();
								this.ServerInstance.ExecutePreUpdateCustomAction(temporalFolder);
								this.UpdateClockWorkServerWizard(temporalFolder);
								bool flag6 = FileSystem.CopyDirectory(temporalFolder, this.ServerInstance.InstallationPath, true);
								bool flag7 = !flag6;
								if (flag7)
								{
									CWLogger.Logger.Error("ClockWorkServerUpdateExecuter::ExecuteUpdate:: Failed when copying new files, rolling back installation ...");
									FileSystem.CopyDirectoryAndContinueIfFailing(text3, text, true);
									result = new ExecuteUpdatesResp
									{
										ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
										LastError = "Failed while copying new installation files"
									};
								}
								else
								{
									this.UpdateAppSettings();
									updateStatus.Status = eUpdateStatus.Done.ToString();
									this.UpdateDAO.SaveExecutionStatus(updateStatus);
									this.UpdateRegistrySettings(version2);
									this.ServerInstance.ExecutePostUpdateCustomAction(temporalFolder);
									this.ExternalLogManager.Log("ClockWorkServer update file '" + updateStatus.Filename + "' was successfully installed on " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"));
									result = new ExecuteUpdatesResp
									{
										ExecuteUpdatesStatus = eExecuteUpdateStatus.Updated,
										Filenames = new List<string>
										{
											updateStatus.Filename
										}
									};
								}
							}
						}
					}
				}
				else
				{
					result = new ExecuteUpdatesResp
					{
						ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
						LastError = "Unable to find ClockWork Server installation path"
					};
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException("ClockWorkServerUpdateExecuter::ExecuteUpdate:: " + ex2.ToString(), ex2);
				result = new ExecuteUpdatesResp
				{
					ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
					LastError = ex2.Message
				};
			}
			finally
			{
				WebApplicationManager.EnableWebApplicationAutoStart(this.ServerInstance.Sitename, this.ServerInstance.VirtualDirectory, this.ServerInstance.AppPoolName);
				this.ServerInstance.StartApplicationPool();
				this.ServerInstance.StartClockWorkServerJobsService();
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003A8C File Offset: 0x00001C8C
		private void UpdateRegistrySettings(Version newServerVersion)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, newServerVersion.ToString(), new string[]
			{
				"ClockWorkServer Application",
				this.ServerInstance.VirtualDirectory,
				"Version"
			});
			IClockWorkServerConnectionInfoManager clockWorkServerConnectionInfoManager = new ClockWorkServerConnectionInfoManager(new ClockWorkServerOperationContext
			{
				WhoAmI = 0,
				ClockWorkServerInstanceName = this.ServerInstance.ClockWorkServerInstanceName,
				ClockWorkServerVirtualDirectory = this.ServerInstance.VirtualDirectory
			});
			string text = Path.Combine(this.ServerInstance.InstallationPath, "ClockWork2.ini");
			bool flag = File.Exists(text);
			if (flag)
			{
				string storageString = File.ReadAllText(text);
				IClockWorkClientConnectionInfoClientManager clockWorkClientConnectionInfoClientManager = new ClockWorkClientConnectionInfoClientManager();
				ClockWorkClientConnectionInfo connectionInfoFromStorageString = clockWorkClientConnectionInfoClientManager.GetConnectionInfoFromStorageString(storageString);
				ClockWorkServerConnectionInfo clockWorkServerConnectionInfo = new ClockWorkServerConnectionInfo
				{
					ClockWorkServerInstanceName = this.ServerInstance.ClockWorkServerInstanceName,
					HttpHostname = connectionInfoFromStorageString.ServerPreferredConnection.ExternalHostname,
					HttpPort = connectionInfoFromStorageString.ServerPreferredConnection.ExternalPort,
					TcpHostname = connectionInfoFromStorageString.ServerPreferredConnection.Hostname,
					TcpPort = connectionInfoFromStorageString.ServerPreferredConnection.Port,
					IdentityDNS = connectionInfoFromStorageString.ServerPreferredConnection.IdentityDNS,
					VirtualDirectory = connectionInfoFromStorageString.ServerPreferredConnection.VirtualDirectory,
					Certificate = connectionInfoFromStorageString.ServerPreferredConnection.Certificate,
					IISVersion = connectionInfoFromStorageString.ServerPreferredConnection.IISVersion
				};
				clockWorkServerConnectionInfoManager.SaveClockWorkServerConnectionInfo(clockWorkServerConnectionInfo);
			}
			else
			{
				CWLogger.Logger.Trace("ClockWorkServerUpdateExecuter::UpdateRegistrySettings: File '{0}' does not exist for {1} instance", text, this.ServerInstance.ClockWorkServerInstanceName);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003C2C File Offset: 0x00001E2C
		private void UpdateAppSettings()
		{
			string configPath = Path.Combine(this.ServerInstance.InstallationPath, "Web.Config");
			Configuration configuration = ClockWorkServerUpdateExecuter.OpenConfigFile(configPath);
			string appSettingsByNameUsingProtection = configuration.GetAppSettingsByNameUsingProtection("vdir");
			bool flag = appSettingsByNameUsingProtection != null;
			if (flag)
			{
				configuration.AppSettings.Settings["vdir"].Value = (this.ServerInstance.VirtualDirectory ?? "ClockWorkServer");
			}
			else
			{
				configuration.AppSettings.Settings.Add("vdir", this.ServerInstance.VirtualDirectory ?? "ClockWorkServer");
			}
			string appSettingsByNameUsingProtection2 = configuration.GetAppSettingsByNameUsingProtection("serverinstancename");
			bool flag2 = appSettingsByNameUsingProtection2 != null;
			if (flag2)
			{
				configuration.AppSettings.Settings["serverinstancename"].Value = this.ServerInstance.ClockWorkServerInstanceName.ToString();
			}
			else
			{
				configuration.AppSettings.Settings.Add("serverinstancename", this.ServerInstance.ClockWorkServerInstanceName.ToString());
			}
			configuration.ProtectSection("appSettings");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003D54 File Offset: 0x00001F54
		private void UpdateClockWorkServerWizard(string tempFolder)
		{
			string text = Path.Combine(tempFolder, "ClockWorkServerWizard");
			try
			{
				bool flag = string.IsNullOrEmpty(text) || !Directory.Exists(text);
				if (!flag)
				{
					bool flag2 = string.IsNullOrEmpty(this.ServerInstance.ProgramFilesFolder) || !Directory.Exists(this.ServerInstance.ProgramFilesFolder);
					if (!flag2)
					{
						CWLogger.Logger.Info("ClockWorkServerUpdateExecuter::ExecuteUpdate:: Updating ClockWorkServer Wizard for '{0}' ...", this.ServerInstance.InstanceName);
						bool flag3 = FileSystem.CopyDirectory(text, this.ServerInstance.ProgramFilesFolder, true);
						bool flag4 = !flag3;
						if (flag4)
						{
							CWLogger.Logger.Error("ClockWorkServerUpdateExecuter::ExecuteUpdate:: Failed to copy files over '{0}'", this.ServerInstance.ProgramFilesFolder);
						}
						else
						{
							CWLogger.Logger.Info("ClockWorkServerUpdateExecuter::ExecuteUpdate::ClockWorkServer Wizard at '{0}' were updated successfully", this.ServerInstance.InstanceName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException("ClockWorkServerUpdateExecuter::ExecuteUpdate:: " + ex.ToString(), ex);
			}
			finally
			{
				bool flag5 = Directory.Exists(text);
				if (flag5)
				{
					FileSystem.DeleteDirectory(text, true);
				}
				CWLogger.Logger.Info("ClockWorkServerUpdateExecuter::ExecuteUpdate:: Files at '{0}' were deleted successfully", text);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003EA0 File Offset: 0x000020A0
		private static Configuration OpenConfigFile(string configPath)
		{
			FileInfo fileInfo = new FileInfo(configPath);
			VirtualDirectoryMapping mapping = new VirtualDirectoryMapping(fileInfo.DirectoryName, true, fileInfo.Name);
			return WebConfigurationManager.OpenMappedWebConfiguration(new WebConfigurationFileMap
			{
				VirtualDirectories = 
				{
					{
						"/",
						mapping
					}
				}
			}, "/");
		}
	}
}
