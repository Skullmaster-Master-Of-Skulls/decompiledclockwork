using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x020001EE RID: 494
	internal class DataGridViewComponentPropertyGridSite : ISite, IServiceProvider
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x00060DC6 File Offset: 0x0005FDC6
		public DataGridViewComponentPropertyGridSite(IServiceProvider sp, IComponent comp)
		{
			this.sp = sp;
			this.comp = comp;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00060DDC File Offset: 0x0005FDDC
		public IComponent Component
		{
			get
			{
				return this.comp;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00060DE4 File Offset: 0x0005FDE4
		public IContainer Container
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00060DE7 File Offset: 0x0005FDE7
		public bool DesignMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00060DEA File Offset: 0x0005FDEA
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00060DED File Offset: 0x0005FDED
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

		// Token: 0x06001305 RID: 4869 RVA: 0x00060DF0 File Offset: 0x0005FDF0
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

		// Token: 0x0400118C RID: 4492
		private IServiceProvider sp;

		// Token: 0x0400118D RID: 4493
		private IComponent comp;

		// Token: 0x0400118E RID: 4494
		private bool inGetService;
	}
}
