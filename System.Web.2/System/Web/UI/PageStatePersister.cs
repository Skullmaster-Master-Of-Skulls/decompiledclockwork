using System;

namespace System.Web.UI
{
	// Token: 0x020002DE RID: 734
	public abstract class PageStatePersister
	{
		// Token: 0x06002230 RID: 8752 RVA: 0x0006FF62 File Offset: 0x0006E162
		protected PageStatePersister(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page", SR.GetString("PageStatePersister_PageCannotBeNull"));
			}
			this._page = page;
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x0006FF89 File Offset: 0x0006E189
		// (set) Token: 0x06002232 RID: 8754 RVA: 0x0006FF91 File Offset: 0x0006E191
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

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x0006FF9A File Offset: 0x0006E19A
		protected IStateFormatter StateFormatter
		{
			get
			{
				return this.StateFormatter2;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x0006FFA2 File Offset: 0x0006E1A2
		internal IStateFormatter2 StateFormatter2
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

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0006FFC3 File Offset: 0x0006E1C3
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x0006FFCB File Offset: 0x0006E1CB
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

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x0006FFD4 File Offset: 0x0006E1D4
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x0006FFDC File Offset: 0x0006E1DC
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

		// Token: 0x06002239 RID: 8761
		public abstract void Load();

		// Token: 0x0600223A RID: 8762
		public abstract void Save();

		// Token: 0x04001C2D RID: 7213
		private Page _page;

		// Token: 0x04001C2E RID: 7214
		private object _viewState;

		// Token: 0x04001C2F RID: 7215
		private object _controlState;

		// Token: 0x04001C30 RID: 7216
		private IStateFormatter2 _stateFormatter;
	}
}
