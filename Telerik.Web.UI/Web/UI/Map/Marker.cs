using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005A4 RID: 1444
	public class Marker : StateManager, IDefaultCheck
	{
		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x000AC25A File Offset: 0x000AA45A
		// (set) Token: 0x060033C9 RID: 13257 RVA: 0x000AC27A File Offset: 0x000AA47A
		[DefaultValue("pinTarget")]
		[TypeConverter(typeof(MarkerShapeStringConverter))]
		public string Shape
		{
			get
			{
				return (string)(base.ViewState["Shape"] ?? "pinTarget");
			}
			set
			{
				base.ViewState["Shape"] = value;
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x000AC28D File Offset: 0x000AA48D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Tooltip TooltipSettings
		{
			get
			{
				if (this._tooltip == null)
				{
					this._tooltip = new Tooltip();
				}
				return this._tooltip;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000AC2A8 File Offset: 0x000AA4A8
		// (set) Token: 0x060033CC RID: 13260 RVA: 0x000AC2D1 File Offset: 0x000AA4D1
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

		// Token: 0x060033CD RID: 13261 RVA: 0x000AC2E9 File Offset: 0x000AA4E9
		internal override void SetDirty()
		{
			base.SetDirty();
			this.TooltipSettings.SetDirty();
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000AC2FC File Offset: 0x000AA4FC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.TooltipSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000AC334 File Offset: 0x000AA534
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.TooltipSettings).SaveViewState()
			};
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000AC362 File Offset: 0x000AA562
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.TooltipSettings).TrackViewState();
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000AC375 File Offset: 0x000AA575
		public bool IsDefault
		{
			get
			{
				return this.Shape == "pinTarget" && this.TooltipSettings.IsDefault && this.Opacity == 1.0;
			}
		}

		// Token: 0x04000E21 RID: 3617
		private Tooltip _tooltip;
	}
}
