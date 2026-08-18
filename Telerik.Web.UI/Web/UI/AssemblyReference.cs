using System;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02000863 RID: 2147
	public class AssemblyReference
	{
		// Token: 0x06004F10 RID: 20240 RVA: 0x000F7E80 File Offset: 0x000F6080
		public AssemblyReference(string fullName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			if (assemblies.Any((Assembly a) => a.FullName == fullName))
			{
				this._assembly = assemblies.Single((Assembly a) => a.FullName == fullName);
				return;
			}
			this._assembly = Assembly.Load(fullName);
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x000F7EF7 File Offset: 0x000F60F7
		// (set) Token: 0x06004F12 RID: 20242 RVA: 0x000F7EFF File Offset: 0x000F60FF
		public Assembly Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		// Token: 0x040013B4 RID: 5044
		private Assembly _assembly;
	}
}
