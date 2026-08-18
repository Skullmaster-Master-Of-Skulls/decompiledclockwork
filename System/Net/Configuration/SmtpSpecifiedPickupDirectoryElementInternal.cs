using System;

namespace System.Net.Configuration
{
	// Token: 0x02000667 RID: 1639
	internal sealed class SmtpSpecifiedPickupDirectoryElementInternal
	{
		// Token: 0x060032BF RID: 12991 RVA: 0x000D7417 File Offset: 0x000D6417
		internal SmtpSpecifiedPickupDirectoryElementInternal(SmtpSpecifiedPickupDirectoryElement element)
		{
			this.pickupDirectoryLocation = element.PickupDirectoryLocation;
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000D742B File Offset: 0x000D642B
		internal string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
		}

		// Token: 0x04002F70 RID: 12144
		private string pickupDirectoryLocation;
	}
}
