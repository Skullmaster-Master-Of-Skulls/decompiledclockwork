using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Azure.Storage;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Azure.Storage;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Azure.Storage
{
	// Token: 0x0200007D RID: 125
	public class ClockWorkSasTokenProviderClientManager : IClockWorkSasTokenProviderClientManager, IWebService
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00014B38 File Offset: 0x00012D38
		public Uri GetContainerSasUri(TokenBasedClientCredentialsDTO clientCredentials, string containerName, bool useClientIdPrefix = false, AzureSharedAccessBlobPermissions permissions = AzureSharedAccessBlobPermissions.Read | AzureSharedAccessBlobPermissions.List)
		{
			GetContainerSasUriReq getContainerSasUriReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetContainerSasUriReq>();
			getContainerSasUriReq.ClientCredentials = clientCredentials;
			getContainerSasUriReq.ContainerName = containerName;
			getContainerSasUriReq.Permissions = permissions;
			getContainerSasUriReq.UseClientIdPrefix = useClientIdPrefix;
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IClockWorkSasTokenProvider cloudServiceClientInstance = ClientServiceFactory.GetCloudServiceClientInstance<IClockWorkSasTokenProvider>(clientCache.SasTokenProviderCloudServiceUri);
			return cloudServiceClientInstance.GetContainerSasUri(getContainerSasUriReq).ContainerSasUri;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00014B94 File Offset: 0x00012D94
		public GetUpdatingSystemClientPrivateContainerSasUriResp GetUpdatingSystemClientPrivateContainerSasUri(TokenBasedClientCredentialsDTO clientCredentials)
		{
			GetUpdatingSystemClientPrivateContainerSasUriReq getUpdatingSystemClientPrivateContainerSasUriReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetUpdatingSystemClientPrivateContainerSasUriReq>();
			getUpdatingSystemClientPrivateContainerSasUriReq.ClientCredentials = clientCredentials;
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			IClockWorkSasTokenProvider cloudServiceClientInstance = ClientServiceFactory.GetCloudServiceClientInstance<IClockWorkSasTokenProvider>(clientCache.SasTokenProviderCloudServiceUri);
			return cloudServiceClientInstance.GetUpdatingSystemClientPrivateContainerSasUri(getUpdatingSystemClientPrivateContainerSasUriReq);
		}
	}
}
