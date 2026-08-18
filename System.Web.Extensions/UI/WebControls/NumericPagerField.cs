using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Resources;
using System.Web.UI.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000BE RID: 190
	public class NumericPagerField : DataPagerField
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x000235C4 File Offset: 0x000217C4
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x000235ED File Offset: 0x000217ED
		[DefaultValue(5)]
		[Category("Appearance")]
		[ResourceDescription("NumericPagerField_ButtonCount")]
		public int ButtonCount
		{
			get
			{
				object obj = base.ViewState["ButtonCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 5;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this.ButtonCount)
				{
					base.ViewState["ButtonCount"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00023624 File Offset: 0x00021824
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x0002364D File Offset: 0x0002184D
		[Category("Appearance")]
		[DefaultValue(ButtonType.Link)]
		[ResourceDescription("NumericPagerField_ButtonType")]
		public ButtonType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (ButtonType)obj;
				}
				return ButtonType.Link;
			}
			set
			{
				if (value < ButtonType.Button || value > ButtonType.Link)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this.ButtonType)
				{
					base.ViewState["ButtonType"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00023688 File Offset: 0x00021888
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x000236B5 File Offset: 0x000218B5
		[Category("Appearance")]
		[DefaultValue("")]
		[ResourceDescription("NumericPagerField_CurrentPageLabelCssClass")]
		[CssClassProperty]
		public string CurrentPageLabelCssClass
		{
			get
			{
				object obj = base.ViewState["CurrentPageLabelCssClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.CurrentPageLabelCssClass)
				{
					base.ViewState["CurrentPageLabelCssClass"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x000236DC File Offset: 0x000218DC
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00023709 File Offset: 0x00021909
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[ResourceDescription("NumericPagerField_NextPageImageUrl")]
		[UrlProperty]
		public string NextPageImageUrl
		{
			get
			{
				object obj = base.ViewState["NextPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.NextPageImageUrl)
				{
					base.ViewState["NextPageImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00023730 File Offset: 0x00021930
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x0002375D File Offset: 0x0002195D
		[Category("Appearance")]
		[Localizable(true)]
		[ResourceDefaultValue("NumericPagerField_DefaultNextPageText")]
		[ResourceDescription("NumericPagerField_NextPageText")]
		public string NextPageText
		{
			get
			{
				object obj = base.ViewState["NextPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return AtlasWeb.NumericPagerField_DefaultNextPageText;
			}
			set
			{
				if (value != this.NextPageText)
				{
					base.ViewState["NextPageText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00023784 File Offset: 0x00021984
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x000237B1 File Offset: 0x000219B1
		[Category("Appearance")]
		[DefaultValue("")]
		[ResourceDescription("NumericPagerField_NextPreviousButtonCssClass")]
		[CssClassProperty]
		public string NextPreviousButtonCssClass
		{
			get
			{
				object obj = base.ViewState["NextPreviousButtonCssClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.NextPreviousButtonCssClass)
				{
					base.ViewState["NextPreviousButtonCssClass"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x000237D8 File Offset: 0x000219D8
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x00023805 File Offset: 0x00021A05
		[Category("Appearance")]
		[DefaultValue("")]
		[ResourceDescription("NumericPagerField_NumericButtonCssClass")]
		[CssClassProperty]
		public string NumericButtonCssClass
		{
			get
			{
				object obj = base.ViewState["NumericButtonCssClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.NumericButtonCssClass)
				{
					base.ViewState["NumericButtonCssClass"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0002382C File Offset: 0x00021A2C
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x00023859 File Offset: 0x00021A59
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[ResourceDescription("NumericPagerField_PreviousPageImageUrl")]
		[UrlProperty]
		public string PreviousPageImageUrl
		{
			get
			{
				object obj = base.ViewState["PreviousPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.PreviousPageImageUrl)
				{
					base.ViewState["PreviousPageImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00023880 File Offset: 0x00021A80
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x000238AD File Offset: 0x00021AAD
		[Category("Appearance")]
		[Localizable(true)]
		[ResourceDefaultValue("NumericPagerField_DefaultPreviousPageText")]
		[ResourceDescription("NumericPagerField_PreviousPageText")]
		public string PreviousPageText
		{
			get
			{
				object obj = base.ViewState["PreviousPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return AtlasWeb.NumericPagerField_DefaultPreviousPageText;
			}
			set
			{
				if (value != this.PreviousPageText)
				{
					base.ViewState["PreviousPageText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x000238D4 File Offset: 0x00021AD4
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x000238FD File Offset: 0x00021AFD
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("NumericPagerField_RenderNonBreakingSpacesBetweenControls")]
		public bool RenderNonBreakingSpacesBetweenControls
		{
			get
			{
				object obj = base.ViewState["RenderNonBreakingSpacesBetweenControls"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value != this.RenderNonBreakingSpacesBetweenControls)
				{
					base.ViewState["RenderNonBreakingSpacesBetweenControls"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00023924 File Offset: 0x00021B24
		private void AddNonBreakingSpace(DataPagerFieldItem container)
		{
			if (this.RenderNonBreakingSpacesBetweenControls)
			{
				container.Controls.Add(new LiteralControl("&nbsp;"));
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00023944 File Offset: 0x00021B44
		protected override void CopyProperties(DataPagerField newField)
		{
			((NumericPagerField)newField).ButtonCount = this.ButtonCount;
			((NumericPagerField)newField).ButtonType = this.ButtonType;
			((NumericPagerField)newField).CurrentPageLabelCssClass = this.CurrentPageLabelCssClass;
			((NumericPagerField)newField).NextPageImageUrl = this.NextPageImageUrl;
			((NumericPagerField)newField).NextPageText = this.NextPageText;
			((NumericPagerField)newField).NextPreviousButtonCssClass = this.NextPreviousButtonCssClass;
			((NumericPagerField)newField).NumericButtonCssClass = this.NumericButtonCssClass;
			((NumericPagerField)newField).PreviousPageImageUrl = this.PreviousPageImageUrl;
			((NumericPagerField)newField).PreviousPageText = this.PreviousPageText;
			base.CopyProperties(newField);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000239F1 File Offset: 0x00021BF1
		protected override DataPagerField CreateField()
		{
			return new NumericPagerField();
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000239F8 File Offset: 0x00021BF8
		public override void HandleEvent(CommandEventArgs e)
		{
			if (string.IsNullOrEmpty(base.DataPager.QueryStringField))
			{
				int num = this._startRowIndex / base.DataPager.PageSize;
				int num2 = this._startRowIndex / (this.ButtonCount * base.DataPager.PageSize) * this.ButtonCount;
				int num3 = num2 + this.ButtonCount - 1;
				int num4 = (num3 + 1) * base.DataPager.PageSize - 1;
				int num5;
				if (string.Equals(e.CommandName, "Prev"))
				{
					num5 = (num2 - 1) * base.DataPager.PageSize;
					if (num5 < 0)
					{
						num5 = 0;
					}
				}
				else if (string.Equals(e.CommandName, "Next"))
				{
					num5 = num4 + 1;
					if (num5 > this._totalRowCount)
					{
						num5 = this._totalRowCount - base.DataPager.PageSize;
					}
				}
				else
				{
					int num6 = Convert.ToInt32(e.CommandName, CultureInfo.InvariantCulture);
					num5 = num6 * base.DataPager.PageSize;
				}
				if (num5 != -1)
				{
					base.DataPager.SetPageProperties(num5, base.DataPager.PageSize, true);
				}
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00023B0C File Offset: 0x00021D0C
		private Control CreateNumericButton(string buttonText, string commandArgument, string commandName)
		{
			ButtonType buttonType = this.ButtonType;
			IButtonControl buttonControl;
			if (buttonType != ButtonType.Button)
			{
				if (buttonType != ButtonType.Link)
				{
				}
				buttonControl = new LinkButton();
			}
			else
			{
				buttonControl = new Button();
			}
			buttonControl.Text = buttonText;
			buttonControl.CausesValidation = false;
			buttonControl.CommandName = commandName;
			buttonControl.CommandArgument = commandArgument;
			WebControl webControl = buttonControl as WebControl;
			if (webControl != null && !string.IsNullOrEmpty(this.NumericButtonCssClass))
			{
				webControl.CssClass = this.NumericButtonCssClass;
			}
			return buttonControl as Control;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00023B7C File Offset: 0x00021D7C
		private HyperLink CreateNumericLink(int pageIndex)
		{
			int pageNumber = pageIndex + 1;
			HyperLink hyperLink = new HyperLink();
			hyperLink.Text = pageNumber.ToString(CultureInfo.InvariantCulture);
			hyperLink.NavigateUrl = base.GetQueryStringNavigateUrl(pageNumber);
			if (!string.IsNullOrEmpty(this.NumericButtonCssClass))
			{
				hyperLink.CssClass = this.NumericButtonCssClass;
			}
			return hyperLink;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00023BCC File Offset: 0x00021DCC
		private Control CreateNextPrevButton(string buttonText, string commandName, string commandArgument, string imageUrl)
		{
			IButtonControl buttonControl;
			switch (this.ButtonType)
			{
			case ButtonType.Button:
				buttonControl = new Button();
				goto IL_4F;
			case ButtonType.Link:
				buttonControl = new LinkButton();
				goto IL_4F;
			}
			buttonControl = new ImageButton();
			((ImageButton)buttonControl).ImageUrl = imageUrl;
			((ImageButton)buttonControl).AlternateText = HttpUtility.HtmlDecode(buttonText);
			IL_4F:
			buttonControl.Text = buttonText;
			buttonControl.CausesValidation = false;
			buttonControl.CommandName = commandName;
			buttonControl.CommandArgument = commandArgument;
			WebControl webControl = buttonControl as WebControl;
			if (webControl != null && !string.IsNullOrEmpty(this.NextPreviousButtonCssClass))
			{
				webControl.CssClass = this.NextPreviousButtonCssClass;
			}
			return buttonControl as Control;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00023C70 File Offset: 0x00021E70
		private HyperLink CreateNextPrevLink(string buttonText, int pageIndex, string imageUrl)
		{
			int pageNumber = pageIndex + 1;
			HyperLink hyperLink = new HyperLink();
			hyperLink.Text = buttonText;
			hyperLink.NavigateUrl = base.GetQueryStringNavigateUrl(pageNumber);
			hyperLink.ImageUrl = imageUrl;
			if (!string.IsNullOrEmpty(this.NextPreviousButtonCssClass))
			{
				hyperLink.CssClass = this.NextPreviousButtonCssClass;
			}
			return hyperLink;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00023CBC File Offset: 0x00021EBC
		public override void CreateDataPagers(DataPagerFieldItem container, int startRowIndex, int maximumRows, int totalRowCount, int fieldIndex)
		{
			this._startRowIndex = startRowIndex;
			this._maximumRows = maximumRows;
			this._totalRowCount = totalRowCount;
			if (string.IsNullOrEmpty(base.DataPager.QueryStringField))
			{
				this.CreateDataPagersForCommand(container, fieldIndex);
				return;
			}
			this.CreateDataPagersForQueryString(container, fieldIndex);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00023CFC File Offset: 0x00021EFC
		private void CreateDataPagersForCommand(DataPagerFieldItem container, int fieldIndex)
		{
			int num = this._startRowIndex / this._maximumRows;
			int num2 = this._startRowIndex / (this.ButtonCount * this._maximumRows) * this.ButtonCount;
			int num3 = num2 + this.ButtonCount - 1;
			int num4 = (num3 + 1) * this._maximumRows - 1;
			if (num2 != 0)
			{
				container.Controls.Add(this.CreateNextPrevButton(this.PreviousPageText, "Prev", fieldIndex.ToString(CultureInfo.InvariantCulture), this.PreviousPageImageUrl));
				this.AddNonBreakingSpace(container);
			}
			int num5 = 0;
			while (num5 < this.ButtonCount && this._totalRowCount > (num5 + num2) * this._maximumRows)
			{
				if (num5 + num2 == num)
				{
					Label label = new Label();
					label.Text = (num5 + num2 + 1).ToString(CultureInfo.InvariantCulture);
					if (!string.IsNullOrEmpty(this.CurrentPageLabelCssClass))
					{
						label.CssClass = this.CurrentPageLabelCssClass;
					}
					container.Controls.Add(label);
				}
				else
				{
					container.Controls.Add(this.CreateNumericButton((num5 + num2 + 1).ToString(CultureInfo.InvariantCulture), fieldIndex.ToString(CultureInfo.InvariantCulture), (num5 + num2).ToString(CultureInfo.InvariantCulture)));
				}
				this.AddNonBreakingSpace(container);
				num5++;
			}
			if (num4 < this._totalRowCount - 1)
			{
				this.AddNonBreakingSpace(container);
				container.Controls.Add(this.CreateNextPrevButton(this.NextPageText, "Next", fieldIndex.ToString(CultureInfo.InvariantCulture), this.NextPageImageUrl));
				this.AddNonBreakingSpace(container);
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00023E94 File Offset: 0x00022094
		private void CreateDataPagersForQueryString(DataPagerFieldItem container, int fieldIndex)
		{
			int num = this._startRowIndex / this._maximumRows;
			base.QueryStringHandled = true;
			int num2 = this._startRowIndex / (this.ButtonCount * this._maximumRows) * this.ButtonCount;
			int num3 = num2 + this.ButtonCount - 1;
			int num4 = (num3 + 1) * this._maximumRows - 1;
			if (num2 != 0)
			{
				container.Controls.Add(this.CreateNextPrevLink(this.PreviousPageText, num2 - 1, this.PreviousPageImageUrl));
				this.AddNonBreakingSpace(container);
			}
			int num5 = 0;
			while (num5 < this.ButtonCount && this._totalRowCount > (num5 + num2) * this._maximumRows)
			{
				if (num5 + num2 == num)
				{
					Label label = new Label();
					label.Text = (num5 + num2 + 1).ToString(CultureInfo.InvariantCulture);
					if (!string.IsNullOrEmpty(this.CurrentPageLabelCssClass))
					{
						label.CssClass = this.CurrentPageLabelCssClass;
					}
					container.Controls.Add(label);
				}
				else
				{
					container.Controls.Add(this.CreateNumericLink(num5 + num2));
				}
				this.AddNonBreakingSpace(container);
				num5++;
			}
			if (num4 < this._totalRowCount - 1)
			{
				this.AddNonBreakingSpace(container);
				container.Controls.Add(this.CreateNextPrevLink(this.NextPageText, num2 + this.ButtonCount, this.NextPageImageUrl));
				this.AddNonBreakingSpace(container);
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00023FEC File Offset: 0x000221EC
		public override bool Equals(object o)
		{
			NumericPagerField numericPagerField = o as NumericPagerField;
			return numericPagerField != null && object.Equals(numericPagerField.ButtonCount, this.ButtonCount) && numericPagerField.ButtonType == this.ButtonType && string.Equals(numericPagerField.CurrentPageLabelCssClass, this.CurrentPageLabelCssClass) && string.Equals(numericPagerField.NextPageImageUrl, this.NextPageImageUrl) && string.Equals(numericPagerField.NextPageText, this.NextPageText) && string.Equals(numericPagerField.NextPreviousButtonCssClass, this.NextPreviousButtonCssClass) && string.Equals(numericPagerField.NumericButtonCssClass, this.NumericButtonCssClass) && string.Equals(numericPagerField.PreviousPageImageUrl, this.PreviousPageImageUrl) && string.Equals(numericPagerField.PreviousPageText, this.PreviousPageText);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000240C0 File Offset: 0x000222C0
		public override int GetHashCode()
		{
			return this.ButtonCount.GetHashCode() | this.ButtonType.GetHashCode() | this.CurrentPageLabelCssClass.GetHashCode() | this.NextPageImageUrl.GetHashCode() | this.NextPageText.GetHashCode() | this.NextPreviousButtonCssClass.GetHashCode() | this.NumericButtonCssClass.GetHashCode() | this.PreviousPageImageUrl.GetHashCode() | this.PreviousPageText.GetHashCode();
		}

		// Token: 0x04000308 RID: 776
		private int _startRowIndex;

		// Token: 0x04000309 RID: 777
		private int _maximumRows;

		// Token: 0x0400030A RID: 778
		private int _totalRowCount;
	}
}
