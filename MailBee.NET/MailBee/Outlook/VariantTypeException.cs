using System;

namespace MailBee.Outlook
{
	// Token: 0x02000596 RID: 1430
	[Serializable]
	internal abstract class VariantTypeException : HPSFException
	{
		// Token: 0x06002FF8 RID: 12280 RVA: 0x000E252F File Offset: 0x000E152F
		public VariantTypeException(long A_0, object A_1, string A_2) : base(A_2)
		{
			this.variantType = A_0;
			this.value = A_1;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x000E2546 File Offset: 0x000E1546
		public long VariantType
		{
			get
			{
				return this.variantType;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002FFA RID: 12282 RVA: 0x000E254E File Offset: 0x000E154E
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400201D RID: 8221
		private object value;

		// Token: 0x0400201E RID: 8222
		private long variantType;
	}
}
