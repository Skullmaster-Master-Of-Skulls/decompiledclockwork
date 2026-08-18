using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage;
using TechnoPro.Common.ClientManager.Core.Azure.Storage;
using TechnoPro.Common.ClientManager.ICore.Azure.Storage;
using TechnoPro.Common.Core.Azure.Storage;
using TechnoPro.Common.Core.ClockWorkServer;
using TechnoPro.Common.Core.Institution;
using TechnoPro.Common.Core.Updates.Adapters;
using TechnoPro.Common.DAO.Impl.Updates;
using TechnoPro.Common.DAO.Updates;
using TechnoPro.Common.ICore.Azure.Storage;
using TechnoPro.Common.ICore.ClockWorkServer;
using TechnoPro.Common.ICore.Updates;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Azure.Storage;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.Updates;
using TechnoPro.Common.Public.Entities.Updates.Adapters;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Updates.FilesProviders
{
	// Token: 0x02000010 RID: 16
	public class AzureUpdateDownloaderManager : IUpdateDownloaderManager
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00005053 File Offset: 0x00003253
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000505B File Offset: 0x0000325B
		public string UpdatingSystemVersion { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00005064 File Offset: 0x00003264
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000506C File Offset: 0x0000326C
		public bool TestMode { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00005075 File Offset: 0x00003275
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000507D File Offset: 0x0000327D
		private IDictionary<string, UpdateFileCondition> UpdatesConditions { get; set; }

		// Token: 0x0600007B RID: 123 RVA: 0x00005086 File Offset: 0x00003286
		public AzureUpdateDownloaderManager()
		{
			this.TestMode = false;
			this.UpdatesConditions = new Dictionary<string, UpdateFileCondition>();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000050A4 File Offset: 0x000032A4
		public void GetNewUpdates()
		{
			string text = this.DownloadClockWorkUpdatesConfig();
			bool flag = string.IsNullOrEmpty(text) || !this.TryParseClockWorkUpdatesConditionFile(text);
			if (flag)
			{
				CWLogger.Logger.Error("AzureUpdateDownloaderManager::GetNewUpdates: File '{0}' does not exist or it is empty or it is corrupt.\nGet new updates has been abort", "ClockWorkUpdates.config");
			}
			else
			{
				IServerInstanceInfoManager serverInstanceInfoManager = new ServerInstanceInfoManager();
				IList<ServerInstanceInfo> serverInstancesInfo = serverInstanceInfoManager.GetServerInstancesInfo();
				CWLogger.Logger.Info("AzureUpdateDownloaderManager::GetNewUpdates:: Server Instances count = {0}", (serverInstancesInfo != null) ? serverInstancesInfo.Count : 0);
				bool flag2 = serverInstancesInfo == null || serverInstancesInfo.Count == 0;
				if (!flag2)
				{
					IClockWorkSasTokenProviderClientManager clockWorkSasTokenProviderClientManager = new ClockWorkSasTokenProviderClientManager();
					IAzureStorageManager azureStorageManager = new AzureStorageManager();
					Uri containerSasUri = clockWorkSasTokenProviderClientManager.GetContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken("technopro", null), "public", false, AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
					IList<CloudBlobInfo> list = azureStorageManager.ListBlockBlobInfoInContainer(containerSasUri);
					Uri containerSasUri2 = clockWorkSasTokenProviderClientManager.GetContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken("technopro", null), "computer", false, AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
					IList<CloudBlobInfo> list2 = azureStorageManager.ListBlockBlobInfoInContainer(containerSasUri2);
					foreach (ServerInstanceInfo serverInstanceInfo in serverInstancesInfo)
					{
						serverInstanceInfo.Version = serverInstanceInfo.Version.FormatVersion();
						CWLogger.Logger.Info("AzureUpdateDownloaderManager::GetNewUpdates:: /******************* Started for {0}... ***********************/", serverInstanceInfo.InstanceName);
						try
						{
							bool flag3 = serverInstanceInfo.ClockWorkServerDbConnectionInfo == null;
							if (!flag3)
							{
								serverInstanceInfo.SetupDatabaseLayerFactory();
								InstitutionManager institutionManager = new InstitutionManager();
								string institutionUniqueName = institutionManager.GetInstitutionUniqueName();
								GetUpdatingSystemClientPrivateContainerSasUriResp updatingSystemClientPrivateContainerSasUri = clockWorkSasTokenProviderClientManager.GetUpdatingSystemClientPrivateContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken(institutionUniqueName, null));
								eUpdateFolderAccess eUpdateFolderAccess = eUpdateFolderAccess.Private;
								bool flag4 = this.ValidSupportLicense();
								if (flag4)
								{
									eUpdateFolderAccess = eUpdateFolderAccess.All;
								}
								string fullComputerName = TechnoPro.Common.Win32.Environment.FullComputerName;
								IList<DotNetVersion> dotNetVersionsInstalled = TechnoPro.Common.Win32.Environment.GetDotNetVersionsInstalled();
								string ipaddress = TechnoPro.Common.Win32.Environment.GetIPAddress();
								Uri logsBlobSasUri = updatingSystemClientPrivateContainerSasUri.LogsBlobSasUri;
								string format = "Updating System Version='{4}', Server '{0}':Server version='{1}':Server IP='{2}':.Net versions={3}";
								object[] array = new object[5];
								array[0] = fullComputerName;
								array[1] = (serverInstanceInfo.Version ?? string.Empty);
								array[2] = ipaddress;
								array[3] = (from v in dotNetVersionsInstalled
								select v.ToString().Substring(1).Replace("_", ".")).ToList<string>().CommaSeparatedValues<string>();
								array[4] = (this.UpdatingSystemVersion ?? "NULL");
								this.WriteAzureLogsAsync(logsBlobSasUri, string.Format(format, array));
								List<CloudBlobInfo> list3 = new List<CloudBlobInfo>();
								list = this.ApplyFilter(list, serverInstanceInfo.Version);
								bool flag5 = list != null && (eUpdateFolderAccess & eUpdateFolderAccess.Public) == eUpdateFolderAccess.Public;
								if (flag5)
								{
									list3.AddRange(list);
								}
								IList<CloudBlobInfo> list4 = this.ApplyFilter(azureStorageManager.ListBlockBlobInfoInContainer(updatingSystemClientPrivateContainerSasUri.PrivateContainerSasUri), serverInstanceInfo.Version);
								list3.AddRange(list4);
								list2 = this.ApplyFilter(list2, serverInstanceInfo.Version);
								bool flag6 = list2 != null && (eUpdateFolderAccess & eUpdateFolderAccess.Computer) == eUpdateFolderAccess.Computer;
								if (flag6)
								{
									list3.AddRange(list2);
								}
								IUpdateManager updateManager = new UpdateManager(new OperationContext
								{
									WhoAmI = 0
								}, null);
								bool flag7 = list3.Count > 0;
								if (flag7)
								{
									string source;
									list3 = this.DownloadUpdatesToTemporalFolder(list3, out source);
									bool flag8 = list3.Count > 0;
									if (flag8)
									{
										try
										{
											IUpdateDAO updateDAO = new UpdateDAO(new OperationContext
											{
												WhoAmI = 0
											});
											updateManager.CopyFilesToUpdatingFolder(source, updateDAO.GetLegacyPrivateFolderPath(), list4);
										}
										catch
										{
										}
										updateManager.MoveFilesToUpdatingFolder(source, list3);
									}
								}
								bool flag9 = list != null;
								if (flag9)
								{
									foreach (CloudBlobInfo cloudBlobInfo in list)
									{
										updateManager.MarkUpdateAsPending(cloudBlobInfo.BlobName, true);
									}
								}
								bool flag10 = list4 != null;
								if (flag10)
								{
									foreach (CloudBlobInfo cloudBlobInfo2 in list4)
									{
										updateManager.MarkUpdateAsPending(cloudBlobInfo2.BlobName, false);
									}
								}
								CWLogger.Logger.Info("AzureUpdateDownloaderManager::GetNewUpdates:: /******************* Finished for {0} ***************************/", serverInstanceInfo.InstanceName);
							}
						}
						catch (Exception ex)
						{
							CWLogger.Logger.ErrorException(string.Format("AzureUpdateDownloaderManager::GetNewUpdates:: Instance Name={1}, {0}", ex.ToString(), serverInstanceInfo.InstanceName), ex);
						}
					}
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005530 File Offset: 0x00003730
		public void GetRecoveryFiles()
		{
			try
			{
				CWLogger.Logger.Info("UpdateDownloaderManager::GetRecoveryFiles:: /**************** Begin *********************/");
				IClockWorkSasTokenProviderClientManager clockWorkSasTokenProviderClientManager = new ClockWorkSasTokenProviderClientManager();
				IAzureStorageManager azureStorageManager = new AzureStorageManager();
				Uri containerSasUri = clockWorkSasTokenProviderClientManager.GetContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken("technopro", null), "recovery", false, AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
				IList<CloudBlobInfo> list = azureStorageManager.ListBlockBlobInfoInContainer(containerSasUri);
				bool flag = list.Count > 0;
				if (flag)
				{
					string source;
					List<CloudBlobInfo> list2 = this.DownloadUpdatesToTemporalFolder(list, out source);
					bool flag2 = list2.Count > 0;
					if (flag2)
					{
						IUpdateManager updateManager = new UpdateManager(new OperationContext
						{
							WhoAmI = 0
						});
						updateManager.MoveFilesToUpdatingFolder(source, list2);
					}
				}
				CWLogger.Logger.Info("UpdateDownloaderManager::GetRecoveryFiles:: /****************** Finished **********************/");
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("UpdateDownloaderManager::GetRecoveryFiles:: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005614 File Offset: 0x00003814
		public IList<string> GetAllUpdatingSystemClientPrivateFolderPath()
		{
			List<string> list = new List<string>();
			IServerInstanceInfoManager serverInstanceInfoManager = new ServerInstanceInfoManager();
			IList<ServerInstanceInfo> serverInstancesInfo = serverInstanceInfoManager.GetServerInstancesInfo();
			foreach (ServerInstanceInfo serverInstanceInfo in serverInstancesInfo)
			{
				try
				{
					bool flag = serverInstanceInfo.ClockWorkServerDbConnectionInfo == null;
					if (!flag)
					{
						serverInstanceInfo.SetupDatabaseLayerFactory();
						InstitutionManager institutionManager = new InstitutionManager();
						string institutionUniqueName = institutionManager.GetInstitutionUniqueName();
						list.Add(Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, institutionUniqueName));
					}
				}
				catch
				{
				}
			}
			return list;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000056C8 File Offset: 0x000038C8
		private IList<CloudBlobInfo> ApplyFilter(IEnumerable<CloudBlobInfo> files, string instanceVersion)
		{
			AzureUpdateDownloaderManager.<>c__DisplayClass17_0 CS$<>8__locals1 = new AzureUpdateDownloaderManager.<>c__DisplayClass17_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.instanceVersion = instanceVersion;
			AzureUpdateDownloaderManager.<>c__DisplayClass17_0 CS$<>8__locals2 = CS$<>8__locals1;
			List<Predicate<CloudBlobInfo>> list = new List<Predicate<CloudBlobInfo>>();
			list.Add(delegate(CloudBlobInfo fi)
			{
				string fileTypeTitle = fi.BlobName.GetFileTypeTitle();
				bool flag = !UpdateFileTypes.UpdateFileTypesList.Contains(fileTypeTitle);
				bool result;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = CS$<>8__locals1.<>4__this.UpdatesConditions == null || CS$<>8__locals1.<>4__this.UpdatesConditions.Count == 0;
					if (flag2)
					{
						result = false;
					}
					else
					{
						string blobName = fi.BlobName;
						bool flag3 = !CS$<>8__locals1.<>4__this.UpdatesConditions.ContainsKey(blobName);
						if (flag3)
						{
							result = false;
						}
						else
						{
							UpdateFileCondition updateFileCondition = CS$<>8__locals1.<>4__this.UpdatesConditions[blobName];
							bool flag4 = updateFileCondition == null;
							if (flag4)
							{
								result = false;
							}
							else
							{
								string item = CS$<>8__locals1.instanceVersion.FormatVersion();
								bool flag5;
								if (updateFileCondition.AllowableToUpgradeVersions != null)
								{
									if (!updateFileCondition.AllowableToUpgradeVersions.Contains("*"))
									{
										flag5 = updateFileCondition.AllowableToUpgradeVersions.ToList<string>().ConvertAll<string>((string s) => s.FormatVersion()).Contains(item);
									}
									else
									{
										flag5 = true;
									}
								}
								else
								{
									flag5 = false;
								}
								result = flag5;
							}
						}
					}
				}
				return result;
			});
			list.Add(delegate(CloudBlobInfo fi)
			{
				string updates_PATH = ClockWorkUpdateSystemPathVariables.UPDATES_PATH;
				string fileName = Path.Combine(updates_PATH, fi.ContainerName, fi.BlobName);
				FileInfo fileInfo = new FileInfo(fileName);
				return !fileInfo.Exists || (fi.SizeinBytes > 0L && fileInfo.Length != fi.SizeinBytes);
			});
			CS$<>8__locals2.filters = list;
			return (from update in files
			where CS$<>8__locals1.filters.All((Predicate<CloudBlobInfo> filter) => filter(update))
			select update).ToList<CloudBlobInfo>();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000574C File Offset: 0x0000394C
		private List<CloudBlobInfo> DownloadUpdatesToTemporalFolder(IEnumerable<CloudBlobInfo> files, out string tempFolder)
		{
			tempFolder = FileSystem.GetTemporalFolderInTechnoPro();
			List<CloudBlobInfo> list = new List<CloudBlobInfo>();
			IAzureStorageManager azureStorageManager = new AzureStorageManager();
			List<Task> list2 = new List<Task>();
			foreach (CloudBlobInfo cloudBlobInfo in files)
			{
				try
				{
					string text = Path.Combine(tempFolder, cloudBlobInfo.ContainerName);
					bool flag = !Directory.Exists(text);
					if (flag)
					{
						Directory.CreateDirectory(text);
					}
					list2.Add(azureStorageManager.DownloadBlobToFileAsync(cloudBlobInfo.ContainerUri, cloudBlobInfo.BlobName, Path.Combine(text, cloudBlobInfo.BlobName)));
					list.Add(cloudBlobInfo);
					CWLogger.Logger.Info("New update available, Filename={0}, Folder={1}", cloudBlobInfo.BlobName, cloudBlobInfo.ContainerName);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("AzureUpdateDownloaderManager::DownloadUpdatesToTemporalFolder:: Failed when getting update {0} from server.\n{1}", cloudBlobInfo.BlobName, ex.ToString()), ex);
				}
			}
			Task.WaitAll(list2.ToArray());
			return list;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005878 File Offset: 0x00003A78
		private string DownloadClockWorkUpdatesConfig()
		{
			IClockWorkSasTokenProviderClientManager clockWorkSasTokenProviderClientManager = new ClockWorkSasTokenProviderClientManager();
			Uri containerSasUri = clockWorkSasTokenProviderClientManager.GetContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken("technopro", null), "environmentconfigs", false, AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);
			IAzureStorageManager azureStorageManager = new AzureStorageManager();
			return azureStorageManager.DownloadTextBlockBlob(containerSasUri, "ClockWorkUpdates.config");
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000058BC File Offset: 0x00003ABC
		private bool TryParseClockWorkUpdatesConditionFile(string xml)
		{
			bool result;
			try
			{
				XDocument xdocument = XDocument.Parse(xml);
				bool isPublic;
				bool isActive;
				List<UpdateFileCondition> list = (from upd in xdocument.Descendants("Update")
				let xFilename = upd.Attribute("filename")
				let xVersion = upd.Attribute("version")
				let xIsPublic = upd.Attribute("isPublic")
				select new
				{
					<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
					xIsActive = upd.Attribute("isActive")
				}).Select(delegate(<>h__TransparentIdentifier3)
				{
					UpdateFileCondition updateFileCondition2 = new UpdateFileCondition();
					updateFileCondition2.Filename = ((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.xFilename != null) ? <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.xFilename.Value.Trim() : string.Empty);
					updateFileCondition2.Version = ((<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.xVersion != null) ? <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.xVersion.Value.Trim() : string.Empty);
					updateFileCondition2.IsPublic = (<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xIsPublic == null || (bool.TryParse(<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.xIsPublic.Value.Trim(), out isPublic) & isPublic));
					updateFileCondition2.IsActive = (<>h__TransparentIdentifier3.xIsActive == null || (bool.TryParse(<>h__TransparentIdentifier3.xIsActive.Value.Trim(), out isActive) & isActive));
					updateFileCondition2.AllowableToUpgradeVersions = (from v in <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.upd.Elements("ApplyOnVersion")
					select v.Value.Trim()).ToList<string>();
					return updateFileCondition2;
				}).ToList<UpdateFileCondition>();
				this.UpdatesConditions = new Dictionary<string, UpdateFileCondition>();
				foreach (UpdateFileCondition updateFileCondition in list)
				{
					bool flag = this.UpdatesConditions.ContainsKey(updateFileCondition.Filename);
					if (flag)
					{
						CWLogger.Logger.Warn("AzureUpdateDownloaderManager::TryParseClockWorkUpdatesConditionFile:: File '{0}' is duplicated in '{1}'", updateFileCondition.Filename, "ClockWorkUpdates.config");
					}
					this.UpdatesConditions.Add(updateFileCondition.Filename, updateFileCondition);
				}
				result = true;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("AzureUpdateDownloaderManager::TryParseClockWorkUpdatesConditionFile:: Failed parsing '{0}' file.\n{1}", "ClockWorkUpdates.config", ex.ToString()), ex);
				this.UpdatesConditions = new Dictionary<string, UpdateFileCondition>();
				result = false;
			}
			return result;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005A88 File Offset: 0x00003C88
		[DebuggerStepThrough]
		private Task WriteAzureLogsAsync(Uri clientLogFileUri, string logs)
		{
			AzureUpdateDownloaderManager.<WriteAzureLogsAsync>d__21 <WriteAzureLogsAsync>d__ = new AzureUpdateDownloaderManager.<WriteAzureLogsAsync>d__21();
			<WriteAzureLogsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAzureLogsAsync>d__.<>4__this = this;
			<WriteAzureLogsAsync>d__.clientLogFileUri = clientLogFileUri;
			<WriteAzureLogsAsync>d__.logs = logs;
			<WriteAzureLogsAsync>d__.<>1__state = -1;
			<WriteAzureLogsAsync>d__.<>t__builder.Start<AzureUpdateDownloaderManager.<WriteAzureLogsAsync>d__21>(ref <WriteAzureLogsAsync>d__);
			return <WriteAzureLogsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005ADC File Offset: 0x00003CDC
		private bool ValidSupportLicense()
		{
			LicensingManager licensingManager = new LicensingManager();
			DateTime? dateTime;
			ProductLicenseState productState = licensingManager.GetProductState("Support Plan", out dateTime);
			return productState == ProductLicenseState.Licensed;
		}

		// Token: 0x0400001E RID: 30
		public const string CLOCKWORK_UPDATES_CONFIG_FILENAME = "ClockWorkUpdates.config";
	}
}
