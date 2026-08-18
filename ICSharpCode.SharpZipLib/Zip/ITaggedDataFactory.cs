using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000078 RID: 120
	internal interface ITaggedDataFactory
	{
		// Token: 0x060004A9 RID: 1193
		ITaggedData Create(short tag, byte[] data, int offset, int count);
	}
}
