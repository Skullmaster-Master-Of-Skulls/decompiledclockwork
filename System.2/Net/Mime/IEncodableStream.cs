using System;
using System.IO;

namespace System.Net.Mime
{
	// Token: 0x02000246 RID: 582
	internal interface IEncodableStream
	{
		// Token: 0x0600161C RID: 5660
		int DecodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x0600161D RID: 5661
		int EncodeBytes(byte[] buffer, int offset, int count);

		// Token: 0x0600161E RID: 5662
		string GetEncodedString();

		// Token: 0x0600161F RID: 5663
		Stream GetStream();
	}
}
