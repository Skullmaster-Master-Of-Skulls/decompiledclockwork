using System;

namespace TechnoPro.Common.DAO.Impl.ServiceProvidersOriginal.QueryStorageServiceProvidersOriginal
{
	// Token: 0x02000068 RID: 104
	public class QueryStorageProviders
	{
		// Token: 0x0400010C RID: 268
		internal const string QS_PROVIDER = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\n";

		// Token: 0x0400010D RID: 269
		internal const string QS_PROVIDER_BASE = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\n";

		// Token: 0x0400010E RID: 270
		internal const string QS_PROVIDER_BY_ID = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.serviceproviderid=@spid";

		// Token: 0x0400010F RID: 271
		internal const string QS_PROVIDER_BY_STUDENT_NUMBER = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.student_no=@snum";

		// Token: 0x04000110 RID: 272
		internal const string QS_PROVIDER_BY_USERNAME = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.altid=@username";

		// Token: 0x04000111 RID: 273
		internal const string QS_PROVIDER_BASE_BY_ID = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.serviceproviderid=@spid";

		// Token: 0x04000112 RID: 274
		internal const string QS_PROVIDER_BASE_BY_STUDENT_NUMBER = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.student_no=@snum";

		// Token: 0x04000113 RID: 275
		internal const string QS_PROVIDER_BASE_BY_USERNAME = "SELECT    sp.[ServiceProviderId],sp.[firstname],sp.[middlename],sp.[lastname],sp.[student_no],sp.[altid],sp.[additionalservices],sp.[specialization],\r\n            sp.[notes1],sp.[notes2],sp.[email],sp.[phone1],sp.[phone2],sp.[phonenote],sp.[address],sp.[dateentered],sp.[whoentered],sp.[isactive],\r\n            sp.[isactivenote],sp.[address2],sp.[email2],sp.[addressactive],sp.[address2active],sp.[RegistrationComplete]\r\nFROM        ServiceProviders sp\r\nWHERE       sp.altid=@username";
	}
}
