using System;

namespace System.Web.Mvc
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class ModelState
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002364E File Offset: 0x0002184E
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00023656 File Offset: 0x00021856
		public ValueProviderResult Value { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002365F File Offset: 0x0002185F
		public ModelErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x0400036B RID: 875
		private ModelErrorCollection _errors = new ModelErrorCollection();
	}
}
