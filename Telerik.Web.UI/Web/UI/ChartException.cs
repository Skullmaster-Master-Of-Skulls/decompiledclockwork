using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001806 RID: 6150
	[Obsolete("This class is obsolete. Please use Telerik.Charting.ChartException")]
	public class ChartException : Exception
	{
		// Token: 0x0600EFC0 RID: 61376 RVA: 0x0036986E File Offset: 0x00367A6E
		public ChartException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600EFC1 RID: 61377 RVA: 0x00369878 File Offset: 0x00367A78
		public ChartException(string message) : base(message)
		{
		}

		// Token: 0x0600EFC2 RID: 61378 RVA: 0x00369881 File Offset: 0x00367A81
		public ChartException()
		{
		}
	}
}
