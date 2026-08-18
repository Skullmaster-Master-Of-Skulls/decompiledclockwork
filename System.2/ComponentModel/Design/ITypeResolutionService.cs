using System;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x020005FB RID: 1531
	public interface ITypeResolutionService
	{
		// Token: 0x06003865 RID: 14437
		Assembly GetAssembly(AssemblyName name);

		// Token: 0x06003866 RID: 14438
		Assembly GetAssembly(AssemblyName name, bool throwOnError);

		// Token: 0x06003867 RID: 14439
		Type GetType(string name);

		// Token: 0x06003868 RID: 14440
		Type GetType(string name, bool throwOnError);

		// Token: 0x06003869 RID: 14441
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		// Token: 0x0600386A RID: 14442
		void ReferenceAssembly(AssemblyName name);

		// Token: 0x0600386B RID: 14443
		string GetPathOfAssembly(AssemblyName name);
	}
}
