using System;

namespace System.Web.Optimization
{
	// Token: 0x0200002A RID: 42
	public static class Styles
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000052B1 File Offset: 0x000034B1
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000052C6 File Offset: 0x000034C6
		internal static HttpContextBase Context
		{
			get
			{
				return Styles._context ?? new HttpContextWrapper(HttpContext.Current);
			}
			set
			{
				Styles._context = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000052CE File Offset: 0x000034CE
		private static AssetManager Manager
		{
			get
			{
				return AssetManager.GetInstance(Styles.Context);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000052DA File Offset: 0x000034DA
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000052E1 File Offset: 0x000034E1
		public static string DefaultTagFormat
		{
			get
			{
				return Styles._defaultTagFormat;
			}
			set
			{
				Styles._defaultTagFormat = value;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000052E9 File Offset: 0x000034E9
		public static IHtmlString Render(params string[] paths)
		{
			return Styles.RenderFormat(Styles.DefaultTagFormat, paths);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000052F8 File Offset: 0x000034F8
		public static IHtmlString RenderFormat(string tagFormat, params string[] paths)
		{
			if (string.IsNullOrEmpty(tagFormat))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("tagFormat");
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			foreach (string value in paths)
			{
				if (string.IsNullOrEmpty(value))
				{
					throw ExceptionUtil.ParameterNullOrEmpty("paths");
				}
			}
			return Styles.Manager.RenderExplicit(tagFormat, paths);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005359 File Offset: 0x00003559
		public static IHtmlString Url(string virtualPath)
		{
			return Styles.Manager.ResolveUrl(virtualPath);
		}

		// Token: 0x04000073 RID: 115
		private static HttpContextBase _context;

		// Token: 0x04000074 RID: 116
		private static string _defaultTagFormat = "<link href=\"{0}\" rel=\"stylesheet\"/>";
	}
}
