using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005AF RID: 1455
	public class Shape : StateManager, IDefaultCheck
	{
		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000ACAE9 File Offset: 0x000AACE9
		// (set) Token: 0x060033FD RID: 13309 RVA: 0x000ACB09 File Offset: 0x000AAD09
		[DefaultValue("")]
		public string Attribution
		{
			get
			{
				return (string)(base.ViewState["Attribution"] ?? "");
			}
			set
			{
				base.ViewState["Attribution"] = value;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000ACB1C File Offset: 0x000AAD1C
		// (set) Token: 0x060033FF RID: 13311 RVA: 0x000ACB45 File Offset: 0x000AAD45
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 1.0);
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000ACB5D File Offset: 0x000AAD5D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style StyleSettings
		{
			get
			{
				if (this._style == null)
				{
					this._style = new Style();
				}
				return this._style;
			}
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000ACB78 File Offset: 0x000AAD78
		internal override void SetDirty()
		{
			base.SetDirty();
			this.StyleSettings.SetDirty();
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000ACB8C File Offset: 0x000AAD8C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.StyleSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000ACBC4 File Offset: 0x000AADC4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.StyleSettings).SaveViewState()
			};
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000ACBF2 File Offset: 0x000AADF2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.StyleSettings).TrackViewState();
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06003405 RID: 13317 RVA: 0x000ACC05 File Offset: 0x000AAE05
		public bool IsDefault
		{
			get
			{
				return this.Attribution == "" && this.Opacity == 1.0 && this.StyleSettings.IsDefault;
			}
		}

		// Token: 0x04000E2B RID: 3627
		private Style _style;
	}
}
