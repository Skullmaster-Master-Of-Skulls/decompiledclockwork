using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.AlternativeFormat
{
	// Token: 0x020000CC RID: 204
	public interface IMediaVendorDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005DB RID: 1499
		int CreateMediaVendor(MediaVendor vendor);

		// Token: 0x060005DC RID: 1500
		bool UpdateMediaVendor(MediaVendor vendor);

		// Token: 0x060005DD RID: 1501
		void DeleteMediaVendor(int vendorId);

		// Token: 0x060005DE RID: 1502
		MediaVendor LoadMediaVendorById(int Id);

		// Token: 0x060005DF RID: 1503
		MediaVendor LoadMediaVendorByName(string name);

		// Token: 0x060005E0 RID: 1504
		IList<MediaVendor> LoadAllMediaVendors();
	}
}
