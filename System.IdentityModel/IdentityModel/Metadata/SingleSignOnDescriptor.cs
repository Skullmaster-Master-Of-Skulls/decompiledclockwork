using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000106 RID: 262
	public class SingleSignOnDescriptor : RoleDescriptor
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
		public ICollection<Uri> NameIdentifierFormats
		{
			get
			{
				return this.nameIdFormats;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		public IndexedProtocolEndpointDictionary ArtifactResolutionServices
		{
			get
			{
				return this.artifactResolutionServices;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		public Collection<ProtocolEndpoint> SingleLogoutServices
		{
			get
			{
				return this.singleLogoutServices;
			}
		}

		// Token: 0x04000A9D RID: 2717
		private IndexedProtocolEndpointDictionary artifactResolutionServices = new IndexedProtocolEndpointDictionary();

		// Token: 0x04000A9E RID: 2718
		private Collection<ProtocolEndpoint> singleLogoutServices = new Collection<ProtocolEndpoint>();

		// Token: 0x04000A9F RID: 2719
		private Collection<Uri> nameIdFormats = new Collection<Uri>();
	}
}
