using System;

namespace TechnoPro.Common.Public.Entities.Intake
{
	// Token: 0x02000323 RID: 803
	[Serializable]
	public enum ePreIntakeStatus
	{
		// Token: 0x04001483 RID: 5251
		[PreIntakeStatus("Unknown")]
		Unknown,
		// Token: 0x04001484 RID: 5252
		[PreIntakeStatus("Ready to intake")]
		ReadyToIntake,
		// Token: 0x04001485 RID: 5253
		[PreIntakeStatus("Student number already exists")]
		StudentNumberAlreadyExists
	}
}
