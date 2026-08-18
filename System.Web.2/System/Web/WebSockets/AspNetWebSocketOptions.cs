using System;

namespace System.Web.WebSockets
{
	// Token: 0x020001B8 RID: 440
	public sealed class AspNetWebSocketOptions
	{
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00047F41 File Offset: 0x00046141
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00047F49 File Offset: 0x00046149
		public bool RequireSameOrigin { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00047F52 File Offset: 0x00046152
		// (set) Token: 0x060016AA RID: 5802 RVA: 0x00047F5A File Offset: 0x0004615A
		public string SubProtocol
		{
			get
			{
				return this._subProtocol;
			}
			set
			{
				if (value != null && !SubProtocolUtil.IsValidSubProtocolName(value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._subProtocol = value;
			}
		}

		// Token: 0x040016BB RID: 5819
		private string _subProtocol;
	}
}
