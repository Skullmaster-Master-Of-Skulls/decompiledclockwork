using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002AD RID: 685
	public class Selectable : StateManager, IDefaultCheck
	{
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x0004FEE2 File Offset: 0x0004E0E2
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x0004FF03 File Offset: 0x0004E103
		[DefaultValue(true)]
		public bool Multiple
		{
			get
			{
				return (bool)(base.ViewState["Multiple"] ?? true);
			}
			set
			{
				base.ViewState["Multiple"] = value;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x0004FF1B File Offset: 0x0004E11B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Stroke StrokeSettings
		{
			get
			{
				if (this._stroke == null)
				{
					this._stroke = new Stroke();
				}
				return this._stroke;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0004FF36 File Offset: 0x0004E136
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0004FF57 File Offset: 0x0004E157
		[DefaultValue(ModifierKey.None)]
		public ModifierKey Key
		{
			get
			{
				return (ModifierKey)(base.ViewState["Key"] ?? ModifierKey.None);
			}
			set
			{
				base.ViewState["Key"] = value;
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0004FF6F File Offset: 0x0004E16F
		internal override void SetDirty()
		{
			base.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0004FF84 File Offset: 0x0004E184
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0004FFBC File Offset: 0x0004E1BC
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0004FFEA File Offset: 0x0004E1EA
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0004FFFD File Offset: 0x0004E1FD
		public bool IsDefault
		{
			get
			{
				return this.Multiple && this.StrokeSettings.IsDefault && this.Key == ModifierKey.None;
			}
		}

		// Token: 0x04000672 RID: 1650
		private Stroke _stroke;
	}
}
