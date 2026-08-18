using System;

namespace TechnoPro.Common.Public.Entities.Tutoring
{
	// Token: 0x0200015F RID: 351
	public class TutorWithActiveStatus : Tutor
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00011ABA File Offset: 0x0000FCBA
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00011AC2 File Offset: 0x0000FCC2
		public eTutorStatus Status { get; set; }
	}
}
