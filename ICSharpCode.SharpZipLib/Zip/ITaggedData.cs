using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000073 RID: 115
	public interface ITaggedData
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000487 RID: 1159
		short TagID { get; }

		// Token: 0x06000488 RID: 1160
		void SetData(byte[] data, int offset, int count);

		// Token: 0x06000489 RID: 1161
		byte[] GetData();
	}
}
