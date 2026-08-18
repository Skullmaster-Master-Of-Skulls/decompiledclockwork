using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Telerik.Web.UI
{
	// Token: 0x02000A0A RID: 2570
	public static class BinaryImageFormatHelper
	{
		// Token: 0x06006185 RID: 24965 RVA: 0x0016FEA8 File Offset: 0x0016E0A8
		public static byte[] CreateByteFromImage(Image image, ImageFormat imageFormat)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (imageFormat == null)
			{
				throw new ArgumentNullException("imageFormat");
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.Save(memoryStream, imageFormat);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x0016FF04 File Offset: 0x0016E104
		public static Image CreateImgFromBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			Image result;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				try
				{
					result = Image.FromStream(memoryStream);
				}
				catch (ArgumentException innerException)
				{
					throw new ArgumentException("The provided binary data may not be valid image or may contains unknown header", innerException);
				}
			}
			return result;
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x0016FF68 File Offset: 0x0016E168
		public static string GetImageMimeType(byte[] image)
		{
			if (BinaryImageFormatHelper.IsJpeg(image))
			{
				return "image/jpeg";
			}
			if (BinaryImageFormatHelper.IsPng(image))
			{
				return "image/png";
			}
			if (BinaryImageFormatHelper.IsGif(image))
			{
				return "image/gif";
			}
			if (BinaryImageFormatHelper.IsBmp(image))
			{
				return "image/bmp";
			}
			if (BinaryImageFormatHelper.IsTiff(image))
			{
				return "image/tiff";
			}
			return "image/*";
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x0016FFC0 File Offset: 0x0016E1C0
		public static ImageFormat GetImageFormat(byte[] image)
		{
			if (BinaryImageFormatHelper.IsJpeg(image))
			{
				return ImageFormat.Jpeg;
			}
			if (BinaryImageFormatHelper.IsPng(image))
			{
				return ImageFormat.Png;
			}
			if (BinaryImageFormatHelper.IsGif(image))
			{
				return ImageFormat.Gif;
			}
			if (BinaryImageFormatHelper.IsBmp(image))
			{
				return ImageFormat.Bmp;
			}
			if (BinaryImageFormatHelper.IsTiff(image))
			{
				return ImageFormat.Tiff;
			}
			return ImageFormat.Gif;
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x00170018 File Offset: 0x0016E218
		public static bool IsTiff(byte[] image)
		{
			return BinaryImageFormatHelper.IsMaskMatch(image, 0, new byte[]
			{
				77,
				77
			}) || BinaryImageFormatHelper.IsMaskMatch(image, 0, new byte[]
			{
				73,
				73
			});
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x00170063 File Offset: 0x0016E263
		public static bool IsPng(byte[] image)
		{
			return BinaryImageFormatHelper.IsMaskMatch(image, 1, new byte[]
			{
				80,
				78,
				71
			});
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x00170084 File Offset: 0x0016E284
		public static bool IsGif(byte[] image)
		{
			return BinaryImageFormatHelper.IsMaskMatch(image, 0, new byte[]
			{
				71,
				73,
				70,
				56
			});
		}

		// Token: 0x0600618C RID: 24972 RVA: 0x001700A0 File Offset: 0x0016E2A0
		public static bool IsJpeg(byte[] image)
		{
			return BinaryImageFormatHelper.IsMaskMatch(image, 0, new byte[]
			{
				byte.MaxValue,
				216
			});
		}

		// Token: 0x0600618D RID: 24973 RVA: 0x001700CC File Offset: 0x0016E2CC
		public static bool IsBmp(byte[] image)
		{
			return BinaryImageFormatHelper.IsMaskMatch(image, 0, new byte[]
			{
				66,
				77
			});
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x001700F4 File Offset: 0x0016E2F4
		public static byte[] RemoveNonHeaderBytes(byte[] image)
		{
			if (image == null || image.Length == 0)
			{
				return image;
			}
			int num = BinaryImageFormatHelper.GetHeaderOffset(image);
			if (num == 0)
			{
				return image;
			}
			num = Math.Min(image.Length, num);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(image, num, image.Length - num))
			{
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x00170154 File Offset: 0x0016E354
		public static int GetHeaderOffset(byte[] bytes)
		{
			if (bytes != null)
			{
				int num = Math.Min(200, bytes.Length);
				for (int i = 0; i <= num; i++)
				{
					if (BinaryImageFormatHelper.IsSupportedImageType(bytes, i))
					{
						return i;
					}
				}
			}
			return 0;
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x0017018C File Offset: 0x0016E38C
		private static bool IsSupportedImageType(byte[] bytes, int offset)
		{
			byte[] array;
			if (offset > 0)
			{
				array = new byte[Math.Max(0, bytes.Length - offset)];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = bytes[i + offset];
				}
			}
			else
			{
				array = bytes;
			}
			return BinaryImageFormatHelper.IsJpeg(array) || BinaryImageFormatHelper.IsPng(array) || BinaryImageFormatHelper.IsGif(array) || BinaryImageFormatHelper.IsBmp(array) || BinaryImageFormatHelper.IsTiff(array);
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x001701F4 File Offset: 0x0016E3F4
		private static bool IsMaskMatch(byte[] bytes, int offset, params byte[] mask)
		{
			if (bytes == null || bytes.Length < mask.Length)
			{
				return false;
			}
			for (int i = 0; i < mask.Length; i++)
			{
				if (bytes[offset + i] != mask[i])
				{
					return false;
				}
			}
			return true;
		}
	}
}
