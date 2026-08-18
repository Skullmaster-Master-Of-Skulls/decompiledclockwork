using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Drawing
{
	// Token: 0x02000022 RID: 34
	public class ImageConverter : TypeConverter
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00010140 File Offset: 0x0000E340
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == this.iconType || sourceType == typeof(byte[]) || (!(sourceType == typeof(InstanceDescriptor)) && base.CanConvertFrom(context, sourceType));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001018D File Offset: 0x0000E38D
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(byte[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000101AC File Offset: 0x0000E3AC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is Icon)
			{
				Icon icon = (Icon)value;
				return icon.ToBitmap();
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				Stream stream = this.GetBitmapStream(array);
				if (stream == null)
				{
					stream = new MemoryStream(array);
				}
				return Image.FromStream(stream);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00010200 File Offset: 0x0000E400
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value != null)
				{
					Image image = (Image)value;
					return image.ToString();
				}
				return SR.GetString("toStringNone");
			}
			else
			{
				if (!(destinationType == typeof(byte[])))
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (value == null)
				{
					return new byte[0];
				}
				bool flag = false;
				MemoryStream memoryStream = null;
				Image image2 = null;
				try
				{
					memoryStream = new MemoryStream();
					image2 = (Image)value;
					if (image2.RawFormat.Equals(ImageFormat.Icon))
					{
						flag = true;
						image2 = new Bitmap(image2, image2.Width, image2.Height);
					}
					image2.Save(memoryStream);
				}
				finally
				{
					if (memoryStream != null)
					{
						memoryStream.Close();
					}
					if (flag && image2 != null)
					{
						image2.Dispose();
					}
				}
				if (memoryStream != null)
				{
					return memoryStream.ToArray();
				}
				return null;
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000102F0 File Offset: 0x0000E4F0
		private unsafe Stream GetBitmapStream(byte[] rawData)
		{
			try
			{
				try
				{
					fixed (byte[] array = rawData)
					{
						byte* ptr;
						if (rawData == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						IntPtr intPtr = (IntPtr)((void*)ptr);
						if (intPtr == IntPtr.Zero)
						{
							return null;
						}
						if (rawData.Length <= sizeof(SafeNativeMethods.OBJECTHEADER) || Marshal.ReadInt16(intPtr) != 7189)
						{
							return null;
						}
						SafeNativeMethods.OBJECTHEADER objectheader = (SafeNativeMethods.OBJECTHEADER)Marshal.PtrToStructure(intPtr, typeof(SafeNativeMethods.OBJECTHEADER));
						if (rawData.Length <= (int)(objectheader.headersize + 18))
						{
							return null;
						}
						string @string = Encoding.ASCII.GetString(rawData, (int)(objectheader.headersize + 12), 6);
						if (@string != "PBrush")
						{
							return null;
						}
						byte[] bytes = Encoding.ASCII.GetBytes("BM");
						int num = (int)(objectheader.headersize + 18);
						while (num < (int)(objectheader.headersize + 510) && num + 1 < rawData.Length)
						{
							if (bytes[0] == ptr[num] && bytes[1] == ptr[num + 1])
							{
								return new MemoryStream(rawData, num, rawData.Length - num);
							}
							num++;
						}
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			catch (OutOfMemoryException)
			{
			}
			catch (ArgumentException)
			{
			}
			return null;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001046C File Offset: 0x0000E66C
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Image), attributes);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000848C File Offset: 0x0000668C
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04000197 RID: 407
		private Type iconType = typeof(Icon);
	}
}
