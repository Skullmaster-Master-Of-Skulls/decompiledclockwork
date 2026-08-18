using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest
{
	// Token: 0x02000A8B RID: 2699
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestForEditClassDefinitionSpecificDTO
	{
		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x0001B706 File Offset: 0x00019906
		// (set) Token: 0x06003891 RID: 14481 RVA: 0x0001B70E File Offset: 0x0001990E
		[DataMember]
		public string TestDeliveredMessage { get; set; }

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x0001B717 File Offset: 0x00019917
		// (set) Token: 0x06003893 RID: 14483 RVA: 0x0001B71F File Offset: 0x0001991F
		[DataMember]
		public string ClassPrivateNote { get; set; }

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x0001B728 File Offset: 0x00019928
		// (set) Token: 0x06003895 RID: 14485 RVA: 0x0001B730 File Offset: 0x00019930
		[DataMember]
		public eClassTestType ExamType { get; set; }

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06003896 RID: 14486 RVA: 0x0001B739 File Offset: 0x00019939
		// (set) Token: 0x06003897 RID: 14487 RVA: 0x0001B741 File Offset: 0x00019941
		[DataMember]
		public string Location { get; set; }

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06003898 RID: 14488 RVA: 0x0001B74A File Offset: 0x0001994A
		// (set) Token: 0x06003899 RID: 14489 RVA: 0x0001B752 File Offset: 0x00019952
		[DataMember]
		public string ExternalExamId { get; set; }

		// Token: 0x0600389A RID: 14490 RVA: 0x0001B75B File Offset: 0x0001995B
		public TestForEditClassDefinitionSpecificDTO()
		{
			this.SetDefaults();
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x0001B76C File Offset: 0x0001996C
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.SetDefaults();
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x00007F9F File Offset: 0x0000619F
		private void SetDefaults()
		{
		}
	}
}
