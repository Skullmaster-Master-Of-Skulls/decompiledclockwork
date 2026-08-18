using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using TechnoPro.Common.Win32;

namespace Common.WinServices
{
	// Token: 0x02000002 RID: 2
	public static class WinService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void ExecuteServiceCommand(string serviceName, int command)
		{
			ServiceController serviceController = new ServiceController(serviceName);
			serviceController.Refresh();
			if (serviceController.Status == ServiceControllerStatus.Stopped && !WinService.StartService(serviceName, 10000))
			{
				throw new Exception("Service was stopped. Unable to re-start service.");
			}
			if (serviceController.Status == ServiceControllerStatus.Running)
			{
				serviceController.ExecuteCommand(command);
				return;
			}
			throw new Exception("Unable to execute command.");
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		public static bool StartServices(int timeoutMilliseconds, params string[] serviceNames)
		{
			return serviceNames == null || serviceNames.Length == 0 || serviceNames.All((string serviceName) => WinService.StartService(serviceName, timeoutMilliseconds));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E0 File Offset: 0x000002E0
		public static bool StartService(string serviceName, int timeoutMilliseconds)
		{
			ServiceController serviceByName = WinService.GetServiceByName(serviceName);
			if (serviceByName == null)
			{
				return true;
			}
			bool result;
			try
			{
				TimeSpan timeout = TimeSpan.FromMilliseconds((double)timeoutMilliseconds);
				if (serviceByName.Status != ServiceControllerStatus.Running)
				{
					serviceByName.Start();
					serviceByName.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				serviceByName.Dispose();
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002148 File Offset: 0x00000348
		public static bool StopServices(int timeoutMilliseconds, params string[] serviceNames)
		{
			return serviceNames == null || serviceNames.Length == 0 || serviceNames.All((string serviceName) => WinService.StopService(serviceName, timeoutMilliseconds));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002180 File Offset: 0x00000380
		public static bool StopService(string serviceName, int timeoutMilliseconds)
		{
			ServiceController serviceByName = WinService.GetServiceByName(serviceName);
			if (serviceByName == null)
			{
				return true;
			}
			bool result;
			try
			{
				TimeSpan timeout = TimeSpan.FromMilliseconds((double)timeoutMilliseconds);
				if (serviceByName.Status != ServiceControllerStatus.Stopped)
				{
					serviceByName.Stop();
					serviceByName.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				serviceByName.Dispose();
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021E8 File Offset: 0x000003E8
		public static bool RestartService(string serviceName, int timeoutMilliseconds)
		{
			ServiceController serviceByName = WinService.GetServiceByName(serviceName);
			if (serviceByName == null)
			{
				return true;
			}
			bool result;
			try
			{
				int tickCount = System.Environment.TickCount;
				TimeSpan timeout = TimeSpan.FromMilliseconds((double)timeoutMilliseconds);
				if (serviceByName.Status != ServiceControllerStatus.Stopped)
				{
					serviceByName.Stop();
					serviceByName.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
				}
				int tickCount2 = System.Environment.TickCount;
				timeout = TimeSpan.FromMilliseconds((double)(timeoutMilliseconds - (tickCount2 - tickCount)));
				if (serviceByName.Status != ServiceControllerStatus.Running)
				{
					serviceByName.Start();
					serviceByName.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				serviceByName.Dispose();
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002280 File Offset: 0x00000480
		public static IEnumerable<ServiceController> GetServices(Predicate<ServiceController> filter)
		{
			return from s in ServiceController.GetServices()
			where filter(s)
			select s;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022B0 File Offset: 0x000004B0
		public static IList<ServiceController> GetServiceList(Predicate<ServiceController> filter)
		{
			return (from s in ServiceController.GetServices()
			where filter(s)
			select s).ToList<ServiceController>();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022E8 File Offset: 0x000004E8
		public static ServiceController GetServiceByName(string serviceName)
		{
			return ServiceController.GetServices().FirstOrDefault((ServiceController s) => s.ServiceName.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002318 File Offset: 0x00000518
		public static ServiceController GetFirstServiceMatching(Predicate<ServiceController> predicate)
		{
			return ServiceController.GetServices().FirstOrDefault((ServiceController s) => predicate(s));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002348 File Offset: 0x00000548
		public static void InstallService(string filename)
		{
			string text = WinService.CopyInstallUtil();
			try
			{
				if (File.Exists(text))
				{
					Process process = Process.Start(new ProcessStartInfo
					{
						FileName = text,
						Arguments = "\"" + filename + "\"",
						Verb = "runas"
					});
					if (process != null && process.WaitForExit((int)TimeSpan.FromSeconds(60.0).TotalMilliseconds))
					{
						process.Close();
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023D4 File Offset: 0x000005D4
		public static void UninstallService(string filename)
		{
			string text = WinService.CopyInstallUtil();
			try
			{
				if (File.Exists(text))
				{
					Process process = Process.Start(new ProcessStartInfo
					{
						FileName = text,
						Arguments = string.Concat(new string[]
						{
							"/u",
							" ",
							"\"",
							filename,
							"\""
						}),
						Verb = "runas"
					});
					if (process != null && process.WaitForExit((int)TimeSpan.FromSeconds(60.0).TotalMilliseconds))
					{
						process.Close();
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002480 File Offset: 0x00000680
		public static void UninstallServiceByName(string serviceName)
		{
			WinService.StopService(serviceName, 30000);
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = "sc.exe",
					Arguments = "delete" + " " + serviceName,
					Verb = "runas"
				});
			}
			catch
			{
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024E8 File Offset: 0x000006E8
		private static string CopyInstallUtil()
		{
			string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "installutil.exe");
			if (!File.Exists(text))
			{
				string arg = TechnoPro.Common.Win32.Environment.Is64BitProcess ? "InstallUtil64.exe" : "InstallUtil32.exe";
				using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("TechnoPro.Common.WinServices.Resources.{0}", arg)))
				{
					if (manifestResourceStream != null)
					{
						byte[] array = new byte[manifestResourceStream.Length];
						manifestResourceStream.Read(array, 0, array.Length);
						using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
						{
							fileStream.Write(array, 0, array.Length);
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025AC File Offset: 0x000007AC
		public static void SetRecoveryOptions(params string[] serviceNames)
		{
			if (serviceNames == null || serviceNames.Length == 0)
			{
				return;
			}
			for (int i = 0; i < serviceNames.Length; i++)
			{
				WinService.SetRecoveryOptions(serviceNames[i]);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025DC File Offset: 0x000007DC
		public static void SetRecoveryOptions(string serviceName)
		{
			try
			{
				using (Process process = new Process())
				{
					ProcessStartInfo startInfo = process.StartInfo;
					startInfo.FileName = "sc";
					startInfo.WindowStyle = ProcessWindowStyle.Hidden;
					startInfo.Arguments = string.Format("failure \"{0}\" reset= 0 actions= restart/60000", serviceName);
					process.Start();
					process.WaitForExit();
					int exitCode = process.ExitCode;
					process.Close();
				}
			}
			catch
			{
			}
		}
	}
}
