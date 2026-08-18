using System;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B1 RID: 689
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventProperty
	{
		// Token: 0x060018EC RID: 6380 RVA: 0x0005B378 File Offset: 0x00059578
		internal EventProperty(object value)
		{
			this.value = value;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0005B387 File Offset: 0x00059587
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000C34 RID: 3124
		private object value;
	}
}
