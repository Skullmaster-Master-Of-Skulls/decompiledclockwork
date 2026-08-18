using System;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x02000274 RID: 628
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	public sealed class DesignerDataBoundLiteralControl : Control
	{
		// Token: 0x06001DD8 RID: 7640 RVA: 0x00060BC9 File Offset: 0x0005EDC9
		public DesignerDataBoundLiteralControl()
		{
			base.PreventAutoID();
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x00060BD7 File Offset: 0x0005EDD7
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x00060BDF File Offset: 0x0005EDDF
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = ((value != null) ? value : string.Empty);
			}
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x00060BF2 File Offset: 0x0005EDF2
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this._text = (string)savedState;
			}
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00060C03 File Offset: 0x0005EE03
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this._text);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00060BD7 File Offset: 0x0005EDD7
		protected override object SaveViewState()
		{
			return this._text;
		}

		// Token: 0x0400196E RID: 6510
		private string _text;
	}
}
