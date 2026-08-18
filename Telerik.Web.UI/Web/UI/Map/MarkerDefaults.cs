using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005A6 RID: 1446
	public class MarkerDefaults : StateManager, IDefaultCheck
	{
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x000AC43E File Offset: 0x000AA63E
		// (set) Token: 0x060033D7 RID: 13271 RVA: 0x000AC45E File Offset: 0x000AA65E
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

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x000AC471 File Offset: 0x000AA671
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

		// Token: 0x060033D9 RID: 13273 RVA: 0x000AC48C File Offset: 0x000AA68C
		internal override void SetDirty()
		{
			base.SetDirty();
			this.TooltipSettings.SetDirty();
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000AC4A0 File Offset: 0x000AA6A0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.TooltipSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000AC4D8 File Offset: 0x000AA6D8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.TooltipSettings).SaveViewState()
			};
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000AC506 File Offset: 0x000AA706
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.TooltipSettings).TrackViewState();
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x000AC519 File Offset: 0x000AA719
		public bool IsDefault
		{
			get
			{
				return this.Shape == "pinTarget" && this.TooltipSettings.IsDefault;
			}
		}

		// Token: 0x04000E22 RID: 3618
		private Tooltip _tooltip;
	}
}
