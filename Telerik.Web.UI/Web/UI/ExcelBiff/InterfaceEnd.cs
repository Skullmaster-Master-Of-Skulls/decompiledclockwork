using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB3 RID: 2739
	internal sealed class InterfaceEnd : BaseBiffRecord, IRecord
	{
		// Token: 0x0600680F RID: 26639 RVA: 0x0018578B File Offset: 0x0018398B
		public InterfaceEnd() : base(226)
		{
			base.Length = 0;
		}

		// Token: 0x06006810 RID: 26640 RVA: 0x001857A0 File Offset: 0x001839A0
		public byte[] GetData()
		{
			int num;
			return base.GetData(out num);
		}

		// Token: 0x06006811 RID: 26641 RVA: 0x001857B8 File Offset: 0x001839B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[INTERFACEEND]");
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("[/INTERFACEEND]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B28 RID: 6952
		private const ushort type = 226;

		// Token: 0x04001B29 RID: 6953
		private const ushort length = 0;
	}
}
