using System;
using System.IO;

namespace System.Web.WebPages
{
	// Token: 0x02000036 RID: 54
	public class DefaultDisplayMode : IDisplayMode
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00005357 File Offset: 0x00003557
		public DefaultDisplayMode() : this(DisplayModeProvider.DefaultDisplayModeId)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005364 File Offset: 0x00003564
		public DefaultDisplayMode(string suffix)
		{
			this._suffix = (suffix ?? string.Empty);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000537C File Offset: 0x0000357C
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00005384 File Offset: 0x00003584
		public Func<HttpContextBase, bool> ContextCondition { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000538D File Offset: 0x0000358D
		public virtual string DisplayModeId
		{
			get
			{
				return this._suffix;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005395 File Offset: 0x00003595
		public bool CanHandleContext(HttpContextBase httpContext)
		{
			return this.ContextCondition == null || this.ContextCondition(httpContext);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000053B0 File Offset: 0x000035B0
		public virtual DisplayInfo GetDisplayInfo(HttpContextBase httpContext, string virtualPath, Func<string, bool> virtualPathExists)
		{
			string text = this.TransformPath(virtualPath, this._suffix);
			if (text != null && virtualPathExists(text))
			{
				return new DisplayInfo(text, this);
			}
			return null;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000053E0 File Offset: 0x000035E0
		protected virtual string TransformPath(string virtualPath, string suffix)
		{
			if (string.IsNullOrEmpty(suffix))
			{
				return virtualPath;
			}
			string extension = Path.GetExtension(virtualPath);
			return Path.ChangeExtension(virtualPath, suffix + extension);
		}

		// Token: 0x04000078 RID: 120
		private readonly string _suffix;
	}
}
