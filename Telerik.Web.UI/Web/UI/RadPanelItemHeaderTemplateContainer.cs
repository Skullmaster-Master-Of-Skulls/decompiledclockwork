using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F86 RID: 3974
	[ToolboxItem(false)]
	public class RadPanelItemHeaderTemplateContainer : WebControl, IDataItemContainer, INamingContainer
	{
		// Token: 0x1700302E RID: 12334
		// (get) Token: 0x06009853 RID: 38995 RVA: 0x0022112D File Offset: 0x0021F32D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06009854 RID: 38996 RVA: 0x00221131 File Offset: 0x0021F331
		public RadPanelItemHeaderTemplateContainer(RadPanelItem owner)
		{
			this._owner = owner;
			this.ID = "HeaderTemplate";
		}

		// Token: 0x06009855 RID: 38997 RVA: 0x0022114B File Offset: 0x0021F34B
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.Controls.Count > 0)
			{
				base.Render(writer);
			}
		}

		// Token: 0x06009856 RID: 38998 RVA: 0x00221162 File Offset: 0x0021F362
		protected virtual object GetDataItem()
		{
			return this._owner;
		}

		// Token: 0x1700302F RID: 12335
		// (get) Token: 0x06009857 RID: 38999 RVA: 0x0022116A File Offset: 0x0021F36A
		public object DataItem
		{
			get
			{
				return this.GetDataItem();
			}
		}

		// Token: 0x17003030 RID: 12336
		// (get) Token: 0x06009858 RID: 39000 RVA: 0x00221172 File Offset: 0x0021F372
		public int DataItemIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17003031 RID: 12337
		// (get) Token: 0x06009859 RID: 39001 RVA: 0x00221175 File Offset: 0x0021F375
		public int DisplayIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04002B7F RID: 11135
		private const string HeaderIDString = "HeaderTemplate";

		// Token: 0x04002B80 RID: 11136
		private RadPanelItem _owner;
	}
}
