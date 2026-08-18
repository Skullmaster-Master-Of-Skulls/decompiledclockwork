using System;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007FB RID: 2043
	internal abstract class EncodedFramingRecord
	{
		// Token: 0x06004D05 RID: 19717 RVA: 0x001198E0 File Offset: 0x00117AE0
		protected EncodedFramingRecord(byte[] encodedBytes)
		{
			this.encodedBytes = encodedBytes;
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x001198F0 File Offset: 0x00117AF0
		internal EncodedFramingRecord(FramingRecordType recordType, string value)
		{
			int byteCount = Encoding.UTF8.GetByteCount(value);
			int encodedSize = IntEncoder.GetEncodedSize(byteCount);
			this.encodedBytes = DiagnosticUtility.Utility.AllocateByteArray(checked(1 + encodedSize + byteCount));
			this.encodedBytes[0] = (byte)recordType;
			int num = 1;
			num += IntEncoder.Encode(byteCount, this.encodedBytes, num);
			Encoding.UTF8.GetBytes(value, 0, value.Length, this.encodedBytes, num);
			this.SetEncodedBytes(this.encodedBytes);
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x0011996D File Offset: 0x00117B6D
		public byte[] EncodedBytes
		{
			get
			{
				return this.encodedBytes;
			}
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x00119975 File Offset: 0x00117B75
		protected void SetEncodedBytes(byte[] encodedBytes)
		{
			this.encodedBytes = encodedBytes;
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0011997E File Offset: 0x00117B7E
		public override int GetHashCode()
		{
			return (int)this.encodedBytes[0] << 16 | (int)this.encodedBytes[this.encodedBytes.Length / 2] << 8 | (int)this.encodedBytes[this.encodedBytes.Length - 1];
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x001199B1 File Offset: 0x00117BB1
		public override bool Equals(object o)
		{
			return o is EncodedFramingRecord && this.Equals((EncodedFramingRecord)o);
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x001199CC File Offset: 0x00117BCC
		public bool Equals(EncodedFramingRecord other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			byte[] array = other.encodedBytes;
			if (this.encodedBytes.Length != array.Length)
			{
				return false;
			}
			for (int i = 0; i < this.encodedBytes.Length; i++)
			{
				if (this.encodedBytes[i] != array[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04002FFC RID: 12284
		private byte[] encodedBytes;
	}
}
