using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000267 RID: 615
	public class UserGroupObject : BusinessBase<UserGroupObjectId>
	{
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00018B58 File Offset: 0x00016D58
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00018B70 File Offset: 0x00016D70
		public virtual UserGroupObjectId ObjectId
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00018B7B File Offset: 0x00016D7B
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00018B83 File Offset: 0x00016D83
		public string DisplayName { get; set; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00018B8C File Offset: 0x00016D8C
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00018B94 File Offset: 0x00016D94
		public string Description { get; set; }

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00018B9D File Offset: 0x00016D9D
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x00018BA5 File Offset: 0x00016DA5
		public PersonBase Person { get; set; }
	}
}
