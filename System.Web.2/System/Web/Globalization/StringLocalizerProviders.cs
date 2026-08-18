using System;

namespace System.Web.Globalization
{
	// Token: 0x02000694 RID: 1684
	public static class StringLocalizerProviders
	{
		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x001178E9 File Offset: 0x00115AE9
		// (set) Token: 0x0600512F RID: 20783 RVA: 0x00117908 File Offset: 0x00115B08
		public static IStringLocalizerProvider DataAnnotationStringLocalizerProvider
		{
			get
			{
				if (StringLocalizerProviders._dataAnnotationStringLocalizerProvider == null && !StringLocalizerProviders._setStringLocalizerProvider)
				{
					StringLocalizerProviders._dataAnnotationStringLocalizerProvider = new ResourceFileStringLocalizerProvider();
				}
				return StringLocalizerProviders._dataAnnotationStringLocalizerProvider;
			}
			set
			{
				StringLocalizerProviders._dataAnnotationStringLocalizerProvider = value;
				StringLocalizerProviders._setStringLocalizerProvider = true;
			}
		}

		// Token: 0x04002AE5 RID: 10981
		private static IStringLocalizerProvider _dataAnnotationStringLocalizerProvider;

		// Token: 0x04002AE6 RID: 10982
		private static bool _setStringLocalizerProvider;
	}
}
