using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System.ComponentModel.Design
{
	// Token: 0x020001B2 RID: 434
	[ComVisible(false)]
	[Guid("665f0ba5-ce72-4e87-9ba0-3c461de74d0b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IDesignTimeAssemblyLoader
	{
		// Token: 0x06000FD1 RID: 4049
		string GetTargetAssemblyPath(AssemblyName runtimeOrTargetAssemblyName, string suggestedAssemblyPath, FrameworkName targetFramework);

		// Token: 0x06000FD2 RID: 4050
		Assembly LoadRuntimeAssembly(AssemblyName targetAssemblyName);
	}
}
