using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
	// Token: 0x02000422 RID: 1058
	internal class DesignTimeLicense : License, ISerialKeyLicense
	{
		// Token: 0x06002612 RID: 9746 RVA: 0x0007D1A4 File Offset: 0x0007B3A4
		internal DesignTimeLicense(ILicenseKey licenseKey)
		{
			this._key = licenseKey.Key;
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x0007D1B8 File Offset: 0x0007B3B8
		public override string LicenseKey
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0007D1C0 File Offset: 0x0007B3C0
		public sealed override void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x0007D1C9 File Offset: 0x0007B3C9
		internal static DesignTimeLicense CreateDesigntimeLicense(ILicenseKey key)
		{
			return new DesignTimeLicense(key);
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x0007D1D1 File Offset: 0x0007B3D1
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x040009B3 RID: 2483
		private readonly string _key;
	}
}
