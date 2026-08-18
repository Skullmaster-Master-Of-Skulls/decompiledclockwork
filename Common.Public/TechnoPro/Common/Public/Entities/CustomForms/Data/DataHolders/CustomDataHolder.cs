using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders
{
	// Token: 0x0200042B RID: 1067
	public class CustomDataHolder : BusinessBase<Guid>
	{
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00024910 File Offset: 0x00022B10
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid DataInstanceId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00024928 File Offset: 0x00022B28
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x00024930 File Offset: 0x00022B30
		public eCustomDataPrimitiveType DataType { get; set; }

		// Token: 0x06002061 RID: 8289 RVA: 0x0000EDF5 File Offset: 0x0000CFF5
		protected CustomDataHolder()
		{
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00024939 File Offset: 0x00022B39
		protected CustomDataHolder(CustomDataHolder dataObj)
		{
			this.DataType = dataObj.DataType;
			this.DataInstanceId = dataObj.DataInstanceId;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0002495D File Offset: 0x00022B5D
		protected CustomDataHolder(Guid dataInstanceId, eCustomDataPrimitiveType dataType)
		{
			this.DataInstanceId = dataInstanceId;
			this.DataType = dataType;
		}
	}
}
