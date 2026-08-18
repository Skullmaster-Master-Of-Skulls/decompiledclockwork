using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AD1 RID: 2769
	internal sealed class Prot4RevPass : BaseBiffRecord, IRecord
	{
		// Token: 0x06006872 RID: 26738 RVA: 0x001874B1 File Offset: 0x001856B1
		public Prot4RevPass() : base(444)
		{
			base.Length = 2;
			this.wRevPass = 0;
		}

		// Token: 0x06006873 RID: 26739 RVA: 0x001874CC File Offset: 0x001856CC
		public byte[] GetData()
		{
			int index;
			byte[] data = base.GetData(out index);
			byte[] bytes = BitConverter.GetBytes(this.wRevPass);
			bytes.CopyTo(data, index);
			return data;
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x001874F8 File Offset: 0x001856F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PROT4REVPASS]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("wRevPass={0};", this.wRevPass);
			stringBuilder.Append("[/PROT4REVPASS]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001BAE RID: 7086
		private const ushort type = 444;

		// Token: 0x04001BAF RID: 7087
		private const ushort length = 2;

		// Token: 0x04001BB0 RID: 7088
		private ushort wRevPass;
	}
}
