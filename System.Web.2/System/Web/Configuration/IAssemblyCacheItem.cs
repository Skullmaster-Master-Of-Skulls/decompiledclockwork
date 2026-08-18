using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Web.Configuration
{
	// Token: 0x02000702 RID: 1794
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9e3aaeb4-d1cd-11d2-bab9-00c04f8eceae")]
	[ComImport]
	internal interface IAssemblyCacheItem
	{
		// Token: 0x060056C2 RID: 22210
		void CreateStream([MarshalAs(UnmanagedType.LPWStr)] string pszName, uint dwFormat, uint dwFlags, uint dwMaxSize, out IStream ppStream);

		// Token: 0x060056C3 RID: 22211
		void IsNameEqual(IAssemblyName pName);

		// Token: 0x060056C4 RID: 22212
		void Commit(uint dwFlags);

		// Token: 0x060056C5 RID: 22213
		void MarkAssemblyVisible(uint dwFlags);
	}
}
