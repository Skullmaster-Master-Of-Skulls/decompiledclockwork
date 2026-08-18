using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000081 RID: 129
	internal class XmlCachedStream : MemoryStream
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x00012478 File Offset: 0x00010678
		internal XmlCachedStream(Uri uri, Stream stream)
		{
			this.uri = uri;
			try
			{
				byte[] buffer = new byte[4096];
				int count;
				while ((count = stream.Read(buffer, 0, 4096)) > 0)
				{
					this.Write(buffer, 0, count);
				}
				base.Position = 0L;
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x040001F2 RID: 498
		private const int MoveBufferSize = 4096;

		// Token: 0x040001F3 RID: 499
		private Uri uri;
	}
}
