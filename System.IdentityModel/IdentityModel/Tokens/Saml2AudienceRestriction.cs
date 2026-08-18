using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000137 RID: 311
	public class Saml2AudienceRestriction
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0002481C File Offset: 0x00022A1C
		public Saml2AudienceRestriction()
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002482F File Offset: 0x00022A2F
		public Saml2AudienceRestriction(Uri audience) : this(new Uri[]
		{
			audience
		})
		{
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00024844 File Offset: 0x00022A44
		public Saml2AudienceRestriction(IEnumerable<Uri> audiences)
		{
			if (audiences == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("audiences");
			}
			foreach (Uri uri in audiences)
			{
				if (null == uri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("audiences");
				}
				this.audiences.Add(uri);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000248D0 File Offset: 0x00022AD0
		public Collection<Uri> Audiences
		{
			get
			{
				return this.audiences;
			}
		}

		// Token: 0x04000B3D RID: 2877
		private Collection<Uri> audiences = new Collection<Uri>();
	}
}
