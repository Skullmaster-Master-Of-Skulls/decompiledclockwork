using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000180 RID: 384
	internal sealed class AsynchronousChannelMergeEnumerator<T> : MergeEnumerator<T>
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x000313C5 File Offset: 0x0002F5C5
		internal AsynchronousChannelMergeEnumerator(QueryTaskGroupState taskGroupState, AsynchronousChannel<T>[] channels, IntValueEvent consumerEvent) : base(taskGroupState)
		{
			this.m_channels = channels;
			this.m_channelIndex = -1;
			this.m_done = new bool[this.m_channels.Length];
			this.m_consumerEvent = consumerEvent;
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x000313F6 File Offset: 0x0002F5F6
		public override T Current
		{
			get
			{
				if (this.m_channelIndex == -1 || this.m_channelIndex == this.m_channels.Length)
				{
					throw new InvalidOperationException(SR.GetString("PLINQ_CommonEnumerator_Current_NotStarted"));
				}
				return this.m_currentElement;
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00031428 File Offset: 0x0002F628
		public override bool MoveNext()
		{
			int num = this.m_channelIndex;
			if (num == -1)
			{
				num = (this.m_channelIndex = 0);
			}
			if (num == this.m_channels.Length)
			{
				return false;
			}
			if (!this.m_done[num] && this.m_channels[num].TryDequeue(ref this.m_currentElement))
			{
				this.m_channelIndex = (num + 1) % this.m_channels.Length;
				return true;
			}
			return this.MoveNextSlowPath();
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00031490 File Offset: 0x0002F690
		private bool MoveNextSlowPath()
		{
			int num = 0;
			int num2 = this.m_channelIndex;
			int num3;
			while ((num3 = this.m_channelIndex) != this.m_channels.Length)
			{
				AsynchronousChannel<T> asynchronousChannel = this.m_channels[num3];
				bool flag = this.m_done[num3];
				if (!flag && asynchronousChannel.TryDequeue(ref this.m_currentElement))
				{
					this.m_channelIndex = (num3 + 1) % this.m_channels.Length;
					return true;
				}
				if (!flag && asynchronousChannel.IsDone)
				{
					if (!asynchronousChannel.IsChunkBufferEmpty)
					{
						bool flag2 = asynchronousChannel.TryDequeue(ref this.m_currentElement);
						return true;
					}
					this.m_done[num3] = true;
					flag = true;
					asynchronousChannel.Dispose();
				}
				if (flag && ++num == this.m_channels.Length)
				{
					this.m_channelIndex = this.m_channels.Length;
					break;
				}
				num3 = (this.m_channelIndex = (num3 + 1) % this.m_channels.Length);
				if (num3 == num2)
				{
					try
					{
						num = 0;
						for (int i = 0; i < this.m_channels.Length; i++)
						{
							bool flag3 = false;
							if (!this.m_done[i] && this.m_channels[i].TryDequeue(ref this.m_currentElement, ref flag3))
							{
								return true;
							}
							if (flag3)
							{
								if (!this.m_done[i])
								{
									this.m_done[i] = true;
								}
								if (++num == this.m_channels.Length)
								{
									num3 = (this.m_channelIndex = this.m_channels.Length);
									break;
								}
							}
						}
						if (num3 == this.m_channels.Length)
						{
							break;
						}
						this.m_consumerEvent.Wait();
						num3 = (this.m_channelIndex = this.m_consumerEvent.Value);
						this.m_consumerEvent.Reset();
						num2 = num3;
						num = 0;
					}
					finally
					{
						for (int j = 0; j < this.m_channels.Length; j++)
						{
							if (!this.m_done[j])
							{
								this.m_channels[j].DoneWithDequeueWait();
							}
						}
					}
					continue;
				}
			}
			this.m_taskGroupState.QueryEnd(false);
			return false;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00031680 File Offset: 0x0002F880
		public override void Dispose()
		{
			if (this.m_consumerEvent != null)
			{
				base.Dispose();
				this.m_consumerEvent.Dispose();
				this.m_consumerEvent = null;
			}
		}

		// Token: 0x04000820 RID: 2080
		private AsynchronousChannel<T>[] m_channels;

		// Token: 0x04000821 RID: 2081
		private IntValueEvent m_consumerEvent;

		// Token: 0x04000822 RID: 2082
		private bool[] m_done;

		// Token: 0x04000823 RID: 2083
		private int m_channelIndex;

		// Token: 0x04000824 RID: 2084
		private T m_currentElement;
	}
}
