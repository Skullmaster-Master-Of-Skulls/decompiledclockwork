using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003F3 RID: 1011
	public class DataSyncDataResult
	{
		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x00022B98 File Offset: 0x00020D98
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x00022BA0 File Offset: 0x00020DA0
		public BasicPerson ExternalStudentInfo { get; set; }

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00022BA9 File Offset: 0x00020DA9
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x00022BB1 File Offset: 0x00020DB1
		public bool UpdatedName { get; set; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00022BBA File Offset: 0x00020DBA
		public bool FoundInClockWork
		{
			get
			{
				BasicPerson externalStudentInfo = this.ExternalStudentInfo;
				return ((externalStudentInfo != null) ? externalStudentInfo.PersonId : 0) > 0;
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00022BD1 File Offset: 0x00020DD1
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x00022BD9 File Offset: 0x00020DD9
		public eDataSyncDataStatus ResultStatus { get; set; }

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x00022BE2 File Offset: 0x00020DE2
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x00022BEA File Offset: 0x00020DEA
		public string ResultMessage { get; set; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00022BF3 File Offset: 0x00020DF3
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x00022BFB File Offset: 0x00020DFB
		public IList<DataSyncDataItemResult> ItemResults { get; set; }
	}
}
