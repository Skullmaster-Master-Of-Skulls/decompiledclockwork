using System;
using System.Windows.Forms;

namespace System.Web.UI.Design
{
	// Token: 0x0200005E RID: 94
	public interface IWebFormsBuilderUIService
	{
		// Token: 0x060002DF RID: 735
		string BuildColor(Control owner, string initialColor);

		// Token: 0x060002E0 RID: 736
		string BuildUrl(Control owner, string initialUrl, string baseUrl, string caption, string filter, UrlBuilderOptions options);
	}
}
