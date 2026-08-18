using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AAA RID: 2730
	internal sealed class Guts : BaseBiffRecord, IRecord
	{
		// Token: 0x060067FA RID: 26618 RVA: 0x001850B1 File Offset: 0x001832B1
		public Guts(ushort maxOutlineRow, ushort maxOutlineCol) : base(128)
		{
			base.Length = 8;
			this.dxRwGut = 0;
			this.dyColGut = 0;
			this.iLevelRwMac = maxOutlineRow;
			this.iLevelColMac = maxOutlineCol;
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x001850E4 File Offset: 0x001832E4
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.dxRwGut);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.dyColGut);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iLevelRwMac);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iLevelColMac);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x00185160 File Offset: 0x00183360
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[GUTS]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("dxRwGut={0};", this.dxRwGut);
			stringBuilder.AppendFormat("dyColGut={0};", this.dyColGut);
			stringBuilder.AppendFormat("iLevelRwMac={0};", this.iLevelRwMac);
			stringBuilder.AppendFormat("iLevelColMac={0};", this.iLevelColMac);
			stringBuilder.Append("[/GUTS]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001AFF RID: 6911
		private const ushort type = 128;

		// Token: 0x04001B00 RID: 6912
		private const ushort length = 8;

		// Token: 0x04001B01 RID: 6913
		private ushort dxRwGut;

		// Token: 0x04001B02 RID: 6914
		private ushort dyColGut;

		// Token: 0x04001B03 RID: 6915
		private ushort iLevelRwMac;

		// Token: 0x04001B04 RID: 6916
		private ushort iLevelColMac;
	}
}
