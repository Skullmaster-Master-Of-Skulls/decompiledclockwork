using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000185 RID: 389
	[ToolboxData("<{0}:SeadragonOverlay runat=server></{0}:SeadragonOverlay>")]
	public abstract class SeadragonOverlay : Panel
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0001C660 File Offset: 0x0001A860
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0001C668 File Offset: 0x0001A868
		public virtual SeadragonOverlayPlacement Placement { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0001C671 File Offset: 0x0001A871
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}
	}
}
