using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000513 RID: 1299
	public class AccommodationForTest : IDynamicDataHoldingObject
	{
		// Token: 0x060027B4 RID: 10164 RVA: 0x00029A8D File Offset: 0x00027C8D
		public AccommodationForTest()
		{
			this.UseForTest = false;
			this.Discrepency = false;
			this.DiscrepencyMessage = "";
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x00029AB3 File Offset: 0x00027CB3
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x00029ABB File Offset: 0x00027CBB
		public DynamicData DynamicFieldData { get; set; }

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x00029AC4 File Offset: 0x00027CC4
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x00029ACC File Offset: 0x00027CCC
		public bool UseForTest { get; set; }

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x00029AD5 File Offset: 0x00027CD5
		// (set) Token: 0x060027BA RID: 10170 RVA: 0x00029ADD File Offset: 0x00027CDD
		public bool Discrepency { get; set; }

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060027BB RID: 10171 RVA: 0x00029AE6 File Offset: 0x00027CE6
		// (set) Token: 0x060027BC RID: 10172 RVA: 0x00029AEE File Offset: 0x00027CEE
		public string DiscrepencyMessage { get; set; }

		// Token: 0x060027BD RID: 10173 RVA: 0x00029AF8 File Offset: 0x00027CF8
		public DynamicData GetDynamicData()
		{
			return this.DynamicFieldData;
		}
	}
}
