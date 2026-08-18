using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000AF RID: 175
	[ClassInterface(0)]
	[DefaultMember("FireEvent")]
	[Guid("3050F5F5-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTCAttachBehaviorClass : DispHTCAttachBehavior, HTCAttachBehavior, IHTCAttachBehavior2, IHTCAttachBehavior
	{
		// Token: 0x06000D99 RID: 3481
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern HTCAttachBehaviorClass();

		// Token: 0x06000D9A RID: 3482
		[DispId(0)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		public virtual extern void FireEvent([MarshalAs(UnmanagedType.Struct)] [In] object evt);

		// Token: 0x06000D9B RID: 3483
		[DispId(-2147417612)]
		[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
		public virtual extern void detachEvent();

		// Token: 0x06000D9C RID: 3484
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void IHTCAttachBehavior2_FireEvent([MarshalAs(UnmanagedType.Struct)] [In] object evt);

		// Token: 0x06000D9D RID: 3485
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void IHTCAttachBehavior_FireEvent([MarshalAs(UnmanagedType.IDispatch)] [In] object evt);

		// Token: 0x06000D9E RID: 3486
		[MethodImpl(MethodImplOptions.InternalCall)]
		public virtual extern void IHTCAttachBehavior_detachEvent();
	}
}
