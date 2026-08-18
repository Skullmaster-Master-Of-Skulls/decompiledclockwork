using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x0200038C RID: 908
	internal class BaseOverlappedAsyncResult : ContextAwareResult
	{
		// Token: 0x06002221 RID: 8737 RVA: 0x000A34EA File Offset: 0x000A16EA
		internal BaseOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
			this.m_UseOverlappedIO = (Socket.UseOverlappedIO || socket.UseOnlyOverlappedIO);
			if (this.m_UseOverlappedIO)
			{
				this.m_CleanupCount = 1;
				return;
			}
			this.m_CleanupCount = 2;
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000A3524 File Offset: 0x000A1724
		internal BaseOverlappedAsyncResult(Socket socket) : base(socket, null, null)
		{
			this.m_CleanupCount = 1;
			this.m_DisableOverlapped = true;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000A353D File Offset: 0x000A173D
		internal virtual object PostCompletion(int numBytes)
		{
			return numBytes;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000A3548 File Offset: 0x000A1748
		internal void SetUnmanagedStructures(object objectsToPin)
		{
			if (!this.m_DisableOverlapped)
			{
				object[] array = null;
				bool alreadyTriedCast = false;
				bool flag = false;
				if (this.m_Cache != null)
				{
					if (objectsToPin == null && this.m_Cache.PinnedObjects == null)
					{
						flag = true;
					}
					else if (this.m_Cache.PinnedObjects != null)
					{
						if (this.m_Cache.PinnedObjectsArray == null)
						{
							if (objectsToPin == this.m_Cache.PinnedObjects)
							{
								flag = true;
							}
						}
						else if (objectsToPin != null)
						{
							alreadyTriedCast = true;
							array = (objectsToPin as object[]);
							if (array != null && array.Length == 0)
							{
								array = null;
							}
							if (array != null && array.Length == this.m_Cache.PinnedObjectsArray.Length)
							{
								flag = true;
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] != this.m_Cache.PinnedObjectsArray[i])
									{
										flag = false;
										break;
									}
								}
							}
						}
					}
				}
				if (!flag && this.m_Cache != null)
				{
					this.m_Cache.Free();
					this.m_Cache = null;
				}
				Socket socket = (Socket)base.AsyncObject;
				if (this.m_UseOverlappedIO)
				{
					this.m_UnmanagedBlob = SafeOverlappedFree.Alloc(socket.SafeHandle);
					this.PinUnmanagedObjects(objectsToPin);
					this.m_OverlappedEvent = new AutoResetEvent(false);
					Marshal.WriteIntPtr(this.m_UnmanagedBlob.DangerousGetHandle(), Win32.OverlappedhEventOffset, this.m_OverlappedEvent.SafeWaitHandle.DangerousGetHandle());
					return;
				}
				socket.BindToCompletionPort();
				if (this.m_Cache == null)
				{
					if (array != null)
					{
						this.m_Cache = new OverlappedCache(new Overlapped(), array, BaseOverlappedAsyncResult.s_IOCallback);
					}
					else
					{
						this.m_Cache = new OverlappedCache(new Overlapped(), objectsToPin, BaseOverlappedAsyncResult.s_IOCallback, alreadyTriedCast);
					}
				}
				this.m_Cache.Overlapped.AsyncResult = this;
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000A36D6 File Offset: 0x000A18D6
		protected void SetupCache(ref OverlappedCache overlappedCache)
		{
			if (!this.m_UseOverlappedIO && !this.m_DisableOverlapped)
			{
				this.m_Cache = ((overlappedCache == null) ? null : Interlocked.Exchange<OverlappedCache>(ref overlappedCache, null));
				this.m_CleanupCount++;
			}
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000A370C File Offset: 0x000A190C
		protected void PinUnmanagedObjects(object objectsToPin)
		{
			if (this.m_Cache != null)
			{
				this.m_Cache.Free();
				this.m_Cache = null;
			}
			if (objectsToPin != null)
			{
				if (objectsToPin.GetType() == typeof(object[]))
				{
					object[] array = (object[])objectsToPin;
					this.m_GCHandles = new GCHandle[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != null)
						{
							this.m_GCHandles[i] = GCHandle.Alloc(array[i], GCHandleType.Pinned);
						}
					}
					return;
				}
				this.m_GCHandles = new GCHandle[1];
				this.m_GCHandles[0] = GCHandle.Alloc(objectsToPin, GCHandleType.Pinned);
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000A37AC File Offset: 0x000A19AC
		internal void ExtractCache(ref OverlappedCache overlappedCache)
		{
			if (!this.m_UseOverlappedIO && !this.m_DisableOverlapped)
			{
				OverlappedCache overlappedCache2 = (this.m_Cache == null) ? null : Interlocked.Exchange<OverlappedCache>(ref this.m_Cache, null);
				if (overlappedCache2 != null)
				{
					if (overlappedCache == null)
					{
						overlappedCache = overlappedCache2;
					}
					else
					{
						OverlappedCache overlappedCache3 = Interlocked.Exchange<OverlappedCache>(ref overlappedCache, overlappedCache2);
						if (overlappedCache3 != null)
						{
							overlappedCache3.Free();
						}
					}
				}
				this.ReleaseUnmanagedStructures();
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x000A3804 File Offset: 0x000A1A04
		private unsafe static void CompletionPortCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			Overlapped overlapped = Overlapped.Unpack(nativeOverlapped);
			BaseOverlappedAsyncResult baseOverlappedAsyncResult = (BaseOverlappedAsyncResult)overlapped.AsyncResult;
			overlapped.AsyncResult = null;
			SocketError socketError = (SocketError)errorCode;
			if (socketError != SocketError.Success && socketError != SocketError.OperationAborted)
			{
				Socket socket = baseOverlappedAsyncResult.AsyncObject as Socket;
				if (socket == null)
				{
					socketError = SocketError.NotSocket;
				}
				else if (socket.CleanedUp)
				{
					socketError = SocketError.OperationAborted;
				}
				else
				{
					try
					{
						SocketFlags socketFlags;
						if (!UnsafeNclNativeMethods.OSSOCK.WSAGetOverlappedResult(socket.SafeHandle, baseOverlappedAsyncResult.m_Cache.NativeOverlapped, out numBytes, false, out socketFlags))
						{
							socketError = (SocketError)Marshal.GetLastWin32Error();
						}
					}
					catch (ObjectDisposedException)
					{
						socketError = SocketError.OperationAborted;
					}
				}
			}
			baseOverlappedAsyncResult.ErrorCode = (int)socketError;
			object result = baseOverlappedAsyncResult.PostCompletion((int)numBytes);
			baseOverlappedAsyncResult.ReleaseUnmanagedStructures();
			baseOverlappedAsyncResult.InvokeCallback(result);
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x000A38C4 File Offset: 0x000A1AC4
		private void OverlappedCallback(object stateObject, bool Signaled)
		{
			BaseOverlappedAsyncResult baseOverlappedAsyncResult = (BaseOverlappedAsyncResult)stateObject;
			uint num = (uint)Marshal.ReadInt32(IntPtrHelper.Add(baseOverlappedAsyncResult.m_UnmanagedBlob.DangerousGetHandle(), 0));
			uint numBytes = (uint)((num != 0U) ? -1 : Marshal.ReadInt32(IntPtrHelper.Add(baseOverlappedAsyncResult.m_UnmanagedBlob.DangerousGetHandle(), Win32.OverlappedInternalHighOffset)));
			baseOverlappedAsyncResult.ErrorCode = (int)num;
			object result = baseOverlappedAsyncResult.PostCompletion((int)numBytes);
			baseOverlappedAsyncResult.ReleaseUnmanagedStructures();
			baseOverlappedAsyncResult.InvokeCallback(result);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x000A392C File Offset: 0x000A1B2C
		internal SocketError CheckAsyncCallOverlappedResult(SocketError errorCode)
		{
			if (this.m_UseOverlappedIO)
			{
				if (errorCode == SocketError.Success || errorCode == SocketError.IOPending)
				{
					ThreadPool.UnsafeRegisterWaitForSingleObject(this.m_OverlappedEvent, new WaitOrTimerCallback(this.OverlappedCallback), this, -1, true);
					return SocketError.Success;
				}
				base.ErrorCode = (int)errorCode;
				base.Result = -1;
				this.ReleaseUnmanagedStructures();
			}
			else
			{
				this.ReleaseUnmanagedStructures();
				if (errorCode == SocketError.Success || errorCode == SocketError.IOPending)
				{
					return SocketError.Success;
				}
				base.ErrorCode = (int)errorCode;
				base.Result = -1;
				if (this.m_Cache != null)
				{
					this.m_Cache.Overlapped.AsyncResult = null;
				}
				this.ReleaseUnmanagedStructures();
			}
			return errorCode;
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000A39CC File Offset: 0x000A1BCC
		internal SafeHandle OverlappedHandle
		{
			get
			{
				if (this.m_UseOverlappedIO)
				{
					if (this.m_UnmanagedBlob != null && !this.m_UnmanagedBlob.IsInvalid)
					{
						return this.m_UnmanagedBlob;
					}
					return SafeOverlappedFree.Zero;
				}
				else
				{
					if (this.m_Cache != null)
					{
						return this.m_Cache.NativeOverlapped;
					}
					return SafeNativeOverlapped.Zero;
				}
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000A3A1C File Offset: 0x000A1C1C
		private void ReleaseUnmanagedStructures()
		{
			if (Interlocked.Decrement(ref this.m_CleanupCount) == 0)
			{
				this.ForceReleaseUnmanagedStructures();
			}
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000A3A31 File Offset: 0x000A1C31
		protected override void Cleanup()
		{
			base.Cleanup();
			if (this.m_CleanupCount > 0 && Interlocked.Exchange(ref this.m_CleanupCount, 0) > 0)
			{
				this.ForceReleaseUnmanagedStructures();
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000A3A58 File Offset: 0x000A1C58
		protected virtual void ForceReleaseUnmanagedStructures()
		{
			this.ReleaseGCHandles();
			GC.SuppressFinalize(this);
			if (this.m_UnmanagedBlob != null && !this.m_UnmanagedBlob.IsInvalid)
			{
				this.m_UnmanagedBlob.Close(true);
				this.m_UnmanagedBlob = null;
			}
			OverlappedCache.InterlockedFree(ref this.m_Cache);
			if (this.m_OverlappedEvent != null)
			{
				this.m_OverlappedEvent.Close();
				this.m_OverlappedEvent = null;
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000A3AC0 File Offset: 0x000A1CC0
		~BaseOverlappedAsyncResult()
		{
			this.ReleaseGCHandles();
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000A3AEC File Offset: 0x000A1CEC
		private void ReleaseGCHandles()
		{
			GCHandle[] gchandles = this.m_GCHandles;
			if (gchandles != null)
			{
				for (int i = 0; i < gchandles.Length; i++)
				{
					if (gchandles[i].IsAllocated)
					{
						gchandles[i].Free();
					}
				}
			}
		}

		// Token: 0x04001F63 RID: 8035
		private SafeOverlappedFree m_UnmanagedBlob;

		// Token: 0x04001F64 RID: 8036
		private AutoResetEvent m_OverlappedEvent;

		// Token: 0x04001F65 RID: 8037
		private int m_CleanupCount;

		// Token: 0x04001F66 RID: 8038
		private bool m_DisableOverlapped;

		// Token: 0x04001F67 RID: 8039
		private bool m_UseOverlappedIO;

		// Token: 0x04001F68 RID: 8040
		private GCHandle[] m_GCHandles;

		// Token: 0x04001F69 RID: 8041
		private OverlappedCache m_Cache;

		// Token: 0x04001F6A RID: 8042
		private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(BaseOverlappedAsyncResult.CompletionPortCallback);
	}
}
