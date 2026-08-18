using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200019D RID: 413
	internal class ImportStructWorkItem
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x0007C13B File Offset: 0x0007A33B
		internal ImportStructWorkItem(StructModel model, StructMapping mapping)
		{
			this.model = model;
			this.mapping = mapping;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0007C151 File Offset: 0x0007A351
		internal StructModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0007C159 File Offset: 0x0007A359
		internal StructMapping Mapping
		{
			get
			{
				return this.mapping;
			}
		}

		// Token: 0x04000C14 RID: 3092
		private StructModel model;

		// Token: 0x04000C15 RID: 3093
		private StructMapping mapping;
	}
}
