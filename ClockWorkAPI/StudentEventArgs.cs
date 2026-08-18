using System;

namespace ClockWorkAPI
{
	// Token: 0x0200009A RID: 154
	public class StudentEventArgs
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x0002CCBC File Offset: 0x0002BCBC
		public StudentEventArgs(int personid, int action_code, string action_text)
		{
			this.personid = personid;
			this.action_code = action_code;
			this.action_text = action_text;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0002CCDC File Offset: 0x0002BCDC
		public StudentEventArgs(int personid)
		{
			this.personid = personid;
			this.action_code = -1;
			this.action_text = "";
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002CD00 File Offset: 0x0002BD00
		public int PersonId
		{
			get
			{
				return this.personid;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0002CD18 File Offset: 0x0002BD18
		public int Action_Code
		{
			get
			{
				return this.action_code;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0002CD30 File Offset: 0x0002BD30
		public string Action_Text
		{
			get
			{
				return this.action_text;
			}
		}

		// Token: 0x040003EA RID: 1002
		private int personid;

		// Token: 0x040003EB RID: 1003
		private int action_code;

		// Token: 0x040003EC RID: 1004
		private string action_text;
	}
}
