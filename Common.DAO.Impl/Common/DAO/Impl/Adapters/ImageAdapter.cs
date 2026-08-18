using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000181 RID: 385
	public static class ImageAdapter
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x0007919C File Offset: 0x0007739C
		public static Image Deserialize(this byte[] binaryData)
		{
			Image result;
			try
			{
				MemoryStream stream = new MemoryStream(binaryData);
				result = Image.FromStream(stream);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000791D4 File Offset: 0x000773D4
		public static byte[] Serialize(this Image img)
		{
			bool flag = img == null;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				MemoryStream memoryStream = new MemoryStream();
				img.Save(memoryStream, ImageFormat.Jpeg);
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
