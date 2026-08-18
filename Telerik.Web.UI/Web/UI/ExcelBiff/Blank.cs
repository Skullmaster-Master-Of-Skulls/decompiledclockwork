using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A69 RID: 2665
	internal sealed class Blank : BaseBiffRecord, IRecord
	{
		// Token: 0x060066ED RID: 26349 RVA: 0x00181600 File Offset: 0x0017F800
		public Blank(ushort row, ushort column, ushort xFIndex) : base(513)
		{
			base.Length = 6;
			this.rw = row;
			this.col = column;
			this.ixfe = xFIndex;
		}

		// Token: 0x060066EE RID: 26350 RVA: 0x0018162C File Offset: 0x0017F82C
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.rw);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.col);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ixfe);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x0400199F RID: 6559
		private const ushort type = 513;

		// Token: 0x040019A0 RID: 6560
		private const ushort length = 6;

		// Token: 0x040019A1 RID: 6561
		private ushort rw;

		// Token: 0x040019A2 RID: 6562
		private ushort col;

		// Token: 0x040019A3 RID: 6563
		private ushort ixfe;
	}
}
