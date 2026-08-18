using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicControls
{
	// Token: 0x020003AB RID: 939
	public class SingleFileMetaData : BusinessBase<int>
	{
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00020B90 File Offset: 0x0001ED90
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int DataId
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

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		public string FileName { get; set; }
	}
}
