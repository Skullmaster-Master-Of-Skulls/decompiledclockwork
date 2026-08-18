using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetFilterMenu
{
	// Token: 0x020008B6 RID: 2230
	internal abstract class ViewBase : IFilterMenuView
	{
		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x00100D61 File Offset: 0x000FEF61
		// (set) Token: 0x060052C2 RID: 21186 RVA: 0x00100D69 File Offset: 0x000FEF69
		public FilterMenuTemplate Owner
		{
			get
			{
				return this._owner;
			}
			protected set
			{
				this._owner = value;
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x00100D72 File Offset: 0x000FEF72
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.Owner.Owner.Localization;
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x060052C4 RID: 21188 RVA: 0x00100D84 File Offset: 0x000FEF84
		// (set) Token: 0x060052C5 RID: 21189 RVA: 0x00100D8C File Offset: 0x000FEF8C
		public WebControl ApplyButton { get; set; }

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x060052C6 RID: 21190 RVA: 0x00100D95 File Offset: 0x000FEF95
		// (set) Token: 0x060052C7 RID: 21191 RVA: 0x00100D9D File Offset: 0x000FEF9D
		public WebControl ClearButton { get; set; }

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x060052C8 RID: 21192 RVA: 0x00100DA6 File Offset: 0x000FEFA6
		// (set) Token: 0x060052C9 RID: 21193 RVA: 0x00100DAE File Offset: 0x000FEFAE
		public WebControl SortAscButton { get; set; }

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x060052CA RID: 21194 RVA: 0x00100DB7 File Offset: 0x000FEFB7
		// (set) Token: 0x060052CB RID: 21195 RVA: 0x00100DBF File Offset: 0x000FEFBF
		public WebControl SortDescButton { get; set; }

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x00100DC8 File Offset: 0x000FEFC8
		// (set) Token: 0x060052CD RID: 21197 RVA: 0x00100DD0 File Offset: 0x000FEFD0
		public WebControl ConditionDropDownList { get; set; }

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x060052CE RID: 21198 RVA: 0x00100DD9 File Offset: 0x000FEFD9
		// (set) Token: 0x060052CF RID: 21199 RVA: 0x00100DE1 File Offset: 0x000FEFE1
		public WebControl ConditionTextBox { get; set; }

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x060052D0 RID: 21200 RVA: 0x00100DEA File Offset: 0x000FEFEA
		// (set) Token: 0x060052D1 RID: 21201 RVA: 0x00100DF2 File Offset: 0x000FEFF2
		public WebControl ConditionNumericTextBox { get; set; }

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x00100DFB File Offset: 0x000FEFFB
		// (set) Token: 0x060052D3 RID: 21203 RVA: 0x00100E03 File Offset: 0x000FF003
		public WebControl ConditionDatePicker { get; set; }

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x060052D4 RID: 21204 RVA: 0x00100E0C File Offset: 0x000FF00C
		// (set) Token: 0x060052D5 RID: 21205 RVA: 0x00100E14 File Offset: 0x000FF014
		public WebControl ValueSearchBox { get; set; }

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x060052D6 RID: 21206 RVA: 0x00100E1D File Offset: 0x000FF01D
		// (set) Token: 0x060052D7 RID: 21207 RVA: 0x00100E25 File Offset: 0x000FF025
		public WebControl ValueListBox { get; set; }

		// Token: 0x060052D8 RID: 21208 RVA: 0x00100E2E File Offset: 0x000FF02E
		public ViewBase(FilterMenuTemplate owner)
		{
			this.Owner = owner;
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x00100E3D File Offset: 0x000FF03D
		public void CreateControls()
		{
			this.CreateSortButtons();
			this.CreatePanelBarControls();
			this.CreateCommandButtons();
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x00100E51 File Offset: 0x000FF051
		private void CreatePanelBarControls()
		{
			this.CreateFilterByConditionControls();
			this.CreateFilterByValueControls();
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x00100E60 File Offset: 0x000FF060
		private void CreateFilterByConditionControls()
		{
			this.ConditionDropDownList = this.CreateDropDownList("ConditionDropDownList");
			this.PopulateConditionDropDownList();
			this.ConditionTextBox = this.CreateTextBox("ConditionTextBox");
			this.ConditionNumericTextBox = this.CreateNumericTextBox("ConditionNumericTextBox");
			this.ConditionDatePicker = this.CreateDatePicker("ConditionDatePicker");
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x00100EB7 File Offset: 0x000FF0B7
		private void CreateFilterByValueControls()
		{
			this.ValueSearchBox = this.CreateSearchBox("ValueSearchBox");
			this.ValueListBox = this.CreateListBox("ValueListBox");
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00100EDC File Offset: 0x000FF0DC
		private void CreateSortButtons()
		{
			this.SortAscButton = this.CreateButton(this.Localization.FilterMenuSortAscending, "rssSortAsc");
			this.SortAscButton.Attributes.Add("data-command", "asc");
			this.SortDescButton = this.CreateButton(this.Localization.FilterMenuSortDescending, "rssSortDesc");
			this.SortDescButton.Attributes.Add("data-command", "desc");
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00100F55 File Offset: 0x000FF155
		protected void CreateCommandButtons()
		{
			this.CreateApplyButton();
			this.CreateClearButton();
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x00100F63 File Offset: 0x000FF163
		protected void CreateApplyButton()
		{
			this.ApplyButton = this.CreateCommandButton(this.Localization.FilterMenuApply, "rssPrimary");
			this.ApplyButton.Attributes.Add("data-command", "apply");
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x00100F9B File Offset: 0x000FF19B
		protected void CreateClearButton()
		{
			this.ClearButton = this.CreateCommandButton(this.Localization.FilterMenuClear, "");
			this.ClearButton.Attributes.Add("data-command", "clear");
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x00100FD4 File Offset: 0x000FF1D4
		private WebControl CreateCommandButton(string text, string cssClass = "")
		{
			return new WebControl(HtmlTextWriterTag.Button)
			{
				Controls = 
				{
					new LiteralControl(text)
				},
				CssClass = string.Format("{0} {1}", "rssButton", cssClass).Trim()
			};
		}

		// Token: 0x060052E2 RID: 21218 RVA: 0x00101018 File Offset: 0x000FF218
		private WebControl CreateButton(string text, string cssClass)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rssLink"
			};
			string cssClass2 = string.Format("{0} {1}", "rssIcon", cssClass).Trim();
			WebControl child = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = cssClass2
			};
			webControl.Controls.Add(child);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rssText"
			};
			webControl2.Controls.Add(new LiteralControl(text));
			webControl.Controls.Add(webControl2);
			return webControl;
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x001010AC File Offset: 0x000FF2AC
		private WebControl CreateDropDownList(string id)
		{
			return new RadDropDownList
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false
			};
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x00101104 File Offset: 0x000FF304
		private void PopulateConditionDropDownList()
		{
			string[,] array = new string[15, 3];
			array[0, 0] = this.Localization.FilterMenuNone;
			array[0, 1] = "";
			array[0, 2] = "";
			array[1, 0] = this.Localization.FilterMenuTextContains;
			array[1, 1] = "contains";
			array[1, 2] = "string";
			array[2, 0] = this.Localization.FilterMenuTextDoesNotContain;
			array[2, 1] = "doesnotcontain";
			array[2, 2] = "string";
			array[3, 0] = this.Localization.FilterMenuTextStartsWith;
			array[3, 1] = "startswith";
			array[3, 2] = "string";
			array[4, 0] = this.Localization.FilterMenuTextEndsWith;
			array[4, 1] = "endswith";
			array[4, 2] = "string";
			array[5, 0] = this.Localization.FilterMenuDateIs;
			array[5, 1] = "eq";
			array[5, 2] = "date";
			array[6, 0] = this.Localization.FilterMenuDateIsNot;
			array[6, 1] = "neq";
			array[6, 2] = "date";
			array[7, 0] = this.Localization.FilterMenuDateIsBefore;
			array[7, 1] = "lt";
			array[7, 2] = "date";
			array[8, 0] = this.Localization.FilterMenuDateIsAfter;
			array[8, 1] = "gt";
			array[8, 2] = "date";
			array[9, 0] = this.Localization.FilterMenuIsEqualTo;
			array[9, 1] = "eq";
			array[9, 2] = "number";
			array[10, 0] = this.Localization.FilterMenuIsNotEqualTo;
			array[10, 1] = "neq";
			array[10, 2] = "number";
			array[11, 0] = this.Localization.FilterMenuIsGreaterThanOrEqualTo;
			array[11, 1] = "gte";
			array[11, 2] = "number";
			array[12, 0] = this.Localization.FilterMenuIsGreaterThan;
			array[12, 1] = "gt";
			array[12, 2] = "number";
			array[13, 0] = this.Localization.FilterMenuIsLessThanOrEqualTo;
			array[13, 1] = "lte";
			array[13, 2] = "number";
			array[14, 0] = this.Localization.FilterMenuIsLessThan;
			array[14, 1] = "lt";
			array[14, 2] = "number";
			string[,] array2 = array;
			RadDropDownList radDropDownList = this.ConditionDropDownList as RadDropDownList;
			for (int i = 0; i < array2.GetLength(0); i++)
			{
				DropDownListItem dropDownListItem = new DropDownListItem
				{
					Text = array2[i, 0],
					Value = array2[i, 1]
				};
				dropDownListItem.Attributes.Add("category", array2[i, 2]);
				radDropDownList.Items.Add(dropDownListItem);
			}
		}

		// Token: 0x060052E5 RID: 21221 RVA: 0x0010146C File Offset: 0x000FF66C
		private WebControl CreateTextBox(string id)
		{
			return new RadTextBox
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false
			};
		}

		// Token: 0x060052E6 RID: 21222 RVA: 0x001014C4 File Offset: 0x000FF6C4
		private WebControl CreateNumericTextBox(string id)
		{
			return new RadNumericTextBox
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				ShowSpinButtons = true,
				EnableViewState = false
			};
		}

		// Token: 0x060052E7 RID: 21223 RVA: 0x00101524 File Offset: 0x000FF724
		private WebControl CreateDatePicker(string id)
		{
			return new RadDatePicker
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				EnableViewState = false
			};
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x0010157C File Offset: 0x000FF77C
		private WebControl CreateSearchBox(string id)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rssInput"
			};
			webControl.Attributes.Add("id", id);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Input)
			{
				CssClass = "rssFakeInput"
			};
			webControl2.Attributes.Add("type", "text");
			webControl2.Attributes.Add("name", id + "FakeInput");
			webControl.Controls.Add(webControl2);
			WebControl child = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format("{0} {1}", "rssIcon", "rssIconSearch")
			};
			webControl.Controls.Add(child);
			return webControl;
		}

		// Token: 0x060052E9 RID: 21225 RVA: 0x00101638 File Offset: 0x000FF838
		private WebControl CreateListBox(string id)
		{
			return new RadListBox
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				Height = Unit.Pixel(280),
				CheckBoxes = true,
				ShowCheckAll = true,
				EnableViewState = false
			};
		}

		// Token: 0x0400144F RID: 5199
		private FilterMenuTemplate _owner;
	}
}
