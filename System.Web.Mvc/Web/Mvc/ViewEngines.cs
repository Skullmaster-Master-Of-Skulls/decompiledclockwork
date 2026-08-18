using System;

namespace System.Web.Mvc
{
	// Token: 0x020001F0 RID: 496
	public static class ViewEngines
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00027D64 File Offset: 0x00025F64
		public static ViewEngineCollection Engines
		{
			get
			{
				return ViewEngines._engines;
			}
		}

		// Token: 0x040003F2 RID: 1010
		private static readonly ViewEngineCollection _engines = new ViewEngineCollection
		{
			new WebFormViewEngine(),
			new RazorViewEngine()
		};
	}
}
