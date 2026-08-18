using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001A0 RID: 416
	public class XmlReflectionMember
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0007C266 File Offset: 0x0007A466
		// (set) Token: 0x06001B60 RID: 7008 RVA: 0x0007C26E File Offset: 0x0007A46E
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

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0007C277 File Offset: 0x0007A477
		// (set) Token: 0x06001B62 RID: 7010 RVA: 0x0007C27F File Offset: 0x0007A47F
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

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0007C288 File Offset: 0x0007A488
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x0007C290 File Offset: 0x0007A490
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

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0007C299 File Offset: 0x0007A499
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x0007C2AF File Offset: 0x0007A4AF
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

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x0007C2B8 File Offset: 0x0007A4B8
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x0007C2C0 File Offset: 0x0007A4C0
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

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0007C2C9 File Offset: 0x0007A4C9
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0007C2D1 File Offset: 0x0007A4D1
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

		// Token: 0x04000C1A RID: 3098
		private string memberName;

		// Token: 0x04000C1B RID: 3099
		private Type type;

		// Token: 0x04000C1C RID: 3100
		private XmlAttributes xmlAttributes = new XmlAttributes();

		// Token: 0x04000C1D RID: 3101
		private SoapAttributes soapAttributes = new SoapAttributes();

		// Token: 0x04000C1E RID: 3102
		private bool isReturnValue;

		// Token: 0x04000C1F RID: 3103
		private bool overrideIsNullable;
	}
}
