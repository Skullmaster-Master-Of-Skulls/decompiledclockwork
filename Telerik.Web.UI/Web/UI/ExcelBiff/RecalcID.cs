using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD5 RID: 2773
	internal sealed class RecalcID : BaseBiffRecord, IRecord
	{
		// Token: 0x0600689F RID: 26783 RVA: 0x00188212 File Offset: 0x00186412
		public RecalcID() : base(449)
		{
			base.Length = 8;
			this.rt = 449;
			this.reserved = 0;
			this.dwBuild = 80000U;
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x00188244 File Offset: 0x00186444
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.rt);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.reserved);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.dwBuild);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x001882A4 File Offset: 0x001864A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[RECALCID]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("rt=0x{0:x4};", this.rt);
			stringBuilder.AppendFormat("reserved=0x{0:x4};", this.reserved);
			stringBuilder.AppendFormat("reserved=0x{0:x8};", this.dwBuild);
			stringBuilder.Append("[/RECALCID]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BBD RID: 7101
		private const ushort type = 449;

		// Token: 0x04001BBE RID: 7102
		private const ushort length = 8;

		// Token: 0x04001BBF RID: 7103
		private ushort rt;

		// Token: 0x04001BC0 RID: 7104
		private ushort reserved;

		// Token: 0x04001BC1 RID: 7105
		private uint dwBuild;
	}
}
