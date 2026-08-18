using System;

namespace ClockWorkAPI.Notetaking
{
	// Token: 0x02000055 RID: 85
	public class NotetakeeCourse
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001731C File Offset: 0x0001631C
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00017334 File Offset: 0x00016334
		public Notetaker Notetaker
		{
			get
			{
				return this.notetaker;
			}
			set
			{
				this.notetaker = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00017340 File Offset: 0x00016340
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00017358 File Offset: 0x00016358
		public Notetakee Notetakee
		{
			get
			{
				return this.notetakee;
			}
			set
			{
				this.notetakee = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00017364 File Offset: 0x00016364
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0001737C File Offset: 0x0001637C
		public Course Course
		{
			get
			{
				return this.course;
			}
			set
			{
				this.course = value;
			}
		}

		// Token: 0x040001C7 RID: 455
		private Course course;

		// Token: 0x040001C8 RID: 456
		private Notetaker notetaker;

		// Token: 0x040001C9 RID: 457
		private Notetakee notetakee;
	}
}
