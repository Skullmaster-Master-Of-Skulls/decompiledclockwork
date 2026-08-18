using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
	// Token: 0x0200042D RID: 1069
	public class TelerikLicenseProvider : LicenseProvider, ILicenseProvider
	{
		// Token: 0x06002660 RID: 9824 RVA: 0x0007DBAA File Offset: 0x0007BDAA
		public TelerikLicenseProvider()
		{
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x0007DBB2 File Offset: 0x0007BDB2
		internal TelerikLicenseProvider(ISessionManagerFactory factory)
		{
			this._manager = factory.TryCreateManager();
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06002662 RID: 9826 RVA: 0x0007DBC8 File Offset: 0x0007BDC8
		// (remove) Token: 0x06002663 RID: 9827 RVA: 0x0007DC00 File Offset: 0x0007BE00
		private event ProductUsedEventHandler ProductUsed;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06002664 RID: 9828 RVA: 0x0007DC35 File Offset: 0x0007BE35
		// (remove) Token: 0x06002665 RID: 9829 RVA: 0x0007DC3E File Offset: 0x0007BE3E
		event ProductUsedEventHandler ILicenseProvider.ProductUsed
		{
			add
			{
				this.ProductUsed += value;
			}
			remove
			{
				this.ProductUsed -= value;
			}
		}

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x06002666 RID: 9830 RVA: 0x0007DC48 File Offset: 0x0007BE48
		// (remove) Token: 0x06002667 RID: 9831 RVA: 0x0007DC80 File Offset: 0x0007BE80
		private event ComponentUsedEventHandler ComponentUsed;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06002668 RID: 9832 RVA: 0x0007DCB5 File Offset: 0x0007BEB5
		// (remove) Token: 0x06002669 RID: 9833 RVA: 0x0007DCBE File Offset: 0x0007BEBE
		event ComponentUsedEventHandler ILicenseProvider.ComponentUsed
		{
			add
			{
				this.ComponentUsed += value;
			}
			remove
			{
				this.ComponentUsed -= value;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x0007DCC7 File Offset: 0x0007BEC7
		internal TypesCollection RegisteredTypes
		{
			get
			{
				return this.CurrentSession.Components;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x0007DCD4 File Offset: 0x0007BED4
		internal virtual IUsageTracker UsageTracker
		{
			get
			{
				if (this._usageTracker == null)
				{
					this._usageTracker = new UsageTracker(this, new TransportService(Config.GetInstance()));
				}
				return this._usageTracker;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x0007DCFA File Offset: 0x0007BEFA
		internal virtual Session CurrentSession
		{
			get
			{
				if (this._session == null)
				{
					this._session = this._manager.GetCurrentSession();
				}
				return this._session;
			}
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0007DD1C File Offset: 0x0007BF1C
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			ILicenseKey key = new DefaultKey();
			try
			{
				LicenseContextManager licenseContextManager = new LicenseContextManager(new LicenseContextData(context, type, allowExceptions));
				switch (context.UsageMode)
				{
				case LicenseUsageMode.Designtime:
					key = new DesignTimeKey();
					licenseContextManager.SaveLicenseKey(type, key);
					this.ProcessEvents(licenseContextManager.ContextData, type, key);
					goto IL_52;
				}
				key = licenseContextManager.ExtractLicenseKey(type);
				IL_52:;
			}
			catch (Exception)
			{
			}
			finally
			{
				this.UsageTracker.StopTracking();
			}
			return LicenseFactory.CreateLicense(key);
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x0007DDB0 File Offset: 0x0007BFB0
		internal virtual void ProcessEvents(ILicenseContextData data, Type type, ILicenseKey key)
		{
			this.EnsureTracking();
			if (this.TryEnsureSessionManager(data.Context))
			{
				this.RegisteredTypes.TryAdd(type.FullName);
				if (!this.CurrentSession.GetProductUsageLogged())
				{
					this.RaiseProductUsed(type, this.CurrentSession.Id);
					this.CurrentSession.SetProductUsageLogged();
				}
				if (this.CurrentSession.GetHasPendingChange())
				{
					this.RaiseComponentUsed(type, this.CurrentSession.Id);
					this.CurrentSession.SetPendingChangeResolved();
				}
			}
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x0007DE38 File Offset: 0x0007C038
		protected virtual bool TryEnsureSessionManager(LicenseContext context)
		{
			if (this._manager == null)
			{
				EnvSessionManagerFactory envSessionManagerFactory = new EnvSessionManagerFactory(context);
				this._manager = envSessionManagerFactory.TryCreateManager();
			}
			return this._manager != null;
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x0007DE6C File Offset: 0x0007C06C
		private void EnsureTracking()
		{
			if (!this.UsageTracker.IsTracking())
			{
				this.UsageTracker.StartTracking();
			}
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x0007DE88 File Offset: 0x0007C088
		private void RaiseProductUsed(Type type, string sessionId)
		{
			ProductUsedEventHandler productUsed = this.ProductUsed;
			if (productUsed != null)
			{
				productUsed(this, new ProductUsedEventArgs(type, sessionId));
			}
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x0007DEB0 File Offset: 0x0007C0B0
		private void RaiseComponentUsed(Type type, string sessionId)
		{
			ComponentUsedEventHandler componentUsed = this.ComponentUsed;
			if (componentUsed != null)
			{
				componentUsed(this, new ComponentUsedEventArgs(type, sessionId));
			}
		}

		// Token: 0x040009CD RID: 2509
		private ISessionManager _manager;

		// Token: 0x040009CE RID: 2510
		private IUsageTracker _usageTracker;

		// Token: 0x040009CF RID: 2511
		private Session _session;
	}
}
