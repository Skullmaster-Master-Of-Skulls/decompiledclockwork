using System;
using System.Web;
using System.Web.SessionState;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.UI.ClientManager.Web.Core.DynamicForms;
using TechnoPro.Common.UI.Web.Entity.DynamicForms.Accommodations;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.DynamicForms
{
	// Token: 0x0200000B RID: 11
	public class AccommodationsWebClientManager : IAccommodationsWebClientManager
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000029FC File Offset: 0x00000BFC
		public AccommodationsExpiryDate GetStudentAccommodationsExpiryDate(int PersonId, bool useCache = false)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid);
			bool flag = settingValue <= 0;
			AccommodationsExpiryDate result;
			if (flag)
			{
				result = this.SetExpiryDateInCache(useCache, eAccommodationsExpiryDateStatus.NotUsingExpiryDate, null);
			}
			else
			{
				HttpSessionState session = HttpContext.Current.Session;
				if (useCache)
				{
					AccommodationsExpiryDate accommodationsExpiryDate = (AccommodationsExpiryDate)session["AccommodationsExpiryDate"];
					bool flag2 = accommodationsExpiryDate != null;
					if (flag2)
					{
						return accommodationsExpiryDate;
					}
				}
				IAccommodationsClientManager accommodationsClientManager = new AccommodationsClientManager();
				DateTime? studentAccommodationsExpiryDate = accommodationsClientManager.GetStudentAccommodationsExpiryDate(PersonId);
				bool flag3 = studentAccommodationsExpiryDate != null;
				if (flag3)
				{
					result = this.SetExpiryDateInCache(useCache, eAccommodationsExpiryDateStatus.Normal, studentAccommodationsExpiryDate);
				}
				else
				{
					result = this.SetExpiryDateInCache(useCache, webSettingsClientManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired) ? eAccommodationsExpiryDateStatus.BlankAndMeansExpired : eAccommodationsExpiryDateStatus.BlankAndMeansValid, null);
				}
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002AD0 File Offset: 0x00000CD0
		private AccommodationsExpiryDate SetExpiryDateInCache(bool useCache, eAccommodationsExpiryDateStatus status, DateTime? expiryDate = null)
		{
			AccommodationsExpiryDate accommodationsExpiryDate = new AccommodationsExpiryDate
			{
				Status = status,
				ExpiryDate = expiryDate
			};
			bool flag = !useCache;
			AccommodationsExpiryDate result;
			if (flag)
			{
				result = accommodationsExpiryDate;
			}
			else
			{
				HttpSessionState session = HttpContext.Current.Session;
				session.Add("AccommodationsExpiryDate", accommodationsExpiryDate);
				result = accommodationsExpiryDate;
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B20 File Offset: 0x00000D20
		public DateTime? GetEffectiveExpiryDate(int PersonId, bool useCache = false)
		{
			return this.GetEffectiveExpiryDate(this.GetStudentAccommodationsExpiryDate(PersonId, useCache));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B40 File Offset: 0x00000D40
		public DateTime? GetEffectiveExpiryDate(AccommodationsExpiryDate ExpiryDate)
		{
			DateTime date = DateTime.Now.Date;
			eAccommodationsExpiryDateStatus status = ExpiryDate.Status;
			eAccommodationsExpiryDateStatus eAccommodationsExpiryDateStatus = status;
			DateTime? result;
			if (eAccommodationsExpiryDateStatus != eAccommodationsExpiryDateStatus.Normal)
			{
				if (eAccommodationsExpiryDateStatus - eAccommodationsExpiryDateStatus.BlankAndMeansValid > 1)
				{
					result = ((ExpiryDate.ExpiryDate != null) ? ExpiryDate.ExpiryDate : new DateTime?(date.AddYears(-1)));
				}
				else
				{
					result = ((ExpiryDate.ExpiryDate != null) ? ExpiryDate.ExpiryDate : new DateTime?(DateTime.Now.AddYears(4)));
				}
			}
			else
			{
				result = ExpiryDate.ExpiryDate;
			}
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public bool AreAccommodationsCurrentlyExpired(int PersonId, DateTime DateToUseInsteadOfToday, bool useCache = false)
		{
			AccommodationsExpiryDate studentAccommodationsExpiryDate = this.GetStudentAccommodationsExpiryDate(PersonId, useCache);
			DateTime date = DateToUseInsteadOfToday.Date;
			eAccommodationsExpiryDateStatus status = studentAccommodationsExpiryDate.Status;
			eAccommodationsExpiryDateStatus eAccommodationsExpiryDateStatus = status;
			bool result;
			if (eAccommodationsExpiryDateStatus != eAccommodationsExpiryDateStatus.Normal)
			{
				result = (eAccommodationsExpiryDateStatus - eAccommodationsExpiryDateStatus.BlankAndMeansValid > 1);
			}
			else
			{
				result = (studentAccommodationsExpiryDate.ExpiryDate != null && studentAccommodationsExpiryDate.ExpiryDate.Value.Date < date);
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C50 File Offset: 0x00000E50
		public bool AreAccommodationsCurrentlyExpired(int PersonId, bool useCache = false)
		{
			return this.AreAccommodationsCurrentlyExpired(PersonId, DateTime.Now.Date, useCache);
		}

		// Token: 0x0400000F RID: 15
		private const string keyAccommodationsExpiryDate = "AccommodationsExpiryDate";
	}
}
