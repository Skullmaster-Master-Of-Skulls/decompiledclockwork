using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020002D1 RID: 721
	internal static class DbProviderFactoryExtensions
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x0007E7F0 File Offset: 0x0007C9F0
		public static string GetProviderInvariantName(this DbProviderFactory factory)
		{
			IEnumerable<DataRow> dataRows = DbProviderFactories.GetFactoryClasses().Rows.OfType<DataRow>();
			DataRow dataRow = new ProviderRowFinder().FindRow(factory.GetType(), (DataRow r) => DbProviderFactories.GetFactory(r).GetType() == factory.GetType(), dataRows);
			if (dataRow == null)
			{
				throw new NotSupportedException(Strings.ProviderNameNotFound(factory));
			}
			return (string)dataRow[2];
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0007E860 File Offset: 0x0007CA60
		internal static DbProviderServices GetProviderServices(this DbProviderFactory factory)
		{
			if (factory is EntityProviderFactory)
			{
				return EntityProviderServices.Instance;
			}
			IProviderInvariantName service = DbConfiguration.DependencyResolver.GetService(factory);
			return DbConfiguration.DependencyResolver.GetService(service.Name);
		}
	}
}
