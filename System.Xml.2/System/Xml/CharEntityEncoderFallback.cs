using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200009B RID: 155
	internal class CharEntityEncoderFallback : EncoderFallback
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x00014289 File Offset: 0x00012489
		internal CharEntityEncoderFallback()
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00014291 File Offset: 0x00012491
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			if (this.fallbackBuffer == null)
			{
				this.fallbackBuffer = new CharEntityEncoderFallbackBuffer(this);
			}
			return this.fallbackBuffer;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x000142AD File Offset: 0x000124AD
		public override int MaxCharCount
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000142B1 File Offset: 0x000124B1
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x000142B9 File Offset: 0x000124B9
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
			set
			{
				this.startOffset = value;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000142C2 File Offset: 0x000124C2
		internal void Reset(int[] textContentMarks, int endMarkPos)
		{
			this.textContentMarks = textContentMarks;
			this.endMarkPos = endMarkPos;
			this.curMarkPos = 0;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000142DC File Offset: 0x000124DC
		internal bool CanReplaceAt(int index)
		{
			int num = this.curMarkPos;
			int num2 = this.startOffset + index;
			while (num < this.endMarkPos && num2 >= this.textContentMarks[num + 1])
			{
				num++;
			}
			this.curMarkPos = num;
			return (num & 1) != 0;
		}

		// Token: 0x0400024B RID: 587
		private CharEntityEncoderFallbackBuffer fallbackBuffer;

		// Token: 0x0400024C RID: 588
		private int[] textContentMarks;

		// Token: 0x0400024D RID: 589
		private int endMarkPos;

		// Token: 0x0400024E RID: 590
		private int curMarkPos;

		// Token: 0x0400024F RID: 591
		private int startOffset;
	}
}
