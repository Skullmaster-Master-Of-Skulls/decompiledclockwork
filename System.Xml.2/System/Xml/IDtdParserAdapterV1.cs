using System;

namespace System.Xml
{
	// Token: 0x020000AD RID: 173
	internal interface IDtdParserAdapterV1 : IDtdParserAdapterWithValidation, IDtdParserAdapter
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600060B RID: 1547
		bool V1CompatibilityMode { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600060C RID: 1548
		bool Normalization { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600060D RID: 1549
		bool Namespaces { get; }
	}
}
