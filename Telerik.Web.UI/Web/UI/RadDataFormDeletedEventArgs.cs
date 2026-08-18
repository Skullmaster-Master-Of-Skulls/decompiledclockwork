using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001DD RID: 477
	public class RadDataFormDeletedEventArgs : RadDataFormDataChangeEventArgs
	{
		// Token: 0x06001105 RID: 4357 RVA: 0x0003E91D File Offset: 0x0003CB1D
		public RadDataFormDeletedEventArgs(int affectedRows, Exception e, RadDataFormDataItem item) : base(affectedRows, e, item)
		{
		}
	}
}
