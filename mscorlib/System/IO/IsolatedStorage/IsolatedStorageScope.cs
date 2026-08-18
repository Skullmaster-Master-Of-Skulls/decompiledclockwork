using System;
using System.Runtime.InteropServices;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020007AA RID: 1962
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum IsolatedStorageScope
	{
		// Token: 0x040022A8 RID: 8872
		None = 0,
		// Token: 0x040022A9 RID: 8873
		User = 1,
		// Token: 0x040022AA RID: 8874
		Domain = 2,
		// Token: 0x040022AB RID: 8875
		Assembly = 4,
		// Token: 0x040022AC RID: 8876
		Roaming = 8,
		// Token: 0x040022AD RID: 8877
		Machine = 16,
		// Token: 0x040022AE RID: 8878
		Application = 32
	}
}
