using System;

namespace System.Xml
{
	// Token: 0x020000AC RID: 172
	internal interface IDtdParserAdapterWithValidation : IDtdParserAdapter
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000609 RID: 1545
		bool DtdValidation { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600060A RID: 1546
		IValidationEventHandling ValidationEventHandling { get; }
	}
}
