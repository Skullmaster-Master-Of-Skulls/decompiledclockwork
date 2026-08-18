using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI.Calendar.Utils
{
	// Token: 0x02000A38 RID: 2616
	internal class MonthYearViewCellHelper
	{
		// Token: 0x060063C8 RID: 25544 RVA: 0x00177078 File Offset: 0x00175278
		internal static void CreateChildControls(MonthYearViewCell ownerViewCell, MonthYearViewCellType cellType, int? index, RadMonthYearPicker ownerMonthYearPicker)
		{
			HyperLink hyperLink = new HyperLink();
			hyperLink.NavigateUrl = "#";
			switch (cellType)
			{
			case MonthYearViewCellType.YearCell:
				MonthYearViewCellHelper.PopulateYearCell(ownerMonthYearPicker, hyperLink, index, ownerViewCell);
				return;
			case MonthYearViewCellType.MonthCell:
				MonthYearViewCellHelper.PopulateMonthCell(ownerMonthYearPicker, hyperLink, index, ownerViewCell);
				return;
			case MonthYearViewCellType.NavigationCell:
				MonthYearViewCellHelper.PopulateNavigationCell(ownerMonthYearPicker, hyperLink, index, ownerViewCell);
				return;
			case MonthYearViewCellType.ButtonCell:
				for (int i = 0; i <= 2; i++)
				{
					MonthYearViewCellHelper.PopulateButtonCell(ownerMonthYearPicker, new int?(i), ownerViewCell);
				}
				ownerViewCell.ColumnSpan = 4;
				ownerViewCell.CssClass = "rcButtons";
				return;
			default:
				return;
			}
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x001770F8 File Offset: 0x001752F8
		private static void PopulateButtonCell(RadMonthYearPicker ownerMonthYearPicker, int? index, MonthYearViewCell ownerViewCell)
		{
			WebControl webControl;
			if (ownerMonthYearPicker.ResolvedRenderMode == RenderMode.Classic)
			{
				webControl = new Button();
			}
			else
			{
				webControl = new LinkButton();
			}
			string text = string.Empty;
			int valueOrDefault = index.GetValueOrDefault();
			if (index != null)
			{
				switch (valueOrDefault)
				{
				case 0:
					webControl.ID = ownerMonthYearPicker.ID + "_TodayButton";
					text = ownerMonthYearPicker.MonthYearNavigationSettings.TodayButtonCaption;
					webControl.CssClass = "rcTodayButton";
					break;
				case 1:
					webControl.ID = ownerMonthYearPicker.ID + "_OkButton";
					text = ownerMonthYearPicker.MonthYearNavigationSettings.OkButtonCaption;
					webControl.CssClass = "rcOkButton";
					break;
				case 2:
					webControl.ID = ownerMonthYearPicker.ID + "_CancelButton";
					text = ownerMonthYearPicker.MonthYearNavigationSettings.CancelButtonCaption;
					webControl.CssClass = "rcCancelButton";
					break;
				}
			}
			if (ownerMonthYearPicker.ResolvedRenderMode == RenderMode.Classic)
			{
				Button button = webControl as Button;
				button.Text = text;
				button.UseSubmitBehavior = false;
			}
			else
			{
				LinkButton linkButton = webControl as LinkButton;
				linkButton.Text = text;
			}
			ownerViewCell.Controls.Add(webControl);
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x00177214 File Offset: 0x00175414
		private static void PopulateYearCell(RadMonthYearPicker ownerMonthYearPicker, HyperLink link, int? index, MonthYearViewCell ownerViewCell)
		{
			int year = DateTime.Now.Year;
			if (ownerMonthYearPicker.SelectedDate == null)
			{
				year = ownerMonthYearPicker.FocusedDate.Year;
			}
			else
			{
				year = ownerMonthYearPicker.SelectedDate.Value.Year;
			}
			string text = (year + index - 4).ToString();
			link.Text = text;
			ownerViewCell.Controls.Add(link);
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x001772DC File Offset: 0x001754DC
		private static void PopulateMonthCell(RadMonthYearPicker ownerMonthYearPicker, HyperLink link, int? index, MonthYearViewCell ownerViewCell)
		{
			string[] abbreviatedMonthNames = ownerMonthYearPicker.Culture.DateTimeFormat.AbbreviatedMonthNames;
			string text = abbreviatedMonthNames[index.Value];
			link.Text = text;
			ownerViewCell.Controls.Add(link);
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x00177318 File Offset: 0x00175518
		private static void PopulateNavigationCell(RadMonthYearPicker ownerMonthYearPicker, HyperLink link, int? index, MonthYearViewCell ownerViewCell)
		{
			if (index % 2 == 0)
			{
				link.ID = ownerMonthYearPicker.ID + "_NavigationPrevLink";
				string id = ownerMonthYearPicker.ID + "_NavigationPrevImg";
				MonthYearViewCellHelper.SetNavigationCellProperties(ownerMonthYearPicker.MonthYearNavigationSettings.NavigationPrevToolTip, ownerMonthYearPicker.MonthYearNavigationSettings.NavigationPrevImage, ownerMonthYearPicker.MonthYearNavigationSettings.NavigationPrevText, link, id);
			}
			else
			{
				link.ID = ownerMonthYearPicker.ID + "_NavigationNextLink";
				string id2 = ownerMonthYearPicker.ID + "_NavigationNextImg";
				MonthYearViewCellHelper.SetNavigationCellProperties(ownerMonthYearPicker.MonthYearNavigationSettings.NavigationNextToolTip, ownerMonthYearPicker.MonthYearNavigationSettings.NavigationNextImage, ownerMonthYearPicker.MonthYearNavigationSettings.NavigationNextText, link, id2);
			}
			ownerViewCell.Controls.Add(link);
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x0017740C File Offset: 0x0017560C
		private static void SetNavigationCellProperties(string toolTip, string imagePath, string text, HyperLink link, string id)
		{
			if (!string.IsNullOrEmpty(toolTip))
			{
				link.ToolTip = toolTip;
			}
			if (!string.IsNullOrEmpty(imagePath))
			{
				Image image = new Image();
				image.ImageUrl = imagePath;
				image.ID = id;
				link.Controls.Add(image);
				return;
			}
			link.Text = text;
		}
	}
}
