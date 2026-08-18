using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AAF RID: 2735
	internal sealed class HorizontalPageBreaks : BaseBiffRecord, IRecord
	{
		// Token: 0x06006806 RID: 26630 RVA: 0x00185489 File Offset: 0x00183689
		public HorizontalPageBreaks(BRK[] pageBreaks) : base(27)
		{
			base.Length = (ushort)(2 + pageBreaks.Length * 6);
			this.cbrk = (ushort)pageBreaks.Length;
			this.rgbrk = pageBreaks;
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x001854B4 File Offset: 0x001836B4
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] array = BitConverter.GetBytes(this.cbrk);
			array.CopyTo(data, num);
			num += array.Length;
			if (this.rgbrk != null)
			{
				foreach (BRK brk in this.rgbrk)
				{
					array = brk.Data;
					array.CopyTo(data, num);
					num += array.Length;
				}
			}
			return data;
		}

		// Token: 0x04001B19 RID: 6937
		private const ushort type = 27;

		// Token: 0x04001B1A RID: 6938
		private const ushort fixedPartLength = 2;

		// Token: 0x04001B1B RID: 6939
		private ushort cbrk;

		// Token: 0x04001B1C RID: 6940
		private BRK[] rgbrk;
	}
}
