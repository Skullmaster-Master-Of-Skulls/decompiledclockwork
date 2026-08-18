using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.ClientManager.ICore.AlternateFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AlternateFormat
{
	// Token: 0x0200008C RID: 140
	public class MediaVendorRestClientManager : BearerTokenRestProxy<IMediaVendorClientManager>, IMediaVendorClientManager, IWebService
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x0001019B File Offset: 0x0000E39B
		public MediaVendorRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000101A5 File Offset: 0x0000E3A5
		public MediaVendorRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public int CreateMediaVendor(MediaVendorDTO vendor)
		{
			return base.Post<MediaVendorDTO, int>(vendor, "mediavendor");
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000101BE File Offset: 0x0000E3BE
		public bool UpdateMediaVendor(MediaVendorDTO vendor)
		{
			return base.Post<MediaVendorDTO, bool>(vendor, "mediavendor/update");
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000101CC File Offset: 0x0000E3CC
		public void DeleteMediaVendor(int vendorId)
		{
			base.Delete(string.Format("mediavendor/id/{0}", vendorId));
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public MediaVendorDTO LoadMediaVendorById(int id)
		{
			return base.Get<MediaVendorDTO>(string.Format("mediavendor/id/{0}", id), true);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000101FD File Offset: 0x0000E3FD
		public MediaVendorDTO LoadMediaVendorByName(string name)
		{
			return base.Get<MediaVendorDTO>(string.Format("mediavendor/name/{0}", name), true);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00010211 File Offset: 0x0000E411
		public IList<MediaVendorDTO> LoadAllMediaVendors()
		{
			return base.GetMany<MediaVendorDTO>("mediavendor", true);
		}
	}
}
