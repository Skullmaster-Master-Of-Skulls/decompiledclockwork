using System;
using log4net.Repository;

namespace log4net.Plugin
{
	// Token: 0x020000C0 RID: 192
	public abstract class PluginSkeleton : IPlugin
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00011714 File Offset: 0x0000F914
		protected PluginSkeleton(string name)
		{
			this.m_name = name;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00011723 File Offset: 0x0000F923
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001172B File Offset: 0x0000F92B
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00011734 File Offset: 0x0000F934
		public virtual void Attach(ILoggerRepository repository)
		{
			this.m_repository = repository;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001173D File Offset: 0x0000F93D
		public virtual void Shutdown()
		{
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001173F File Offset: 0x0000F93F
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00011747 File Offset: 0x0000F947
		protected virtual ILoggerRepository LoggerRepository
		{
			get
			{
				return this.m_repository;
			}
			set
			{
				this.m_repository = value;
			}
		}

		// Token: 0x04000245 RID: 581
		private string m_name;

		// Token: 0x04000246 RID: 582
		private ILoggerRepository m_repository;
	}
}
