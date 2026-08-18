using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002C2 RID: 706
	internal class DataGridViewComponentPropertyGridSite : ISite, IServiceProvider
	{
		// Token: 0x06001C03 RID: 7171 RVA: 0x000A901E File Offset: 0x000A721E
		public DataGridViewComponentPropertyGridSite(IServiceProvider sp, IComponent comp)
		{
			this.sp = sp;
			this.comp = comp;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x000A9034 File Offset: 0x000A7234
		public IComponent Component
		{
			get
			{
				return this.comp;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x00003598 File Offset: 0x00001798
		public IContainer Container
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0000445B File Offset: 0x0000265B
		public bool DesignMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x00003937 File Offset: 0x00001B37
		public string Name
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x000A903C File Offset: 0x000A723C
		public object GetService(Type t)
		{
			if (!this.inGetService && this.sp != null)
			{
				try
				{
					this.inGetService = true;
					return this.sp.GetService(t);
				}
				finally
				{
					this.inGetService = false;
				}
			}
			return null;
		}

		// Token: 0x040016C1 RID: 5825
		private IServiceProvider sp;

		// Token: 0x040016C2 RID: 5826
		private IComponent comp;

		// Token: 0x040016C3 RID: 5827
		private bool inGetService;
	}
}
