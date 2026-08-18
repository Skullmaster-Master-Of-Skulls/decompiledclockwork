using System;

namespace TechnoPro.Common.DAO.Impl.ConfidentialityAgreement
{
	// Token: 0x02000109 RID: 265
	internal static class QueryStorageConfidentialityAgreement
	{
		// Token: 0x0400046E RID: 1134
		internal const string IQ_Signed_Student_Confidentiality_Agreement = "insert into Student_ConfidentialityAgreement \r\n(PersonId, ModuleName)\r\nvalues (@personid, @modulename)";

		// Token: 0x0400046F RID: 1135
		internal const string SQ_Last_Signed_Confidentiality_Agreement_By_Module = "select * from Student_ConfidentialityAgreement where personid=@pid and ModuleName=@modulename";

		// Token: 0x04000470 RID: 1136
		internal const string SQ_Is_Confidentiality_Agreement_Sign_By_Name = "select 1 from Student_ConfidentialityAgreement where personid=@pid and ModuleName=@modulename";
	}
}
