using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000986 RID: 2438
	[ToolboxItem(false)]
	public class WindowWaiAriaSettings : WaiAriaSettings
	{
		// Token: 0x06005D21 RID: 23841 RVA: 0x0011C5C0 File Offset: 0x0011A7C0
		public WindowWaiAriaSettings() : base(new JavaScriptConverter[]
		{
			new WindowWaiAriaSettingsConverter()
		})
		{
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x06005D22 RID: 23842 RVA: 0x0011C5E3 File Offset: 0x0011A7E3
		// (set) Token: 0x06005D23 RID: 23843 RVA: 0x0011C603 File Offset: 0x0011A803
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the ID of the html element containing the label of the control.")]
		[Localizable(true)]
		public string LabelledBy
		{
			get
			{
				return ((string)base.ViewState["LabelledBy"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["LabelledBy"] = value;
			}
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x0011C616 File Offset: 0x0011A816
		public override bool IsDefault
		{
			get
			{
				return base.IsDefault && string.IsNullOrEmpty(this.LabelledBy);
			}
		}
	}
}
