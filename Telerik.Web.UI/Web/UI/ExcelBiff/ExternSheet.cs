using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AA2 RID: 2722
	internal sealed class ExternSheet : BaseBiffRecord, IRecord
	{
		// Token: 0x060067D7 RID: 26583 RVA: 0x00184764 File Offset: 0x00182964
		public ExternSheet() : base(23)
		{
			this.rgXTI = null;
		}

		// Token: 0x060067D8 RID: 26584 RVA: 0x00184778 File Offset: 0x00182978
		public int AddXTI(ushort supBookIndex, ushort firstTab, ushort lastTab)
		{
			if (this.rgXTI == null)
			{
				this.rgXTI = new List<ExternSheet.XTI>();
				this.rgXTI.Add(new ExternSheet.XTI(supBookIndex, firstTab, lastTab));
				return 0;
			}
			if (this.rgXTI[this.rgXTI.Count - 1].Tab != firstTab)
			{
				this.rgXTI.Add(new ExternSheet.XTI(supBookIndex, firstTab, lastTab));
			}
			return this.rgXTI.Count - 1;
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x001847F0 File Offset: 0x001829F0
		public byte[] GetData()
		{
			ushort num = (ushort)this.rgXTI.Count;
			base.Length = 2 + num * 6;
			int num2;
			byte[] data = base.GetData(out num2);
			byte[] bytes = BitConverter.GetBytes(num);
			bytes.CopyTo(data, num2);
			num2 += bytes.Length;
			foreach (ExternSheet.XTI xti in this.rgXTI)
			{
				xti.XtiInfo.CopyTo(data, num2);
				num2 += 6;
			}
			return data;
		}

		// Token: 0x04001ACB RID: 6859
		private const ushort type = 23;

		// Token: 0x04001ACC RID: 6860
		private const ushort fixedPartLength = 2;

		// Token: 0x04001ACD RID: 6861
		private List<ExternSheet.XTI> rgXTI;

		// Token: 0x02000AA3 RID: 2723
		private class XTI
		{
			// Token: 0x17002226 RID: 8742
			// (get) Token: 0x060067DA RID: 26586 RVA: 0x00184888 File Offset: 0x00182A88
			public ushort Tab
			{
				get
				{
					return this.tab;
				}
			}

			// Token: 0x17002227 RID: 8743
			// (get) Token: 0x060067DB RID: 26587 RVA: 0x00184890 File Offset: 0x00182A90
			public byte[] XtiInfo
			{
				get
				{
					return this.xtiInfo;
				}
			}

			// Token: 0x060067DC RID: 26588 RVA: 0x00184898 File Offset: 0x00182A98
			public XTI(ushort supBookIndex, ushort firstTab, ushort lastTab)
			{
				this.tab = firstTab;
				this.xtiInfo = new byte[6];
				int num = 0;
				byte[] bytes = BitConverter.GetBytes(supBookIndex);
				bytes.CopyTo(this.xtiInfo, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(firstTab);
				bytes.CopyTo(this.xtiInfo, num);
				num += bytes.Length;
				bytes = BitConverter.GetBytes(lastTab);
				bytes.CopyTo(this.xtiInfo, num);
			}

			// Token: 0x04001ACE RID: 6862
			public const ushort Length = 6;

			// Token: 0x04001ACF RID: 6863
			private ushort tab;

			// Token: 0x04001AD0 RID: 6864
			private byte[] xtiInfo;
		}
	}
}
