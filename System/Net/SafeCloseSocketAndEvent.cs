using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x0200052A RID: 1322
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseSocketAndEvent : SafeCloseSocket
	{
		// Token: 0x0600288D RID: 10381 RVA: 0x000A7BBB File Offset: 0x000A6BBB
		internal SafeCloseSocketAndEvent()
		{
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000A7BC4 File Offset: 0x000A6BC4
		protected override bool ReleaseHandle()
		{
			bool result = base.ReleaseHandle();
			this.DeleteEvent();
			return result;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000A7BE0 File Offset: 0x000A6BE0
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

		// Token: 0x06002890 RID: 10384 RVA: 0x000A7C24 File Offset: 0x000A6C24
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

		// Token: 0x06002891 RID: 10385 RVA: 0x000A7C8C File Offset: 0x000A6C8C
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

		// Token: 0x06002892 RID: 10386 RVA: 0x000A7CC8 File Offset: 0x000A6CC8
		internal WaitHandle GetEventHandle()
		{
			return this.waitHandle;
		}

		// Token: 0x04002795 RID: 10133
		private AutoResetEvent waitHandle;
	}
}
