using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200053B RID: 1339
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumerator instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("496B0ABF-CDEE-11d3-88E8-00902754C43A")]
	internal interface UCOMIEnumerator
	{
		// Token: 0x06003349 RID: 13129
		bool MoveNext();

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600334A RID: 13130
		object Current { get; }

		// Token: 0x0600334B RID: 13131
		void Reset();
	}
}
