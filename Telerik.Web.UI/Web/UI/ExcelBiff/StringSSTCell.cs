using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE5 RID: 2789
	internal sealed class StringSSTCell : BiffCell
	{
		// Token: 0x060068DB RID: 26843 RVA: 0x0018947D File Offset: 0x0018767D
		public StringSSTCell(int sstIndex)
		{
			this.sstIndex = -1;
			base.XFIndex = 15;
			this.sstIndex = sstIndex;
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x060068DC RID: 26844 RVA: 0x0018949B File Offset: 0x0018769B
		// (set) Token: 0x060068DD RID: 26845 RVA: 0x001894A3 File Offset: 0x001876A3
		public int SSTIndex
		{
			get
			{
				return this.sstIndex;
			}
			set
			{
				this.sstIndex = value;
			}
		}

		// Token: 0x060068DE RID: 26846 RVA: 0x001894AC File Offset: 0x001876AC
		public override IRecord GetRecord(int row, int col)
		{
			return new LabelSST((ushort)row, (ushort)col, (ushort)base.XFIndex, (uint)this.sstIndex);
		}

		// Token: 0x04001C13 RID: 7187
		private int sstIndex;
	}
}
