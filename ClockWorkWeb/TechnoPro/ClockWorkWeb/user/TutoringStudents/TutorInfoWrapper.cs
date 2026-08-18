using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x0200004E RID: 78
	public class TutorInfoWrapper
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000AF9E File Offset: 0x0000919E
		public TutorInfoWrapper()
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C664 File Offset: 0x0000A864
		public TutorInfoWrapper(TutorDTO tutor, List<DynamicDataDTO> data)
		{
			bool flag = tutor == null;
			if (!flag)
			{
				this.TutorId = tutor.PersonId;
				this.FirstName = tutor.FirstName;
				this.LastName = tutor.LastName;
				this.DynamicDataItems = (from g in data
				select new DynamicDataItemWrapper(g)).ToList<DynamicDataItemWrapper>();
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000C6DB File Offset: 0x0000A8DB
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000C6E3 File Offset: 0x0000A8E3
		public IList<DynamicDataItemWrapper> DynamicDataItems { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000C6F4 File Offset: 0x0000A8F4
		public int TutorId { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C6FD File Offset: 0x0000A8FD
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000C705 File Offset: 0x0000A905
		public string FirstName { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000C70E File Offset: 0x0000A90E
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000C716 File Offset: 0x0000A916
		public string LastName { get; set; }
	}
}
