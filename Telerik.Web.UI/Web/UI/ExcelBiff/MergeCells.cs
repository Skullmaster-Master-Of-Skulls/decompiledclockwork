using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ABA RID: 2746
	internal sealed class MergeCells : BaseBiffRecord, IRecord
	{
		// Token: 0x06006829 RID: 26665 RVA: 0x00185C87 File Offset: 0x00183E87
		public MergeCells(Ref[] refs) : base(229)
		{
			this.cmcs = (ushort)refs.Length;
			this.rgRef = refs;
			base.Length = 2 + this.cmcs * 8;
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x00185CB8 File Offset: 0x00183EB8
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.cmcs);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			foreach (Ref @ref in this.rgRef)
			{
				bytes = BitConverter.GetBytes(@ref.rwFirst);
				bytes.CopyTo(data, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(@ref.rwLast);
				bytes.CopyTo(data, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(@ref.colFirst);
				bytes.CopyTo(data, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(@ref.colLast);
				bytes.CopyTo(data, num);
				num += bytes.Length;
			}
			return data;
		}

		// Token: 0x04001B48 RID: 6984
		private const ushort type = 229;

		// Token: 0x04001B49 RID: 6985
		private const ushort fixedPartLength = 2;

		// Token: 0x04001B4A RID: 6986
		public const ushort MaxRecordCount = 1027;

		// Token: 0x04001B4B RID: 6987
		private ushort cmcs;

		// Token: 0x04001B4C RID: 6988
		private Ref[] rgRef;
	}
}
