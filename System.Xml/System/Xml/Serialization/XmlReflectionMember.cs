using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200031A RID: 794
	public class XmlReflectionMember
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x000B35C2 File Offset: 0x000B25C2
		// (set) Token: 0x06002592 RID: 9618 RVA: 0x000B35CA File Offset: 0x000B25CA
		public Type MemberType
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000B35D3 File Offset: 0x000B25D3
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x000B35DB File Offset: 0x000B25DB
		public XmlAttributes XmlAttributes
		{
			get
			{
				return this.xmlAttributes;
			}
			set
			{
				this.xmlAttributes = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x000B35E4 File Offset: 0x000B25E4
		// (set) Token: 0x06002596 RID: 9622 RVA: 0x000B35EC File Offset: 0x000B25EC
		public SoapAttributes SoapAttributes
		{
			get
			{
				return this.soapAttributes;
			}
			set
			{
				this.soapAttributes = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x000B35F5 File Offset: 0x000B25F5
		// (set) Token: 0x06002598 RID: 9624 RVA: 0x000B360B File Offset: 0x000B260B
		public string MemberName
		{
			get
			{
				if (this.memberName != null)
				{
					return this.memberName;
				}
				return string.Empty;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x000B3614 File Offset: 0x000B2614
		// (set) Token: 0x0600259A RID: 9626 RVA: 0x000B361C File Offset: 0x000B261C
		public bool IsReturnValue
		{
			get
			{
				return this.isReturnValue;
			}
			set
			{
				this.isReturnValue = value;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000B3625 File Offset: 0x000B2625
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x000B362D File Offset: 0x000B262D
		public bool OverrideIsNullable
		{
			get
			{
				return this.overrideIsNullable;
			}
			set
			{
				this.overrideIsNullable = value;
			}
		}

		// Token: 0x040015A9 RID: 5545
		private string memberName;

		// Token: 0x040015AA RID: 5546
		private Type type;

		// Token: 0x040015AB RID: 5547
		private XmlAttributes xmlAttributes = new XmlAttributes();

		// Token: 0x040015AC RID: 5548
		private SoapAttributes soapAttributes = new SoapAttributes();

		// Token: 0x040015AD RID: 5549
		private bool isReturnValue;

		// Token: 0x040015AE RID: 5550
		private bool overrideIsNullable;
	}
}
