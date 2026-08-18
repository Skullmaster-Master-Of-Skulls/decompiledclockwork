using System;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000160 RID: 352
	[Serializable]
	public class TPMailAddress : BusinessBase<string>, ICloneable<TPMailAddress>, ICloneable
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x00011ADC File Offset: 0x0000FCDC
		public string EmailAddress { get; set; }

		// Token: 0x0600085A RID: 2138 RVA: 0x00011AE5 File Offset: 0x0000FCE5
		public TPMailAddress()
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00011AF0 File Offset: 0x0000FCF0
		public TPMailAddress Clone()
		{
			return new TPMailAddress(this);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00011B08 File Offset: 0x0000FD08
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00011B20 File Offset: 0x0000FD20
		public TPMailAddress(TPMailAddress item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.EmailAddress = item.EmailAddress;
			}
		}
	}
}
