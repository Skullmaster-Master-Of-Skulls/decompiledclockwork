using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003AC RID: 940
	[Obsolete("The mechanism to provide pre-generated views has changed. Implement a class that derives from System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCache and has a parameterless constructor, then associate it with a type that derives from DbContext or ObjectContext by using System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCacheTypeAttribute.", true)]
	public abstract class EntityViewContainer
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000A0A34 File Offset: 0x0009EC34
		internal IEnumerable<KeyValuePair<string, string>> ExtentViews
		{
			get
			{
				for (int i = 0; i < this.ViewCount; i++)
				{
					yield return this.GetViewAt(i);
				}
				yield break;
			}
		}

		// Token: 0x0600224C RID: 8780
		protected abstract KeyValuePair<string, string> GetViewAt(int index);

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000A0A51 File Offset: 0x0009EC51
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x000A0A59 File Offset: 0x0009EC59
		public string EdmEntityContainerName { get; set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x000A0A62 File Offset: 0x0009EC62
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x000A0A6A File Offset: 0x0009EC6A
		public string StoreEntityContainerName { get; set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x000A0A73 File Offset: 0x0009EC73
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x000A0A7B File Offset: 0x0009EC7B
		public string HashOverMappingClosure { get; set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000A0A84 File Offset: 0x0009EC84
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x000A0A8C File Offset: 0x0009EC8C
		[SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "OverAll")]
		public string HashOverAllExtentViews { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000A0A95 File Offset: 0x0009EC95
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x000A0A9D File Offset: 0x0009EC9D
		public int ViewCount { get; protected set; }
	}
}
