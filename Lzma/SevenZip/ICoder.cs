using System;
using System.IO;

namespace SevenZip
{
	// Token: 0x02000004 RID: 4
	public interface ICoder
	{
		// Token: 0x0600000E RID: 14
		void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress);
	}
}
