using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000ABF RID: 2751
	[DataContract(Namespace = "http://tpro.ca")]
	public class RecurringInstanceSetModifyBehaviourDTO
	{
		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x0001C4BF File Offset: 0x0001A6BF
		// (set) Token: 0x06003A55 RID: 14933 RVA: 0x0001C4C7 File Offset: 0x0001A6C7
		[DataMember]
		public eRecurringInstanceSetPropertyModifyBehaviour PrivateChangeBehaviour { get; set; }

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		// (set) Token: 0x06003A57 RID: 14935 RVA: 0x0001C4D8 File Offset: 0x0001A6D8
		[DataMember]
		public eRecurringInstanceSetPropertyModifyBehaviour LockedChangeBehaviour { get; set; }

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x0001C4E1 File Offset: 0x0001A6E1
		// (set) Token: 0x06003A59 RID: 14937 RVA: 0x0001C4E9 File Offset: 0x0001A6E9
		[DataMember]
		public eRecurringInstanceSetPropertyModifyBehaviour AttendeesChangeBehaviour { get; set; }
	}
}
