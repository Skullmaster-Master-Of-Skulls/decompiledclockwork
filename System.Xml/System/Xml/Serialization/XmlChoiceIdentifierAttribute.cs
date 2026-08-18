using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000307 RID: 775
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false)]
	public class XmlChoiceIdentifierAttribute : Attribute
	{
		// Token: 0x06002441 RID: 9281 RVA: 0x000AAC46 File Offset: 0x000A9C46
		public XmlChoiceIdentifierAttribute()
		{
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000AAC4E File Offset: 0x000A9C4E
		public XmlChoiceIdentifierAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x000AAC5D File Offset: 0x000A9C5D
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x000AAC73 File Offset: 0x000A9C73
		public string MemberName
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04001570 RID: 5488
		private string name;
	}
}
