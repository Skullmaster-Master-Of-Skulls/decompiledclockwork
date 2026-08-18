using System;

namespace Telerik.Web.UI
{
	// Token: 0x020010F4 RID: 4340
	[Serializable]
	public class NullEnumerableException : GridException
	{
		// Token: 0x0600B1D3 RID: 45523 RVA: 0x00269B45 File Offset: 0x00267D45
		public NullEnumerableException() : base("Cannot perform this operation when DataSource is not assigned")
		{
		}
	}
}
