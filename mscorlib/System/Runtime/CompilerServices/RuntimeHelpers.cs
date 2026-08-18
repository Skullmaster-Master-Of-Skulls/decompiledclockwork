using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005DB RID: 1499
	public static class RuntimeHelpers
	{
		// Token: 0x060037BB RID: 14267
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitializeArray(Array array, RuntimeFieldHandle fldHandle);

		// Token: 0x060037BC RID: 14268
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectValue(object obj);

		// Token: 0x060037BD RID: 14269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunClassConstructor(IntPtr type);

		// Token: 0x060037BE RID: 14270 RVA: 0x000BBA96 File Offset: 0x000BAA96
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			RuntimeHelpers._RunClassConstructor(type.Value);
		}

		// Token: 0x060037BF RID: 14271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _RunModuleConstructor(IntPtr module);

		// Token: 0x060037C0 RID: 14272 RVA: 0x000BBAA4 File Offset: 0x000BAAA4
		public static void RunModuleConstructor(ModuleHandle module)
		{
			RuntimeHelpers._RunModuleConstructor(new IntPtr(module.Value));
		}

		// Token: 0x060037C1 RID: 14273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _PrepareMethod(IntPtr method, RuntimeTypeHandle[] instantiation);

		// Token: 0x060037C2 RID: 14274
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void _CompileMethod(IntPtr method);

		// Token: 0x060037C3 RID: 14275 RVA: 0x000BBAB7 File Offset: 0x000BAAB7
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void PrepareMethod(RuntimeMethodHandle method)
		{
			RuntimeHelpers._PrepareMethod(method.Value, null);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000BBAC6 File Offset: 0x000BAAC6
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
		{
			RuntimeHelpers._PrepareMethod(method.Value, instantiation);
		}

		// Token: 0x060037C5 RID: 14277
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrepareDelegate(Delegate d);

		// Token: 0x060037C6 RID: 14278 RVA: 0x000BBAD5 File Offset: 0x000BAAD5
		public static int GetHashCode(object o)
		{
			return object.InternalGetHashCode(o);
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000BBADD File Offset: 0x000BAADD
		public new static bool Equals(object o1, object o2)
		{
			return object.InternalEquals(o1, o2);
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x000BBAE6 File Offset: 0x000BAAE6
		public static int OffsetToStringData
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x060037C9 RID: 14281
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProbeForSufficientStack();

		// Token: 0x060037CA RID: 14282 RVA: 0x000BBAEA File Offset: 0x000BAAEA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void PrepareConstrainedRegions()
		{
			RuntimeHelpers.ProbeForSufficientStack();
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000BBAF1 File Offset: 0x000BAAF1
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void PrepareConstrainedRegionsNoOP()
		{
		}

		// Token: 0x060037CC RID: 14284
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData);

		// Token: 0x060037CD RID: 14285 RVA: 0x000BBAF3 File Offset: 0x000BAAF3
		[PrePrepareMethod]
		internal static void ExecuteBackoutCodeHelper(object backoutCode, object userData, bool exceptionThrown)
		{
			((RuntimeHelpers.CleanupCode)backoutCode)(userData, exceptionThrown);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000BBB04 File Offset: 0x000BAB04
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal static void ExecuteCodeWithLock(object lockObject, RuntimeHelpers.TryCode code, object userState)
		{
			RuntimeHelpers.ExecuteWithLockHelper userData = new RuntimeHelpers.ExecuteWithLockHelper(lockObject, code, userState);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.s_EnterMonitor, RuntimeHelpers.s_ExitMonitor, userData);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000BBB2C File Offset: 0x000BAB2C
		private static void EnterMonitorAndTryCode(object helper)
		{
			RuntimeHelpers.ExecuteWithLockHelper executeWithLockHelper = (RuntimeHelpers.ExecuteWithLockHelper)helper;
			Monitor.ReliableEnter(executeWithLockHelper.m_lockObject, ref executeWithLockHelper.m_tookLock);
			executeWithLockHelper.m_userCode(executeWithLockHelper.m_userState);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000BBB64 File Offset: 0x000BAB64
		[PrePrepareMethod]
		private static void ExitMonitorOnBackout(object helper, bool exceptionThrown)
		{
			RuntimeHelpers.ExecuteWithLockHelper executeWithLockHelper = (RuntimeHelpers.ExecuteWithLockHelper)helper;
			if (executeWithLockHelper.m_tookLock)
			{
				Monitor.Exit(executeWithLockHelper.m_lockObject);
			}
		}

		// Token: 0x04001CE1 RID: 7393
		private static RuntimeHelpers.TryCode s_EnterMonitor = new RuntimeHelpers.TryCode(RuntimeHelpers.EnterMonitorAndTryCode);

		// Token: 0x04001CE2 RID: 7394
		private static RuntimeHelpers.CleanupCode s_ExitMonitor = new RuntimeHelpers.CleanupCode(RuntimeHelpers.ExitMonitorOnBackout);

		// Token: 0x020005DC RID: 1500
		// (Invoke) Token: 0x060037D3 RID: 14291
		public delegate void TryCode(object userData);

		// Token: 0x020005DD RID: 1501
		// (Invoke) Token: 0x060037D7 RID: 14295
		public delegate void CleanupCode(object userData, bool exceptionThrown);

		// Token: 0x020005DE RID: 1502
		private class ExecuteWithLockHelper
		{
			// Token: 0x060037DA RID: 14298 RVA: 0x000BBBAF File Offset: 0x000BABAF
			internal ExecuteWithLockHelper(object lockObject, RuntimeHelpers.TryCode userCode, object userState)
			{
				this.m_lockObject = lockObject;
				this.m_userCode = userCode;
				this.m_userState = userState;
			}

			// Token: 0x04001CE3 RID: 7395
			internal object m_lockObject;

			// Token: 0x04001CE4 RID: 7396
			internal bool m_tookLock;

			// Token: 0x04001CE5 RID: 7397
			internal RuntimeHelpers.TryCode m_userCode;

			// Token: 0x04001CE6 RID: 7398
			internal object m_userState;
		}
	}
}
