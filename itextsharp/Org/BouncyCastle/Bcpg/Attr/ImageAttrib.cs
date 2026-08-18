using System;
using System.IO;

namespace Org.BouncyCastle.Bcpg.Attr
{
	// Token: 0x020001A1 RID: 417
	public class ImageAttrib : UserAttributeSubpacket
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x0005CD78 File Offset: 0x0005BD78
		public ImageAttrib(byte[] data) : base(UserAttributeSubpacketTag.ImageAttribute, data)
		{
			this.hdrLength = ((int)(data[1] & byte.MaxValue) << 8 | (int)(data[0] & byte.MaxValue));
			this._version = (int)(data[2] & byte.MaxValue);
			this._encoding = (int)(data[3] & byte.MaxValue);
			this.imageData = new byte[data.Length - this.hdrLength];
			Array.Copy(data, this.hdrLength, this.imageData, 0, this.imageData.Length);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0005CDF6 File Offset: 0x0005BDF6
		public ImageAttrib(ImageAttrib.Format imageType, byte[] imageData) : this(ImageAttrib.ToByteArray(imageType, imageData))
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0005CE08 File Offset: 0x0005BE08
		private static byte[] ToByteArray(ImageAttrib.Format imageType, byte[] imageData)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(16);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(1);
			memoryStream.WriteByte((byte)imageType);
			memoryStream.Write(ImageAttrib.Zeroes, 0, ImageAttrib.Zeroes.Length);
			memoryStream.Write(imageData, 0, imageData.Length);
			return memoryStream.ToArray();
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0005CE5C File Offset: 0x0005BE5C
		public int Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0005CE64 File Offset: 0x0005BE64
		public int Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005CE6C File Offset: 0x0005BE6C
		public byte[] GetImageData()
		{
			return this.imageData;
		}

		// Token: 0x04000B98 RID: 2968
		private static readonly byte[] Zeroes = new byte[12];

		// Token: 0x04000B99 RID: 2969
		private int hdrLength;

		// Token: 0x04000B9A RID: 2970
		private int _version;

		// Token: 0x04000B9B RID: 2971
		private int _encoding;

		// Token: 0x04000B9C RID: 2972
		private byte[] imageData;

		// Token: 0x020001A2 RID: 418
		public enum Format : byte
		{
			// Token: 0x04000B9E RID: 2974
			Jpeg = 1
		}
	}
}
