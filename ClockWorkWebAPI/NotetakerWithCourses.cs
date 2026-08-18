using System;
using System.Collections.Generic;
using ClockWorkWebAPI.ClockWorkAPIReplacement;

namespace ClockWorkWebAPI
{
	// Token: 0x02000023 RID: 35
	public class NotetakerWithCourses
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000E117 File Offset: 0x0000C317
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000E11F File Offset: 0x0000C31F
		public ServiceProvider Notetaker { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000E128 File Offset: 0x0000C328
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000E130 File Offset: 0x0000C330
		public List<Course> Courses { get; set; }
	}
}
