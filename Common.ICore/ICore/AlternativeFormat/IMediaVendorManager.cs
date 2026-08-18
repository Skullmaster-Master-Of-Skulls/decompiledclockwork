using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.ICore.AlternativeFormat
{
	// Token: 0x020000F2 RID: 242
	public interface IMediaVendorManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007C3 RID: 1987
		int CreateMediaVendor(MediaVendor vendor);

		// Token: 0x060007C4 RID: 1988
		bool UpdateMediaVendor(MediaVendor vendor);

		// Token: 0x060007C5 RID: 1989
		void DeleteMediaVendor(int vendorId);

		// Token: 0x060007C6 RID: 1990
		MediaVendor LoadMediaVendorById(int Id);

		// Token: 0x060007C7 RID: 1991
		MediaVendor LoadMediaVendorByName(string name);

		// Token: 0x060007C8 RID: 1992
		IList<MediaVendor> LoadAllMediaVendors();
	}
}
