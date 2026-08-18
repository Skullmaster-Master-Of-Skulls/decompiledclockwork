using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ABF RID: 2751
	internal class MulBlankCell
	{
		// Token: 0x06006838 RID: 26680 RVA: 0x00186417 File Offset: 0x00184617
		public MulBlankCell(ushort row, ushort col, BlankCell blankCell)
		{
			this.row = row;
			this.firstCol = col;
			this.lastCol = col;
			this.blankCells = new List<BlankCell>();
			this.blankCells.Add(blankCell);
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x0018644B File Offset: 0x0018464B
		public void Add(BlankCell blankCell)
		{
			this.lastCol += 1;
			this.blankCells.Add(blankCell);
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x00186468 File Offset: 0x00184668
		public void Write(Stream stream)
		{
			if (stream != null)
			{
				IRecord record;
				if (this.blankCells.Count == 1)
				{
					BiffCell biffCell = this.blankCells[0];
					record = biffCell.GetRecord((int)this.row, (int)this.firstCol);
				}
				else
				{
					record = new MulBlank(this.row, this.firstCol, this.lastCol, this.blankCells);
				}
				byte[] data = record.GetData();
				stream.Write(data, 0, data.Length);
			}
		}

		// Token: 0x04001B5D RID: 7005
		private List<BlankCell> blankCells;

		// Token: 0x04001B5E RID: 7006
		private ushort firstCol;

		// Token: 0x04001B5F RID: 7007
		private ushort lastCol;

		// Token: 0x04001B60 RID: 7008
		private ushort row;
	}
}
