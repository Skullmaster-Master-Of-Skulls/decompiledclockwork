using System;

namespace System.Web.Mvc
{
	// Token: 0x020000CB RID: 203
	public static class ModelBinderProviders
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000ED16 File Offset: 0x0000CF16
		public static ModelBinderProviderCollection BinderProviders
		{
			get
			{
				return ModelBinderProviders._binderProviders;
			}
		}

		// Token: 0x04000173 RID: 371
		private static readonly ModelBinderProviderCollection _binderProviders = new ModelBinderProviderCollection();
	}
}
