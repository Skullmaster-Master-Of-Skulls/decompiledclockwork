using System;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000A9 RID: 169
	public abstract class MsmqBindingBase : Binding, IBindingRuntimePreferences
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0001133A File Offset: 0x0000F53A
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00011347 File Offset: 0x0000F547
		[DefaultValue(typeof(TimeSpan), "00:05:00")]
		public TimeSpan ValidityDuration
		{
			get
			{
				return this.transport.ValidityDuration;
			}
			set
			{
				this.transport.ValidityDuration = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00011355 File Offset: 0x0000F555
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00011362 File Offset: 0x0000F562
		[DefaultValue(null)]
		public Uri CustomDeadLetterQueue
		{
			get
			{
				return this.transport.CustomDeadLetterQueue;
			}
			set
			{
				this.transport.CustomDeadLetterQueue = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00011370 File Offset: 0x0000F570
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0001137D File Offset: 0x0000F57D
		[DefaultValue(DeadLetterQueue.System)]
		public DeadLetterQueue DeadLetterQueue
		{
			get
			{
				return this.transport.DeadLetterQueue;
			}
			set
			{
				this.transport.DeadLetterQueue = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0001138B File Offset: 0x0000F58B
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00011398 File Offset: 0x0000F598
		[DefaultValue(true)]
		public bool Durable
		{
			get
			{
				return this.transport.Durable;
			}
			set
			{
				this.transport.Durable = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000113A6 File Offset: 0x0000F5A6
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x000113B3 File Offset: 0x0000F5B3
		[DefaultValue(true)]
		public bool ExactlyOnce
		{
			get
			{
				return this.transport.ExactlyOnce;
			}
			set
			{
				this.transport.ExactlyOnce = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000113C1 File Offset: 0x0000F5C1
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000113CE File Offset: 0x0000F5CE
		[DefaultValue(65536L)]
		public long MaxReceivedMessageSize
		{
			get
			{
				return this.transport.MaxReceivedMessageSize;
			}
			set
			{
				this.transport.MaxReceivedMessageSize = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000113DC File Offset: 0x0000F5DC
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000113E9 File Offset: 0x0000F5E9
		[DefaultValue(5)]
		public int ReceiveRetryCount
		{
			get
			{
				return this.transport.ReceiveRetryCount;
			}
			set
			{
				this.transport.ReceiveRetryCount = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000113F7 File Offset: 0x0000F5F7
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00011404 File Offset: 0x0000F604
		[DefaultValue(2)]
		public int MaxRetryCycles
		{
			get
			{
				return this.transport.MaxRetryCycles;
			}
			set
			{
				this.transport.MaxRetryCycles = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00011412 File Offset: 0x0000F612
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0001141F File Offset: 0x0000F61F
		[DefaultValue(true)]
		public bool ReceiveContextEnabled
		{
			get
			{
				return this.transport.ReceiveContextEnabled;
			}
			set
			{
				this.transport.ReceiveContextEnabled = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0001142D File Offset: 0x0000F62D
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0001143A File Offset: 0x0000F63A
		[DefaultValue(ReceiveErrorHandling.Fault)]
		public ReceiveErrorHandling ReceiveErrorHandling
		{
			get
			{
				return this.transport.ReceiveErrorHandling;
			}
			set
			{
				this.transport.ReceiveErrorHandling = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00011448 File Offset: 0x0000F648
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00011455 File Offset: 0x0000F655
		[DefaultValue(typeof(TimeSpan), "00:30:00")]
		public TimeSpan RetryCycleDelay
		{
			get
			{
				return this.transport.RetryCycleDelay;
			}
			set
			{
				this.transport.RetryCycleDelay = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00011463 File Offset: 0x0000F663
		public override string Scheme
		{
			get
			{
				return this.transport.Scheme;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00011470 File Offset: 0x0000F670
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0001147D File Offset: 0x0000F67D
		[DefaultValue(typeof(TimeSpan), "1.00:00:00")]
		public TimeSpan TimeToLive
		{
			get
			{
				return this.transport.TimeToLive;
			}
			set
			{
				this.transport.TimeToLive = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0001148B File Offset: 0x0000F68B
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00011498 File Offset: 0x0000F698
		[DefaultValue(false)]
		public bool UseSourceJournal
		{
			get
			{
				return this.transport.UseSourceJournal;
			}
			set
			{
				this.transport.UseSourceJournal = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000114A6 File Offset: 0x0000F6A6
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000114B3 File Offset: 0x0000F6B3
		[DefaultValue(false)]
		public bool UseMsmqTracing
		{
			get
			{
				return this.transport.UseMsmqTracing;
			}
			set
			{
				this.transport.UseMsmqTracing = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000114C1 File Offset: 0x0000F6C1
		bool IBindingRuntimePreferences.ReceiveSynchronously
		{
			get
			{
				return this.ExactlyOnce;
			}
		}

		// Token: 0x0400094B RID: 2379
		internal MsmqBindingElementBase transport;
	}
}
