using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.FinalExamRequest
{
	// Token: 0x02000047 RID: 71
	public class StudentWrapper : WrapperBase<PersonBaseDTO>
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x000040F6 File Offset: 0x000022F6
		public StudentWrapper()
		{
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00004100 File Offset: 0x00002300
		public StudentWrapper(PersonBaseDTO student) : base(student)
		{
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000410B File Offset: 0x0000230B
		public StudentWrapper(PersonBaseDTO student, bool submittedFinalExamRequest) : base(student)
		{
			this.SubmittedFinalExamRequest = submittedFinalExamRequest;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000411E File Offset: 0x0000231E
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00004126 File Offset: 0x00002326
		public bool SubmittedFinalExamRequest { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00004130 File Offset: 0x00002330
		public int PersonId
		{
			get
			{
				return (base.Item == null) ? 0 : base.Item.PersonId;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00004158 File Offset: 0x00002358
		public string NameAndNumber
		{
			get
			{
				return (base.Item == null) ? "" : base.Item.GetStudentName();
			}
		}
	}
}
