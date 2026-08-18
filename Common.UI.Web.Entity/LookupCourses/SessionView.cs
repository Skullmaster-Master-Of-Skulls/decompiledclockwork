using System;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.UI.Web.Entity.LookupCourses
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class SessionView : ICloneable<SessionView>, ICloneable
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00002221 File Offset: 0x00000421
		public SessionView()
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003238 File Offset: 0x00001438
		protected SessionView(SessionView protoType)
		{
			this.AcademicTerm = ((protoType.AcademicTerm == null) ? null : protoType.AcademicTerm.Clone());
			this.StartDate = protoType.StartDate;
			this.EndDate = protoType.EndDate;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003284 File Offset: 0x00001484
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000328C File Offset: 0x0000148C
		public AcademicTermView AcademicTerm { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003298 File Offset: 0x00001498
		public string Title
		{
			get
			{
				return (this.AcademicTerm == null) ? "" : string.Format("{0} {1}", this.AcademicTerm.Title ?? "", this.StartDate.Year.ToString());
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000032ED File Offset: 0x000014ED
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000032F5 File Offset: 0x000014F5
		public DateTime StartDate { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000032FE File Offset: 0x000014FE
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003306 File Offset: 0x00001506
		public DateTime EndDate { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003310 File Offset: 0x00001510
		public string Id
		{
			get
			{
				return this.StartDate.ToString("yyyy-MM-dd");
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003338 File Offset: 0x00001538
		public SessionView Clone()
		{
			return new SessionView(this);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003350 File Offset: 0x00001550
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
