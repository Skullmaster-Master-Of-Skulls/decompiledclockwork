using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001B7 RID: 439
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class XmlSerializerAssemblyAttribute : Attribute
	{
		// Token: 0x06001E71 RID: 7793 RVA: 0x000A7881 File Offset: 0x000A5A81
		public XmlSerializerAssemblyAttribute() : this(null, null)
		{
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000A788B File Offset: 0x000A5A8B
		public XmlSerializerAssemblyAttribute(string assemblyName) : this(assemblyName, null)
		{
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000A7895 File Offset: 0x000A5A95
		public XmlSerializerAssemblyAttribute(string assemblyName, string codeBase)
		{
			this.assemblyName = assemblyName;
			this.codeBase = codeBase;
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000A78AB File Offset: 0x000A5AAB
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x000A78B3 File Offset: 0x000A5AB3
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

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x000A78BC File Offset: 0x000A5ABC
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x000A78C4 File Offset: 0x000A5AC4
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

		// Token: 0x04000CD9 RID: 3289
		private string assemblyName;

		// Token: 0x04000CDA RID: 3290
		private string codeBase;
	}
}
