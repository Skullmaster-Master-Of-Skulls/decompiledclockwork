using System;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A7C RID: 2684
	internal sealed class Country : BaseBiffRecord, IRecord
	{
		// Token: 0x0600675B RID: 26459 RVA: 0x00182597 File Offset: 0x00180797
		public Country() : base(140)
		{
			base.Length = 4;
			this.iCountryDef = 1;
			this.iCountryWinIni = 1;
		}

		// Token: 0x0600675C RID: 26460 RVA: 0x001825BC File Offset: 0x001807BC
		public byte[] GetData()
		{
			int num;
			byte[] data = base.GetData(out num);
			byte[] bytes = BitConverter.GetBytes(this.iCountryDef);
			bytes.CopyTo(data, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.iCountryWinIni);
			bytes.CopyTo(data, num);
			return data;
		}

		// Token: 0x0600675D RID: 26461 RVA: 0x00182604 File Offset: 0x00180804
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[COUNTRY]");
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendFormat("iCountryDef={0};", this.iCountryDef);
			stringBuilder.AppendFormat("iCountryWinIni={0};", this.iCountryWinIni);
			stringBuilder.Append("[/COUNTRY]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001A13 RID: 6675
		private const ushort type = 140;

		// Token: 0x04001A14 RID: 6676
		private const ushort length = 4;

		// Token: 0x04001A15 RID: 6677
		private ushort iCountryDef;

		// Token: 0x04001A16 RID: 6678
		private ushort iCountryWinIni;
	}
}
