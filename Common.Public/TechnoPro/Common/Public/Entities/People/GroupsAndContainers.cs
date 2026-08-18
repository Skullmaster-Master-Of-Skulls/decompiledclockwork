using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x0200025D RID: 605
	public class GroupsAndContainers
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x000188BC File Offset: 0x00016ABC
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x000188C4 File Offset: 0x00016AC4
		public IList<Group> Groups { get; set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x000188CD File Offset: 0x00016ACD
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x000188D5 File Offset: 0x00016AD5
		public IList<GroupContainer> GroupContainers { get; set; }
	}
}
