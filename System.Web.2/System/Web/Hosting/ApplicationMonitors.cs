using System;

namespace System.Web.Hosting
{
	// Token: 0x02000783 RID: 1923
	public sealed class ApplicationMonitors
	{
		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x0013F564 File Offset: 0x0013D764
		// (set) Token: 0x06005C44 RID: 23620 RVA: 0x0013F56C File Offset: 0x0013D76C
		public IApplicationMonitor MemoryMonitor
		{
			get
			{
				return this._memoryMonitor;
			}
			set
			{
				if (this._memoryMonitor != null && this._memoryMonitor != value)
				{
					this._memoryMonitor.Stop();
					this._memoryMonitor.Dispose();
				}
				this._memoryMonitor = value;
				if (this._memoryMonitor != null)
				{
					this._memoryMonitor.Start();
				}
			}
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x0013F5BC File Offset: 0x0013D7BC
		internal ApplicationMonitors()
		{
			this._memoryMonitor = new AspNetMemoryMonitor();
			this._memoryMonitor.Start();
			ApplicationMonitors.AppMonitorRegisteredObject obj = new ApplicationMonitors.AppMonitorRegisteredObject(this);
			HostingEnvironment.RegisterObject(obj);
		}

		// Token: 0x0400308C RID: 12428
		private IApplicationMonitor _memoryMonitor;

		// Token: 0x02000A4A RID: 2634
		private class AppMonitorRegisteredObject : IRegisteredObject
		{
			// Token: 0x06006EA5 RID: 28325 RVA: 0x0018A214 File Offset: 0x00188414
			public AppMonitorRegisteredObject(ApplicationMonitors appMonitors)
			{
				this._appMonitors = appMonitors;
			}

			// Token: 0x06006EA6 RID: 28326 RVA: 0x0018A224 File Offset: 0x00188424
			public void Stop(bool immediate)
			{
				if (this._appMonitors != null)
				{
					IApplicationMonitor memoryMonitor = this._appMonitors.MemoryMonitor;
					if (memoryMonitor != null)
					{
						memoryMonitor.Stop();
						memoryMonitor.Dispose();
					}
				}
				HostingEnvironment.UnregisterObject(this);
			}

			// Token: 0x04003B33 RID: 15155
			private ApplicationMonitors _appMonitors;
		}
	}
}
