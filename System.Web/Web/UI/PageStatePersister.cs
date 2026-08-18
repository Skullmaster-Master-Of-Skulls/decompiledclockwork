using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003F7 RID: 1015
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class PageStatePersister
	{
		// Token: 0x0600322D RID: 12845 RVA: 0x000DC512 File Offset: 0x000DB512
		protected PageStatePersister(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page", SR.GetString("PageStatePersister_PageCannotBeNull"));
			}
			this._page = page;
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000DC539 File Offset: 0x000DB539
		// (set) Token: 0x0600322F RID: 12847 RVA: 0x000DC541 File Offset: 0x000DB541
		public object ControlState
		{
			get
			{
				return this._controlState;
			}
			set
			{
				this._controlState = value;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000DC54A File Offset: 0x000DB54A
		protected IStateFormatter StateFormatter
		{
			get
			{
				if (this._stateFormatter == null)
				{
					this._stateFormatter = this.Page.CreateStateFormatter();
				}
				return this._stateFormatter;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000DC56B File Offset: 0x000DB56B
		// (set) Token: 0x06003232 RID: 12850 RVA: 0x000DC573 File Offset: 0x000DB573
		protected Page Page
		{
			get
			{
				return this._page;
			}
			set
			{
				this._page = value;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000DC57C File Offset: 0x000DB57C
		// (set) Token: 0x06003234 RID: 12852 RVA: 0x000DC584 File Offset: 0x000DB584
		public object ViewState
		{
			get
			{
				return this._viewState;
			}
			set
			{
				this._viewState = value;
			}
		}

		// Token: 0x06003235 RID: 12853
		public abstract void Load();

		// Token: 0x06003236 RID: 12854
		public abstract void Save();

		// Token: 0x040022F8 RID: 8952
		private Page _page;

		// Token: 0x040022F9 RID: 8953
		private object _viewState;

		// Token: 0x040022FA RID: 8954
		private object _controlState;

		// Token: 0x040022FB RID: 8955
		private IStateFormatter _stateFormatter;
	}
}
