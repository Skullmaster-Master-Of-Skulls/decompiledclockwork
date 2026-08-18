using System;
using System.Collections;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200053A RID: 1338
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface UCOMIEnumerable
	{
		// Token: 0x06003348 RID: 13128
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
