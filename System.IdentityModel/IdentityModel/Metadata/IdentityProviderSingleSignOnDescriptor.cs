using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F4 RID: 244
	public class IdentityProviderSingleSignOnDescriptor : SingleSignOnDescriptor
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001A9D1 File Offset: 0x00018BD1
		public ICollection<ProtocolEndpoint> SingleSignOnServices
		{
			get
			{
				return this._singleSignOnServices;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001A9D9 File Offset: 0x00018BD9
		public ICollection<Saml2Attribute> SupportedAttributes
		{
			get
			{
				return this._supportedAttributes;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001A9E1 File Offset: 0x00018BE1
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x0001A9E9 File Offset: 0x00018BE9
		public bool WantAuthenticationRequestsSigned
		{
			get
			{
				return this._wantAuthenticationRequestsSigned;
			}
			set
			{
				this._wantAuthenticationRequestsSigned = value;
			}
		}

		// Token: 0x04000A6E RID: 2670
		private bool _wantAuthenticationRequestsSigned;

		// Token: 0x04000A6F RID: 2671
		private Collection<ProtocolEndpoint> _singleSignOnServices = new Collection<ProtocolEndpoint>();

		// Token: 0x04000A70 RID: 2672
		private Collection<Saml2Attribute> _supportedAttributes = new Collection<Saml2Attribute>();
	}
}
