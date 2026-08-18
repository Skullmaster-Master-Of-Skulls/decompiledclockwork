using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PageLayout;

namespace Telerik.Web.UI
{
	// Token: 0x0200063E RID: 1598
	[ParseChildren(true)]
	[ToolboxItem(false)]
	public class CompositeLayoutColumn : LayoutColumn
	{
		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000BEEF4 File Offset: 0x000BD0F4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PlaceHolder Content
		{
			get
			{
				if (this._content == null)
				{
					this._content = new PlaceHolder();
					this.Controls.Add(this._content);
				}
				return this._content;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000BEF20 File Offset: 0x000BD120
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LayoutRowCollection Rows
		{
			get
			{
				if (this._rows == null)
				{
					this._rows = new LayoutRowCollection(this);
					this._rows.SetOwner(base.Owner);
				}
				return this._rows;
			}
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06003A7D RID: 14973 RVA: 0x000BEF4D File Offset: 0x000BD14D
		// (set) Token: 0x06003A7E RID: 14974 RVA: 0x000BEF55 File Offset: 0x000BD155
		internal bool RenderRowWrapper { get; set; }

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000BEF5E File Offset: 0x000BD15E
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x000BEF66 File Offset: 0x000BD166
		internal TagName RowWrapperHtmlTag { get; set; }

		// Token: 0x06003A81 RID: 14977 RVA: 0x000BEF6F File Offset: 0x000BD16F
		protected internal override void SetOwner(RadPageLayout owner)
		{
			base.Owner = owner;
			this.Rows.SetOwner(owner);
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000BEF9C File Offset: 0x000BD19C
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			this.Content.RenderControl(writer);
			this.Rows.ForEach(delegate(Control row)
			{
				row.RenderControl(writer);
			});
		}

		// Token: 0x04000FA8 RID: 4008
		private PlaceHolder _content;

		// Token: 0x04000FA9 RID: 4009
		private LayoutRowCollection _rows;
	}
}
