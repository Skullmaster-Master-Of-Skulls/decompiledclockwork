using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetCustomFormat
{
	// Token: 0x020008A7 RID: 2215
	internal abstract class ViewBase : ICustomFormatView
	{
		// Token: 0x17001AF5 RID: 6901
		// (get) Token: 0x0600524C RID: 21068 RVA: 0x000FFFB5 File Offset: 0x000FE1B5
		// (set) Token: 0x0600524D RID: 21069 RVA: 0x000FFFBD File Offset: 0x000FE1BD
		public CustomFormatTemplate Owner
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

		// Token: 0x17001AF6 RID: 6902
		// (get) Token: 0x0600524E RID: 21070 RVA: 0x000FFFC6 File Offset: 0x000FE1C6
		public SpreadsheetStrings Localization
		{
			get
			{
				return this.Owner.Owner.Localization;
			}
		}

		// Token: 0x17001AF7 RID: 6903
		// (get) Token: 0x0600524F RID: 21071 RVA: 0x000FFFD8 File Offset: 0x000FE1D8
		public CultureInfo Culture
		{
			get
			{
				return this.Owner.Owner.Culture;
			}
		}

		// Token: 0x17001AF8 RID: 6904
		// (get) Token: 0x06005250 RID: 21072 RVA: 0x000FFFEA File Offset: 0x000FE1EA
		// (set) Token: 0x06005251 RID: 21073 RVA: 0x000FFFF2 File Offset: 0x000FE1F2
		public WebControl SaveButton { get; set; }

		// Token: 0x17001AF9 RID: 6905
		// (get) Token: 0x06005252 RID: 21074 RVA: 0x000FFFFB File Offset: 0x000FE1FB
		// (set) Token: 0x06005253 RID: 21075 RVA: 0x00100003 File Offset: 0x000FE203
		public WebControl CancelButton { get; set; }

		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x06005254 RID: 21076 RVA: 0x0010000C File Offset: 0x000FE20C
		// (set) Token: 0x06005255 RID: 21077 RVA: 0x00100014 File Offset: 0x000FE214
		public WebControl NumberFormatsListBox { get; set; }

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x06005256 RID: 21078 RVA: 0x0010001D File Offset: 0x000FE21D
		// (set) Token: 0x06005257 RID: 21079 RVA: 0x00100025 File Offset: 0x000FE225
		public WebControl CurrencyFormatsListBox { get; set; }

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x06005258 RID: 21080 RVA: 0x0010002E File Offset: 0x000FE22E
		// (set) Token: 0x06005259 RID: 21081 RVA: 0x00100036 File Offset: 0x000FE236
		public WebControl DateTimeFormatsListBox { get; set; }

		// Token: 0x0600525A RID: 21082 RVA: 0x0010003F File Offset: 0x000FE23F
		public ViewBase(CustomFormatTemplate owner)
		{
			this.Owner = owner;
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x00100067 File Offset: 0x000FE267
		public void CreateControls()
		{
			this.CreateListBoxes();
			this.CreateCommandButtons();
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x00100078 File Offset: 0x000FE278
		protected void CreateListBoxes()
		{
			this.NumberFormatsListBox = this.CreateListBox("NumberFormatsListBox");
			this.PopulateNumberFormatsListBox();
			this.CurrencyFormatsListBox = this.CreateListBox("CurrencyFormatsListBox");
			this.PopulateCurrencyFormatsListBox();
			this.DateTimeFormatsListBox = this.CreateListBox("DateTimeFormatsListBox");
			this.PopulateDateTimeFormatsListBox();
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x001000CC File Offset: 0x000FE2CC
		private void PopulateNumberFormatsListBox()
		{
			string[,] array = new string[4, 2];
			array[0, 0] = "100.00%";
			array[0, 1] = "#.00%";
			array[1, 0] = "100%";
			array[1, 1] = "#%";
			array[2, 0] = "1024.00";
			array[2, 1] = "#.00";
			array[3, 0] = "1,024.00";
			array[3, 1] = "#,###.00";
			string[,] values = array;
			this.PopulateListBox(this.NumberFormatsListBox, values);
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x00100158 File Offset: 0x000FE358
		private void PopulateCurrencyFormatsListBox()
		{
			string[,] array = new string[3, 2];
			array[0, 0] = "$100.00";
			array[0, 1] = "$?.00";
			array[1, 0] = "USD 100.00";
			array[1, 1] = "\"USD\" ?.00";
			array[2, 0] = "$100";
			array[2, 1] = "$?";
			string[,] values = array;
			this.PopulateListBox(this.CurrencyFormatsListBox, values);
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x001001CC File Offset: 0x000FE3CC
		private void PopulateDateTimeFormatsListBox()
		{
			string[,] array = new string[9, 2];
			array[0, 0] = this.DefaultDate.ToString("d", this.Culture);
			array[0, 1] = this.Culture.DateTimeFormat.ShortDatePattern;
			array[1, 0] = this.DefaultDate.ToString("D", this.Culture);
			array[1, 1] = this.Culture.DateTimeFormat.LongDatePattern;
			array[2, 0] = this.DefaultDate.ToString("F", this.Culture);
			array[2, 1] = this.Culture.DateTimeFormat.FullDateTimePattern;
			array[3, 0] = this.DefaultDate.ToString("g", this.Culture);
			array[3, 1] = this.Culture.DateTimeFormat.ShortDatePattern + " " + this.Culture.DateTimeFormat.ShortTimePattern;
			array[4, 0] = this.DefaultDate.ToString("G", this.Culture);
			array[4, 1] = this.Culture.DateTimeFormat.ShortDatePattern + " " + this.Culture.DateTimeFormat.LongTimePattern;
			array[5, 0] = this.DefaultDate.ToString("M", this.Culture);
			array[5, 1] = this.Culture.DateTimeFormat.MonthDayPattern;
			array[6, 0] = this.DefaultDate.ToString("t", this.Culture);
			array[6, 1] = this.Culture.DateTimeFormat.ShortTimePattern;
			array[7, 0] = this.DefaultDate.ToString("T", this.Culture);
			array[7, 1] = this.Culture.DateTimeFormat.LongTimePattern;
			array[8, 0] = this.DefaultDate.ToString("Y", this.Culture);
			array[8, 1] = this.Culture.DateTimeFormat.YearMonthPattern;
			string[,] values = array;
			this.PopulateListBox(this.DateTimeFormatsListBox, values);
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x0010042D File Offset: 0x000FE62D
		protected void CreateCommandButtons()
		{
			this.CreateSaveButton();
			this.CreateCancelButton();
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0010043B File Offset: 0x000FE63B
		protected void CreateSaveButton()
		{
			this.SaveButton = this.CreateCommandButton(this.Localization.CustomFormatSave, "rssPrimary");
			this.SaveButton.Attributes.Add("data-command", "save");
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00100473 File Offset: 0x000FE673
		protected void CreateCancelButton()
		{
			this.CancelButton = this.CreateCommandButton(this.Localization.CustomFormatCancel, "");
			this.CancelButton.Attributes.Add("data-command", "cancel");
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x001004AC File Offset: 0x000FE6AC
		private WebControl CreateCommandButton(string text, string cssClass = "")
		{
			return new WebControl(HtmlTextWriterTag.Span)
			{
				Controls = 
				{
					new LiteralControl(text)
				},
				CssClass = string.Format("{0} {1}", "rssButton", cssClass).Trim()
			};
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x001004F0 File Offset: 0x000FE6F0
		private WebControl CreateListBox(string id)
		{
			return new RadListBox
			{
				ID = id,
				RenderMode = RenderMode.Lightweight,
				Skin = this.Owner.Owner.ResolvedSkin,
				EnableEmbeddedSkins = this.Owner.Owner.EnableEmbeddedSkins,
				Height = Unit.Pixel(210),
				Width = Unit.Percentage(100.0),
				EnableViewState = false
			};
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x0010056C File Offset: 0x000FE76C
		private void PopulateListBox(WebControl listBox, string[,] values)
		{
			RadListBox radListBox = listBox as RadListBox;
			for (int i = 0; i < values.GetLength(0); i++)
			{
				RadListBoxItem item = new RadListBoxItem
				{
					Text = values[i, 0],
					Value = values[i, 1]
				};
				radListBox.Items.Add(item);
			}
		}

		// Token: 0x04001411 RID: 5137
		private const int Year = 1994;

		// Token: 0x04001412 RID: 5138
		private const int Month = 8;

		// Token: 0x04001413 RID: 5139
		private const int Day = 21;

		// Token: 0x04001414 RID: 5140
		private const int Hour = 18;

		// Token: 0x04001415 RID: 5141
		private const int Minutes = 25;

		// Token: 0x04001416 RID: 5142
		private const int Seconds = 37;

		// Token: 0x04001417 RID: 5143
		private readonly DateTime DefaultDate = new DateTime(1994, 8, 21, 18, 25, 37);

		// Token: 0x04001418 RID: 5144
		private CustomFormatTemplate _owner;
	}
}
