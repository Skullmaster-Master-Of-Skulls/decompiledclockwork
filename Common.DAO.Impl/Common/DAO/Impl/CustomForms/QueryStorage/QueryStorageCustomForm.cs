using System;

namespace TechnoPro.Common.DAO.Impl.CustomForms.QueryStorage
{
	// Token: 0x02000104 RID: 260
	public static class QueryStorageCustomForm
	{
		// Token: 0x04000441 RID: 1089
		internal const string QS_FORM_BY_ID = "SELECT CustomFormId,FormTitle,Xml,IsHidden FROM CustomForm WHERE CustomFormId=@formid";

		// Token: 0x04000442 RID: 1090
		internal const string QS_ALL_FORMS = "SELECT CustomFormId,FormTitle,Xml,IsHidden FROM CustomForm ORDER BY FormTitle";

		// Token: 0x04000443 RID: 1091
		internal const string QI_CREATE_FORM = "INSERT INTO CustomForm (CustomFormId, FormTitle,IsHidden,Xml) VALUES (@customformid, @title,@ishidden,@xml)";

		// Token: 0x04000444 RID: 1092
		internal const string QD_DELETE_FORM = "UPDATE CustomForm SET ishidden=1 WHERE CustomFormId=@formid";

		// Token: 0x04000445 RID: 1093
		internal const string QU_UPDATE_FORM = "UPDATE CustomForm SET FormTitle=@title,IsHidden=@ishidden,xml=@xml WHERE CustomFormId=@formid";
	}
}
