using System;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x02000184 RID: 388
	[Serializable]
	public enum eStudentFileStatusType
	{
		// Token: 0x0400075E RID: 1886
		[StudentFileStatusType]
		Unknown,
		// Token: 0x0400075F RID: 1887
		[StudentFileStatusType]
		Open,
		// Token: 0x04000760 RID: 1888
		[StudentFileStatusType("[closed]")]
		Closed
	}
}
