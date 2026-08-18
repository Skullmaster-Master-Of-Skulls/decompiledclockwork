using System;
using System.Collections;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200056D RID: 1389
	[Guid("496B0ABE-CDEE-11d3-88E8-00902754C43A")]
	internal interface IEnumerable
	{
		// Token: 0x060033D0 RID: 13264
		[DispId(-4)]
		IEnumerator GetEnumerator();
	}
}
