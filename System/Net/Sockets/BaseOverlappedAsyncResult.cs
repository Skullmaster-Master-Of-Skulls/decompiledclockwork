using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020005CD RID: 1485
	internal class BaseOverlappedAsyncResult : ContextAwareResult
	{
		// Token: 0x06002EA7 RID: 11943 RVA: 0x000CDBD5 File Offset: 0x000CCBD5
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

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000CDC0D File Offset: 0x000CCC0D
		internal BaseOverlappedAsyncResult(Socket socket) : base(socket, null, null)
		{
			this.m_CleanupCount = 1;
			this.m_DisableOverlapped = true;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000CDC26 File Offset: 0x000CCC26
		internal virtual object PostCompletion(int numBytes)
		{
			return numBytes;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000CDC30 File Offset: 0x000CCC30
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

		// Token: 0x06002EAB RID: 11947 RVA: 0x000CDDB9 File Offset: 0x000CCDB9
		protected void SetupCache(ref OverlappedCache overlappedCache)
		{
			if (!this.m_UseOverlappedIO && !this.m_DisableOverlapped)
			{
				this.m_Cache = ((overlappedCache == null) ? null : Interlocked.Exchange<OverlappedCache>(ref overlappedCache, null));
				this.m_CleanupCount++;
			}
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000CDDF0 File Offset: 0x000CCDF0
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

		// Token: 0x06002EAD RID: 11949 RVA: 0x000CDE94 File Offset: 0x000CCE94
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

		// Token: 0x06002EAE RID: 11950 RVA: 0x000CDEEC File Offset: 0x000CCEEC
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

		// Token: 0x06002EAF RID: 11951 RVA: 0x000CDFAC File Offset: 0x000CCFAC
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

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000CE014 File Offset: 0x000CD014
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

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000CE0B8 File Offset: 0x000CD0B8
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

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000CE108 File Offset: 0x000CD108
		private void ReleaseUnmanagedStructures()
		{
			if (Interlocked.Decrement(ref this.m_CleanupCount) == 0)
			{
				this.ForceReleaseUnmanagedStructures();
			}
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000CE11D File Offset: 0x000CD11D
		protected override void Cleanup()
		{
			base.Cleanup();
			if (this.m_CleanupCount > 0 && Interlocked.Exchange(ref this.m_CleanupCount, 0) > 0)
			{
				this.ForceReleaseUnmanagedStructures();
			}
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000CE144 File Offset: 0x000CD144
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

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000CE1AC File Offset: 0x000CD1AC
		~BaseOverlappedAsyncResult()
		{
			this.ReleaseGCHandles();
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000CE1D8 File Offset: 0x000CD1D8
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

		// Token: 0x04002C44 RID: 11332
		private SafeOverlappedFree m_UnmanagedBlob;

		// Token: 0x04002C45 RID: 11333
		private AutoResetEvent m_OverlappedEvent;

		// Token: 0x04002C46 RID: 11334
		private int m_CleanupCount;

		// Token: 0x04002C47 RID: 11335
		private bool m_DisableOverlapped;

		// Token: 0x04002C48 RID: 11336
		private bool m_UseOverlappedIO;

		// Token: 0x04002C49 RID: 11337
		private GCHandle[] m_GCHandles;

		// Token: 0x04002C4A RID: 11338
		private OverlappedCache m_Cache;

		// Token: 0x04002C4B RID: 11339
		private static readonly IOCompletionCallback s_IOCallback = new IOCompletionCallback(BaseOverlappedAsyncResult.CompletionPortCallback);
	}
}
