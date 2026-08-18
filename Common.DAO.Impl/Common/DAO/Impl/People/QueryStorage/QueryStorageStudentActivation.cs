using System;

namespace TechnoPro.Common.DAO.Impl.People.QueryStorage
{
	// Token: 0x0200007D RID: 125
	public class QueryStorageStudentActivation
	{
		// Token: 0x04000164 RID: 356
		internal const string QU_MERGE_ACTIVATIONS = "UPDATE peopledatesadded SET personid=@pidnew WHERE personid=@pidold; UPDATE peoplepreviousyears SET personid=@pidnew WHERE personid=@pidold";
	}
}
