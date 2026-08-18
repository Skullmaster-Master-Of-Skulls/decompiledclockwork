using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider.QueryStorage
{
	// Token: 0x0200005F RID: 95
	public class QueryStorageServiceProviderApplication
	{
		// Token: 0x040000ED RID: 237
		internal const string QS_APPLICATION_BY_ID = "SELECT    spa.SPApplicationId,spa.SPProviderId,p.firstname,p.middlename,p.lastname,p.student_no,\r\n\t\t  sp.UserName,sp.ExternalId,sp.Address1,sp.Address1IsPrimary,sp.AlternateEmail,sp.Email,\r\n\t\t  sp.IsActive,sp.Note1,sp.Note2,sp.Phone1,sp.Phone2,sp.PhoneNote,sp.Specializations,\r\n\t\t  spa.SPProviderTypeId,spt.ProviderTypeTitle,spt.ProviderTypeDescription,spt.ProviderTypeIsActive,\r\n\t\t  spt.SPProviderTypeBehaviourCode,\r\n\t\t  spa.SPApplicationAvailabilityTypeId,sat.ApplicationAvailabilityTitle,sat.ApplicationAvailabilityDescription,\r\n\t\t  sat.ApplicationAvailabilityIsActive,sat.ApplicationAvailabilityIsVisible,\r\n\t\t  spa.note1,spa.note2,spa.dateentered,spa.whoentered,spa.isactive,spa.rateofpay,\r\n\t\t  spa.sprateofpaytypeid,rop.rateofpaytitle,rop.rateofpaydescription,rop.RateOfPayIsOneTimePayment,\r\n\t\t  rop.RateOfPayIsHourlyRate,rop.rateofpayisactive\r\nFROM        spapplication spa LEFT JOIN SPProvider sp ON sp.SPProviderId=spa.SPProviderId \r\n\t\t\tLEFT JOIN people_hide p ON p.personid=sp.PersonId \r\n\t\t\tLEFT JOIN SPProviderType spt ON spt.SPProviderTypeId=spa.SPProviderTypeId \r\n\t\t\tLEFT JOIN SPApplicationAvailabilityType sat ON sat.SPApplicationAvailabilityTypeId=spa.SPApplicationAvailabilityTypeId \r\n\t\t\tLEFT JOIN sprateofpaytype rop ON rop.sprateofpaytypeid=spa.SPRateOfPayTypeId \r\nWHERE\t\tspa.SPApplicationId=@id";
	}
}
