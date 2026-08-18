using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000201 RID: 513
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseSocketAndEvent : SafeCloseSocket
	{
		// Token: 0x06001356 RID: 4950 RVA: 0x00065EF8 File Offset: 0x000640F8
		internal SafeCloseSocketAndEvent()
		{
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00065F00 File Offset: 0x00064100
		protected override bool ReleaseHandle()
		{
			bool result = base.ReleaseHandle();
			this.DeleteEvent();
			return result;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00065F1C File Offset: 0x0006411C
		internal static SafeCloseSocketAndEvent CreateWSASocketWithEvent(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, bool autoReset, bool signaled)
		{
			SafeCloseSocketAndEvent safeCloseSocketAndEvent = new SafeCloseSocketAndEvent();
			SafeCloseSocket.CreateSocket(SafeCloseSocket.InnerSafeCloseSocket.CreateWSASocket(addressFamily, socketType, protocolType), safeCloseSocketAndEvent);
			if (safeCloseSocketAndEvent.IsInvalid)
			{
				throw new SocketException();
			}
			safeCloseSocketAndEvent.waitHandle = new AutoResetEvent(false);
			SafeCloseSocketAndEvent.CompleteInitialization(safeCloseSocketAndEvent);
			return safeCloseSocketAndEvent;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00065F60 File Offset: 0x00064160
		internal static void CompleteInitialization(SafeCloseSocketAndEvent socketAndEventHandle)
		{
			SafeWaitHandle safeWaitHandle = socketAndEventHandle.waitHandle.SafeWaitHandle;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeWaitHandle.DangerousAddRef(ref flag);
			}
			catch
			{
				if (flag)
				{
					safeWaitHandle.DangerousRelease();
					socketAndEventHandle.waitHandle = null;
					flag = false;
				}
			}
			finally
			{
				if (flag)
				{
					safeWaitHandle.Dispose();
				}
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00065FC8 File Offset: 0x000641C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void DeleteEvent()
		{
			try
			{
				if (this.waitHandle != null)
				{
					this.waitHandle.SafeWaitHandle.DangerousRelease();
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00066004 File Offset: 0x00064204
		internal WaitHandle GetEventHandle()
		{
			return this.waitHandle;
		}

		// Token: 0x0400155D RID: 5469
		private AutoResetEvent waitHandle;
	}
}
