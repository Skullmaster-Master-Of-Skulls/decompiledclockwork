using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DF3 RID: 3571
	[Serializable]
	public class PivotGridException : Exception
	{
		// Token: 0x060084B0 RID: 33968 RVA: 0x001E4675 File Offset: 0x001E2875
		public PivotGridException()
		{
		}

		// Token: 0x060084B1 RID: 33969 RVA: 0x001E467D File Offset: 0x001E287D
		public PivotGridException(string message) : base(message)
		{
		}

		// Token: 0x060084B2 RID: 33970 RVA: 0x001E4686 File Offset: 0x001E2886
		public PivotGridException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
