using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001DC RID: 476
	public class RadDataFormDataChangeEventArgs : EventArgs
	{
		// Token: 0x060010FC RID: 4348 RVA: 0x0003E8B5 File Offset: 0x0003CAB5
		public RadDataFormDataChangeEventArgs(int affectedRows, Exception e, RadDataFormDataItem item)
		{
			this.AffectedRows = affectedRows;
			this.ExceptionHandled = false;
			this.Exception = e;
			this.Item = item;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0003E8D9 File Offset: 0x0003CAD9
		// (set) Token: 0x060010FE RID: 4350 RVA: 0x0003E8E1 File Offset: 0x0003CAE1
		public int AffectedRows { get; private set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0003E8EA File Offset: 0x0003CAEA
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x0003E8F2 File Offset: 0x0003CAF2
		public Exception Exception { get; private set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0003E8FB File Offset: 0x0003CAFB
		// (set) Token: 0x06001102 RID: 4354 RVA: 0x0003E903 File Offset: 0x0003CB03
		public RadDataFormDataItem Item { get; private set; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0003E90C File Offset: 0x0003CB0C
		// (set) Token: 0x06001104 RID: 4356 RVA: 0x0003E914 File Offset: 0x0003CB14
		public bool ExceptionHandled { get; set; }
	}
}
