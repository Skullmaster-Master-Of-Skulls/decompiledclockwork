using System;
using System.Drawing;
using System.IO;
using System.Web;
using TechnoPro.Common.DAO.Impl.Adapters;

namespace TechnoPro.ClockWorkWeb.Adapters
{
	// Token: 0x02000195 RID: 405
	public static class HttpPostedFileBaseAdapter
	{
		// Token: 0x06000BD8 RID: 3032 RVA: 0x0004D2C8 File Offset: 0x0004B4C8
		public static Image SaveAsImage(this HttpPostedFileBase file)
		{
			Image result;
			try
			{
				bool flag = file == null || file.ContentLength <= 0;
				if (flag)
				{
					result = null;
				}
				else
				{
					MemoryStream memoryStream = file.InputStream as MemoryStream;
					bool flag2 = memoryStream == null;
					if (flag2)
					{
						memoryStream = new MemoryStream();
						file.InputStream.CopyTo(memoryStream);
					}
					result = memoryStream.ToArray().Deserialize();
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0004D340 File Offset: 0x0004B540
		public static byte[] SaveAsBytes(this HttpPostedFileBase file)
		{
			byte[] result;
			try
			{
				bool flag = file == null || file.ContentLength <= 0;
				if (flag)
				{
					result = null;
				}
				else
				{
					MemoryStream memoryStream = file.InputStream as MemoryStream;
					bool flag2 = memoryStream == null;
					if (flag2)
					{
						memoryStream = new MemoryStream();
						file.InputStream.CopyTo(memoryStream);
					}
					result = memoryStream.ToArray();
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0004D3B4 File Offset: 0x0004B5B4
		public static bool ValidateReceiptFormat(this HttpPostedFileBase file)
		{
			string text = file.ContentType.ToLower();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1776687947U)
			{
				if (num <= 1168299599U)
				{
					if (num != 944469275U)
					{
						if (num != 1168299599U)
						{
							goto IL_12C;
						}
						if (!(text == "image/x-tiff"))
						{
							goto IL_12C;
						}
					}
					else if (!(text == "image/pjpeg"))
					{
						goto IL_12C;
					}
				}
				else if (num != 1578133620U)
				{
					if (num != 1776687947U)
					{
						goto IL_12C;
					}
					if (!(text == "image/x-windows-bmp"))
					{
						goto IL_12C;
					}
				}
				else if (!(text == "image/tiff"))
				{
					goto IL_12C;
				}
			}
			else if (num <= 2953494330U)
			{
				if (num != 2899107204U)
				{
					if (num != 2953494330U)
					{
						goto IL_12C;
					}
					if (!(text == "image/png"))
					{
						goto IL_12C;
					}
				}
				else if (!(text == "application/pdf"))
				{
					goto IL_12C;
				}
			}
			else if (num != 3072015935U)
			{
				if (num != 3901389917U)
				{
					if (num != 4233377562U)
					{
						goto IL_12C;
					}
					if (!(text == "image/bmp"))
					{
						goto IL_12C;
					}
				}
				else if (!(text == "image/jpeg"))
				{
					goto IL_12C;
				}
			}
			else if (!(text == "image/gif"))
			{
				goto IL_12C;
			}
			return true;
			IL_12C:
			return false;
		}

		// Token: 0x040008E7 RID: 2279
		public static readonly string[] ReceiptSupportedFiles = new string[]
		{
			".gif",
			".jpg",
			".jpeg",
			".tiff",
			".png",
			".bmp",
			".pdf"
		};
	}
}
