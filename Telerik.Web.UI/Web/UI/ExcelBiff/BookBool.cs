using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A6C RID: 2668
	internal sealed class BookBool : BaseBiffRecord, IRecord
	{
		// Token: 0x060066F5 RID: 26357 RVA: 0x001818A4 File Offset: 0x0017FAA4
		public BookBool() : base(218)
		{
			base.Length = 2;
			this.fNoSaveSupp = 0;
		}

		// Token: 0x060066F6 RID: 26358 RVA: 0x001818C0 File Offset: 0x0017FAC0
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fNoSaveSupp);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060066F7 RID: 26359 RVA: 0x001818EC File Offset: 0x0017FAEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BOOKBOOL]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fNoSaveSupp={0};", this.fNoSaveSupp);
			stringBuilder.Append("[/BOOKBOOL]");
			return stringBuilder.ToString();
		}

		// Token: 0x040019B3 RID: 6579
		private const ushort type = 218;

		// Token: 0x040019B4 RID: 6580
		private const ushort length = 2;

		// Token: 0x040019B5 RID: 6581
		private ushort fNoSaveSupp;
	}
}
