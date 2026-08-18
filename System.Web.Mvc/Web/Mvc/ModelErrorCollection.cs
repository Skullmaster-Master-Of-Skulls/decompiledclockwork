using System;
using System.Collections.ObjectModel;

namespace System.Web.Mvc
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	public class ModelErrorCollection : Collection<ModelError>
	{
		// Token: 0x06000D60 RID: 3424 RVA: 0x0002367A File Offset: 0x0002187A
		public void Add(Exception exception)
		{
			base.Add(new ModelError(exception));
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00023688 File Offset: 0x00021888
		public void Add(string errorMessage)
		{
			base.Add(new ModelError(errorMessage));
		}
	}
}
