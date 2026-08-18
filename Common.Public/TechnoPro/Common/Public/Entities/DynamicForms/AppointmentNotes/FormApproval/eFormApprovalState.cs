using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003AE RID: 942
	[Serializable]
	public enum eFormApprovalState
	{
		// Token: 0x040016A0 RID: 5792
		[FormApprovalState("Unknown")]
		Unknown,
		// Token: 0x040016A1 RID: 5793
		[FormApprovalState("Waiting for trainee to sign")]
		WaitingForTraineeToSign,
		// Token: 0x040016A2 RID: 5794
		[FormApprovalState("Waiting for supervisor to approve")]
		WaitingForSupervisorToApprove,
		// Token: 0x040016A3 RID: 5795
		[FormApprovalState("Waiting for trainee to update notes")]
		WaitingForTraineeToUpdateNotes,
		// Token: 0x040016A4 RID: 5796
		[FormApprovalState("Approved")]
		Approved
	}
}
