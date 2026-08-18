using System;
using System.IO;
using System.Text;

namespace Telerik.Web.Apoc.Image
{
	// Token: 0x020015D3 RID: 5587
	internal sealed class JpegParser : IDisposable
	{
		// Token: 0x0600D9ED RID: 55789 RVA: 0x002FC538 File Offset: 0x002FA738
		public JpegParser(byte[] data)
		{
			this.ms = new MemoryStream(data);
			this.headerInfo = new JpegInfo();
		}

		// Token: 0x0600D9EE RID: 55790 RVA: 0x002FC558 File Offset: 0x002FA758
		public JpegInfo Parse()
		{
			if (this.ReadFirstMarker() != 216)
			{
				throw new InvalidOperationException("Expected SOI marker first");
			}
			while (this.ms.Position < this.ms.Length)
			{
				int num = this.ReadNextMarker();
				int num2 = num;
				switch (num2)
				{
				case 192:
				case 193:
				case 194:
				case 195:
				case 197:
				case 198:
				case 199:
				case 201:
				case 202:
				case 203:
				case 205:
				case 206:
				case 207:
					this.ReadHeader();
					continue;
				case 196:
				case 200:
				case 204:
					break;
				default:
					if (num2 == 226)
					{
						this.ReadICCProfile();
						continue;
					}
					break;
				}
				this.SkipVariable();
			}
			if (this.iccProfileData != null)
			{
				this.headerInfo.SetICCProfile(this.iccProfileData.ToArray());
			}
			return this.headerInfo;
		}

		// Token: 0x0600D9EF RID: 55791 RVA: 0x002FC634 File Offset: 0x002FA834
		private void ReadICCProfile()
		{
			if (this.iccProfileData == null)
			{
				this.iccProfileData = new MemoryStream();
			}
			int num = this.ReadInt();
			string text = this.ReadString(12);
			if (!text.Contains("ICC_PROFILE\0") && !text.Contains("FPXR\0"))
			{
				this.iccProfileData = null;
				return;
			}
			this.ReadByte();
			this.ReadByte();
			byte[] array = new byte[num - 16];
			this.ms.Read(array, 0, array.Length);
			this.iccProfileData.Write(array, 0, array.Length);
		}

		// Token: 0x0600D9F0 RID: 55792 RVA: 0x002FC6C0 File Offset: 0x002FA8C0
		private void ReadHeader()
		{
			this.ReadInt();
			this.headerInfo.SetBitsPerSample((int)this.ReadByte());
			this.headerInfo.SetHeight(this.ReadInt());
			this.headerInfo.SetWidth(this.ReadInt());
			this.headerInfo.SetNumColourComponents((int)this.ReadByte());
		}

		// Token: 0x0600D9F1 RID: 55793 RVA: 0x002FC718 File Offset: 0x002FA918
		private int ReadInt()
		{
			return ((int)this.ReadByte() << 8) + (int)this.ReadByte();
		}

		// Token: 0x0600D9F2 RID: 55794 RVA: 0x002FC729 File Offset: 0x002FA929
		private byte ReadByte()
		{
			return (byte)this.ms.ReadByte();
		}

		// Token: 0x0600D9F3 RID: 55795 RVA: 0x002FC738 File Offset: 0x002FA938
		private string ReadString(int numBytes)
		{
			byte[] array = new byte[numBytes];
			this.ms.Read(array, 0, array.Length);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x0600D9F4 RID: 55796 RVA: 0x002FC768 File Offset: 0x002FA968
		private int ReadFirstMarker()
		{
			int num = this.ms.ReadByte();
			int num2 = this.ms.ReadByte();
			if (num != 255 || num2 != 216)
			{
				throw new InvalidOperationException("Not a JPEG file");
			}
			return num2;
		}

		// Token: 0x0600D9F5 RID: 55797 RVA: 0x002FC7AC File Offset: 0x002FA9AC
		private int ReadNextMarker()
		{
			int num = this.ms.ReadByte();
			while (num != 255 && num != -1)
			{
				num = this.ms.ReadByte();
			}
			do
			{
				num = this.ms.ReadByte();
			}
			while (num == 255 && num != -1);
			return num;
		}

		// Token: 0x0600D9F6 RID: 55798 RVA: 0x002FC7F8 File Offset: 0x002FA9F8
		private void SkipVariable()
		{
			int num = this.ReadInt();
			this.ms.Seek((long)(num - 2), SeekOrigin.Current);
		}

		// Token: 0x0600D9F7 RID: 55799 RVA: 0x002FC81D File Offset: 0x002FAA1D
		public void Dispose()
		{
			if (this.ms != null)
			{
				this.ms.Close();
			}
			if (this.iccProfileData != null)
			{
				this.iccProfileData.Close();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04003C50 RID: 15440
		public const int M_SOF0 = 192;

		// Token: 0x04003C51 RID: 15441
		public const int M_SOF1 = 193;

		// Token: 0x04003C52 RID: 15442
		public const int M_SOF2 = 194;

		// Token: 0x04003C53 RID: 15443
		public const int M_SOF3 = 195;

		// Token: 0x04003C54 RID: 15444
		public const int M_SOF5 = 197;

		// Token: 0x04003C55 RID: 15445
		public const int M_SOF6 = 198;

		// Token: 0x04003C56 RID: 15446
		public const int M_SOF7 = 199;

		// Token: 0x04003C57 RID: 15447
		public const int M_SOF9 = 201;

		// Token: 0x04003C58 RID: 15448
		public const int M_SOF10 = 202;

		// Token: 0x04003C59 RID: 15449
		public const int M_SOF11 = 203;

		// Token: 0x04003C5A RID: 15450
		public const int M_SOF13 = 205;

		// Token: 0x04003C5B RID: 15451
		public const int M_SOF14 = 206;

		// Token: 0x04003C5C RID: 15452
		public const int M_SOF15 = 207;

		// Token: 0x04003C5D RID: 15453
		public const int M_SOI = 216;

		// Token: 0x04003C5E RID: 15454
		public const int M_EOI = 217;

		// Token: 0x04003C5F RID: 15455
		public const int M_SOS = 218;

		// Token: 0x04003C60 RID: 15456
		public const int M_APP0 = 224;

		// Token: 0x04003C61 RID: 15457
		public const int M_APP1 = 225;

		// Token: 0x04003C62 RID: 15458
		public const int M_APP2 = 226;

		// Token: 0x04003C63 RID: 15459
		public const int M_APP3 = 227;

		// Token: 0x04003C64 RID: 15460
		public const int M_APP4 = 228;

		// Token: 0x04003C65 RID: 15461
		public const int M_APP5 = 229;

		// Token: 0x04003C66 RID: 15462
		public const int M_APP12 = 236;

		// Token: 0x04003C67 RID: 15463
		public const int M_COM = 254;

		// Token: 0x04003C68 RID: 15464
		public const string ICC_PROFILE = "ICC_PROFILE\0";

		// Token: 0x04003C69 RID: 15465
		public const string FPXR = "FPXR\0";

		// Token: 0x04003C6A RID: 15466
		private MemoryStream ms;

		// Token: 0x04003C6B RID: 15467
		private JpegInfo headerInfo;

		// Token: 0x04003C6C RID: 15468
		private MemoryStream iccProfileData;
	}
}
