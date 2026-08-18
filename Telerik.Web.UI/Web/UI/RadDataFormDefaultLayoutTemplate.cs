using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000209 RID: 521
	internal class RadDataFormDefaultLayoutTemplate : ITemplate
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x00044434 File Offset: 0x00042634
		public RadDataFormDefaultLayoutTemplate(string placeHolderId)
		{
			this._placeHolderId = placeHolderId;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00044444 File Offset: 0x00042644
		public void InstantiateIn(Control container)
		{
			PlaceHolder placeHolder = new PlaceHolder();
			placeHolder.ID = HttpUtility.HtmlEncode(this._placeHolderId);
			container.Controls.Add(placeHolder);
		}

		// Token: 0x04000568 RID: 1384
		private string _placeHolderId;
	}
}
