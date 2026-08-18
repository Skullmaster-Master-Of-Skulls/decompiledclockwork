using System;
using System.Timers;

namespace TechnoPro.Common.Public.Entities.Timers
{
	// Token: 0x0200016C RID: 364
	public class ClockWorkServerTimer : BusinessBase<string>
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000122FC File Offset: 0x000104FC
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Name
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00012314 File Offset: 0x00010514
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x0001231C File Offset: 0x0001051C
		public double TimeInterval { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00012325 File Offset: 0x00010525
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0001232D File Offset: 0x0001052D
		public bool Enabled { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00012336 File Offset: 0x00010536
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0001233E File Offset: 0x0001053E
		public Action<object, ElapsedEventArgs> TimeElapsedFunc { get; set; }
	}
}
