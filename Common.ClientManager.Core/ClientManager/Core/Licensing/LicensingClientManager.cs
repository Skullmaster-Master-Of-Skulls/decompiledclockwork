using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Licensing;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Licensing;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Exceptions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Licensing
{
	// Token: 0x02000046 RID: 70
	public class LicensingClientManager : ILicensingClientManager, IWebService
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		public LicensingProductStatusResp GetProductStatus(string ProductName)
		{
			LicensingProductStatusReq licensingProductStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LicensingProductStatusReq>();
			licensingProductStatusReq.ProductName = ProductName;
			return ClientServiceFactory.GetClientInstance<ILicensing>().GetProductStatus(licensingProductStatusReq);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		public LicensingProductStatusResp GetProductStatus(Group Module)
		{
			GroupDataAttribute attribute = GroupDataAttribute.GetAttribute(Module);
			return string.IsNullOrEmpty((attribute != null) ? attribute.LicensingProductName : null) ? null : this.GetProductStatus(attribute.LicensingProductName);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public LicenseState GetLicenseState(LicenseInfoDTO Key)
		{
			GetLicenseStateReq getLicenseStateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetLicenseStateReq>();
			getLicenseStateReq.Key = Key;
			return ClientServiceFactory.GetClientInstance<ILicensing>().GetLicenseState(getLicenseStateReq).Status;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000BE44 File Offset: 0x0000A044
		public IList<LicenseInfoDTO> GetKeys()
		{
			LicensingKeysReq licKeysReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LicensingKeysReq>();
			return ClientServiceFactory.GetClientInstance<ILicensing>().GetKeys(licKeysReq).Keys;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000BE74 File Offset: 0x0000A074
		public void SaveValidationParameters(string productName, string validationParameters)
		{
			LicensingValidationParametersReq licensingValidationParametersReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LicensingValidationParametersReq>();
			licensingValidationParametersReq.Parameters = new ValidationParameters
			{
				ProductName = productName,
				Parameters = validationParameters
			};
			ClientServiceFactory.GetClientInstance<ILicensing>().SaveValidationParameters(licensingValidationParametersReq);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public void ImportKey(LicenseInfoDTO license)
		{
			LicensingImportKeyReq licensingImportKeyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LicensingImportKeyReq>();
			licensingImportKeyReq.License = license;
			ClientServiceFactory.GetClientInstance<ILicensing>().ImportKey(licensingImportKeyReq);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		public IDictionary<string, LicenseKeyInfo> FromFile(string filename)
		{
			IDictionary<string, LicenseKeyInfo> result;
			try
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Open))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					Dictionary<string, LicenseKeyInfo> dictionary = (Dictionary<string, LicenseKeyInfo>)binaryFormatter.Deserialize(fileStream);
					result = dictionary;
				}
			}
			catch (Exception ex)
			{
				throw new InvalidLicenseKeyException("Invalid key file." + Environment.NewLine + ex.Message, ex);
			}
			return result;
		}
	}
}
