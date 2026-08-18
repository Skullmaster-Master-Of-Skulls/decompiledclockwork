using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AlternateFormat
{
	// Token: 0x0200009E RID: 158
	public interface IMediaVendorClientManager : IWebService
	{
		// Token: 0x06000503 RID: 1283
		int CreateMediaVendor(MediaVendorDTO vendor);

		// Token: 0x06000504 RID: 1284
		bool UpdateMediaVendor(MediaVendorDTO vendor);

		// Token: 0x06000505 RID: 1285
		void DeleteMediaVendor(int vendorId);

		// Token: 0x06000506 RID: 1286
		MediaVendorDTO LoadMediaVendorById(int id);

		// Token: 0x06000507 RID: 1287
		MediaVendorDTO LoadMediaVendorByName(string name);

		// Token: 0x06000508 RID: 1288
		IList<MediaVendorDTO> LoadAllMediaVendors();
	}
}
