using System;

namespace Telerik.Pdf
{
	// Token: 0x02001650 RID: 5712
	public class PdfDate
	{
		// Token: 0x0600DD85 RID: 56709 RVA: 0x00306B50 File Offset: 0x00304D50
		public static string Format(DateTime dt)
		{
			return dt.ToString("'D:'yyyyMMddHHmmss'Z'");
		}
	}
}
