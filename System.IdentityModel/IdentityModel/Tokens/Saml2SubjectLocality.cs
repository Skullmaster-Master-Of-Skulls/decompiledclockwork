using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000148 RID: 328
	public class Saml2SubjectLocality
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x00004469 File Offset: 0x00002669
		public Saml2SubjectLocality()
		{
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002B93F File Offset: 0x00029B3F
		public Saml2SubjectLocality(string address, string dnsName)
		{
			this.Address = address;
			this.DnsName = dnsName;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002B955 File Offset: 0x00029B55
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0002B95D File Offset: 0x00029B5D
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0002B96B File Offset: 0x00029B6B
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x0002B973 File Offset: 0x00029B73
		public string DnsName
		{
			get
			{
				return this.dnsName;
			}
			set
			{
				this.dnsName = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x04000B78 RID: 2936
		private string address;

		// Token: 0x04000B79 RID: 2937
		private string dnsName;
	}
}
