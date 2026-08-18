using System;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x0200021D RID: 541
	internal sealed class SqlFedAuthInfo
	{
		// Token: 0x060021FE RID: 8702 RVA: 0x000EC670 File Offset: 0x000EBA70
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "STSURL: {0}, SPN: {1}", new object[]
			{
				this.stsurl ?? string.Empty,
				this.spn ?? string.Empty
			});
		}

		// Token: 0x04001456 RID: 5206
		internal string spn;

		// Token: 0x04001457 RID: 5207
		internal string stsurl;
	}
}
