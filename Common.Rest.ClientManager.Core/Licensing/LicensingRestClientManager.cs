using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.ClientManager.ICore.Licensing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Licensing
{
	// Token: 0x0200003A RID: 58
	public class LicensingRestClientManager : BearerTokenRestProxy<ILicensingClientManager>, ILicensingClientManager, IWebService
	{
		// Token: 0x0600021E RID: 542 RVA: 0x000071D5 File Offset: 0x000053D5
		public LicensingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000071DF File Offset: 0x000053DF
		public LicensingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000071EA File Offset: 0x000053EA
		public LicensingProductStatusResp GetProductStatus(string ProductName)
		{
			return base.Get<LicensingProductStatusResp>(string.Format("licensing/productstatus/productname/{0}", ProductName), true);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007200 File Offset: 0x00005400
		public LicensingProductStatusResp GetProductStatus(Group Module)
		{
			GroupDataAttribute attribute = GroupDataAttribute.GetAttribute(Module);
			if (!string.IsNullOrEmpty((attribute != null) ? attribute.LicensingProductName : null))
			{
				return this.GetProductStatus(attribute.LicensingProductName);
			}
			return null;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007235 File Offset: 0x00005435
		public LicenseState GetLicenseState(LicenseInfoDTO Key)
		{
			return base.Post<LicenseInfoDTO, LicenseState>(Key, "licensing/licensestate");
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007243 File Offset: 0x00005443
		public IList<LicenseInfoDTO> GetKeys()
		{
			return base.GetMany<LicenseInfoDTO>("licensing/keys", true);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007254 File Offset: 0x00005454
		public void SaveValidationParameters(string productName, string validationParameters)
		{
			ValidationParameters model = new ValidationParameters
			{
				ProductName = productName,
				Parameters = validationParameters
			};
			base.Post<ValidationParameters>(model, "licensing/savevalidationparameters");
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007281 File Offset: 0x00005481
		public void ImportKey(LicenseInfoDTO license)
		{
			base.Post<LicenseInfoDTO>(license, "licensing/importkey");
		}
	}
}
