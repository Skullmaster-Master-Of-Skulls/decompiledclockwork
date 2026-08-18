using System;

namespace Telerik.Web.UI
{
	// Token: 0x020019FD RID: 6653
	[Serializable]
	public class RadRotatorItemData
	{
		// Token: 0x0601019E RID: 65950 RVA: 0x0039EB46 File Offset: 0x0039CD46
		public RadRotatorItemData()
		{
			this.Visible = true;
			this.CssClass = string.Empty;
			this.Html = string.Empty;
		}

		// Token: 0x17004DB7 RID: 19895
		// (get) Token: 0x0601019F RID: 65951 RVA: 0x0039EB6B File Offset: 0x0039CD6B
		// (set) Token: 0x060101A0 RID: 65952 RVA: 0x0039EB73 File Offset: 0x0039CD73
		public string Html
		{
			get
			{
				return this._html;
			}
			set
			{
				this._html = value;
			}
		}

		// Token: 0x17004DB8 RID: 19896
		// (get) Token: 0x060101A1 RID: 65953 RVA: 0x0039EB7C File Offset: 0x0039CD7C
		// (set) Token: 0x060101A2 RID: 65954 RVA: 0x0039EB84 File Offset: 0x0039CD84
		public bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				this._visible = value;
			}
		}

		// Token: 0x17004DB9 RID: 19897
		// (get) Token: 0x060101A3 RID: 65955 RVA: 0x0039EB8D File Offset: 0x0039CD8D
		// (set) Token: 0x060101A4 RID: 65956 RVA: 0x0039EB95 File Offset: 0x0039CD95
		public string CssClass
		{
			get
			{
				return this._cssClass;
			}
			set
			{
				this._cssClass = value;
			}
		}

		// Token: 0x040048EA RID: 18666
		private bool _visible;

		// Token: 0x040048EB RID: 18667
		private string _cssClass;

		// Token: 0x040048EC RID: 18668
		private string _html;
	}
}
