using System;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200001F RID: 31
	internal class ReferencedAssembly
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00004BB1 File Offset: 0x00002DB1
		public ReferencedAssembly()
		{
			this.m_AssemblyName = string.Empty;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004BC4 File Offset: 0x00002DC4
		public ReferencedAssembly(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			this.m_AssemblyName = assemblyName;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004BE1 File Offset: 0x00002DE1
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00004BE9 File Offset: 0x00002DE9
		[XmlAttribute]
		public string AssemblyName
		{
			get
			{
				return this.m_AssemblyName;
			}
			set
			{
				this.m_AssemblyName = value;
			}
		}

		// Token: 0x04000062 RID: 98
		private string m_AssemblyName;
	}
}
