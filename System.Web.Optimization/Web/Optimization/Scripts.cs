using System;

namespace System.Web.Optimization
{
	// Token: 0x0200002B RID: 43
	public static class Scripts
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005372 File Offset: 0x00003572
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005387 File Offset: 0x00003587
		internal static HttpContextBase Context
		{
			get
			{
				return Scripts._context ?? new HttpContextWrapper(HttpContext.Current);
			}
			set
			{
				Scripts._context = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000538F File Offset: 0x0000358F
		private static AssetManager Manager
		{
			get
			{
				return AssetManager.GetInstance(Scripts.Context);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000539B File Offset: 0x0000359B
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000053A2 File Offset: 0x000035A2
		public static string DefaultTagFormat
		{
			get
			{
				return Scripts._defaultTagFormat;
			}
			set
			{
				Scripts._defaultTagFormat = value;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000053AA File Offset: 0x000035AA
		public static IHtmlString Render(params string[] paths)
		{
			return Scripts.RenderFormat(Scripts.DefaultTagFormat, paths);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000053B8 File Offset: 0x000035B8
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
			return Scripts.Manager.RenderExplicit(tagFormat, paths);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005419 File Offset: 0x00003619
		public static IHtmlString Url(string virtualPath)
		{
			return Scripts.Manager.ResolveUrl(virtualPath);
		}

		// Token: 0x04000075 RID: 117
		private static HttpContextBase _context;

		// Token: 0x04000076 RID: 118
		private static string _defaultTagFormat = "<script src=\"{0}\"></script>";
	}
}
