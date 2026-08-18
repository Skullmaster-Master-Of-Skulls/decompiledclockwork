using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A6B RID: 2667
	internal sealed class BOF : BaseBiffRecord, IRecord
	{
		// Token: 0x060066F1 RID: 26353 RVA: 0x001816BB File Offset: 0x0017F8BB
		public BOF() : this(false)
		{
		}

		// Token: 0x060066F2 RID: 26354 RVA: 0x001816C4 File Offset: 0x0017F8C4
		public BOF(bool isWorksheet) : base(2057)
		{
			base.Length = 16;
			this.version = 1536;
			this.substreamType = (isWorksheet ? 16 : 5);
			this.rupBuild = 5612;
			this.rupYear = 1997;
			this.bfh = 49345U;
			this.sfo = 774U;
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x0018172C File Offset: 0x0017F92C
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.version);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.substreamType);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.rupBuild);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.rupYear);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.bfh);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.sfo);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x060066F4 RID: 26356 RVA: 0x001817DC File Offset: 0x0017F9DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BOF]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("version=0x{0:x4};", this.version);
			stringBuilder.AppendFormat("substreamType=0x{0:x4};", this.substreamType);
			stringBuilder.AppendFormat("rupBuild={0};", this.rupBuild);
			stringBuilder.AppendFormat("rupYear={0};", this.rupYear);
			stringBuilder.AppendFormat("bfh=0x{0:x4};", this.bfh);
			stringBuilder.AppendFormat("sfo=0x{0:x4};", this.sfo);
			stringBuilder.Append("[/BOF]");
			return stringBuilder.ToString();
		}

		// Token: 0x040019A4 RID: 6564
		private const ushort type = 2057;

		// Token: 0x040019A5 RID: 6565
		private const ushort length = 16;

		// Token: 0x040019A6 RID: 6566
		private const ushort Version = 1536;

		// Token: 0x040019A7 RID: 6567
		private const ushort WorkBookSubstream = 5;

		// Token: 0x040019A8 RID: 6568
		private const ushort WorkSheetSubstream = 16;

		// Token: 0x040019A9 RID: 6569
		private const ushort RupBuild = 5612;

		// Token: 0x040019AA RID: 6570
		private const ushort RupYear = 1997;

		// Token: 0x040019AB RID: 6571
		private const ushort Bfh = 49345;

		// Token: 0x040019AC RID: 6572
		private const ushort Sfo = 774;

		// Token: 0x040019AD RID: 6573
		private ushort version;

		// Token: 0x040019AE RID: 6574
		private ushort substreamType;

		// Token: 0x040019AF RID: 6575
		private ushort rupBuild;

		// Token: 0x040019B0 RID: 6576
		private ushort rupYear;

		// Token: 0x040019B1 RID: 6577
		private uint bfh;

		// Token: 0x040019B2 RID: 6578
		private uint sfo;
	}
}
