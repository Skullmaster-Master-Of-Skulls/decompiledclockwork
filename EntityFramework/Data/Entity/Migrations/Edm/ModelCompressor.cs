using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Edm
{
	// Token: 0x020006E7 RID: 1767
	internal class ModelCompressor
	{
		// Token: 0x06004702 RID: 18178 RVA: 0x0015040C File Offset: 0x0014E60C
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public virtual byte[] Compress(XDocument model)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
				{
					model.Save(gzipStream);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x0015046C File Offset: 0x0014E66C
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		public virtual XDocument Decompress(byte[] bytes)
		{
			XDocument result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					result = XDocument.Load(gzipStream);
				}
			}
			return result;
		}
	}
}
