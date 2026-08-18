using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000265 RID: 613
	public class ShapeFill : StateManager, IDefaultCheck
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0004BB20 File Offset: 0x00049D20
		// (set) Token: 0x0600163B RID: 5691 RVA: 0x0004BB40 File Offset: 0x00049D40
		[DefaultValue("")]
		public string Color
		{
			get
			{
				return (string)(base.ViewState["Color"] ?? "");
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0004BB53 File Offset: 0x00049D53
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0004BB7C File Offset: 0x00049D7C
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

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0004BB94 File Offset: 0x00049D94
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Gradient GradientSettings
		{
			get
			{
				if (this._gradient == null)
				{
					this._gradient = new Gradient();
				}
				return this._gradient;
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0004BBAF File Offset: 0x00049DAF
		internal override void SetDirty()
		{
			base.SetDirty();
			this.GradientSettings.SetDirty();
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0004BBC4 File Offset: 0x00049DC4
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.GradientSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0004BBFC File Offset: 0x00049DFC
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.GradientSettings).SaveViewState()
			};
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0004BC2A File Offset: 0x00049E2A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.GradientSettings).TrackViewState();
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0004BC3D File Offset: 0x00049E3D
		public bool IsDefault
		{
			get
			{
				return this.Color == "" && this.Opacity == 1.0 && this.GradientSettings.IsDefault;
			}
		}

		// Token: 0x040005E9 RID: 1513
		private Gradient _gradient;
	}
}
