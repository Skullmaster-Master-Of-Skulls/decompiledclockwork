using System;

namespace System.Net
{
	// Token: 0x020004C7 RID: 1223
	internal struct BufferChunkBytes : IReadChunkBytes
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x0009641C File Offset: 0x0009541C
		// (set) Token: 0x060025BF RID: 9663 RVA: 0x00096459 File Offset: 0x00095459
		public int NextByte
		{
			get
			{
				if (this.Count != 0)
				{
					this.Count--;
					return (int)this.Buffer[this.Offset++];
				}
				return -1;
			}
			set
			{
				this.Count++;
				this.Offset--;
				this.Buffer[this.Offset] = (byte)value;
			}
		}

		// Token: 0x0400257F RID: 9599
		public byte[] Buffer;

		// Token: 0x04002580 RID: 9600
		public int Offset;

		// Token: 0x04002581 RID: 9601
		public int Count;
	}
}
