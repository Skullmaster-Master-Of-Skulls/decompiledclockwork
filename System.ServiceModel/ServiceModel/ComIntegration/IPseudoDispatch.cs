using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200022C RID: 556
	[Guid("16BFA998-CA5B-4f29-B64F-123293EB159D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IPseudoDispatch
	{
		// Token: 0x060010BA RID: 4282
		void GetIDsOfNames(uint cNames, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] rgszNames, IntPtr pDispID);

		// Token: 0x060010BB RID: 4283
		[PreserveSig]
		int Invoke(uint dispIdMember, uint cArgs, uint cNamedArgs, IntPtr rgvarg, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] uint[] rgdispidNamedArgs, IntPtr pVarResult, IntPtr pExcepInfo, out uint pArgErr);
	}
}
