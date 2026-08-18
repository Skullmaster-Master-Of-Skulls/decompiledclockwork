using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020005AE RID: 1454
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadMapClientState
	{
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x000ACAAE File Offset: 0x000AACAE
		// (set) Token: 0x060033F6 RID: 13302 RVA: 0x000ACAB6 File Offset: 0x000AACB6
		public double CenterLatitude { get; set; }

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060033F7 RID: 13303 RVA: 0x000ACABF File Offset: 0x000AACBF
		// (set) Token: 0x060033F8 RID: 13304 RVA: 0x000ACAC7 File Offset: 0x000AACC7
		public double CenterLongitude { get; set; }

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000ACAD0 File Offset: 0x000AACD0
		// (set) Token: 0x060033FA RID: 13306 RVA: 0x000ACAD8 File Offset: 0x000AACD8
		public double Zoom { get; set; }
	}
}
