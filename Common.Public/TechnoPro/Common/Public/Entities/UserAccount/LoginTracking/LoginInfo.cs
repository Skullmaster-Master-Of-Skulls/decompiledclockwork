using System;
using System.Collections.Generic;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Public.Entities.UserAccount.LoginTracking
{
	// Token: 0x02000139 RID: 313
	public class LoginInfo : BusinessBase<int>
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00010734 File Offset: 0x0000E934
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001074C File Offset: 0x0000E94C
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x00010754 File Offset: 0x0000E954
		public DateTime LoginDate { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001075D File Offset: 0x0000E95D
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00010765 File Offset: 0x0000E965
		public string Ip { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001076E File Offset: 0x0000E96E
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00010776 File Offset: 0x0000E976
		public Version ClockWorkVersion { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001077F File Offset: 0x0000E97F
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00010787 File Offset: 0x0000E987
		public IList<DotNetVersion> NetVersions { get; set; }
	}
}
