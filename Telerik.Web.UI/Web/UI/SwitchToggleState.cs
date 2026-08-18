using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000019 RID: 25
	[ToolboxItem(false)]
	public class SwitchToggleState : StateManager
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00004194 File Offset: 0x00002394
		public SwitchToggleState()
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000419C File Offset: 0x0000239C
		public SwitchToggleState(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000041AB File Offset: 0x000023AB
		public SwitchToggleState(string text, string value) : this()
		{
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000041C1 File Offset: 0x000023C1
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000041E1 File Offset: 0x000023E1
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the text displayed in the RadButton control.")]
		public string Text
		{
			get
			{
				return ((string)base.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000041F4 File Offset: 0x000023F4
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004214 File Offset: 0x00002414
		[Description("Gets or sets optional Value.")]
		[Localizable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				return ((string)base.ViewState["Value"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}
	}
}
