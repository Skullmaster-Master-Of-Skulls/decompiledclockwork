using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.AlternativeFormat;
using TechnoPro.Common.ICore.AlternativeFormat;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.Core.AlternativeFormat
{
	// Token: 0x0200015C RID: 348
	public class MediaVendorManager : IMediaVendorManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00073591 File Offset: 0x00071791
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00073599 File Offset: 0x00071799
		private IMediaVendorDAO MediaVendorDAO { get; set; }

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000735A2 File Offset: 0x000717A2
		public MediaVendorManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.MediaVendorDAO = new MediaVendorDAO(opContext);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x000735C1 File Offset: 0x000717C1
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x000735C9 File Offset: 0x000717C9
		public OperationContext OpContext { get; set; }

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000735D4 File Offset: 0x000717D4
		public int CreateMediaVendor(MediaVendor vendor)
		{
			return this.MediaVendorDAO.CreateMediaVendor(vendor);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000735F4 File Offset: 0x000717F4
		public bool UpdateMediaVendor(MediaVendor vendor)
		{
			return this.MediaVendorDAO.UpdateMediaVendor(vendor);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00073612 File Offset: 0x00071812
		public void DeleteMediaVendor(int vendorId)
		{
			this.MediaVendorDAO.DeleteMediaVendor(vendorId);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00073624 File Offset: 0x00071824
		public MediaVendor LoadMediaVendorById(int Id)
		{
			return this.MediaVendorDAO.LoadMediaVendorById(Id);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00073644 File Offset: 0x00071844
		public MediaVendor LoadMediaVendorByName(string name)
		{
			return this.MediaVendorDAO.LoadMediaVendorByName(name);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00073664 File Offset: 0x00071864
		public IList<MediaVendor> LoadAllMediaVendors()
		{
			return this.MediaVendorDAO.LoadAllMediaVendors();
		}
	}
}
