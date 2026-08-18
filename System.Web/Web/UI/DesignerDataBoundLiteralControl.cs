using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003D6 RID: 982
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DesignerDataBoundLiteralControl : Control
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000D473C File Offset: 0x000D373C
		public DesignerDataBoundLiteralControl()
		{
			base.PreventAutoID();
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000D474A File Offset: 0x000D374A
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x000D4752 File Offset: 0x000D3752
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

		// Token: 0x06002FDB RID: 12251 RVA: 0x000D4765 File Offset: 0x000D3765
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000D476D File Offset: 0x000D376D
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this._text = (string)savedState;
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000D477E File Offset: 0x000D377E
		protected internal override void Render(HtmlTextWriter output)
		{
			output.Write(this._text);
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000D478C File Offset: 0x000D378C
		protected override object SaveViewState()
		{
			return this._text;
		}

		// Token: 0x040021F7 RID: 8695
		private string _text;
	}
}
