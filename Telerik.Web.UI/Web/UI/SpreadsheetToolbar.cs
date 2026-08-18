using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020008D3 RID: 2259
	[ParseChildren(typeof(SpreadsheetToolbarTab), ChildrenAsProperties = true, DefaultProperty = "Tabs")]
	public class SpreadsheetToolbar : StateManager
	{
		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x06005514 RID: 21780 RVA: 0x001041F7 File Offset: 0x001023F7
		[Description("Gets a collection of groups that will be shown in the Spreadsheet toolbar.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SpreadsheetToolbarTabCollection Tabs
		{
			get
			{
				if (this._tabs == null)
				{
					this._tabs = new SpreadsheetToolbarTabCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tabs).TrackViewState();
					}
				}
				return this._tabs;
			}
		}

		// Token: 0x040014EB RID: 5355
		private SpreadsheetToolbarTabCollection _tabs;
	}
}
