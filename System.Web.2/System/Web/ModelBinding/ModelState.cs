using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200065D RID: 1629
	[Serializable]
	public class ModelState
	{
		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06005007 RID: 20487 RVA: 0x00114F13 File Offset: 0x00113113
		// (set) Token: 0x06005008 RID: 20488 RVA: 0x00114F1B File Offset: 0x0011311B
		public ValueProviderResult Value { get; set; }

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06005009 RID: 20489 RVA: 0x00114F24 File Offset: 0x00113124
		public ModelErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x04002AB1 RID: 10929
		private ModelErrorCollection _errors = new ModelErrorCollection();
	}
}
