using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount
{
	// Token: 0x02000148 RID: 328
	[DataContract(Namespace = "http://tpro.ca")]
	public class PasswordPolicyDTO
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00003A30 File Offset: 0x00001C30
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00003A38 File Offset: 0x00001C38
		[DataMember]
		public int MinimumLengthTotal { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00003A41 File Offset: 0x00001C41
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00003A49 File Offset: 0x00001C49
		[DataMember]
		public int MinimumLengthLowercase { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00003A52 File Offset: 0x00001C52
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00003A5A File Offset: 0x00001C5A
		[DataMember]
		public int MinimumLengthUppercase { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00003A63 File Offset: 0x00001C63
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00003A6B File Offset: 0x00001C6B
		[DataMember]
		public int MinimumLengthSpecialCharacter { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00003A74 File Offset: 0x00001C74
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00003A7C File Offset: 0x00001C7C
		[DataMember]
		public int MinimumLengthNumeric { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00003A85 File Offset: 0x00001C85
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00003A8D File Offset: 0x00001C8D
		[DataMember]
		public int NumPreviousPasswordsCantUse { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00003A96 File Offset: 0x00001C96
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00003A9E File Offset: 0x00001C9E
		[DataMember]
		public int AutoPasswordExpiryNumDays { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00003AA7 File Offset: 0x00001CA7
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00003AAF File Offset: 0x00001CAF
		[DataMember]
		public int MaxFailedAttempts { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00003AB8 File Offset: 0x00001CB8
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x00003AC0 File Offset: 0x00001CC0
		[DataMember]
		public int LockoutDurationMinutes { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00003AC9 File Offset: 0x00001CC9
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x00003AD1 File Offset: 0x00001CD1
		[DataMember]
		public bool EnforcePasswordPolicy { get; set; }

		// Token: 0x0600083F RID: 2111 RVA: 0x00003ADC File Offset: 0x00001CDC
		public bool AreSettingsIdentical(PasswordPolicyDTO otherPolicy)
		{
			return this.MinimumLengthTotal == otherPolicy.MinimumLengthTotal && this.MinimumLengthLowercase == otherPolicy.MinimumLengthLowercase && this.MinimumLengthUppercase == otherPolicy.MinimumLengthUppercase && this.MinimumLengthSpecialCharacter == otherPolicy.MinimumLengthSpecialCharacter && this.MinimumLengthNumeric == otherPolicy.MinimumLengthNumeric && this.NumPreviousPasswordsCantUse == otherPolicy.NumPreviousPasswordsCantUse && this.AutoPasswordExpiryNumDays == otherPolicy.AutoPasswordExpiryNumDays && this.MaxFailedAttempts == otherPolicy.MaxFailedAttempts && this.LockoutDurationMinutes == otherPolicy.LockoutDurationMinutes && this.EnforcePasswordPolicy == otherPolicy.EnforcePasswordPolicy;
		}
	}
}
