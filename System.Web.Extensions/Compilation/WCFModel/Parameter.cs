using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200001D RID: 29
	internal class Parameter
	{
		// Token: 0x0600012C RID: 300 RVA: 0x000049AC File Offset: 0x00002BAC
		public Parameter()
		{
			this.m_Name = string.Empty;
			this.m_Value = string.Empty;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000049CA File Offset: 0x00002BCA
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000049D2 File Offset: 0x00002BD2
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000049DB File Offset: 0x00002BDB
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000049E3 File Offset: 0x00002BE3
		[XmlAttribute]
		public string Value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x0400005A RID: 90
		private string m_Name;

		// Token: 0x0400005B RID: 91
		private string m_Value;
	}
}
