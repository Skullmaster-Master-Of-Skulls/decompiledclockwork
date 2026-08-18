using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.CustomForms
{
	// Token: 0x0200005D RID: 93
	public class CustomDataRestClientManager : BearerTokenRestProxy<ICustomDataClientManager>, ICustomDataClientManager, IWebService
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0000AC81 File Offset: 0x00008E81
		public CustomDataRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000AC8B File Offset: 0x00008E8B
		public CustomDataRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000AC98 File Offset: 0x00008E98
		public async Task<CustomDataSetDTO> LoadDataAsync(CustomDataContextDTO context, IList<Guid> dataInstanceIds)
		{
			LoadCustomDataReq loadCustomDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCustomDataReq>();
			loadCustomDataReq.Context = context;
			loadCustomDataReq.DataInstanceIds = ((dataInstanceIds != null) ? dataInstanceIds.ToArray<Guid>() : null);
			return await this.PostAsync<LoadCustomDataReq, CustomDataSetDTO>(loadCustomDataReq, "customdata/loadcustomdata").ConfigureAwait(false);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public CustomDataSetDTO LoadData(CustomDataContextDTO context, IList<Guid> dataInstanceIds)
		{
			LoadCustomDataReq loadCustomDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCustomDataReq>();
			loadCustomDataReq.Context = context;
			loadCustomDataReq.DataInstanceIds = ((dataInstanceIds != null) ? dataInstanceIds.ToArray<Guid>() : null);
			return base.Post<LoadCustomDataReq, CustomDataSetDTO>(loadCustomDataReq, "customdata/loadcustomdata");
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000AD30 File Offset: 0x00008F30
		public async Task SaveCustomFormsDataAsync(CustomDataSetDTO dataSet, IList<Guid> dataInstanceIds)
		{
			SaveCustomFormsDataReq saveCustomFormsDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveCustomFormsDataReq>();
			saveCustomFormsDataReq.DataSet = dataSet;
			saveCustomFormsDataReq.DataInstanceIds = dataInstanceIds;
			await this.PostAsync<SaveCustomFormsDataReq>(saveCustomFormsDataReq, "customdata").ConfigureAwait(false);
		}
	}
}
