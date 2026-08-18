using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos;

namespace TechnoPro.Common.Public.Entities.DataSync.Student
{
	// Token: 0x020003DA RID: 986
	public class StudentDataSyncPreviewData : BusinessBase<string>
	{
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x00021F64 File Offset: 0x00020164
		// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string StudentNumber
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

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x00021F7C File Offset: 0x0002017C
		// (set) Token: 0x06001E70 RID: 7792 RVA: 0x00021F84 File Offset: 0x00020184
		public string FirstName { get; set; }

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06001E71 RID: 7793 RVA: 0x00021F8D File Offset: 0x0002018D
		// (set) Token: 0x06001E72 RID: 7794 RVA: 0x00021F95 File Offset: 0x00020195
		public string MiddleName { get; set; }

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00021F9E File Offset: 0x0002019E
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x00021FA6 File Offset: 0x000201A6
		public string LastName { get; set; }

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00021FAF File Offset: 0x000201AF
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x00021FB7 File Offset: 0x000201B7
		public string Email { get; set; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x00021FC0 File Offset: 0x000201C0
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x00021FC8 File Offset: 0x000201C8
		public string Username { get; set; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00021FD1 File Offset: 0x000201D1
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x00021FD9 File Offset: 0x000201D9
		public IList<DataSyncExternalData> ExternalDataItems { get; set; }
	}
}
