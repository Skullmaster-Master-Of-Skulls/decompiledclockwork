using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Selectors;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200010C RID: 268
	public class AudienceRestriction
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		public AudienceRestriction()
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001F5EE File Offset: 0x0001D7EE
		public AudienceRestriction(AudienceUriMode audienceMode)
		{
			this._audienceMode = audienceMode;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001F60F File Offset: 0x0001D80F
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x0001F617 File Offset: 0x0001D817
		public AudienceUriMode AudienceMode
		{
			get
			{
				return this._audienceMode;
			}
			set
			{
				this._audienceMode = value;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001F620 File Offset: 0x0001D820
		public Collection<Uri> AllowedAudienceUris
		{
			get
			{
				return this._audience;
			}
		}

		// Token: 0x04000AAA RID: 2730
		private AudienceUriMode _audienceMode = AudienceUriMode.Always;

		// Token: 0x04000AAB RID: 2731
		private Collection<Uri> _audience = new Collection<Uri>();
	}
}
