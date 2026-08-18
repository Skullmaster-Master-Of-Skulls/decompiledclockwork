using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB1 RID: 2737
	internal sealed class Index : BaseBiffRecord, IRecord
	{
		// Token: 0x0600680C RID: 26636 RVA: 0x001855B8 File Offset: 0x001837B8
		public Index(uint firstRow, uint lastRow, uint[] dbCellOffsets) : base(523)
		{
			base.Length = 16;
			this.reserved1 = 0U;
			this.rwMic = firstRow;
			this.rwMac = lastRow;
			this.reserved2 = 0U;
			this.dbcellsOffsets = dbCellOffsets;
			if (this.dbcellsOffsets != null)
			{
				base.Length += (ushort)(4 * this.dbcellsOffsets.Length);
			}
		}

		// Token: 0x0600680D RID: 26637 RVA: 0x0018561C File Offset: 0x0018381C
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.reserved1);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.rwMic);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.rwMac);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.reserved2);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			if (this.dbcellsOffsets != null)
			{
				foreach (uint value in this.dbcellsOffsets)
				{
					bytes = BitConverter.GetBytes(value);
					bytes.CopyTo(data, num);
					num += bytes.Length;
				}
			}
			return data;
		}

		// Token: 0x0600680E RID: 26638 RVA: 0x001856D8 File Offset: 0x001838D8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[INDEX]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("reserved1={0};", this.reserved1);
			stringBuilder.AppendFormat("rwMic={0};", this.rwMic);
			stringBuilder.AppendFormat("rwMac={0};", this.rwMac);
			stringBuilder.AppendFormat("reserved2={0};", this.reserved2);
			stringBuilder.AppendFormat("dbcellsOffsets.Length={0};", this.dbcellsOffsets.Length);
			stringBuilder.Append("[/INDEX]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001B1E RID: 6942
		private const ushort type = 523;

		// Token: 0x04001B1F RID: 6943
		internal const ushort FixedPartLength = 16;

		// Token: 0x04001B20 RID: 6944
		private uint reserved1;

		// Token: 0x04001B21 RID: 6945
		private uint rwMic;

		// Token: 0x04001B22 RID: 6946
		private uint rwMac;

		// Token: 0x04001B23 RID: 6947
		private uint reserved2;

		// Token: 0x04001B24 RID: 6948
		private uint[] dbcellsOffsets;
	}
}
