using System;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.UI.Web.Entity.LookupCourses
{
	// Token: 0x0200002E RID: 46
	public class AcademicTermView : ICloneable<AcademicTermView>, ICloneable
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00002221 File Offset: 0x00000421
		public AcademicTermView()
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00002E73 File Offset: 0x00001073
		protected AcademicTermView(AcademicTermView protoType)
		{
			this.StartMonthDay = protoType.StartMonthDay;
			this.EndMonthDay = protoType.EndMonthDay;
			this.Title = protoType.Title;
			this.TermId = protoType.TermId;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002EB1 File Offset: 0x000010B1
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002EB9 File Offset: 0x000010B9
		public DateTime StartMonthDay { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002EC2 File Offset: 0x000010C2
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00002ECA File Offset: 0x000010CA
		public DateTime EndMonthDay { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002ED3 File Offset: 0x000010D3
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002EDB File Offset: 0x000010DB
		public string Title { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002EE4 File Offset: 0x000010E4
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002EEC File Offset: 0x000010EC
		public int TermId { get; set; }

		// Token: 0x06000117 RID: 279 RVA: 0x00002EF8 File Offset: 0x000010F8
		public AcademicTermView Clone()
		{
			return new AcademicTermView(this);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00002F10 File Offset: 0x00001110
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
