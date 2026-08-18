using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI
{
	// Token: 0x02000A3A RID: 2618
	public class MonthYearView : Table
	{
		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x060063D3 RID: 25555 RVA: 0x00177613 File Offset: 0x00175813
		// (set) Token: 0x060063D4 RID: 25556 RVA: 0x0017761B File Offset: 0x0017581B
		public RadMonthYearPicker Owner { get; internal set; }

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x060063D5 RID: 25557 RVA: 0x00177624 File Offset: 0x00175824
		// (set) Token: 0x060063D6 RID: 25558 RVA: 0x0017764F File Offset: 0x0017584F
		[NotifyParentProperty(true)]
		[Category("Accessibility")]
		[DefaultValue("Table holding time picker for selecting time of day.")]
		[Description("Gets or sets the summary attribute for the RadTimeView.")]
		[Localizable(true)]
		public virtual string Summary
		{
			get
			{
				return (this.ViewState["Summary"] as string) ?? this.Owner.Localization.MonthYearViewSummary;
			}
			set
			{
				this.ViewState["Summary"] = value;
			}
		}

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x060063D7 RID: 25559 RVA: 0x00177662 File Offset: 0x00175862
		// (set) Token: 0x060063D8 RID: 25560 RVA: 0x0017768D File Offset: 0x0017588D
		[DefaultValue("Month year picker")]
		[Localizable(true)]
		[Category("Accessibility")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the caption for the RadTimeView")]
		public string CaptionText
		{
			get
			{
				return (this.ViewState["Caption"] as string) ?? this.Owner.Localization.MonthYearViewCaptionText;
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x060063D9 RID: 25561 RVA: 0x001776A0 File Offset: 0x001758A0
		// (set) Token: 0x060063DA RID: 25562 RVA: 0x001776CB File Offset: 0x001758CB
		internal bool RenderInvisible
		{
			get
			{
				bool result = false;
				object obj = this.ViewState["_myvRenderInvisible"];
				if (obj == null)
				{
					return result;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["_myvRenderInvisible"] = value;
			}
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x001776E3 File Offset: 0x001758E3
		public MonthYearView(RadMonthYearPicker owner)
		{
			this.Owner = owner;
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x001776F2 File Offset: 0x001758F2
		public virtual void Initialize()
		{
			AccessibilityHelper.AddAccessibilityRow(this, string.IsNullOrEmpty(this.Caption) ? "<span style='display: none'>empty</span>" : this.Caption);
			this.CreateMonthYearViewRows();
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x0017771C File Offset: 0x0017591C
		protected override void Render(HtmlTextWriter writer)
		{
			string text = string.Empty;
			if (this.RenderInvisible)
			{
				text = " style=\"display: none\" ";
			}
			writer.Write(string.Format(CultureInfo.InvariantCulture, "<div id='{0}' {1}>", new object[]
			{
				this.Owner.ClientID + "_wrapperElement",
				text
			}));
			if (!string.IsNullOrEmpty(this.Summary))
			{
				writer.AddAttribute("summary", this.Summary);
			}
			base.Render(writer);
			writer.Write("</div>");
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x001777A6 File Offset: 0x001759A6
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.CaptionText))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Caption);
				writer.Write(this.CaptionText);
				writer.RenderEndTag();
			}
			base.RenderContents(writer);
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x001777E4 File Offset: 0x001759E4
		internal void RecreateNavigationChildControls()
		{
			for (int i = 0; i < this.Rows.Count; i++)
			{
				for (int j = 0; j < this.Rows[i].Cells.Count; j++)
				{
					MonthYearViewCell monthYearViewCell = this.Rows[i].Cells[j] as MonthYearViewCell;
					if (monthYearViewCell != null && monthYearViewCell.CellType == MonthYearViewCellType.NavigationCell)
					{
						monthYearViewCell.Controls.Clear();
						monthYearViewCell.Initialize(new int?(j));
					}
				}
			}
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x00177868 File Offset: 0x00175A68
		private void CreateMonthYearViewRows()
		{
			string[] abbreviatedMonthNames = this.Owner.Culture.DateTimeFormat.AbbreviatedMonthNames;
			int num = abbreviatedMonthNames.Length - 1;
			int num2 = num / 2;
			MonthYearViewRow monthYearViewRow;
			for (int i = 0; i < num2; i++)
			{
				if (i == num2 - 1)
				{
					monthYearViewRow = new MonthYearViewRow(this.Owner, MonthYearViewRowType.NavigationRow);
					this.Controls.Add(monthYearViewRow);
					monthYearViewRow.Initialize(new int?(i * 2));
				}
				else
				{
					monthYearViewRow = new MonthYearViewRow(this.Owner, MonthYearViewRowType.BodyRow);
					this.Controls.Add(monthYearViewRow);
					monthYearViewRow.Initialize(new int?(i));
				}
			}
			monthYearViewRow = new MonthYearViewRow(this.Owner, MonthYearViewRowType.FooterRow);
			this.Controls.Add(monthYearViewRow);
			monthYearViewRow.Initialize(null);
		}
	}
}
