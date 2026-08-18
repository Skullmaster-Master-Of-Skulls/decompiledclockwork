using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Protocols.WSTrust;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000107 RID: 263
	public abstract class WebServiceDescriptor : RoleDescriptor
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001F220 File Offset: 0x0001D420
		public ICollection<DisplayClaim> ClaimTypesOffered
		{
			get
			{
				return this._claimTypesOffered;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0001F228 File Offset: 0x0001D428
		public ICollection<DisplayClaim> ClaimTypesRequested
		{
			get
			{
				return this._claimTypesRequested;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001F230 File Offset: 0x0001D430
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001F238 File Offset: 0x0001D438
		public string ServiceDescription
		{
			get
			{
				return this._serviceDescription;
			}
			set
			{
				this._serviceDescription = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001F241 File Offset: 0x0001D441
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001F249 File Offset: 0x0001D449
		public string ServiceDisplayName
		{
			get
			{
				return this._serviceDisplayName;
			}
			set
			{
				this._serviceDisplayName = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001F252 File Offset: 0x0001D452
		public ICollection<EndpointReference> TargetScopes
		{
			get
			{
				return this._targetScopes;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001F25A File Offset: 0x0001D45A
		public ICollection<Uri> TokenTypesOffered
		{
			get
			{
				return this._tokenTypesOffered;
			}
		}

		// Token: 0x04000AA0 RID: 2720
		private Collection<DisplayClaim> _claimTypesOffered = new Collection<DisplayClaim>();

		// Token: 0x04000AA1 RID: 2721
		private Collection<DisplayClaim> _claimTypesRequested = new Collection<DisplayClaim>();

		// Token: 0x04000AA2 RID: 2722
		private string _serviceDisplayName;

		// Token: 0x04000AA3 RID: 2723
		private string _serviceDescription;

		// Token: 0x04000AA4 RID: 2724
		private Collection<EndpointReference> _targetScopes = new Collection<EndpointReference>();

		// Token: 0x04000AA5 RID: 2725
		private Collection<Uri> _tokenTypesOffered = new Collection<Uri>();
	}
}
