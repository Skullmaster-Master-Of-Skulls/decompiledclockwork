using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200001C RID: 28
	internal class NamespaceMapping
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000498A File Offset: 0x00002B8A
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00004992 File Offset: 0x00002B92
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000499B File Offset: 0x00002B9B
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000049A3 File Offset: 0x00002BA3
		[XmlAttribute]
		public string ClrNamespace
		{
			get
			{
				return this.m_ClrNamespace;
			}
			set
			{
				this.m_ClrNamespace = value;
			}
		}

		// Token: 0x04000058 RID: 88
		private string m_TargetNamespace;

		// Token: 0x04000059 RID: 89
		private string m_ClrNamespace;
	}
}
