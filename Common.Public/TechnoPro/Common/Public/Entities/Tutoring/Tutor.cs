using System;

namespace TechnoPro.Common.Public.Entities.Tutoring
{
	// Token: 0x0200015B RID: 347
	public class Tutor : TutorBase
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00011977 File Offset: 0x0000FB77
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0001197F File Offset: 0x0000FB7F
		public string Specializations { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00011988 File Offset: 0x0000FB88
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x00011990 File Offset: 0x0000FB90
		public string PublicNoteFromTutor { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00011999 File Offset: 0x0000FB99
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x000119A1 File Offset: 0x0000FBA1
		public string Email { get; set; }
	}
}
