using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002AB RID: 683
	public class User : BusinessBase<string>
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0001A130 File Offset: 0x00018330
		// (set) Token: 0x06001499 RID: 5273 RVA: 0x0001A138 File Offset: 0x00018338
		public virtual int UserId { get; set; }

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0001A141 File Offset: 0x00018341
		// (set) Token: 0x0600149B RID: 5275 RVA: 0x0001A149 File Offset: 0x00018349
		public virtual string FirstName { get; set; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0001A152 File Offset: 0x00018352
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x0001A15A File Offset: 0x0001835A
		public virtual string LastName { get; set; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0001A164 File Offset: 0x00018364
		public virtual string FullName
		{
			get
			{
				return string.Format("{0} {1}", this.FirstName, this.LastName);
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0001A18C File Offset: 0x0001838C
		// (set) Token: 0x060014A0 RID: 5280 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string Name
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

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0001A1A4 File Offset: 0x000183A4
		// (set) Token: 0x060014A2 RID: 5282 RVA: 0x0001A1AC File Offset: 0x000183AC
		public virtual AuthenticationSession AuthenticationSession { get; set; }

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0001A1B5 File Offset: 0x000183B5
		// (set) Token: 0x060014A4 RID: 5284 RVA: 0x0001A1BD File Offset: 0x000183BD
		public virtual IList<Role> Roles { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0001A1C6 File Offset: 0x000183C6
		// (set) Token: 0x060014A6 RID: 5286 RVA: 0x0001A1CE File Offset: 0x000183CE
		public virtual string Email { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0001A1D7 File Offset: 0x000183D7
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0001A1DF File Offset: 0x000183DF
		public virtual string Phone { get; set; }

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0001A1E8 File Offset: 0x000183E8
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x0001A1F0 File Offset: 0x000183F0
		public bool RequirePasswordChange { get; set; }
	}
}
