using System;

namespace System.Data
{
	// Token: 0x020000BF RID: 191
	public interface IDbDataParameter : IDataParameter
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000C99 RID: 3225
		// (set) Token: 0x06000C9A RID: 3226
		byte Precision { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C9B RID: 3227
		// (set) Token: 0x06000C9C RID: 3228
		byte Scale { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000C9D RID: 3229
		// (set) Token: 0x06000C9E RID: 3230
		int Size { get; set; }
	}
}
