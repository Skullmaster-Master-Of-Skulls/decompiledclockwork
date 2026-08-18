using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000FAF RID: 4015
	public class DockToggleCommand : DockCommand
	{
		// Token: 0x06009A14 RID: 39444 RVA: 0x00225B6B File Offset: 0x00223D6B
		public DockToggleCommand() : this("Telerik.Web.UI.DockToggleCommand", "rdCustom", "rdCustom", "Custom", "Custom", "Custom", false)
		{
		}

		// Token: 0x06009A15 RID: 39445 RVA: 0x00225B92 File Offset: 0x00223D92
		protected DockToggleCommand(string clientTypeName, string cssClass, string alternateCssClass, string name, string text, string alternateText, bool autoPostBack) : base(clientTypeName, cssClass, name, text, autoPostBack)
		{
			this._alternateCssClass = alternateCssClass;
			this._alternateText = alternateText;
		}

		// Token: 0x170030BE RID: 12478
		// (get) Token: 0x06009A16 RID: 39446 RVA: 0x00225BB8 File Offset: 0x00223DB8
		// (set) Token: 0x06009A17 RID: 39447 RVA: 0x00225BC0 File Offset: 0x00223DC0
		[DefaultValue(DockToggleCommandState.Primary)]
		public virtual DockToggleCommandState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		// Token: 0x170030BF RID: 12479
		// (get) Token: 0x06009A18 RID: 39448 RVA: 0x00225BC9 File Offset: 0x00223DC9
		// (set) Token: 0x06009A19 RID: 39449 RVA: 0x00225BD1 File Offset: 0x00223DD1
		[DefaultValue("rdCustom")]
		public virtual string AlternateCssClass
		{
			get
			{
				return this._alternateCssClass;
			}
			set
			{
				this._alternateCssClass = value;
			}
		}

		// Token: 0x170030C0 RID: 12480
		// (get) Token: 0x06009A1A RID: 39450 RVA: 0x00225BDA File Offset: 0x00223DDA
		// (set) Token: 0x06009A1B RID: 39451 RVA: 0x00225BE2 File Offset: 0x00223DE2
		[DefaultValue("Custom")]
		public virtual string AlternateText
		{
			get
			{
				return this._alternateText;
			}
			set
			{
				this._alternateText = value;
			}
		}

		// Token: 0x06009A1C RID: 39452 RVA: 0x00225BEB File Offset: 0x00223DEB
		protected override string GetCssClass()
		{
			if (this.State != DockToggleCommandState.Primary)
			{
				return this.AlternateCssClass;
			}
			return this.CssClass;
		}

		// Token: 0x06009A1D RID: 39453 RVA: 0x00225C03 File Offset: 0x00223E03
		protected override string GetText()
		{
			if (this.State != DockToggleCommandState.Primary)
			{
				return this.AlternateText;
			}
			return this.Text;
		}

		// Token: 0x04002BBA RID: 11194
		private string _alternateCssClass;

		// Token: 0x04002BBB RID: 11195
		private string _alternateText;

		// Token: 0x04002BBC RID: 11196
		private DockToggleCommandState _state = DockToggleCommandState.Primary;
	}
}
