using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Resources;
using System.Web.UI.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000BD RID: 189
	public class NextPreviousPagerField : DataPagerField
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x00022850 File Offset: 0x00020A50
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0002287D File Offset: 0x00020A7D
		[Category("Appearance")]
		[DefaultValue("")]
		[ResourceDescription("NextPreviousPagerField_ButtonCssClass")]
		[CssClassProperty]
		public string ButtonCssClass
		{
			get
			{
				object obj = base.ViewState["ButtonCssClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.ButtonCssClass)
				{
					base.ViewState["ButtonCssClass"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x000228A4 File Offset: 0x00020AA4
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x000228CD File Offset: 0x00020ACD
		[Category("Appearance")]
		[DefaultValue(ButtonType.Link)]
		[ResourceDescription("NextPreviousPagerField_ButtonType")]
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00022907 File Offset: 0x00020B07
		private bool EnableNextPage
		{
			get
			{
				return this._startRowIndex + this._maximumRows < this._totalRowCount;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0002291E File Offset: 0x00020B1E
		private bool EnablePreviousPage
		{
			get
			{
				return this._startRowIndex > 0;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0002292C File Offset: 0x00020B2C
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x00022959 File Offset: 0x00020B59
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[ResourceDescription("NextPreviousPagerField_FirstPageImageUrl")]
		[UrlProperty]
		public string FirstPageImageUrl
		{
			get
			{
				object obj = base.ViewState["FirstPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.FirstPageImageUrl)
				{
					base.ViewState["FirstPageImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00022980 File Offset: 0x00020B80
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x000229AD File Offset: 0x00020BAD
		[Category("Appearance")]
		[Localizable(true)]
		[ResourceDefaultValue("NextPrevPagerField_DefaultFirstPageText")]
		[ResourceDescription("NextPreviousPagerField_FirstPageText")]
		public string FirstPageText
		{
			get
			{
				object obj = base.ViewState["FirstPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return AtlasWeb.NextPrevPagerField_DefaultFirstPageText;
			}
			set
			{
				if (value != this.FirstPageText)
				{
					base.ViewState["FirstPageText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x000229D4 File Offset: 0x00020BD4
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00022A01 File Offset: 0x00020C01
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[ResourceDescription("NextPreviousPagerField_LastPageImageUrl")]
		[UrlProperty]
		public string LastPageImageUrl
		{
			get
			{
				object obj = base.ViewState["LastPageImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value != this.LastPageImageUrl)
				{
					base.ViewState["LastPageImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00022A28 File Offset: 0x00020C28
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00022A55 File Offset: 0x00020C55
		[Category("Appearance")]
		[Localizable(true)]
		[ResourceDefaultValue("NextPrevPagerField_DefaultLastPageText")]
		[ResourceDescription("NextPreviousPagerField_LastPageText")]
		public string LastPageText
		{
			get
			{
				object obj = base.ViewState["LastPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return AtlasWeb.NextPrevPagerField_DefaultLastPageText;
			}
			set
			{
				if (value != this.LastPageText)
				{
					base.ViewState["LastPageText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00022A7C File Offset: 0x00020C7C
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00022AA9 File Offset: 0x00020CA9
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[ResourceDescription("NextPreviousPagerField_NextPageImageUrl")]
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

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00022AD0 File Offset: 0x00020CD0
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x00022AFD File Offset: 0x00020CFD
		[Category("Appearance")]
		[Localizable(true)]
		[ResourceDefaultValue("NextPrevPagerField_DefaultNextPageText")]
		[ResourceDescription("NextPreviousPagerField_NextPageText")]
		public string NextPageText
		{
			get
			{
				object obj = base.ViewState["NextPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return AtlasWeb.NextPrevPagerField_DefaultNextPageText;
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

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00022B24 File Offset: 0x00020D24
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x00022B51 File Offset: 0x00020D51
		[Category("Appearance")]
		[DefaultValue("")]
		[Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
		[ResourceDescription("NextPreviousPagerField_PreviousPageImageUrl")]
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

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00022B78 File Offset: 0x00020D78
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x00022BA5 File Offset: 0x00020DA5
		[Category("Appearance")]
		[Localizable(true)]
		[ResourceDefaultValue("NextPrevPagerField_DefaultPreviousPageText")]
		[ResourceDescription("NextPreviousPagerField_PreviousPageText")]
		public string PreviousPageText
		{
			get
			{
				object obj = base.ViewState["PreviousPageText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return AtlasWeb.NextPrevPagerField_DefaultPreviousPageText;
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

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x00022BCC File Offset: 0x00020DCC
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x00022BF5 File Offset: 0x00020DF5
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("NextPreviousPagerField_RenderNonBreakingSpacesBetweenControls")]
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

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00022C1C File Offset: 0x00020E1C
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x00022C45 File Offset: 0x00020E45
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("NextPreviousPagerField_RenderDisabledButtonsAsLabels")]
		public bool RenderDisabledButtonsAsLabels
		{
			get
			{
				object obj = base.ViewState["RenderDisabledButtonsAsLabels"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value != this.RenderDisabledButtonsAsLabels)
				{
					base.ViewState["RenderDisabledButtonsAsLabels"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00022C6C File Offset: 0x00020E6C
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x00022C95 File Offset: 0x00020E95
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("NextPreviousPagerField_ShowFirstPageButton")]
		public bool ShowFirstPageButton
		{
			get
			{
				object obj = base.ViewState["ShowFirstPageButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value != this.ShowFirstPageButton)
				{
					base.ViewState["ShowFirstPageButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00022CBC File Offset: 0x00020EBC
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00022CE5 File Offset: 0x00020EE5
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("NextPreviousPagerField_ShowLastPageButton")]
		public bool ShowLastPageButton
		{
			get
			{
				object obj = base.ViewState["ShowLastPageButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value != this.ShowLastPageButton)
				{
					base.ViewState["ShowLastPageButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x00022D0C File Offset: 0x00020F0C
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00022D35 File Offset: 0x00020F35
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("NextPreviousPagerField_ShowNextPageButton")]
		public bool ShowNextPageButton
		{
			get
			{
				object obj = base.ViewState["ShowNextPageButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value != this.ShowNextPageButton)
				{
					base.ViewState["ShowNextPageButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00022D5C File Offset: 0x00020F5C
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00022D85 File Offset: 0x00020F85
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("NextPreviousPagerField_ShowPreviousPageButton")]
		public bool ShowPreviousPageButton
		{
			get
			{
				object obj = base.ViewState["ShowPreviousPageButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value != this.ShowPreviousPageButton)
				{
					base.ViewState["ShowPreviousPageButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00022DAC File Offset: 0x00020FAC
		private void AddNonBreakingSpace(DataPagerFieldItem container)
		{
			if (this.RenderNonBreakingSpacesBetweenControls)
			{
				container.Controls.Add(new LiteralControl("&nbsp;"));
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00022DCC File Offset: 0x00020FCC
		protected override void CopyProperties(DataPagerField newField)
		{
			((NextPreviousPagerField)newField).ButtonCssClass = this.ButtonCssClass;
			((NextPreviousPagerField)newField).ButtonType = this.ButtonType;
			((NextPreviousPagerField)newField).FirstPageImageUrl = this.FirstPageImageUrl;
			((NextPreviousPagerField)newField).FirstPageText = this.FirstPageText;
			((NextPreviousPagerField)newField).LastPageImageUrl = this.LastPageImageUrl;
			((NextPreviousPagerField)newField).LastPageText = this.LastPageText;
			((NextPreviousPagerField)newField).NextPageImageUrl = this.NextPageImageUrl;
			((NextPreviousPagerField)newField).NextPageText = this.NextPageText;
			((NextPreviousPagerField)newField).PreviousPageImageUrl = this.PreviousPageImageUrl;
			((NextPreviousPagerField)newField).PreviousPageText = this.PreviousPageText;
			((NextPreviousPagerField)newField).ShowFirstPageButton = this.ShowFirstPageButton;
			((NextPreviousPagerField)newField).ShowLastPageButton = this.ShowLastPageButton;
			((NextPreviousPagerField)newField).ShowNextPageButton = this.ShowNextPageButton;
			((NextPreviousPagerField)newField).ShowPreviousPageButton = this.ShowPreviousPageButton;
			base.CopyProperties(newField);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00022ECE File Offset: 0x000210CE
		protected override DataPagerField CreateField()
		{
			return new NextPreviousPagerField();
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00022ED8 File Offset: 0x000210D8
		public override void HandleEvent(CommandEventArgs e)
		{
			if (string.IsNullOrEmpty(base.DataPager.QueryStringField))
			{
				if (string.Equals(e.CommandName, "Prev"))
				{
					int num = this._startRowIndex - base.DataPager.PageSize;
					if (num < 0)
					{
						num = 0;
					}
					base.DataPager.SetPageProperties(num, base.DataPager.PageSize, true);
					return;
				}
				if (string.Equals(e.CommandName, "Next"))
				{
					int num2 = this._startRowIndex + base.DataPager.PageSize;
					if (num2 > this._totalRowCount)
					{
						num2 = this._totalRowCount - base.DataPager.PageSize;
					}
					base.DataPager.SetPageProperties(num2, base.DataPager.PageSize, true);
					return;
				}
				if (string.Equals(e.CommandName, "First"))
				{
					base.DataPager.SetPageProperties(0, base.DataPager.PageSize, true);
					return;
				}
				if (string.Equals(e.CommandName, "Last"))
				{
					int num3 = this._totalRowCount % base.DataPager.PageSize;
					int startRowIndex;
					if (num3 == 0)
					{
						startRowIndex = this._totalRowCount - base.DataPager.PageSize;
					}
					else
					{
						startRowIndex = this._totalRowCount - num3;
					}
					base.DataPager.SetPageProperties(startRowIndex, base.DataPager.PageSize, true);
				}
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00023024 File Offset: 0x00021224
		private Control CreateControl(string commandName, string buttonText, int fieldIndex, string imageUrl, bool enabled)
		{
			if (!enabled && this.RenderDisabledButtonsAsLabels)
			{
				Label label = new Label();
				label.Text = buttonText;
				if (!string.IsNullOrEmpty(this.ButtonCssClass))
				{
					label.CssClass = this.ButtonCssClass;
				}
				return label;
			}
			IButtonControl buttonControl;
			switch (this.ButtonType)
			{
			case ButtonType.Button:
				buttonControl = new Button();
				((Button)buttonControl).Enabled = enabled;
				goto IL_AA;
			case ButtonType.Link:
				buttonControl = new LinkButton();
				((LinkButton)buttonControl).Enabled = enabled;
				goto IL_AA;
			}
			buttonControl = new ImageButton();
			((ImageButton)buttonControl).ImageUrl = imageUrl;
			((ImageButton)buttonControl).Enabled = enabled;
			((ImageButton)buttonControl).AlternateText = HttpUtility.HtmlDecode(buttonText);
			IL_AA:
			buttonControl.Text = buttonText;
			buttonControl.CausesValidation = false;
			buttonControl.CommandName = commandName;
			buttonControl.CommandArgument = fieldIndex.ToString(CultureInfo.InvariantCulture);
			WebControl webControl = buttonControl as WebControl;
			if (webControl != null && !string.IsNullOrEmpty(this.ButtonCssClass))
			{
				webControl.CssClass = this.ButtonCssClass;
			}
			return buttonControl as Control;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002312C File Offset: 0x0002132C
		private HyperLink CreateLink(string buttonText, int pageIndex, string imageUrl, bool enabled)
		{
			int pageNumber = pageIndex + 1;
			HyperLink hyperLink = new HyperLink();
			hyperLink.Text = buttonText;
			hyperLink.NavigateUrl = base.GetQueryStringNavigateUrl(pageNumber);
			hyperLink.ImageUrl = imageUrl;
			hyperLink.Enabled = enabled;
			if (!string.IsNullOrEmpty(this.ButtonCssClass))
			{
				hyperLink.CssClass = this.ButtonCssClass;
			}
			return hyperLink;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00023180 File Offset: 0x00021380
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

		// Token: 0x06000937 RID: 2359 RVA: 0x000231C0 File Offset: 0x000213C0
		private void CreateDataPagersForCommand(DataPagerFieldItem container, int fieldIndex)
		{
			if (this.ShowFirstPageButton)
			{
				container.Controls.Add(this.CreateControl("First", this.FirstPageText, fieldIndex, this.FirstPageImageUrl, this.EnablePreviousPage));
				this.AddNonBreakingSpace(container);
			}
			if (this.ShowPreviousPageButton)
			{
				container.Controls.Add(this.CreateControl("Prev", this.PreviousPageText, fieldIndex, this.PreviousPageImageUrl, this.EnablePreviousPage));
				this.AddNonBreakingSpace(container);
			}
			if (this.ShowNextPageButton)
			{
				container.Controls.Add(this.CreateControl("Next", this.NextPageText, fieldIndex, this.NextPageImageUrl, this.EnableNextPage));
				this.AddNonBreakingSpace(container);
			}
			if (this.ShowLastPageButton)
			{
				container.Controls.Add(this.CreateControl("Last", this.LastPageText, fieldIndex, this.LastPageImageUrl, this.EnableNextPage));
				this.AddNonBreakingSpace(container);
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000232B0 File Offset: 0x000214B0
		private void CreateDataPagersForQueryString(DataPagerFieldItem container, int fieldIndex)
		{
			base.QueryStringHandled = true;
			if (this.ShowFirstPageButton)
			{
				container.Controls.Add(this.CreateLink(this.FirstPageText, 0, this.FirstPageImageUrl, this.EnablePreviousPage));
				this.AddNonBreakingSpace(container);
			}
			if (this.ShowPreviousPageButton)
			{
				int pageIndex = this._startRowIndex / this._maximumRows - 1;
				container.Controls.Add(this.CreateLink(this.PreviousPageText, pageIndex, this.PreviousPageImageUrl, this.EnablePreviousPage));
				this.AddNonBreakingSpace(container);
			}
			if (this.ShowNextPageButton)
			{
				int pageIndex2 = (this._startRowIndex + this._maximumRows) / this._maximumRows;
				container.Controls.Add(this.CreateLink(this.NextPageText, pageIndex2, this.NextPageImageUrl, this.EnableNextPage));
				this.AddNonBreakingSpace(container);
			}
			if (this.ShowLastPageButton)
			{
				int pageIndex3 = this._totalRowCount / this._maximumRows - ((this._totalRowCount % this._maximumRows == 0) ? 1 : 0);
				container.Controls.Add(this.CreateLink(this.LastPageText, pageIndex3, this.LastPageImageUrl, this.EnableNextPage));
				this.AddNonBreakingSpace(container);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000233D8 File Offset: 0x000215D8
		public override bool Equals(object o)
		{
			NextPreviousPagerField nextPreviousPagerField = o as NextPreviousPagerField;
			return nextPreviousPagerField != null && string.Equals(nextPreviousPagerField.ButtonCssClass, this.ButtonCssClass) && nextPreviousPagerField.ButtonType == this.ButtonType && string.Equals(nextPreviousPagerField.FirstPageImageUrl, this.FirstPageImageUrl) && string.Equals(nextPreviousPagerField.FirstPageText, this.FirstPageText) && string.Equals(nextPreviousPagerField.LastPageImageUrl, this.LastPageImageUrl) && string.Equals(nextPreviousPagerField.LastPageText, this.LastPageText) && string.Equals(nextPreviousPagerField.NextPageImageUrl, this.NextPageImageUrl) && string.Equals(nextPreviousPagerField.NextPageText, this.NextPageText) && string.Equals(nextPreviousPagerField.PreviousPageImageUrl, this.PreviousPageImageUrl) && string.Equals(nextPreviousPagerField.PreviousPageText, this.PreviousPageText) && nextPreviousPagerField.ShowFirstPageButton == this.ShowFirstPageButton && nextPreviousPagerField.ShowLastPageButton == this.ShowLastPageButton && nextPreviousPagerField.ShowNextPageButton == this.ShowNextPageButton && nextPreviousPagerField.ShowPreviousPageButton == this.ShowPreviousPageButton;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000234F8 File Offset: 0x000216F8
		public override int GetHashCode()
		{
			return this.ButtonCssClass.GetHashCode() | this.ButtonType.GetHashCode() | this.FirstPageImageUrl.GetHashCode() | this.FirstPageText.GetHashCode() | this.LastPageImageUrl.GetHashCode() | this.LastPageText.GetHashCode() | this.NextPageImageUrl.GetHashCode() | this.NextPageText.GetHashCode() | this.PreviousPageImageUrl.GetHashCode() | this.PreviousPageText.GetHashCode() | this.ShowFirstPageButton.GetHashCode() | this.ShowLastPageButton.GetHashCode() | this.ShowNextPageButton.GetHashCode() | this.ShowPreviousPageButton.GetHashCode();
		}

		// Token: 0x04000305 RID: 773
		private int _startRowIndex;

		// Token: 0x04000306 RID: 774
		private int _maximumRows;

		// Token: 0x04000307 RID: 775
		private int _totalRowCount;
	}
}
