using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200013D RID: 317
	public class UpdateFileCondition : BusinessBase<string>
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x000107C4 File Offset: 0x0000E9C4
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public string Filename
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

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x000107DC File Offset: 0x0000E9DC
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public string Version { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000107ED File Offset: 0x0000E9ED
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x000107F5 File Offset: 0x0000E9F5
		public bool IsPublic { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x000107FE File Offset: 0x0000E9FE
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00010806 File Offset: 0x0000EA06
		public bool IsActive { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x0001080F File Offset: 0x0000EA0F
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x00010817 File Offset: 0x0000EA17
		public IList<string> AllowableToUpgradeVersions { get; set; }
	}
}
