using System;

namespace ICSharpCode.SharpZipLib.Checksums
{
	// Token: 0x02000005 RID: 5
	public interface IChecksum
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16
		long Value { get; }

		// Token: 0x06000011 RID: 17
		void Reset();

		// Token: 0x06000012 RID: 18
		void Update(int value);

		// Token: 0x06000013 RID: 19
		void Update(byte[] buffer);

		// Token: 0x06000014 RID: 20
		void Update(byte[] buffer, int offset, int count);
	}
}
