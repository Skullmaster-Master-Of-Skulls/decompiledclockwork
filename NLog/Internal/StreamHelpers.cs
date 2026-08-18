using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x020000B1 RID: 177
	public static class StreamHelpers
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public static void CopyWithOffset(this Stream input, Stream output, int offset)
		{
			if (offset < 0)
			{
				throw new ArgumentException("negative offset");
			}
			input.Seek((long)offset, SeekOrigin.Current);
			byte[] array = new byte[4096];
			int count;
			while ((count = input.Read(array, 0, array.Length)) > 0)
			{
				output.Write(array, 0, count);
			}
		}
	}
}
