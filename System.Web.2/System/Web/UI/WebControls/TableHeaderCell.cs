using System;
using System.ComponentModel;
using System.Text;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E8 RID: 1256
	public class TableHeaderCell : TableCell
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x000C9D0A File Offset: 0x000C7F0A
		public TableHeaderCell() : base(HtmlTextWriterTag.Th)
		{
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06003EB7 RID: 16055 RVA: 0x000C9D14 File Offset: 0x000C7F14
		// (set) Token: 0x06003EB8 RID: 16056 RVA: 0x00095FE1 File Offset: 0x000941E1
		[WebCategory("Accessibility")]
		[DefaultValue("")]
		[WebSysDescription("TableHeaderCell_AbbreviatedText")]
		public virtual string AbbreviatedText
		{
			get
			{
				object obj = this.ViewState["AbbrText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["AbbrText"] = value;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x000C9D44 File Offset: 0x000C7F44
		// (set) Token: 0x06003EBA RID: 16058 RVA: 0x0009601D File Offset: 0x0009421D
		[WebCategory("Accessibility")]
		[DefaultValue(TableHeaderScope.NotSet)]
		[WebSysDescription("TableHeaderCell_Scope")]
		public virtual TableHeaderScope Scope
		{
			get
			{
				object obj = this.ViewState["Scope"];
				if (obj != null)
				{
					return (TableHeaderScope)obj;
				}
				return TableHeaderScope.NotSet;
			}
			set
			{
				this.ViewState["Scope"] = value;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x000C9D70 File Offset: 0x000C7F70
		// (set) Token: 0x06003EBC RID: 16060 RVA: 0x000C9DA8 File Offset: 0x000C7FA8
		[DefaultValue(null)]
		[TypeConverter(typeof(StringArrayConverter))]
		[WebCategory("Accessibility")]
		[WebSysDescription("TableHeaderCell_CategoryText")]
		public virtual string[] CategoryText
		{
			get
			{
				object obj = this.ViewState["CategoryText"];
				if (obj == null)
				{
					return new string[0];
				}
				return (string[])((string[])obj).Clone();
			}
			set
			{
				if (value != null)
				{
					this.ViewState["CategoryText"] = (string[])value.Clone();
					return;
				}
				this.ViewState["CategoryText"] = null;
			}
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000C9DDC File Offset: 0x000C7FDC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			TableHeaderScope scope = this.Scope;
			if (scope != TableHeaderScope.NotSet)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Scope, scope.ToString().ToLowerInvariant());
			}
			string abbreviatedText = this.AbbreviatedText;
			if (!string.IsNullOrEmpty(abbreviatedText))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Abbr, abbreviatedText);
			}
			string[] categoryText = this.CategoryText;
			if (categoryText.Length != 0)
			{
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in categoryText)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(value);
				}
				string value2 = stringBuilder.ToString();
				if (!string.IsNullOrEmpty(value2))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Axis, value2);
				}
			}
		}
	}
}
