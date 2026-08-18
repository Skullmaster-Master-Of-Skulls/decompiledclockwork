using System;

namespace ReportFunctions.ClockWorkDataSync.ServiceProviders.ServiceProviderData
{
	// Token: 0x02000026 RID: 38
	public class ServiceProviderDataSyncDataItem
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00039F00 File Offset: 0x00038F00
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00039F17 File Offset: 0x00038F17
		public eServiceProviderDataItemType DataItemType { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00039F20 File Offset: 0x00038F20
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00039F37 File Offset: 0x00038F37
		public string DataItemExternalValue { get; set; }

		// Token: 0x060002B3 RID: 691 RVA: 0x00039F40 File Offset: 0x00038F40
		public ServiceProviderDataSyncDataItem()
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00039F4B File Offset: 0x00038F4B
		public ServiceProviderDataSyncDataItem(eServiceProviderDataItemType DataItemType)
		{
			this.DataItemType = DataItemType;
		}
	}
}
