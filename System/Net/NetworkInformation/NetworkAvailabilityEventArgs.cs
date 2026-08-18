using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000612 RID: 1554
	public class NetworkAvailabilityEventArgs : EventArgs
	{
		// Token: 0x06002FFA RID: 12282 RVA: 0x000CF5AB File Offset: 0x000CE5AB
		internal NetworkAvailabilityEventArgs(bool isAvailable)
		{
			this.isAvailable = isAvailable;
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000CF5BA File Offset: 0x000CE5BA
		public bool IsAvailable
		{
			get
			{
				return this.isAvailable;
			}
		}

		// Token: 0x04002DD1 RID: 11729
		private bool isAvailable;
	}
}
