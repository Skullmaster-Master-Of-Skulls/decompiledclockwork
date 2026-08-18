using System;
using Telerik.Web.Apoc.Extensions;
using Telerik.Web.Apoc.Fo;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015E7 RID: 5607
	internal class ExtensionArea : Area
	{
		// Token: 0x0600DA79 RID: 55929 RVA: 0x002FDA07 File Offset: 0x002FBC07
		public ExtensionArea(ExtensionObj obj) : base(null)
		{
			this._extensionObj = obj;
		}

		// Token: 0x0600DA7A RID: 55930 RVA: 0x002FDA17 File Offset: 0x002FBC17
		public FObj getExtensionObj()
		{
			return this._extensionObj;
		}

		// Token: 0x0600DA7B RID: 55931 RVA: 0x002FDA1F File Offset: 0x002FBC1F
		public override void render(IRenderer renderer)
		{
		}

		// Token: 0x04003CCF RID: 15567
		private ExtensionObj _extensionObj;
	}
}
