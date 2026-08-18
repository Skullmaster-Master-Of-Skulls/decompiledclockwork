using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x0200160A RID: 5642
	public class UnsupportedFilterException : Exception
	{
		// Token: 0x0600DBBB RID: 56251 RVA: 0x00300ACF File Offset: 0x002FECCF
		public UnsupportedFilterException(string filterName) : base(string.Format("The {0} filter is not supported.", filterName))
		{
		}
	}
}
