using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002F3 RID: 755
	public class SoapSchemaMember
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000A81B1 File Offset: 0x000A71B1
		// (set) Token: 0x06002363 RID: 9059 RVA: 0x000A81B9 File Offset: 0x000A71B9
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

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x000A81C2 File Offset: 0x000A71C2
		// (set) Token: 0x06002365 RID: 9061 RVA: 0x000A81D8 File Offset: 0x000A71D8
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

		// Token: 0x040014F4 RID: 5364
		private string memberName;

		// Token: 0x040014F5 RID: 5365
		private XmlQualifiedName type = XmlQualifiedName.Empty;
	}
}
