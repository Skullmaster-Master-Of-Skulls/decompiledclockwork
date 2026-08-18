using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EncryptionClassLibrary;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000186 RID: 390
	public static class ServiceProvidersOriginal
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00079530 File Offset: 0x00077730
		public static ServiceProviderType GetServiceProviderType(this int serviceProviderTypeId, ServiceProvidersOperationContext opContext)
		{
			IList<ServiceProviderType> serviceProviderTypes = opContext.ServiceProviderTypes;
			return serviceProviderTypes.FirstOrDefault((ServiceProviderType g) => g.ServiceProviderTypeId == serviceProviderTypeId);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00079568 File Offset: 0x00077768
		public static string DecryptString(this IDataReader record, IBatchDecryptor decryptor, string colName)
		{
			return (record[colName] is DBNull) ? string.Empty : decryptor.Decrypt((byte[])record[colName]);
		}
	}
}
