using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x0200022E RID: 558
	internal sealed class TdsParserStateObject
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000EDBD4 File Offset: 0x000ECFD4
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000EDBE8 File Offset: 0x000ECFE8
		internal TdsParserStateObject(TdsParser parser)
		{
			this._parser = parser;
			this.SetPacketSize(4096);
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = new LastIOTimer();
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000EDCC8 File Offset: 0x000ED0C8
		internal TdsParserStateObject(TdsParser parser, SNIHandle physicalConnection, bool async)
		{
			this._parser = parser;
			this.SniContext = SniContext.Snix_GetMarsSession;
			this.SetPacketSize(this._parser._physicalStateObj._outBuff.Length);
			SNINativeMethodWrapper.ConsumerInfo myInfo = this.CreateConsumerInfo(async);
			this._sessionHandle = new SNIHandle(myInfo, physicalConnection);
			if (this._sessionHandle.Status != 0U)
			{
				this.AddError(parser.ProcessSNIError(this));
				this.ThrowExceptionAndWarning(false, false);
			}
			this.IncrementPendingCallbacks();
			this._lastSuccessfulIOTimer = parser._physicalStateObj._lastSuccessfulIOTimer;
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x000EDDFC File Offset: 0x000ED1FC
		// (set) Token: 0x0600225C RID: 8796 RVA: 0x000EDE10 File Offset: 0x000ED210
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

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x000EDE24 File Offset: 0x000ED224
		internal SNIHandle Handle
		{
			get
			{
				return this._sessionHandle;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x000EDE38 File Offset: 0x000ED238
		internal bool HasOpenResult
		{
			get
			{
				return this._hasOpenResult;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000EDE4C File Offset: 0x000ED24C
		internal bool IsOrphaned
		{
			get
			{
				return this._activateCount != 0 && !this._owner.IsAlive;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (set) Token: 0x06002260 RID: 8800 RVA: 0x000EDE74 File Offset: 0x000ED274
		internal object Owner
		{
			set
			{
				SqlDataReader sqlDataReader = value as SqlDataReader;
				if (sqlDataReader == null)
				{
					this._readerState = null;
				}
				else
				{
					this._readerState = sqlDataReader._sharedState;
				}
				this._owner.Target = value;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x000EDEAC File Offset: 0x000ED2AC
		internal bool HasOwner
		{
			get
			{
				return this._owner.IsAlive;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000EDEC4 File Offset: 0x000ED2C4
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000EDED8 File Offset: 0x000ED2D8
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x000EDEEC File Offset: 0x000ED2EC
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

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000EDF00 File Offset: 0x000ED300
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

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x000EDF24 File Offset: 0x000ED324
		internal bool TimeoutHasExpired
		{
			get
			{
				return TdsParserStaticMethods.TimeoutHasExpired(this._timeoutTime);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x000EDF3C File Offset: 0x000ED33C
		// (set) Token: 0x06002268 RID: 8808 RVA: 0x000EDF70 File Offset: 0x000ED370
		internal long TimeoutTime
		{
			get
			{
				if (this._timeoutMilliseconds != 0L)
				{
					this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
					this._timeoutMilliseconds = 0L;
				}
				return this._timeoutTime;
			}
			set
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = value;
			}
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000EDF8C File Offset: 0x000ED38C
		internal int GetTimeoutRemaining()
		{
			int result;
			if (this._timeoutMilliseconds != 0L)
			{
				result = (int)Math.Min(2147483647L, this._timeoutMilliseconds);
				this._timeoutTime = TdsParserStaticMethods.GetTimeout(this._timeoutMilliseconds);
				this._timeoutMilliseconds = 0L;
			}
			else
			{
				result = TdsParserStaticMethods.GetTimeoutMilliseconds(this._timeoutTime);
			}
			return result;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000EDFDC File Offset: 0x000ED3DC
		internal bool TryStartNewRow(bool isNullCompressed, int nullBitmapColumnsCount = 0)
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneNullBitmapInfo();
			}
			if (isNullCompressed)
			{
				if (!this._nullBitmapInfo.TryInitialize(this, nullBitmapColumnsCount))
				{
					return false;
				}
			}
			else
			{
				this._nullBitmapInfo.Clean();
			}
			return true;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000EE01C File Offset: 0x000ED41C
		internal bool IsRowTokenReady()
		{
			int num = Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed) - 1;
			if (num > 0)
			{
				if (this._inBuff[this._inBytesUsed] == 209)
				{
					return true;
				}
				if (this._inBuff[this._inBytesUsed] == 210)
				{
					int num2 = 1 + (this._cleanupMetaData.Length + 7) / 8;
					return num2 <= num;
				}
			}
			return false;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x000EE090 File Offset: 0x000ED490
		internal bool IsNullCompressionBitSet(int columnOrdinal)
		{
			return this._nullBitmapInfo.IsGuaranteedNull(columnOrdinal);
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000EE0AC File Offset: 0x000ED4AC
		internal void Activate(object owner)
		{
			this.Owner = owner;
			int num = Interlocked.Increment(ref this._activateCount);
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000EE0CC File Offset: 0x000ED4CC
		internal void Cancel(int objectID)
		{
			bool flag = false;
			try
			{
				while (!flag && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
				{
					Monitor.TryEnter(this, 100, ref flag);
					if (flag && !this._cancelled && objectID == this._allowObjectID && objectID != -1)
					{
						this._cancelled = true;
						if (this._pendingData && !this._attentionSent)
						{
							bool flag2 = false;
							while (!flag2 && this._parser.State != TdsParserState.Closed && this._parser.State != TdsParserState.Broken)
							{
								try
								{
									this._parser.Connection._parserLock.Wait(false, 100, ref flag2);
									if (flag2)
									{
										this._parser.Connection.ThreadHasParserLockForClose = true;
										this.SendAttention(false);
									}
								}
								finally
								{
									if (flag2)
									{
										if (this._parser.Connection.ThreadHasParserLockForClose)
										{
											this._parser.Connection.ThreadHasParserLockForClose = false;
										}
										this._parser.Connection._parserLock.Release();
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this);
				}
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000EE22C File Offset: 0x000ED62C
		internal void CancelRequest()
		{
			this.ResetBuffer();
			this._outputPacketNumber = 1;
			if (!this._bulkCopyWriteTimeout)
			{
				this.SendAttention(false);
				this.Parser.ProcessPendingAck(this);
			}
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000EE264 File Offset: 0x000ED664
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

		// Token: 0x06002271 RID: 8817 RVA: 0x000EE2DC File Offset: 0x000ED6DC
		internal void CloseSession()
		{
			this.ResetCancelAndProcessAttention();
			this.Parser.PutSession(this);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000EE2FC File Offset: 0x000ED6FC
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
				if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
				{
					this._internalTimeout = false;
				}
				else
				{
					this.SetTimeoutStateStopped();
				}
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000EE37C File Offset: 0x000ED77C
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

		// Token: 0x06002274 RID: 8820 RVA: 0x000EE3E0 File Offset: 0x000ED7E0
		internal void CreatePhysicalSNIHandle(string serverName, bool ignoreSniOpenTimeout, long timerExpire, out byte[] instanceName, byte[] spnBuffer, bool flushCache, bool async, bool fParallel, TransparentNetworkResolutionState transparentNetworkResolutionState, int totalTimeout)
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
			this._sessionHandle = new SNIHandle(myInfo, serverName, spnBuffer, ignoreSniOpenTimeout, checked((int)num), ref instanceName, flushCache, !async, fParallel, transparentNetworkResolutionState, totalTimeout);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000EE450 File Offset: 0x000ED850
		internal bool Deactivate()
		{
			bool result = false;
			try
			{
				TdsParserState state = this.Parser.State;
				if (state != TdsParserState.Broken && state != TdsParserState.Closed)
				{
					if (this._pendingData)
					{
						this.Parser.DrainData(this);
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

		// Token: 0x06002276 RID: 8822 RVA: 0x000EE4D0 File Offset: 0x000ED8D0
		internal void RemoveOwner()
		{
			if (this._parser.MARSOn)
			{
				int num = Interlocked.Decrement(ref this._activateCount);
			}
			this.Owner = null;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000EE500 File Offset: 0x000ED900
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

		// Token: 0x06002278 RID: 8824 RVA: 0x000EE53C File Offset: 0x000ED93C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int DecrementPendingCallbacks(bool release)
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
			return num;
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000EE5AC File Offset: 0x000ED9AC
		internal void Dispose()
		{
			SafeHandle sniPacket = this._sniPacket;
			SafeHandle sessionHandle = this._sessionHandle;
			SafeHandle sniAsyncAttnPacket = this._sniAsyncAttnPacket;
			this._sniPacket = null;
			this._sessionHandle = null;
			this._sniAsyncAttnPacket = null;
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				this._networkPacketTimeout = null;
				networkPacketTimeout.Dispose();
			}
			if (Volatile.Read(ref this._readingCount) > 0)
			{
				SpinWait.SpinUntil(() => Volatile.Read(ref this._readingCount) == 0);
			}
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
			if (this._writePacketCache != null)
			{
				object writePacketLockObject = this._writePacketLockObject;
				lock (writePacketLockObject)
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						this._writePacketCache.Dispose();
					}
				}
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000EE6D4 File Offset: 0x000EDAD4
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

		// Token: 0x0600227B RID: 8827 RVA: 0x000EE704 File Offset: 0x000EDB04
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal int IncrementPendingCallbacks()
		{
			int result = Interlocked.Increment(ref this._pendingCallbacks);
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.TdsParserStateObject.IncrementPendingCallbacks|ADV> %d#, after incrementing _pendingCallbacks: %d\n", this.ObjectID, this._pendingCallbacks);
			}
			return result;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000EE73C File Offset: 0x000EDB3C
		internal void SetTimeoutSeconds(int timeout)
		{
			this.SetTimeoutMilliseconds((long)timeout * 1000L);
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000EE758 File Offset: 0x000EDB58
		internal void SetTimeoutMilliseconds(long timeout)
		{
			if (timeout <= 0L)
			{
				this._timeoutMilliseconds = 0L;
				this._timeoutTime = long.MaxValue;
				return;
			}
			this._timeoutMilliseconds = timeout;
			this._timeoutTime = 0L;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x000EE794 File Offset: 0x000EDB94
		internal void StartSession(int objectID)
		{
			this._allowObjectID = objectID;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000EE7AC File Offset: 0x000EDBAC
		internal void ThrowExceptionAndWarning(bool callerHasConnectionLock = false, bool asyncClose = false)
		{
			this._parser.ThrowExceptionAndWarning(this, callerHasConnectionLock, asyncClose);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x000EE7C8 File Offset: 0x000EDBC8
		internal Task ExecuteFlush()
		{
			Task result;
			lock (this)
			{
				if (this._cancelled && 1 == this._outputPacketNumber)
				{
					this.ResetBuffer();
					this._cancelled = false;
					throw SQL.OperationCancelled();
				}
				Task task = this.WritePacket(1, false);
				if (task == null)
				{
					this._pendingData = true;
					this._messageStatus = 0;
					result = null;
				}
				else
				{
					result = AsyncHelper.CreateContinuationTask(task, delegate()
					{
						this._pendingData = true;
						this._messageStatus = 0;
					}, null, null);
				}
			}
			return result;
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000EE864 File Offset: 0x000EDC64
		internal bool TryProcessHeader()
		{
			if (this._partialHeaderBytesRead > 0 || this._inBytesUsed + this._inputHeaderLen > this._inBytesRead)
			{
				for (;;)
				{
					int num = Math.Min(this._inBytesRead - this._inBytesUsed, this._inputHeaderLen - this._partialHeaderBytesRead);
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, this._partialHeaderBuffer, this._partialHeaderBytesRead, num);
					this._partialHeaderBytesRead += num;
					this._inBytesUsed += num;
					if (this._partialHeaderBytesRead == this._inputHeaderLen)
					{
						this._partialHeaderBytesRead = 0;
						this._inBytesPacket = ((int)this._partialHeaderBuffer[2] << 8 | (int)this._partialHeaderBuffer[3]) - this._inputHeaderLen;
						this._messageStatus = this._partialHeaderBuffer[1];
					}
					else
					{
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							break;
						}
						if (!this.TryReadNetworkPacket())
						{
							return false;
						}
						bool flag;
						if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
						{
							flag = this._internalTimeout;
						}
						else
						{
							flag = this.IsTimeoutStateExpired;
						}
						if (flag)
						{
							goto Block_6;
						}
					}
					if (this._partialHeaderBytesRead == 0)
					{
						goto Block_7;
					}
				}
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_6:
				this.ThrowExceptionAndWarning(false, false);
				return true;
				Block_7:;
			}
			else
			{
				this._messageStatus = this._inBuff[this._inBytesUsed + 1];
				this._inBytesPacket = ((int)this._inBuff[this._inBytesUsed + 2] << 8 | (int)this._inBuff[this._inBytesUsed + 2 + 1]) - this._inputHeaderLen;
				this._inBytesUsed += this._inputHeaderLen;
			}
			if (this._inBytesPacket < 0)
			{
				throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
			}
			return true;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000EE9FC File Offset: 0x000EDDFC
		internal bool TryPrepareBuffer()
		{
			if (this._inBytesPacket == 0 && this._inBytesUsed < this._inBytesRead && !this.TryProcessHeader())
			{
				return false;
			}
			if (this._inBytesUsed == this._inBytesRead)
			{
				if (this._inBytesPacket > 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
				}
				else if (this._inBytesPacket == 0)
				{
					if (!this.TryReadNetworkPacket())
					{
						return false;
					}
					if (!this.TryProcessHeader())
					{
						return false;
					}
					if (this._inBytesUsed == this._inBytesRead && !this.TryReadNetworkPacket())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000EEA80 File Offset: 0x000EDE80
		internal void ResetBuffer()
		{
			this._outBytesUsed = this._outputHeaderLen;
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000EEA9C File Offset: 0x000EDE9C
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
							string str = string.Concat(new string[]
							{
								Res.GetString("SQL_InvalidInternalPacketSize"),
								" ",
								inBuff.Length.ToString(),
								", ",
								this._inBytesUsed.ToString(),
								", ",
								num.ToString(),
								", ",
								this._inBuff.Length.ToString()
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

		// Token: 0x06002285 RID: 8837 RVA: 0x000EEC30 File Offset: 0x000EE030
		internal bool TryPeekByte(out byte value)
		{
			if (!this.TryReadByte(out value))
			{
				return false;
			}
			this._inBytesPacket++;
			this._inBytesUsed--;
			return true;
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000EEC68 File Offset: 0x000EE068
		public bool TryReadByteArray(byte[] buff, int offset, int len)
		{
			int num;
			return this.TryReadByteArray(buff, offset, len, out num);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000EEC80 File Offset: 0x000EE080
		public bool TryReadByteArray(byte[] buff, int offset, int len, out int totalRead)
		{
			totalRead = 0;
			while (len > 0)
			{
				if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
				{
					return false;
				}
				int num = Math.Min(len, Math.Min(this._inBytesPacket, this._inBytesRead - this._inBytesUsed));
				if (buff != null)
				{
					Buffer.BlockCopy(this._inBuff, this._inBytesUsed, buff, offset + totalRead, num);
				}
				totalRead += num;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
				len -= num;
			}
			return true;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000EED24 File Offset: 0x000EE124
		internal bool TryReadByte(out byte value)
		{
			value = 0;
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				return false;
			}
			this._inBytesPacket--;
			byte[] inBuff = this._inBuff;
			int inBytesUsed = this._inBytesUsed;
			this._inBytesUsed = inBytesUsed + 1;
			value = inBuff[inBytesUsed];
			return true;
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000EED80 File Offset: 0x000EE180
		internal bool TryReadChar(out char value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = '\0';
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (char)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000EEE00 File Offset: 0x000EE200
		internal bool TryReadInt16(out short value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (short)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000EEE80 File Offset: 0x000EE280
		internal bool TryReadInt32(out int value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0;
				return false;
			}
			value = BitConverter.ToInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000EEEFC File Offset: 0x000EE2FC
		internal bool TryReadInt64(out long value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0L;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToInt64(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 8 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0L;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToInt64(this._bTmp, 0);
			return true;
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000EEFCC File Offset: 0x000EE3CC
		internal bool TryReadUInt16(out ushort value)
		{
			byte[] array;
			int num;
			if (this._inBytesUsed + 2 > this._inBytesRead || this._inBytesPacket < 2)
			{
				if (!this.TryReadByteArray(this._bTmp, 0, 2))
				{
					value = 0;
					return false;
				}
				array = this._bTmp;
				num = 0;
			}
			else
			{
				array = this._inBuff;
				num = this._inBytesUsed;
				this._inBytesUsed += 2;
				this._inBytesPacket -= 2;
			}
			value = (ushort)(((int)array[num + 1] << 8) + (int)array[num]);
			return true;
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000EF04C File Offset: 0x000EE44C
		internal bool TryReadUInt32(out uint value)
		{
			if ((this._inBytesPacket == 0 || this._inBytesUsed == this._inBytesRead) && !this.TryPrepareBuffer())
			{
				value = 0U;
				return false;
			}
			if (this._bTmpRead <= 0 && this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToUInt32(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			int num = 0;
			if (!this.TryReadByteArray(this._bTmp, this._bTmpRead, 4 - this._bTmpRead, out num))
			{
				this._bTmpRead += num;
				value = 0U;
				return false;
			}
			this._bTmpRead = 0;
			value = BitConverter.ToUInt32(this._bTmp, 0);
			return true;
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000EF118 File Offset: 0x000EE518
		internal bool TryReadSingle(out float value)
		{
			if (this._inBytesUsed + 4 <= this._inBytesRead && this._inBytesPacket >= 4)
			{
				value = BitConverter.ToSingle(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 4;
				this._inBytesPacket -= 4;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 4))
			{
				value = 0f;
				return false;
			}
			value = BitConverter.ToSingle(this._bTmp, 0);
			return true;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000EF198 File Offset: 0x000EE598
		internal bool TryReadDouble(out double value)
		{
			if (this._inBytesUsed + 8 <= this._inBytesRead && this._inBytesPacket >= 8)
			{
				value = BitConverter.ToDouble(this._inBuff, this._inBytesUsed);
				this._inBytesUsed += 8;
				this._inBytesPacket -= 8;
				return true;
			}
			if (!this.TryReadByteArray(this._bTmp, 0, 8))
			{
				value = 0.0;
				return false;
			}
			value = BitConverter.ToDouble(this._bTmp, 0);
			return true;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000EF21C File Offset: 0x000EE61C
		internal bool TryReadString(int length, out string value)
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
				if (!this.TryReadByteArray(this._bTmp, 0, num))
				{
					value = null;
					return false;
				}
				bytes = this._bTmp;
			}
			else
			{
				bytes = this._inBuff;
				index = this._inBytesUsed;
				this._inBytesUsed += num;
				this._inBytesPacket -= num;
			}
			value = Encoding.Unicode.GetString(bytes, index, num);
			return true;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000EF2C0 File Offset: 0x000EE6C0
		internal bool TryReadStringWithEncoding(int length, Encoding encoding, bool isPlp, out string value)
		{
			if (encoding == null)
			{
				if (isPlp)
				{
					ulong num;
					if (!this._parser.TrySkipPlpValue((ulong)((long)length), this, out num))
					{
						value = null;
						return false;
					}
				}
				else if (!this.TrySkipBytes(length))
				{
					value = null;
					return false;
				}
				this._parser.ThrowUnsupportedCollationEncountered(this);
			}
			byte[] bytes = null;
			int index = 0;
			if (isPlp)
			{
				if (!this.TryReadPlpBytes(ref bytes, 0, 2147483647, out length))
				{
					value = null;
					return false;
				}
			}
			else if (this._inBytesUsed + length > this._inBytesRead || this._inBytesPacket < length)
			{
				if (this._bTmp == null || this._bTmp.Length < length)
				{
					this._bTmp = new byte[length];
				}
				if (!this.TryReadByteArray(this._bTmp, 0, length))
				{
					value = null;
					return false;
				}
				bytes = this._bTmp;
			}
			else
			{
				bytes = this._inBuff;
				index = this._inBytesUsed;
				this._inBytesUsed += length;
				this._inBytesPacket -= length;
			}
			value = encoding.GetString(bytes, index, length);
			return true;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000EF3B8 File Offset: 0x000EE7B8
		internal ulong ReadPlpLength(bool returnPlpNullIfNull)
		{
			ulong result;
			if (!this.TryReadPlpLength(returnPlpNullIfNull, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000EF3DC File Offset: 0x000EE7DC
		internal bool TryReadPlpLength(bool returnPlpNullIfNull, out ulong lengthLeft)
		{
			bool flag = false;
			if (this._longlen == 0UL)
			{
				long longlen;
				if (!this.TryReadInt64(out longlen))
				{
					lengthLeft = 0UL;
					return false;
				}
				this._longlen = (ulong)longlen;
			}
			if (this._longlen == 18446744073709551615UL)
			{
				this._longlen = 0UL;
				this._longlenleft = 0UL;
				flag = true;
			}
			else
			{
				uint num;
				if (!this.TryReadUInt32(out num))
				{
					lengthLeft = 0UL;
					return false;
				}
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
				lengthLeft = ulong.MaxValue;
				return true;
			}
			lengthLeft = this._longlenleft;
			return true;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000EF46C File Offset: 0x000EE86C
		internal int ReadPlpBytesChunk(byte[] buff, int offset, int len)
		{
			int num = (int)Math.Min(this._longlenleft, (ulong)((long)len));
			int result;
			bool flag = this.TryReadByteArray(buff, offset, num, out result);
			this._longlenleft -= (ulong)((long)num);
			if (!flag)
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000EF4B0 File Offset: 0x000EE8B0
		internal bool TryReadPlpBytes(ref byte[] buff, int offst, int len, out int totalBytesRead)
		{
			int num = 0;
			if (this._longlen == 0UL)
			{
				if (buff == null)
				{
					buff = new byte[0];
				}
				totalBytesRead = 0;
				return true;
			}
			int i = len;
			if (buff == null && this._longlen != 18446744073709551614UL)
			{
				buff = new byte[Math.Min((int)this._longlen, len)];
			}
			if (this._longlenleft == 0UL)
			{
				ulong num2;
				if (!this.TryReadPlpLength(false, out num2))
				{
					totalBytesRead = 0;
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					totalBytesRead = 0;
					return true;
				}
			}
			if (buff == null)
			{
				buff = new byte[this._longlenleft];
			}
			totalBytesRead = 0;
			while (i > 0)
			{
				int num3 = (int)Math.Min(this._longlenleft, (ulong)((long)i));
				if (buff.Length < offst + num3)
				{
					byte[] array = new byte[offst + num3];
					Buffer.BlockCopy(buff, 0, array, 0, offst);
					buff = array;
				}
				bool flag = this.TryReadByteArray(buff, offst, num3, out num);
				i -= num;
				offst += num;
				totalBytesRead += num;
				this._longlenleft -= (ulong)((long)num);
				if (!flag)
				{
					return false;
				}
				ulong num2;
				if (this._longlenleft == 0UL && !this.TryReadPlpLength(false, out num2))
				{
					return false;
				}
				if (this._longlenleft == 0UL)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x000EF5C8 File Offset: 0x000EE9C8
		internal bool TrySkipLongBytes(long num)
		{
			while (num > 0L)
			{
				int num2 = (int)Math.Min(2147483647L, num);
				if (!this.TryReadByteArray(null, 0, num2))
				{
					return false;
				}
				num -= (long)num2;
			}
			return true;
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x000EF600 File Offset: 0x000EEA00
		internal bool TrySkipBytes(int num)
		{
			return this.TryReadByteArray(null, 0, num);
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x000EF618 File Offset: 0x000EEA18
		internal void SetSnapshot()
		{
			this._snapshot = new TdsParserStateObject.StateSnapshot(this);
			this._snapshot.Snap();
			this._snapshotReplay = false;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000EF644 File Offset: 0x000EEA44
		internal void ResetSnapshot()
		{
			this._snapshot = null;
			this._snapshotReplay = false;
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x000EF660 File Offset: 0x000EEA60
		internal bool TryReadNetworkPacket()
		{
			if (this._snapshot != null)
			{
				if (this._snapshotReplay && this._snapshot.Replay())
				{
					Bid.Trace("<sc.TdsParser.ReadNetworkPacket|INFO|ADV> Async packet replay\n");
					return true;
				}
				this._inBuff = new byte[this._inBuff.Length];
			}
			if (this._syncOverAsync)
			{
				this.ReadSniSyncOverAsync();
				return true;
			}
			this.ReadSni(new TaskCompletionSource<object>());
			return false;
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000EF6C8 File Offset: 0x000EEAC8
		internal void PrepareReplaySnapshot()
		{
			this._networkPacketTaskSource = null;
			this._snapshot.PrepareReplay();
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000EF6E8 File Offset: 0x000EEAE8
		internal void ReadSniSyncOverAsync()
		{
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			IntPtr zero = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag = false;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				flag = true;
				SNIHandle handle = this.Handle;
				if (handle == null)
				{
					throw ADP.ClosedConnectionError();
				}
				uint num = SNINativeMethodWrapper.SNIReadSyncOverAsync(handle, ref zero, this.GetTimeoutRemaining());
				Interlocked.Decrement(ref this._readingCount);
				flag = false;
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(num, CallbackType.Read);
				}
				if (num == 0U)
				{
					this.ProcessSniPacket(zero, 0U);
				}
				else
				{
					this.ReadSniError(this, num);
				}
			}
			finally
			{
				if (flag)
				{
					Interlocked.Decrement(ref this._readingCount);
				}
				if (zero != IntPtr.Zero)
				{
					SNINativeMethodWrapper.SNIPacketRelease(zero);
				}
			}
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000EF7CC File Offset: 0x000EEBCC
		internal void OnConnectionClosed()
		{
			this.Parser.State = TdsParserState.Broken;
			this.Parser.Connection.BreakConnection();
			Thread.MemoryBarrier();
			TaskCompletionSource<object> taskCompletionSource = this._networkPacketTaskSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			taskCompletionSource = this._writeCompletionSource;
			if (taskCompletionSource != null)
			{
				taskCompletionSource.TrySetException(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000EF834 File Offset: 0x000EEC34
		public void SetTimeoutStateStopped()
		{
			Interlocked.Exchange(ref this._timeoutState, 0);
			this._timeoutIdentityValue = 0;
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000EF858 File Offset: 0x000EEC58
		public bool IsTimeoutStateExpired
		{
			get
			{
				int timeoutState = this._timeoutState;
				return timeoutState == 2 || timeoutState == 3;
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000EF878 File Offset: 0x000EEC78
		private void OnTimeoutAsync(object state)
		{
			if (this._enforceTimeoutDelay)
			{
				Thread.Sleep(this._enforcedTimeoutDelayInMilliSeconds);
			}
			int timeoutIdentityValue = this._timeoutIdentityValue;
			TdsParserStateObject.TimeoutState timeoutState = (TdsParserStateObject.TimeoutState)state;
			if (timeoutState.IdentityValue == this._timeoutIdentityValue)
			{
				this.OnTimeoutCore(1, 2);
			}
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000EF8C0 File Offset: 0x000EECC0
		private void OnTimeoutSync(object state = null)
		{
			this.OnTimeoutCore(1, 3);
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x000EF8D8 File Offset: 0x000EECD8
		private void OnTimeoutCore(int expectedState, int targetState)
		{
			bool disableHardenedQueryTimeouts = LocalAppContextSwitches.DisableHardenedQueryTimeouts;
			bool flag;
			if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
			{
				flag = !this._internalTimeout;
			}
			else
			{
				flag = (Interlocked.CompareExchange(ref this._timeoutState, targetState, expectedState) == expectedState);
			}
			if (flag)
			{
				if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
				{
					this._internalTimeout = true;
				}
				lock (this)
				{
					if (!this._attentionSent)
					{
						this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U));
						TaskCompletionSource<object> source = this._networkPacketTaskSource;
						if (this._parser.Connection.IsInPool)
						{
							this._parser.State = TdsParserState.Broken;
							this._parser.Connection.BreakConnection();
							if (source != null)
							{
								source.TrySetCanceled();
							}
						}
						else if (this._parser.State == TdsParserState.OpenLoggedIn)
						{
							try
							{
								this.SendAttention(true);
							}
							catch (Exception e)
							{
								if (!ADP.IsCatchableExceptionType(e))
								{
									throw;
								}
								if (source != null)
								{
									source.TrySetCanceled();
								}
							}
						}
						if (source != null)
						{
							Task.Delay(5000).ContinueWith(delegate(Task _)
							{
								if (!source.Task.IsCompleted)
								{
									int num = this.IncrementPendingCallbacks();
									RuntimeHelpers.PrepareConstrainedRegions();
									try
									{
										if (num == 3 && !source.Task.IsCompleted)
										{
											bool flag3 = false;
											try
											{
												this.CheckThrowSNIException();
											}
											catch (Exception exception)
											{
												if (source.TrySetException(exception))
												{
													flag3 = true;
												}
											}
											this._parser.State = TdsParserState.Broken;
											this._parser.Connection.BreakConnection();
											if (!flag3)
											{
												source.TrySetCanceled();
											}
										}
									}
									finally
									{
										this.DecrementPendingCallbacks(false);
									}
								}
							});
						}
					}
				}
			}
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x000EFA74 File Offset: 0x000EEE74
		internal void ReadSni(TaskCompletionSource<object> completion)
		{
			this._networkPacketTaskSource = completion;
			Thread.MemoryBarrier();
			if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
				{
					if (this._networkPacketTimeout == null)
					{
						this._networkPacketTimeout = new Timer(new TimerCallback(this.OnTimeoutSync), null, -1, -1);
					}
				}
				else
				{
					if (Interlocked.CompareExchange(ref this._timeoutState, 1, 0) == 0)
					{
						this._timeoutIdentityValue = Interlocked.Increment(ref this._timeoutIdentitySource);
					}
					Timer networkPacketTimeout = this._networkPacketTimeout;
					if (networkPacketTimeout != null)
					{
						networkPacketTimeout.Dispose();
					}
					this._networkPacketTimeout = new Timer(new TimerCallback(this.OnTimeoutAsync), new TdsParserStateObject.TimeoutState(this._timeoutIdentityValue), -1, -1);
				}
				int timeoutRemaining = this.GetTimeoutRemaining();
				if (timeoutRemaining > 0)
				{
					this.ChangeNetworkPacketTimeout(timeoutRemaining, -1);
				}
				SNIHandle snihandle = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					Interlocked.Increment(ref this._readingCount);
					snihandle = this.Handle;
					if (snihandle != null)
					{
						this.IncrementPendingCallbacks();
						num = SNINativeMethodWrapper.SNIReadAsync(snihandle, ref zero);
						if (num != 0U && 997U != num)
						{
							this.DecrementPendingCallbacks(false);
						}
					}
					Interlocked.Decrement(ref this._readingCount);
				}
				if (snihandle == null)
				{
					throw ADP.ClosedConnectionError();
				}
				if (num == 0U)
				{
					this.ReadAsyncCallback(ADP.PtrZero, zero, 0U);
				}
				else if (997U != num)
				{
					this.ReadSniError(this, num);
					this._networkPacketTaskSource.TrySetResult(null);
					if (!LocalAppContextSwitches.DisableHardenedQueryTimeouts)
					{
						this.SetTimeoutStateStopped();
					}
					this.ChangeNetworkPacketTimeout(-1, -1);
				}
				else if (timeoutRemaining == 0)
				{
					if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
					{
						this.ChangeNetworkPacketTimeout(0, -1);
					}
					else
					{
						this.ChangeNetworkPacketTimeout(-1, -1);
						this.OnTimeoutSync(null);
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

		// Token: 0x060022A5 RID: 8869 RVA: 0x000EFC64 File Offset: 0x000EF064
		internal bool IsConnectionAlive(bool throwOnException)
		{
			bool result = true;
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value > 50000L)
			{
				if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					result = false;
					if (throwOnException)
					{
						throw SQL.ConnectionDoomed();
					}
				}
				else if (this._pendingCallbacks <= 1 && (this._parser.Connection == null || this._parser.Connection.IsInPool))
				{
					IntPtr zero = IntPtr.Zero;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this.SniContext = SniContext.Snix_Connect;
						uint num = SNINativeMethodWrapper.SNICheckConnection(this.Handle);
						if (num != 0U && num != 258U)
						{
							Bid.Trace("<sc.TdsParser.IsConnectionAlive|Info> received error %d on idle connection\n", (int)num);
							result = false;
							if (throwOnException)
							{
								this.AddError(this._parser.ProcessSNIError(this));
								this.ThrowExceptionAndWarning(false, false);
							}
						}
						else
						{
							this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
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
			}
			return result;
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000EFD98 File Offset: 0x000EF198
		internal bool ValidateSNIConnection()
		{
			if (this._parser == null || this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
			{
				return false;
			}
			if (DateTime.UtcNow.Ticks - this._lastSuccessfulIOTimer._value <= 50000L)
			{
				return true;
			}
			uint num = 0U;
			this.SniContext = SniContext.Snix_Connect;
			try
			{
				Interlocked.Increment(ref this._readingCount);
				SNIHandle handle = this.Handle;
				if (handle != null)
				{
					num = SNINativeMethodWrapper.SNICheckConnection(handle);
				}
			}
			finally
			{
				Interlocked.Decrement(ref this._readingCount);
			}
			return num == 0U || num == 258U;
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000EFE4C File Offset: 0x000EF24C
		private void ReadSniError(TdsParserStateObject stateObj, uint error)
		{
			if (258U == error)
			{
				bool flag = false;
				bool flag2;
				if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
				{
					flag2 = this._internalTimeout;
				}
				else
				{
					flag2 = this.IsTimeoutStateExpired;
				}
				if (flag2)
				{
					flag = true;
				}
				else
				{
					if (LocalAppContextSwitches.DisableHardenedQueryTimeouts)
					{
						stateObj._internalTimeout = true;
					}
					else
					{
						stateObj.SetTimeoutStateStopped();
					}
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U));
					if (!stateObj._attentionSent)
					{
						if (stateObj.Parser.State == TdsParserState.OpenLoggedIn)
						{
							stateObj.SendAttention(true);
							IntPtr zero = IntPtr.Zero;
							RuntimeHelpers.PrepareConstrainedRegions();
							bool flag3 = false;
							try
							{
								Interlocked.Increment(ref this._readingCount);
								flag3 = true;
								SNIHandle handle = this.Handle;
								if (handle == null)
								{
									throw ADP.ClosedConnectionError();
								}
								error = SNINativeMethodWrapper.SNIReadSyncOverAsync(handle, ref zero, stateObj.GetTimeoutRemaining());
								Interlocked.Decrement(ref this._readingCount);
								flag3 = false;
								if (error == 0U)
								{
									stateObj.ProcessSniPacket(zero, 0U);
									return;
								}
								flag = true;
								goto IL_18F;
							}
							finally
							{
								if (flag3)
								{
									Interlocked.Decrement(ref this._readingCount);
								}
								if (zero != IntPtr.Zero)
								{
									SNINativeMethodWrapper.SNIPacketRelease(zero);
								}
							}
						}
						if (this._parser._loginWithFailover)
						{
							this._parser.Disconnect();
						}
						else if (this._parser.State == TdsParserState.OpenNotLoggedIn && (this._parser.Connection.ConnectionOptions.MultiSubnetFailover || this._parser.Connection.ConnectionOptions.TransparentNetworkIPResolution))
						{
							this._parser.Disconnect();
						}
						else
						{
							flag = true;
						}
					}
				}
				IL_18F:
				if (flag)
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
				}
			}
			else
			{
				this.AddError(this._parser.ProcessSNIError(stateObj));
			}
			this.ThrowExceptionAndWarning(false, false);
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000F0040 File Offset: 0x000EF440
		public void ProcessSniPacket(IntPtr packet, uint error)
		{
			if (error != 0U)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				this.AddError(this._parser.ProcessSNIError(this));
				return;
			}
			else
			{
				uint num = 0U;
				if (SNINativeMethodWrapper.SNIPacketGetData(packet, this._inBuff, ref num) != 0U)
				{
					throw SQL.ParsingError(ParsingErrorState.ProcessSniPacketFailed);
				}
				if ((long)this._inBuff.Length < (long)((ulong)num))
				{
					throw SQL.InvalidInternalPacketSize(Res.GetString("SqlMisc_InvalidArraySizeMessage"));
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
				this._inBytesRead = (int)num;
				this._inBytesUsed = 0;
				if (this._snapshot != null)
				{
					this._snapshot.PushBuffer(this._inBuff, this._inBytesRead);
					if (this._snapshotReplay)
					{
						this._snapshot.Replay();
					}
				}
				this.SniReadStatisticsAndTracing();
				if (Bid.AdvancedOn)
				{
					Bid.TraceBin("<sc.TdsParser.ReadNetworkPacketAsyncCallback|INFO|ADV> Packet read", this._inBuff, (ushort)this._inBytesRead);
					return;
				}
				return;
			}
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x000F013C File Offset: 0x000EF53C
		private void ChangeNetworkPacketTimeout(int dueTime, int period)
		{
			Timer networkPacketTimeout = this._networkPacketTimeout;
			if (networkPacketTimeout != null)
			{
				try
				{
					networkPacketTimeout.Change(dueTime, period);
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x000F0180 File Offset: 0x000EF580
		public void ReadAsyncCallback(IntPtr key, IntPtr packet, uint error)
		{
			TaskCompletionSource<object> source = this._networkPacketTaskSource;
			if (source == null && this._parser._pMarsPhysicalConObj == this)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			bool flag = true;
			try
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(error, CallbackType.Read);
				}
				this.ChangeNetworkPacketTimeout(-1, -1);
				if (!LocalAppContextSwitches.DisableHardenedQueryTimeouts)
				{
					if (this.TimeoutHasExpired)
					{
						this.OnTimeoutSync(null);
					}
					int num = Interlocked.CompareExchange(ref this._timeoutState, 0, 1);
					if (this._timeoutState != 1)
					{
						this._timeoutIdentityValue = 0;
					}
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
				int num2 = this.DecrementPendingCallbacks(false);
				if (flag && source != null && num2 < 2)
				{
					if (error == 0U)
					{
						if (this._executionContext != null)
						{
							ExecutionContext.Run(this._executionContext, delegate(object state)
							{
								source.TrySetResult(null);
							}, null);
						}
						else
						{
							source.TrySetResult(null);
						}
					}
					else if (this._executionContext != null)
					{
						ExecutionContext.Run(this._executionContext, delegate(object state)
						{
							this.ReadAsyncCallbackCaptureException(source);
						}, null);
					}
					else
					{
						this.ReadAsyncCallbackCaptureException(source);
					}
				}
			}
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000F02E4 File Offset: 0x000EF6E4
		private void ReadAsyncCallbackCaptureException(TaskCompletionSource<object> source)
		{
			bool flag = false;
			try
			{
				if (this._hasErrorOrWarning)
				{
					this.ThrowExceptionAndWarning(false, true);
				}
				else if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					throw ADP.ClosedConnectionError();
				}
			}
			catch (Exception exception)
			{
				if (source.TrySetException(exception))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Task.Factory.StartNew(delegate()
				{
					this._parser.State = TdsParserState.Broken;
					this._parser.Connection.BreakConnection();
					source.TrySetCanceled();
				});
			}
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000F0388 File Offset: 0x000EF788
		public void WriteAsyncCallback(IntPtr key, IntPtr packet, uint sniError)
		{
			this.RemovePacketFromPendingList(packet);
			try
			{
				if (sniError != 0U)
				{
					Bid.Trace("<sc.TdsParser.WriteAsyncCallback|Info> write async returned error code %d\n", (int)sniError);
					try
					{
						this.AddError(this._parser.ProcessSNIError(this));
						this.ThrowExceptionAndWarning(false, true);
						goto IL_A4;
					}
					catch (Exception ex)
					{
						TaskCompletionSource<object> writeCompletionSource = this._writeCompletionSource;
						if (writeCompletionSource != null)
						{
							writeCompletionSource.TrySetException(ex);
						}
						else
						{
							this._delayedWriteAsyncCallbackException = ex;
							Thread.MemoryBarrier();
							writeCompletionSource = this._writeCompletionSource;
							if (writeCompletionSource != null)
							{
								Exception ex2 = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
								if (ex2 != null)
								{
									writeCompletionSource.TrySetException(ex2);
								}
							}
						}
						return;
					}
				}
				this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncWriteCount);
			}
			IL_A4:
			TaskCompletionSource<object> writeCompletionSource2 = this._writeCompletionSource;
			if (this._asyncWriteCount == 0 && writeCompletionSource2 != null)
			{
				writeCompletionSource2.TrySetResult(null);
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000F048C File Offset: 0x000EF88C
		internal void WriteSecureString(SecureString secureString)
		{
			int num = (this._securePasswords[0] != null) ? 1 : 0;
			this._securePasswords[num] = secureString;
			this._securePasswordOffsetsInBuffer[num] = this._outBytesUsed;
			int num2 = secureString.Length * 2;
			this._outBytesUsed += num2;
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000F04D8 File Offset: 0x000EF8D8
		internal void ResetSecurePasswordsInfomation()
		{
			for (int i = 0; i < this._securePasswords.Length; i++)
			{
				this._securePasswords[i] = null;
				this._securePasswordOffsetsInBuffer[i] = 0;
			}
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000F050C File Offset: 0x000EF90C
		internal Task WaitForAccumulatedWrites()
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0)
			{
				return null;
			}
			this._writeCompletionSource = new TaskCompletionSource<object>();
			Task task = this._writeCompletionSource.Task;
			Thread.MemoryBarrier();
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
			{
				task = null;
			}
			return task;
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000F05A8 File Offset: 0x000EF9A8
		internal void WriteByte(byte b)
		{
			if (this._outBytesUsed == this._outBuff.Length)
			{
				this.WritePacket(0, true);
			}
			byte[] outBuff = this._outBuff;
			int outBytesUsed = this._outBytesUsed;
			this._outBytesUsed = outBytesUsed + 1;
			outBuff[outBytesUsed] = b;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000F05E8 File Offset: 0x000EF9E8
		internal Task WriteByteArray(byte[] b, int len, int offsetBuffer, bool canAccumulate = true, TaskCompletionSource<object> completion = null)
		{
			Task result2;
			try
			{
				bool asyncWrite = this._parser._asyncWrite;
				int num = offsetBuffer;
				while (this._outBytesUsed + len > this._outBuff.Length)
				{
					int num2 = this._outBuff.Length - this._outBytesUsed;
					Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, num2);
					num += num2;
					this._outBytesUsed += num2;
					len -= num2;
					Task task = this.WritePacket(0, canAccumulate);
					if (task != null)
					{
						Task result = null;
						if (completion == null)
						{
							completion = new TaskCompletionSource<object>();
							result = completion.Task;
						}
						this.WriteByteArraySetupContinuation(b, len, completion, num, task);
						return result;
					}
					if (len <= 0)
					{
						IL_BC:
						if (completion != null)
						{
							completion.SetResult(null);
						}
						return null;
					}
				}
				Buffer.BlockCopy(b, num, this._outBuff, this._outBytesUsed, len);
				this._outBytesUsed += len;
				goto IL_BC;
			}
			catch (Exception exception)
			{
				if (completion == null)
				{
					throw;
				}
				completion.SetException(exception);
				result2 = null;
			}
			return result2;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000F06F8 File Offset: 0x000EFAF8
		private void WriteByteArraySetupContinuation(byte[] b, int len, TaskCompletionSource<object> completion, int offset, Task packetTask)
		{
			AsyncHelper.ContinueTask(packetTask, completion, delegate
			{
				this.WriteByteArray(b, len, offset, false, completion);
			}, this._parser.Connection, null, null, null, null);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000F0758 File Offset: 0x000EFB58
		internal Task WritePacket(byte flushMode, bool canAccumulate = false)
		{
			if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
			{
				throw ADP.ClosedConnectionError();
			}
			if ((this._parser.IsYukonOrNewer && !this._bulkCopyOpperationInProgress && this._outBytesUsed == this._outputHeaderLen + BitConverter.ToInt32(this._outBuff, this._outputHeaderLen) && this._outputPacketNumber == 1) || (this._outBytesUsed == this._outputHeaderLen && this._outputPacketNumber == 1))
			{
				return null;
			}
			byte outputPacketNumber = this._outputPacketNumber;
			bool flag = this._cancelled && this._parser._asyncWrite;
			byte b;
			if (flag)
			{
				b = 3;
				this._outputPacketNumber = 1;
			}
			else if (1 == flushMode)
			{
				b = 1;
				this._outputPacketNumber = 1;
			}
			else if (flushMode == 0)
			{
				b = 4;
				this._outputPacketNumber += 1;
			}
			else
			{
				b = 1;
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
			Task task = this.WriteSni(canAccumulate);
			if (flag)
			{
				task = AsyncHelper.CreateContinuationTask(task, new Action(this.CancelWritePacket), this._parser.Connection, null);
			}
			return task;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000F08CC File Offset: 0x000EFCCC
		private void CancelWritePacket()
		{
			this._parser.Connection.ThreadHasParserLockForClose = true;
			try
			{
				this.SendAttention(false);
				this.ResetCancelAndProcessAttention();
				throw SQL.OperationCancelled();
			}
			finally
			{
				this._parser.Connection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000F092C File Offset: 0x000EFD2C
		private Task SNIWritePacket(SNIHandle handle, SNIPacket packet, out uint sniError, bool canAccumulate, bool callerHasConnectionLock)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
			if (ex != null)
			{
				throw ex;
			}
			Task task = null;
			this._writeCompletionSource = null;
			IntPtr pointer = IntPtr.Zero;
			bool flag = !this._parser._asyncWrite;
			if (flag && this._asyncWriteCount > 0)
			{
				Task task2 = this.WaitForAccumulatedWrites();
				if (task2 != null)
				{
					try
					{
						task2.Wait();
					}
					catch (AggregateException ex2)
					{
						throw ex2.InnerException;
					}
				}
			}
			if (!flag)
			{
				pointer = this.AddPacketToPendingList(packet);
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				sniError = SNINativeMethodWrapper.SNIWritePacket(handle, packet, flag);
			}
			if (sniError == 997U)
			{
				Interlocked.Increment(ref this._asyncWriteCount);
				if (!canAccumulate)
				{
					this._writeCompletionSource = new TaskCompletionSource<object>();
					task = this._writeCompletionSource.Task;
					Thread.MemoryBarrier();
					ex = Interlocked.Exchange<Exception>(ref this._delayedWriteAsyncCallbackException, null);
					if (ex != null)
					{
						throw ex;
					}
					if (this._asyncWriteCount == 0 && (!task.IsCompleted || task.Exception == null))
					{
						task = null;
					}
				}
			}
			else
			{
				if (this._parser.MARSOn)
				{
					this.CheckSetResetConnectionState(sniError, CallbackType.Write);
				}
				if (sniError == 0U)
				{
					this._lastSuccessfulIOTimer._value = DateTime.UtcNow.Ticks;
					if (!flag)
					{
						this.RemovePacketFromPendingList(pointer);
					}
				}
				else
				{
					Bid.Trace("<sc.TdsParser.WritePacket|Info> write async returned error code %d\n", (int)sniError);
					this.AddError(this._parser.ProcessSNIError(this));
					this.ThrowExceptionAndWarning(callerHasConnectionLock, false);
				}
			}
			return task;
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000F0AC4 File Offset: 0x000EFEC4
		internal void SendAttention(bool mustTakeWriteLock = false)
		{
			if (!this._attentionSent)
			{
				if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
				{
					return;
				}
				SNIPacket snipacket = new SNIPacket(this.Handle);
				this._sniAsyncAttnPacket = snipacket;
				SNINativeMethodWrapper.SNIPacketSetData(snipacket, SQL.AttentionHeader, 8, null, null);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._attentionSending = true;
					bool flag = false;
					if (mustTakeWriteLock && !this._parser.Connection.ThreadHasParserLockForClose)
					{
						flag = true;
						this._parser.Connection._parserLock.Wait(false);
						this._parser.Connection.ThreadHasParserLockForClose = true;
					}
					try
					{
						if (this._parser.State == TdsParserState.Closed || this._parser.State == TdsParserState.Broken)
						{
							return;
						}
						this._parser._asyncWrite = false;
						uint num;
						this.SNIWritePacket(this.Handle, snipacket, out num, false, false);
						Bid.Trace("<sc.TdsParser.SendAttention|Info> Send Attention ASync .\n");
					}
					finally
					{
						if (flag)
						{
							this._parser.Connection.ThreadHasParserLockForClose = false;
							this._parser.Connection._parserLock.Release();
						}
					}
					this.SetTimeoutSeconds(5);
					this._attentionSent = true;
				}
				finally
				{
					this._attentionSending = false;
				}
				if (Bid.AdvancedOn)
				{
					Bid.TraceBin("<sc.TdsParser.WritePacket|INFO|ADV>  Packet sent", this._outBuff, (ushort)this._outBytesUsed);
				}
				Bid.Trace("<sc.TdsParser.SendAttention|Info> Attention sent to the server.\n");
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000F0C58 File Offset: 0x000F0058
		private Task WriteSni(bool canAccumulate)
		{
			SNIPacket resetWritePacket = this.GetResetWritePacket();
			SNINativeMethodWrapper.SNIPacketSetData(resetWritePacket, this._outBuff, this._outBytesUsed, this._securePasswords, this._securePasswordOffsetsInBuffer);
			uint num;
			Task result = this.SNIWritePacket(this.Handle, resetWritePacket, out num, canAccumulate, true);
			if (this._bulkCopyOpperationInProgress && this.GetTimeoutRemaining() == 0)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this.AddError(new SqlError(-2, 0, 11, this._parser.Server, this._parser.Connection.TimeoutErrorInternal.GetErrorMessage(), "", 0, 258U));
					this._bulkCopyWriteTimeout = true;
					this.SendAttention(false);
					this._parser.ProcessPendingAck(this);
					this.ThrowExceptionAndWarning(false, false);
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
			if (this._parser.State == TdsParserState.OpenNotLoggedIn && this._parser.EncryptionOptions == EncryptionOptions.LOGIN)
			{
				this._parser.RemoveEncryption();
				this._parser.EncryptionOptions = EncryptionOptions.OFF;
				this.ClearAllWritePackets();
			}
			this.SniWriteStatisticsAndTracing();
			this.ResetBuffer();
			return result;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000F0D98 File Offset: 0x000F0198
		internal SNIPacket GetResetWritePacket()
		{
			if (this._sniPacket != null)
			{
				SNINativeMethodWrapper.SNIPacketReset(this.Handle, SNINativeMethodWrapper.IOType.WRITE, this._sniPacket, SNINativeMethodWrapper.ConsumerNumber.SNI_Consumer_SNI);
			}
			else
			{
				object writePacketLockObject = this._writePacketLockObject;
				lock (writePacketLockObject)
				{
					this._sniPacket = this._writePacketCache.Take(this.Handle);
				}
			}
			return this._sniPacket;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000F0E18 File Offset: 0x000F0218
		internal void ClearAllWritePackets()
		{
			if (this._sniPacket != null)
			{
				this._sniPacket.Dispose();
				this._sniPacket = null;
			}
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._writePacketCache.Clear();
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000F0E84 File Offset: 0x000F0284
		private IntPtr AddPacketToPendingList(SNIPacket packet)
		{
			this._sniPacket = null;
			IntPtr intPtr = packet.DangerousGetHandle();
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				this._pendingWritePackets.Add(intPtr, packet);
			}
			return intPtr;
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000F0EE8 File Offset: 0x000F02E8
		private void RemovePacketFromPendingList(IntPtr pointer)
		{
			object writePacketLockObject = this._writePacketLockObject;
			lock (writePacketLockObject)
			{
				SNIPacket packet;
				if (this._pendingWritePackets.TryGetValue(pointer, out packet))
				{
					this._pendingWritePackets.Remove(pointer);
					this._writePacketCache.Add(packet);
				}
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000F0F58 File Offset: 0x000F0358
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

		// Token: 0x060022BD RID: 8893 RVA: 0x000F0FB0 File Offset: 0x000F03B0
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

		// Token: 0x060022BE RID: 8894 RVA: 0x000F108C File Offset: 0x000F048C
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

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x000F1124 File Offset: 0x000F0524
		internal bool HasErrorOrWarning
		{
			get
			{
				return this._hasErrorOrWarning;
			}
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000F1138 File Offset: 0x000F0538
		internal void AddError(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				this._errors.Add(error);
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000F11AC File Offset: 0x000F05AC
		internal int ErrorCount
		{
			get
			{
				int result = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._errors != null)
					{
						result = this._errors.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000F120C File Offset: 0x000F060C
		internal void AddWarning(SqlError error)
		{
			this._syncOverAsync = true;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = true;
				if (this._warnings == null)
				{
					this._warnings = new SqlErrorCollection();
				}
				this._warnings.Add(error);
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000F1280 File Offset: 0x000F0680
		internal int WarningCount
		{
			get
			{
				int result = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._warnings != null)
					{
						result = this._warnings.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x000F12E0 File Offset: 0x000F06E0
		internal int PreAttentionErrorCount
		{
			get
			{
				int result = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._preAttentionErrors != null)
					{
						result = this._preAttentionErrors.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x000F1340 File Offset: 0x000F0740
		internal int PreAttentionWarningCount
		{
			get
			{
				int result = 0;
				object errorAndWarningsLock = this._errorAndWarningsLock;
				lock (errorAndWarningsLock)
				{
					if (this._preAttentionWarnings != null)
					{
						result = this._preAttentionWarnings.Count;
					}
				}
				return result;
			}
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000F13A0 File Offset: 0x000F07A0
		internal SqlErrorCollection GetFullErrorAndWarningCollection(out bool broken)
		{
			SqlErrorCollection result = new SqlErrorCollection();
			broken = false;
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this.AddErrorsToCollection(this._errors, ref result, ref broken);
				this.AddErrorsToCollection(this._warnings, ref result, ref broken);
				this._errors = null;
				this._warnings = null;
				this.AddErrorsToCollection(this._preAttentionErrors, ref result, ref broken);
				this.AddErrorsToCollection(this._preAttentionWarnings, ref result, ref broken);
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
			return result;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000F1450 File Offset: 0x000F0850
		private void AddErrorsToCollection(SqlErrorCollection inCollection, ref SqlErrorCollection collectionToAddTo, ref bool broken)
		{
			if (inCollection != null)
			{
				foreach (object obj in inCollection)
				{
					SqlError sqlError = (SqlError)obj;
					collectionToAddTo.Add(sqlError);
					broken |= (sqlError.Class >= 20);
				}
			}
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000F14C8 File Offset: 0x000F08C8
		internal void StoreErrorAndWarningForAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = false;
				this._preAttentionErrors = this._errors;
				this._preAttentionWarnings = this._warnings;
				this._errors = null;
				this._warnings = null;
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000F153C File Offset: 0x000F093C
		internal void RestoreErrorAndWarningAfterAttention()
		{
			object errorAndWarningsLock = this._errorAndWarningsLock;
			lock (errorAndWarningsLock)
			{
				this._hasErrorOrWarning = ((this._preAttentionErrors != null && this._preAttentionErrors.Count > 0) || (this._preAttentionWarnings != null && this._preAttentionWarnings.Count > 0));
				this._errors = this._preAttentionErrors;
				this._warnings = this._preAttentionWarnings;
				this._preAttentionErrors = null;
				this._preAttentionWarnings = null;
			}
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000F15E0 File Offset: 0x000F09E0
		internal void CheckThrowSNIException()
		{
			if (this.HasErrorOrWarning)
			{
				this.ThrowExceptionAndWarning(false, false);
			}
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000F1600 File Offset: 0x000F0A00
		[Conditional("DEBUG")]
		internal void AssertStateIsClean()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State != TdsParserState.Closed && parser.State != TdsParserState.Broken)
			{
				bool disableHardenedQueryTimeouts = LocalAppContextSwitches.DisableHardenedQueryTimeouts;
			}
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000F1630 File Offset: 0x000F0A30
		internal void CloneCleanupAltMetaDataSetArray()
		{
			if (this._snapshot != null)
			{
				this._snapshot.CloneCleanupAltMetaDataSetArray();
			}
		}

		// Token: 0x040014C0 RID: 5312
		private const int AttentionTimeoutSeconds = 5;

		// Token: 0x040014C1 RID: 5313
		private const long CheckConnectionWindow = 50000L;

		// Token: 0x040014C2 RID: 5314
		private static int _objectTypeCount;

		// Token: 0x040014C3 RID: 5315
		internal readonly int _objectID = Interlocked.Increment(ref TdsParserStateObject._objectTypeCount);

		// Token: 0x040014C4 RID: 5316
		private readonly TdsParser _parser;

		// Token: 0x040014C5 RID: 5317
		private SNIHandle _sessionHandle;

		// Token: 0x040014C6 RID: 5318
		private readonly WeakReference _owner = new WeakReference(null);

		// Token: 0x040014C7 RID: 5319
		internal SqlDataReader.SharedState _readerState;

		// Token: 0x040014C8 RID: 5320
		private int _activateCount;

		// Token: 0x040014C9 RID: 5321
		internal readonly int _inputHeaderLen = 8;

		// Token: 0x040014CA RID: 5322
		internal readonly int _outputHeaderLen = 8;

		// Token: 0x040014CB RID: 5323
		internal byte[] _outBuff;

		// Token: 0x040014CC RID: 5324
		internal int _outBytesUsed = 8;

		// Token: 0x040014CD RID: 5325
		private byte[] _inBuff;

		// Token: 0x040014CE RID: 5326
		internal int _inBytesUsed;

		// Token: 0x040014CF RID: 5327
		internal int _inBytesRead;

		// Token: 0x040014D0 RID: 5328
		internal int _inBytesPacket;

		// Token: 0x040014D1 RID: 5329
		internal byte _outputMessageType;

		// Token: 0x040014D2 RID: 5330
		internal byte _messageStatus;

		// Token: 0x040014D3 RID: 5331
		internal byte _outputPacketNumber = 1;

		// Token: 0x040014D4 RID: 5332
		internal bool _pendingData;

		// Token: 0x040014D5 RID: 5333
		internal volatile bool _fResetEventOwned;

		// Token: 0x040014D6 RID: 5334
		internal volatile bool _fResetConnectionSent;

		// Token: 0x040014D7 RID: 5335
		internal bool _errorTokenReceived;

		// Token: 0x040014D8 RID: 5336
		internal bool _bulkCopyOpperationInProgress;

		// Token: 0x040014D9 RID: 5337
		internal bool _bulkCopyWriteTimeout;

		// Token: 0x040014DA RID: 5338
		private SNIPacket _sniPacket;

		// Token: 0x040014DB RID: 5339
		internal SNIPacket _sniAsyncAttnPacket;

		// Token: 0x040014DC RID: 5340
		private WritePacketCache _writePacketCache = new WritePacketCache();

		// Token: 0x040014DD RID: 5341
		private Dictionary<IntPtr, SNIPacket> _pendingWritePackets = new Dictionary<IntPtr, SNIPacket>();

		// Token: 0x040014DE RID: 5342
		private object _writePacketLockObject = new object();

		// Token: 0x040014DF RID: 5343
		private GCHandle _gcHandle;

		// Token: 0x040014E0 RID: 5344
		private int _pendingCallbacks;

		// Token: 0x040014E1 RID: 5345
		private long _timeoutMilliseconds;

		// Token: 0x040014E2 RID: 5346
		private long _timeoutTime;

		// Token: 0x040014E3 RID: 5347
		private int _timeoutState;

		// Token: 0x040014E4 RID: 5348
		private int _timeoutIdentitySource;

		// Token: 0x040014E5 RID: 5349
		private volatile int _timeoutIdentityValue;

		// Token: 0x040014E6 RID: 5350
		internal volatile bool _attentionSent;

		// Token: 0x040014E7 RID: 5351
		internal bool _attentionReceived;

		// Token: 0x040014E8 RID: 5352
		internal volatile bool _attentionSending;

		// Token: 0x040014E9 RID: 5353
		internal bool _internalTimeout;

		// Token: 0x040014EA RID: 5354
		internal bool _enforceTimeoutDelay;

		// Token: 0x040014EB RID: 5355
		internal int _enforcedTimeoutDelayInMilliSeconds = 5000;

		// Token: 0x040014EC RID: 5356
		private readonly LastIOTimer _lastSuccessfulIOTimer;

		// Token: 0x040014ED RID: 5357
		private SecureString[] _securePasswords = new SecureString[2];

		// Token: 0x040014EE RID: 5358
		private int[] _securePasswordOffsetsInBuffer = new int[2];

		// Token: 0x040014EF RID: 5359
		private bool _cancelled;

		// Token: 0x040014F0 RID: 5360
		private const int _waitForCancellationLockPollTimeout = 100;

		// Token: 0x040014F1 RID: 5361
		private volatile int _allowObjectID;

		// Token: 0x040014F2 RID: 5362
		internal bool _hasOpenResult;

		// Token: 0x040014F3 RID: 5363
		internal SqlInternalTransaction _executedUnderTransaction;

		// Token: 0x040014F4 RID: 5364
		internal ulong _longlen;

		// Token: 0x040014F5 RID: 5365
		internal ulong _longlenleft;

		// Token: 0x040014F6 RID: 5366
		internal int[] _decimalBits;

		// Token: 0x040014F7 RID: 5367
		internal byte[] _bTmp = new byte[12];

		// Token: 0x040014F8 RID: 5368
		internal int _bTmpRead;

		// Token: 0x040014F9 RID: 5369
		internal Decoder _plpdecoder;

		// Token: 0x040014FA RID: 5370
		internal bool _accumulateInfoEvents;

		// Token: 0x040014FB RID: 5371
		internal List<SqlError> _pendingInfoEvents;

		// Token: 0x040014FC RID: 5372
		internal byte[] _bLongBytes;

		// Token: 0x040014FD RID: 5373
		internal byte[] _bIntBytes;

		// Token: 0x040014FE RID: 5374
		internal byte[] _bShortBytes;

		// Token: 0x040014FF RID: 5375
		internal byte[] _bDecimalBytes;

		// Token: 0x04001500 RID: 5376
		private byte[] _partialHeaderBuffer = new byte[8];

		// Token: 0x04001501 RID: 5377
		internal int _partialHeaderBytesRead;

		// Token: 0x04001502 RID: 5378
		internal _SqlMetaDataSet _cleanupMetaData;

		// Token: 0x04001503 RID: 5379
		internal _SqlMetaDataSetCollection _cleanupAltMetaDataSetArray;

		// Token: 0x04001504 RID: 5380
		internal int _tracePasswordOffset;

		// Token: 0x04001505 RID: 5381
		internal int _tracePasswordLength;

		// Token: 0x04001506 RID: 5382
		internal int _traceChangePasswordOffset;

		// Token: 0x04001507 RID: 5383
		internal int _traceChangePasswordLength;

		// Token: 0x04001508 RID: 5384
		internal bool _receivedColMetaData;

		// Token: 0x04001509 RID: 5385
		private SniContext _sniContext;

		// Token: 0x0400150A RID: 5386
		private bool _bcpLock;

		// Token: 0x0400150B RID: 5387
		private TdsParserStateObject.NullBitmap _nullBitmapInfo;

		// Token: 0x0400150C RID: 5388
		internal TaskCompletionSource<object> _networkPacketTaskSource;

		// Token: 0x0400150D RID: 5389
		private Timer _networkPacketTimeout;

		// Token: 0x0400150E RID: 5390
		internal bool _syncOverAsync = true;

		// Token: 0x0400150F RID: 5391
		private bool _snapshotReplay;

		// Token: 0x04001510 RID: 5392
		private TdsParserStateObject.StateSnapshot _snapshot;

		// Token: 0x04001511 RID: 5393
		internal ExecutionContext _executionContext;

		// Token: 0x04001512 RID: 5394
		internal bool _asyncReadWithoutSnapshot;

		// Token: 0x04001513 RID: 5395
		internal SqlErrorCollection _errors;

		// Token: 0x04001514 RID: 5396
		internal SqlErrorCollection _warnings;

		// Token: 0x04001515 RID: 5397
		internal object _errorAndWarningsLock = new object();

		// Token: 0x04001516 RID: 5398
		private bool _hasErrorOrWarning;

		// Token: 0x04001517 RID: 5399
		internal SqlErrorCollection _preAttentionErrors;

		// Token: 0x04001518 RID: 5400
		internal SqlErrorCollection _preAttentionWarnings;

		// Token: 0x04001519 RID: 5401
		private volatile TaskCompletionSource<object> _writeCompletionSource;

		// Token: 0x0400151A RID: 5402
		private volatile int _asyncWriteCount;

		// Token: 0x0400151B RID: 5403
		private volatile Exception _delayedWriteAsyncCallbackException;

		// Token: 0x0400151C RID: 5404
		private int _readingCount;

		// Token: 0x020003F3 RID: 1011
		private sealed class TimeoutState
		{
			// Token: 0x060035A6 RID: 13734 RVA: 0x00146720 File Offset: 0x00145B20
			public TimeoutState(int value)
			{
				this._value = value;
			}

			// Token: 0x17000862 RID: 2146
			// (get) Token: 0x060035A7 RID: 13735 RVA: 0x0014673C File Offset: 0x00145B3C
			public int IdentityValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x04002183 RID: 8579
			public const int Stopped = 0;

			// Token: 0x04002184 RID: 8580
			public const int Running = 1;

			// Token: 0x04002185 RID: 8581
			public const int ExpiredAsync = 2;

			// Token: 0x04002186 RID: 8582
			public const int ExpiredSync = 3;

			// Token: 0x04002187 RID: 8583
			private readonly int _value;
		}

		// Token: 0x020003F4 RID: 1012
		private struct NullBitmap
		{
			// Token: 0x060035A8 RID: 13736 RVA: 0x00146750 File Offset: 0x00145B50
			internal bool TryInitialize(TdsParserStateObject stateObj, int columnsCount)
			{
				this._columnsCount = columnsCount;
				int num = (columnsCount + 7) / 8;
				if (this._nullBitmap == null || this._nullBitmap.Length != num)
				{
					this._nullBitmap = new byte[num];
				}
				if (!stateObj.TryReadByteArray(this._nullBitmap, 0, this._nullBitmap.Length))
				{
					return false;
				}
				if (Bid.TraceOn)
				{
					Bid.Trace("<sc.TdsParserStateObject.NullBitmap.Initialize|INFO|ADV> %d#, NBCROW bitmap received, column count = %d\n", stateObj.ObjectID, columnsCount);
					Bid.TraceBin("<sc.TdsParserStateObject.NullBitmap.Initialize|INFO|ADV> NBCROW bitmap data: ", this._nullBitmap, (ushort)this._nullBitmap.Length);
				}
				return true;
			}

			// Token: 0x060035A9 RID: 13737 RVA: 0x001467D4 File Offset: 0x00145BD4
			internal bool ReferenceEquals(TdsParserStateObject.NullBitmap obj)
			{
				return this._nullBitmap == obj._nullBitmap;
			}

			// Token: 0x060035AA RID: 13738 RVA: 0x001467F0 File Offset: 0x00145BF0
			internal TdsParserStateObject.NullBitmap Clone()
			{
				return new TdsParserStateObject.NullBitmap
				{
					_nullBitmap = ((this._nullBitmap == null) ? null : ((byte[])this._nullBitmap.Clone())),
					_columnsCount = this._columnsCount
				};
			}

			// Token: 0x060035AB RID: 13739 RVA: 0x00146838 File Offset: 0x00145C38
			internal void Clean()
			{
				this._columnsCount = 0;
			}

			// Token: 0x060035AC RID: 13740 RVA: 0x0014684C File Offset: 0x00145C4C
			internal bool IsGuaranteedNull(int columnOrdinal)
			{
				if (this._columnsCount == 0)
				{
					return false;
				}
				byte b = (byte)(1 << (columnOrdinal & 7));
				byte b2 = this._nullBitmap[columnOrdinal >> 3];
				return (b & b2) > 0;
			}

			// Token: 0x04002188 RID: 8584
			private byte[] _nullBitmap;

			// Token: 0x04002189 RID: 8585
			private int _columnsCount;
		}

		// Token: 0x020003F5 RID: 1013
		private class PacketData
		{
			// Token: 0x0400218A RID: 8586
			public byte[] Buffer;

			// Token: 0x0400218B RID: 8587
			public int Read;
		}

		// Token: 0x020003F6 RID: 1014
		private class StateSnapshot
		{
			// Token: 0x060035AE RID: 13742 RVA: 0x00146894 File Offset: 0x00145C94
			public StateSnapshot(TdsParserStateObject state)
			{
				this._snapshotInBuffs = new List<TdsParserStateObject.PacketData>();
				this._stateObj = state;
			}

			// Token: 0x060035AF RID: 13743 RVA: 0x001468BC File Offset: 0x00145CBC
			internal void CloneNullBitmapInfo()
			{
				if (this._stateObj._nullBitmapInfo.ReferenceEquals(this._snapshotNullBitmapInfo))
				{
					this._stateObj._nullBitmapInfo = this._stateObj._nullBitmapInfo.Clone();
				}
			}

			// Token: 0x060035B0 RID: 13744 RVA: 0x001468FC File Offset: 0x00145CFC
			internal void CloneCleanupAltMetaDataSetArray()
			{
				if (this._stateObj._cleanupAltMetaDataSetArray != null && this._snapshotCleanupAltMetaDataSetArray == this._stateObj._cleanupAltMetaDataSetArray)
				{
					this._stateObj._cleanupAltMetaDataSetArray = (_SqlMetaDataSetCollection)this._stateObj._cleanupAltMetaDataSetArray.Clone();
				}
			}

			// Token: 0x060035B1 RID: 13745 RVA: 0x0014694C File Offset: 0x00145D4C
			internal void PushBuffer(byte[] buffer, int read)
			{
				TdsParserStateObject.PacketData packetData = new TdsParserStateObject.PacketData();
				packetData.Buffer = buffer;
				packetData.Read = read;
				this._snapshotInBuffs.Add(packetData);
			}

			// Token: 0x060035B2 RID: 13746 RVA: 0x0014697C File Offset: 0x00145D7C
			internal bool Replay()
			{
				if (this._snapshotInBuffCurrent < this._snapshotInBuffs.Count)
				{
					TdsParserStateObject.PacketData packetData = this._snapshotInBuffs[this._snapshotInBuffCurrent];
					this._stateObj._inBuff = packetData.Buffer;
					this._stateObj._inBytesUsed = 0;
					this._stateObj._inBytesRead = packetData.Read;
					this._snapshotInBuffCurrent++;
					return true;
				}
				return false;
			}

			// Token: 0x060035B3 RID: 13747 RVA: 0x001469F0 File Offset: 0x00145DF0
			internal void Snap()
			{
				this._snapshotInBuffs.Clear();
				this._snapshotInBuffCurrent = 0;
				this._snapshotInBytesUsed = this._stateObj._inBytesUsed;
				this._snapshotInBytesPacket = this._stateObj._inBytesPacket;
				this._snapshotPendingData = this._stateObj._pendingData;
				this._snapshotErrorTokenReceived = this._stateObj._errorTokenReceived;
				this._snapshotMessageStatus = this._stateObj._messageStatus;
				this._snapshotNullBitmapInfo = this._stateObj._nullBitmapInfo;
				this._snapshotLongLen = this._stateObj._longlen;
				this._snapshotLongLenLeft = this._stateObj._longlenleft;
				this._snapshotCleanupMetaData = this._stateObj._cleanupMetaData;
				this._snapshotCleanupAltMetaDataSetArray = this._stateObj._cleanupAltMetaDataSetArray;
				this._snapshotHasOpenResult = this._stateObj._hasOpenResult;
				this._snapshotReceivedColumnMetadata = this._stateObj._receivedColMetaData;
				this._snapshotAttentionReceived = this._stateObj._attentionReceived;
				this.PushBuffer(this._stateObj._inBuff, this._stateObj._inBytesRead);
			}

			// Token: 0x060035B4 RID: 13748 RVA: 0x00146B08 File Offset: 0x00145F08
			internal void ResetSnapshotState()
			{
				this._snapshotInBuffCurrent = 0;
				this.Replay();
				this._stateObj._inBytesUsed = this._snapshotInBytesUsed;
				this._stateObj._inBytesPacket = this._snapshotInBytesPacket;
				this._stateObj._pendingData = this._snapshotPendingData;
				this._stateObj._errorTokenReceived = this._snapshotErrorTokenReceived;
				this._stateObj._messageStatus = this._snapshotMessageStatus;
				this._stateObj._nullBitmapInfo = this._snapshotNullBitmapInfo;
				this._stateObj._cleanupMetaData = this._snapshotCleanupMetaData;
				this._stateObj._cleanupAltMetaDataSetArray = this._snapshotCleanupAltMetaDataSetArray;
				this._stateObj._hasOpenResult = this._snapshotHasOpenResult;
				this._stateObj._receivedColMetaData = this._snapshotReceivedColumnMetadata;
				this._stateObj._attentionReceived = this._snapshotAttentionReceived;
				this._stateObj._bTmpRead = 0;
				this._stateObj._partialHeaderBytesRead = 0;
				this._stateObj._longlen = this._snapshotLongLen;
				this._stateObj._longlenleft = this._snapshotLongLenLeft;
				this._stateObj._snapshotReplay = true;
			}

			// Token: 0x060035B5 RID: 13749 RVA: 0x00146C24 File Offset: 0x00146024
			internal void PrepareReplay()
			{
				this.ResetSnapshotState();
			}

			// Token: 0x0400218C RID: 8588
			private List<TdsParserStateObject.PacketData> _snapshotInBuffs;

			// Token: 0x0400218D RID: 8589
			private int _snapshotInBuffCurrent;

			// Token: 0x0400218E RID: 8590
			private int _snapshotInBytesUsed;

			// Token: 0x0400218F RID: 8591
			private int _snapshotInBytesPacket;

			// Token: 0x04002190 RID: 8592
			private bool _snapshotPendingData;

			// Token: 0x04002191 RID: 8593
			private bool _snapshotErrorTokenReceived;

			// Token: 0x04002192 RID: 8594
			private bool _snapshotHasOpenResult;

			// Token: 0x04002193 RID: 8595
			private bool _snapshotReceivedColumnMetadata;

			// Token: 0x04002194 RID: 8596
			private bool _snapshotAttentionReceived;

			// Token: 0x04002195 RID: 8597
			private byte _snapshotMessageStatus;

			// Token: 0x04002196 RID: 8598
			private TdsParserStateObject.NullBitmap _snapshotNullBitmapInfo;

			// Token: 0x04002197 RID: 8599
			private ulong _snapshotLongLen;

			// Token: 0x04002198 RID: 8600
			private ulong _snapshotLongLenLeft;

			// Token: 0x04002199 RID: 8601
			private _SqlMetaDataSet _snapshotCleanupMetaData;

			// Token: 0x0400219A RID: 8602
			private _SqlMetaDataSetCollection _snapshotCleanupAltMetaDataSetArray;

			// Token: 0x0400219B RID: 8603
			private readonly TdsParserStateObject _stateObj;
		}
	}
}
