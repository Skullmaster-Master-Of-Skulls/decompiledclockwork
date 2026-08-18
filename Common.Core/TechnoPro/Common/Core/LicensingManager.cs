using System;
using System.Collections.Generic;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO;
using TechnoPro.Common.DAO.Impl;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Exceptions;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core
{
	// Token: 0x0200001F RID: 31
	public class LicensingManager : ILicensingManager
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006125 File Offset: 0x00004325
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000612D File Offset: 0x0000432D
		public ILicenseKeyDAO LicenseKeyDAO { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006136 File Offset: 0x00004336
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000613E File Offset: 0x0000433E
		protected IRepository<string, LicenseProductInfo> ProductParameters { get; set; }

		// Token: 0x060000F9 RID: 249 RVA: 0x00006148 File Offset: 0x00004348
		public LicensingManager()
		{
			this.LicenseKeyDAO = new LicenseKeyDAO();
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			this.ProductParameters = (IRepository<string, LicenseProductInfo>)cacheStorageManager["Licensing.ProductParameters"];
			bool flag = this.ProductParameters == null;
			if (flag)
			{
				this.ProductParameters = new Repository<string, LicenseProductInfo>();
				List<LicenseProductInfo> productsInfo = this.LicenseKeyDAO.GetProductsInfo();
				foreach (LicenseProductInfo entity in productsInfo)
				{
					this.ProductParameters.Save(entity);
				}
				cacheStorageManager["Licensing.ProductParameters"] = this.ProductParameters;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000620C File Offset: 0x0000440C
		public ProductLicenseState GetProductState(string productName, out DateTime? expiryDate)
		{
			expiryDate = null;
			LicenseKeyInfo productKey = this.GetProductKey(productName);
			bool flag = productKey == null;
			ProductLicenseState result;
			if (flag)
			{
				result = ProductLicenseState.NoneLicense;
			}
			else
			{
				expiryDate = productKey.ExpiryDate;
				bool flag2 = this.ProductParameters.Contains(productName) && productKey.IsValidKey(this.ProductParameters[productName].ProductParameters);
				if (flag2)
				{
					bool flag3 = productKey.LicenseType.DoesExpire();
					if (flag3)
					{
						bool flag4 = productKey.ExpiryDate != null;
						if (flag4)
						{
							result = ((productKey.ExpiryDate.Value < DateTime.Now) ? ProductLicenseState.OutdatedLicense : ProductLicenseState.Licensed);
						}
						else
						{
							result = ProductLicenseState.NotValidLicense;
						}
					}
					else
					{
						result = ProductLicenseState.Licensed;
					}
				}
				else
				{
					result = ProductLicenseState.NotValidLicense;
				}
			}
			return result;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000062CC File Offset: 0x000044CC
		public void ImportKey(LicenseKeyInfo keyInfo)
		{
			bool flag = this.IsValidLicense(keyInfo);
			if (flag)
			{
				this._ImportKey(keyInfo);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000062F0 File Offset: 0x000044F0
		public IDictionary<string, LicenseKeyInfo> FromFile(string filename)
		{
			return this.LicenseKeyDAO.FromFile(filename);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006310 File Offset: 0x00004510
		public LicenseKeyInfo GetSupportPlanKey()
		{
			return this.LicenseKeyDAO.GetSupportPlanKey();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006330 File Offset: 0x00004530
		public List<LicenseKeyInfo> GetKeys()
		{
			return this.LicenseKeyDAO.GetKeys();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006350 File Offset: 0x00004550
		public List<string> GetProductNames()
		{
			return this.LicenseKeyDAO.GetProductNames();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006370 File Offset: 0x00004570
		public LicenseState GetLicenseState(LicenseKeyInfo key)
		{
			bool flag = this.ProductParameters.Contains(key.ProductName) && key.IsValidKey(this.ProductParameters[key.ProductName].ProductParameters);
			LicenseState result;
			if (flag)
			{
				bool flag2 = key.LicenseType.DoesExpire();
				if (flag2)
				{
					bool flag3 = key.ExpiryDate != null;
					if (flag3)
					{
						result = ((key.ExpiryDate.Value < DateTime.Now) ? LicenseState.Outdated : LicenseState.Updated);
					}
					else
					{
						result = LicenseState.Invalid;
					}
				}
				else
				{
					result = LicenseState.Updated;
				}
			}
			else
			{
				result = LicenseState.Invalid;
			}
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000640C File Offset: 0x0000460C
		public void SaveValidationParameters(string productName, string validationParameters)
		{
			this.LicenseKeyDAO.SaveValidationParameters(productName, validationParameters);
			bool flag = !this.ProductParameters.Contains(productName);
			if (flag)
			{
				this.ProductParameters.Save(new LicenseProductInfo
				{
					ProductName = productName,
					ProductParameters = validationParameters
				});
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000645C File Offset: 0x0000465C
		public LicenseKeyInfo GetProductKey(string productName)
		{
			return this.LicenseKeyDAO.GetProductKey(productName);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000647C File Offset: 0x0000467C
		public void ImportLicenseFromFile(string filename)
		{
			try
			{
				IDictionary<string, LicenseKeyInfo> dictionary = this.FromFile(filename);
				foreach (KeyValuePair<string, LicenseKeyInfo> keyValuePair in dictionary)
				{
					bool flag = keyValuePair.Value != null && this.IsValidLicense(keyValuePair.Value);
					if (flag)
					{
						this.SaveValidationParameters(keyValuePair.Value.ProductName, keyValuePair.Key);
						this._ImportKey(keyValuePair.Value);
					}
					else
					{
						CWLogger.Logger.Error("LicensingManager::ImportLicenseFromFile: License key '{0}' is invalid", keyValuePair.Value.ProductName);
					}
				}
			}
			catch (InvalidLicenseKeyException ex)
			{
				CWLogger.Logger.ErrorException(string.Format("LicensingManager::ImportLicenseFromFile: {0}", ex.ToString()), ex);
				throw;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006568 File Offset: 0x00004768
		private bool IsValidLicense(LicenseKeyInfo key)
		{
			bool flag = key.LicenseType.DoesExpire() && key.ExpiryDate != null && key.ExpiryDate.Value < DateTime.Now;
			bool result;
			if (flag)
			{
				CWLogger.Logger.Error("LicensingManager::IsValidLicense: License key '{0}' is expired", key.ProductName);
				result = false;
			}
			else
			{
				LicenseKeyInfo productKey = this.GetProductKey(key.ProductName);
				bool flag2 = productKey != null && productKey.IssuedDate > key.IssuedDate;
				if (flag2)
				{
					CWLogger.Logger.Error("LicensingManager::IsValidLicense: A newer license for '{0}' is already applied", key.ProductName);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006617 File Offset: 0x00004817
		protected void _ImportKey(LicenseKeyInfo key)
		{
			this.LicenseKeyDAO.Save(key);
		}
	}
}
