using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019B8 RID: 6584
	internal class RadListViewDefaultLayoutTemplate : ITemplate
	{
		// Token: 0x0600FE8A RID: 65162 RVA: 0x00392770 File Offset: 0x00390970
		public RadListViewDefaultLayoutTemplate(string placeHolderId)
		{
			this._placeHolderId = placeHolderId;
		}

		// Token: 0x0600FE8B RID: 65163 RVA: 0x00392780 File Offset: 0x00390980
		public void InstantiateIn(Control container)
		{
			PlaceHolder placeHolder = new PlaceHolder();
			placeHolder.ID = HttpUtility.HtmlEncode(this._placeHolderId);
			container.Controls.Add(placeHolder);
		}

		// Token: 0x04004835 RID: 18485
		private string _placeHolderId;
	}
}
