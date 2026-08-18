using System;
using System.Collections.Specialized;
using System.Web;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001AC RID: 428
	public class SetMetaDataEventArgs : EventArgs
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00039E54 File Offset: 0x00038054
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00039E5C File Offset: 0x0003805C
		public NameValueCollection MetaData { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00039E65 File Offset: 0x00038065
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00039E6D File Offset: 0x0003806D
		public HttpContext Context { get; set; }
	}
}
