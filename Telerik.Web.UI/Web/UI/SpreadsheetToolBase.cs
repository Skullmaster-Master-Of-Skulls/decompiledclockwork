using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020008DA RID: 2266
	public abstract class SpreadsheetToolBase : StateManager
	{
		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x0600554D RID: 21837 RVA: 0x001060D4 File Offset: 0x001042D4
		// (set) Token: 0x0600554E RID: 21838 RVA: 0x001060F5 File Offset: 0x001042F5
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x0600554F RID: 21839 RVA: 0x0010610D File Offset: 0x0010430D
		// (set) Token: 0x06005550 RID: 21840 RVA: 0x0010612E File Offset: 0x0010432E
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool ShowLabel
		{
			get
			{
				return (bool)(base.ViewState["ShowLabel"] ?? true);
			}
			set
			{
				base.ViewState["ShowLabel"] = value;
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x06005551 RID: 21841 RVA: 0x00106146 File Offset: 0x00104346
		// (set) Token: 0x06005552 RID: 21842 RVA: 0x00106171 File Offset: 0x00104371
		[NotifyParentProperty(true)]
		[DefaultValue(SpreadsheetToolName.Empty)]
		public SpreadsheetToolName Name
		{
			get
			{
				if (base.ViewState["Name"] == null)
				{
					return SpreadsheetToolName.Empty;
				}
				return (SpreadsheetToolName)base.ViewState["Name"];
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}
	}
}
