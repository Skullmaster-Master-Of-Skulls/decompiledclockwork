using System;

namespace TechnoPro.Common.DAO.Impl.Legacy.QueryStorage
{
	// Token: 0x020000AA RID: 170
	public static class QueryStorageLegacyAccommodation
	{
		// Token: 0x0400022B RID: 555
		internal const string QI_ADD_LOA_ISSUED_ROW = "INSERT INTO accommodationloaissued (personid,lucourseid,whoissued,issuedmethod,loa)\r\nVALUES (@pid,@lucid,@whoissued,@issuedmethod,@loa)";

		// Token: 0x0400022C RID: 556
		internal const string QI_CREATE_OR_UPDATE_ACCOMMODATION_APPROVAL_NOTE = "IF EXISTS(SELECT personid FROM AccommodationsApprovalNotes WHERE personid=@pid)\r\n    UPDATE AccommodationsApprovalNotes SET controlvalue=@bb WHERE personid=@pid\r\nELSE\r\n    INSERT INTO AccommodationsApprovalNotes (whoentered,personid,controlvalue) VALUES (@whoentered,@pid,@bb)";

		// Token: 0x0400022D RID: 557
		internal const string QS_LOAD_ACCOMMODATION_APPROVAL_NOTES = "SELECT controlvalue FROM AccommodationsApprovalNotes WHERE personid=@pid";
	}
}
