using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000659 RID: 1625
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableHeaderCell : TableCell
	{
		// Token: 0x06004F91 RID: 20369 RVA: 0x0013FAF6 File Offset: 0x0013EAF6
		public TableHeaderCell() : base(HtmlTextWriterTag.Th)
		{
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06004F92 RID: 20370 RVA: 0x0013FB00 File Offset: 0x0013EB00
		// (set) Token: 0x06004F93 RID: 20371 RVA: 0x0013FB2D File Offset: 0x0013EB2D
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

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06004F94 RID: 20372 RVA: 0x0013FB40 File Offset: 0x0013EB40
		// (set) Token: 0x06004F95 RID: 20373 RVA: 0x0013FB69 File Offset: 0x0013EB69
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

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06004F96 RID: 20374 RVA: 0x0013FB84 File Offset: 0x0013EB84
		// (set) Token: 0x06004F97 RID: 20375 RVA: 0x0013FBBC File Offset: 0x0013EBBC
		[WebSysDescription("TableHeaderCell_CategoryText")]
		[TypeConverter(typeof(StringArrayConverter))]
		[DefaultValue(null)]
		[WebCategory("Accessibility")]
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

		// Token: 0x06004F98 RID: 20376 RVA: 0x0013FBF0 File Offset: 0x0013EBF0
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
			if (categoryText.Length > 0)
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
