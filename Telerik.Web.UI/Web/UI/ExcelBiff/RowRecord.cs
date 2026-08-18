using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000ADC RID: 2780
	internal sealed class RowRecord : BaseBiffRecord, IRecord
	{
		// Token: 0x060068B6 RID: 26806 RVA: 0x0018855C File Offset: 0x0018675C
		public RowRecord(ushort rowNumber, ushort firstCol, ushort lastCol, ushort outlineLevel, bool collapseOutline, bool outlineRowHeight, bool autoSize) : base(520)
		{
			base.Length = 16;
			this.rw = rowNumber;
			this.colMic = firstCol;
			this.colMax = lastCol;
			this.miyRw = 255;
			this.irwMac = 0;
			this.reserved = 0;
			if (autoSize)
			{
				this.grbit = 256;
			}
			else
			{
				this.grbit = 320;
			}
			this.ixfe = 15;
			this.SetOutline(outlineLevel, collapseOutline, outlineRowHeight);
		}

		// Token: 0x060068B7 RID: 26807 RVA: 0x001885DC File Offset: 0x001867DC
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.rw);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colMic);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colMax);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.miyRw);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.irwMac);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.reserved);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ixfe);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x17002256 RID: 8790
		// (set) Token: 0x060068B8 RID: 26808 RVA: 0x001886BD File Offset: 0x001868BD
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public ushort RowHeight
		{
			set
			{
				this.miyRw = value * 20;
			}
		}

		// Token: 0x060068B9 RID: 26809 RVA: 0x001886CA File Offset: 0x001868CA
		private void SetOutline(ushort outlineLevel, bool collapseOutline, bool outlineRowHeight)
		{
			if (outlineLevel <= 7)
			{
				this.grbit |= outlineLevel;
				if (collapseOutline)
				{
					this.grbit |= 16;
				}
				if (outlineRowHeight)
				{
					this.grbit |= 32;
				}
			}
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00188708 File Offset: 0x00186908
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ROW]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("rw={0};", this.rw);
			stringBuilder.AppendFormat("colMic={0};", this.colMic);
			stringBuilder.AppendFormat("colMax={0};", this.colMax);
			stringBuilder.AppendFormat("miyRw={0};", this.miyRw);
			stringBuilder.AppendFormat("irwMac={0};", this.irwMac);
			stringBuilder.AppendFormat("reserved={0};", this.reserved);
			stringBuilder.AppendFormat("grbit=0x{0:x4};", this.grbit);
			stringBuilder.AppendFormat("ixfe=0x{0:x4};", this.ixfe);
			stringBuilder.Append("[/ROW]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BD5 RID: 7125
		private const ushort type = 520;

		// Token: 0x04001BD6 RID: 7126
		private const ushort length = 16;

		// Token: 0x04001BD7 RID: 7127
		private const ushort fCollapsed = 16;

		// Token: 0x04001BD8 RID: 7128
		private const ushort fZeroHeight = 32;

		// Token: 0x04001BD9 RID: 7129
		private ushort rw;

		// Token: 0x04001BDA RID: 7130
		private ushort colMic;

		// Token: 0x04001BDB RID: 7131
		private ushort colMax;

		// Token: 0x04001BDC RID: 7132
		private ushort miyRw;

		// Token: 0x04001BDD RID: 7133
		private ushort irwMac;

		// Token: 0x04001BDE RID: 7134
		private ushort reserved;

		// Token: 0x04001BDF RID: 7135
		private ushort grbit;

		// Token: 0x04001BE0 RID: 7136
		private ushort ixfe;
	}
}
