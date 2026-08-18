using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000335 RID: 821
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class XmlSerializerAssemblyAttribute : Attribute
	{
		// Token: 0x06002826 RID: 10278 RVA: 0x000D0530 File Offset: 0x000CF530
		public XmlSerializerAssemblyAttribute() : this(null, null)
		{
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000D053A File Offset: 0x000CF53A
		public XmlSerializerAssemblyAttribute(string assemblyName) : this(assemblyName, null)
		{
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000D0544 File Offset: 0x000CF544
		public XmlSerializerAssemblyAttribute(string assemblyName, string codeBase)
		{
			this.assemblyName = assemblyName;
			this.codeBase = codeBase;
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000D055A File Offset: 0x000CF55A
		// (set) Token: 0x0600282A RID: 10282 RVA: 0x000D0562 File Offset: 0x000CF562
		public string CodeBase
		{
			get
			{
				return this.codeBase;
			}
			set
			{
				this.codeBase = value;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x000D056B File Offset: 0x000CF56B
		// (set) Token: 0x0600282C RID: 10284 RVA: 0x000D0573 File Offset: 0x000CF573
		public string AssemblyName
		{
			get
			{
				return this.assemblyName;
			}
			set
			{
				this.assemblyName = value;
			}
		}

		// Token: 0x04001671 RID: 5745
		private string assemblyName;

		// Token: 0x04001672 RID: 5746
		private string codeBase;
	}
}
