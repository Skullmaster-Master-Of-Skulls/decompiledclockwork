using System;
using System.IO.Ports;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020003D4 RID: 980
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class Semaphore : WaitHandle
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x000AF532 File Offset: 0x000AD732
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public Semaphore(int initialCount, int maximumCount) : this(initialCount, maximumCount, null)
		{
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000AF540 File Offset: 0x000AD740
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Semaphore(int initialCount, int maximumCount, string name)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (maximumCount < 1)
			{
				throw new ArgumentOutOfRangeException("maximumCount", SR.GetString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (initialCount > maximumCount)
			{
				throw new ArgumentException(SR.GetString("Argument_SemaphoreInitialMaximum"));
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_WaitHandleNameTooLong"));
			}
			SafeWaitHandle safeWaitHandle = SafeNativeMethods.CreateSemaphore(null, initialCount, maximumCount, name);
			if (safeWaitHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					throw new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
					{
						name
					}));
				}
				InternalResources.WinIOError();
			}
			base.SafeWaitHandle = safeWaitHandle;
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000AF606 File Offset: 0x000AD806
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public Semaphore(int initialCount, int maximumCount, string name, out bool createdNew) : this(initialCount, maximumCount, name, out createdNew, null)
		{
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x000AF614 File Offset: 0x000AD814
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public unsafe Semaphore(int initialCount, int maximumCount, string name, out bool createdNew, SemaphoreSecurity semaphoreSecurity)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (maximumCount < 1)
			{
				throw new ArgumentOutOfRangeException("maximumCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			if (initialCount > maximumCount)
			{
				throw new ArgumentException(SR.GetString("Argument_SemaphoreInitialMaximum"));
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_WaitHandleNameTooLong"));
			}
			SafeWaitHandle safeWaitHandle;
			if (semaphoreSecurity != null)
			{
				NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = semaphoreSecurity.GetSecurityDescriptorBinaryForm();
				byte[] array;
				byte* value;
				if ((array = securityDescriptorBinaryForm) == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				security_ATTRIBUTES.lpSecurityDescriptor = new SafeLocalMemHandle((IntPtr)((void*)value), false);
				safeWaitHandle = SafeNativeMethods.CreateSemaphore(security_ATTRIBUTES, initialCount, maximumCount, name);
				array = null;
			}
			else
			{
				safeWaitHandle = SafeNativeMethods.CreateSemaphore(null, initialCount, maximumCount, name);
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeWaitHandle.IsInvalid)
			{
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					throw new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
					{
						name
					}));
				}
				InternalResources.WinIOError();
			}
			createdNew = (lastWin32Error != 183);
			base.SafeWaitHandle = safeWaitHandle;
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000AF747 File Offset: 0x000AD947
		private Semaphore(SafeWaitHandle handle)
		{
			base.SafeWaitHandle = handle;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000AF756 File Offset: 0x000AD956
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static Semaphore OpenExisting(string name)
		{
			return Semaphore.OpenExisting(name, SemaphoreRights.Modify | SemaphoreRights.Synchronize);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x000AF764 File Offset: 0x000AD964
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static Semaphore OpenExisting(string name, SemaphoreRights rights)
		{
			Semaphore result;
			switch (Semaphore.OpenExistingWorker(name, rights, out result))
			{
			case Semaphore.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case Semaphore.OpenExistingResult.PathNotFound:
				InternalResources.WinIOError(3, string.Empty);
				return result;
			case Semaphore.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
				{
					name
				}));
			default:
				return result;
			}
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x000AF7BF File Offset: 0x000AD9BF
		[__DynamicallyInvokable]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool TryOpenExisting(string name, out Semaphore result)
		{
			return Semaphore.OpenExistingWorker(name, SemaphoreRights.Modify | SemaphoreRights.Synchronize, out result) == Semaphore.OpenExistingResult.Success;
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000AF7D0 File Offset: 0x000AD9D0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static bool TryOpenExisting(string name, SemaphoreRights rights, out Semaphore result)
		{
			return Semaphore.OpenExistingWorker(name, rights, out result) == Semaphore.OpenExistingResult.Success;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000AF7E0 File Offset: 0x000AD9E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private static Semaphore.OpenExistingResult OpenExistingWorker(string name, SemaphoreRights rights, out Semaphore result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidNullEmptyArgument", new object[]
				{
					"name"
				}), "name");
			}
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_WaitHandleNameTooLong"));
			}
			result = null;
			SafeWaitHandle safeWaitHandle = SafeNativeMethods.OpenSemaphore((int)rights, false, name);
			if (safeWaitHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (2 == lastWin32Error || 123 == lastWin32Error)
				{
					return Semaphore.OpenExistingResult.NameNotFound;
				}
				if (3 == lastWin32Error)
				{
					return Semaphore.OpenExistingResult.PathNotFound;
				}
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					return Semaphore.OpenExistingResult.NameInvalid;
				}
				InternalResources.WinIOError();
			}
			result = new Semaphore(safeWaitHandle);
			return Semaphore.OpenExistingResult.Success;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000AF890 File Offset: 0x000ADA90
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[PrePrepareMethod]
		[__DynamicallyInvokable]
		public int Release()
		{
			return this.Release(1);
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000AF89C File Offset: 0x000ADA9C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public int Release(int releaseCount)
		{
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", SR.GetString("ArgumentOutOfRange_NeedNonNegNumRequired"));
			}
			int result;
			if (!SafeNativeMethods.ReleaseSemaphore(base.SafeWaitHandle, releaseCount, out result))
			{
				throw new SemaphoreFullException();
			}
			return result;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000AF8D9 File Offset: 0x000ADAD9
		public SemaphoreSecurity GetAccessControl()
		{
			return new SemaphoreSecurity(base.SafeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000AF8E8 File Offset: 0x000ADAE8
		public void SetAccessControl(SemaphoreSecurity semaphoreSecurity)
		{
			if (semaphoreSecurity == null)
			{
				throw new ArgumentNullException("semaphoreSecurity");
			}
			semaphoreSecurity.Persist(base.SafeWaitHandle);
		}

		// Token: 0x04002066 RID: 8294
		private const int MAX_PATH = 260;

		// Token: 0x0200080E RID: 2062
		private new enum OpenExistingResult
		{
			// Token: 0x0400357D RID: 13693
			Success,
			// Token: 0x0400357E RID: 13694
			NameNotFound,
			// Token: 0x0400357F RID: 13695
			PathNotFound,
			// Token: 0x04003580 RID: 13696
			NameInvalid
		}
	}
}
