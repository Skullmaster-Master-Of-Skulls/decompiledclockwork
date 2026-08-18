using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200109C RID: 4252
	[Serializable]
	public class GridException : Exception
	{
		// Token: 0x0600ACC9 RID: 44233 RVA: 0x00252192 File Offset: 0x00250392
		public GridException()
		{
		}

		// Token: 0x0600ACCA RID: 44234 RVA: 0x0025219A File Offset: 0x0025039A
		public GridException(string Message) : base(Message)
		{
		}

		// Token: 0x0600ACCB RID: 44235 RVA: 0x002521A3 File Offset: 0x002503A3
		public GridException(string Message, Exception Inner) : base(Message, Inner)
		{
		}
	}
}
