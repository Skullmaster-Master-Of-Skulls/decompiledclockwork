using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Converter.CustomFormControls;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.CustomForms
{
	// Token: 0x02000060 RID: 96
	public class CustomFormRestClientManager : BearerTokenRestProxy<ICustomFormClientManager>, ICustomFormClientManager, IWebService
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x0000B165 File Offset: 0x00009365
		public CustomFormRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000B16F File Offset: 0x0000936F
		public CustomFormRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000B17C File Offset: 0x0000937C
		public async Task<CustomFormDTO> LoadFormByIdAsync(Guid formId)
		{
			return await this.GetAsync<CustomFormDTO>(string.Format("customform/formid/{0}", formId), true);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000B1C9 File Offset: 0x000093C9
		public CustomFormDTO LoadFormById(Guid formId)
		{
			return base.Get<CustomFormDTO>(string.Format("customform/formid/{0}", formId), true);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000B1E4 File Offset: 0x000093E4
		public Forest<CustomControlBaseDTO> LoadFormForestById(Guid formId)
		{
			CustomFormDTO customFormDTO = this.LoadFormById(formId);
			Guid guid;
			return (((customFormDTO != null) ? customFormDTO.Xml : null) ?? "").ExtractControlForest(out guid);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000B214 File Offset: 0x00009414
		public async Task<Forest<CustomControlBaseDTO>> LoadFormForestByIdAsync(Guid formId)
		{
			CustomFormDTO customFormDTO = await this.LoadFormByIdAsync(formId);
			Guid guid;
			return (((customFormDTO != null) ? customFormDTO.Xml : null) ?? "").ExtractControlForest(out guid);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000B264 File Offset: 0x00009464
		public async Task<Guid> CreateFormAsync(CustomFormDTO form)
		{
			return await this.PostAsync<CustomFormDTO, Guid>(form, "customform").ConfigureAwait(false);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000B2B4 File Offset: 0x000094B4
		public async Task DeleteFormAsync(Guid formId)
		{
			await this.DeleteAsync(string.Format("customform/formid/{0}", formId)).ConfigureAwait(false);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B304 File Offset: 0x00009504
		public async Task UpdateFormAsync(CustomFormDTO form)
		{
			await this.PutAsync("customform").ConfigureAwait(false);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B34C File Offset: 0x0000954C
		public async Task<IList<CustomFormDTO>> LoadAllCustomFormsAsync()
		{
			return await this.GetManyAsync<CustomFormDTO>("customform", true);
		}
	}
}
