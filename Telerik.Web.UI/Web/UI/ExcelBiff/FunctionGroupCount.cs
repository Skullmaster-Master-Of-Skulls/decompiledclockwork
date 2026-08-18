using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA8 RID: 2728
	internal sealed class FunctionGroupCount : BaseBiffRecord, IRecord
	{
		// Token: 0x060067F4 RID: 26612 RVA: 0x00184F75 File Offset: 0x00183175
		public FunctionGroupCount() : base(156)
		{
			base.Length = 2;
			this.fnGroup = 14;
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x00184F94 File Offset: 0x00183194
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.fnGroup);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x00184FC0 File Offset: 0x001831C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FNGROUPCOUNT]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("fnGroup={0};", this.fnGroup);
			stringBuilder.Append("[/FNGROUPCOUNT]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001AF9 RID: 6905
		private const ushort type = 156;

		// Token: 0x04001AFA RID: 6906
		private const ushort length = 2;

		// Token: 0x04001AFB RID: 6907
		private ushort fnGroup;
	}
}
