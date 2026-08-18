using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000367 RID: 871
	internal class PropertyGridSite : ISite, IServiceProvider
	{
		// Token: 0x060023CC RID: 9164 RVA: 0x000DFE4E File Offset: 0x000DE04E
		public PropertyGridSite(IServiceProvider sp, IComponent comp)
		{
			this.sp = sp;
			this.comp = comp;
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x000DFE64 File Offset: 0x000DE064
		public IComponent Component
		{
			get
			{
				return this.comp;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x00003598 File Offset: 0x00001798
		public IContainer Container
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x0000445B File Offset: 0x0000265B
		public bool DesignMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x00003937 File Offset: 0x00001B37
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

		// Token: 0x060023D2 RID: 9170 RVA: 0x000DFE6C File Offset: 0x000DE06C
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

		// Token: 0x04001A3D RID: 6717
		private IServiceProvider sp;

		// Token: 0x04001A3E RID: 6718
		private IComponent comp;

		// Token: 0x04001A3F RID: 6719
		private bool inGetService;
	}
}
