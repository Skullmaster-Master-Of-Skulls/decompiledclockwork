using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000838 RID: 2104
	[ComDefaultInterface(typeof(_MethodRental))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class MethodRental : _MethodRental
	{
		// Token: 0x06004B5D RID: 19293 RVA: 0x001058FC File Offset: 0x001048FC
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void SwapMethodBody(Type cls, int methodtoken, IntPtr rgIL, int methodSize, int flags)
		{
			if (methodSize <= 0 || methodSize >= 4128768)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"), "methodSize");
			}
			if (cls == null)
			{
				throw new ArgumentNullException("cls");
			}
			if (!(cls.Module is ModuleBuilder))
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotDynamicModule"));
			}
			RuntimeType runtimeType;
			if (cls is TypeBuilder)
			{
				TypeBuilder typeBuilder = (TypeBuilder)cls;
				if (!typeBuilder.m_hasBeenCreated)
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_NotAllTypesAreBaked"), new object[]
					{
						typeBuilder.Name
					}));
				}
				runtimeType = typeBuilder.m_runtimeType;
			}
			else
			{
				runtimeType = (cls as RuntimeType);
			}
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), "cls");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (runtimeType.Assembly.m_assemblyData.m_isSynchronized)
			{
				lock (runtimeType.Assembly.m_assemblyData)
				{
					MethodRental.SwapMethodBodyHelper(runtimeType, methodtoken, rgIL, methodSize, flags, ref stackCrawlMark);
					return;
				}
			}
			MethodRental.SwapMethodBodyHelper(runtimeType, methodtoken, rgIL, methodSize, flags, ref stackCrawlMark);
		}

		// Token: 0x06004B5E RID: 19294
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SwapMethodBodyHelper(RuntimeType cls, int methodtoken, IntPtr rgIL, int methodSize, int flags, ref StackCrawlMark stackMark);

		// Token: 0x06004B5F RID: 19295 RVA: 0x00105A1C File Offset: 0x00104A1C
		private MethodRental()
		{
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00105A24 File Offset: 0x00104A24
		void _MethodRental.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x00105A2B File Offset: 0x00104A2B
		void _MethodRental.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00105A32 File Offset: 0x00104A32
		void _MethodRental.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x00105A39 File Offset: 0x00104A39
		void _MethodRental.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002683 RID: 9859
		public const int JitOnDemand = 0;

		// Token: 0x04002684 RID: 9860
		public const int JitImmediate = 1;
	}
}
