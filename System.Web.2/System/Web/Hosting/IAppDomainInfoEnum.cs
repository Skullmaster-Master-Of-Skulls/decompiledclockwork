using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007DD RID: 2013
	[Guid("F79648FB-558B-4a09-88F1-1E3BCB30E34F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAppDomainInfoEnum
	{
		// Token: 0x06006046 RID: 24646
		[return: MarshalAs(UnmanagedType.Interface)]
		IAppDomainInfo GetData();

		// Token: 0x06006047 RID: 24647
		[return: MarshalAs(UnmanagedType.I4)]
		int Count();

		// Token: 0x06006048 RID: 24648
		[return: MarshalAs(UnmanagedType.Bool)]
		bool MoveNext();

		// Token: 0x06006049 RID: 24649
		void Reset();
	}
}
