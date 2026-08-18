using System;
using System.IO;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000CE RID: 206
	public class PdfString : PdfObject
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00025DB1 File Offset: 0x00024DB1
		public PdfString() : base(3)
		{
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00025DD0 File Offset: 0x00024DD0
		public PdfString(string value) : base(3)
		{
			this.value = value;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00025DF6 File Offset: 0x00024DF6
		public PdfString(string value, string encoding) : base(3)
		{
			this.value = value;
			this.encoding = encoding;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00025E23 File Offset: 0x00024E23
		public PdfString(byte[] bytes) : base(3)
		{
			this.value = PdfEncodings.ConvertToString(bytes, null);
			this.encoding = "";
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00025E5C File Offset: 0x00024E5C
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			byte[] array = this.GetBytes();
			PdfEncryption pdfEncryption = null;
			if (writer != null)
			{
				pdfEncryption = writer.Encryption;
			}
			if (pdfEncryption != null && !pdfEncryption.IsEmbeddedFilesOnly())
			{
				array = pdfEncryption.EncryptByteArray(array);
			}
			if (this.hexWriting)
			{
				ByteBuffer byteBuffer = new ByteBuffer();
				byteBuffer.Append('<');
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					byteBuffer.AppendHex(array[i]);
				}
				byteBuffer.Append('>');
				os.Write(byteBuffer.ToByteArray(), 0, byteBuffer.Size);
				return;
			}
			array = PdfContentByte.EscapeString(array);
			os.Write(array, 0, array.Length);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00025EF4 File Offset: 0x00024EF4
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00025EFC File Offset: 0x00024EFC
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00025F04 File Offset: 0x00024F04
		public string ToUnicodeString()
		{
			if (this.encoding != null && this.encoding.Length != 0)
			{
				return this.value;
			}
			this.GetBytes();
			if (this.bytes.Length >= 2 && this.bytes[0] == 254 && this.bytes[1] == 255)
			{
				return PdfEncodings.ConvertToString(this.bytes, "UnicodeBig");
			}
			return PdfEncodings.ConvertToString(this.bytes, "PDF");
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00025F7E File Offset: 0x00024F7E
		internal void SetObjNum(int objNum, int objGen)
		{
			this.objNum = objNum;
			this.objGen = objGen;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00025F90 File Offset: 0x00024F90
		internal void Decrypt(PdfReader reader)
		{
			PdfEncryption decrypt = reader.Decrypt;
			if (decrypt != null)
			{
				this.originalValue = this.value;
				decrypt.SetHashKey(this.objNum, this.objGen);
				this.bytes = PdfEncodings.ConvertToBytes(this.value, null);
				this.bytes = decrypt.DecryptByteArray(this.bytes);
				this.value = PdfEncodings.ConvertToString(this.bytes, null);
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00025FFC File Offset: 0x00024FFC
		public override byte[] GetBytes()
		{
			if (this.bytes == null)
			{
				if (this.encoding != null && this.encoding.Equals("UnicodeBig") && PdfEncodings.IsPdfDocEncoding(this.value))
				{
					this.bytes = PdfEncodings.ConvertToBytes(this.value, "PDF");
				}
				else
				{
					this.bytes = PdfEncodings.ConvertToBytes(this.value, this.encoding);
				}
			}
			return this.bytes;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0002606D File Offset: 0x0002506D
		public byte[] GetOriginalBytes()
		{
			if (this.originalValue == null)
			{
				return this.GetBytes();
			}
			return PdfEncodings.ConvertToBytes(this.originalValue, null);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0002608A File Offset: 0x0002508A
		public PdfString SetHexWriting(bool hexWriting)
		{
			this.hexWriting = hexWriting;
			return this;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00026094 File Offset: 0x00025094
		public bool IsHexWriting()
		{
			return this.hexWriting;
		}

		// Token: 0x04000616 RID: 1558
		protected string value = "";

		// Token: 0x04000617 RID: 1559
		protected string originalValue;

		// Token: 0x04000618 RID: 1560
		protected string encoding = "PDF";

		// Token: 0x04000619 RID: 1561
		protected int objNum;

		// Token: 0x0400061A RID: 1562
		protected int objGen;

		// Token: 0x0400061B RID: 1563
		protected bool hexWriting;
	}
}
