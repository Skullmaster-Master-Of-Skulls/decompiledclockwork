using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.XmlSerialization
{
	// Token: 0x020005EC RID: 1516
	public class FileVersion
	{
		// Token: 0x060059D9 RID: 23001 RVA: 0x00385A08 File Offset: 0x00384A08
		public FileVersion()
		{
			int a_ = 16;
			this.ApplicationName = RecordTableEnumerator.b("㹅⑇", a_);
			this.BuildVersion = RecordTableEnumerator.b("牅絇穉穋", a_);
			this.LastEdited = RecordTableEnumerator.b("牅", a_);
			this.LowestEdited = RecordTableEnumerator.b("牅", a_);
			base..ctor();
		}

		// Token: 0x04002C01 RID: 11265
		private float \u2593\u00A9\u00AE\u00B0;

		// Token: 0x04002C02 RID: 11266
		public string ApplicationName;

		// Token: 0x04002C03 RID: 11267
		public string BuildVersion;

		// Token: 0x04002C04 RID: 11268
		private string \u2460\u009B\u0080\u0081;

		// Token: 0x04002C05 RID: 11269
		public string LastEdited;

		// Token: 0x04002C06 RID: 11270
		private bool[] \u25D9\u00A5\u0095\u0091;

		// Token: 0x04002C07 RID: 11271
		public string LowestEdited;

		// Token: 0x04002C08 RID: 11272
		private long \u25D8\u009C\u00B0\u0093;

		// Token: 0x04002C09 RID: 11273
		public string CodeName;
	}
}
