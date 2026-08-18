using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020008D5 RID: 2261
	[ParseChildren(typeof(SpreadsheetTool), ChildrenAsProperties = true, DefaultProperty = "Tools")]
	public class SpreadsheetToolbarGroup : StateManager
	{
		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x00104296 File Offset: 0x00102496
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets a collection of tools that will be shown in the spreadsheet toolbar.")]
		public SpreadsheetToolCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new SpreadsheetToolCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x040014ED RID: 5357
		private SpreadsheetToolCollection _tools;
	}
}
