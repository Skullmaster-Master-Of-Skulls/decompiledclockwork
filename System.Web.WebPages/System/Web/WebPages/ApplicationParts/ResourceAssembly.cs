using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace System.Web.WebPages.ApplicationParts
{
	// Token: 0x0200000E RID: 14
	internal class ResourceAssembly : IResourceAssembly
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public ResourceAssembly(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this._assembly = assembly;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E0C File Offset: 0x0000100C
		public string Name
		{
			get
			{
				AssemblyName assemblyName = new AssemblyName(this._assembly.FullName);
				return assemblyName.Name;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E30 File Offset: 0x00001030
		public Stream GetManifestResourceStream(string name)
		{
			return this._assembly.GetManifestResourceStream(name);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E3E File Offset: 0x0000103E
		public IEnumerable<string> GetManifestResourceNames()
		{
			return this._assembly.GetManifestResourceNames();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E4B File Offset: 0x0000104B
		public IEnumerable<Type> GetTypes()
		{
			return this._assembly.GetExportedTypes();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E58 File Offset: 0x00001058
		public override bool Equals(object obj)
		{
			ResourceAssembly resourceAssembly = obj as ResourceAssembly;
			return resourceAssembly != null && resourceAssembly._assembly.Equals(this._assembly);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E82 File Offset: 0x00001082
		public override int GetHashCode()
		{
			return this._assembly.GetHashCode();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E8F File Offset: 0x0000108F
		public override string ToString()
		{
			return this._assembly.ToString();
		}

		// Token: 0x04000019 RID: 25
		private readonly Assembly _assembly;
	}
}
