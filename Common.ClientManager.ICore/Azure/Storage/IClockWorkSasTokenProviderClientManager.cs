using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Azure.Storage;

namespace TechnoPro.Common.ClientManager.ICore.Azure.Storage
{
	// Token: 0x02000076 RID: 118
	public interface IClockWorkSasTokenProviderClientManager : IWebService
	{
		// Token: 0x0600036B RID: 875
		Uri GetContainerSasUri(TokenBasedClientCredentialsDTO clientCredentials, string containerName, bool useClientIdPrefix = false, AzureSharedAccessBlobPermissions permissions = AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List);

		// Token: 0x0600036C RID: 876
		GetUpdatingSystemClientPrivateContainerSasUriResp GetUpdatingSystemClientPrivateContainerSasUri(TokenBasedClientCredentialsDTO clientCredentials);
	}
}
