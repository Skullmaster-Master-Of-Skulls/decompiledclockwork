using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C10 RID: 3088
	[ToolboxItem(false)]
	public class OrgChartNodeCollectionRenderer : WebControl
	{
		// Token: 0x1700264A RID: 9802
		// (get) Token: 0x060075B9 RID: 30137 RVA: 0x001B63F4 File Offset: 0x001B45F4
		// (set) Token: 0x060075BA RID: 30138 RVA: 0x001B63FC File Offset: 0x001B45FC
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
				this._isRootNodeCollection = (this._level == 0);
			}
		}

		// Token: 0x060075BB RID: 30139 RVA: 0x001B6414 File Offset: 0x001B4614
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.GetCssClass());
			base.RenderBeginTag(writer);
		}

		// Token: 0x1700264B RID: 9803
		// (get) Token: 0x060075BC RID: 30140 RVA: 0x001B642B File Offset: 0x001B462B
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Ul;
			}
		}

		// Token: 0x060075BD RID: 30141 RVA: 0x001B6430 File Offset: 0x001B4630
		private string GetCssClass()
		{
			string arg = this._isRootNodeCollection ? "rocRootNodeList" : ("rocLevel" + this._level);
			return string.Format("{0} {1}", "rocNodeList", arg).Trim();
		}

		// Token: 0x04002046 RID: 8262
		private bool _isRootNodeCollection;

		// Token: 0x04002047 RID: 8263
		private int _level;
	}
}
