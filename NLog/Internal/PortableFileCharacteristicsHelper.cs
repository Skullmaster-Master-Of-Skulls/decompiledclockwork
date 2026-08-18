using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x020000A6 RID: 166
	internal class PortableFileCharacteristicsHelper : FileCharacteristicsHelper
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0000B864 File Offset: 0x00009A64
		public override FileCharacteristics GetFileCharacteristics(string fileName, IntPtr fileHandle)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (fileInfo.Exists)
			{
				return new FileCharacteristics(fileInfo.CreationTimeUtc, fileInfo.LastWriteTimeUtc, fileInfo.Length);
			}
			return null;
		}
	}
}
