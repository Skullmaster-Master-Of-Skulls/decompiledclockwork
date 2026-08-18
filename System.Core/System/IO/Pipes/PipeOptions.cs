using System;

namespace System.IO.Pipes
{
	// Token: 0x020000AD RID: 173
	[Flags]
	[Serializable]
	public enum PipeOptions
	{
		// Token: 0x04000547 RID: 1351
		None = 0,
		// Token: 0x04000548 RID: 1352
		WriteThrough = -2147483648,
		// Token: 0x04000549 RID: 1353
		Asynchronous = 1073741824
	}
}
