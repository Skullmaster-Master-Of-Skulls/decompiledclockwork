using System;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x02000334 RID: 820
	internal sealed class TdsParserStateObject
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x002BF568 File Offset: 0x002BE968
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x002BF588 File Offset: 0x002BE988
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x002BF5A8 File Offset: 0x002BE9A8
		internal TdsParserStateObject NextPooledObject
		{
			get
			{
				return this._nextPooledObject;
			}
			set
			{
				this._nextPooledObject = value;
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x002BF5C8 File Offset: 0x002BE9C8
		internal TdsParserStateObject(TdsParser parser)
		{
			this._parser = parser;
			this.SetPacketSize(4096);
			this.IncrementPendingCallbacks();
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x002BF648 File Offset: 0x002BEA48
		internal TdsParserStateObject(TdsParser parser, SNIHandle physicalConnection, bool async)
		{
			this._parser = parser;
			this.SniContext = SniContext.Snix_GetMarsSession;
			this.SetPacketSize(this._parser._physicalStateObj._outBuff.Length);
			SNINativeMethodWrapper.ConsumerInfo myInfo = this.CreateConsumerInfo(async);
			this._sessionHandle = new SNIHandle(myInfo, "session:", physicalConnection);
			if (this._sessionHandle.Status != 0U)
			{
				parser.Errors.Add(parser.ProcessSNIError(this));
				parser.ThrowExceptionAndWarning(this);
			}
			this.IncrementPendingCallbacks();
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x002BF718 File Offset: 0x002BEB18
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x002BF738 File Offset: 0x002BEB38
		internal bool BcpLock
		{
			get
			{
				return this._bcpLock;
			}
			set
			{
				this._bcpLock = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x002BF758 File Offset: 0x002BEB58
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x002BF778 File Offset: 0x002BEB78
		internal bool HasOpenResult
		{
			get
			{
				return this._hasOpenResult;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x002BF798 File Offset: 0x002BEB98
		internal bool IsOrphaned
		{
			get
			{
				return this._activateCount != 0 && !this._owner.IsAlive;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x002BF7C8 File Offset: 0x002BEBC8
		internal object Owner
		{
			set
			{
				this._owner.Target = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x002BF7E8 File Offset: 0x002BEBE8
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x002BF808 File Offset: 0x002BEC08
		// (set) Token: 0x06002AA4 RID: 10916 RVA: 0x002BF828 File Offset: 0x002BEC28
		internal SniContext SniContext
		{
			get
			{
				return this._sniContext;
			}
			set
			{
				this._sniContext = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x002BF848 File Offset: 0x002BEC48
		internal uint Status
		{
			get
			{
				if (this._sessionHandle != null)
				{
					return this._sessionHandle.Status;
				}
				return uint.MaxValue;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x002BF878 File Offset: 0x002BEC78
		internal bool TimeoutHasExpired
		{
			get
			{
				return TdsParserStaticMethods.TimeoutHasExpired(this._timeoutTime);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x002BF898 File Offset: 0x002BEC98
		// (set) Token: 0x06002AA8 RID: 10920 RVA: 0x002BF8D8 File Offset: 0x002BECD8
		internal long TimeoutTime
		{
			get
			{
				if (this._timeoutSeconds != 0)
				{
					this._timeoutTime = TdsParserStaticMethods.GetTimeoutSeconds(this._timeoutSeconds);
					this._timeoutSeconds = 0;
				}
				return this._timeoutTime;
			}
			set
			{
				this._timeoutSeconds = 0;
				this._timeoutTime = value;
			}
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x002BF8F8 File Offset: 0x002BECF8
		internal void Activate(object owner)
		{
			this.Owner = owner;
			Interlocked.Increment(ref this._activateCount);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x002BF918 File Offset: 0x002BED18
		internal void Cancel(int objectID)
		{
			lock (this)
			{
				if (!this._cancelled && objectID == this._allowObjectID && objectID != -1)
				{
					this._cancelled = true;
					if (this._pendingData && !this._attentionSent)
					{
						this.SendAttention();
					}
				}
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x002BF988 File Offset: 0x002BED88
		internal void CancelRequest()
		{
			this.ResetBuffer();
			this.SendAttention();
			this.Parser.ProcessPendingAck(this);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x002BF9B8 File Offset: 0x002BEDB8
		public void CheckSetResetConnectionState(uint error, CallbackType callbackType)
		{
			if (this._fResetEventOwned)
			{
				if (callbackType == CallbackType.Read && error == 0U)
				{
					this._parser._fResetConnection = false;
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
				if (error != 0U)
				{
					this._fResetConnectionSent = false;
					this._fResetEventOwned = !this._parser._resetConnectionEvent.Set();
				}
			}
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x002BFA38 File Offset: 0x002BEE38
		internal void CloseSession()
		{
			this.ResetCancelAndProcessAttention();
			this.Parser.PutSession(this);
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x002BFA58 File Offset: 0x002BEE58
		private void ResetCancelAndProcessAttention()
		{
			lock (this)
			{
				this._cancelled = false;
				this._allowObjectID = -1;
				if (this._attentionSent)
				{
					this.Parser.ProcessPendingAck(this);
				}
				this._internalTimeout = false;
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x002BFAC8 File Offset: 0x002BEEC8
		private SNINativeMethodWrapper.ConsumerInfo CreateConsumerInfo(bool async)
		{
			SNINativeMethodWrapper.ConsumerInfo consumerInfo = new SNINativeMethodWrapper.ConsumerInfo();
			consumerInfo.defaultBufferSize = this._outBuff.Length;
			if (async)
			{
				consumerInfo.readDelegate = SNILoadHandle.SingletonInstance.ReadAsyncCallbackDispatcher;
				consumerInfo.writeDelegate = SNILoadHandle.SingletonInstance.WriteAsyncCallbackDispatcher;
				this._gcHandle = GCHandle.Alloc(this, GCHandleType.Normal);
				consumerInfo.key = (IntPtr)this._gcHandle;
			}
			return consumerInfo;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x002BFB38 File Offset: 0x002BEF38
		internal void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, byte[] spnBuffer, bool flushCache, bool async, bool fParallel)
		{
			SNINativeMethodWrapper.ConsumerInfo myInfo = this.CreateConsumerInfo(async);
			long num;
			if (9223372036854775807L == timerExpire)
			{
				num = 2147483647L;
			}
			else
			{
				num = ADP.TimerRemainingMilliseconds(timerExpire);
				if (num > 2147483647L)
				{
					num = 2147483647L;
				}
				else if (0L > num)
				{
					num = 0L;
				}
			}
			this._sessionHandle = new SNIHandle(myInfo, serverName, spnBuffer, ignoreSniOpenTimeout, checked((int)num), ref instanceName, flushCache, !async, fParallel);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x002BFBA8 File Offset: 0x002BEFA8
		internal bool Deactivate()
		{
			bool result = false;
			Interlocked.Decrement(ref this._activateCount);
			this.Owner = null;
			try
			{
				TdsParserState state = this.Parser.State;
				if (state != TdsParserState.Broken && state != TdsParserState.Closed)
				{
					if (this._pendingData)
					{
						this.CleanWire();
					}
					if (this.HasOpenResult)
					{
						this.DecrementOpenResultCount();
					}
					this.ResetCancelAndProcessAttention();
					result = true;
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(e);
			}
			return result;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x002BFC38 File Offset: 0x002BF038
		internal void DecrementOpenResultCount()
		{
			if (this._executedUnderTransaction == null)
			{
				this._parser.DecrementNonTransactedOpenResultCount();
			}
			else
			{
				this._executedUnderTransaction.DecrementAndObtainOpenResultCount();
				this._executedUnderTransaction = null;
			}
			this._hasOpenResult = false;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x002BFC78 File Offset: 0x002BF078
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void DecrementPendingCallbacks(bool release)
		{
			int num = Interlocked.Decrement(ref this._pendingCallbacks);
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserStateObject.DecrementPendingCallbacks|ADV> %d#, after decrementing _pendingCallbacks: %d\n", this.ObjectID, this._pendingCallbacks);
			}
			if ((num == 0 || release) && this._gcHandle.IsAllocated)
			{
				if (Bid.AdvancedOn)
				{
					Bid.Trace("<sc.TdsParserStateObject.DecrementPendingCallbacks|ADV> %d#, FREEING HANDLE!\n", this.ObjectID);
				}
				this._gcHandle.Free();
			}
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x002BFCE8 File Offset: 0x002BF0E8
		internal void Dispose()
		{
			SafeHandle sniPacket = this._sniPacket;
			SafeHandle sessionHandle = this._sessionHandle;
			SafeHandle sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
			if (sessionHandle != null || sniPacket != null)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (sniPacket != null)
					{
						sniPacket.Dispose();
					}
					if (sniAsyncAttnPacket != null)
					{
						sniAsyncAttnPacket.Dispose();
					}
					if (sessionHandle != null)
					{
						sessionHandle.Dispose();
						this.DecrementPendingCallbacks(true);
					}
				}
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x002BFD78 File Offset: 0x002BF178
		internal int IncrementAndObtainOpenResultCount(SqlInternalTransaction transaction)
		{
			this._hasOpenResult = true;
			if (transaction == null)
			{
				return this._parser.IncrementNonTransactedOpenResultCount();
			}
			this._executedUnderTransaction = transaction;
			return transaction.IncrementAndObtainOpenResultCount();
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x002BFDA8 File Offset: 0x002BF1A8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void IncrementPendingCallbacks()
		{
			Interlocked.Increment(ref this._pendingCallbacks);
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserStateObject.IncrementPendingCallbacks|ADV> %d#, after incrementing _pendingCallbacks: %d\n", this.ObjectID, this._pendingCallbacks);
			}
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x002BFDE8 File Offset: 0x002BF1E8
		internal void SetTimeoutSeconds(int timeout)
		{
			this._timeoutSeconds = timeout;
			if (timeout == 0)
			{
				this._timeoutTime = long.MaxValue;
			}
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x002BFE18 File Offset: 0x002BF218
		internal void StartSession(int objectID)
		{
			this._allowObjectID = objectID;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x002BFE38 File Offset: 0x002BF238
		private void ThrowExceptionAndWarning()
		{
			this.Parser.ThrowExceptionAndWarning(this);
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x002BFE58 File Offset: 0x002BF258
		internal void CleanWire()
		{
			if (TdsParserState.Broken == this.Parser.State || this.Parser.State == TdsParserState.Closed)
			{
				return;
			}
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserStateObject.CleanWire|ADV> %d#\n", this.ObjectID);
			}
			while (this._messageStatus != 1 || (this._messageStatus == 1 && this._inBytesPacket != 0))
			{
				int num = this._inBytesRead - this._inBytesUsed;
				if (this._inBytesPacket >= num)
				{
					this._inBytesPacket -= num;
					this._inBytesUsed = this._inBytesRead;
					if (this._messageStatus != 1 || this._inBytesPacket > 0)
					{
						this.ReadBuffer();
					}
				}
				else
				{
					this._inBytesUsed += this._inBytesPacket;
					this._inBytesPacket = 0;
					this.ProcessHeader();
				}
			}
			this._inBytesUsed = (this._inBytesPacket = (this._inBytesRead = 0));
			this._pendingData = false;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x002BFF48 File Offset: 0x002BF348
		internal void ExecuteFlush()
		{
			lock (this)
			{
				if (this._cancelled && 1 == this._outputPacketNumber)
				{
					this.ResetBuffer();
					this._cancelled = false;
					throw SQL.OperationCancelled();
				}
				this.WritePacket(1);
				this._pendingData = true;
			}
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x002BFFB8 File Offset: 0x002BF3B8
		internal void ProcessHeader()
		{
			if (this._inBytesUsed + this._inputHeaderLen > this._inBytesRead)
			{
				int num = this._inBytesRead - this._inBytesUsed;
				int num2 = this._inputHeaderLen - num;
				if (this._bHeaderBuffer == null)
				{
					this._bHeaderBuffer = new byte[this._inputHeaderLen];
				}
				Buffer.BlockCopy(this._inBuff, this._inBytesUsed, this._bHeaderBuffer, 0, num);
				this._inBytesUsed = this._inBytesRead;
				int num3 = num;
				while (this._parser.State != TdsParserState.Broken && this._parser.State != TdsParserState.Closed)
				{
					this.ReadNetworkPacket();
					if (this._internalTimeout)
					{
						this.ThrowExceptionAndWarning();
						return;
					}
					int num4 = Math.Min(this._inBytesRead - this._inBytesUsed, num2);
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, this._bHeaderBuffer, num3, num4);
					num3 += num4;
					num2 -= num4;
					this._inBytesUsed += num4;
					if (num2 <= 0)
					{
						this._inBytesPacket = ((int)this._bHeaderBuffer[2] << 8 | (int)this._bHeaderBuffer[3]) - this._inputHeaderLen;
						this._messageStatus = this._bHeaderBuffer[1];
						return;
					}
				}
				this.ThrowExceptionAndWarning();
				return;
			}
			this._messageStatus = this._inBuff[this._inBytesUsed + 1];
			this._inBytesPacket = ((int)this._inBuff[this._inBytesUsed + 2] << 8 | (int)this._inBuff[this._inBytesUsed + 2 + 1]) - this._inputHeaderLen;
			this._inBytesUsed += this._inputHeaderLen;
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x002C0148 File Offset: 0x002BF548
		internal void ReadBuffer()
		{
			if (this._inBytesPacket > 0)
			{
				this.ReadNetworkPacket();
				return;
			}
			if (this._inBytesPacket == 0)
			{
				this.ReadNetworkPacket();
				this.ProcessHeader();
				if (this._inBytesUsed == this._inBytesRead)
				{
					this.ReadNetworkPacket();
				}
			}
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x002C0198 File Offset: 0x002BF598
		internal void ResetBuffer()
		{
			this._outBytesUsed = this._outputHeaderLen;
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x002C01B8 File Offset: 0x002BF5B8
		internal bool SetPacketSize(int size)
		{
			if (size > 32768)
			{
				throw SQL.InvalidPacketSize();
			}
			if (this._inBuff == null || this._inBuff.Length != size)
			{
				if (this._inBuff == null)
				{
					this._inBuff = new byte[size];
					this._inBytesRead = 0;
					this._inBytesUsed = 0;
				}
				else if (size != this._inBuff.Length)
				{
					if (this._inBytesRead > this._inBytesUsed)
					{
						byte[] inBuff = this._inBuff;
						this._inBuff = new byte[size];
						int num = this._inBytesRead - this._inBytesUsed;
						if (inBuff.Length < this._inBytesUsed + num || this._inBuff.Length < num)
						{
							string str = string.Concat(new object[]
							{
								Res.GetString("SQL_InvalidInternalPacketSize"),
								' ',
								inBuff.Length,
								", ",
								this._inBytesUsed,
								", ",
								num,
								", ",
								this._inBuff.Length
							});
							throw SQL.InvalidInternalPacketSize(str);
						}
						Buffer.BlockCopy(inBuff, this._inBytesUsed, this._inBuff, 0, num);
						this._inBytesRead -= this._inBytesUsed;
						this._inBytesUsed = 0;
					}
					else
					{
						this._inBuff = new byte[size];
						this._inBytesRead = 0;
						this._inBytesUsed = 0;
					}
				}
				this._outBuff = new byte[size];
				this._outBytesUsed = this._outputHeaderLen;
				return true;
			}
			return false;
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x002C0348 File Offset: 0x002BF748
		internal byte PeekByte()
		{
			byte result = this.ReadByte();
			this._inBytesPacket++;
			this._inBytesUsed--;
			return result;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x002C0388 File Offset: 0x002BF788
		public void ReadByteArray(byte[] buff, int offset, int len)
		{
			while (len > 0)
			{
				if (len <= this._inBytesPacket && this._inBytesUsed + len <= this._inBytesRead)
				{
					if (buff != null)
					{
						Buffer.BlockCopy(this._inBuff, this._inBytesUsed, buff, offset, len);
					}
					this._inBytesUsed += len;
					this._inBytesPacket -= len;
					return;
				}
				if ((len <= this._inBytesPacket && this._inBytesUsed + len > this._inBytesRead) || (len > this._inBytesPacket && this._inBytesUsed + this._inBytesPacket > this._inBytesRead))
				{
					int num = this._inBytesRead - this._inBytesUsed;
					if (buff != null)
					{
						Buffer.BlockCopy(this._inBuff, this._inBytesUsed, buff, offset, num);
					}
					offset += num;
					this._inBytesUsed += num;
					this._inBytesPacket -= num;
					len -= num;
					this.ReadBuffer();
				}
				else if (len > this._inBytesPacket && this._inBytesUsed + this._inBytesPacket <= this._inBytesRead)
				{
					if (buff != null)
					{
						Buffer.BlockCopy(this._inBuff, this._inBytesUsed, buff, offset, this._inBytesPacket);
					}
					this._inBytesUsed += this._inBytesPacket;
					offset += this._inBytesPacket;
					len -= this._inBytesPacket;
					this._inBytesPacket = 0;
					if (this._inBytesUsed == this._inBytesRead)
					{
						this.ReadBuffer();
					}
					else
					{
						this.ProcessHeader();
						if (this._inBytesUsed == this._inBytesRead)
						{
							this.ReadBuffer();
						}
					}
				}
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x002C0528 File Offset: 0x002BF928
		internal byte ReadByte()
		{
			if (this._inBytesUsed == this._inBytesRead)
			{
				this.ReadBuffer();
			}
			else if (this._inBytesPacket == 0)
			{
				this.ProcessHeader();
				if (this._inBytesUsed == this._inBytesRead)
				{
					this.ReadBuffer();
				}
			}
			this._inBytesPacket--;
			return this._inBuff[this._inBytesUsed++];
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x002C0598 File Offset: 0x002BF998
		internal char ReadChar()
		{
			byte b = this.ReadByte();
			byte b2 = this.ReadByte();
			return (char)(((int)(b2 & byte.MaxValue) << 8) + (int)(b & byte.MaxValue));
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x002C05C8 File Offset: 0x002BF9C8
		internal short ReadInt16()
		{
			byte b = this.ReadByte();
			byte b2 = this.ReadByte();
			return (short)(((int)b2 << 8) + (int)b);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x002C05F8 File Offset: 0x002BF9F8
		internal int ReadInt32()
		{
			if (this._inBytesUsed + 4 > this._inBytesRead || this._inBytesPacket < 4)
			{
				this.ReadByteArray(this._bTmp, 0, 4);
				return BitConverter.ToInt32(this._bTmp, 0);
			}
			int result = BitConverter.ToInt32(this._inBuff, this._inBytesUsed);
			this._inBytesUsed += 4;
			this._inBytesPacket -= 4;
			return result;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x002C0668 File Offset: 0x002BFA68
		internal long ReadInt64()
		{
			if (this._inBytesUsed + 8 > this._inBytesRead || this._inBytesPacket < 8)
			{
				this.ReadByteArray(this._bTmp, 0, 8);
				return BitConverter.ToInt64(this._bTmp, 0);
			}
			long result = BitConverter.ToInt64(this._inBuff, this._inBytesUsed);
			this._inBytesUsed += 8;
			this._inBytesPacket -= 8;
			return result;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x002C06D8 File Offset: 0x002BFAD8
		internal ushort ReadUInt16()
		{
			byte b = this.ReadByte();
			byte b2 = this.ReadByte();
			return (ushort)(((int)b2 << 8) + (int)b);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x002C0708 File Offset: 0x002BFB08
		internal uint ReadUInt32()
		{
			if (this._inBytesUsed + 4 > this._inBytesRead || this._inBytesPacket < 4)
			{
				this.ReadByteArray(this._bTmp, 0, 4);
				return BitConverter.ToUInt32(this._bTmp, 0);
			}
			uint result = BitConverter.ToUInt32(this._inBuff, this._inBytesUsed);
			this._inBytesUsed += 4;
			this._inBytesPacket -= 4;
			return result;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x002C0778 File Offset: 0x002BFB78
		internal float ReadSingle()
		{
			if (this._inBytesUsed + 4 > this._inBytesRead || this._inBytesPacket < 4)
			{
				this.ReadByteArray(this._bTmp, 0, 4);
				return BitConverter.ToSingle(this._bTmp, 0);
			}
			float result = BitConverter.ToSingle(this._inBuff, this._inBytesUsed);
			this._inBytesUsed += 4;
			this._inBytesPacket -= 4;
			return result;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x002C07E8 File Offset: 0x002BFBE8
		internal double ReadDouble()
		{
			if (this._inBytesUsed + 8 > this._inBytesRead || this._inBytesPacket < 8)
			{
				this.ReadByteArray(this._bTmp, 0, 8);
				return BitConverter.ToDouble(this._bTmp, 0);
			}
			double result = BitConverter.ToDouble(this._inBuff, this._inBytesUsed);
			this._inBytesUsed += 8;
			this._inBytesPacket -= 8;
			return result;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x002C0858 File Offset: 0x002BFC58
		internal string ReadString(int length)
		{
			int num = length << 1;
			int index = 0;
			byte[] bytes;
			if (this._inBytesUsed + num > this._inBytesRead || this._inBytesPacket < num)
			{
				if (this._bTmp == null || this._bTmp.Length < num)
				{
					this._bTmp = new byte[num];
				}
				this.ReadByteArray(this._bTmp, 0, num);
				bytes = this._bTmp;
			}
			else
			{
				bytes = this._inBuff;
				index = this._inBytesUsed;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
			}
			return Encoding.Unicode.GetString(bytes, index, num);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x002C08F8 File Offset: 0x002BFCF8
		internal string ReadStringWithEncoding(int length, Encoding encoding, bool isPlp)
		{
			if (encoding == null)
			{
				this._parser.ThrowUnsupportedCollationEncountered(this);
			}
			byte[] bytes = null;
			int index = 0;
			if (isPlp)
			{
				length = this.ReadPlpBytes(ref bytes, 0, int.MaxValue);
			}
			else if (this._inBytesUsed + length > this._inBytesRead || this._inBytesPacket < length)
			{
				if (this._bTmp == null || this._bTmp.Length < length)
				{
					this._bTmp = new byte[length];
				}
				this.ReadByteArray(this._bTmp, 0, length);
				bytes = this._bTmp;
			}
			else
			{
				bytes = this._inBuff;
				index = this._inBytesUsed;
				this._inBytesUsed += length;
				this._inBytesPacket -= length;
			}
			return encoding.GetString(bytes, index, length);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x002C09B8 File Offset: 0x002BFDB8
		internal ulong ReadPlpLength(bool returnPlpNullIfNull)
		{
			bool flag = false;
			if (this._longlen == 0UL)
			{
				this._longlen = (ulong)this.ReadInt64();
			}
			if (this._longlen == 18446744073709551615UL)
			{
				this._longlen = 0UL;
				this._longlenleft = 0UL;
				flag = true;
			}
			else
			{
				uint num = this.ReadUInt32();
				if (num == 0U)
				{
					this._longlenleft = 0UL;
					this._longlen = 0UL;
				}
				else
				{
					this._longlenleft = (ulong)num;
				}
			}
			if (flag && returnPlpNullIfNull)
			{
				return ulong.MaxValue;
			}
			return this._longlenleft;
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x002C0A38 File Offset: 0x002BFE38
		internal int ReadPlpBytesChunk(byte[] buff, int offset, int len)
		{
			if (this._longlenleft == 0UL)
			{
				return 0;
			}
			int num = len;
			if (this._longlenleft < (ulong)((long)len))
			{
				num = (int)this._longlenleft;
			}
			this.ReadByteArray(buff, offset, num);
			this._longlenleft -= (ulong)((long)num);
			return num;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x002C0A88 File Offset: 0x002BFE88
		internal int ReadPlpBytes(ref byte[] buff, int offst, int len)
		{
			int num = 0;
			if (this._longlen == 0UL)
			{
				if (buff == null)
				{
					buff = new byte[0];
				}
				return 0;
			}
			int i = len;
			if (buff == null && this._longlen != 18446744073709551614UL)
			{
				buff = new byte[Math.Min((int)this._longlen, len)];
			}
			if (this._longlenleft == 0UL)
			{
				this.ReadPlpLength(false);
				if (this._longlenleft == 0UL)
				{
					return 0;
				}
			}
			if (buff == null)
			{
				buff = new byte[this._longlenleft];
			}
			while (i > 0)
			{
				int num2 = (int)Math.Min(this._longlenleft, (ulong)((long)i));
				if (buff.Length < offst + num2)
				{
					byte[] array = new byte[offst + num2];
					Buffer.BlockCopy(buff, 0, array, 0, offst);
					buff = array;
				}
				num2 = this.ReadPlpBytesChunk(buff, offst, num2);
				i -= num2;
				offst += num2;
				num += num2;
				if (this._longlenleft == 0UL)
				{
					this.ReadPlpLength(false);
				}
				if (this._longlenleft == 0UL)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x002C0B78 File Offset: 0x002BFF78
		internal void ReadNetworkPacket()
		{
			this._inBytesUsed = 0;
			if (this.Parser.AsyncOn && this._cachedAsyncResult == null)
			{
				this._cachedAsyncResult = new DbAsyncResult(this, string.Empty, null, null, null);
			}
			this.ReadSni(this._cachedAsyncResult, this);
			if (this.Parser.AsyncOn)
			{
				this.ReadSniSyncOverAsync();
			}
			this.SniReadStatisticsAndTracing();
			if (Bid.AdvancedOn)
			{
				Bid.TraceBin("<sc.TdsParser.ReadNetworkPacket|INFO|ADV> Packet read", this._inBuff, (ushort)this._inBytesRead);
			}
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x002C0C08 File Offset: 0x002C0008
		internal void ReadSniSyncOverAsync()
		{
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				return;
			}
			try
			{
				if (!((IAsyncResult)this._cachedAsyncResult).AsyncWaitHandle.WaitOne(TdsParserStaticMethods.GetTimeoutMilliseconds(this.TimeoutTime), false))
				{
					bool flag = false;
					if (this._internalTimeout)
					{
						flag = true;
					}
					else
					{
						this._internalTimeout = true;
						this._parser.Errors.Add(new SqlError(-2, 0, 11, this._parser.Server, SQLMessage.Timeout(), "", 0));
						if (!this._attentionSent)
						{
							if (this._parser.State == TdsParserState.OpenLoggedIn)
							{
								this.SendAttention();
							}
							else
							{
								flag = true;
							}
							if (!((IAsyncResult)this._cachedAsyncResult).AsyncWaitHandle.WaitOne(TdsParserStaticMethods.GetTimeoutMilliseconds(this.TimeoutTime), false))
							{
								flag = true;
							}
						}
					}
					if (flag)
					{
						this._parser.State = TdsParserState.Broken;
						this._parser.Connection.BreakConnection();
						this._parser.ThrowExceptionAndWarning(this);
					}
				}
				if (this._error != null)
				{
					this.Parser.Errors.Add(this._error);
					this._error = null;
					this._parser.ThrowExceptionAndWarning(this);
				}
			}
			finally
			{
				if (this._cachedAsyncResult != null)
				{
					this._cachedAsyncResult.Reset();
				}
			}
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x002C0D78 File Offset: 0x002C0178
		internal void ReadSni(DbAsyncResult asyncResult, TdsParserStateObject stateObj)
		{
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				return;
			}
			IntPtr zero = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!this._parser.AsyncOn)
				{
					uint num = SNINativeMethodWrapper.SNIReadSync(stateObj.Handle, ref zero, TdsParserStaticMethods.GetTimeoutMilliseconds(stateObj.TimeoutTime));
					if (num == 0U)
					{
						stateObj.ProcessSniPacket(zero, 0U);
					}
					else
					{
						this.ReadSniError(stateObj, num);
					}
				}
				else
				{
					stateObj._asyncResult = asyncResult;
					RuntimeHelpers.PrepareConstrainedRegions();
					uint num;
					try
					{
					}
					finally
					{
						stateObj.IncrementPendingCallbacks();
						num = SNINativeMethodWrapper.SNIReadAsync(stateObj.Handle, ref zero);
						if (num != 0U && 997U != num)
						{
							stateObj.DecrementPendingCallbacks(false);
						}
					}
					if (num == 0U)
					{
						stateObj._asyncResult.SetCompletedSynchronously();
						stateObj.ReadAsyncCallback(ADP.PtrZero, zero, 0U);
					}
					else if (997U != num)
					{
						this.ReadSniError(stateObj, num);
					}
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					SNINativeMethodWrapper.SNIPacketRelease(zero);
				}
			}
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x002C0E98 File Offset: 0x002C0298
		private void ReadSniError(TdsParserStateObject stateObj, uint error)
		{
			if (this._parser._fAwaitingPreLogin && error != 258U)
			{
				this._parser._fPreLoginErrorOccurred = true;
				return;
			}
			if (258U == error)
			{
				bool flag = false;
				if (this._internalTimeout)
				{
					flag = true;
				}
				else
				{
					stateObj._internalTimeout = true;
					this._parser.Errors.Add(new SqlError(-2, 0, 11, this._parser.Server, SQLMessage.Timeout(), "", 0));
					if (!stateObj._attentionSent)
					{
						if (stateObj.Parser.State == TdsParserState.OpenLoggedIn)
						{
							stateObj.SendAttention();
							IntPtr zero = IntPtr.Zero;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								error = SNINativeMethodWrapper.SNIReadSync(stateObj.Handle, ref zero, TdsParserStaticMethods.GetTimeoutMilliseconds(stateObj.TimeoutTime));
								if (error == 0U)
								{
									stateObj.ProcessSniPacket(zero, 0U);
									return;
								}
								flag = true;
								goto IL_109;
							}
							finally
							{
								if (zero != IntPtr.Zero)
								{
									SNINativeMethodWrapper.SNIPacketRelease(zero);
								}
							}
						}
						if (this._parser._loginWithFailover)
						{
							stateObj._internalTimeout = false;
							this._parser.State = TdsParserState.Broken;
						}
						else
						{
							flag = true;
						}
					}
				}
				IL_109:
				if (flag)
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
				}
			}
			else
			{
				this._parser.Errors.Add(this._parser.ProcessSNIError(stateObj));
			}
			this._parser.ThrowExceptionAndWarning(stateObj);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x002C1018 File Offset: 0x002C0418
		public void ProcessSniPacket(IntPtr packet, uint error)
		{
			if ((this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken) && error != 0U)
			{
				return;
			}
			if (error != 0U)
			{
				if (this._parser._fAwaitingPreLogin && error != 258U)
				{
					this._parser._fPreLoginErrorOccurred = true;
					return;
				}
				this._error = this._parser.ProcessSNIError(this);
				return;
			}
			else
			{
				SNINativeMethodWrapper.SNIPacketGetConnection(packet);
				uint num = 0U;
				IntPtr ptrZero = ADP.PtrZero;
				SNINativeMethodWrapper.SNIPacketGetData(packet, ref ptrZero, ref num);
				if ((long)this._inBuff.Length < (long)((ulong)num))
				{
					throw SQL.InvalidInternalPacketSize(Res.GetString("SqlMisc_InvalidArraySizeMessage"));
				}
				Marshal.Copy(ptrZero, this._inBuff, 0, (int)num);
				this._inBytesRead = (int)num;
				this._inBytesUsed = 0;
				return;
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x002C10D8 File Offset: 0x002C04D8
		public void ReadAsyncCallback(IntPtr key, IntPtr packet, uint error)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag = true;
			try
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(error, CallbackType.Read);
				}
				this.ProcessSniPacket(packet, error);
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				this.DecrementPendingCallbacks(false);
				if (flag)
				{
					this._asyncResult.SetCompleted();
				}
			}
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x002C1168 File Offset: 0x002C0568
		internal void WritePacket(byte flushMode)
		{
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken || (this._parser.IsYukonOrNewer && !this._bulkCopyOpperationInProgress && this._outBytesUsed == this._outputHeaderLen + BitConverter.ToInt32(this._outBuff, this._outputHeaderLen) && this._outputPacketNumber == 1) || (this._outBytesUsed == this._outputHeaderLen && this._outputPacketNumber == 1))
			{
				return;
			}
			byte b = 1;
			byte outputPacketNumber = this._outputPacketNumber;
			if (1 == flushMode)
			{
				b = 1;
				this._outputPacketNumber = 1;
			}
			else if (flushMode == 0)
			{
				b = 4;
				this._outputPacketNumber += 1;
			}
			this._outBuff[0] = this._outputMessageType;
			this._outBuff[1] = b;
			this._outBuff[2] = (byte)(this._outBytesUsed >> 8);
			this._outBuff[3] = (byte)(this._outBytesUsed & 255);
			this._outBuff[4] = 0;
			this._outBuff[5] = 0;
			this._outBuff[6] = outputPacketNumber;
			this._outBuff[7] = 0;
			this._parser.CheckResetConnection(this);
			this.WriteSni();
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x002C1288 File Offset: 0x002C0688
		private uint SNIWriteAsync(SNIHandle handle, SNIPacket packet, DbAsyncResult asyncResult)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			uint num;
			try
			{
			}
			finally
			{
				this.IncrementPendingCallbacks();
				num = SNINativeMethodWrapper.SNIWriteAsync(handle, packet);
				if (num == 0U || num != 997U)
				{
					this.DecrementPendingCallbacks(false);
				}
			}
			if (num != 0U)
			{
				if (num != 997U)
				{
					Bid.Trace("<sc.TdsParser.WritePacket|Info> write async returned error code %d\n", (int)num);
					this._parser.Errors.Add(this._parser.ProcessSNIError(this));
					this.ThrowExceptionAndWarning();
				}
				else if (num == 997U)
				{
					try
					{
						((IAsyncResult)asyncResult).AsyncWaitHandle.WaitOne();
						if (this._error != null)
						{
							this._parser.Errors.Add(this._error);
							this._error = null;
							Bid.Trace("<sc.TdsParser.WritePacket|Info> write async returned error code %d\n", (int)num);
							this.ThrowExceptionAndWarning();
						}
					}
					finally
					{
						asyncResult.Reset();
					}
				}
			}
			return num;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x002C1388 File Offset: 0x002C0788
		internal void SendAttention()
		{
			if (!this._attentionSent)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				SNIPacket snipacket = new SNIPacket(this.Handle);
				if (this._parser.AsyncOn)
				{
					this._sniAsyncAttnPacket = snipacket;
					if (this._asyncAttentionResult == null)
					{
						this._asyncAttentionResult = new DbAsyncResult(this._parser, string.Empty, null, null, null);
					}
				}
				else
				{
					this._sniAsyncAttnPacket = null;
				}
				SNINativeMethodWrapper.SNIPacketSetData(snipacket, SQL.AttentionHeader, 8);
				if (this._parser.AsyncOn)
				{
					uint num = this.SNIWriteAsync(this.Handle, snipacket, this._asyncAttentionResult);
					Bid.Trace("<sc.TdsParser.SendAttention|Info> Send Attention ASync .\n");
				}
				else
				{
					uint num = SNINativeMethodWrapper.SNIWriteSync(this.Handle, snipacket);
					Bid.Trace("<sc.TdsParser.SendAttention|Info> Send Attention Sync.\n");
					if (num != 0U)
					{
						Bid.Trace("<sc.TdsParser.SendAttention|Info> SNIWriteSync returned error code %d\n", (int)num);
						this._parser.Errors.Add(this._parser.ProcessSNIError(this));
						this._parser.ThrowExceptionAndWarning(this);
					}
				}
				this.SetTimeoutSeconds(5);
				this._attentionSent = true;
				if (Bid.AdvancedOn)
				{
					Bid.TraceBin("<sc.TdsParser.WritePacket|INFO|ADV>  Packet sent", this._outBuff, (ushort)this._outBytesUsed);
				}
				Bid.Trace("<sc.TdsParser.SendAttention|Info> Attention sent to the server.\n");
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x002C14C8 File Offset: 0x002C08C8
		private void WriteSni()
		{
			if (this._sniPacket == null)
			{
				this._sniPacket = new SNIPacket(this.Handle);
			}
			else
			{
				SNINativeMethodWrapper.SNIPacketReset(this.Handle, SNINativeMethodWrapper.IOType.WRITE, this._sniPacket);
			}
			SNINativeMethodWrapper.SNIPacketSetData(this._sniPacket, this._outBuff, this._outBytesUsed);
			if (this._parser.AsyncOn)
			{
				if (this._cachedAsyncResult == null)
				{
					this._cachedAsyncResult = new DbAsyncResult(this._parser, string.Empty, null, null, null);
				}
				this._asyncResult = this._cachedAsyncResult;
				uint num = this.SNIWriteAsync(this.Handle, this._sniPacket, this._cachedAsyncResult);
			}
			else
			{
				uint num = SNINativeMethodWrapper.SNIWriteSync(this.Handle, this._sniPacket);
				if (num != 0U)
				{
					Bid.Trace("<sc.TdsParser.WritePacket|Info> write sync returned error code %d\n", (int)num);
					this._parser.Errors.Add(this._parser.ProcessSNIError(this));
					this.ThrowExceptionAndWarning();
				}
				if (this._bulkCopyOpperationInProgress && TdsParserStaticMethods.GetTimeoutMilliseconds(this.TimeoutTime) == 0)
				{
					this._parser.Errors.Add(new SqlError(-2, 0, 11, this._parser.Server, SQLMessage.Timeout(), "", 0));
					this.SendAttention();
					this._parser.ProcessPendingAck(this);
					this._parser.ThrowExceptionAndWarning(this);
				}
			}
			if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.EncryptionOptions == EncryptionOptions.LOGIN)
			{
				this._parser.RemoveEncryption();
				this._parser.EncryptionOptions = EncryptionOptions.OFF;
				this._sniPacket.Dispose();
				this._sniPacket = new SNIPacket(this.Handle);
			}
			this.SniWriteStatisticsAndTracing();
			this.ResetBuffer();
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x002C1678 File Offset: 0x002C0A78
		public void WriteAsyncCallback(IntPtr key, IntPtr packet, uint error)
		{
			DbAsyncResult dbAsyncResult = this._asyncResult;
			if (this._sniAsyncAttnPacket != null && this._sniAsyncAttnPacket.DangerousGetHandle() == packet)
			{
				dbAsyncResult = this._asyncAttentionResult;
			}
			bool flag = true;
			try
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(error, CallbackType.Read);
				}
				if (error != 0U)
				{
					this._error = this._parser.ProcessSNIError(this);
				}
			}
			catch (Exception e)
			{
				flag = ADP.IsCatchableExceptionType(e);
				throw;
			}
			finally
			{
				this.DecrementPendingCallbacks(false);
				if (flag)
				{
					dbAsyncResult.SetCompleted();
				}
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x002C1738 File Offset: 0x002C0B38
		private void SniReadStatisticsAndTracing()
		{
			SqlStatistics statistics = this.Parser.Statistics;
			if (statistics != null)
			{
				if (statistics.WaitForReply)
				{
					statistics.SafeIncrement(ref statistics._serverRoundtrips);
					statistics.ReleaseAndUpdateNetworkServerTimer();
				}
				statistics.SafeAdd(ref statistics._bytesReceived, (long)this._inBytesRead);
				statistics.SafeIncrement(ref statistics._buffersReceived);
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x002C1798 File Offset: 0x002C0B98
		private void SniWriteStatisticsAndTracing()
		{
			SqlStatistics statistics = this._parser.Statistics;
			if (statistics != null)
			{
				statistics.SafeIncrement(ref statistics._buffersSent);
				statistics.SafeAdd(ref statistics._bytesSent, (long)this._outBytesUsed);
				statistics.RequestNetworkServerTimer();
			}
			if (Bid.AdvancedOn)
			{
				if (this._tracePasswordOffset != 0)
				{
					for (int i = this._tracePasswordOffset; i < this._tracePasswordOffset + this._tracePasswordLength; i++)
					{
						this._outBuff[i] = 0;
					}
					this._tracePasswordOffset = 0;
					this._tracePasswordLength = 0;
				}
				if (this._traceChangePasswordOffset != 0)
				{
					for (int j = this._traceChangePasswordOffset; j < this._traceChangePasswordOffset + this._traceChangePasswordLength; j++)
					{
						this._outBuff[j] = 0;
					}
					this._traceChangePasswordOffset = 0;
					this._traceChangePasswordLength = 0;
				}
				Bid.TraceBin("<sc.TdsParser.WritePacket|INFO|ADV>  Packet sent", this._outBuff, (ushort)this._outBytesUsed);
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x002C1878 File Offset: 0x002C0C78
		[Conditional("DEBUG")]
		private void AssertValidState()
		{
			if (this._inBytesUsed < 0 || this._inBytesRead < 0)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "either _inBytesUsed or _inBytesRead is negative: {0}, {1}", new object[]
				{
					this._inBytesUsed,
					this._inBytesRead
				});
			}
			else if (this._inBytesUsed > this._inBytesRead)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "_inBytesUsed > _inBytesRead: {0} > {1}", new object[]
				{
					this._inBytesUsed,
					this._inBytesRead
				});
			}
		}

		// Token: 0x04001C10 RID: 7184
		private static int _objectTypeCount;

		// Token: 0x04001C11 RID: 7185
		internal readonly int _objectID = Interlocked.Increment(ref TdsParserStateObject._objectTypeCount);

		// Token: 0x04001C12 RID: 7186
		private TdsParserStateObject _nextPooledObject;

		// Token: 0x04001C13 RID: 7187
		private readonly TdsParser _parser;

		// Token: 0x04001C14 RID: 7188
		private SNIHandle _sessionHandle;

		// Token: 0x04001C15 RID: 7189
		private readonly WeakReference _owner = new WeakReference(null);

		// Token: 0x04001C16 RID: 7190
		private int _activateCount;

		// Token: 0x04001C17 RID: 7191
		internal readonly int _inputHeaderLen = 8;

		// Token: 0x04001C18 RID: 7192
		internal readonly int _outputHeaderLen = 8;

		// Token: 0x04001C19 RID: 7193
		internal byte[] _outBuff;

		// Token: 0x04001C1A RID: 7194
		internal int _outBytesUsed = 8;

		// Token: 0x04001C1B RID: 7195
		private byte[] _inBuff;

		// Token: 0x04001C1C RID: 7196
		internal int _inBytesUsed;

		// Token: 0x04001C1D RID: 7197
		internal int _inBytesRead;

		// Token: 0x04001C1E RID: 7198
		internal int _inBytesPacket;

		// Token: 0x04001C1F RID: 7199
		internal byte _outputMessageType;

		// Token: 0x04001C20 RID: 7200
		internal byte _messageStatus;

		// Token: 0x04001C21 RID: 7201
		internal byte _outputPacketNumber = 1;

		// Token: 0x04001C22 RID: 7202
		internal bool _pendingData;

		// Token: 0x04001C23 RID: 7203
		internal volatile bool _fResetEventOwned;

		// Token: 0x04001C24 RID: 7204
		internal volatile bool _fResetConnectionSent;

		// Token: 0x04001C25 RID: 7205
		internal bool _errorTokenReceived;

		// Token: 0x04001C26 RID: 7206
		internal bool _bulkCopyOpperationInProgress;

		// Token: 0x04001C27 RID: 7207
		internal SNIPacket _sniPacket;

		// Token: 0x04001C28 RID: 7208
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x04001C29 RID: 7209
		internal DbAsyncResult _asyncResult;

		// Token: 0x04001C2A RID: 7210
		internal DbAsyncResult _cachedAsyncResult;

		// Token: 0x04001C2B RID: 7211
		internal DbAsyncResult _asyncAttentionResult;

		// Token: 0x04001C2C RID: 7212
		private GCHandle _gcHandle;

		// Token: 0x04001C2D RID: 7213
		private int _pendingCallbacks;

		// Token: 0x04001C2E RID: 7214
		private int _timeoutSeconds;

		// Token: 0x04001C2F RID: 7215
		private long _timeoutTime;

		// Token: 0x04001C30 RID: 7216
		internal bool _attentionSent;

		// Token: 0x04001C31 RID: 7217
		internal bool _attentionReceived;

		// Token: 0x04001C32 RID: 7218
		internal bool _internalTimeout;

		// Token: 0x04001C33 RID: 7219
		private bool _cancelled;

		// Token: 0x04001C34 RID: 7220
		private volatile int _allowObjectID;

		// Token: 0x04001C35 RID: 7221
		internal bool _hasOpenResult;

		// Token: 0x04001C36 RID: 7222
		internal SqlInternalTransaction _executedUnderTransaction;

		// Token: 0x04001C37 RID: 7223
		internal ulong _longlen;

		// Token: 0x04001C38 RID: 7224
		internal ulong _longlenleft;

		// Token: 0x04001C39 RID: 7225
		internal int[] _decimalBits;

		// Token: 0x04001C3A RID: 7226
		internal byte[] _bTmp = new byte[12];

		// Token: 0x04001C3B RID: 7227
		internal byte[] _bHeaderBuffer;

		// Token: 0x04001C3C RID: 7228
		internal SqlError _error;

		// Token: 0x04001C3D RID: 7229
		internal _SqlMetaDataSet _cleanupMetaData;

		// Token: 0x04001C3E RID: 7230
		internal _SqlMetaDataSetCollection _cleanupAltMetaDataSetArray;

		// Token: 0x04001C3F RID: 7231
		internal int _tracePasswordOffset;

		// Token: 0x04001C40 RID: 7232
		internal int _tracePasswordLength;

		// Token: 0x04001C41 RID: 7233
		internal int _traceChangePasswordOffset;

		// Token: 0x04001C42 RID: 7234
		internal int _traceChangePasswordLength;

		// Token: 0x04001C43 RID: 7235
		internal bool _receivedColMetaData;

		// Token: 0x04001C44 RID: 7236
		private SniContext _sniContext;

		// Token: 0x04001C45 RID: 7237
		private bool _bcpLock;
	}
}
