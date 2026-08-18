using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PageLayout;
using Telerik.Web.UI.PageLayout.Enums;
using Telerik.Web.UI.PageLayout.Utils;

namespace Telerik.Web.UI
{
	// Token: 0x02000646 RID: 1606
	[ParseChildren(true)]
	[ToolboxItem(false)]
	public class LayoutRow : BaseContainer
	{
		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000BF4A7 File Offset: 0x000BD6A7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
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

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x000BF4D3 File Offset: 0x000BD6D3
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LayoutColumnCollection Columns
		{
			get
			{
				if (this._columns == null)
				{
					this._columns = new LayoutColumnCollection(this);
				}
				return this._columns;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000BF4EF File Offset: 0x000BD6EF
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LayoutRowCollection Rows
		{
			get
			{
				if (this._rows == null)
				{
					this._rows = new LayoutRowCollection(this);
				}
				return this._rows;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x000BF50B File Offset: 0x000BD70B
		// (set) Token: 0x06003AB4 RID: 15028 RVA: 0x000BF513 File Offset: 0x000BD713
		public string WrapperCssClass { get; set; }

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x000BF51C File Offset: 0x000BD71C
		// (set) Token: 0x06003AB6 RID: 15030 RVA: 0x000BF524 File Offset: 0x000BD724
		public TagName WrapperHtmlTag { get; set; }

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x000BF52D File Offset: 0x000BD72D
		// (set) Token: 0x06003AB8 RID: 15032 RVA: 0x000BF535 File Offset: 0x000BD735
		public RowType RowType { get; set; }

		// Token: 0x06003AB9 RID: 15033 RVA: 0x000BF53E File Offset: 0x000BD73E
		public LayoutRow()
		{
			this.RowType = RowType.Row;
			this.WrapperHtmlTag = Telerik.Web.UI.PageLayout.TagName.None;
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x000BF555 File Offset: 0x000BD755
		protected internal override void SetOwner(RadPageLayout owner)
		{
			base.SetOwner(owner);
			this.Columns.SetOwner(owner);
			this.Rows.SetOwner(owner);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x000BF578 File Offset: 0x000BD778
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			List<string> list = new List<string>();
			switch (this.RowType)
			{
			case RowType.Region:
				list.Add("t-region");
				break;
			case RowType.Row:
				list.Add("t-row");
				break;
			case RowType.Container:
				list.AddRange(base.Owner.GetAllCssClasses(base.Owner.CssClass).Split(null, StringSplitOptions.RemoveEmptyEntries));
				break;
			}
			list.AddRange(base.GetTransformationToggleClassNames());
			list.AddRange(cssClass.Split(null, StringSplitOptions.RemoveEmptyEntries));
			this.CssClass = CssUtils.NormalizeClassNames(list);
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x000BF644 File Offset: 0x000BD844
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			this.Content.RenderControl(writer);
			this.Columns.ForEach(delegate(Control column)
			{
				column.RenderControl(writer);
			});
			this.Rows.ForEach(delegate(Control row)
			{
				row.RenderControl(writer);
			});
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x000BF6A0 File Offset: 0x000BD8A0
		public override void RenderControl(HtmlTextWriter writer)
		{
			if (this.Visible && this.WrapperHtmlTag != Telerik.Web.UI.PageLayout.TagName.None)
			{
				string value = string.Format("{0} {1}", "t-row-wrap", this.WrapperCssClass).Trim();
				writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
				writer.RenderBeginTag(this.WrapperHtmlTag.ToString().ToLower());
				base.RenderControl(writer);
				writer.RenderEndTag();
				return;
			}
			base.RenderControl(writer);
		}

		// Token: 0x04000FF8 RID: 4088
		private PlaceHolder _content;

		// Token: 0x04000FF9 RID: 4089
		private LayoutColumnCollection _columns;

		// Token: 0x04000FFA RID: 4090
		private LayoutRowCollection _rows;
	}
}
