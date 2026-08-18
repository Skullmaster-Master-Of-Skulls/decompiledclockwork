using System;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Surveys
{
	// Token: 0x0200017E RID: 382
	public class Survey : BusinessBase<int>
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x00012B44 File Offset: 0x00010D44
		public Survey()
		{
			this.Title = "";
			this.Description = "";
			this.ShortCode = "";
			this.UseWizard = false;
			this.RequiresLogin = false;
			this.Captcha = 0;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00012B98 File Offset: 0x00010D98
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SurveyId
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00012BB0 File Offset: 0x00010DB0
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00012BB8 File Offset: 0x00010DB8
		public string Title { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00012BC1 File Offset: 0x00010DC1
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00012BC9 File Offset: 0x00010DC9
		public string Description { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00012BD2 File Offset: 0x00010DD2
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00012BDA File Offset: 0x00010DDA
		public string ShortCode { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00012BE3 File Offset: 0x00010DE3
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00012BEB File Offset: 0x00010DEB
		public DynamicForm Form { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00012BF4 File Offset: 0x00010DF4
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00012BFC File Offset: 0x00010DFC
		public bool UseWizard { get; set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00012C05 File Offset: 0x00010E05
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x00012C0D File Offset: 0x00010E0D
		public bool RequiresLogin { get; set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00012C16 File Offset: 0x00010E16
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00012C1E File Offset: 0x00010E1E
		public bool CanOnlyBeFilledInOnce { get; set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00012C27 File Offset: 0x00010E27
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00012C2F File Offset: 0x00010E2F
		public int Captcha { get; set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00012C38 File Offset: 0x00010E38
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00012C40 File Offset: 0x00010E40
		public int StudentEmailConfirmationTemplateId { get; set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00012C49 File Offset: 0x00010E49
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00012C51 File Offset: 0x00010E51
		public int StaffEmailConfirmationTemplateId { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00012C5A File Offset: 0x00010E5A
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00012C62 File Offset: 0x00010E62
		public string SubmitMessage { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00012C6B File Offset: 0x00010E6B
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00012C73 File Offset: 0x00010E73
		public string SubmitButtonText { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00012C7C File Offset: 0x00010E7C
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00012C84 File Offset: 0x00010E84
		public DateTime? StartDate { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00012C8D File Offset: 0x00010E8D
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00012C95 File Offset: 0x00010E95
		public DateTime? EndDate { get; set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00012C9E File Offset: 0x00010E9E
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00012CA6 File Offset: 0x00010EA6
		public Group RestrictedToGroup { get; set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00012CAF File Offset: 0x00010EAF
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00012CB7 File Offset: 0x00010EB7
		public bool IsDeleted { get; set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00012CC0 File Offset: 0x00010EC0
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public bool IsDisabled { get; set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00012CD1 File Offset: 0x00010ED1
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00012CD9 File Offset: 0x00010ED9
		public BasicPerson WhoCreated { get; set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00012CE2 File Offset: 0x00010EE2
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00012CEA File Offset: 0x00010EEA
		public BasicPerson WhoLastModified { get; set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00012CF3 File Offset: 0x00010EF3
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00012CFB File Offset: 0x00010EFB
		public DateTime DateCreated { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00012D04 File Offset: 0x00010F04
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00012D0C File Offset: 0x00010F0C
		public DateTime? DateLastModified { get; set; }
	}
}
