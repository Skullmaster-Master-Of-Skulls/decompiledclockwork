using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ACA RID: 2762
	internal sealed class Pane : BaseBiffRecord, IRecord
	{
		// Token: 0x0600685D RID: 26717 RVA: 0x00186BC6 File Offset: 0x00184DC6
		public Pane(ushort verticalSplitPos, ushort horizontalSplitPos, ushort topRowVisible, ushort leftColumnVisible, ushort activePaneNumber) : base(65)
		{
			base.Length = 10;
			this.verticalSplit = verticalSplitPos;
			this.horizontalSplit = horizontalSplitPos;
			this.rwTop = topRowVisible;
			this.colLeft = leftColumnVisible;
			this.pnnAct = activePaneNumber;
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x00186C00 File Offset: 0x00184E00
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.verticalSplit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.horizontalSplit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.rwTop);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colLeft);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.pnnAct);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x00186C94 File Offset: 0x00184E94
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PANE]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("verticalSplit={0};", this.verticalSplit);
			stringBuilder.AppendFormat("horizontalSplit={0};", this.horizontalSplit);
			stringBuilder.AppendFormat("rwTop={0};", this.rwTop);
			stringBuilder.AppendFormat("colLeft={0};", this.colLeft);
			stringBuilder.AppendFormat("pnnAct={0};", this.pnnAct);
			stringBuilder.Append("[/PANE]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B92 RID: 7058
		private const ushort type = 65;

		// Token: 0x04001B93 RID: 7059
		private const ushort length = 10;

		// Token: 0x04001B94 RID: 7060
		private ushort verticalSplit;

		// Token: 0x04001B95 RID: 7061
		private ushort horizontalSplit;

		// Token: 0x04001B96 RID: 7062
		private ushort rwTop;

		// Token: 0x04001B97 RID: 7063
		private ushort colLeft;

		// Token: 0x04001B98 RID: 7064
		private ushort pnnAct;
	}
}
