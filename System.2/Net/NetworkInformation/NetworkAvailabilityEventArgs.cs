using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002DB RID: 731
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x060019E0 RID: 6624 RVA: 0x0007E4DB File Offset: 0x0007C6DB
		internal NetworkAvailabilityEventArgs(bool isAvailable)
		{
			this.isAvailable = isAvailable;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0007E4EA File Offset: 0x0007C6EA
		public bool IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		// Token: 0x04001A52 RID: 6738
		private bool isAvailable;
	}
}
