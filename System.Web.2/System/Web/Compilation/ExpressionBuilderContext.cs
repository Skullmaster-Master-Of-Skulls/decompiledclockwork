using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200083B RID: 2107
	public sealed class ExpressionBuilderContext
	{
		// Token: 0x06006489 RID: 25737 RVA: 0x001607FC File Offset: 0x0015E9FC
		internal ExpressionBuilderContext(VirtualPath virtualPath)
		{
			this._virtualPath = virtualPath;
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x0016080B File Offset: 0x0015EA0B
		public ExpressionBuilderContext(string virtualPath)
		{
			this._virtualPath = System.Web.VirtualPath.Create(virtualPath);
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x0016081F File Offset: 0x0015EA1F
		public ExpressionBuilderContext(TemplateControl templateControl)
		{
			this._templateControl = templateControl;
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x0600648C RID: 25740 RVA: 0x0016082E File Offset: 0x0015EA2E
		public TemplateControl TemplateControl
		{
			get
			{
				return this._templateControl;
			}
		}

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x0600648D RID: 25741 RVA: 0x00160836 File Offset: 0x0015EA36
		public string VirtualPath
		{
			get
			{
				if (this._virtualPath == null && this._templateControl != null)
				{
					return this._templateControl.AppRelativeVirtualPath;
				}
				return System.Web.VirtualPath.GetVirtualPathString(this._virtualPath);
			}
		}

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x0600648E RID: 25742 RVA: 0x00160865 File Offset: 0x0015EA65
		internal VirtualPath VirtualPathObject
		{
			get
			{
				if (this._virtualPath == null && this._templateControl != null)
				{
					return this._templateControl.VirtualPath;
				}
				return this._virtualPath;
			}
		}

		// Token: 0x040033E4 RID: 13284
		private TemplateControl _templateControl;

		// Token: 0x040033E5 RID: 13285
		private VirtualPath _virtualPath;
	}
}
