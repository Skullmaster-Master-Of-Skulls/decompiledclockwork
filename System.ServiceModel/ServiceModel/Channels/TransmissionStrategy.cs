using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000960 RID: 2400
	internal sealed class TransmissionStrategy
	{
		// Token: 0x06005D12 RID: 23826 RVA: 0x00157A44 File Offset: 0x00155C44
		public TransmissionStrategy(ReliableMessagingVersion reliableMessagingVersion, TimeSpan initRtt, int maxWindowSize, bool requestAcks, UniqueId id)
		{
			if (initRtt < TimeSpan.Zero)
			{
				if (DiagnosticUtility.ShouldTrace(TraceEventType.Warning))
				{
					TraceUtility.TraceEvent(TraceEventType.Warning, 262254, SR.GetString("TraceCodeWsrmNegativeElapsedTimeDetected"), this);
				}
				initRtt = ReliableMessagingConstants.UnknownInitiationTime;
			}
			if (maxWindowSize <= 0)
			{
				throw Fx.AssertAndThrow("Argument maxWindow size must be positive.");
			}
			this.id = id;
			this.lossWindowSize = maxWindowSize;
			this.maxWindowSize = maxWindowSize;
			this.meanRtt = Math.Min((long)initRtt.TotalMilliseconds, 24019198012642645L) << 7;
			this.serrRtt = this.meanRtt >> 1;
			this.window = new TransmissionStrategy.SlidingWindow(maxWindowSize);
			this.slowStartThreshold = maxWindowSize;
			this.timeout = Math.Max(51200L + this.meanRtt, this.meanRtt + (this.serrRtt << 2));
			this.quotaRemaining = int.MaxValue;
			this.retryTimer = new IOThreadTimer(new Action<object>(this.OnRetryElapsed), null, true);
			this.requestAcks = requestAcks;
			this.reliableMessagingVersion = reliableMessagingVersion;
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06005D13 RID: 23827 RVA: 0x00157B80 File Offset: 0x00155D80
		public bool DoneTransmitting
		{
			get
			{
				return this.last != 0L && this.windowStart == this.last + 1L;
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x00157B9D File Offset: 0x00155D9D
		public bool HasPending
		{
			get
			{
				return this.window.Count > 0 || this.waitQueue.Count > 0;
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06005D15 RID: 23829 RVA: 0x00157BBD File Offset: 0x00155DBD
		public long Last
		{
			get
			{
				return this.last;
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06005D16 RID: 23830 RVA: 0x00157BC5 File Offset: 0x00155DC5
		private static long Now
		{
			get
			{
				return Ticks.Now / 10000L << 7;
			}
		}

		// Token: 0x17001641 RID: 5697
		// (set) Token: 0x06005D17 RID: 23831 RVA: 0x00157BD5 File Offset: 0x00155DD5
		public ComponentExceptionHandler OnException
		{
			set
			{
				this.onException = value;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (set) Token: 0x06005D18 RID: 23832 RVA: 0x00157BDE File Offset: 0x00155DDE
		public RetryHandler RetryTimeoutElapsed
		{
			set
			{
				this.retryTimeoutElapsedHandler = value;
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x00157BE7 File Offset: 0x00155DE7
		public int QuotaRemaining
		{
			get
			{
				return this.quotaRemaining;
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06005D1A RID: 23834 RVA: 0x00157BEF File Offset: 0x00155DEF
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06005D1B RID: 23835 RVA: 0x00157BF7 File Offset: 0x00155DF7
		public int Timeout
		{
			get
			{
				return (int)(this.timeout >> 7);
			}
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x00157C04 File Offset: 0x00155E04
		public void Abort(ChannelBase channel)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.aborted = true;
				if (!this.closed)
				{
					this.closed = true;
					this.retryTimer.Cancel();
					while (this.waitQueue.Count > 0)
					{
						this.waitQueue.Dequeue().Abort(channel);
					}
					this.window.Close();
				}
			}
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x00157C90 File Offset: 0x00155E90
		public bool Add(Message message, TimeSpan timeout, object state, out MessageAttemptInfo attemptInfo)
		{
			return this.InternalAdd(message, false, timeout, state, out attemptInfo);
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x00157CA0 File Offset: 0x00155EA0
		public MessageAttemptInfo AddLast(Message message, TimeSpan timeout, object state)
		{
			if (this.reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				throw Fx.AssertAndThrow("Last message supported only in February 2005.");
			}
			MessageAttemptInfo result = default(MessageAttemptInfo);
			this.InternalAdd(message, true, timeout, state, out result);
			return result;
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x00157CDC File Offset: 0x00155EDC
		private MessageAttemptInfo AddToWindow(Message message, bool isLast, object state)
		{
			MessageAttemptInfo result = default(MessageAttemptInfo);
			long num = this.windowStart + (long)this.window.Count;
			WsrmUtilities.AddSequenceHeader(this.reliableMessagingVersion, message, this.id, num, isLast);
			if (this.requestAcks && (this.window.Count == this.windowSize - 1 || this.quotaRemaining == 1))
			{
				message.Properties.AllowOutputBatching = false;
				WsrmUtilities.AddAckRequestedHeader(this.reliableMessagingVersion, message, this.id);
			}
			if (this.window.Count == 0)
			{
				this.retryTimer.Set(this.Timeout);
			}
			this.window.Add(message, TransmissionStrategy.Now, state);
			this.quotaRemaining--;
			if (isLast)
			{
				this.last = num;
			}
			int index = (int)(num - this.windowStart);
			result = new MessageAttemptInfo(this.window.GetMessage(index), num, 0, state);
			return result;
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x00157DC6 File Offset: 0x00155FC6
		public IAsyncResult BeginAdd(Message message, TimeSpan timeout, object state, AsyncCallback callback, object asyncState)
		{
			return this.InternalBeginAdd(message, false, timeout, state, callback, asyncState);
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x00157DD6 File Offset: 0x00155FD6
		public IAsyncResult BeginAddLast(Message message, TimeSpan timeout, object state, AsyncCallback callback, object asyncState)
		{
			if (this.reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				throw Fx.AssertAndThrow("Last message supported only in February 2005.");
			}
			return this.InternalBeginAdd(message, true, timeout, state, callback, asyncState);
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x00157DFE File Offset: 0x00155FFE
		private bool CanAdd()
		{
			return this.window.Count < this.windowSize && this.quotaRemaining > 0 && this.waitQueue.Count == 0;
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x00157E2C File Offset: 0x0015602C
		public void Close()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (!this.closed)
				{
					this.closed = true;
					this.retryTimer.Cancel();
					if (this.waitQueue.Count != 0)
					{
						throw Fx.AssertAndThrow("The reliable channel must throw prior to the call to Close() if there are outstanding send or request operations.");
					}
					this.window.Close();
				}
			}
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x00157EA8 File Offset: 0x001560A8
		public void DequeuePending()
		{
			Queue<TransmissionStrategy.IQueueAdder> queue = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.closed || this.waitQueue.Count == 0)
				{
					return;
				}
				int num = Math.Min(this.windowSize, this.quotaRemaining) - this.window.Count;
				if (num <= 0)
				{
					return;
				}
				num = Math.Min(num, this.waitQueue.Count);
				queue = new Queue<TransmissionStrategy.IQueueAdder>(num);
				while (num-- > 0)
				{
					TransmissionStrategy.IQueueAdder queueAdder = this.waitQueue.Dequeue();
					queueAdder.Complete0();
					queue.Enqueue(queueAdder);
				}
				goto IL_A7;
			}
			IL_9C:
			queue.Dequeue().Complete1();
			IL_A7:
			if (queue.Count > 0)
			{
				goto IL_9C;
			}
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x00157F78 File Offset: 0x00156178
		public bool EndAdd(IAsyncResult result, out MessageAttemptInfo attemptInfo)
		{
			return this.InternalEndAdd(result, out attemptInfo);
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x00157F84 File Offset: 0x00156184
		public MessageAttemptInfo EndAddLast(IAsyncResult result)
		{
			MessageAttemptInfo result2 = default(MessageAttemptInfo);
			this.InternalEndAdd(result, out result2);
			return result2;
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x00157FA4 File Offset: 0x001561A4
		private bool IsAddValid()
		{
			return !this.aborted && !this.closed;
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x00157FBC File Offset: 0x001561BC
		public void OnRetryElapsed(object state)
		{
			try
			{
				MessageAttemptInfo attemptInfo = default(MessageAttemptInfo);
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.closed)
					{
						return;
					}
					if (this.window.Count == 0)
					{
						return;
					}
					this.window.RecordRetry(0, TransmissionStrategy.Now);
					this.congestionControlModeAcks = 0;
					this.slowStartThreshold = Math.Max(1, this.windowSize >> 1);
					this.lossWindowSize = this.windowSize;
					this.windowSize = 1;
					this.timeout <<= 1;
					this.startup = false;
					attemptInfo = new MessageAttemptInfo(this.window.GetMessage(0), this.windowStart, this.window.GetRetryCount(0), this.window.GetState(0));
				}
				this.retryTimeoutElapsedHandler(attemptInfo);
				object obj2 = this.ThisLock;
				lock (obj2)
				{
					if (!this.closed && this.window.Count > 0)
					{
						this.retryTimer.Set(this.Timeout);
					}
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				this.onException(exception);
			}
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x00158150 File Offset: 0x00156350
		public void Fault(ChannelBase channel)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (!this.closed)
				{
					this.closed = true;
					this.retryTimer.Cancel();
					while (this.waitQueue.Count > 0)
					{
						this.waitQueue.Dequeue().Fault(channel);
					}
					this.window.Close();
				}
			}
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x001581D4 File Offset: 0x001563D4
		public MessageAttemptInfo GetMessageInfoForRetry(bool remove)
		{
			object obj = this.ThisLock;
			MessageAttemptInfo messageAttemptInfo;
			lock (obj)
			{
				if (this.closed)
				{
					messageAttemptInfo = default(MessageAttemptInfo);
					messageAttemptInfo = messageAttemptInfo;
				}
				else
				{
					if (remove)
					{
						if (this.retransmissionWindow.Count == 0)
						{
							throw Fx.AssertAndThrow("The caller is not allowed to remove a message attempt when there are no message attempts.");
						}
						this.retransmissionWindow.RemoveAt(0);
					}
					while (this.retransmissionWindow.Count > 0)
					{
						long num = this.retransmissionWindow[0];
						if (num < this.windowStart)
						{
							this.retransmissionWindow.RemoveAt(0);
						}
						else
						{
							int index = (int)(num - this.windowStart);
							if (!this.window.GetTransferred(index))
							{
								return new MessageAttemptInfo(this.window.GetMessage(index), num, this.window.GetRetryCount(index), this.window.GetState(index));
							}
							this.retransmissionWindow.RemoveAt(0);
						}
					}
					messageAttemptInfo = default(MessageAttemptInfo);
				}
			}
			return messageAttemptInfo;
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x001582EC File Offset: 0x001564EC
		public bool SetLast()
		{
			if (this.reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
			{
				throw Fx.AssertAndThrow("SetLast supported only in 1.1.");
			}
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.last != 0L)
				{
					throw Fx.AssertAndThrow("Cannot set last more than once.");
				}
				this.last = this.windowStart + (long)this.window.Count - 1L;
				result = (this.last == 0L || this.DoneTransmitting);
			}
			return result;
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x00158380 File Offset: 0x00156580
		private bool InternalAdd(Message message, bool isLast, TimeSpan timeout, object state, out MessageAttemptInfo attemptInfo)
		{
			attemptInfo = default(MessageAttemptInfo);
			object obj = this.ThisLock;
			TransmissionStrategy.WaitQueueAdder waitQueueAdder;
			lock (obj)
			{
				if (isLast && this.last != 0L)
				{
					throw Fx.AssertAndThrow("Can't add more than one last message.");
				}
				if (!this.IsAddValid())
				{
					return false;
				}
				this.ThrowIfRollover();
				if (this.CanAdd())
				{
					attemptInfo = this.AddToWindow(message, isLast, state);
					return true;
				}
				waitQueueAdder = new TransmissionStrategy.WaitQueueAdder(this, message, isLast, state);
				this.waitQueue.Enqueue(waitQueueAdder);
			}
			attemptInfo = waitQueueAdder.Wait(timeout);
			return true;
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x00158430 File Offset: 0x00156630
		private IAsyncResult InternalBeginAdd(Message message, bool isLast, TimeSpan timeout, object state, AsyncCallback callback, object asyncState)
		{
			MessageAttemptInfo parameter = default(MessageAttemptInfo);
			object obj = this.ThisLock;
			bool flag2;
			lock (obj)
			{
				if (isLast && this.last != 0L)
				{
					throw Fx.AssertAndThrow("Can't add more than one last message.");
				}
				flag2 = this.IsAddValid();
				if (flag2)
				{
					this.ThrowIfRollover();
					if (!this.CanAdd())
					{
						TransmissionStrategy.AsyncQueueAdder asyncQueueAdder = new TransmissionStrategy.AsyncQueueAdder(message, isLast, timeout, state, this, callback, asyncState);
						this.waitQueue.Enqueue(asyncQueueAdder);
						return asyncQueueAdder;
					}
					parameter = this.AddToWindow(message, isLast, state);
				}
			}
			return new CompletedAsyncResult<bool, MessageAttemptInfo>(flag2, parameter, callback, asyncState);
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x001584E0 File Offset: 0x001566E0
		private bool InternalEndAdd(IAsyncResult result, out MessageAttemptInfo attemptInfo)
		{
			if (result is CompletedAsyncResult<bool, MessageAttemptInfo>)
			{
				return CompletedAsyncResult<bool, MessageAttemptInfo>.End(result, out attemptInfo);
			}
			attemptInfo = TransmissionStrategy.AsyncQueueAdder.End((TransmissionStrategy.AsyncQueueAdder)result);
			return true;
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x00158504 File Offset: 0x00156704
		public bool IsFinalAckConsistent(SequenceRangeCollection ranges)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.closed)
				{
					result = true;
				}
				else if (this.windowStart == 1L && this.window.Count == 0)
				{
					result = (ranges.Count == 0);
				}
				else if (ranges.Count == 0 || ranges[0].Lower != 1L)
				{
					result = false;
				}
				else
				{
					result = (ranges[0].Upper >= this.windowStart - 1L);
				}
			}
			return result;
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x001585AC File Offset: 0x001567AC
		public void ProcessAcknowledgement(SequenceRangeCollection ranges, out bool invalidAck, out bool inconsistentAck)
		{
			invalidAck = false;
			inconsistentAck = false;
			bool flag = false;
			bool flag2 = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.closed)
				{
					return;
				}
				long num = this.windowStart + (long)this.window.Count - 1L;
				long num2 = this.windowStart - 1L;
				int num3 = this.window.TransferredCount;
				for (int i = 0; i < ranges.Count; i++)
				{
					SequenceRange sequenceRange = ranges[i];
					if (sequenceRange.Upper > num)
					{
						invalidAck = true;
						return;
					}
					if ((sequenceRange.Lower > 1L && sequenceRange.Lower <= num2) || sequenceRange.Upper < num2)
					{
						flag2 = true;
					}
					if (sequenceRange.Upper >= this.windowStart)
					{
						if (sequenceRange.Lower <= this.windowStart)
						{
							flag = true;
						}
						if (!flag)
						{
							int num4 = (int)(sequenceRange.Lower - this.windowStart);
							int num5 = (int)((sequenceRange.Upper > num) ? ((long)(this.window.Count - 1)) : (sequenceRange.Upper - this.windowStart));
							flag = (this.window.GetTransferredInRangeCount(num4, num5) < num5 - num4 + 1);
						}
						if (num3 > 0 && !flag2)
						{
							int beginIndex = (int)((sequenceRange.Lower < this.windowStart) ? 0L : (sequenceRange.Lower - this.windowStart));
							int endIndex = (int)((sequenceRange.Upper > num) ? ((long)(this.window.Count - 1)) : (sequenceRange.Upper - this.windowStart));
							num3 -= this.window.GetTransferredInRangeCount(beginIndex, endIndex);
						}
					}
				}
				if (num3 > 0)
				{
					flag2 = true;
				}
			}
			inconsistentAck = (flag2 && flag);
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x00158788 File Offset: 0x00156988
		public bool ProcessTransferred(long transferred, int quotaRemaining)
		{
			if (transferred <= 0L)
			{
				throw Fx.AssertAndThrow("Argument transferred must be a valid sequence number.");
			}
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.closed)
				{
					result = false;
				}
				else
				{
					result = this.ProcessTransferred(new SequenceRange(transferred), quotaRemaining);
				}
			}
			return result;
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x001587F0 File Offset: 0x001569F0
		public bool ProcessTransferred(SequenceRangeCollection ranges, int quotaRemaining)
		{
			if (ranges.Count == 0)
			{
				return false;
			}
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.closed)
				{
					result = false;
				}
				else
				{
					bool flag2 = false;
					for (int i = 0; i < ranges.Count; i++)
					{
						if (this.ProcessTransferred(ranges[i], quotaRemaining))
						{
							flag2 = true;
						}
					}
					result = flag2;
				}
			}
			return result;
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x00158870 File Offset: 0x00156A70
		private bool ProcessTransferred(SequenceRange range, int quotaRemaining)
		{
			if (range.Upper < this.windowStart)
			{
				if (range.Upper == this.windowStart - 1L && quotaRemaining != -1 && quotaRemaining > this.quotaRemaining)
				{
					this.quotaRemaining = quotaRemaining - Math.Min(this.windowSize, this.window.Count);
				}
				return false;
			}
			if (range.Lower <= this.windowStart)
			{
				bool result = false;
				this.retryTimer.Cancel();
				long num = range.Upper - this.windowStart + 1L;
				if (num == 1L)
				{
					int num2 = 1;
					while (num2 < this.window.Count && this.window.GetTransferred(num2))
					{
						num += 1L;
						num2++;
					}
				}
				long now = TransmissionStrategy.Now;
				long num3 = this.windowStart + (long)this.windowSize;
				for (int i = 0; i < (int)num; i++)
				{
					this.UpdateStats(now, this.window.GetLastAttemptTime(i));
				}
				if (quotaRemaining != -1)
				{
					int val = Math.Min(this.windowSize, this.window.Count) - (int)num;
					this.quotaRemaining = quotaRemaining - Math.Max(0, val);
				}
				this.window.Remove((int)num);
				this.windowStart += num;
				int num4;
				if (this.windowSize <= this.slowStartThreshold)
				{
					this.windowSize = Math.Min(this.maxWindowSize, Math.Min(this.slowStartThreshold + 1, this.windowSize + (int)num));
					if (!this.startup)
					{
						num4 = 0;
					}
					else
					{
						num4 = Math.Max(0, (int)num3 - (int)this.windowStart);
					}
				}
				else
				{
					this.congestionControlModeAcks += (int)num;
					int num5 = Math.Max(1, (this.lossWindowSize - this.slowStartThreshold) / 8);
					int num6 = (this.windowSize - this.slowStartThreshold) * this.windowSize / num5;
					if (this.congestionControlModeAcks > num6)
					{
						this.congestionControlModeAcks = 0;
						this.windowSize = Math.Min(this.maxWindowSize, this.windowSize + 1);
					}
					num4 = Math.Max(0, (int)num3 - (int)this.windowStart);
				}
				int num7 = Math.Min(this.windowSize, this.window.Count);
				if (num4 < num7)
				{
					result = (this.retransmissionWindow.Count == 0);
					int num8 = num4;
					while (num8 < this.windowSize && num8 < this.window.Count)
					{
						long item = this.windowStart + (long)num8;
						if (!this.window.GetTransferred(num8) && !this.retransmissionWindow.Contains(item))
						{
							this.window.RecordRetry(num8, TransmissionStrategy.Now);
							this.retransmissionWindow.Add(item);
						}
						num8++;
					}
				}
				if (this.window.Count > 0)
				{
					this.retryTimer.Set(this.Timeout);
				}
				return result;
			}
			for (long num9 = range.Lower; num9 <= range.Upper; num9 += 1L)
			{
				this.window.SetTransferred((int)(num9 - this.windowStart));
			}
			return false;
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x00158B7C File Offset: 0x00156D7C
		private bool RemoveAdder(TransmissionStrategy.IQueueAdder adder)
		{
			object obj = this.ThisLock;
			bool result;
			lock (obj)
			{
				if (this.closed)
				{
					result = false;
				}
				else
				{
					bool flag2 = false;
					for (int i = 0; i < this.waitQueue.Count; i++)
					{
						TransmissionStrategy.IQueueAdder queueAdder = this.waitQueue.Dequeue();
						if (adder == queueAdder)
						{
							flag2 = true;
						}
						else
						{
							this.waitQueue.Enqueue(queueAdder);
						}
					}
					result = flag2;
				}
			}
			return result;
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x00158C04 File Offset: 0x00156E04
		private void ThrowIfRollover()
		{
			if (this.windowStart + (long)this.window.Count + (long)this.waitQueue.Count == 9223372036854775807L)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageNumberRolloverFault(this.id).CreateException());
			}
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x00158C58 File Offset: 0x00156E58
		private void UpdateStats(long now, long lastAttemptTime)
		{
			now = Math.Max(now, lastAttemptTime);
			long num = now - lastAttemptTime;
			long num2 = num - this.meanRtt;
			this.serrRtt = Math.Min(this.serrRtt + (Math.Abs(num2) - this.serrRtt >> 3), 1537228672809129301L);
			this.meanRtt = Math.Min(this.meanRtt + (num2 >> 3), 3074457345618258602L);
			this.timeout = Math.Max(51200L + this.meanRtt, this.meanRtt + (this.serrRtt << 2));
		}

		// Token: 0x04003766 RID: 14182
		private bool aborted;

		// Token: 0x04003767 RID: 14183
		private bool closed;

		// Token: 0x04003768 RID: 14184
		private int congestionControlModeAcks;

		// Token: 0x04003769 RID: 14185
		private UniqueId id;

		// Token: 0x0400376A RID: 14186
		private long last;

		// Token: 0x0400376B RID: 14187
		private int lossWindowSize;

		// Token: 0x0400376C RID: 14188
		private int maxWindowSize;

		// Token: 0x0400376D RID: 14189
		private long meanRtt;

		// Token: 0x0400376E RID: 14190
		private ComponentExceptionHandler onException;

		// Token: 0x0400376F RID: 14191
		private int quotaRemaining;

		// Token: 0x04003770 RID: 14192
		private ReliableMessagingVersion reliableMessagingVersion;

		// Token: 0x04003771 RID: 14193
		private List<long> retransmissionWindow = new List<long>();

		// Token: 0x04003772 RID: 14194
		private IOThreadTimer retryTimer;

		// Token: 0x04003773 RID: 14195
		private RetryHandler retryTimeoutElapsedHandler;

		// Token: 0x04003774 RID: 14196
		private bool requestAcks;

		// Token: 0x04003775 RID: 14197
		private long serrRtt;

		// Token: 0x04003776 RID: 14198
		private int slowStartThreshold;

		// Token: 0x04003777 RID: 14199
		private bool startup = true;

		// Token: 0x04003778 RID: 14200
		private object thisLock = new object();

		// Token: 0x04003779 RID: 14201
		private long timeout;

		// Token: 0x0400377A RID: 14202
		private Queue<TransmissionStrategy.IQueueAdder> waitQueue = new Queue<TransmissionStrategy.IQueueAdder>();

		// Token: 0x0400377B RID: 14203
		private TransmissionStrategy.SlidingWindow window;

		// Token: 0x0400377C RID: 14204
		private int windowSize = 1;

		// Token: 0x0400377D RID: 14205
		private long windowStart = 1L;

		// Token: 0x02000DEA RID: 3562
		private class AsyncQueueAdder : WaitAsyncResult, TransmissionStrategy.IQueueAdder
		{
			// Token: 0x060080C1 RID: 32961 RVA: 0x001DE264 File Offset: 0x001DC464
			public AsyncQueueAdder(Message message, bool isLast, TimeSpan timeout, object state, TransmissionStrategy strategy, AsyncCallback callback, object asyncState) : base(timeout, true, callback, asyncState)
			{
				this.attemptInfo = new MessageAttemptInfo(message, 0L, 0, state);
				this.isLast = isLast;
				this.strategy = strategy;
				base.Begin();
			}

			// Token: 0x060080C2 RID: 32962 RVA: 0x001DE298 File Offset: 0x001DC498
			public void Abort(CommunicationObject communicationObject)
			{
				this.attemptInfo.Message.Close();
				base.OnAborted(communicationObject);
			}

			// Token: 0x060080C3 RID: 32963 RVA: 0x001DE2B1 File Offset: 0x001DC4B1
			public void Complete0()
			{
				this.attemptInfo = this.strategy.AddToWindow(this.attemptInfo.Message, this.isLast, this.attemptInfo.State);
			}

			// Token: 0x060080C4 RID: 32964 RVA: 0x001DE2E0 File Offset: 0x001DC4E0
			public void Complete1()
			{
				base.OnSignaled();
			}

			// Token: 0x060080C5 RID: 32965 RVA: 0x001DE2E8 File Offset: 0x001DC4E8
			public static MessageAttemptInfo End(TransmissionStrategy.AsyncQueueAdder result)
			{
				AsyncResult.End<TransmissionStrategy.AsyncQueueAdder>(result);
				return result.attemptInfo;
			}

			// Token: 0x060080C6 RID: 32966 RVA: 0x001DE2F7 File Offset: 0x001DC4F7
			public void Fault(CommunicationObject communicationObject)
			{
				this.attemptInfo.Message.Close();
				base.OnFaulted(communicationObject);
			}

			// Token: 0x060080C7 RID: 32967 RVA: 0x001DE310 File Offset: 0x001DC510
			protected override string GetTimeoutString(TimeSpan timeout)
			{
				return SR.GetString("TimeoutOnAddToWindow", new object[]
				{
					timeout
				});
			}

			// Token: 0x060080C8 RID: 32968 RVA: 0x001DE32B File Offset: 0x001DC52B
			protected override void OnTimerElapsed(object state)
			{
				if (this.strategy.RemoveAdder(this))
				{
					base.OnTimerElapsed(state);
				}
			}

			// Token: 0x0400496C RID: 18796
			private bool isLast;

			// Token: 0x0400496D RID: 18797
			private MessageAttemptInfo attemptInfo;

			// Token: 0x0400496E RID: 18798
			private TransmissionStrategy strategy;
		}

		// Token: 0x02000DEB RID: 3563
		private static class Constants
		{
			// Token: 0x0400496F RID: 18799
			public const int ChebychevFactor = 2;

			// Token: 0x04004970 RID: 18800
			public const int Gain = 3;

			// Token: 0x04004971 RID: 18801
			public const int TimeMultiplier = 7;

			// Token: 0x04004972 RID: 18802
			public const long MaxMeanRtt = 3074457345618258602L;

			// Token: 0x04004973 RID: 18803
			public const long MaxSerrRtt = 1537228672809129301L;
		}

		// Token: 0x02000DEC RID: 3564
		private interface IQueueAdder
		{
			// Token: 0x060080C9 RID: 32969
			void Abort(CommunicationObject communicationObject);

			// Token: 0x060080CA RID: 32970
			void Fault(CommunicationObject communicationObject);

			// Token: 0x060080CB RID: 32971
			void Complete0();

			// Token: 0x060080CC RID: 32972
			void Complete1();
		}

		// Token: 0x02000DED RID: 3565
		private class SlidingWindow
		{
			// Token: 0x060080CD RID: 32973 RVA: 0x001DE342 File Offset: 0x001DC542
			public SlidingWindow(int maxSize)
			{
				this.maxSize = maxSize + 1;
				this.buffer = new TransmissionStrategy.SlidingWindow.TransmissionInfo[this.maxSize];
			}

			// Token: 0x17001C7C RID: 7292
			// (get) Token: 0x060080CE RID: 32974 RVA: 0x001DE364 File Offset: 0x001DC564
			public int Count
			{
				get
				{
					if (this.tail >= this.head)
					{
						return this.tail - this.head;
					}
					return this.tail - this.head + this.maxSize;
				}
			}

			// Token: 0x17001C7D RID: 7293
			// (get) Token: 0x060080CF RID: 32975 RVA: 0x001DE396 File Offset: 0x001DC596
			public int TransferredCount
			{
				get
				{
					if (this.Count == 0)
					{
						return 0;
					}
					return this.GetTransferredInRangeCount(0, this.Count - 1);
				}
			}

			// Token: 0x060080D0 RID: 32976 RVA: 0x001DE3B4 File Offset: 0x001DC5B4
			public void Add(Message message, long addTime, object state)
			{
				if (this.Count >= this.maxSize - 1)
				{
					throw Fx.AssertAndThrow("The caller is not allowed to add messages beyond the sliding window's maximum size.");
				}
				this.buffer[this.tail] = new TransmissionStrategy.SlidingWindow.TransmissionInfo(message, addTime, state);
				this.tail = (this.tail + 1) % this.maxSize;
			}

			// Token: 0x060080D1 RID: 32977 RVA: 0x001DE40A File Offset: 0x001DC60A
			private void AssertIndex(int index)
			{
				if (index >= this.Count)
				{
					throw Fx.AssertAndThrow("Argument index must be less than Count.");
				}
				if (index < 0)
				{
					throw Fx.AssertAndThrow("Argument index must be positive.");
				}
			}

			// Token: 0x060080D2 RID: 32978 RVA: 0x001DE42F File Offset: 0x001DC62F
			public void Close()
			{
				this.Remove(this.Count);
			}

			// Token: 0x060080D3 RID: 32979 RVA: 0x001DE43D File Offset: 0x001DC63D
			public long GetLastAttemptTime(int index)
			{
				this.AssertIndex(index);
				return this.buffer[(this.head + index) % this.maxSize].LastAttemptTime;
			}

			// Token: 0x060080D4 RID: 32980 RVA: 0x001DE468 File Offset: 0x001DC668
			public Message GetMessage(int index)
			{
				this.AssertIndex(index);
				if (!this.buffer[(this.head + index) % this.maxSize].Transferred)
				{
					return this.buffer[(this.head + index) % this.maxSize].Buffer.CreateMessage();
				}
				return null;
			}

			// Token: 0x060080D5 RID: 32981 RVA: 0x001DE4C3 File Offset: 0x001DC6C3
			public int GetRetryCount(int index)
			{
				this.AssertIndex(index);
				return this.buffer[(this.head + index) % this.maxSize].RetryCount;
			}

			// Token: 0x060080D6 RID: 32982 RVA: 0x001DE4EB File Offset: 0x001DC6EB
			public object GetState(int index)
			{
				this.AssertIndex(index);
				return this.buffer[(this.head + index) % this.maxSize].State;
			}

			// Token: 0x060080D7 RID: 32983 RVA: 0x001DE513 File Offset: 0x001DC713
			public bool GetTransferred(int index)
			{
				this.AssertIndex(index);
				return this.buffer[(this.head + index) % this.maxSize].Transferred;
			}

			// Token: 0x060080D8 RID: 32984 RVA: 0x001DE53C File Offset: 0x001DC73C
			public int GetTransferredInRangeCount(int beginIndex, int endIndex)
			{
				if (beginIndex < 0)
				{
					throw Fx.AssertAndThrow("Argument beginIndex cannot be negative.");
				}
				if (endIndex >= this.Count)
				{
					throw Fx.AssertAndThrow("Argument endIndex cannot be greater than Count.");
				}
				if (endIndex < beginIndex)
				{
					throw Fx.AssertAndThrow("Argument endIndex cannot be less than argument beginIndex.");
				}
				int num = 0;
				for (int i = beginIndex; i <= endIndex; i++)
				{
					if (this.buffer[(this.head + i) % this.maxSize].Transferred)
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x060080D9 RID: 32985 RVA: 0x001DE5B0 File Offset: 0x001DC7B0
			public int RecordRetry(int index, long retryTime)
			{
				this.AssertIndex(index);
				this.buffer[(this.head + index) % this.maxSize].LastAttemptTime = retryTime;
				TransmissionStrategy.SlidingWindow.TransmissionInfo[] array = this.buffer;
				int num = (this.head + index) % this.maxSize;
				int num2 = array[num].RetryCount + 1;
				array[num].RetryCount = num2;
				return num2;
			}

			// Token: 0x060080DA RID: 32986 RVA: 0x001DE60C File Offset: 0x001DC80C
			public void Remove(int count)
			{
				if (count > this.Count)
				{
				}
				while (count-- > 0)
				{
					this.buffer[this.head].Buffer.Close();
					this.buffer[this.head].Buffer = null;
					this.head = (this.head + 1) % this.maxSize;
				}
			}

			// Token: 0x060080DB RID: 32987 RVA: 0x001DE674 File Offset: 0x001DC874
			public void SetTransferred(int index)
			{
				this.AssertIndex(index);
				this.buffer[(this.head + index) % this.maxSize].Transferred = true;
			}

			// Token: 0x04004974 RID: 18804
			private TransmissionStrategy.SlidingWindow.TransmissionInfo[] buffer;

			// Token: 0x04004975 RID: 18805
			private int head;

			// Token: 0x04004976 RID: 18806
			private int tail;

			// Token: 0x04004977 RID: 18807
			private int maxSize;

			// Token: 0x02000F7A RID: 3962
			private struct TransmissionInfo
			{
				// Token: 0x060087EF RID: 34799 RVA: 0x001F9520 File Offset: 0x001F7720
				public TransmissionInfo(Message message, long lastAttemptTime, object state)
				{
					this.Buffer = message.CreateBufferedCopy(int.MaxValue);
					this.LastAttemptTime = lastAttemptTime;
					this.RetryCount = 0;
					this.State = state;
					this.Transferred = false;
				}

				// Token: 0x04004F4C RID: 20300
				internal MessageBuffer Buffer;

				// Token: 0x04004F4D RID: 20301
				internal long LastAttemptTime;

				// Token: 0x04004F4E RID: 20302
				internal int RetryCount;

				// Token: 0x04004F4F RID: 20303
				internal object State;

				// Token: 0x04004F50 RID: 20304
				internal bool Transferred;
			}
		}

		// Token: 0x02000DEE RID: 3566
		private class WaitQueueAdder : TransmissionStrategy.IQueueAdder
		{
			// Token: 0x060080DC RID: 32988 RVA: 0x001DE69D File Offset: 0x001DC89D
			public WaitQueueAdder(TransmissionStrategy strategy, Message message, bool isLast, object state)
			{
				this.strategy = strategy;
				this.isLast = isLast;
				this.attemptInfo = new MessageAttemptInfo(message, 0L, 0, state);
			}

			// Token: 0x060080DD RID: 32989 RVA: 0x001DE6D0 File Offset: 0x001DC8D0
			public void Abort(CommunicationObject communicationObject)
			{
				this.exception = communicationObject.CreateClosedException();
				this.completeEvent.Set();
			}

			// Token: 0x060080DE RID: 32990 RVA: 0x001DE6EA File Offset: 0x001DC8EA
			public void Complete0()
			{
				this.attemptInfo = this.strategy.AddToWindow(this.attemptInfo.Message, this.isLast, this.attemptInfo.State);
				this.completeEvent.Set();
			}

			// Token: 0x060080DF RID: 32991 RVA: 0x001DE725 File Offset: 0x001DC925
			public void Complete1()
			{
			}

			// Token: 0x060080E0 RID: 32992 RVA: 0x001DE727 File Offset: 0x001DC927
			public void Fault(CommunicationObject communicationObject)
			{
				this.exception = communicationObject.GetTerminalException();
				this.completeEvent.Set();
			}

			// Token: 0x060080E1 RID: 32993 RVA: 0x001DE744 File Offset: 0x001DC944
			public MessageAttemptInfo Wait(TimeSpan timeout)
			{
				if (!TimeoutHelper.WaitOne(this.completeEvent, timeout) && this.strategy.RemoveAdder(this) && this.exception == null)
				{
					this.exception = new TimeoutException(SR.GetString("TimeoutOnAddToWindow", new object[]
					{
						timeout
					}));
				}
				if (this.exception != null)
				{
					this.attemptInfo.Message.Close();
					this.completeEvent.Close();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.exception);
				}
				this.completeEvent.Close();
				return this.attemptInfo;
			}

			// Token: 0x04004978 RID: 18808
			private ManualResetEvent completeEvent = new ManualResetEvent(false);

			// Token: 0x04004979 RID: 18809
			private Exception exception;

			// Token: 0x0400497A RID: 18810
			private bool isLast;

			// Token: 0x0400497B RID: 18811
			private MessageAttemptInfo attemptInfo;

			// Token: 0x0400497C RID: 18812
			private TransmissionStrategy strategy;
		}
	}
}
