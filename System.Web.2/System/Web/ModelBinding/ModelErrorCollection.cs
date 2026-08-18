using System;
using System.Collections.ObjectModel;

namespace System.Web.ModelBinding
{
	// Token: 0x02000659 RID: 1625
	[Serializable]
	public class ModelErrorCollection : Collection<ModelError>
	{
		// Token: 0x06004FC8 RID: 20424 RVA: 0x00114AA2 File Offset: 0x00112CA2
		public void Add(Exception exception)
		{
			base.Add(new ModelError(exception));
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x00114AB0 File Offset: 0x00112CB0
		public void Add(string errorMessage)
		{
			base.Add(new ModelError(errorMessage));
		}
	}
}
