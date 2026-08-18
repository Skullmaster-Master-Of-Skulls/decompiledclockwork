using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008A2 RID: 2210
	public sealed class NamedPipeConnectionPoolSettings
	{
		// Token: 0x06005444 RID: 21572 RVA: 0x00136610 File Offset: 0x00134810
		internal NamedPipeConnectionPoolSettings()
		{
			this.groupName = "default";
			this.idleTimeout = ConnectionOrientedTransportDefaults.IdleTimeout;
			this.maxOutputConnectionsPerEndpoint = 10;
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00136636 File Offset: 0x00134836
		internal NamedPipeConnectionPoolSettings(NamedPipeConnectionPoolSettings namedPipe)
		{
			this.groupName = namedPipe.groupName;
			this.idleTimeout = namedPipe.idleTimeout;
			this.maxOutputConnectionsPerEndpoint = namedPipe.maxOutputConnectionsPerEndpoint;
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06005446 RID: 21574 RVA: 0x00136662 File Offset: 0x00134862
		// (set) Token: 0x06005447 RID: 21575 RVA: 0x0013666A File Offset: 0x0013486A
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.groupName = value;
			}
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06005448 RID: 21576 RVA: 0x00136686 File Offset: 0x00134886
		// (set) Token: 0x06005449 RID: 21577 RVA: 0x00136690 File Offset: 0x00134890
		public TimeSpan IdleTimeout
		{
			get
			{
				return this.idleTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.idleTimeout = value;
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x0600544A RID: 21578 RVA: 0x00136703 File Offset: 0x00134903
		// (set) Token: 0x0600544B RID: 21579 RVA: 0x0013670B File Offset: 0x0013490B
		public int MaxOutboundConnectionsPerEndpoint
		{
			get
			{
				return this.maxOutputConnectionsPerEndpoint;
			}
			set
			{
				if (value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
				}
				this.maxOutputConnectionsPerEndpoint = value;
			}
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0013673D File Offset: 0x0013493D
		internal NamedPipeConnectionPoolSettings Clone()
		{
			return new NamedPipeConnectionPoolSettings(this);
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x00136745 File Offset: 0x00134945
		internal bool IsMatch(NamedPipeConnectionPoolSettings namedPipe)
		{
			return !(this.groupName != namedPipe.groupName) && !(this.idleTimeout != namedPipe.idleTimeout) && this.maxOutputConnectionsPerEndpoint == namedPipe.maxOutputConnectionsPerEndpoint;
		}

		// Token: 0x04003307 RID: 13063
		private string groupName;

		// Token: 0x04003308 RID: 13064
		private TimeSpan idleTimeout;

		// Token: 0x04003309 RID: 13065
		private int maxOutputConnectionsPerEndpoint;
	}
}
