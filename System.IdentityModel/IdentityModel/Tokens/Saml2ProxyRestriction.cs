using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000140 RID: 320
	public class Saml2ProxyRestriction
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000250AE File Offset: 0x000232AE
		public Collection<Uri> Audiences
		{
			get
			{
				return this.audiences;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000250B6 File Offset: 0x000232B6
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x000250BE File Offset: 0x000232BE
		public int? Count
		{
			get
			{
				return this.count;
			}
			set
			{
				if (value != null && value.Value < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", SR.GetString("ID0002")));
				}
				this.count = value;
			}
		}

		// Token: 0x04000B5D RID: 2909
		private Collection<Uri> audiences = new AbsoluteUriCollection();

		// Token: 0x04000B5E RID: 2910
		private int? count;
	}
}
