using System;
using System.IO;
using System.ServiceProcess;
using ClockWorkLogger;
using Common.WinServices;
using Databases;
using TechnoPro.Common.Core.Web.Deploy;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.ICore.ApplicationPool;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Updates.Adapters;
using TechnoPro.Common.Win32;
using TechnoPro.Common.WinServices;

namespace TechnoPro.Common.Core.Updates.Adapters
{
	// Token: 0x02000011 RID: 17
	public static class InstanceInfoAdapter
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00005B08 File Offset: 0x00003D08
		public static bool StartApplicationPool(this InstanceInfo app)
		{
			InternetInformationServicesVersion issversion = TechnoPro.Common.Win32.Environment.ISSVersion;
			bool flag = issversion >= InternetInformationServicesVersion.IIS7;
			bool result;
			if (flag)
			{
				IApplicationPoolManager applicationPoolManager = new ApplicationPoolManager
				{
					OpContext = new ApplicationPoolOperationContext
					{
						WhoAmI = 0,
						ApplicationPoolName = app.AppPoolName
					}
				};
				applicationPoolManager.StartApplicationPool(false);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005B60 File Offset: 0x00003D60
		public static bool StopApplicationPool(this InstanceInfo app)
		{
			InternetInformationServicesVersion issversion = TechnoPro.Common.Win32.Environment.ISSVersion;
			bool flag = issversion >= InternetInformationServicesVersion.IIS7;
			bool result;
			if (flag)
			{
				IApplicationPoolManager applicationPoolManager = new ApplicationPoolManager
				{
					OpContext = new ApplicationPoolOperationContext
					{
						WhoAmI = 0,
						ApplicationPoolName = app.AppPoolName
					}
				};
				applicationPoolManager.StopApplicationPool(true);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public static void StopClockWorkServerJobsService(this ServerInstanceInfo server)
		{
			string serviceName = server.ClockWorkServerInstanceName.ToString() + "JobsService";
			ServiceController serviceByName = WinService.GetServiceByName(serviceName);
			bool flag = serviceByName != null;
			if (flag)
			{
				WinService.StopService(serviceName, 60000);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005C00 File Offset: 0x00003E00
		public static void StartClockWorkServerJobsService(this ServerInstanceInfo server)
		{
			string text = null;
			try
			{
				try
				{
					bool flag = server.ClockWorkServerInstanceName == eClockWorkServerInstanceName.ClockWorkServer;
					if (flag)
					{
						Version versionObject = server.Version.GetVersionObject();
						bool flag2 = versionObject != null && (versionObject.Equals(new Version(5, 15, 1, 1)) || versionObject.Equals(new Version(5, 15, 1, 2)));
						if (flag2)
						{
							string serviceName = "ClockWorkServerJobsService";
							ServiceController serviceByName = WinService.GetServiceByName(serviceName);
							bool flag3 = serviceByName != null;
							if (flag3)
							{
								WinService.UninstallServiceByName(serviceName);
							}
						}
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("InstanceInfoAdapter::StartClockWorkServerJobsService:: Legacy code: {0}", ex.ToString()), ex);
				}
				text = server.ClockWorkServerInstanceName.ToString() + "JobsService";
				ServiceController serviceByName2 = WinService.GetServiceByName(text);
				bool flag4 = serviceByName2 == null;
				if (flag4)
				{
					string path = Path.Combine(server.InstallationPath, "bin");
					string text2 = Path.Combine(path, string.Format("ClockWorkServer.Deploy.{0}JobsService.exe", server.ClockWorkServerInstanceName));
					bool flag5 = File.Exists(text2);
					if (flag5)
					{
						WinService.InstallService(text2);
						serviceByName2 = WinService.GetServiceByName(text);
						bool flag6 = serviceByName2 != null;
						if (flag6)
						{
							WinService.StartService(text, 60000);
						}
					}
				}
				else
				{
					WinService.StartService(text, 60000);
				}
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.ErrorException(string.Format("StartClockWorkServerJobsService:: ServiceName={0} {1}", text ?? "NULL", ex2), ex2);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public static void SetupDatabaseLayerFactory(this ServerInstanceInfo server)
		{
			DatabaseLayerFactory.Clear();
			DatabaseLayer patchDatabaseLayer = server.GetPatchDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
			bool flag = patchDatabaseLayer != null;
			if (flag)
			{
				patchDatabaseLayer.DatabaseRole = eDatabaseConnectionStringName.ClockWork;
			}
			DatabaseLayerFactory.SetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, patchDatabaseLayer);
			DatabaseLayerFactory.SetPatchDatabaseLayer(eDatabaseConnectionStringName.ClockWork, patchDatabaseLayer);
			try
			{
				patchDatabaseLayer = server.GetPatchDatabaseLayer(eDatabaseConnectionStringName.ClockWorkFiles);
				bool flag2 = patchDatabaseLayer != null;
				if (flag2)
				{
					patchDatabaseLayer.DatabaseRole = eDatabaseConnectionStringName.ClockWorkFiles;
				}
				DatabaseLayerFactory.SetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkFiles, patchDatabaseLayer);
				DatabaseLayerFactory.SetPatchDatabaseLayer(eDatabaseConnectionStringName.ClockWorkFiles, patchDatabaseLayer);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.WarnException(string.Format("Updates::InstanceInfoAdapter::SetupDatabaseLayerFactory: Server={0}, Database Role={1}, {2}", server.ClockWorkServerInstanceName, eDatabaseConnectionStringName.ClockWorkFiles, ex.ToString()), ex);
			}
			try
			{
				patchDatabaseLayer = server.GetPatchDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking);
				bool flag3 = patchDatabaseLayer != null;
				if (flag3)
				{
					patchDatabaseLayer.DatabaseRole = eDatabaseConnectionStringName.ClockWorkTracking;
				}
				DatabaseLayerFactory.SetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking, patchDatabaseLayer);
				DatabaseLayerFactory.SetPatchDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking, patchDatabaseLayer);
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.WarnException(string.Format("Updates::InstanceInfoAdapter::SetupDatabaseLayerFactory: Server={0}, Database Role={1}, {2}", server.ClockWorkServerInstanceName, eDatabaseConnectionStringName.ClockWorkTracking, ex2.ToString()), ex2);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005ED4 File Offset: 0x000040D4
		public static bool ExecutePreUpdateCustomAction(this InstanceInfo instanceInfo, string tempFolder)
		{
			bool result;
			try
			{
				string text = Path.Combine(tempFolder, "preupdate");
				bool flag = Directory.Exists(text);
				if (flag)
				{
					try
					{
						CWLogger.Logger.Trace(instanceInfo.InstanceName + "::ExecuteUpdate:: PreCustomAction: preupdate folder found");
						string text2 = Path.Combine(text, "ca.exe");
						bool flag2 = File.Exists(text2);
						if (flag2)
						{
							bool flag3 = CommandPrompt.ExecuteProgram(text2, instanceInfo.VirtualDirectory, (int)TimeSpan.FromMinutes(60.0).TotalMilliseconds);
							CWLogger.Logger.Trace(string.Format("{0}::ExecuteUpdate:: PreCustomAction: preupdate custom action ca.exe were executed, success='{1}'", instanceInfo.InstanceName, flag3));
							return flag3;
						}
						CWLogger.Logger.Trace(instanceInfo.InstanceName + "::ExecuteUpdate:: PreCustomAction: No custom action ca.exe were found on preupdate found");
					}
					finally
					{
						bool flag4 = Directory.Exists(text);
						if (flag4)
						{
							FileSystem.DeleteDirectory(text, true);
						}
					}
				}
				else
				{
					CWLogger.Logger.Trace(instanceInfo.InstanceName + "::ExecuteUpdate:: PreCustomAction: No preupdate folder found");
				}
				result = false;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("{0}::ExecuteUpdate:: PreCustomAction: {1}", instanceInfo.InstanceName, ex), ex);
				result = false;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006024 File Offset: 0x00004224
		public static bool ExecutePostUpdateCustomAction(this InstanceInfo instanceInfo, string tempFolder)
		{
			bool result;
			try
			{
				string text = Path.Combine(tempFolder, "postupdate");
				bool flag = Directory.Exists(text);
				if (flag)
				{
					try
					{
						CWLogger.Logger.Trace(instanceInfo.InstanceName + "::ExecuteUpdate:: PostCustomAction: postupdate folder found");
						string text2 = Path.Combine(text, "ca.exe");
						bool flag2 = File.Exists(text2);
						if (flag2)
						{
							bool flag3 = CommandPrompt.ExecuteProgram(text2, instanceInfo.VirtualDirectory, (int)TimeSpan.FromMinutes(60.0).TotalMilliseconds);
							CWLogger.Logger.Trace(string.Format("{0}::ExecuteUpdate:: PostCustomAction: postupdate custom action ca.exe were executed, success='{1}'", instanceInfo.InstanceName, flag3));
							return flag3;
						}
						CWLogger.Logger.Trace(instanceInfo.InstanceName + "::ExecuteUpdate:: PostCustomAction: No custom action ca.exe were found on postupdate found");
					}
					finally
					{
						bool flag4 = Directory.Exists(text);
						if (flag4)
						{
							FileSystem.DeleteDirectory(text, true);
						}
					}
				}
				else
				{
					CWLogger.Logger.Trace(instanceInfo.InstanceName + "::ExecuteUpdate:: PostCustomAction: No postupdate folder found");
				}
				result = false;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("{0}::ExecuteUpdate:: PostCustomAction: {1}", instanceInfo.InstanceName, ex), ex);
				result = false;
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006174 File Offset: 0x00004374
		public static void CreateNewServerApplicationPoolIfNotExists(this InstanceInfo instanceInfo)
		{
			IApplicationPoolManager applicationPoolManager = new ApplicationPoolManager
			{
				OpContext = new ApplicationPoolOperationContext
				{
					WhoAmI = 0,
					ApplicationPoolName = instanceInfo.VirtualDirectory + "45AppPool",
					ManageRuntimeVersion = "v4.0"
				}
			};
			bool flag = applicationPoolManager.CreateApplicationPoolIfNotExists();
			if (flag)
			{
				instanceInfo.AppPoolName = instanceInfo.VirtualDirectory + "45AppPool";
				applicationPoolManager.SetApplicationPoolRecyclingScheduler(new TimeSpan(1, 0, 0));
				MsmqServiceStatus messagingQueueServiceStatus = MessaggingQueueAdapter.GetMessagingQueueServiceStatus();
				bool flag2 = messagingQueueServiceStatus > MsmqServiceStatus.NotInstalled;
				if (flag2)
				{
					applicationPoolManager.SetApplicationPoolToWebApplication(instanceInfo.VirtualDirectory, instanceInfo.Sitename, new string[]
					{
						"net.tcp",
						"net.msmq",
						"http"
					});
				}
				else
				{
					applicationPoolManager.SetApplicationPoolToWebApplication(instanceInfo.VirtualDirectory, instanceInfo.Sitename, new string[]
					{
						"net.tcp",
						"http"
					});
				}
				applicationPoolManager.SetApplicationPoolSettings(instanceInfo.Sitename, instanceInfo.VirtualDirectory, instanceInfo.AppPoolName);
			}
		}
	}
}
