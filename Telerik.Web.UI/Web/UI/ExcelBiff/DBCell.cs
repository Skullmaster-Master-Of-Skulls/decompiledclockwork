using System;
using System.Collections.Generic;
using System.IO;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A7E RID: 2686
	internal sealed class DBCell : BaseBiffRecord, IRecord
	{
		// Token: 0x06006762 RID: 26466 RVA: 0x00182714 File Offset: 0x00180914
		public DBCell(uint dbRtrw, List<ushort> preallocatedEmptyOffset) : base(215)
		{
			base.Length = 4;
			this.dbRtrw = dbRtrw;
			this.rgdb = preallocatedEmptyOffset;
			if (this.rgdb != null)
			{
				int num = this.rgdb.Count;
				if (num > 4110)
				{
					num = 4110;
				}
				base.Length = (ushort)((int)base.Length + 2 * num);
			}
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x00182774 File Offset: 0x00180974
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.dbRtrw);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			if (this.rgdb != null)
			{
				int num2 = 4110;
				foreach (ushort value in this.rgdb)
				{
					if (num2 == 0)
					{
						break;
					}
					bytes = BitConverter.GetBytes(value);
					bytes.CopyTo(data, num);
					num += bytes.Length;
					num2--;
				}
				if (this.rgdb.Count > 4110)
				{
					this.rgdb.RemoveRange(0, 4110);
					return data;
				}
				this.rgdb = null;
			}
			return data;
		}

		// Token: 0x06006764 RID: 26468 RVA: 0x00182840 File Offset: 0x00180A40
		private void WriteContinueRecord(Stream stream, int rgdbItems)
		{
			stream.Write(new Continue
			{
				Length = (ushort)(2 * rgdbItems)
			}.GetBaseData(), 0, 4);
			for (int i = 0; i < rgdbItems; i++)
			{
				byte[] bytes = BitConverter.GetBytes(this.rgdb[i]);
				stream.Write(bytes, 0, bytes.Length);
			}
			this.rgdb.RemoveRange(0, rgdbItems);
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x001828A4 File Offset: 0x00180AA4
		public void WriteToStream(Stream stream)
		{
			byte[] data = this.GetData();
			stream.Write(data, 0, data.Length);
			if (this.rgdb != null)
			{
				int num = this.rgdb.Count / 4110;
				for (int i = 0; i < num; i++)
				{
					this.WriteContinueRecord(stream, 4110);
				}
				if (this.rgdb.Count > 0)
				{
					this.WriteContinueRecord(stream, this.rgdb.Count);
				}
			}
		}

		// Token: 0x04001A19 RID: 6681
		private const ushort type = 215;

		// Token: 0x04001A1A RID: 6682
		private const ushort length = 4;

		// Token: 0x04001A1B RID: 6683
		private const ushort maxRgdbItems = 4110;

		// Token: 0x04001A1C RID: 6684
		private uint dbRtrw;

		// Token: 0x04001A1D RID: 6685
		private List<ushort> rgdb;
	}
}
