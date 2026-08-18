using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Web.Administration;
using TechnoPro.Common.ICore.ApplicationPool;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Web.Deploy
{
	// Token: 0x02000002 RID: 2
	public class ApplicationPoolManager : IApplicationPoolManager, IBaseOperationContext<ApplicationPoolOperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public void SetApplicationPoolSettings(string sitename, string vDir, string appPoolName)
		{
			Site site = null;
			try
			{
				site = this.GetSite(sitename);
				if (site != null)
				{
					using (ServerManager serverManager = new ServerManager())
					{
						ApplicationPool applicationPool = serverManager.ApplicationPools[appPoolName];
						if (applicationPool != null)
						{
							applicationPool.SetAttributeValue("loadUserProfile", true);
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
					if (!string.IsNullOrEmpty(environmentVariable))
					{
						CommandPrompt.ExecuteProgram(Path.Combine(environmentVariable, "system32\\inetsrv\\appcmd.exe"), "set apppool \"" + appPoolName + "\" /loadUserProfile:true", 0);
					}
				}
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002100 File Offset: 0x00000300
		public bool CreateApplicationPoolIfNotExists()
		{
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools.FirstOrDefault((ApplicationPool a) => a.Name.Equals(this.OpContext.ApplicationPoolName));
				if (applicationPool == null)
				{
					applicationPool = serverManager.ApplicationPools.Add(this.OpContext.ApplicationPoolName);
					applicationPool.ManagedRuntimeVersion = (this.OpContext.ManageRuntimeVersion ?? "v4.0");
					applicationPool.Enable32BitAppOnWin64 = false;
					applicationPool.ProcessModel.LoadUserProfile = true;
					applicationPool.AutoStart = true;
					serverManager.CommitChanges();
					return true;
				}
				if (!applicationPool.ManagedRuntimeVersion.Equals(this.OpContext.ManageRuntimeVersion ?? "v4.0"))
				{
					applicationPool.ManagedRuntimeVersion = (this.OpContext.ManageRuntimeVersion ?? "v4.0");
					applicationPool.Enable32BitAppOnWin64 = false;
					applicationPool.ProcessModel.LoadUserProfile = true;
					applicationPool.AutoStart = true;
					serverManager.CommitChanges();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002204 File Offset: 0x00000404
		public void SetApplicationPoolToDefaultWebSiteApplication(string vDir, params string[] protocols)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				Site site = serverManager.Sites["Default Web Site"] ?? serverManager.Sites[0];
				int siteId = (int)site.Id;
				this.SetupSiteBindings(siteId, protocols);
				Application application = site.Applications[string.Format("/{0}", vDir)];
				if (application != null)
				{
					application.ApplicationPoolName = this.OpContext.ApplicationPoolName;
					if (protocols != null)
					{
						foreach (string text in protocols)
						{
							if (!application.EnabledProtocols.Contains(text))
							{
								Application application2 = application;
								application2.EnabledProtocols += string.Format(",{0}", text);
							}
						}
					}
					serverManager.CommitChanges();
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022DC File Offset: 0x000004DC
		public void SetApplicationPoolToWebApplication(string vDir, string siteName, params string[] protocols)
		{
			int siteId = this.GetSiteId(siteName);
			this.SetupSiteBindings(siteId, protocols);
			using (ServerManager serverManager = new ServerManager())
			{
				Site site = serverManager.Sites.FirstOrDefault((Site s) => s.Id == (long)siteId);
				if (site != null)
				{
					Application application = site.Applications[string.Format("/{0}", vDir)];
					if (application != null)
					{
						application.ApplicationPoolName = this.OpContext.ApplicationPoolName;
						if (protocols != null)
						{
							foreach (string text in protocols)
							{
								if (!application.EnabledProtocols.Contains(text))
								{
									Application application2 = application;
									application2.EnabledProtocols += string.Format(",{0}", text);
								}
							}
						}
						serverManager.CommitChanges();
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023C8 File Offset: 0x000005C8
		public void StartApplicationPool(bool waitForStarting = false)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools[this.OpContext.ApplicationPoolName];
				if (applicationPool.State == ObjectState.Stopped)
				{
					applicationPool.Start();
					if (waitForStarting)
					{
						int num = 0;
						while (applicationPool.State != ObjectState.Started && num < 300)
						{
							Thread.Sleep(2000);
							num++;
						}
					}
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002444 File Offset: 0x00000644
		public void StopApplicationPool(bool waitForStopping = false)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools[this.OpContext.ApplicationPoolName];
				if (applicationPool.State != ObjectState.Stopped)
				{
					applicationPool.Stop();
					if (waitForStopping)
					{
						int num = 0;
						while (applicationPool.State != ObjectState.Stopped && num < 300)
						{
							Thread.Sleep(2000);
							num++;
						}
					}
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000024C0 File Offset: 0x000006C0
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000250C File Offset: 0x0000070C
		public bool Enable32BitAppOnWin64
		{
			get
			{
				bool enable32BitAppOnWin;
				using (ServerManager serverManager = new ServerManager())
				{
					enable32BitAppOnWin = serverManager.ApplicationPools[this.OpContext.ApplicationPoolName].Enable32BitAppOnWin64;
				}
				return enable32BitAppOnWin;
			}
			set
			{
				using (ServerManager serverManager = new ServerManager())
				{
					serverManager.ApplicationPools[this.OpContext.ApplicationPoolName].Enable32BitAppOnWin64 = value;
					serverManager.CommitChanges();
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002560 File Offset: 0x00000760
		public void SetApplicationPoolRecyclingScheduler(TimeSpan ts)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				bool flag = false;
				ApplicationPool applicationPool = serverManager.ApplicationPools[this.OpContext.ApplicationPoolName];
				if (applicationPool != null)
				{
					if (applicationPool.Recycling.PeriodicRestart.Time != TimeSpan.Zero)
					{
						applicationPool.Recycling.PeriodicRestart.Time = TimeSpan.Zero;
						flag = true;
					}
					if (!applicationPool.Recycling.PeriodicRestart.Schedule.Any((Schedule sch) => sch.Time == ts))
					{
						applicationPool.Recycling.PeriodicRestart.Schedule.Add(ts);
						flag = true;
					}
					if (flag)
					{
						serverManager.CommitChanges();
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002638 File Offset: 0x00000838
		public void SetApplicationPoolManagedRuntimeVersion(string manageRuntimeVersion)
		{
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools[this.OpContext.ApplicationPoolName];
				if (applicationPool != null)
				{
					applicationPool.ManagedRuntimeVersion = manageRuntimeVersion;
					serverManager.CommitChanges();
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002690 File Offset: 0x00000890
		private Site GetSite(int siteId)
		{
			Site result;
			try
			{
				using (ServerManager serverManager = new ServerManager())
				{
					result = serverManager.Sites.FirstOrDefault((Site s) => s.Id == (long)siteId);
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000026F8 File Offset: 0x000008F8
		private Site GetSite(string sitename)
		{
			return this.GetSite(this.GetSiteId(sitename));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002708 File Offset: 0x00000908
		private int GetSiteId(string sitename)
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

		// Token: 0x0600000E RID: 14 RVA: 0x00002790 File Offset: 0x00000990
		private void SetupSiteBindings(int siteId, params string[] protocols)
		{
			foreach (string a in protocols)
			{
				if (!(a == "net.tcp"))
				{
					if (a == "net.msmq")
					{
						this.AddBinding(siteId, "net.msmq", "localhost");
						this.AddBinding(siteId, "msmq.formatname", "localhost");
					}
				}
				else
				{
					this.AddBinding(siteId, "net.pipe", "*");
					this.AddBinding(siteId, "net.tcp", "808:*");
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002814 File Offset: 0x00000A14
		private void AddBinding(int id, string bindingProtocol, string bindingInformation)
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000028B8 File Offset: 0x00000AB8
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000028C0 File Offset: 0x00000AC0
		public ApplicationPoolOperationContext OpContext { get; set; }

		// Token: 0x04000001 RID: 1
		public const string Managed_Runtime_Version_20 = "v2.0";

		// Token: 0x04000002 RID: 2
		public const string Managed_Runtime_Version_40 = "v4.0";
	}
}
