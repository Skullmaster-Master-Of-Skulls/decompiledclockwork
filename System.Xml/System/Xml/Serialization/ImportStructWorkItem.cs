using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000317 RID: 791
	internal class ImportStructWorkItem
	{
		// Token: 0x06002581 RID: 9601 RVA: 0x000B3498 File Offset: 0x000B2498
		internal ImportStructWorkItem(StructModel model, StructMapping mapping)
		{
			this.model = model;
			this.mapping = mapping;
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x000B34AE File Offset: 0x000B24AE
		internal StructModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x000B34B6 File Offset: 0x000B24B6
		internal StructMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x040015A3 RID: 5539
		private StructModel model;

		// Token: 0x040015A4 RID: 5540
		private StructMapping mapping;
	}
}
