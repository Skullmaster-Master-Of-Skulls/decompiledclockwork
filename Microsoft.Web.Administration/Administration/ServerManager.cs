using System;
using System.ComponentModel;
using System.ServiceProcess;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200006C RID: 108
	public sealed class ServerManager : IDisposable
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000799C File Offset: 0x0000699C
		~ServerManager()
		{
			this.Dispose(false);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000079CC File Offset: 0x000069CC
		public ServerManager() : this(null)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000079D5 File Offset: 0x000069D5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ServerManager(bool readOnly, string applicationHostConfigurationPath) : this(applicationHostConfigurationPath)
		{
			this._readOnly = readOnly;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000079E5 File Offset: 0x000069E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public ServerManager(string applicationHostConfigurationPath)
		{
			this._configurationManager = new ConfigurationManager(this, applicationHostConfigurationPath);
			this._applicationHostConfigurationPath = applicationHostConfigurationPath;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00007A01 File Offset: 0x00006A01
		public ApplicationDefaults ApplicationDefaults
		{
			get
			{
				if (this._applicationDefaults == null)
				{
					this._applicationDefaults = (ApplicationDefaults)this.SitesSection.GetChildElement("applicationDefaults", typeof(ApplicationDefaults));
				}
				return this._applicationDefaults;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00007A36 File Offset: 0x00006A36
		public ApplicationPoolDefaults ApplicationPoolDefaults
		{
			get
			{
				if (this._applicationPoolDefaults == null)
				{
					this._applicationPoolDefaults = (ApplicationPoolDefaults)this.ApplicationPoolsSection.GetChildElement("applicationPoolDefaults", typeof(ApplicationPoolDefaults));
				}
				return this._applicationPoolDefaults;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00007A6C File Offset: 0x00006A6C
		public ApplicationPoolCollection ApplicationPools
		{
			get
			{
				if (this._applicationPools == null)
				{
					ConfigurationSection applicationPoolsSection = this.ApplicationPoolsSection;
					this._applicationPools = new ApplicationPoolCollection(this);
					this._applicationPools.Initialize(applicationPoolsSection.Configuration, applicationPoolsSection.AppHostElement);
				}
				return this._applicationPools;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00007AB1 File Offset: 0x00006AB1
		private ConfigurationSection ApplicationPoolsSection
		{
			get
			{
				if (this._applicationPoolsSection == null)
				{
					this._applicationPoolsSection = this.GetApplicationHostConfiguration().GetSection("system.applicationHost/applicationPools");
				}
				return this._applicationPoolsSection;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00007AD7 File Offset: 0x00006AD7
		internal BindingManager BindingManager
		{
			get
			{
				if (this._bindingManager == null)
				{
					this._bindingManager = new BindingManager(this);
				}
				return this._bindingManager;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00007AF3 File Offset: 0x00006AF3
		internal bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00007AFB File Offset: 0x00006AFB
		internal string ServerName
		{
			get
			{
				return this._serverName;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00007B03 File Offset: 0x00006B03
		public SiteDefaults SiteDefaults
		{
			get
			{
				if (this._siteDefaults == null)
				{
					this._siteDefaults = (SiteDefaults)this.SitesSection.GetChildElement("siteDefaults", typeof(SiteDefaults));
				}
				return this._siteDefaults;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00007B38 File Offset: 0x00006B38
		public SiteCollection Sites
		{
			get
			{
				if (this._sites == null)
				{
					ConfigurationSection sitesSection = this.SitesSection;
					this._sites = new SiteCollection(this);
					this._sites.Initialize(sitesSection.Configuration, sitesSection.AppHostElement);
				}
				return this._sites;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00007B7D File Offset: 0x00006B7D
		private ConfigurationSection SitesSection
		{
			get
			{
				if (this._sitesSection == null)
				{
					this._sitesSection = this.GetApplicationHostConfiguration().GetSection("system.applicationHost/sites");
				}
				return this._sitesSection;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00007BA3 File Offset: 0x00006BA3
		public VirtualDirectoryDefaults VirtualDirectoryDefaults
		{
			get
			{
				if (this._virtualDirectoryDefaults == null)
				{
					this._virtualDirectoryDefaults = (VirtualDirectoryDefaults)this.SitesSection.GetChildElement("virtualDirectoryDefaults", typeof(VirtualDirectoryDefaults));
				}
				return this._virtualDirectoryDefaults;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00007BD8 File Offset: 0x00006BD8
		public WorkerProcessCollection WorkerProcesses
		{
			get
			{
				if (this._workerProcesses == null)
				{
					this.EnsureNotDisposed();
					this._workerProcesses = (WorkerProcessCollection)this.ApplicationPoolsSection.GetCollection("workerProcesses", typeof(WorkerProcessCollection));
				}
				return this._workerProcesses;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00007C13 File Offset: 0x00006C13
		public void CommitChanges()
		{
			this.EnsureNotDisposed();
			this._configurationManager.CommitChanges();
			if (this._bindingManager != null)
			{
				this.BindingManager.Save();
			}
			this.InvalidateCachedReferences();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00007C3F File Offset: 0x00006C3F
		internal void ConfigurationInvalidated(Configuration config)
		{
			if ((this._applicationPoolsSection != null && this._applicationPoolsSection.Configuration == config) || (this._sitesSection != null && this._sitesSection.Configuration == config))
			{
				this.InvalidateCachedReferences();
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00007C73 File Offset: 0x00006C73
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00007C82 File Offset: 0x00006C82
		private void Dispose(bool disposing)
		{
			if (this._configurationManager != null)
			{
				this._configurationManager.Release();
				this._configurationManager = null;
			}
			this.InvalidateCachedReferences();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00007CA4 File Offset: 0x00006CA4
		internal void EnsureLocal()
		{
			if (!string.IsNullOrEmpty(this._serverName))
			{
				throw new NotSupportedException(Resources.RemoteNotSupported);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00007CBE File Offset: 0x00006CBE
		private void EnsureNotDisposed()
		{
			if (this._configurationManager == null)
			{
				throw new ObjectDisposedException("ServerManager");
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00007CD3 File Offset: 0x00006CD3
		public Configuration GetAdministrationConfiguration()
		{
			this.EnsureNotDisposed();
			return this._configurationManager.GetAdministrationConfiguration(null, null);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00007CE8 File Offset: 0x00006CE8
		public Configuration GetAdministrationConfiguration(WebConfigurationMap configMap, string configurationPath)
		{
			this.EnsureNotDisposed();
			return this._configurationManager.GetAdministrationConfiguration(configMap, configurationPath);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00007CFD File Offset: 0x00006CFD
		public Configuration GetApplicationHostConfiguration()
		{
			this.EnsureNotDisposed();
			return this._configurationManager.GetApplicationHostConfiguration();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00007D10 File Offset: 0x00006D10
		public Configuration GetRedirectionConfiguration()
		{
			this.EnsureNotDisposed();
			return this._configurationManager.GetConfiguration("MACHINE/REDIRECTION", "MACHINE/REDIRECTION", false);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00007D2E File Offset: 0x00006D2E
		public Configuration GetWebConfiguration(string siteName)
		{
			this.EnsureNotDisposed();
			return this._configurationManager.GetWebConfiguration(null, siteName);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00007D43 File Offset: 0x00006D43
		public Configuration GetWebConfiguration(string siteName, string virtualPath)
		{
			this.EnsureNotDisposed();
			return this._configurationManager.GetWebConfiguration(null, ConfigurationManager.CombineConfigurationPath(siteName, virtualPath));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00007D5E File Offset: 0x00006D5E
		public Configuration GetWebConfiguration(WebConfigurationMap configMap, string configurationPath)
		{
			this.EnsureNotDisposed();
			if (configMap == null)
			{
				throw new ArgumentNullException("configMap");
			}
			return this._configurationManager.GetWebConfiguration(configMap, configurationPath);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00007D84 File Offset: 0x00006D84
		private void InvalidateCachedReferences()
		{
			this._bindingManager = null;
			this._sitesSection = null;
			this._applicationPoolsSection = null;
			this._sites = null;
			this._siteDefaults = null;
			this._applicationDefaults = null;
			this._virtualDirectoryDefaults = null;
			this._applicationPools = null;
			this._applicationPoolDefaults = null;
			this._workerProcesses = null;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00007DD8 File Offset: 0x00006DD8
		public static ServerManager OpenRemote(string serverName)
		{
			return new ServerManager(null)
			{
				_serverName = serverName
			};
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00007DF4 File Offset: 0x00006DF4
		public void SetMetadata(string metadataType, object value)
		{
			this.EnsureNotDisposed();
			if (string.Equals(metadataType, "ServiceModel", StringComparison.OrdinalIgnoreCase))
			{
				this._configurationManager.ServiceModel = (bool)value;
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00007E21 File Offset: 0x00006E21
		public object GetMetadata(string metadataType)
		{
			this.EnsureNotDisposed();
			if (string.Equals(metadataType, "ServiceModel", StringComparison.OrdinalIgnoreCase))
			{
				return this._configurationManager.ServiceModel;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00007E4D File Offset: 0x00006E4D
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00007E5C File Offset: 0x00006E5C
		internal static ServiceControllerStatus GetServiceStatus(string serviceName)
		{
			ServiceController serviceController = null;
			ServiceControllerStatus status;
			try
			{
				serviceController = new ServiceController(serviceName);
				status = serviceController.Status;
			}
			finally
			{
				if (serviceController != null)
				{
					serviceController.Dispose();
					serviceController = null;
				}
			}
			return status;
		}

		// Token: 0x04000108 RID: 264
		private SiteCollection _sites;

		// Token: 0x04000109 RID: 265
		private SiteDefaults _siteDefaults;

		// Token: 0x0400010A RID: 266
		private ApplicationDefaults _applicationDefaults;

		// Token: 0x0400010B RID: 267
		private VirtualDirectoryDefaults _virtualDirectoryDefaults;

		// Token: 0x0400010C RID: 268
		private ApplicationPoolCollection _applicationPools;

		// Token: 0x0400010D RID: 269
		private ApplicationPoolDefaults _applicationPoolDefaults;

		// Token: 0x0400010E RID: 270
		private WorkerProcessCollection _workerProcesses;

		// Token: 0x0400010F RID: 271
		private ConfigurationManager _configurationManager;

		// Token: 0x04000110 RID: 272
		private BindingManager _bindingManager;

		// Token: 0x04000111 RID: 273
		private ConfigurationSection _sitesSection;

		// Token: 0x04000112 RID: 274
		private ConfigurationSection _applicationPoolsSection;

		// Token: 0x04000113 RID: 275
		private string _applicationHostConfigurationPath;

		// Token: 0x04000114 RID: 276
		private string _serverName;

		// Token: 0x04000115 RID: 277
		private bool _readOnly;
	}
}
