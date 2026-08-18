using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005ED RID: 1517
	[DataContract(Namespace = "http://tpro.ca")]
	public class IntakeUserAccountDTO
	{
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0000E120 File Offset: 0x0000C320
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x0000E128 File Offset: 0x0000C328
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0000E131 File Offset: 0x0000C331
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x0000E139 File Offset: 0x0000C339
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0000E142 File Offset: 0x0000C342
		// (set) Token: 0x06001EFB RID: 7931 RVA: 0x0000E14A File Offset: 0x0000C34A
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0000E153 File Offset: 0x0000C353
		// (set) Token: 0x06001EFD RID: 7933 RVA: 0x0000E15B File Offset: 0x0000C35B
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0000E164 File Offset: 0x0000C364
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x0000E16C File Offset: 0x0000C36C
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0000E175 File Offset: 0x0000C375
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x0000E17D File Offset: 0x0000C37D
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0000E186 File Offset: 0x0000C386
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x0000E18E File Offset: 0x0000C38E
		[DataMember]
		public string IpAddress { get; set; }
	}
}
