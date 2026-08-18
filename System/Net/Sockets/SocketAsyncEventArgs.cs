using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Net.Sockets
{
	// Token: 0x020005C1 RID: 1473
	public class SocketAsyncEventArgs : EventArgs, IDisposable
	{
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06002DFB RID: 11771 RVA: 0x000CA446 File Offset: 0x000C9446
		// (remove) Token: 0x06002DFC RID: 11772 RVA: 0x000CA45F File Offset: 0x000C945F
		private event EventHandler<SocketAsyncEventArgs> m_Completed;

		// Token: 0x06002DFD RID: 11773 RVA: 0x000CA478 File Offset: 0x000C9478
		public SocketAsyncEventArgs()
		{
			if (!ComNetOS.IsPostWin2K)
			{
				throw new NotSupportedException(SR.GetString("WinXPRequired"));
			}
			this.m_ExecutionCallback = new ContextCallback(this.ExecutionCallback);
			this.m_SendPacketsSendSize = -1;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x000CA4B0 File Offset: 0x000C94B0
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x000CA4B8 File Offset: 0x000C94B8
		public Socket AcceptSocket
		{
			get
			{
				return this.m_AcceptSocket;
			}
			set
			{
				this.m_AcceptSocket = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x000CA4C1 File Offset: 0x000C94C1
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000CA4C9 File Offset: 0x000C94C9
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000CA4D1 File Offset: 0x000C94D1
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x000CA4D9 File Offset: 0x000C94D9
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x000CA4E4 File Offset: 0x000C94E4
		public IList<ArraySegment<byte>> BufferList
		{
			get
			{
				return this.m_BufferList;
			}
			set
			{
				this.StartConfiguring();
				try
				{
					if (value != null && this.m_Buffer != null)
					{
						throw new ArgumentException(SR.GetString("net_ambiguousbuffers", new object[]
						{
							"Buffer"
						}));
					}
					this.m_BufferList = value;
					this.m_BufferListChanged = true;
					this.CheckPinMultipleBuffers();
				}
				finally
				{
					this.Complete();
				}
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000CA550 File Offset: 0x000C9550
		public int BytesTransferred
		{
			get
			{
				return this.m_BytesTransferred;
			}
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06002E06 RID: 11782 RVA: 0x000CA558 File Offset: 0x000C9558
		// (remove) Token: 0x06002E07 RID: 11783 RVA: 0x000CA578 File Offset: 0x000C9578
		public event EventHandler<SocketAsyncEventArgs> Completed
		{
			add
			{
				this.m_Completed = (EventHandler<SocketAsyncEventArgs>)Delegate.Combine(this.m_Completed, value);
				this.m_CompletedChanged = true;
			}
			remove
			{
				this.m_Completed = (EventHandler<SocketAsyncEventArgs>)Delegate.Remove(this.m_Completed, value);
				this.m_CompletedChanged = true;
			}
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000CA598 File Offset: 0x000C9598
		protected virtual void OnCompleted(SocketAsyncEventArgs e)
		{
			EventHandler<SocketAsyncEventArgs> completed = this.m_Completed;
			if (completed != null)
			{
				completed(e.m_CurrentSocket, e);
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x000CA5BC File Offset: 0x000C95BC
		// (set) Token: 0x06002E0A RID: 11786 RVA: 0x000CA5C4 File Offset: 0x000C95C4
		public bool DisconnectReuseSocket
		{
			get
			{
				return this.m_DisconnectReuseSocket;
			}
			set
			{
				this.m_DisconnectReuseSocket = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x000CA5CD File Offset: 0x000C95CD
		public SocketAsyncOperation LastOperation
		{
			get
			{
				return this.m_CompletedOperation;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000CA5D5 File Offset: 0x000C95D5
		public IPPacketInformation ReceiveMessageFromPacketInfo
		{
			get
			{
				return this.m_ReceiveMessageFromPacketInfo;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000CA5DD File Offset: 0x000C95DD
		// (set) Token: 0x06002E0E RID: 11790 RVA: 0x000CA5E5 File Offset: 0x000C95E5
		public EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_RemoteEndPoint;
			}
			set
			{
				this.m_RemoteEndPoint = value;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x000CA5EE File Offset: 0x000C95EE
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x000CA5F8 File Offset: 0x000C95F8
		public SendPacketsElement[] SendPacketsElements
		{
			get
			{
				return this.m_SendPacketsElements;
			}
			set
			{
				this.StartConfiguring();
				try
				{
					this.m_SendPacketsElements = value;
					this.m_SendPacketsElementsInternal = null;
				}
				finally
				{
					this.Complete();
				}
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000CA634 File Offset: 0x000C9634
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x000CA63C File Offset: 0x000C963C
		public TransmitFileOptions SendPacketsFlags
		{
			get
			{
				return this.m_SendPacketsFlags;
			}
			set
			{
				this.m_SendPacketsFlags = value;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000CA645 File Offset: 0x000C9645
		// (set) Token: 0x06002E14 RID: 11796 RVA: 0x000CA64D File Offset: 0x000C964D
		public int SendPacketsSendSize
		{
			get
			{
				return this.m_SendPacketsSendSize;
			}
			set
			{
				this.m_SendPacketsSendSize = value;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x000CA656 File Offset: 0x000C9656
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x000CA65E File Offset: 0x000C965E
		public SocketError SocketError
		{
			get
			{
				return this.m_SocketError;
			}
			set
			{
				this.m_SocketError = value;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000CA667 File Offset: 0x000C9667
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x000CA66F File Offset: 0x000C966F
		public SocketFlags SocketFlags
		{
			get
			{
				return this.m_SocketFlags;
			}
			set
			{
				this.m_SocketFlags = value;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000CA678 File Offset: 0x000C9678
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x000CA680 File Offset: 0x000C9680
		public object UserToken
		{
			get
			{
				return this.m_UserToken;
			}
			set
			{
				this.m_UserToken = value;
			}
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000CA689 File Offset: 0x000C9689
		public void SetBuffer(byte[] buffer, int offset, int count)
		{
			this.SetBufferInternal(buffer, offset, count);
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000CA694 File Offset: 0x000C9694
		public void SetBuffer(int offset, int count)
		{
			this.SetBufferInternal(this.m_Buffer, offset, count);
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000CA6A4 File Offset: 0x000C96A4
		private void SetBufferInternal(byte[] buffer, int offset, int count)
		{
			this.StartConfiguring();
			try
			{
				if (buffer == null)
				{
					this.m_Buffer = null;
					this.m_Offset = 0;
					this.m_Count = 0;
				}
				else
				{
					if (this.m_BufferList != null)
					{
						throw new ArgumentException(SR.GetString("net_ambiguousbuffers", new object[]
						{
							"BufferList"
						}));
					}
					if (offset < 0 || offset > buffer.Length)
					{
						throw new ArgumentOutOfRangeException("offset");
					}
					if (count < 0 || count > buffer.Length - offset)
					{
						throw new ArgumentOutOfRangeException("count");
					}
					this.m_Buffer = buffer;
					this.m_Offset = offset;
					this.m_Count = count;
				}
				this.CheckPinSingleBuffer(true);
			}
			finally
			{
				this.Complete();
			}
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000CA75C File Offset: 0x000C975C
		internal void SetResults(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.m_SocketError = socketError;
			this.m_BytesTransferred = bytesTransferred;
			this.m_SocketFlags = flags;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000CA773 File Offset: 0x000C9773
		private void ExecutionCallback(object ignored)
		{
			this.OnCompleted(this);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000CA77C File Offset: 0x000C977C
		internal void Complete()
		{
			this.m_Operating = 0;
			if (this.m_DisposeCalled)
			{
				this.Dispose();
			}
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000CA793 File Offset: 0x000C9793
		public void Dispose()
		{
			this.m_DisposeCalled = true;
			if (Interlocked.CompareExchange(ref this.m_Operating, 2, 0) != 0)
			{
				return;
			}
			this.FreeOverlapped(false);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000CA7BC File Offset: 0x000C97BC
		~SocketAsyncEventArgs()
		{
			this.FreeOverlapped(true);
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000CA7EC File Offset: 0x000C97EC
		private void StartConfiguring()
		{
			int num = Interlocked.CompareExchange(ref this.m_Operating, -1, 0);
			if (num == 1 || num == -1)
			{
				throw new InvalidOperationException(SR.GetString("net_socketopinprogress"));
			}
			if (num == 2)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000CA834 File Offset: 0x000C9834
		internal void StartOperationCommon(Socket socket)
		{
			if (Interlocked.CompareExchange(ref this.m_Operating, 1, 0) == 0)
			{
				if (ExecutionContext.IsFlowSuppressed())
				{
					this.m_Context = null;
					this.m_ContextCopy = null;
				}
				else
				{
					if (this.m_CompletedChanged || socket != this.m_CurrentSocket)
					{
						this.m_CompletedChanged = false;
						this.m_Context = null;
						this.m_ContextCopy = null;
					}
					if (this.m_Context == null)
					{
						this.m_Context = ExecutionContext.Capture();
					}
					if (this.m_Context != null)
					{
						this.m_ContextCopy = this.m_Context.CreateCopy();
					}
				}
				this.m_CurrentSocket = socket;
				return;
			}
			if (this.m_DisposeCalled)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			throw new InvalidOperationException(SR.GetString("net_socketopinprogress"));
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000CA8EC File Offset: 0x000C98EC
		internal void StartOperationAccept()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Accept;
			this.m_AcceptAddressBufferCount = 2 * (this.m_CurrentSocket.m_RightEndPoint.Serialize().Size + 16);
			if (this.m_Buffer != null)
			{
				if (this.m_Count < this.m_AcceptAddressBufferCount)
				{
					throw new ArgumentException(SR.GetString("net_buffercounttoosmall", new object[]
					{
						"Count"
					}));
				}
			}
			else
			{
				if (this.m_AcceptBuffer == null || this.m_AcceptBuffer.Length < this.m_AcceptAddressBufferCount)
				{
					this.m_AcceptBuffer = new byte[this.m_AcceptAddressBufferCount];
				}
				this.CheckPinSingleBuffer(false);
			}
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000CA986 File Offset: 0x000C9986
		internal void StartOperationConnect()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Connect;
			this.PinSocketAddressBuffer();
			this.CheckPinNoBuffer();
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000CA99B File Offset: 0x000C999B
		internal void StartOperationDisconnect()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Disconnect;
			this.CheckPinNoBuffer();
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000CA9AA File Offset: 0x000C99AA
		internal void StartOperationReceive()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Receive;
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000CA9B3 File Offset: 0x000C99B3
		internal void StartOperationReceiveFrom()
		{
			this.m_CompletedOperation = SocketAsyncOperation.ReceiveFrom;
			this.PinSocketAddressBuffer();
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000CA9C4 File Offset: 0x000C99C4
		internal unsafe void StartOperationReceiveMessageFrom()
		{
			this.m_CompletedOperation = SocketAsyncOperation.ReceiveFrom;
			this.PinSocketAddressBuffer();
			if (this.m_WSAMessageBuffer == null)
			{
				this.m_WSAMessageBuffer = new byte[SocketAsyncEventArgs.s_WSAMsgSize];
				this.m_WSAMessageBufferGCHandle = GCHandle.Alloc(this.m_WSAMessageBuffer, GCHandleType.Pinned);
				this.m_PtrWSAMessageBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSAMessageBuffer, 0);
			}
			bool flag = this.m_CurrentSocket.AddressFamily == AddressFamily.InterNetwork;
			bool flag2 = this.m_CurrentSocket.AddressFamily == AddressFamily.InterNetworkV6;
			if (flag && (this.m_ControlBuffer == null || this.m_ControlBuffer.Length != SocketAsyncEventArgs.s_ControlDataSize))
			{
				if (this.m_ControlBufferGCHandle.IsAllocated)
				{
					this.m_ControlBufferGCHandle.Free();
				}
				this.m_ControlBuffer = new byte[SocketAsyncEventArgs.s_ControlDataSize];
			}
			else if (flag2 && (this.m_ControlBuffer == null || this.m_ControlBuffer.Length != SocketAsyncEventArgs.s_ControlDataIPv6Size))
			{
				if (this.m_ControlBufferGCHandle.IsAllocated)
				{
					this.m_ControlBufferGCHandle.Free();
				}
				this.m_ControlBuffer = new byte[SocketAsyncEventArgs.s_ControlDataIPv6Size];
			}
			if (!this.m_ControlBufferGCHandle.IsAllocated)
			{
				this.m_ControlBufferGCHandle = GCHandle.Alloc(this.m_ControlBuffer, GCHandleType.Pinned);
				this.m_PtrControlBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_ControlBuffer, 0);
			}
			if (this.m_Buffer != null)
			{
				if (this.m_WSARecvMsgWSABufferArray == null)
				{
					this.m_WSARecvMsgWSABufferArray = new WSABuffer[1];
				}
				this.m_WSARecvMsgWSABufferArray[0].Pointer = this.m_PtrSingleBuffer;
				this.m_WSARecvMsgWSABufferArray[0].Length = this.m_Count;
				this.m_WSARecvMsgWSABufferArrayGCHandle = GCHandle.Alloc(this.m_WSARecvMsgWSABufferArray, GCHandleType.Pinned);
				this.m_PtrWSARecvMsgWSABufferArray = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSARecvMsgWSABufferArray, 0);
			}
			else
			{
				this.m_WSARecvMsgWSABufferArrayGCHandle = GCHandle.Alloc(this.m_WSABufferArray, GCHandleType.Pinned);
				this.m_PtrWSARecvMsgWSABufferArray = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSABufferArray, 0);
			}
			UnsafeNclNativeMethods.OSSOCK.WSAMsg* ptr = (UnsafeNclNativeMethods.OSSOCK.WSAMsg*)((void*)this.m_PtrWSAMessageBuffer);
			ptr->socketAddress = this.m_PtrSocketAddressBuffer;
			ptr->addressLength = (uint)this.m_SocketAddress.Size;
			ptr->buffers = this.m_PtrWSARecvMsgWSABufferArray;
			if (this.m_Buffer != null)
			{
				ptr->count = 1U;
			}
			else
			{
				ptr->count = (uint)this.m_WSABufferArray.Length;
			}
			if (this.m_ControlBuffer != null)
			{
				ptr->controlBuffer.Pointer = this.m_PtrControlBuffer;
				ptr->controlBuffer.Length = this.m_ControlBuffer.Length;
			}
			ptr->flags = this.m_SocketFlags;
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000CAC10 File Offset: 0x000C9C10
		internal void StartOperationSend()
		{
			this.m_CompletedOperation = SocketAsyncOperation.Send;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000CAC1C File Offset: 0x000C9C1C
		internal void StartOperationSendPackets()
		{
			this.m_CompletedOperation = SocketAsyncOperation.SendPackets;
			if (this.m_SendPacketsElements != null)
			{
				this.m_SendPacketsElementsInternal = (SendPacketsElement[])this.m_SendPacketsElements.Clone();
			}
			this.m_SendPacketsElementsFileCount = 0;
			this.m_SendPacketsElementsBufferCount = 0;
			foreach (SendPacketsElement sendPacketsElement in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement != null)
				{
					if (sendPacketsElement.m_FilePath != null && sendPacketsElement.m_FilePath.Length > 0)
					{
						this.m_SendPacketsElementsFileCount++;
					}
					if (sendPacketsElement.m_Buffer != null)
					{
						this.m_SendPacketsElementsBufferCount++;
					}
				}
			}
			if (this.m_SendPacketsElementsFileCount > 0)
			{
				this.m_SendPacketsFileStreams = new FileStream[this.m_SendPacketsElementsFileCount];
				this.m_SendPacketsFileHandles = new SafeHandle[this.m_SendPacketsElementsFileCount];
				int num = 0;
				foreach (SendPacketsElement sendPacketsElement2 in this.m_SendPacketsElementsInternal)
				{
					if (sendPacketsElement2 != null && sendPacketsElement2.m_FilePath != null && sendPacketsElement2.m_FilePath.Length > 0)
					{
						Exception ex = null;
						try
						{
							this.m_SendPacketsFileStreams[num] = new FileStream(sendPacketsElement2.m_FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
						}
						catch (Exception ex2)
						{
							ex = ex2;
						}
						if (ex != null)
						{
							for (int k = 0; k < this.m_SendPacketsElementsFileCount; k++)
							{
								this.m_SendPacketsFileHandles[k] = null;
								if (this.m_SendPacketsFileStreams[k] != null)
								{
									this.m_SendPacketsFileStreams[k].Close();
									this.m_SendPacketsFileStreams[k] = null;
								}
							}
							throw ex;
						}
						ExceptionHelper.UnmanagedPermission.Assert();
						try
						{
							this.m_SendPacketsFileHandles[num] = this.m_SendPacketsFileStreams[num].SafeFileHandle;
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						num++;
					}
				}
			}
			this.CheckPinSendPackets();
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x000CADE8 File Offset: 0x000C9DE8
		internal void StartOperationSendTo()
		{
			this.m_CompletedOperation = SocketAsyncOperation.SendTo;
			this.PinSocketAddressBuffer();
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000CADF8 File Offset: 0x000C9DF8
		private void CheckPinNoBuffer()
		{
			if (this.m_PinState == SocketAsyncEventArgs.PinState.None)
			{
				this.SetupOverlappedSingle(true);
			}
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000CAE0C File Offset: 0x000C9E0C
		private void CheckPinSingleBuffer(bool pinUsersBuffer)
		{
			if (pinUsersBuffer)
			{
				if (this.m_Buffer == null)
				{
					if (this.m_PinState == SocketAsyncEventArgs.PinState.SingleBuffer)
					{
						this.FreeOverlapped(false);
						return;
					}
				}
				else
				{
					if (this.m_PinState != SocketAsyncEventArgs.PinState.SingleBuffer || this.m_PinnedSingleBuffer != this.m_Buffer)
					{
						this.FreeOverlapped(false);
						this.SetupOverlappedSingle(true);
						return;
					}
					if (this.m_Offset != this.m_PinnedSingleBufferOffset)
					{
						this.m_PinnedSingleBufferOffset = this.m_Offset;
						this.m_PtrSingleBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, this.m_Offset);
						this.m_WSABuffer.Pointer = this.m_PtrSingleBuffer;
					}
					if (this.m_Count != this.m_PinnedSingleBufferCount)
					{
						this.m_PinnedSingleBufferCount = this.m_Count;
						this.m_WSABuffer.Length = this.m_Count;
						return;
					}
				}
			}
			else if (this.m_PinState != SocketAsyncEventArgs.PinState.SingleAcceptBuffer || this.m_PinnedSingleBuffer != this.m_AcceptBuffer)
			{
				this.FreeOverlapped(false);
				this.SetupOverlappedSingle(false);
			}
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000CAEF4 File Offset: 0x000C9EF4
		private void CheckPinMultipleBuffers()
		{
			if (this.m_BufferList == null)
			{
				if (this.m_PinState == SocketAsyncEventArgs.PinState.MultipleBuffer)
				{
					this.FreeOverlapped(false);
					return;
				}
			}
			else if (this.m_PinState != SocketAsyncEventArgs.PinState.MultipleBuffer || this.m_BufferListChanged)
			{
				this.m_BufferListChanged = false;
				this.FreeOverlapped(false);
				try
				{
					this.SetupOverlappedMultiple();
				}
				catch (Exception)
				{
					this.FreeOverlapped(false);
					throw;
				}
			}
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000CAF5C File Offset: 0x000C9F5C
		private void CheckPinSendPackets()
		{
			if (this.m_PinState != SocketAsyncEventArgs.PinState.None)
			{
				this.FreeOverlapped(false);
			}
			this.SetupOverlappedSendPackets();
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000CAF74 File Offset: 0x000C9F74
		private void PinSocketAddressBuffer()
		{
			if (this.m_PinnedSocketAddress == this.m_SocketAddress)
			{
				return;
			}
			if (this.m_SocketAddressGCHandle.IsAllocated)
			{
				this.m_SocketAddressGCHandle.Free();
			}
			this.m_SocketAddressGCHandle = GCHandle.Alloc(this.m_SocketAddress.m_Buffer, GCHandleType.Pinned);
			this.m_SocketAddress.CopyAddressSizeIntoBuffer();
			this.m_PtrSocketAddressBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, 0);
			this.m_PtrSocketAddressBufferSize = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
			this.m_PinnedSocketAddress = this.m_SocketAddress;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000CB010 File Offset: 0x000CA010
		private void FreeOverlapped(bool checkForShutdown)
		{
			if (!checkForShutdown || !NclUtilities.HasShutdownStarted)
			{
				if (this.m_PtrNativeOverlapped != null && !this.m_PtrNativeOverlapped.IsInvalid)
				{
					this.m_PtrNativeOverlapped.Dispose();
					this.m_PtrNativeOverlapped = null;
					this.m_Overlapped = null;
					this.m_PinState = SocketAsyncEventArgs.PinState.None;
					this.m_PinnedAcceptBuffer = null;
					this.m_PinnedSingleBuffer = null;
					this.m_PinnedSingleBufferOffset = 0;
					this.m_PinnedSingleBufferCount = 0;
				}
				if (this.m_SocketAddressGCHandle.IsAllocated)
				{
					this.m_SocketAddressGCHandle.Free();
				}
				if (this.m_WSAMessageBufferGCHandle.IsAllocated)
				{
					this.m_WSAMessageBufferGCHandle.Free();
				}
				if (this.m_WSARecvMsgWSABufferArrayGCHandle.IsAllocated)
				{
					this.m_WSARecvMsgWSABufferArrayGCHandle.Free();
				}
				if (this.m_ControlBufferGCHandle.IsAllocated)
				{
					this.m_ControlBufferGCHandle.Free();
				}
			}
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000CB0DC File Offset: 0x000CA0DC
		private void SetupOverlappedSingle(bool pinSingleBuffer)
		{
			this.m_Overlapped = new Overlapped();
			if (!pinSingleBuffer)
			{
				this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_AcceptBuffer));
				this.m_PinnedAcceptBuffer = this.m_AcceptBuffer;
				this.m_PtrAcceptBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_AcceptBuffer, 0);
				this.m_PtrSingleBuffer = IntPtr.Zero;
				this.m_PinState = SocketAsyncEventArgs.PinState.SingleAcceptBuffer;
				return;
			}
			if (this.m_Buffer != null)
			{
				this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_Buffer));
				this.m_PinnedSingleBuffer = this.m_Buffer;
				this.m_PinnedSingleBufferOffset = this.m_Offset;
				this.m_PinnedSingleBufferCount = this.m_Count;
				this.m_PtrSingleBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, this.m_Offset);
				this.m_PtrAcceptBuffer = IntPtr.Zero;
				this.m_WSABuffer.Pointer = this.m_PtrSingleBuffer;
				this.m_WSABuffer.Length = this.m_Count;
				this.m_PinState = SocketAsyncEventArgs.PinState.SingleBuffer;
				return;
			}
			this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), null));
			this.m_PinnedSingleBuffer = null;
			this.m_PinnedSingleBufferOffset = 0;
			this.m_PinnedSingleBufferCount = 0;
			this.m_PtrSingleBuffer = IntPtr.Zero;
			this.m_PtrAcceptBuffer = IntPtr.Zero;
			this.m_WSABuffer.Pointer = this.m_PtrSingleBuffer;
			this.m_WSABuffer.Length = this.m_Count;
			this.m_PinState = SocketAsyncEventArgs.PinState.NoBuffer;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000CB270 File Offset: 0x000CA270
		private void SetupOverlappedMultiple()
		{
			ArraySegment<byte>[] array = new ArraySegment<byte>[this.m_BufferList.Count];
			this.m_BufferList.CopyTo(array, 0);
			this.m_Overlapped = new Overlapped();
			if (this.m_ObjectsToPin == null || this.m_ObjectsToPin.Length != array.Length)
			{
				this.m_ObjectsToPin = new object[array.Length];
			}
			for (int i = 0; i < array.Length; i++)
			{
				this.m_ObjectsToPin[i] = array[i].Array;
			}
			if (this.m_WSABufferArray == null || this.m_WSABufferArray.Length != array.Length)
			{
				this.m_WSABufferArray = new WSABuffer[array.Length];
			}
			this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_ObjectsToPin));
			for (int j = 0; j < array.Length; j++)
			{
				ArraySegment<byte> segment = array[j];
				ValidationHelper.ValidateSegment(segment);
				this.m_WSABufferArray[j].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(segment.Array, segment.Offset);
				this.m_WSABufferArray[j].Length = segment.Count;
			}
			this.m_PinState = SocketAsyncEventArgs.PinState.MultipleBuffer;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000CB39C File Offset: 0x000CA39C
		private void SetupOverlappedSendPackets()
		{
			this.m_Overlapped = new Overlapped();
			this.m_SendPacketsDescriptor = new UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElement[this.m_SendPacketsElementsFileCount + this.m_SendPacketsElementsBufferCount];
			if (this.m_ObjectsToPin == null || this.m_ObjectsToPin.Length != this.m_SendPacketsElementsBufferCount + 1)
			{
				this.m_ObjectsToPin = new object[this.m_SendPacketsElementsBufferCount + 1];
			}
			this.m_ObjectsToPin[0] = this.m_SendPacketsDescriptor;
			int num = 1;
			foreach (SendPacketsElement sendPacketsElement in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement.m_Buffer != null && sendPacketsElement.m_Count > 0)
				{
					this.m_ObjectsToPin[num] = sendPacketsElement.m_Buffer;
					num++;
				}
			}
			this.m_PtrNativeOverlapped = new SafeNativeOverlapped(this.m_Overlapped.UnsafePack(new IOCompletionCallback(this.CompletionPortCallback), this.m_ObjectsToPin));
			this.m_PtrSendPacketsDescriptor = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SendPacketsDescriptor, 0);
			int num2 = 0;
			int num3 = 0;
			foreach (SendPacketsElement sendPacketsElement2 in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement2 != null)
				{
					if (sendPacketsElement2.m_Buffer != null && sendPacketsElement2.m_Count > 0)
					{
						this.m_SendPacketsDescriptor[num2].buffer = Marshal.UnsafeAddrOfPinnedArrayElement(sendPacketsElement2.m_Buffer, sendPacketsElement2.m_Offset);
						this.m_SendPacketsDescriptor[num2].length = (uint)sendPacketsElement2.m_Count;
						this.m_SendPacketsDescriptor[num2].flags = sendPacketsElement2.m_Flags;
						num2++;
					}
					else if (sendPacketsElement2.m_FilePath != null && sendPacketsElement2.m_FilePath.Length != 0)
					{
						this.m_SendPacketsDescriptor[num2].fileHandle = this.m_SendPacketsFileHandles[num3].DangerousGetHandle();
						this.m_SendPacketsDescriptor[num2].fileOffset = (long)sendPacketsElement2.m_Offset;
						this.m_SendPacketsDescriptor[num2].length = (uint)sendPacketsElement2.m_Count;
						this.m_SendPacketsDescriptor[num2].flags = sendPacketsElement2.m_Flags;
						num3++;
						num2++;
					}
				}
			}
			this.m_PinState = SocketAsyncEventArgs.PinState.SendPackets;
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000CB5B8 File Offset: 0x000CA5B8
		internal void LogBuffer(int size)
		{
			switch (this.m_PinState)
			{
			case SocketAsyncEventArgs.PinState.SingleAcceptBuffer:
				Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation + "Async)", this.m_AcceptBuffer, 0, size);
				return;
			case SocketAsyncEventArgs.PinState.SingleBuffer:
				Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation + "Async)", this.m_Buffer, this.m_Offset, size);
				return;
			case SocketAsyncEventArgs.PinState.MultipleBuffer:
				foreach (WSABuffer wsabuffer in this.m_WSABufferArray)
				{
					Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation + "Async)", wsabuffer.Pointer, Math.Min(wsabuffer.Length, size));
					if ((size -= wsabuffer.Length) <= 0)
					{
						return;
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000CB6BC File Offset: 0x000CA6BC
		internal void LogSendPacketsBuffers(int size)
		{
			foreach (SendPacketsElement sendPacketsElement in this.m_SendPacketsElementsInternal)
			{
				if (sendPacketsElement != null)
				{
					if (sendPacketsElement.m_Buffer != null && sendPacketsElement.m_Count > 0)
					{
						Logging.Dump(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation + "Async)Buffer", sendPacketsElement.m_Buffer, sendPacketsElement.m_Offset, Math.Min(sendPacketsElement.m_Count, size));
					}
					else if (sendPacketsElement.m_FilePath != null && sendPacketsElement.m_FilePath.Length != 0)
					{
						Logging.PrintInfo(Logging.Sockets, this.m_CurrentSocket, "FinishOperation(" + this.m_CompletedOperation + "Async)", "Not logging data from file: " + sendPacketsElement.m_FilePath);
					}
				}
			}
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000CB792 File Offset: 0x000CA792
		internal void UpdatePerfCounters(int size, bool sendOp)
		{
			if (sendOp)
			{
				NetworkingPerfCounters.AddBytesSent(size);
				if (this.m_CurrentSocket.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.IncrementDatagramsSent();
					return;
				}
			}
			else
			{
				NetworkingPerfCounters.AddBytesReceived(size);
				if (this.m_CurrentSocket.Transport == TransportType.Udp)
				{
					NetworkingPerfCounters.IncrementDatagramsReceived();
				}
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000CB7CA File Offset: 0x000CA7CA
		internal void FinishOperationSyncFailure(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(socketError, bytesTransferred, flags);
			this.m_CurrentSocket.UpdateStatusAfterSocketError(socketError);
			this.Complete();
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000CB7E7 File Offset: 0x000CA7E7
		internal void FinishOperationAsyncFailure(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(socketError, bytesTransferred, flags);
			this.m_CurrentSocket.UpdateStatusAfterSocketError(socketError);
			this.Complete();
			if (this.m_Context == null)
			{
				this.OnCompleted(this);
				return;
			}
			ExecutionContext.Run(this.m_ContextCopy, this.m_ExecutionCallback, null);
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000CB828 File Offset: 0x000CA828
		internal unsafe void FinishOperationSuccess(SocketError socketError, int bytesTransferred, SocketFlags flags)
		{
			this.SetResults(socketError, bytesTransferred, flags);
			SocketAddress socketAddress2;
			switch (this.m_CompletedOperation)
			{
			case SocketAsyncOperation.Accept:
			{
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogBuffer(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, false);
					}
				}
				SocketAddress socketAddress = this.m_CurrentSocket.m_RightEndPoint.Serialize();
				IntPtr intPtr;
				int num;
				IntPtr source;
				UnsafeNclNativeMethods.OSSOCK.GetAcceptExSockaddrs((this.m_PtrSingleBuffer != IntPtr.Zero) ? this.m_PtrSingleBuffer : this.m_PtrAcceptBuffer, (this.m_Count != 0) ? (this.m_Count - this.m_AcceptAddressBufferCount) : 0, this.m_AcceptAddressBufferCount / 2, this.m_AcceptAddressBufferCount / 2, out intPtr, out num, out source, out socketAddress.m_Size);
				Marshal.Copy(source, socketAddress.m_Buffer, 0, socketAddress.m_Size);
				try
				{
					IntPtr intPtr2 = this.m_CurrentSocket.SafeHandle.DangerousGetHandle();
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_AcceptSocket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateAcceptContext, ref intPtr2, Marshal.SizeOf(intPtr2));
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.OperationAborted;
				}
				if (socketError == SocketError.Success)
				{
					this.m_AcceptSocket = this.m_CurrentSocket.UpdateAcceptSocket(this.m_AcceptSocket, this.m_CurrentSocket.m_RightEndPoint.Create(socketAddress), false);
					goto IL_4E7;
				}
				this.SetResults(socketError, bytesTransferred, SocketFlags.None);
				this.m_AcceptSocket = null;
				goto IL_4E7;
			}
			case SocketAsyncOperation.Connect:
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogBuffer(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, true);
					}
				}
				try
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_CurrentSocket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateConnectContext, null, 0);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.OperationAborted;
				}
				if (socketError == SocketError.Success)
				{
					this.m_CurrentSocket.SetToConnected();
					goto IL_4E7;
				}
				goto IL_4E7;
			case SocketAsyncOperation.Disconnect:
				this.m_CurrentSocket.SetToDisconnected();
				this.m_CurrentSocket.m_RemoteEndPoint = null;
				goto IL_4E7;
			case SocketAsyncOperation.Receive:
				if (bytesTransferred <= 0)
				{
					goto IL_4E7;
				}
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, false);
					goto IL_4E7;
				}
				goto IL_4E7;
			case SocketAsyncOperation.ReceiveFrom:
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogBuffer(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, false);
					}
				}
				this.m_SocketAddress.SetSize(this.m_PtrSocketAddressBufferSize);
				socketAddress2 = this.m_RemoteEndPoint.Serialize();
				if (socketAddress2.Equals(this.m_SocketAddress))
				{
					goto IL_4E7;
				}
				try
				{
					this.m_RemoteEndPoint = this.m_RemoteEndPoint.Create(this.m_SocketAddress);
					goto IL_4E7;
				}
				catch
				{
					goto IL_4E7;
				}
				break;
			case SocketAsyncOperation.ReceiveMessageFrom:
				break;
			case SocketAsyncOperation.Send:
				if (bytesTransferred <= 0)
				{
					goto IL_4E7;
				}
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, true);
					goto IL_4E7;
				}
				goto IL_4E7;
			case SocketAsyncOperation.SendPackets:
				if (bytesTransferred > 0)
				{
					if (SocketAsyncEventArgs.s_LoggingEnabled)
					{
						this.LogSendPacketsBuffers(bytesTransferred);
					}
					if (Socket.s_PerfCountersEnabled)
					{
						this.UpdatePerfCounters(bytesTransferred, true);
					}
				}
				if (this.m_SendPacketsFileStreams != null)
				{
					for (int i = 0; i < this.m_SendPacketsElementsFileCount; i++)
					{
						this.m_SendPacketsFileHandles[i] = null;
						if (this.m_SendPacketsFileStreams[i] != null)
						{
							this.m_SendPacketsFileStreams[i].Close();
							this.m_SendPacketsFileStreams[i] = null;
						}
					}
				}
				this.m_SendPacketsFileStreams = null;
				this.m_SendPacketsFileHandles = null;
				goto IL_4E7;
			case SocketAsyncOperation.SendTo:
				if (bytesTransferred <= 0)
				{
					goto IL_4E7;
				}
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, true);
					goto IL_4E7;
				}
				goto IL_4E7;
			default:
				goto IL_4E7;
			}
			if (bytesTransferred > 0)
			{
				if (SocketAsyncEventArgs.s_LoggingEnabled)
				{
					this.LogBuffer(bytesTransferred);
				}
				if (Socket.s_PerfCountersEnabled)
				{
					this.UpdatePerfCounters(bytesTransferred, false);
				}
			}
			this.m_SocketAddress.SetSize(this.m_PtrSocketAddressBufferSize);
			socketAddress2 = this.m_RemoteEndPoint.Serialize();
			if (!socketAddress2.Equals(this.m_SocketAddress))
			{
				try
				{
					this.m_RemoteEndPoint = this.m_RemoteEndPoint.Create(this.m_SocketAddress);
				}
				catch
				{
				}
			}
			IPAddress ipaddress = null;
			UnsafeNclNativeMethods.OSSOCK.WSAMsg* ptr = (UnsafeNclNativeMethods.OSSOCK.WSAMsg*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSAMessageBuffer, 0));
			if (this.m_ControlBuffer.Length == SocketAsyncEventArgs.s_ControlDataSize)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlData controlData = (UnsafeNclNativeMethods.OSSOCK.ControlData)Marshal.PtrToStructure(ptr->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));
				if (controlData.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress((long)((ulong)controlData.address));
				}
				this.m_ReceiveMessageFromPacketInfo = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.None, (int)controlData.index);
			}
			else if (this.m_ControlBuffer.Length == SocketAsyncEventArgs.s_ControlDataIPv6Size)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6 controlDataIPv = (UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6)Marshal.PtrToStructure(ptr->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));
				if (controlDataIPv.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress(controlDataIPv.address);
				}
				this.m_ReceiveMessageFromPacketInfo = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.IPv6None, (int)controlDataIPv.index);
			}
			else
			{
				this.m_ReceiveMessageFromPacketInfo = default(IPPacketInformation);
			}
			IL_4E7:
			if (socketError != SocketError.Success)
			{
				this.SetResults(socketError, bytesTransferred, flags);
				this.m_CurrentSocket.UpdateStatusAfterSocketError(socketError);
			}
			this.Complete();
			if (this.m_ContextCopy == null)
			{
				this.OnCompleted(this);
				return;
			}
			ExecutionContext.Run(this.m_ContextCopy, this.m_ExecutionCallback, null);
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000CBD90 File Offset: 0x000CAD90
		private unsafe void CompletionPortCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			SocketFlags flags = SocketFlags.None;
			SocketError socketError = (SocketError)errorCode;
			if (socketError == SocketError.Success)
			{
				this.FinishOperationSuccess(socketError, (int)numBytes, flags);
				return;
			}
			if (socketError != SocketError.OperationAborted)
			{
				if (this.m_CurrentSocket.CleanedUp)
				{
					socketError = SocketError.OperationAborted;
				}
				else
				{
					try
					{
						UnsafeNclNativeMethods.OSSOCK.WSAGetOverlappedResult(this.m_CurrentSocket.SafeHandle, this.m_PtrNativeOverlapped, out numBytes, false, out flags);
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
					catch
					{
						socketError = SocketError.OperationAborted;
					}
				}
			}
			this.FinishOperationAsyncFailure(socketError, (int)numBytes, flags);
		}

		// Token: 0x04002B6A RID: 11114
		private const int Configuring = -1;

		// Token: 0x04002B6B RID: 11115
		private const int Free = 0;

		// Token: 0x04002B6C RID: 11116
		private const int InProgress = 1;

		// Token: 0x04002B6D RID: 11117
		private const int Disposed = 2;

		// Token: 0x04002B6E RID: 11118
		internal static readonly int s_ControlDataSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));

		// Token: 0x04002B6F RID: 11119
		internal static readonly int s_ControlDataIPv6Size = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));

		// Token: 0x04002B70 RID: 11120
		internal static readonly int s_WSAMsgSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.WSAMsg));

		// Token: 0x04002B71 RID: 11121
		internal Socket m_AcceptSocket;

		// Token: 0x04002B72 RID: 11122
		internal byte[] m_Buffer;

		// Token: 0x04002B73 RID: 11123
		internal WSABuffer m_WSABuffer;

		// Token: 0x04002B74 RID: 11124
		internal IntPtr m_PtrSingleBuffer;

		// Token: 0x04002B75 RID: 11125
		internal int m_Count;

		// Token: 0x04002B76 RID: 11126
		internal int m_Offset;

		// Token: 0x04002B77 RID: 11127
		internal IList<ArraySegment<byte>> m_BufferList;

		// Token: 0x04002B78 RID: 11128
		private bool m_BufferListChanged;

		// Token: 0x04002B79 RID: 11129
		internal WSABuffer[] m_WSABufferArray;

		// Token: 0x04002B7A RID: 11130
		private int m_BytesTransferred;

		// Token: 0x04002B7C RID: 11132
		private bool m_CompletedChanged;

		// Token: 0x04002B7D RID: 11133
		private bool m_DisconnectReuseSocket;

		// Token: 0x04002B7E RID: 11134
		private SocketAsyncOperation m_CompletedOperation;

		// Token: 0x04002B7F RID: 11135
		private IPPacketInformation m_ReceiveMessageFromPacketInfo;

		// Token: 0x04002B80 RID: 11136
		private EndPoint m_RemoteEndPoint;

		// Token: 0x04002B81 RID: 11137
		internal TransmitFileOptions m_SendPacketsFlags;

		// Token: 0x04002B82 RID: 11138
		internal int m_SendPacketsSendSize;

		// Token: 0x04002B83 RID: 11139
		internal SendPacketsElement[] m_SendPacketsElements;

		// Token: 0x04002B84 RID: 11140
		private SendPacketsElement[] m_SendPacketsElementsInternal;

		// Token: 0x04002B85 RID: 11141
		internal int m_SendPacketsElementsFileCount;

		// Token: 0x04002B86 RID: 11142
		internal int m_SendPacketsElementsBufferCount;

		// Token: 0x04002B87 RID: 11143
		private SocketError m_SocketError;

		// Token: 0x04002B88 RID: 11144
		internal SocketFlags m_SocketFlags;

		// Token: 0x04002B89 RID: 11145
		private object m_UserToken;

		// Token: 0x04002B8A RID: 11146
		internal byte[] m_AcceptBuffer;

		// Token: 0x04002B8B RID: 11147
		internal int m_AcceptAddressBufferCount;

		// Token: 0x04002B8C RID: 11148
		internal IntPtr m_PtrAcceptBuffer;

		// Token: 0x04002B8D RID: 11149
		internal SocketAddress m_SocketAddress;

		// Token: 0x04002B8E RID: 11150
		private GCHandle m_SocketAddressGCHandle;

		// Token: 0x04002B8F RID: 11151
		private SocketAddress m_PinnedSocketAddress;

		// Token: 0x04002B90 RID: 11152
		internal IntPtr m_PtrSocketAddressBuffer;

		// Token: 0x04002B91 RID: 11153
		internal IntPtr m_PtrSocketAddressBufferSize;

		// Token: 0x04002B92 RID: 11154
		private byte[] m_WSAMessageBuffer;

		// Token: 0x04002B93 RID: 11155
		private GCHandle m_WSAMessageBufferGCHandle;

		// Token: 0x04002B94 RID: 11156
		internal IntPtr m_PtrWSAMessageBuffer;

		// Token: 0x04002B95 RID: 11157
		private byte[] m_ControlBuffer;

		// Token: 0x04002B96 RID: 11158
		private GCHandle m_ControlBufferGCHandle;

		// Token: 0x04002B97 RID: 11159
		internal IntPtr m_PtrControlBuffer;

		// Token: 0x04002B98 RID: 11160
		private WSABuffer[] m_WSARecvMsgWSABufferArray;

		// Token: 0x04002B99 RID: 11161
		private GCHandle m_WSARecvMsgWSABufferArrayGCHandle;

		// Token: 0x04002B9A RID: 11162
		private IntPtr m_PtrWSARecvMsgWSABufferArray;

		// Token: 0x04002B9B RID: 11163
		internal FileStream[] m_SendPacketsFileStreams;

		// Token: 0x04002B9C RID: 11164
		internal SafeHandle[] m_SendPacketsFileHandles;

		// Token: 0x04002B9D RID: 11165
		private UnsafeNclNativeMethods.OSSOCK.TransmitPacketsElement[] m_SendPacketsDescriptor;

		// Token: 0x04002B9E RID: 11166
		internal IntPtr m_PtrSendPacketsDescriptor;

		// Token: 0x04002B9F RID: 11167
		private ExecutionContext m_Context;

		// Token: 0x04002BA0 RID: 11168
		private ExecutionContext m_ContextCopy;

		// Token: 0x04002BA1 RID: 11169
		private ContextCallback m_ExecutionCallback;

		// Token: 0x04002BA2 RID: 11170
		private Socket m_CurrentSocket;

		// Token: 0x04002BA3 RID: 11171
		private bool m_DisposeCalled;

		// Token: 0x04002BA4 RID: 11172
		private int m_Operating;

		// Token: 0x04002BA5 RID: 11173
		internal SafeNativeOverlapped m_PtrNativeOverlapped;

		// Token: 0x04002BA6 RID: 11174
		private Overlapped m_Overlapped;

		// Token: 0x04002BA7 RID: 11175
		private object[] m_ObjectsToPin;

		// Token: 0x04002BA8 RID: 11176
		private SocketAsyncEventArgs.PinState m_PinState;

		// Token: 0x04002BA9 RID: 11177
		private byte[] m_PinnedAcceptBuffer;

		// Token: 0x04002BAA RID: 11178
		private byte[] m_PinnedSingleBuffer;

		// Token: 0x04002BAB RID: 11179
		private int m_PinnedSingleBufferOffset;

		// Token: 0x04002BAC RID: 11180
		private int m_PinnedSingleBufferCount;

		// Token: 0x04002BAD RID: 11181
		private static bool s_LoggingEnabled = Logging.On;

		// Token: 0x020005C2 RID: 1474
		private enum PinState
		{
			// Token: 0x04002BAF RID: 11183
			None,
			// Token: 0x04002BB0 RID: 11184
			NoBuffer,
			// Token: 0x04002BB1 RID: 11185
			SingleAcceptBuffer,
			// Token: 0x04002BB2 RID: 11186
			SingleBuffer,
			// Token: 0x04002BB3 RID: 11187
			MultipleBuffer,
			// Token: 0x04002BB4 RID: 11188
			SendPackets
		}
	}
}
