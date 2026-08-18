using System;

namespace System.Xml
{
	// Token: 0x02000069 RID: 105
	internal class BitStack
	{
		// Token: 0x0600039D RID: 925 RVA: 0x0000E75D File Offset: 0x0000C95D
		public BitStack()
		{
			this.curr = 1U;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000E76C File Offset: 0x0000C96C
		public void PushBit(bool bit)
		{
			if ((this.curr & 2147483648U) != 0U)
			{
				this.PushCurr();
			}
			this.curr = (this.curr << 1 | (bit ? 1U : 0U));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000E798 File Offset: 0x0000C998
		public bool PopBit()
		{
			bool result = (this.curr & 1U) > 0U;
			this.curr >>= 1;
			if (this.curr == 1U)
			{
				this.PopCurr();
			}
			return result;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000E7CF File Offset: 0x0000C9CF
		public bool PeekBit()
		{
			return (this.curr & 1U) > 0U;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public bool IsEmpty
		{
			get
			{
				return this.curr == 1U;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		private void PushCurr()
		{
			if (this.bitStack == null)
			{
				this.bitStack = new uint[16];
			}
			uint[] array = this.bitStack;
			int num = this.stackPos;
			this.stackPos = num + 1;
			array[num] = this.curr;
			this.curr = 1U;
			int num2 = this.bitStack.Length;
			if (this.stackPos >= num2)
			{
				uint[] destinationArray = new uint[2 * num2];
				Array.Copy(this.bitStack, destinationArray, num2);
				this.bitStack = destinationArray;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000E860 File Offset: 0x0000CA60
		private void PopCurr()
		{
			if (this.stackPos > 0)
			{
				uint[] array = this.bitStack;
				int num = this.stackPos - 1;
				this.stackPos = num;
				this.curr = array[num];
			}
		}

		// Token: 0x040001AE RID: 430
		private uint[] bitStack;

		// Token: 0x040001AF RID: 431
		private int stackPos;

		// Token: 0x040001B0 RID: 432
		private uint curr;
	}
}
