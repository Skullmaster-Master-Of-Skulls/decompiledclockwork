using System;

namespace System.Web.WebPages
{
	// Token: 0x02000015 RID: 21
	public class BrowserOverrideStores
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000378F File Offset: 0x0000198F
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000379B File Offset: 0x0000199B
		public static BrowserOverrideStore Current
		{
			get
			{
				return BrowserOverrideStores._instance.CurrentInternal;
			}
			set
			{
				BrowserOverrideStores._instance.CurrentInternal = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000037A8 File Offset: 0x000019A8
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000037B0 File Offset: 0x000019B0
		internal BrowserOverrideStore CurrentInternal
		{
			get
			{
				return this._currentOverrideStore;
			}
			set
			{
				this._currentOverrideStore = (value ?? new RequestBrowserOverrideStore());
			}
		}

		// Token: 0x0400002E RID: 46
		private static BrowserOverrideStores _instance = new BrowserOverrideStores();

		// Token: 0x0400002F RID: 47
		private BrowserOverrideStore _currentOverrideStore = new CookieBrowserOverrideStore();
	}
}
