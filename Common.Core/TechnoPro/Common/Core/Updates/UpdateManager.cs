using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ClockWorkLogger;
using Common.WinServices;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.FileStorages;
using TechnoPro.Common.Core.Institution;
using TechnoPro.Common.DAO.Impl.Updates;
using TechnoPro.Common.DAO.Updates;
using TechnoPro.Common.ICore.FileStorages;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Azure.Storage;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;

namespace TechnoPro.Common.Core.Updates
{
	// Token: 0x02000030 RID: 48
	public class UpdateManager : IUpdateManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009113 File Offset: 0x00007313
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000911B File Offset: 0x0000731B
		private IUpdateDAO UpdateDAO { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009124 File Offset: 0x00007324
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000913C File Offset: 0x0000733C
		public OperationContext OpContext
		{
			get
			{
				return this._OpContext;
			}
			set
			{
				this.UpdateDAO.OpContext = value;
				this._OpContext = value;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009160 File Offset: 0x00007360
		public UpdateManager(OperationContext operationContext)
		{
			this.UpdateDAO = new UpdateDAO(operationContext);
			this.OpContext = operationContext;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000917F File Offset: 0x0000737F
		public UpdateManager(OperationContext operationContext, string BinPath)
		{
			this.BinPath = BinPath;
			this.UpdateDAO = new UpdateDAO(operationContext);
			this.OpContext = operationContext;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000091A5 File Offset: 0x000073A5
		public void ForceUpdatingServiceToRun()
		{
			WinService.ExecuteServiceCommand("UpdatingService", 200);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000091B8 File Offset: 0x000073B8
		public void ApplyUpdates(IList<UpdateFileInfo> updates)
		{
			this.UpdateDAO.ApplyUpdate(updates);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000091C8 File Offset: 0x000073C8
		public IList<UpdateFileInfo> GetAvailableUpdates()
		{
			List<UpdateFileInfo> list = new List<UpdateFileInfo>();
			LicensingManager licensingManager = new LicensingManager();
			DateTime? dateTime;
			ProductLicenseState productState = licensingManager.GetProductState("Support Plan", out dateTime);
			IList<UpdateFileInfo> availableUpdates = this.UpdateDAO.GetAvailableUpdates((productState == ProductLicenseState.Licensed) ? eUpdateFolderAccess.All : eUpdateFolderAccess.Private);
			IList<FileType> fileTypes = UpdateFileTypeFactory.GetFileTypes();
			foreach (FileType fileType in fileTypes)
			{
				bool addrSizeVersion = fileType.AddrSizeVersion;
				if (addrSizeVersion)
				{
					UpdateFileInfo availableUpdate = this.GetAvailableUpdate(availableUpdates, fileType, 32);
					bool flag = availableUpdate != null;
					if (flag)
					{
						list.Add(availableUpdate);
					}
					UpdateFileInfo availableUpdate2 = this.GetAvailableUpdate(availableUpdates, fileType, 64);
					bool flag2 = availableUpdate2 != null;
					if (flag2)
					{
						list.Add(availableUpdate2);
					}
				}
				else
				{
					UpdateFileInfo availableUpdate3 = this.GetAvailableUpdate(availableUpdates, fileType, 0);
					bool flag3 = availableUpdate3 != null;
					if (flag3)
					{
						list.Add(availableUpdate3);
					}
				}
			}
			CWLogger.Logger.Info("UpdateManager::GetAvailableUpdates:: {0} updates", list.Count);
			foreach (UpdateFileInfo updateFileInfo in list)
			{
				CWLogger.Logger.Info("    - Filename = {0}, Status = {1}", updateFileInfo.Filename, updateFileInfo.Status.ToString());
			}
			return list;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009350 File Offset: 0x00007550
		public IList<UpdateFileInfo> GetOnScheduleUpdates()
		{
			return this.UpdateDAO.GetOnScheduleUpdates();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000936D File Offset: 0x0000756D
		public void CancelOnScheduleUpdates(IList<UpdateFileInfo> updates)
		{
			this.UpdateDAO.CancelOnScheduleUpdates(updates);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009380 File Offset: 0x00007580
		public IList<UploadUpdateFileResult> UploadUpdateFiles(IList<FileSystemStructure> updFiles)
		{
			List<UploadUpdateFileResult> list = new List<UploadUpdateFileResult>();
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TechnoPro" + Path.DirectorySeparatorChar.ToString() + "UpdatesLocalFolder");
			bool flag = !Directory.Exists(text);
			if (flag)
			{
				Directory.CreateDirectory(text);
			}
			foreach (FileSystemStructure fileSystemStructure in updFiles)
			{
				try
				{
					IFileSignManager fileSignManager = new FileSignManager();
					bool flag2 = fileSignManager.VerifySign(fileSystemStructure.BinaryData);
					bool flag3 = !flag2;
					if (flag3)
					{
						CWLogger.Logger.Error("UploadManager::UploadUpdateFiles:: Verifying file '{0}' signature failed.", fileSystemStructure.Filename);
						list.Add(new UploadUpdateFileResult
						{
							Filename = fileSystemStructure.Filename,
							Folder = fileSystemStructure.Version,
							WasSuccessfullUpload = false,
							ErrorMessage = "Verifying file signature failed"
						});
					}
					else
					{
						string version = fileSystemStructure.Version;
						string text2 = Path.Combine(text, version);
						bool flag4 = !Directory.Exists(text2);
						if (flag4)
						{
							Directory.CreateDirectory(text2);
						}
						string path = Path.Combine(text2, fileSystemStructure.Filename);
						UpdateManager.DeletePreviousInstallers(fileSystemStructure.Filename, text2);
						File.WriteAllBytes(path, fileSystemStructure.BinaryData);
						CWLogger.Logger.Trace("UpdateManager::UploadUpdateFiles:: Upload update {0} successfully to the server.", fileSystemStructure.Filename);
						list.Add(new UploadUpdateFileResult
						{
							Filename = fileSystemStructure.Filename,
							Folder = fileSystemStructure.Version,
							WasSuccessfullUpload = true,
							ErrorMessage = string.Empty
						});
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("UpdateManager::UploadUpdateFiles:: {0}", ex.ToString()), ex);
					list.Add(new UploadUpdateFileResult
					{
						Filename = fileSystemStructure.Filename,
						Folder = fileSystemStructure.Version,
						WasSuccessfullUpload = false,
						ErrorMessage = ex.ToString()
					});
				}
			}
			return list;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000095D0 File Offset: 0x000077D0
		public void MoveFilesToUpdatingFolder(string source, IList<CloudBlobInfo> files)
		{
			foreach (CloudBlobInfo cloudBlobInfo in files)
			{
				try
				{
					string updateHoldingFolder = TechnoPro.Common.DAO.Impl.Updates.UpdateDAO.GetUpdateHoldingFolder(cloudBlobInfo.ContainerName);
					string text = Path.Combine(updateHoldingFolder, cloudBlobInfo.BlobName);
					UpdateManager.DeletePreviousInstallers(cloudBlobInfo.BlobName, updateHoldingFolder);
					string sourceFileName = Path.Combine(source, cloudBlobInfo.ContainerName, cloudBlobInfo.BlobName);
					bool flag = File.Exists(text);
					if (flag)
					{
						File.Delete(text);
					}
					File.Move(sourceFileName, text);
					Thread.Sleep(TimeSpan.FromSeconds(30.0));
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException(string.Format("UpdateManager::MovesFilesToUpdatingFolder:: {0}", cloudBlobInfo.BlobName), exception);
				}
				CWLogger.Logger.Info("UpdateManager::MovesFilesToUpdatingFolder:: File {0} was moved successfully to Updating Folder.", cloudBlobInfo.BlobName);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000096D0 File Offset: 0x000078D0
		public void CopyFilesToUpdatingFolder(string source, string destination, IList<CloudBlobInfo> files)
		{
			foreach (CloudBlobInfo cloudBlobInfo in files)
			{
				try
				{
					string text = Path.Combine(destination, cloudBlobInfo.BlobName);
					UpdateManager.DeletePreviousInstallers(cloudBlobInfo.BlobName, destination);
					string sourceFileName = Path.Combine(source, cloudBlobInfo.ContainerName, cloudBlobInfo.BlobName);
					bool flag = File.Exists(text);
					if (flag)
					{
						File.Delete(text);
					}
					File.Copy(sourceFileName, text);
					Thread.Sleep(TimeSpan.FromSeconds(30.0));
				}
				catch (Exception exception)
				{
					CWLogger.Logger.ErrorException("UpdateManager::MovesFilesToUpdatingFolder:: " + cloudBlobInfo.BlobName, exception);
				}
				CWLogger.Logger.Info("UpdateManager::MovesFilesToUpdatingFolder:: File {0} was moved successfully to Updating Folder.", cloudBlobInfo.BlobName);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000097C4 File Offset: 0x000079C4
		public void MarkUpdateAsPending(string serverFilename, bool isPublicFolder)
		{
			string fileTypeTitle = serverFilename.GetFileTypeTitle();
			IUpdateFileType updateFileType = UpdateFileTypeFactory.GetUpdateFileType(fileTypeTitle);
			bool flag = updateFileType == null;
			if (!flag)
			{
				int addressSize = updateFileType.GetAddressSize(serverFilename);
				bool flag2 = updateFileType.IsHotFix(serverFilename);
				try
				{
					IInstitutionManager institutionManager = new InstitutionManager();
					string institutionName = institutionManager.GetInstitutionName();
					UpdateStatus updateStatus = this.UpdateDAO.GetExecutionStatus(fileTypeTitle, addressSize, isPublicFolder);
					bool flag3 = updateStatus != null;
					if (flag3)
					{
						Version versionObject = updateStatus.Filename.GetVersionObject();
						Version versionObject2 = serverFilename.GetVersionObject();
						bool flag4 = versionObject != null && versionObject2 != null && versionObject >= versionObject2;
						if (!flag4)
						{
							bool flag5 = updateStatus.Status == eUpdateStatus.OnSchedule.ToString();
							if (flag5)
							{
								IList<string> updateFileTypesList = UpdateFileTypes.UpdateFileTypesList;
								int num = updateFileTypesList.IndexOf(fileTypeTitle);
								IEnumerable<UpdateStatus> enumerable = from es in this.UpdateDAO.GetExecutionStatus()
								where es.Status == eUpdateStatus.OnSchedule.ToString()
								select es;
								foreach (UpdateStatus updateStatus2 in enumerable)
								{
									int num2 = updateFileTypesList.IndexOf(updateStatus.FileType);
									bool flag6 = num2 >= num;
									if (flag6)
									{
										updateStatus2.Status = (flag2 ? eUpdateStatus.OnSchedule.ToString() : eUpdateStatus.Pending.ToString());
										updateStatus2.Filename = serverFilename;
										this.UpdateDAO.SaveExecutionStatus(updateStatus2);
										bool flag7 = !flag2;
										if (flag7)
										{
											this.OnSendingNotificationEmailAsync(Setting.AUTOMATICUPDATING_Email_NewUpdatesNotification, new Dictionary<string, string>
											{
												{
													"updatefilename",
													updateStatus2.Filename
												},
												{
													"updatetype",
													updateStatus2.FileType
												},
												{
													"executiondatetime",
													DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt")
												},
												{
													"institutionname",
													institutionName
												},
												{
													"updatechangesurl",
													"https://clockworks.ca/UpdateChanges/"
												}
											}, eTPMessagePriority.Normal);
											this.OnSendingNotificationEmailAsync(Setting.AUTOMATICUPDATING_Email_OnScheduleCancellationNotification, new Dictionary<string, string>
											{
												{
													"updatefilename",
													updateStatus2.Filename
												},
												{
													"updatetype",
													updateStatus2.FileType
												},
												{
													"executiondatetime",
													DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt")
												},
												{
													"dependentupdatetype",
													updateStatus2.FileType
												},
												{
													"institutionname",
													institutionName
												}
											}, eTPMessagePriority.High);
										}
									}
								}
							}
							else
							{
								updateStatus.Filename = serverFilename;
								updateStatus.Status = (flag2 ? eUpdateStatus.OnSchedule.ToString() : eUpdateStatus.Pending.ToString());
								this.UpdateDAO.SaveExecutionStatus(updateStatus);
								bool flag8 = !flag2;
								if (flag8)
								{
									this.OnSendingNotificationEmailAsync(Setting.AUTOMATICUPDATING_Email_NewUpdatesNotification, new Dictionary<string, string>
									{
										{
											"updatefilename",
											updateStatus.Filename
										},
										{
											"updatetype",
											updateStatus.FileType
										},
										{
											"executiondatetime",
											DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt")
										},
										{
											"institutionname",
											institutionName
										},
										{
											"updatechangesurl",
											"https://clockworks.ca/UpdateChanges/"
										}
									}, eTPMessagePriority.Normal);
								}
							}
						}
					}
					else
					{
						updateStatus = new UpdateStatus
						{
							FileType = fileTypeTitle,
							AddressSize = addressSize,
							IsPublic = isPublicFolder,
							Filename = serverFilename,
							Status = (flag2 ? eUpdateStatus.OnSchedule.ToString() : eUpdateStatus.Pending.ToString())
						};
						this.UpdateDAO.SaveExecutionStatus(updateStatus);
						bool flag9 = !flag2;
						if (flag9)
						{
							this.OnSendingNotificationEmailAsync(Setting.AUTOMATICUPDATING_Email_NewUpdatesNotification, new Dictionary<string, string>
							{
								{
									"updatefilename",
									updateStatus.Filename
								},
								{
									"updatetype",
									updateStatus.FileType
								},
								{
									"executiondatetime",
									DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt")
								},
								{
									"institutionname",
									institutionName
								},
								{
									"updatechangesurl",
									"https://clockworks.ca/UpdateChanges/"
								}
							}, eTPMessagePriority.Normal);
						}
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("UpdateManager::MarkAsPending:: Marking '{0}' file type as pending failed with error \n: {1}", fileTypeTitle, ex.ToString()), ex);
					string path = Path.Combine(isPublicFolder ? ClockWorkUpdateSystemPathVariables.UPDATES_PUBLIC_PATH : this.UpdateDAO.UpdatesPrivatePath, serverFilename);
					bool flag10 = File.Exists(path);
					if (flag10)
					{
						File.Delete(path);
					}
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009CBC File Offset: 0x00007EBC
		public static string DeletePreviousInstallersUsingVersion(string fn, string folder)
		{
			string fileTypeTitle = fn.GetFileTypeTitle();
			IUpdateFileType updateFileType = UpdateFileTypeFactory.GetUpdateFileType(fileTypeTitle);
			string version = fn.GetVersion();
			bool flag = updateFileType != null;
			string result;
			if (flag)
			{
				result = UpdateManager.DeletePreviousInstallersByPattern(updateFileType.GetFilenamePattern(updateFileType.GetAddressSize(fn)), folder, version);
			}
			else
			{
				string text = Path.GetExtension(fn);
				bool flag2 = !string.IsNullOrEmpty(text) && text.StartsWith(".");
				if (flag2)
				{
					text = text.Substring(1);
				}
				result = UpdateManager.DeletePreviousInstallers(fileTypeTitle, fn.GetAddressSize(), text, folder, version);
			}
			return result;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009D48 File Offset: 0x00007F48
		public static void DeletePreviousInstallers(string fn, string folder)
		{
			bool flag = !Directory.Exists(folder);
			if (!flag)
			{
				string fileTypeTitle = fn.GetFileTypeTitle();
				IUpdateFileType updateFileType = UpdateFileTypeFactory.GetUpdateFileType(fileTypeTitle);
				bool flag2 = updateFileType != null;
				if (flag2)
				{
					UpdateManager.DeletePreviousInstallersByPattern(updateFileType.GetFilenamePattern(updateFileType.GetAddressSize(fn)), folder);
				}
				else
				{
					string text = Path.GetExtension(fn);
					bool flag3 = !string.IsNullOrEmpty(text) && text.StartsWith(".");
					if (flag3)
					{
						text = text.Substring(1);
					}
					UpdateManager.DeletePreviousInstallers(fileTypeTitle, fn.GetAddressSize(), text, folder);
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009DD8 File Offset: 0x00007FD8
		private static void DeletePreviousInstallersByPattern(string filePattern, string folder)
		{
			bool flag = !Directory.Exists(folder);
			if (!flag)
			{
				try
				{
					List<string> list = Directory.GetFiles(folder, filePattern).ToList<string>();
					foreach (string path in list)
					{
						File.Delete(path);
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("UpdateDAO::DeletePreviousInstallers:: {0}", ex.ToString()), ex);
				}
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009E7C File Offset: 0x0000807C
		private static string DeletePreviousInstallersByPattern(string filePattern, string folder, string version)
		{
			Version version2 = null;
			try
			{
				string[] files = Directory.GetFiles(folder, filePattern);
				bool flag = files.Length == 0;
				if (flag)
				{
					return string.Empty;
				}
				Version version3 = string.IsNullOrEmpty(version) ? null : new Version(version.FormatVersion());
				foreach (string text in files)
				{
					bool flag2 = version3 != null;
					if (flag2)
					{
						Version versionObject = text.GetVersionObject();
						bool flag3 = versionObject == null || versionObject < version3;
						if (flag3)
						{
							File.Delete(text);
						}
						else
						{
							bool flag4 = version2 == null || version2 < versionObject;
							if (flag4)
							{
								version2 = versionObject;
							}
						}
					}
					else
					{
						File.Delete(text);
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("UpdateDAO::DeletePreviousInstallers:: {0}", ex.ToString()), ex);
			}
			return (version2 != null) ? version2.ToString() : string.Empty;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009F9C File Offset: 0x0000819C
		private static void DeletePreviousInstallers(string fileTypeTitle, int addSize, string extension, string folder)
		{
			string filePattern = (addSize > 0) ? string.Format("{0}.x{1}.*.{2}", fileTypeTitle, addSize, extension) : string.Format("{0}.*.{1}", fileTypeTitle, extension);
			UpdateManager.DeletePreviousInstallersByPattern(filePattern, folder);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009FD8 File Offset: 0x000081D8
		private static string DeletePreviousInstallers(string fileTypeTitle, int addSize, string extension, string folder, string version)
		{
			string filePattern = (addSize > 0) ? string.Format("{0}.x{1}.*.{2}", fileTypeTitle, addSize, extension) : string.Format("{0}.*.{1}", fileTypeTitle, extension);
			return UpdateManager.DeletePreviousInstallersByPattern(filePattern, folder, version);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A018 File Offset: 0x00008218
		[DebuggerStepThrough]
		private Task OnSendingNotificationEmailAsync(Setting email, Dictionary<string, string> mailMergeValues, eTPMessagePriority messagePriority = eTPMessagePriority.Normal)
		{
			UpdateManager.<OnSendingNotificationEmailAsync>d__27 <OnSendingNotificationEmailAsync>d__ = new UpdateManager.<OnSendingNotificationEmailAsync>d__27();
			<OnSendingNotificationEmailAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnSendingNotificationEmailAsync>d__.<>4__this = this;
			<OnSendingNotificationEmailAsync>d__.email = email;
			<OnSendingNotificationEmailAsync>d__.mailMergeValues = mailMergeValues;
			<OnSendingNotificationEmailAsync>d__.messagePriority = messagePriority;
			<OnSendingNotificationEmailAsync>d__.<>1__state = -1;
			<OnSendingNotificationEmailAsync>d__.<>t__builder.Start<UpdateManager.<OnSendingNotificationEmailAsync>d__27>(ref <OnSendingNotificationEmailAsync>d__);
			return <OnSendingNotificationEmailAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A074 File Offset: 0x00008274
		private UpdateFileInfo GetAvailableUpdate(IList<UpdateFileInfo> updates, FileType fileType, int addSize)
		{
			IEnumerable<UpdateFileInfo> enumerable = from up in updates
			where up.Filename.GetFileTypeTitle().Equals(fileType.Title) && up.AddressSize == addSize
			select up;
			UpdateFileInfo updateFileInfo = enumerable.WithMaxVersion();
			bool flag = updateFileInfo == null;
			UpdateFileInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (UpdateFileInfo updateFileInfo2 in enumerable)
				{
					bool flag2 = updateFileInfo2.Filename != updateFileInfo.Filename && updateFileInfo2.Status == eUpdateStatus.Pending;
					if (flag2)
					{
						this.UpdateDAO.SaveExecutionStatus(new UpdateStatus
						{
							FileType = updateFileInfo2.Filename.GetFileTypeTitle(),
							AddressSize = updateFileInfo2.AddressSize,
							IsPublic = updateFileInfo2.IsPublic,
							Status = eUpdateStatus.Dismiss.ToString(),
							Filename = updateFileInfo2.Filename
						});
					}
				}
				bool flag3 = updateFileInfo.Status == eUpdateStatus.Pending;
				if (flag3)
				{
					result = updateFileInfo;
				}
				else
				{
					IEnumerable<UpdateFileInfo> enumerable2 = from up in updates
					where up.Filename.GetFileTypeTitle().Equals(fileType.SecondaryTitle) && up.AddressSize == addSize
					select up;
					UpdateFileInfo updateFileInfo3 = enumerable2.WithMaxVersion();
					bool flag4 = updateFileInfo3 == null;
					if (flag4)
					{
						result = null;
					}
					else
					{
						foreach (UpdateFileInfo updateFileInfo4 in enumerable2)
						{
							bool flag5 = updateFileInfo4.Filename != updateFileInfo3.Filename && updateFileInfo4.Status == eUpdateStatus.Pending;
							if (flag5)
							{
								this.UpdateDAO.SaveExecutionStatus(new UpdateStatus
								{
									FileType = updateFileInfo4.Filename.GetFileTypeTitle(),
									AddressSize = updateFileInfo4.AddressSize,
									IsPublic = updateFileInfo4.IsPublic,
									Status = eUpdateStatus.Dismiss.ToString(),
									Filename = updateFileInfo4.Filename
								});
							}
						}
						bool flag6 = updateFileInfo3.Status == eUpdateStatus.Pending;
						if (flag6)
						{
							bool flag7 = new Version(updateFileInfo3.Version) > new Version(updateFileInfo.Version);
							if (flag7)
							{
								return updateFileInfo3;
							}
							this.UpdateDAO.SaveExecutionStatus(new UpdateStatus
							{
								FileType = updateFileInfo3.Filename.GetFileTypeTitle(),
								AddressSize = updateFileInfo3.AddressSize,
								IsPublic = updateFileInfo3.IsPublic,
								Status = eUpdateStatus.Dismiss.ToString(),
								Filename = updateFileInfo3.Filename
							});
						}
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x0400005B RID: 91
		private const string ReleaseNotesUrl = "https://clockworks.ca/UpdateChanges/";

		// Token: 0x0400005D RID: 93
		protected OperationContext _OpContext;

		// Token: 0x0400005E RID: 94
		private string BinPath;
	}
}
