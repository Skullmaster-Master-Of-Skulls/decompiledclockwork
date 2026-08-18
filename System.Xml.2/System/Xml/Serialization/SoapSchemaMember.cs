using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000179 RID: 377
	public class SoapSchemaMember
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x00070539 File Offset: 0x0006E739
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x00070541 File Offset: 0x0006E741
		public XmlQualifiedName MemberType
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

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x0007054A File Offset: 0x0006E74A
		// (set) Token: 0x0600191C RID: 6428 RVA: 0x00070560 File Offset: 0x0006E760
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

		// Token: 0x04000B60 RID: 2912
		private string memberName;

		// Token: 0x04000B61 RID: 2913
		private XmlQualifiedName type = XmlQualifiedName.Empty;
	}
}
