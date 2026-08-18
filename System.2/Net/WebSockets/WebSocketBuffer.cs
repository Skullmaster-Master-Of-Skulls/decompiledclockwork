using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Net.WebSockets
{
	// Token: 0x02000231 RID: 561
	internal class WebSocketBuffer : IDisposable
	{
		// Token: 0x060014F8 RID: 5368 RVA: 0x0006DAEC File Offset: 0x0006BCEC
		private WebSocketBuffer(ArraySegment<byte> internalBuffer, int receiveBufferSize, int sendBufferSize)
		{
			this.m_ReceiveBufferSize = receiveBufferSize;
			this.m_SendBufferSize = sendBufferSize;
			this.m_InternalBuffer = internalBuffer;
			this.m_GCHandle = GCHandle.Alloc(internalBuffer.Array, GCHandleType.Pinned);
			int num = this.m_ReceiveBufferSize + this.m_SendBufferSize + 144;
			this.m_StartAddress = Marshal.UnsafeAddrOfPinnedArrayElement(internalBuffer.Array, internalBuffer.Offset).ToInt64();
			this.m_EndAddress = this.m_StartAddress + (long)num;
			this.m_NativeBuffer = new ArraySegment<byte>(internalBuffer.Array, internalBuffer.Offset, num);
			this.m_PayloadBuffer = new ArraySegment<byte>(internalBuffer.Array, this.m_NativeBuffer.Offset + this.m_NativeBuffer.Count, this.m_ReceiveBufferSize);
			this.m_PropertyBuffer = new ArraySegment<byte>(internalBuffer.Array, this.m_PayloadBuffer.Offset + this.m_PayloadBuffer.Count, WebSocketBuffer.s_PropertyBufferSize);
			this.m_SendBufferState = 0;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0006DBF4 File Offset: 0x0006BDF4
		internal static int SizeOfUInt
		{
			get
			{
				return WebSocketBuffer.s_SizeOfUInt;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0006DBFB File Offset: 0x0006BDFB
		public int ReceiveBufferSize
		{
			get
			{
				return this.m_ReceiveBufferSize;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0006DC03 File Offset: 0x0006BE03
		public int SendBufferSize
		{
			get
			{
				return this.m_SendBufferSize;
			}
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0006DC0B File Offset: 0x0006BE0B
		internal static WebSocketBuffer CreateClientBuffer(ArraySegment<byte> internalBuffer, int receiveBufferSize, int sendBufferSize)
		{
			return new WebSocketBuffer(internalBuffer, receiveBufferSize, WebSocketBuffer.GetNativeSendBufferSize(sendBufferSize, false));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0006DC1C File Offset: 0x0006BE1C
		internal static WebSocketBuffer CreateServerBuffer(ArraySegment<byte> internalBuffer, int receiveBufferSize)
		{
			int nativeSendBufferSize = WebSocketBuffer.GetNativeSendBufferSize(16, true);
			return new WebSocketBuffer(internalBuffer, receiveBufferSize, nativeSendBufferSize);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0006DC3A File Offset: 0x0006BE3A
		public void Dispose(WebSocketState webSocketState)
		{
			if (Interlocked.CompareExchange(ref this.m_StateWhenDisposing, (int)webSocketState, -2147483648) != -2147483648)
			{
				return;
			}
			this.CleanUp();
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0006DC5B File Offset: 0x0006BE5B
		public void Dispose()
		{
			this.Dispose(WebSocketState.None);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0006DC64 File Offset: 0x0006BE64
		internal WebSocketProtocolComponent.Property[] CreateProperties(bool useZeroMaskingKey)
		{
			this.ThrowIfDisposed();
			IntPtr intPtr = this.m_GCHandle.AddrOfPinnedObject();
			int num = this.m_PropertyBuffer.Offset;
			Marshal.WriteInt32(intPtr, num, this.m_ReceiveBufferSize);
			num += WebSocketBuffer.s_SizeOfUInt;
			Marshal.WriteInt32(intPtr, num, this.m_SendBufferSize);
			num += WebSocketBuffer.s_SizeOfUInt;
			Marshal.WriteIntPtr(intPtr, num, intPtr + this.m_InternalBuffer.Offset);
			num += IntPtr.Size;
			Marshal.WriteInt32(intPtr, num, useZeroMaskingKey ? 1 : 0);
			int num2 = useZeroMaskingKey ? 4 : 3;
			WebSocketProtocolComponent.Property[] array = new WebSocketProtocolComponent.Property[num2];
			num = this.m_PropertyBuffer.Offset;
			array[0] = new WebSocketProtocolComponent.Property
			{
				Type = WebSocketProtocolComponent.PropertyType.ReceiveBufferSize,
				PropertySize = (uint)WebSocketBuffer.s_SizeOfUInt,
				PropertyData = IntPtr.Add(intPtr, num)
			};
			num += WebSocketBuffer.s_SizeOfUInt;
			array[1] = new WebSocketProtocolComponent.Property
			{
				Type = WebSocketProtocolComponent.PropertyType.SendBufferSize,
				PropertySize = (uint)WebSocketBuffer.s_SizeOfUInt,
				PropertyData = IntPtr.Add(intPtr, num)
			};
			num += WebSocketBuffer.s_SizeOfUInt;
			array[2] = new WebSocketProtocolComponent.Property
			{
				Type = WebSocketProtocolComponent.PropertyType.AllocatedBuffer,
				PropertySize = (uint)this.m_NativeBuffer.Count,
				PropertyData = IntPtr.Add(intPtr, num)
			};
			num += IntPtr.Size;
			if (useZeroMaskingKey)
			{
				array[3] = new WebSocketProtocolComponent.Property
				{
					Type = WebSocketProtocolComponent.PropertyType.DisableMasking,
					PropertySize = (uint)WebSocketBuffer.s_SizeOfBool,
					PropertyData = IntPtr.Add(intPtr, num)
				};
			}
			return array;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0006DE04 File Offset: 0x0006C004
		internal void PinSendBuffer(ArraySegment<byte> payload, out bool bufferHasBeenPinned)
		{
			bufferHasBeenPinned = false;
			WebSocketHelpers.ValidateBuffer(payload.Array, payload.Offset, payload.Count);
			int num = Interlocked.Exchange(ref this.m_SendBufferState, 1);
			if (num != 0)
			{
				throw new AccessViolationException();
			}
			this.m_PinnedSendBuffer = payload;
			this.m_PinnedSendBufferHandle = GCHandle.Alloc(this.m_PinnedSendBuffer.Array, GCHandleType.Pinned);
			bufferHasBeenPinned = true;
			this.m_PinnedSendBufferStartAddress = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_PinnedSendBuffer.Array, this.m_PinnedSendBuffer.Offset).ToInt64();
			this.m_PinnedSendBufferEndAddress = this.m_PinnedSendBufferStartAddress + (long)this.m_PinnedSendBuffer.Count;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0006DEA7 File Offset: 0x0006C0A7
		internal IntPtr ConvertPinnedSendPayloadToNative(ArraySegment<byte> payload)
		{
			return this.ConvertPinnedSendPayloadToNative(payload.Array, payload.Offset, payload.Count);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0006DEC4 File Offset: 0x0006C0C4
		internal IntPtr ConvertPinnedSendPayloadToNative(byte[] buffer, int offset, int count)
		{
			if (!this.IsPinnedSendPayloadBuffer(buffer, offset, count))
			{
				throw new AccessViolationException();
			}
			return new IntPtr(this.m_PinnedSendBufferStartAddress + (long)offset - (long)this.m_PinnedSendBuffer.Offset);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0006DEF4 File Offset: 0x0006C0F4
		internal ArraySegment<byte> ConvertPinnedSendPayloadFromNative(WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType)
		{
			if (!this.IsPinnedSendPayloadBuffer(buffer, bufferType))
			{
				throw new AccessViolationException();
			}
			IntPtr intPtr;
			uint count;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out count);
			int num = (int)(intPtr.ToInt64() - this.m_PinnedSendBufferStartAddress);
			return new ArraySegment<byte>(this.m_PinnedSendBuffer.Array, this.m_PinnedSendBuffer.Offset + num, (int)count);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0006DF4C File Offset: 0x0006C14C
		private bool IsPinnedSendPayloadBuffer(byte[] buffer, int offset, int count)
		{
			return this.m_SendBufferState == 1 && (buffer == this.m_PinnedSendBuffer.Array && offset >= this.m_PinnedSendBuffer.Offset) && offset + count <= this.m_PinnedSendBuffer.Offset + this.m_PinnedSendBuffer.Count;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0006DFA4 File Offset: 0x0006C1A4
		internal bool IsPinnedSendPayloadBuffer(WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType)
		{
			if (this.m_SendBufferState != 1)
			{
				return false;
			}
			IntPtr intPtr;
			uint num;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out num);
			long num2 = intPtr.ToInt64();
			long num3 = num2 + (long)((ulong)num);
			return num2 >= this.m_PinnedSendBufferStartAddress && num3 >= this.m_PinnedSendBufferStartAddress && num2 <= this.m_PinnedSendBufferEndAddress && num3 <= this.m_PinnedSendBufferEndAddress;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0006E000 File Offset: 0x0006C200
		internal void ReleasePinnedSendBuffer()
		{
			int num = Interlocked.Exchange(ref this.m_SendBufferState, 0);
			if (num != 1)
			{
				return;
			}
			if (this.m_PinnedSendBufferHandle.IsAllocated)
			{
				this.m_PinnedSendBufferHandle.Free();
			}
			this.m_PinnedSendBuffer = WebSocketHelpers.EmptyPayload;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0006E044 File Offset: 0x0006C244
		internal void BufferPayload(ArraySegment<byte> payload, int unconsumedDataOffset, WebSocketMessageType messageType, bool endOfMessage)
		{
			this.ThrowIfDisposed();
			int count = payload.Count - unconsumedDataOffset;
			Buffer.BlockCopy(payload.Array, payload.Offset + unconsumedDataOffset, this.m_PayloadBuffer.Array, this.m_PayloadBuffer.Offset, count);
			this.m_BufferedPayloadReceiveResult = new WebSocketReceiveResult(count, messageType, endOfMessage);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0006E0A4 File Offset: 0x0006C2A4
		internal bool ReceiveFromBufferedPayload(ArraySegment<byte> buffer, out WebSocketReceiveResult receiveResult)
		{
			this.ThrowIfDisposed();
			int num = Math.Min(buffer.Count, this.m_BufferedPayloadReceiveResult.Count);
			receiveResult = this.m_BufferedPayloadReceiveResult.Copy(num);
			Buffer.BlockCopy(this.m_PayloadBuffer.Array, this.m_PayloadBuffer.Offset + this.m_PayloadOffset, buffer.Array, buffer.Offset, num);
			bool result;
			if (this.m_BufferedPayloadReceiveResult.Count == 0)
			{
				this.m_PayloadOffset = 0;
				this.m_BufferedPayloadReceiveResult = null;
				result = false;
			}
			else
			{
				this.m_PayloadOffset += num;
				result = true;
			}
			return result;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0006E154 File Offset: 0x0006C354
		internal ArraySegment<byte> ConvertNativeBuffer(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType)
		{
			this.ThrowIfDisposed();
			IntPtr intPtr;
			uint num;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out num);
			if (intPtr == IntPtr.Zero)
			{
				return WebSocketHelpers.EmptyPayload;
			}
			if (this.IsNativeBuffer(intPtr, num))
			{
				return new ArraySegment<byte>(this.m_InternalBuffer.Array, this.GetOffset(intPtr), (int)num);
			}
			throw new AccessViolationException();
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0006E1B0 File Offset: 0x0006C3B0
		internal void ConvertCloseBuffer(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.Buffer buffer, out WebSocketCloseStatus closeStatus, out string reason)
		{
			this.ThrowIfDisposed();
			closeStatus = (WebSocketCloseStatus)buffer.CloseStatus.CloseStatus;
			IntPtr intPtr;
			uint num;
			WebSocketBuffer.UnwrapWebSocketBuffer(buffer, (WebSocketProtocolComponent.BufferType)2147483652U, out intPtr, out num);
			if (intPtr == IntPtr.Zero)
			{
				reason = null;
				return;
			}
			if (this.IsNativeBuffer(intPtr, num))
			{
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(this.m_InternalBuffer.Array, this.GetOffset(intPtr), (int)num);
				reason = Encoding.UTF8.GetString(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				return;
			}
			throw new AccessViolationException();
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0006E244 File Offset: 0x0006C444
		internal void ValidateNativeBuffers(WebSocketProtocolComponent.Action action, WebSocketProtocolComponent.BufferType bufferType, WebSocketProtocolComponent.Buffer[] dataBuffers, uint dataBufferCount)
		{
			this.ThrowIfDisposed();
			if ((ulong)dataBufferCount > (ulong)((long)dataBuffers.Length))
			{
				throw new AccessViolationException();
			}
			int num = dataBuffers.Length;
			bool flag = action == WebSocketProtocolComponent.Action.IndicateSendComplete || action == WebSocketProtocolComponent.Action.SendToNetwork;
			if (flag)
			{
				num = (int)dataBufferCount;
			}
			bool flag2 = false;
			for (int i = 0; i < num; i++)
			{
				WebSocketProtocolComponent.Buffer buffer = dataBuffers[i];
				IntPtr intPtr;
				uint num2;
				WebSocketBuffer.UnwrapWebSocketBuffer(buffer, bufferType, out intPtr, out num2);
				if (!(intPtr == IntPtr.Zero))
				{
					flag2 = true;
					bool flag3 = this.IsPinnedSendPayloadBuffer(buffer, bufferType);
					if ((ulong)num2 > (ulong)((long)this.GetMaxBufferSize()) && (!flag || !flag3))
					{
						throw new AccessViolationException();
					}
					if (!flag3 && !this.IsNativeBuffer(intPtr, num2))
					{
						throw new AccessViolationException();
					}
				}
			}
			if (flag2 || action == WebSocketProtocolComponent.Action.NoAction || action != WebSocketProtocolComponent.Action.IndicateReceiveComplete)
			{
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0006E2F7 File Offset: 0x0006C4F7
		private static int GetNativeSendBufferSize(int sendBufferSize, bool isServerBuffer)
		{
			if (!isServerBuffer)
			{
				return sendBufferSize;
			}
			return 16;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0006E300 File Offset: 0x0006C500
		internal static void UnwrapWebSocketBuffer(WebSocketProtocolComponent.Buffer buffer, WebSocketProtocolComponent.BufferType bufferType, out IntPtr bufferData, out uint bufferLength)
		{
			bufferData = IntPtr.Zero;
			bufferLength = 0U;
			if (bufferType != WebSocketProtocolComponent.BufferType.None)
			{
				switch (bufferType)
				{
				case (WebSocketProtocolComponent.BufferType)2147483648U:
				case (WebSocketProtocolComponent.BufferType)2147483649U:
				case (WebSocketProtocolComponent.BufferType)2147483650U:
				case (WebSocketProtocolComponent.BufferType)2147483651U:
				case (WebSocketProtocolComponent.BufferType)2147483653U:
				case (WebSocketProtocolComponent.BufferType)2147483654U:
					break;
				case (WebSocketProtocolComponent.BufferType)2147483652U:
					bufferData = buffer.CloseStatus.ReasonData;
					bufferLength = buffer.CloseStatus.ReasonLength;
					return;
				default:
					return;
				}
			}
			bufferData = buffer.Data.BufferData;
			bufferLength = buffer.Data.BufferLength;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0006E378 File Offset: 0x0006C578
		private void ThrowIfDisposed()
		{
			int stateWhenDisposing = this.m_StateWhenDisposing;
			if (stateWhenDisposing == -2147483648)
			{
				return;
			}
			if (stateWhenDisposing - 5 > 1)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			throw new WebSocketException(WebSocketError.InvalidState, SR.GetString("net_WebSockets_InvalidState_ClosedOrAborted", new object[]
			{
				typeof(WebSocketBase),
				this.m_StateWhenDisposing
			}));
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0006E3E0 File Offset: 0x0006C5E0
		[Conditional("DEBUG")]
		[Conditional("CONTRACTS_FULL")]
		private void ValidateBufferedPayload()
		{
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0006E3E4 File Offset: 0x0006C5E4
		private int GetOffset(IntPtr pBuffer)
		{
			return (int)(pBuffer.ToInt64() - this.m_StartAddress + (long)this.m_InternalBuffer.Offset);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0006E412 File Offset: 0x0006C612
		private int GetMaxBufferSize()
		{
			return Math.Max(this.m_ReceiveBufferSize, this.m_SendBufferSize);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0006E428 File Offset: 0x0006C628
		internal bool IsInternalBuffer(byte[] buffer, int offset, int count)
		{
			return buffer == this.m_NativeBuffer.Array && offset >= this.m_NativeBuffer.Offset && offset + count <= this.m_NativeBuffer.Offset + this.m_NativeBuffer.Count;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0006E480 File Offset: 0x0006C680
		internal IntPtr ToIntPtr(int offset)
		{
			return new IntPtr(this.m_StartAddress + (long)offset - (long)this.m_InternalBuffer.Offset);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0006E4AC File Offset: 0x0006C6AC
		private bool IsNativeBuffer(IntPtr pBuffer, uint bufferSize)
		{
			long num = pBuffer.ToInt64();
			long num2 = (long)((ulong)bufferSize + (ulong)num);
			return num >= this.m_StartAddress && num <= this.m_EndAddress && num2 >= this.m_StartAddress && num2 <= this.m_EndAddress;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0006E4F0 File Offset: 0x0006C6F0
		private void CleanUp()
		{
			if (this.m_GCHandle.IsAllocated)
			{
				this.m_GCHandle.Free();
			}
			this.ReleasePinnedSendBuffer();
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0006E524 File Offset: 0x0006C724
		internal static ArraySegment<byte> CreateInternalBufferArraySegment(int receiveBufferSize, int sendBufferSize, bool isServerBuffer)
		{
			int internalBufferSize = WebSocketBuffer.GetInternalBufferSize(receiveBufferSize, sendBufferSize, isServerBuffer);
			return new ArraySegment<byte>(new byte[internalBufferSize]);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0006E548 File Offset: 0x0006C748
		internal static void Validate(int count, int receiveBufferSize, int sendBufferSize, bool isServerBuffer)
		{
			int internalBufferSize = WebSocketBuffer.GetInternalBufferSize(receiveBufferSize, sendBufferSize, isServerBuffer);
			if (count < internalBufferSize)
			{
				throw new ArgumentOutOfRangeException("internalBuffer", SR.GetString("net_WebSockets_ArgumentOutOfRange_InternalBuffer", new object[]
				{
					internalBufferSize
				}));
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0006E588 File Offset: 0x0006C788
		private static int GetInternalBufferSize(int receiveBufferSize, int sendBufferSize, bool isServerBuffer)
		{
			int nativeSendBufferSize = WebSocketBuffer.GetNativeSendBufferSize(sendBufferSize, isServerBuffer);
			return 2 * receiveBufferSize + nativeSendBufferSize + 144 + WebSocketBuffer.s_PropertyBufferSize;
		}

		// Token: 0x04001675 RID: 5749
		private const int NativeOverheadBufferSize = 144;

		// Token: 0x04001676 RID: 5750
		internal const int MinSendBufferSize = 16;

		// Token: 0x04001677 RID: 5751
		internal const int MinReceiveBufferSize = 256;

		// Token: 0x04001678 RID: 5752
		internal const int MaxBufferSize = 65536;

		// Token: 0x04001679 RID: 5753
		private static readonly int s_SizeOfUInt = Marshal.SizeOf(typeof(uint));

		// Token: 0x0400167A RID: 5754
		private static readonly int s_SizeOfBool = Marshal.SizeOf(typeof(bool));

		// Token: 0x0400167B RID: 5755
		private static readonly int s_PropertyBufferSize = 2 * WebSocketBuffer.s_SizeOfUInt + WebSocketBuffer.s_SizeOfBool + IntPtr.Size;

		// Token: 0x0400167C RID: 5756
		private readonly int m_ReceiveBufferSize;

		// Token: 0x0400167D RID: 5757
		private readonly long m_StartAddress;

		// Token: 0x0400167E RID: 5758
		private readonly long m_EndAddress;

		// Token: 0x0400167F RID: 5759
		private readonly GCHandle m_GCHandle;

		// Token: 0x04001680 RID: 5760
		private readonly ArraySegment<byte> m_InternalBuffer;

		// Token: 0x04001681 RID: 5761
		private readonly ArraySegment<byte> m_NativeBuffer;

		// Token: 0x04001682 RID: 5762
		private readonly ArraySegment<byte> m_PayloadBuffer;

		// Token: 0x04001683 RID: 5763
		private readonly ArraySegment<byte> m_PropertyBuffer;

		// Token: 0x04001684 RID: 5764
		private readonly int m_SendBufferSize;

		// Token: 0x04001685 RID: 5765
		private volatile int m_PayloadOffset;

		// Token: 0x04001686 RID: 5766
		private volatile WebSocketReceiveResult m_BufferedPayloadReceiveResult;

		// Token: 0x04001687 RID: 5767
		private long m_PinnedSendBufferStartAddress;

		// Token: 0x04001688 RID: 5768
		private long m_PinnedSendBufferEndAddress;

		// Token: 0x04001689 RID: 5769
		private ArraySegment<byte> m_PinnedSendBuffer;

		// Token: 0x0400168A RID: 5770
		private GCHandle m_PinnedSendBufferHandle;

		// Token: 0x0400168B RID: 5771
		private int m_StateWhenDisposing = int.MinValue;

		// Token: 0x0400168C RID: 5772
		private int m_SendBufferState;

		// Token: 0x02000779 RID: 1913
		private static class SendBufferState
		{
			// Token: 0x040032CB RID: 13003
			public const int None = 0;

			// Token: 0x040032CC RID: 13004
			public const int SendPayloadSpecified = 1;
		}
	}
}
