using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200000E RID: 14
	internal class ContractMapping
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000037C0 File Offset: 0x000019C0
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000037C8 File Offset: 0x000019C8
		[XmlAttribute]
		public string Name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000037D1 File Offset: 0x000019D1
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000037D9 File Offset: 0x000019D9
		[XmlAttribute]
		public string TargetNamespace
		{
			get
			{
				return this.m_TargetNamespace;
			}
			set
			{
				this.m_TargetNamespace = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000037E2 File Offset: 0x000019E2
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000037EA File Offset: 0x000019EA
		[XmlAttribute]
		public string TypeName
		{
			get
			{
				return this.m_TypeName;
			}
			set
			{
				this.m_TypeName = value;
			}
		}

		// Token: 0x04000033 RID: 51
		private string m_Name;

		// Token: 0x04000034 RID: 52
		private string m_TargetNamespace;

		// Token: 0x04000035 RID: 53
		private string m_TypeName;
	}
}
