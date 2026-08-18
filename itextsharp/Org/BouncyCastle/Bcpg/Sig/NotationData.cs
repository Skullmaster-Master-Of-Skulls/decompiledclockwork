using System;
using System.IO;
using System.Text;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x02000139 RID: 313
	public class NotationData : SignatureSubpacket
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x00040391 File Offset: 0x0003F391
		public NotationData(bool critical, byte[] data) : base(SignatureSubpacketTag.NotationData, critical, data)
		{
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0004039D File Offset: 0x0003F39D
		public NotationData(bool critical, bool humanReadable, string notationName, string notationValue) : base(SignatureSubpacketTag.NotationData, critical, NotationData.createData(humanReadable, notationName, notationValue))
		{
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000403B4 File Offset: 0x0003F3B4
		private static byte[] createData(bool humanReadable, string notationName, string notationValue)
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.WriteByte(humanReadable ? 128 : 0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			memoryStream.WriteByte(0);
			byte[] bytes = Encoding.UTF8.GetBytes(notationName);
			int num = Math.Min(bytes.Length, 255);
			byte[] bytes2 = Encoding.UTF8.GetBytes(notationValue);
			int num2 = Math.Min(bytes2.Length, 255);
			memoryStream.WriteByte((byte)(num >> 8));
			memoryStream.WriteByte((byte)num);
			memoryStream.WriteByte((byte)(num2 >> 8));
			memoryStream.WriteByte((byte)num2);
			memoryStream.Write(bytes, 0, num);
			memoryStream.Write(bytes2, 0, num2);
			return memoryStream.ToArray();
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00040463 File Offset: 0x0003F463
		public bool IsHumanReadable
		{
			get
			{
				return this.data[0] == 128;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00040474 File Offset: 0x0003F474
		public string GetNotationName()
		{
			int count = ((int)this.data[4] << 8) + (int)this.data[5];
			int index = 8;
			return Encoding.UTF8.GetString(this.data, index, count);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000404AC File Offset: 0x0003F4AC
		public string GetNotationValue()
		{
			int num = ((int)this.data[4] << 8) + (int)this.data[5];
			int count = ((int)this.data[6] << 8) + (int)this.data[7];
			int index = 8 + num;
			return Encoding.UTF8.GetString(this.data, index, count);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000404F8 File Offset: 0x0003F4F8
		public byte[] GetNotationValueBytes()
		{
			int num = ((int)this.data[4] << 8) + (int)this.data[5];
			int num2 = ((int)this.data[6] << 8) + (int)this.data[7];
			int sourceIndex = 8 + num;
			byte[] array = new byte[num2];
			Array.Copy(this.data, sourceIndex, array, 0, num2);
			return array;
		}

		// Token: 0x040008FB RID: 2299
		public const int HeaderFlagLength = 4;

		// Token: 0x040008FC RID: 2300
		public const int HeaderNameLength = 2;

		// Token: 0x040008FD RID: 2301
		public const int HeaderValueLength = 2;
	}
}
