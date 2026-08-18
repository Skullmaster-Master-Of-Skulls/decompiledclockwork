using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x0200043C RID: 1084
	public class Bubble : StateManager, IDefaultCheck
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x0007E9D9 File Offset: 0x0007CBD9
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x0007E9F9 File Offset: 0x0007CBF9
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

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x0007EA0C File Offset: 0x0007CC0C
		// (set) Token: 0x060026DD RID: 9949 RVA: 0x0007EA35 File Offset: 0x0007CC35
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

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x0007EA4D File Offset: 0x0007CC4D
		// (set) Token: 0x060026DF RID: 9951 RVA: 0x0007EA76 File Offset: 0x0007CC76
		[DefaultValue(100.0)]
		public double MaxSize
		{
			get
			{
				return (double)(base.ViewState["MaxSize"] ?? 100.0);
			}
			set
			{
				base.ViewState["MaxSize"] = value;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x0007EA8E File Offset: 0x0007CC8E
		// (set) Token: 0x060026E1 RID: 9953 RVA: 0x0007EAB7 File Offset: 0x0007CCB7
		[DefaultValue(0.0)]
		public double MinSize
		{
			get
			{
				return (double)(base.ViewState["MinSize"] ?? 0.0);
			}
			set
			{
				base.ViewState["MinSize"] = value;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x0007EACF File Offset: 0x0007CCCF
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

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x0007EAEA File Offset: 0x0007CCEA
		// (set) Token: 0x060026E4 RID: 9956 RVA: 0x0007EB0A File Offset: 0x0007CD0A
		[DefaultValue("circle")]
		public string Symbol
		{
			get
			{
				return (string)(base.ViewState["Symbol"] ?? "circle");
			}
			set
			{
				base.ViewState["Symbol"] = value;
			}
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x0007EB1D File Offset: 0x0007CD1D
		internal override void SetDirty()
		{
			base.SetDirty();
			this.StyleSettings.SetDirty();
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x0007EB30 File Offset: 0x0007CD30
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.StyleSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x0007EB68 File Offset: 0x0007CD68
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.StyleSettings).SaveViewState()
			};
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x0007EB96 File Offset: 0x0007CD96
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.StyleSettings).TrackViewState();
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x0007EBAC File Offset: 0x0007CDAC
		public bool IsDefault
		{
			get
			{
				return this.Attribution == "" && this.Opacity == 1.0 && this.MaxSize == 100.0 && this.MinSize == 0.0 && this.StyleSettings.IsDefault && this.Symbol == "circle";
			}
		}

		// Token: 0x04000A00 RID: 2560
		private Style _style;
	}
}
