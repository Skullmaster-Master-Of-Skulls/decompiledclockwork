using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020008D4 RID: 2260
	[ParseChildren(typeof(SpreadsheetToolbarGroup), ChildrenAsProperties = true, DefaultProperty = "Groups")]
	public class SpreadsheetToolbarTab : StateManager
	{
		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x06005516 RID: 21782 RVA: 0x0010422D File Offset: 0x0010242D
		[Description("Gets a collection of groups that will be shown in the Spreadsheet toolbar.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SpreadsheetToolbarGroupCollection Groups
		{
			get
			{
				if (this._groups == null)
				{
					this._groups = new SpreadsheetToolbarGroupCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._groups).TrackViewState();
					}
				}
				return this._groups;
			}
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x06005517 RID: 21783 RVA: 0x0010425B File Offset: 0x0010245B
		// (set) Token: 0x06005518 RID: 21784 RVA: 0x0010427B File Offset: 0x0010247B
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x040014EC RID: 5356
		private SpreadsheetToolbarGroupCollection _groups;
	}
}
