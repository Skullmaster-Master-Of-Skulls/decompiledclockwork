using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007EB RID: 2027
	internal struct IntDecoder
	{
		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06004CAD RID: 19629 RVA: 0x00117C14 File Offset: 0x00115E14
		public int Value
		{
			get
			{
				if (!this.isValueDecoded)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.value;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x00117C3E File Offset: 0x00115E3E
		public bool IsValueDecoded
		{
			get
			{
				return this.isValueDecoded;
			}
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x00117C46 File Offset: 0x00115E46
		public void Reset()
		{
			this.index = 0;
			this.value = 0;
			this.isValueDecoded = false;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x00117C60 File Offset: 0x00115E60
		public int Decode(byte[] buffer, int offset, int size)
		{
			DecoderHelper.ValidateSize(size);
			if (this.isValueDecoded)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
			}
			int i = 0;
			while (i < size)
			{
				int num = (int)buffer[offset];
				this.value |= (num & 127) << (int)(this.index * 7);
				i++;
				if (this.index == 4 && (num & 248) != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("FramingSizeTooLarge")));
				}
				this.index += 1;
				if ((num & 128) == 0)
				{
					this.isValueDecoded = true;
					break;
				}
				offset++;
			}
			return i;
		}

		// Token: 0x04002FBD RID: 12221
		private int value;

		// Token: 0x04002FBE RID: 12222
		private short index;

		// Token: 0x04002FBF RID: 12223
		private bool isValueDecoded;

		// Token: 0x04002FC0 RID: 12224
		private const int LastIndex = 4;
	}
}
