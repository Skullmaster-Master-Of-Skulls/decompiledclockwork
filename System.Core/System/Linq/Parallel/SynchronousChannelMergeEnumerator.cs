using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200018A RID: 394
	internal sealed class SynchronousChannelMergeEnumerator<T> : MergeEnumerator<T>
	{
		// Token: 0x06000E19 RID: 3609 RVA: 0x00031C78 File Offset: 0x0002FE78
		internal SynchronousChannelMergeEnumerator(QueryTaskGroupState taskGroupState, SynchronousChannel<T>[] channels) : base(taskGroupState)
		{
			this.m_channels = channels;
			this.m_channelIndex = -1;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00031C8F File Offset: 0x0002FE8F
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

		// Token: 0x06000E1B RID: 3611 RVA: 0x00031CC0 File Offset: 0x0002FEC0
		public override bool MoveNext()
		{
			if (this.m_channelIndex == -1)
			{
				this.m_channelIndex = 0;
			}
			while (this.m_channelIndex != this.m_channels.Length)
			{
				SynchronousChannel<T> synchronousChannel = this.m_channels[this.m_channelIndex];
				if (synchronousChannel.Count != 0)
				{
					this.m_currentElement = synchronousChannel.Dequeue();
					return true;
				}
				this.m_channelIndex++;
			}
			return false;
		}

		// Token: 0x04000844 RID: 2116
		private SynchronousChannel<T>[] m_channels;

		// Token: 0x04000845 RID: 2117
		private int m_channelIndex;

		// Token: 0x04000846 RID: 2118
		private T m_currentElement;
	}
}
