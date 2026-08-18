using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE9 RID: 2793
	internal sealed class TabID : BaseBiffRecord, IRecord
	{
		// Token: 0x060068ED RID: 26861 RVA: 0x00189A4C File Offset: 0x00187C4C
		public TabID() : base(317)
		{
			base.Length = 2;
			this.rgiTab = new ushort[]
			{
				1
			};
		}

		// Token: 0x060068EE RID: 26862 RVA: 0x00189A80 File Offset: 0x00187C80
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.rgiTab[0]);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x00189AB0 File Offset: 0x00187CB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[TABID]");
			stringBuilder.Append(base.ToString());
			for (int i = 0; i < this.rgiTab.Length; i++)
			{
				stringBuilder.AppendFormat("rgiTab[{0}]={1};", i, this.rgiTab[i]);
			}
			stringBuilder.Append("[/TABID]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C1D RID: 7197
		private const ushort type = 317;

		// Token: 0x04001C1E RID: 7198
		private const ushort length = 2;

		// Token: 0x04001C1F RID: 7199
		private ushort[] rgiTab;
	}
}
