using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x02000927 RID: 2343
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppCancelReasonGroupDTO : ICloneable<AppCancelReasonGroupDTO>, ICloneable
	{
		// Token: 0x06002F82 RID: 12162 RVA: 0x000036BD File Offset: 0x000018BD
		public AppCancelReasonGroupDTO()
		{
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x00016ACC File Offset: 0x00014CCC
		public AppCancelReasonGroupDTO(AppCancelReasonGroupDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.CancelReasonGroupName = item.CancelReasonGroupName;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x00016AF8 File Offset: 0x00014CF8
		// (set) Token: 0x06002F85 RID: 12165 RVA: 0x00016B00 File Offset: 0x00014D00
		[DataMember]
		public string CancelReasonGroupName { get; set; }

		// Token: 0x06002F86 RID: 12166 RVA: 0x00016B0C File Offset: 0x00014D0C
		public AppCancelReasonGroupDTO Clone()
		{
			return new AppCancelReasonGroupDTO(this);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x00016B24 File Offset: 0x00014D24
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
