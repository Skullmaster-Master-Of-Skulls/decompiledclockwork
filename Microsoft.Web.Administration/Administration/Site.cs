using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200006E RID: 110
	[DebuggerDisplay("Name = {Name}")]
	public sealed class Site : ConfigurationElement
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00007F14 File Offset: 0x00006F14
		internal Site(ServerManager owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00007F24 File Offset: 0x00006F24
		public ApplicationDefaults ApplicationDefaults
		{
			get
			{
				if (this._applicationDefaults == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("applicationDefaults");
					this._applicationDefaults = new ApplicationDefaults(this._owner.ApplicationDefaults);
					this._applicationDefaults.Initialize(base.Configuration, elementByName);
				}
				return this._applicationDefaults;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00007F78 File Offset: 0x00006F78
		public ApplicationCollection Applications
		{
			get
			{
				if (this._applicationCollection == null)
				{
					this._applicationCollection = (ApplicationCollection)base.GetCollection(typeof(ApplicationCollection));
					this._applicationCollection.SetValues(this._owner, this);
				}
				return this._applicationCollection;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00007FB8 File Offset: 0x00006FB8
		public BindingCollection Bindings
		{
			get
			{
				if (this._bindingCollection == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("bindings");
					this._bindingCollection = new BindingCollection(this._owner);
					this._bindingCollection.Initialize(base.Configuration, elementByName);
				}
				return this._bindingCollection;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00008007 File Offset: 0x00007007
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000801C File Offset: 0x0000701C
		public long Id
		{
			get
			{
				return (long)((ulong)((uint)this.IdProperty.Value));
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Resources.UIntArgumentOutOfRange, new object[]
					{
						"Id",
						0U,
						uint.MaxValue
					}));
				}
				this.IdProperty.Value = (uint)value;
				base.SetDirty();
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00008087 File Offset: 0x00007087
		private IAppHostProperty IdProperty
		{
			get
			{
				if (this._idProperty == null)
				{
					this._idProperty = base.AppHostElement.GetPropertyByName("id");
				}
				return this._idProperty;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000080B0 File Offset: 0x000070B0
		public SiteLimits Limits
		{
			get
			{
				if (this._limits == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("limits");
					this._limits = new SiteLimits();
					this._limits.Initialize(base.Configuration, elementByName);
				}
				return this._limits;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000080FC File Offset: 0x000070FC
		public SiteLogFile LogFile
		{
			get
			{
				if (this._logfile == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("logFile");
					this._logfile = new SiteLogFile();
					this._logfile.Initialize(base.Configuration, elementByName);
				}
				return this._logfile;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00008145 File Offset: 0x00007145
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00008157 File Offset: 0x00007157
		public string Name
		{
			get
			{
				return (string)this.NameProperty.Value;
			}
			set
			{
				this.NameProperty.Value = value;
				base.SetDirty();
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000816B File Offset: 0x0000716B
		private IAppHostProperty NameProperty
		{
			get
			{
				if (this._nameProperty == null)
				{
					this._nameProperty = base.AppHostElement.GetPropertyByName("name");
				}
				return this._nameProperty;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00008191 File Offset: 0x00007191
		// (set) Token: 0x06000312 RID: 786 RVA: 0x000081A3 File Offset: 0x000071A3
		public bool ServerAutoStart
		{
			get
			{
				return (bool)base["serverAutoStart"];
			}
			set
			{
				base["serverAutoStart"] = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000081B8 File Offset: 0x000071B8
		public ObjectState State
		{
			get
			{
				ObjectState result;
				try
				{
					result = (ObjectState)base["state"];
				}
				catch (Exception ex)
				{
					COMException ex2 = ex as COMException;
					if (ex2 != null && ex2.ErrorCode == -2147023174)
					{
						result = ObjectState.Stopped;
					}
					else
					{
						if (this._owner.ServerName != null || ServerManager.GetServiceStatus("W3SVC") == ServiceControllerStatus.Running)
						{
							throw;
						}
						result = ObjectState.Stopped;
					}
				}
				return result;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00008228 File Offset: 0x00007228
		public SiteTraceFailedRequestsLogging TraceFailedRequestsLogging
		{
			get
			{
				if (this._traceFailedRequestsLogging == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("traceFailedRequestsLogging");
					this._traceFailedRequestsLogging = new SiteTraceFailedRequestsLogging();
					this._traceFailedRequestsLogging.Initialize(base.Configuration, elementByName);
				}
				return this._traceFailedRequestsLogging;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00008274 File Offset: 0x00007274
		public VirtualDirectoryDefaults VirtualDirectoryDefaults
		{
			get
			{
				if (this._virtualDirectoryDefaults == null)
				{
					IAppHostElement elementByName = base.AppHostElement.GetElementByName("virtualDirectoryDefaults");
					this._virtualDirectoryDefaults = new VirtualDirectoryDefaults(this._owner.VirtualDirectoryDefaults);
					this._virtualDirectoryDefaults.Initialize(base.Configuration, elementByName);
				}
				return this._virtualDirectoryDefaults;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000082C8 File Offset: 0x000072C8
		public Configuration GetWebConfiguration()
		{
			return this._owner.GetWebConfiguration(this.Name);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000082DC File Offset: 0x000072DC
		public ObjectState Start()
		{
			ObjectState state;
			try
			{
				base.ExecuteMethod("Start");
				state = this.State;
			}
			catch (Exception ex)
			{
				if (this._owner.ServerName == null)
				{
					if (ServerManager.GetServiceStatus("WAS") != ServiceControllerStatus.Running)
					{
						throw new ServerManagerException(Resources.UnableToStartWasNotStarted, 100);
					}
					if (ServerManager.GetServiceStatus("W3SVC") != ServiceControllerStatus.Running)
					{
						throw new ServerManagerException(Resources.UnableToStartW3svcNotStarted, 101);
					}
				}
				COMException ex2 = ex as COMException;
				if (ex2 != null)
				{
					if (ex2.ErrorCode == -2147024713)
					{
						throw new ServerManagerException(Resources.WebSiteCannotStartBecausePortUsed, 102);
					}
					if (ex2.ErrorCode == -2147023174)
					{
						throw new ServerManagerException(Resources.UnableToStartW3svcNotStarted, 101);
					}
				}
				throw;
			}
			return state;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008394 File Offset: 0x00007394
		public ObjectState Stop()
		{
			ObjectState result;
			try
			{
				base.ExecuteMethod("Stop");
				result = this.State;
			}
			catch (Exception ex)
			{
				if (this._owner.ServerName != null)
				{
					throw;
				}
				if (ServerManager.GetServiceStatus("WAS") == ServiceControllerStatus.Running && ServerManager.GetServiceStatus("W3SVC") == ServiceControllerStatus.Running)
				{
					COMException ex2 = ex as COMException;
					if (ex2 == null || ex2.ErrorCode != -2147024713)
					{
						throw;
					}
					result = ObjectState.Unknown;
				}
				else
				{
					result = ObjectState.Unknown;
				}
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008414 File Offset: 0x00007414
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000118 RID: 280
		private const uint ERROR_ALREADY_EXISTS = 2147942583U;

		// Token: 0x04000119 RID: 281
		private const uint RPC_S_SERVER_UNAVAILABLE = 2147944122U;

		// Token: 0x0400011A RID: 282
		private ApplicationDefaults _applicationDefaults;

		// Token: 0x0400011B RID: 283
		private VirtualDirectoryDefaults _virtualDirectoryDefaults;

		// Token: 0x0400011C RID: 284
		private ApplicationCollection _applicationCollection;

		// Token: 0x0400011D RID: 285
		private BindingCollection _bindingCollection;

		// Token: 0x0400011E RID: 286
		private SiteLimits _limits;

		// Token: 0x0400011F RID: 287
		private SiteLogFile _logfile;

		// Token: 0x04000120 RID: 288
		private SiteTraceFailedRequestsLogging _traceFailedRequestsLogging;

		// Token: 0x04000121 RID: 289
		private ServerManager _owner;

		// Token: 0x04000122 RID: 290
		private IAppHostProperty _idProperty;

		// Token: 0x04000123 RID: 291
		private IAppHostProperty _nameProperty;
	}
}
