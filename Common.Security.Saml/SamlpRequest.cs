using System;
using System.Xml;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000013 RID: 19
	public class SamlpRequest
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003B88 File Offset: 0x00001D88
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003B90 File Offset: 0x00001D90
		public string ID { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003B99 File Offset: 0x00001D99
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public string AssertionConsumerServiceURL { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003BAA File Offset: 0x00001DAA
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003BB2 File Offset: 0x00001DB2
		public string Destination { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003BBB File Offset: 0x00001DBB
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003BC3 File Offset: 0x00001DC3
		public string Issuer { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003BCC File Offset: 0x00001DCC
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public bool NameIDPolicy_AllowCreation { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public virtual SamlpRequestWriter TokenWriter
		{
			get
			{
				return new SamlpRequestWriter();
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003BF7 File Offset: 0x00001DF7
		public void WriteTo(XmlWriter writer)
		{
			this.TokenWriter.WriteToSamlp(writer, this);
		}
	}
}
