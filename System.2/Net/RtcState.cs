using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200010A RID: 266
	[FriendAccessAllowed]
	internal class RtcState
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0003BC86 File Offset: 0x00039E86
		internal bool IsAborted
		{
			get
			{
				return this.isAborted != 0;
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0003BC91 File Offset: 0x00039E91
		internal RtcState()
		{
			this.connectComplete = new ManualResetEvent(false);
			this.flushComplete = new ManualResetEvent(false);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0003BCB1 File Offset: 0x00039EB1
		internal void Abort()
		{
			Interlocked.Exchange(ref this.isAborted, 1);
			this.connectComplete.Set();
			this.flushComplete.Set();
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0003BCD8 File Offset: 0x00039ED8
		internal bool IsEnabled()
		{
			RtcState.ControlChannelTriggerStatus controlChannelTriggerStatus = (RtcState.ControlChannelTriggerStatus)BitConverter.ToInt32(this.outputData, 0);
			return this.result == 0 && (controlChannelTriggerStatus == RtcState.ControlChannelTriggerStatus.SoftwareSlotAllocated || controlChannelTriggerStatus == RtcState.ControlChannelTriggerStatus.HardwareSlotAllocated);
		}

		// Token: 0x04000F35 RID: 3893
		internal byte[] inputData;

		// Token: 0x04000F36 RID: 3894
		internal byte[] outputData;

		// Token: 0x04000F37 RID: 3895
		internal ManualResetEvent connectComplete;

		// Token: 0x04000F38 RID: 3896
		internal ManualResetEvent flushComplete;

		// Token: 0x04000F39 RID: 3897
		internal int result;

		// Token: 0x04000F3A RID: 3898
		private int isAborted;

		// Token: 0x02000709 RID: 1801
		[FriendAccessAllowed]
		internal enum ControlChannelTriggerStatus
		{
			// Token: 0x040030F6 RID: 12534
			Invalid,
			// Token: 0x040030F7 RID: 12535
			SoftwareSlotAllocated,
			// Token: 0x040030F8 RID: 12536
			HardwareSlotAllocated,
			// Token: 0x040030F9 RID: 12537
			PolicyError,
			// Token: 0x040030FA RID: 12538
			SystemError,
			// Token: 0x040030FB RID: 12539
			TransportDisconnected,
			// Token: 0x040030FC RID: 12540
			ServiceUnavailable
		}
	}
}
