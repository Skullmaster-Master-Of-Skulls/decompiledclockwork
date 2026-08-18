using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal
{
	// Token: 0x02000203 RID: 515
	public class ServiceProviderType : BusinessBase<int>
	{
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00016E1C File Offset: 0x0001501C
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderTypeId
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

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00016E34 File Offset: 0x00015034
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x00016E3C File Offset: 0x0001503C
		public string Title { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00016E45 File Offset: 0x00015045
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x00016E4D File Offset: 0x0001504D
		public eServiceProviderMatchingMethod MatchingMethod { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00016E56 File Offset: 0x00015056
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x00016E5E File Offset: 0x0001505E
		public eSpecializedServiceProviderType SpecializedServiceProviderType { get; set; }
	}
}
