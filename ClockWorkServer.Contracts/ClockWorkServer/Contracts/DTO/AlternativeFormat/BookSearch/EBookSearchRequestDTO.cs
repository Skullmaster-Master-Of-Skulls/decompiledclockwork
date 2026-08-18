using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C6C RID: 3180
	[DataContract(Namespace = "http://tpro.ca")]
	public class EBookSearchRequestDTO
	{
		// Token: 0x1700186A RID: 6250
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x000204F4 File Offset: 0x0001E6F4
		// (set) Token: 0x06004233 RID: 16947 RVA: 0x000204FC File Offset: 0x0001E6FC
		[DataMember]
		public string Id { get; set; }

		// Token: 0x1700186B RID: 6251
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x00020505 File Offset: 0x0001E705
		// (set) Token: 0x06004235 RID: 16949 RVA: 0x0002050D File Offset: 0x0001E70D
		[DataMember]
		public string SearchText { get; set; }

		// Token: 0x1700186C RID: 6252
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x00020516 File Offset: 0x0001E716
		// (set) Token: 0x06004237 RID: 16951 RVA: 0x0002051E File Offset: 0x0001E71E
		[DataMember]
		public string ISBN { get; set; }

		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x06004238 RID: 16952 RVA: 0x00020527 File Offset: 0x0001E727
		// (set) Token: 0x06004239 RID: 16953 RVA: 0x0002052F File Offset: 0x0001E72F
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700186E RID: 6254
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x00020538 File Offset: 0x0001E738
		// (set) Token: 0x0600423B RID: 16955 RVA: 0x00020540 File Offset: 0x0001E740
		[DataMember]
		public string Author { get; set; }

		// Token: 0x1700186F RID: 6255
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x00020549 File Offset: 0x0001E749
		// (set) Token: 0x0600423D RID: 16957 RVA: 0x00020551 File Offset: 0x0001E751
		[DataMember]
		public string Publisher { get; set; }

		// Token: 0x17001870 RID: 6256
		// (get) Token: 0x0600423E RID: 16958 RVA: 0x0002055A File Offset: 0x0001E75A
		// (set) Token: 0x0600423F RID: 16959 RVA: 0x00020562 File Offset: 0x0001E762
		[DataMember]
		public int MaxNumberOfBooksToReturn { get; set; }

		// Token: 0x17001871 RID: 6257
		// (get) Token: 0x06004240 RID: 16960 RVA: 0x0002056B File Offset: 0x0001E76B
		// (set) Token: 0x06004241 RID: 16961 RVA: 0x00020573 File Offset: 0x0001E773
		[DataMember]
		public eBookSearchProviderType SearchProviderType { get; set; }

		// Token: 0x06004242 RID: 16962 RVA: 0x0002057C File Offset: 0x0001E77C
		public EBookSearchRequestDTO()
		{
			this.SetDefaults();
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x0002058D File Offset: 0x0001E78D
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x00020597 File Offset: 0x0001E797
		private void SetDefaults()
		{
			this.MaxNumberOfBooksToReturn = 40;
			this.SearchProviderType = eBookSearchProviderType.All;
		}
	}
}
