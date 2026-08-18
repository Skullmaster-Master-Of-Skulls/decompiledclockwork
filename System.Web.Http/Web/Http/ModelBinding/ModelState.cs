using System;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	public class ModelState
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001AE30 File Offset: 0x00019030
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0001AE38 File Offset: 0x00019038
		public ValueProviderResult Value { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001AE41 File Offset: 0x00019041
		public ModelErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x0400026A RID: 618
		private ModelErrorCollection _errors = new ModelErrorCollection();
	}
}
