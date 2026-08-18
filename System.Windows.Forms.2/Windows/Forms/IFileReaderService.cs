using System;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000290 RID: 656
	public interface IFileReaderService
	{
		// Token: 0x060029AC RID: 10668
		Stream OpenFileFromSource(string relativePath);
	}
}
