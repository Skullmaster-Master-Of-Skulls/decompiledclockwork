using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000321 RID: 801
	[Serializable]
	public enum eCreateRealStudentAccountFromIntakeStatus
	{
		// Token: 0x0400147D RID: 5245
		[CreateRealStudentAccountFromIntakeStatus("Unknown")]
		Unknown,
		// Token: 0x0400147E RID: 5246
		[CreateRealStudentAccountFromIntakeStatus("Successfully created student account")]
		SuccessfullyCreatedStudentAccount,
		// Token: 0x0400147F RID: 5247
		[CreateRealStudentAccountFromIntakeStatus("Failed (Unknown reason)")]
		FailedUnknown,
		// Token: 0x04001480 RID: 5248
		[CreateRealStudentAccountFromIntakeStatus("Failed (Student [number] already exists in ClockWork)")]
		FailedStudentNumberAlreadyExistsInClockWork
	}
}
