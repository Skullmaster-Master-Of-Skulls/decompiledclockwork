using System;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using Microsoft.Web.Administration;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Web.Deploy
{
	// Token: 0x02000002 RID: 2
	public class WebApplicationManager
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void StartApplicationPool(string appPoolName)
		{
			try
			{
				using (ServerManager serverManager = new ServerManager())
				{
					ApplicationPool applicationPool = serverManager.ApplicationPools[appPoolName];
					if (applicationPool != null)
					{
						applicationPool.Start();
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		public static void SetWebApplicationPoolSettings(string sitename, string vDir, string appPoolName)
		{
			Site site = null;
			try
			{
				site = WebApplicationManager.GetSite(sitename);
				if (site == null)
				{
					CWLogger.Logger.Error("WebApplicationManager::SetWebApplicationPoolSettings: Sitename '{0}' does not exist", sitename);
				}
				else
				{
					using (ServerManager serverManager = new ServerManager())
					{
						ApplicationPool applicationPool = serverManager.ApplicationPools[appPoolName];
						if (applicationPool != null)
						{
							applicationPool.ProcessModel.LoadUserProfile = true;
							applicationPool.ProcessModel.IdleTimeout = TimeSpan.Zero;
							applicationPool.Recycling.PeriodicRestart.Time = TimeSpan.Zero;
							serverManager.CommitChanges();
						}
					}
				}
			}
			catch
			{
				if (site != null)
				{
					string environmentVariable = System.Environment.GetEnvironmentVariable("windir");
					if (!string.IsNullOrEmpty(environmentVariable))
					{
						CommandPrompt.ExecuteProgram(Path.Combine(environmentVariable, "system32\\inetsrv\\appcmd.exe"), "set apppool \"" + appPoolName + "\" /loadUserProfile:true", 0);
					}
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002188 File Offset: 0x00000388
		public static void EnableWebApplicationAutoStart(string sitename, string vDir, string appPoolName)
		{
			Site site = null;
			try
			{
				site = WebApplicationManager.GetSite(sitename);
				if (site == null)
				{
					CWLogger.Logger.Error("WebApplicationManager::EnableWebApplicationAutoStart: Sitename '{0}' does not exist", sitename);
				}
				else
				{
					using (ServerManager serverManager = new ServerManager())
					{
						ApplicationPool applicationPool = serverManager.ApplicationPools[appPoolName];
						applicationPool.AutoStart = true;
						applicationPool.SetAttributeValue("startMode", "AlwaysRunning");
						serverManager.CommitChanges();
						Application application = site.Applications["/" + vDir];
						if (application != null)
						{
							application.SetAttributeValue("preloadEnabled", "True");
						}
						serverManager.CommitChanges();
					}
				}
			}
			catch
			{
				if (site != null)
				{
					string environmentVariable = System.Environment.GetEnvironmentVariable("windir");
					if (!string.IsNullOrEmpty(environmentVariable) && CommandPrompt.ExecuteProgram(Path.Combine(environmentVariable, "system32\\inetsrv\\appcmd.exe"), "set apppool \"" + appPoolName + "\" /startMode:AlwaysRunning", 0))
					{
						CommandPrompt.ExecuteProgram(Path.Combine(environmentVariable, "system32\\inetsrv\\appcmd.exe"), string.Concat(new string[]
						{
							"set app \"",
							site.Name,
							"/",
							vDir,
							"\" /preloadEnabled:true"
						}), 0);
					}
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022C0 File Offset: 0x000004C0
		public static void SetApplicationPoolToWebApplication(string sitename, string vDir, string appPool)
		{
			Site site = null;
			try
			{
				site = WebApplicationManager.GetSite(sitename);
				if (site == null)
				{
					CWLogger.Logger.Error("WebApplicationManager::SetApplicationPoolToWebApplication: Sitename '{0}' does not exist", sitename);
				}
				else
				{
					using (ServerManager serverManager = new ServerManager())
					{
						Application application = site.Applications["/" + vDir];
						if (application != null)
						{
							application.ApplicationPoolName = appPool;
							serverManager.CommitChanges();
						}
					}
				}
			}
			catch
			{
				if (site != null)
				{
					string environmentVariable = System.Environment.GetEnvironmentVariable("windir");
					if (!string.IsNullOrEmpty(environmentVariable))
					{
						CommandPrompt.ExecuteProgram(Path.Combine(environmentVariable, "system32\\inetsrv\\appcmd.exe"), string.Concat(new string[]
						{
							"set app \"",
							site.Name,
							"/",
							vDir,
							"\" /applicationPool:",
							appPool
						}), 0);
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023A4 File Offset: 0x000005A4
		public static void SetWebApplicationEnableProtocols(string siteName, string vDir, params string[] protocols)
		{
			Site site = null;
			try
			{
				site = WebApplicationManager.GetSite(siteName);
				if (site == null)
				{
					CWLogger.Logger.Error("WebApplicationManager::SetWebApplicationEnableProtocols: Sitename '{0}' does not exist", siteName);
				}
				else
				{
					WebApplicationManager.SetupSiteBindings((int)site.Id, protocols);
					using (ServerManager serverManager = new ServerManager())
					{
						Application application = site.Applications["/" + vDir];
						if (application != null)
						{
							bool flag = false;
							if (protocols != null)
							{
								foreach (string text in protocols)
								{
									if (!application.EnabledProtocols.Contains(text))
									{
										Application application2 = application;
										application2.EnabledProtocols = application2.EnabledProtocols + "," + text;
										flag = true;
									}
								}
							}
							if (flag)
							{
								serverManager.CommitChanges();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("WebApplicationManager::SetWebApplicationEnableProtocols: {0}", ex), ex);
				if (site != null)
				{
					string environmentVariable = System.Environment.GetEnvironmentVariable("windir");
					if (!string.IsNullOrEmpty(environmentVariable))
					{
						CommandPrompt.ExecuteProgram(Path.Combine(environmentVariable, "system32\\inetsrv\\appcmd.exe"), string.Concat(new string[]
						{
							"set app \"",
							site.Name,
							"/",
							vDir,
							"\" /enabledProtocols:http,net.tcp,net.msmq"
						}), 0);
					}
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000024F4 File Offset: 0x000006F4
		private static void SetupSiteBindings(int siteId, params string[] protocols)
		{
			foreach (string a in protocols)
			{
				if (!(a == "net.tcp"))
				{
					if (a == "net.msmq")
					{
						WebApplicationManager.AddBinding(siteId, "net.msmq", "localhost");
						WebApplicationManager.AddBinding(siteId, "msmq.formatname", "localhost");
					}
				}
				else
				{
					WebApplicationManager.AddBinding(siteId, "net.pipe", "*");
					WebApplicationManager.AddBinding(siteId, "net.tcp", "808:*");
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002574 File Offset: 0x00000774
		private static void AddBinding(int id, string bindingProtocol, string bindingInformation)
		{
			try
			{
				using (ServerManager serverManager = new ServerManager())
				{
					Site site = serverManager.Sites.FirstOrDefault((Site s) => s.Id == (long)id);
					if (site != null)
					{
						if (!site.Bindings.Any((Binding b) => b.Protocol.Equals(bindingProtocol, StringComparison.OrdinalIgnoreCase)))
						{
							site.Bindings.Add(bindingInformation, bindingProtocol);
							serverManager.CommitChanges();
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002618 File Offset: 0x00000818
		private static Site GetSite(int siteId)
		{
			Site result;
			try
			{
				using (ServerManager serverManager = new ServerManager())
				{
					result = serverManager.Sites.FirstOrDefault((Site s) => s.Id == (long)siteId);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("WebApplicationManager::GetSite by Id: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000269C File Offset: 0x0000089C
		private static Site GetSite(string sitename)
		{
			return WebApplicationManager.GetSite(WebApplicationManager.GetSiteId(sitename));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026AC File Offset: 0x000008AC
		private static int GetSiteId(string sitename)
		{
			int num;
			if (int.TryParse(string.IsNullOrEmpty(sitename) ? "1" : sitename.Substring(sitename.LastIndexOf('/') + 1), out num) && num > 0)
			{
				return num;
			}
			int result;
			using (ServerManager serverManager = new ServerManager())
			{
				result = (int)(serverManager.Sites["Default Web Site"] ?? serverManager.Sites[0]).Id;
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		public const string WebApplication_Protocol_NetTcp = "net.tcp";

		// Token: 0x04000002 RID: 2
		public const string WebApplication_Protocol_HTTP = "http";

		// Token: 0x04000003 RID: 3
		public const string WebApplication_Protocol_HTTPS = "https";

		// Token: 0x04000004 RID: 4
		public const string WebApplication_Protocol_NetMSMQ = "net.msmq";

		// Token: 0x04000005 RID: 5
		public const string WebApplication_DefaultSiteName = "Default Web Site";
	}
}
