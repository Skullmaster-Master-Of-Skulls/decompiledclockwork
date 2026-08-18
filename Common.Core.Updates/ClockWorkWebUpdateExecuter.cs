using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Compression;
using TechnoPro.Common.Core.InstanceInfo;
using TechnoPro.Common.Core.Institution;
using TechnoPro.Common.Core.Updates.Adapters;
using TechnoPro.Common.DAO.FileSign.Impl;
using TechnoPro.Common.DAO.Impl.Updates;
using TechnoPro.Common.DAO.Updates;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x0200000C RID: 12
	[ExecuterFileType("ClockWorkWeb update")]
	internal class ClockWorkWebUpdateExecuter : IUpdateExecuter
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003EEF File Offset: 0x000020EF
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003EF7 File Offset: 0x000020F7
		private IUpdateDAO UpdateDAO { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003F00 File Offset: 0x00002100
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003F08 File Offset: 0x00002108
		public string Name { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003F14 File Offset: 0x00002114
		public int ExecutionOrder
		{
			get
			{
				return UpdateFileTypes.UpdateFileTypesList.IndexOf(this.ExecutingFileType());
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003F36 File Offset: 0x00002136
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003F3E File Offset: 0x0000213E
		public ServerInstanceInfo ServerInstance { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003F47 File Offset: 0x00002147
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003F4F File Offset: 0x0000214F
		public IExternalLogManager ExternalLogManager { get; set; }

		// Token: 0x0600005F RID: 95 RVA: 0x00003F58 File Offset: 0x00002158
		public ClockWorkWebUpdateExecuter()
		{
			this.UpdateDAO = new UpdateDAO();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003F70 File Offset: 0x00002170
		public ExecuteUpdatesResp ExecuteUpdate()
		{
			ExecuteUpdatesResp result;
			try
			{
				bool flag = string.IsNullOrEmpty(this.ServerInstance.InstallationPath);
				if (flag)
				{
					result = new ExecuteUpdatesResp
					{
						ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
						LastError = "Unable to find ClockWork Server intallation path"
					};
				}
				else
				{
					string text = Path.Combine(this.ServerInstance.InstallationPath, "FileSystem Storage");
					string updates_PATH = ClockWorkUpdateSystemPathVariables.UPDATES_PATH;
					InstitutionManager institutionManager = new InstitutionManager();
					string institutionUniqueName = institutionManager.GetInstitutionUniqueName();
					IList<UpdateStatus> executionStatus = this.UpdateDAO.GetExecutionStatus();
					UpdateStatus updateStatus = (from u in executionStatus
					where u.Status == eUpdateStatus.OnSchedule.ToString() && u.FileType == "ClockWorkWeb update"
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
						string text2 = (this.ServerInstance.Version != null) ? this.ServerInstance.Version.FormatVersion() : null;
						Version version = string.IsNullOrEmpty(text2) ? null : new Version(text2);
						string text3 = updateStatus.Filename.GetVersion().FormatVersion();
						Version version2 = new Version(text3);
						bool flag3 = updateStatus.Filename.IsHotFix();
						if (flag3)
						{
							bool flag4 = version != null && version > version2;
							if (flag4)
							{
								updateStatus.Status = eUpdateStatus.Dismiss.ToString();
								this.UpdateDAO.SaveExecutionStatus(updateStatus);
								CWLogger.Logger.Info("ClockWorkWebUpdateExecuter::ExecuteUpdate: Update file '{0}' was dismmiss because its version {1} is less than or equals than server version {2}", updateStatus.Filename ?? string.Empty, text3 ?? string.Empty, text2 ?? string.Empty);
								return new ExecuteUpdatesResp
								{
									ExecuteUpdatesStatus = eExecuteUpdateStatus.UpToDate
								};
							}
						}
						text2 = UpdateManager.DeletePreviousInstallersUsingVersion(updateStatus.Filename, text);
						version = (string.IsNullOrEmpty(text2) ? null : new Version(text2));
						string path = Path.Combine(updates_PATH, updateStatus.IsPublic ? "Public" : institutionUniqueName);
						bool flag5 = version == null || version < version2;
						if (flag5)
						{
							string fileName = Path.Combine(path, updateStatus.Filename);
							FileInfo fileInfo = new FileInfo(fileName);
							fileInfo.CopyTo(Path.Combine(text, updateStatus.Filename), true);
						}
						ExecuteUpdatesResp executeUpdatesResp = null;
						RegistryHelper registryHelper = new RegistryHelper();
						string text4 = Path.Combine(path, updateStatus.Filename);
						IFileSignDAO fileSignDAO = new FileSignDAO();
						string tempFileName = FileSystem.GetTempFileName(Path.GetExtension(text4));
						try
						{
							fileSignDAO.DecryptAndVerifyUsingFileSystem(text4, tempFileName);
						}
						catch (DecryptAndVerifyFailedException ex)
						{
							CWLogger.Logger.ErrorException("ClockWorkWebUpdateExecuter::ExecuteUpdate:: Error when verifying file signature: " + ex.ToString(), ex);
							return new ExecuteUpdatesResp
							{
								ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
								LastError = "ClockWorkWebUpdateExecuter::ExecuteUpdate:: " + ex.ToString()
							};
						}
						string temporalFolder = FileSystem.GetTemporalFolder();
						CompressDataAdapter.expandFolder(tempFileName, temporalFolder);
						WebInstanceInfoManager webInstanceInfoManager = new WebInstanceInfoManager();
						IList<WebInstanceInfo> webInstancesInfo = webInstanceInfoManager.GetWebInstancesInfo(this.ServerInstance.ClockWorkServerDbConnectionInfo);
						CWLogger.Logger.Trace(string.Format("ClockWorkWebUpdateExecuter::ExecuteUpdate:: Web Apps instances for '{0}' = '{1}'", this.ServerInstance.InstanceName, webInstancesInfo.Count));
						foreach (WebInstanceInfo webInstanceInfo in webInstancesInfo)
						{
							try
							{
								bool flag6 = string.IsNullOrEmpty(webInstanceInfo.InstallationPath);
								if (!flag6)
								{
									string version3 = webInstanceInfo.Version;
									string text5 = (version3 != null) ? version3.FormatVersion() : null;
									Version v = string.IsNullOrEmpty(text5) ? null : new Version(text5);
									bool flag7 = v != null && v >= version2;
									if (flag7)
									{
										CWLogger.Logger.Trace(string.Concat(new string[]
										{
											"ClockWorkWebUpdateExecuter::ExecuteUpdate:: Current version '",
											version.ToString(),
											"' is greater or equal to new version '",
											version2.ToString(),
											"'"
										}));
									}
									else
									{
										string temporalFolderInTechnoPro = FileSystem.GetTemporalFolderInTechnoPro();
										string text6 = Path.Combine(temporalFolderInTechnoPro, webInstanceInfo.InstanceName);
										bool flag8 = FileSystem.CopyDirectory(webInstanceInfo.InstallationPath, text6, true);
										bool flag9 = !flag8;
										if (flag9)
										{
											CWLogger.Logger.Error("ClockWorkWebUpdateExecuter::ExecuteUpdate:: Failed while creating {0} files backup", webInstanceInfo.InstanceName);
											executeUpdatesResp = new ExecuteUpdatesResp
											{
												ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
												LastError = "Failed while creating " + webInstanceInfo.InstanceName + " files backup"
											};
										}
										else
										{
											webInstanceInfo.StopApplicationPool();
											webInstanceInfo.ExecutePreUpdateCustomAction(temporalFolder);
											this.UpdateClockWorkWebWizard(webInstanceInfo, temporalFolder);
											bool flag10 = FileSystem.CopyDirectory(temporalFolder, webInstanceInfo.InstallationPath, true);
											bool flag11 = !flag10;
											if (flag11)
											{
												CWLogger.Logger.Error("ClockWorkWebUpdateExecuter::ExecuteUpdate:: Failed when copying new files on {0}, rolling back installation ...", webInstanceInfo.InstanceName);
												FileSystem.CopyDirectoryAndContinueIfFailing(text6, webInstanceInfo.InstallationPath, true);
												executeUpdatesResp = new ExecuteUpdatesResp
												{
													ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
													LastError = "Failed while copying new installation files"
												};
											}
											else
											{
												registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, version2.ToString(), new string[]
												{
													"ClockWorkWeb",
													webInstanceInfo.InstanceName,
													"Version"
												});
												webInstanceInfo.ExecutePostUpdateCustomAction(temporalFolder);
											}
										}
									}
								}
							}
							catch (Exception ex2)
							{
								CWLogger.Logger.ErrorException("ClockWorkWebUpdateExecuter::ExecuteUpdate:: " + ex2.ToString(), ex2);
								executeUpdatesResp = new ExecuteUpdatesResp
								{
									ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
									LastError = "ClockWorkWebUpdateExecuter::ExecuteUpdate:: " + ex2.ToString()
								};
							}
							finally
							{
								webInstanceInfo.StartApplicationPool();
							}
						}
						bool flag12 = executeUpdatesResp != null;
						if (flag12)
						{
							result = executeUpdatesResp;
						}
						else
						{
							updateStatus.Status = eUpdateStatus.Done.ToString();
							this.UpdateDAO.SaveExecutionStatus(updateStatus);
							this.ExternalLogManager.Log("ClockWorkWeb update file '" + updateStatus.Filename + "' was successfully installed on " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt"));
							result = new ExecuteUpdatesResp
							{
								ExecuteUpdatesStatus = eExecuteUpdateStatus.Updated,
								Filenames = new string[]
								{
									updateStatus.Filename
								}
							};
						}
					}
				}
			}
			catch (Exception ex3)
			{
				CWLogger.Logger.ErrorException("ClockWorkWebUpdateExecuter::ExecuteUpdate:: " + ex3.ToString(), ex3);
				result = new ExecuteUpdatesResp
				{
					ExecuteUpdatesStatus = eExecuteUpdateStatus.Error,
					LastError = ex3.Message
				};
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004668 File Offset: 0x00002868
		private void UpdateClockWorkWebWizard(WebInstanceInfo webInstanceInfo, string tempFolder)
		{
			string text = Path.Combine(tempFolder, "ClockWorkWebWizard");
			try
			{
				bool flag = !string.IsNullOrEmpty(text) && Directory.Exists(text);
				if (flag)
				{
					bool flag2 = !string.IsNullOrEmpty(webInstanceInfo.ProgramFilesFolder) && Directory.Exists(webInstanceInfo.ProgramFilesFolder);
					if (flag2)
					{
						CWLogger.Logger.Info("ClockWorkWebUpdateExecuter::ExecuteUpdate:: Updating ClockWorkWeb Wizard for '{0}' ...", webInstanceInfo.InstanceName);
						bool flag3 = FileSystem.CopyDirectory(text, webInstanceInfo.ProgramFilesFolder, true);
						bool flag4 = !flag3;
						if (flag4)
						{
							CWLogger.Logger.Error("ClockWorWebUpdateExecuter::ExecuteUpdate:: Failed to copy files over '{0}'", webInstanceInfo.ProgramFilesFolder);
						}
						else
						{
							CWLogger.Logger.Info("ClockWorkWebUpdateExecuter::ExecuteUpdate::ClockWorkWeb Wizard at '{0}' were updated successfully", this.ServerInstance.InstanceName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException("ClockWorkWebUpdateExecuter::ExecuteUpdate:: " + ex.ToString(), ex);
			}
			finally
			{
				bool flag5 = Directory.Exists(text);
				if (flag5)
				{
					FileSystem.DeleteDirectory(text, true);
				}
				CWLogger.Logger.Info("ClockWorkWebUpdateExecuter::ExecuteUpdate:: Files at '{0}' were deleted successfully", text);
			}
		}
	}
}
