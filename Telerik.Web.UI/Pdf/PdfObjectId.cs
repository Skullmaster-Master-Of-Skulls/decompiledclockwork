using System;

namespace Telerik.Pdf
{
	// Token: 0x02001667 RID: 5735
	public struct PdfObjectId
	{
		// Token: 0x0600DE07 RID: 56839 RVA: 0x003086A9 File Offset: 0x003068A9
		public PdfObjectId(int objectNumber)
		{
			this.objectNumber = objectNumber;
			this.generationNumber = 0;
		}

		// Token: 0x0600DE08 RID: 56840 RVA: 0x003086B9 File Offset: 0x003068B9
		public PdfObjectId(int objectNumber, int generationNumber)
		{
			this.objectNumber = objectNumber;
			this.generationNumber = generationNumber;
		}

		// Token: 0x170043EF RID: 17391
		// (get) Token: 0x0600DE09 RID: 56841 RVA: 0x003086C9 File Offset: 0x003068C9
		public int ObjectNumber
		{
			get
			{
				return this.objectNumber;
			}
		}

		// Token: 0x170043F0 RID: 17392
		// (get) Token: 0x0600DE0A RID: 56842 RVA: 0x003086D1 File Offset: 0x003068D1
		public int GenerationNumber
		{
			get
			{
				return this.generationNumber;
			}
		}

		// Token: 0x04003FC8 RID: 16328
		private int objectNumber;

		// Token: 0x04003FC9 RID: 16329
		private int generationNumber;
	}
}
