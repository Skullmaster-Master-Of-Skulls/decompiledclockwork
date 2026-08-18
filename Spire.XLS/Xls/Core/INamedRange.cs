using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000354 RID: 852
	public interface INamedRange : IExcelApplication
	{
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x060033BB RID: 13243
		int Index { get; }

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x060033BC RID: 13244
		// (set) Token: 0x060033BD RID: 13245
		string Name { get; set; }

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x060033BE RID: 13246
		// (set) Token: 0x060033BF RID: 13247
		string NameLocal { get; set; }

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x060033C0 RID: 13248
		// (set) Token: 0x060033C1 RID: 13249
		IXLSRange RefersToRange { get; set; }

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x060033C2 RID: 13250
		// (set) Token: 0x060033C3 RID: 13251
		string Value { get; set; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x060033C4 RID: 13252
		// (set) Token: 0x060033C5 RID: 13253
		bool Visible { get; set; }

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x060033C6 RID: 13254
		bool IsLocal { get; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x060033C7 RID: 13255
		string ValueR1C1 { get; }

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x060033C8 RID: 13256
		IWorksheet Worksheet { get; }

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x060033C9 RID: 13257
		string Scope { get; }

		// Token: 0x060033CA RID: 13258
		void Delete();
	}
}
