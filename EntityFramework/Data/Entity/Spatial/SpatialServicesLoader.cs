using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace System.Data.Entity.Spatial
{
	// Token: 0x020002CE RID: 718
	internal class SpatialServicesLoader
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x0007E5C9 File Offset: 0x0007C7C9
		public SpatialServicesLoader(IDbDependencyResolver resolver)
		{
			this._resolver = resolver;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0007E5D8 File Offset: 0x0007C7D8
		public virtual DbSpatialServices LoadDefaultServices()
		{
			DbSpatialServices service = this._resolver.GetService<DbSpatialServices>();
			if (service != null)
			{
				return service;
			}
			service = this._resolver.GetService(new DbProviderInfo("System.Data.SqlClient", "2012"));
			if (service != null && service.NativeTypesAvailable)
			{
				return service;
			}
			return DefaultSpatialServices.Instance;
		}

		// Token: 0x040008AF RID: 2223
		private readonly IDbDependencyResolver _resolver;
	}
}
