using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B6 RID: 950
	public class DataControlFieldHeaderCell : DataControlFieldCell
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x00095FA7 File Offset: 0x000941A7
		public DataControlFieldHeaderCell(DataControlField containingField) : base(HtmlTextWriterTag.Th, containingField)
		{
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x00095FB4 File Offset: 0x000941B4
		// (set) Token: 0x06002DED RID: 11757 RVA: 0x00095FE1 File Offset: 0x000941E1
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

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x00095FF4 File Offset: 0x000941F4
		// (set) Token: 0x06002DEF RID: 11759 RVA: 0x0009601D File Offset: 0x0009421D
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

		// Token: 0x06002DF0 RID: 11760 RVA: 0x00096038 File Offset: 0x00094238
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			TableHeaderScope scope = this.Scope;
			if (scope != TableHeaderScope.NotSet)
			{
				if (scope == TableHeaderScope.Column)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Scope, "row");
				}
			}
			string abbreviatedText = this.AbbreviatedText;
			if (!string.IsNullOrEmpty(abbreviatedText))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Abbr, abbreviatedText);
			}
		}
	}
}
