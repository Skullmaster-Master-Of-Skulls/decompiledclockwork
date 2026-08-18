using System;
using System.IO;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x020009A9 RID: 2473
	public static class StreamExtensions
	{
		// Token: 0x06005EE3 RID: 24291 RVA: 0x00121B68 File Offset: 0x0011FD68
		public static void CopyTo(Stream source, Stream dest)
		{
			byte[] array = new byte[4096];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				dest.Write(array, 0, count);
			}
		}

		// Token: 0x040016D3 RID: 5843
		private const int BufferSize = 4096;
	}
}
