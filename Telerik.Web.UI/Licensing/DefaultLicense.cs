using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
	// Token: 0x02000421 RID: 1057
	internal class DefaultLicense : License, ISerialKeyLicense
	{
		// Token: 0x0600260D RID: 9741 RVA: 0x0007D182 File Offset: 0x0007B382
		internal DefaultLicense(ILicenseKey key)
		{
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x0007D18A File Offset: 0x0007B38A
		public override string LicenseKey
		{
			get
			{
				return "default@telerik.com";
			}
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0007D191 File Offset: 0x0007B391
		public sealed override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0007D19A File Offset: 0x0007B39A
		internal static DefaultLicense CreateDefaultLicense(ILicenseKey key)
		{
			return new DefaultLicense(key);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0007D1A2 File Offset: 0x0007B3A2
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x040009B2 RID: 2482
		private const string DefaultLicenseKey = "default@telerik.com";
	}
}
