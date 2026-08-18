using System;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x020005A7 RID: 1447
	[Serializable]
	internal class BitField
	{
		// Token: 0x060030BC RID: 12476 RVA: 0x000E3EA0 File Offset: 0x000E2EA0
		public BitField(int A_0)
		{
			this._mask = A_0;
			int num = 0;
			int num2 = A_0;
			if (num2 != 0)
			{
				while ((num2 & 1) == 0)
				{
					num++;
					num2 >>= 1;
				}
			}
			this._shift_count = num;
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000E3ED7 File Offset: 0x000E2ED7
		public BitField(uint A_0) : this((int)A_0)
		{
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000E3EE0 File Offset: 0x000E2EE0
		public int f(int A_0)
		{
			return A_0 & ~this._mask;
		}

		// Token: 0x060030BF RID: 12479 RVA: 0x000E3EEB File Offset: 0x000E2EEB
		public short d(short A_0)
		{
			return (short)this.f((int)A_0);
		}

		// Token: 0x060030C0 RID: 12480 RVA: 0x000E3EF5 File Offset: 0x000E2EF5
		public int b(int A_0)
		{
			return A_0 & this._mask;
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000E3EFF File Offset: 0x000E2EFF
		public short a(short A_0)
		{
			return (short)this.b((int)A_0);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000E3F09 File Offset: 0x000E2F09
		public short c(short A_0)
		{
			return (short)this.a((int)A_0);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x000E3F13 File Offset: 0x000E2F13
		public int a(int A_0)
		{
			return ak.a(this.b(A_0), this._shift_count);
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x000E3F27 File Offset: 0x000E2F27
		public bool e(int A_0)
		{
			return (A_0 & this._mask) == this._mask;
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000E3F39 File Offset: 0x000E2F39
		public bool d(int A_0)
		{
			return (A_0 & this._mask) != 0;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000E3F46 File Offset: 0x000E2F46
		public int c(int A_0)
		{
			return A_0 | this._mask;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000E3F50 File Offset: 0x000E2F50
		public int a(int A_0, bool A_1)
		{
			if (A_1)
			{
				return this.c(A_0);
			}
			return this.f(A_0);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000E3F64 File Offset: 0x000E2F64
		public short b(short A_0)
		{
			return (short)this.c((int)A_0);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000E3F6E File Offset: 0x000E2F6E
		public short a(short A_0, bool A_1)
		{
			if (A_1)
			{
				return this.b(A_0);
			}
			return this.d(A_0);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000E3F82 File Offset: 0x000E2F82
		public short a(short A_0, short A_1)
		{
			return (short)this.a((int)A_0, (int)A_1);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000E3F8D File Offset: 0x000E2F8D
		public int a(int A_0, int A_1)
		{
			return (A_0 & ~this._mask) | (A_1 << this._shift_count & this._mask);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000E3FAB File Offset: 0x000E2FAB
		public byte a(byte A_0, bool A_1)
		{
			if (A_1)
			{
				return this.b(A_0);
			}
			return this.a(A_0);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000E3FBF File Offset: 0x000E2FBF
		public byte a(byte A_0)
		{
			return (byte)this.f((int)A_0);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000E3FC9 File Offset: 0x000E2FC9
		public byte b(byte A_0)
		{
			return (byte)this.c((int)A_0);
		}

		// Token: 0x04002034 RID: 8244
		private int _mask;

		// Token: 0x04002035 RID: 8245
		private int _shift_count;
	}
}
