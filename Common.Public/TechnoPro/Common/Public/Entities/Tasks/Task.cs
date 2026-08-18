using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Tasks
{
	// Token: 0x02000175 RID: 373
	public class Task : BusinessBase<int>
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00012868 File Offset: 0x00010A68
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TaskId
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

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00012880 File Offset: 0x00010A80
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x00012888 File Offset: 0x00010A88
		public PersonBase Owner { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00012891 File Offset: 0x00010A91
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x00012899 File Offset: 0x00010A99
		public string Title { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x000128A2 File Offset: 0x00010AA2
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x000128AA File Offset: 0x00010AAA
		public string Description { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x000128B3 File Offset: 0x00010AB3
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x000128BB File Offset: 0x00010ABB
		public DateTime? DueDate { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x000128C4 File Offset: 0x00010AC4
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x000128CC File Offset: 0x00010ACC
		public bool IsCompleted { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000128D5 File Offset: 0x00010AD5
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x000128DD File Offset: 0x00010ADD
		public int IconId { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x000128E6 File Offset: 0x00010AE6
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x000128EE File Offset: 0x00010AEE
		public int OrderNum { get; set; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000128F7 File Offset: 0x00010AF7
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x000128FF File Offset: 0x00010AFF
		public DateTime? Reminder { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00012908 File Offset: 0x00010B08
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00012910 File Offset: 0x00010B10
		public TaskGroup TaskGroup { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00012919 File Offset: 0x00010B19
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00012921 File Offset: 0x00010B21
		public int Progress { get; set; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001292A File Offset: 0x00010B2A
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x00012932 File Offset: 0x00010B32
		public eTaskPriority Priority { get; set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001293B File Offset: 0x00010B3B
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x00012943 File Offset: 0x00010B43
		public int? OverrideColourArgb { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001294C File Offset: 0x00010B4C
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x00012954 File Offset: 0x00010B54
		public DateTime DateEntered { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001295D File Offset: 0x00010B5D
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x00012965 File Offset: 0x00010B65
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001296E File Offset: 0x00010B6E
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x00012976 File Offset: 0x00010B76
		public DateTime? DateLastModified { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001297F File Offset: 0x00010B7F
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00012987 File Offset: 0x00010B87
		public PersonBase WhoLastModified { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00012990 File Offset: 0x00010B90
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00012998 File Offset: 0x00010B98
		public int? PrimaryTaskId { get; set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000129A1 File Offset: 0x00010BA1
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x000129A9 File Offset: 0x00010BA9
		public List<TaskNote> Notes { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000129B2 File Offset: 0x00010BB2
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x000129BA File Offset: 0x00010BBA
		public List<TaskClient> Clients { get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x000129C3 File Offset: 0x00010BC3
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000129CB File Offset: 0x00010BCB
		public bool IsPrivate { get; set; }
	}
}
