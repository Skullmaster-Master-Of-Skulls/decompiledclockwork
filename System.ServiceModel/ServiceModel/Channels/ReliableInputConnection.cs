using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200092F RID: 2351
	internal sealed class ReliableInputConnection
	{
		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06005A61 RID: 23137 RVA: 0x0014C314 File Offset: 0x0014A514
		public bool AllAdded
		{
			get
			{
				return (this.ranges.Count == 1 && this.ranges[0].Lower == 1L && this.ranges[0].Upper == this.last) || this.isLastKnown;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06005A62 RID: 23138 RVA: 0x0014C36B File Offset: 0x0014A56B
		public bool IsLastKnown
		{
			get
			{
				return this.last != 0L || this.isLastKnown;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06005A63 RID: 23139 RVA: 0x0014C37D File Offset: 0x0014A57D
		public bool IsSequenceClosed
		{
			get
			{
				return this.isSequenceClosed;
			}
		}

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06005A64 RID: 23140 RVA: 0x0014C385 File Offset: 0x0014A585
		public long Last
		{
			get
			{
				return this.last;
			}
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06005A65 RID: 23141 RVA: 0x0014C38D File Offset: 0x0014A58D
		public SequenceRangeCollection Ranges
		{
			get
			{
				return this.ranges;
			}
		}

		// Token: 0x170015EA RID: 5610
		// (set) Token: 0x06005A66 RID: 23142 RVA: 0x0014C395 File Offset: 0x0014A595
		public ReliableMessagingVersion ReliableMessagingVersion
		{
			set
			{
				this.reliableMessagingVersion = value;
			}
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x0014C39E File Offset: 0x0014A59E
		public void Abort(ChannelBase channel)
		{
			this.shutdownWaitObject.Abort(channel);
			this.terminateWaitObject.Abort(channel);
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x0014C3B8 File Offset: 0x0014A5B8
		public bool CanMerge(long sequenceNumber)
		{
			return ReliableInputConnection.CanMerge(sequenceNumber, this.ranges);
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x0014C3C6 File Offset: 0x0014A5C6
		public static bool CanMerge(long sequenceNumber, SequenceRangeCollection ranges)
		{
			if (ranges.Count < ReliableMessagingConstants.MaxSequenceRanges)
			{
				return true;
			}
			ranges = ranges.MergeWith(sequenceNumber);
			return ranges.Count <= ReliableMessagingConstants.MaxSequenceRanges;
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x0014C3F0 File Offset: 0x0014A5F0
		public void Fault(ChannelBase channel)
		{
			this.shutdownWaitObject.Fault(channel);
			this.terminateWaitObject.Fault(channel);
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x0014C40C File Offset: 0x0014A60C
		public bool IsValid(long sequenceNumber, bool isLast)
		{
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (isLast)
				{
					if (this.last == 0L)
					{
						return this.ranges.Count <= 0 || sequenceNumber > this.ranges[this.ranges.Count - 1].Upper;
					}
					return sequenceNumber == this.last;
				}
				else if (this.last > 0L)
				{
					return sequenceNumber < this.last;
				}
			}
			else if (this.isLastKnown)
			{
				return this.ranges.Contains(sequenceNumber);
			}
			return true;
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x0014C499 File Offset: 0x0014A699
		public void Merge(long sequenceNumber, bool isLast)
		{
			this.ranges = this.ranges.MergeWith(sequenceNumber);
			if (isLast)
			{
				this.last = sequenceNumber;
			}
			if (this.AllAdded)
			{
				this.shutdownWaitObject.Set();
			}
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x0014C4CC File Offset: 0x0014A6CC
		public bool SetCloseSequenceLast(long last)
		{
			WsrmUtilities.AssertWsrm11(this.reliableMessagingVersion);
			bool flag = last < 1L || this.ranges.Count == 0 || last >= this.ranges[this.ranges.Count - 1].Upper;
			if (flag)
			{
				this.isSequenceClosed = true;
				this.SetLast(last);
			}
			return flag;
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x0014C533 File Offset: 0x0014A733
		private void SetLast(long last)
		{
			if (this.isLastKnown)
			{
				throw Fx.AssertAndThrow("Last can only be set once.");
			}
			this.last = last;
			this.isLastKnown = true;
			this.shutdownWaitObject.Set();
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x0014C564 File Offset: 0x0014A764
		public bool SetTerminateSequenceLast(long last, out bool isLastLargeEnough)
		{
			WsrmUtilities.AssertWsrm11(this.reliableMessagingVersion);
			isLastLargeEnough = true;
			if (last < 1L)
			{
				return false;
			}
			int count = this.ranges.Count;
			long num = (count > 0) ? this.ranges[count - 1].Upper : 0L;
			if (last < num)
			{
				isLastLargeEnough = false;
				return false;
			}
			if (count > 1 || last > num)
			{
				return false;
			}
			this.SetLast(last);
			return true;
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x0014C5CC File Offset: 0x0014A7CC
		public bool Terminate()
		{
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 || this.isSequenceClosed)
			{
				if (!this.terminated && this.AllAdded)
				{
					this.terminateWaitObject.Set();
					this.terminated = true;
				}
				return this.terminated;
			}
			return this.isLastKnown;
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x0014C620 File Offset: 0x0014A820
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.shutdownWaitObject.BeginWait),
				new OperationWithTimeoutBeginCallback(this.terminateWaitObject.BeginWait)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				new OperationEndCallback(this.shutdownWaitObject.EndWait),
				new OperationEndCallback(this.terminateWaitObject.EndWait)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x0014C698 File Offset: 0x0014A898
		public void Close(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.shutdownWaitObject.Wait(timeoutHelper.RemainingTime());
			this.terminateWaitObject.Wait(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x0014C6D3 File Offset: 0x0014A8D3
		public void EndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x04003692 RID: 13970
		private bool isLastKnown;

		// Token: 0x04003693 RID: 13971
		private bool isSequenceClosed;

		// Token: 0x04003694 RID: 13972
		private long last;

		// Token: 0x04003695 RID: 13973
		private SequenceRangeCollection ranges = SequenceRangeCollection.Empty;

		// Token: 0x04003696 RID: 13974
		private ReliableMessagingVersion reliableMessagingVersion;

		// Token: 0x04003697 RID: 13975
		private InterruptibleWaitObject shutdownWaitObject = new InterruptibleWaitObject(false);

		// Token: 0x04003698 RID: 13976
		private bool terminated;

		// Token: 0x04003699 RID: 13977
		private InterruptibleWaitObject terminateWaitObject = new InterruptibleWaitObject(false, false);
	}
}
