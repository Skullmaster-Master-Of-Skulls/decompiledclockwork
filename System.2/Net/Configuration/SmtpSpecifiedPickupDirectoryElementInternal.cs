using System;

namespace System.Net.Configuration
{
	// Token: 0x02000347 RID: 839
	internal sealed class SmtpSpecifiedPickupDirectoryElementInternal
	{
		// Token: 0x06001E28 RID: 7720 RVA: 0x0008D953 File Offset: 0x0008BB53
		internal SmtpSpecifiedPickupDirectoryElementInternal(SmtpSpecifiedPickupDirectoryElement element)
		{
			this.pickupDirectoryLocation = element.PickupDirectoryLocation;
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0008D967 File Offset: 0x0008BB67
		internal string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
		}

		// Token: 0x04001CB5 RID: 7349
		private string pickupDirectoryLocation;
	}
}
