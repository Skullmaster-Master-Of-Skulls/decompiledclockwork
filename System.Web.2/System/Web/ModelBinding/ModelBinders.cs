using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000657 RID: 1623
	public static class ModelBinders
	{
		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06004FBE RID: 20414 RVA: 0x00114A18 File Offset: 0x00112C18
		public static ModelBinderDictionary Binders
		{
			get
			{
				return ModelBinders._binders;
			}
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00114A20 File Offset: 0x00112C20
		private static ModelBinderDictionary CreateDefaultBinderDictionary()
		{
			return new ModelBinderDictionary();
		}

		// Token: 0x04002A91 RID: 10897
		private static readonly ModelBinderDictionary _binders = ModelBinders.CreateDefaultBinderDictionary();
	}
}
