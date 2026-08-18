using System;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A79 RID: 2681
	internal sealed class ColInfo : BaseBiffRecord, IRecord
	{
		// Token: 0x06006750 RID: 26448 RVA: 0x0018238C File Offset: 0x0018058C
		public ColInfo(ushort firstCol, ushort lastCol, double colWidth, ushort outlineLevel, bool collapseOutline, bool hideOutline) : base(125)
		{
			base.Length = 12;
			this.colFirst = firstCol;
			this.colLast = lastCol;
			if (colWidth < 1.0)
			{
				this.coldx = (ushort)(colWidth * 12.0 * 36.5);
			}
			else
			{
				this.coldx = (ushort)((colWidth + 0.7128571423) * 256.0);
			}
			this.ixfe = 15;
			this.grbit = 0;
			this.reserved = 0;
			this.SetOutline(outlineLevel, collapseOutline, hideOutline);
		}

		// Token: 0x06006751 RID: 26449 RVA: 0x00182420 File Offset: 0x00180620
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.colFirst);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.colLast);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.coldx);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.ixfe);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.grbit);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.reserved);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x06006752 RID: 26450 RVA: 0x001824CD File Offset: 0x001806CD
		private void SetOutline(ushort outlineLevel, bool collapseOutline, bool hideOutline)
		{
			if (outlineLevel <= 7)
			{
				this.grbit = (ushort)((int)this.grbit | (int)outlineLevel << 8);
				if (collapseOutline)
				{
					this.grbit |= 4096;
				}
				if (hideOutline)
				{
					this.grbit |= 1;
				}
			}
		}

		// Token: 0x04001A05 RID: 6661
		private const ushort type = 125;

		// Token: 0x04001A06 RID: 6662
		private const ushort length = 12;

		// Token: 0x04001A07 RID: 6663
		private const ushort fCollapse = 1;

		// Token: 0x04001A08 RID: 6664
		private const double margins = 0.7128571423;

		// Token: 0x04001A09 RID: 6665
		private ushort colFirst;

		// Token: 0x04001A0A RID: 6666
		private ushort colLast;

		// Token: 0x04001A0B RID: 6667
		private ushort coldx;

		// Token: 0x04001A0C RID: 6668
		private ushort ixfe;

		// Token: 0x04001A0D RID: 6669
		private ushort grbit;

		// Token: 0x04001A0E RID: 6670
		private ushort reserved;
	}
}
