using System;
using System.Collections.ObjectModel;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	public class ModelErrorCollection : Collection<ModelError>
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x0001AE0C File Offset: 0x0001900C
		public void Add(Exception exception)
		{
			base.Add(new ModelError(exception));
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001AE1A File Offset: 0x0001901A
		public void Add(string errorMessage)
		{
			base.Add(new ModelError(errorMessage));
		}
	}
}
