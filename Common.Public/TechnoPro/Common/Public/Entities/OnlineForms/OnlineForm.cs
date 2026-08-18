using System;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.OnlineForms
{
	// Token: 0x02000275 RID: 629
	public class OnlineForm : BusinessBase<int>
	{
		// Token: 0x060012D0 RID: 4816 RVA: 0x00019110 File Offset: 0x00017310
		public OnlineForm()
		{
			this.Title = "";
			this.Description = "";
			this.ShortCode = "";
			this.UseWizard = false;
			this.RequiresLogin = false;
			this.Captcha = 0;
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00019164 File Offset: 0x00017364
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int OnlineFormId
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

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0001917C File Offset: 0x0001737C
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x00019184 File Offset: 0x00017384
		public string Title { get; set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0001918D File Offset: 0x0001738D
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x00019195 File Offset: 0x00017395
		public string Description { get; set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0001919E File Offset: 0x0001739E
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x000191A6 File Offset: 0x000173A6
		public string ShortCode { get; set; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000191AF File Offset: 0x000173AF
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x000191B7 File Offset: 0x000173B7
		public DynamicForm Form { get; set; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x000191C0 File Offset: 0x000173C0
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x000191C8 File Offset: 0x000173C8
		public bool UseWizard { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x000191D1 File Offset: 0x000173D1
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x000191D9 File Offset: 0x000173D9
		public bool RequiresLogin { get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x000191E2 File Offset: 0x000173E2
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x000191EA File Offset: 0x000173EA
		public bool CanOnlyBeFilledInOnce { get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000191F3 File Offset: 0x000173F3
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x000191FB File Offset: 0x000173FB
		public int Captcha { get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00019204 File Offset: 0x00017404
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x0001920C File Offset: 0x0001740C
		public int StudentEmailConfirmationTemplateId { get; set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00019215 File Offset: 0x00017415
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x0001921D File Offset: 0x0001741D
		public int StaffEmailConfirmationTemplateId { get; set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00019226 File Offset: 0x00017426
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0001922E File Offset: 0x0001742E
		public string SubmitMessage { get; set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00019237 File Offset: 0x00017437
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0001923F File Offset: 0x0001743F
		public string SubmitButtonText { get; set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00019248 File Offset: 0x00017448
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x00019250 File Offset: 0x00017450
		public DateTime? StartDate { get; set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00019259 File Offset: 0x00017459
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x00019261 File Offset: 0x00017461
		public DateTime? EndDate { get; set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0001926A File Offset: 0x0001746A
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x00019272 File Offset: 0x00017472
		public Group RestrictedToGroup { get; set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0001927B File Offset: 0x0001747B
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x00019283 File Offset: 0x00017483
		public bool IsDeleted { get; set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0001928C File Offset: 0x0001748C
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x00019294 File Offset: 0x00017494
		public bool IsDisabled { get; set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0001929D File Offset: 0x0001749D
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x000192A5 File Offset: 0x000174A5
		public BasicPerson WhoCreated { get; set; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000192AE File Offset: 0x000174AE
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x000192B6 File Offset: 0x000174B6
		public BasicPerson WhoLastModified { get; set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x000192BF File Offset: 0x000174BF
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x000192C7 File Offset: 0x000174C7
		public DateTime DateCreated { get; set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x000192D0 File Offset: 0x000174D0
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x000192D8 File Offset: 0x000174D8
		public DateTime? DateLastModified { get; set; }
	}
}
