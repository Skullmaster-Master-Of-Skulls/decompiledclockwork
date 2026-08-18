using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019F6 RID: 6646
	public class GridNumericColumn : GridBoundColumn
	{
		// Token: 0x0601015A RID: 65882 RVA: 0x0039D430 File Offset: 0x0039B630
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public GridNumericColumn()
		{
			this.CurrentFilterFunction = GridKnownFunction.NoFilter;
			this.FilterListOptions = GridFilterListOptions.VaryByDataType;
		}

		// Token: 0x0601015B RID: 65883 RVA: 0x0039D448 File Offset: 0x0039B648
		public override GridColumn Clone()
		{
			GridNumericColumn gridNumericColumn = new GridNumericColumn();
			gridNumericColumn.CopyBaseProperties(this);
			return gridNumericColumn;
		}

		// Token: 0x0601015C RID: 65884 RVA: 0x0039D464 File Offset: 0x0039B664
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridNumericColumn gridNumericColumn = (GridNumericColumn)fromColumn;
			this.NumericType = gridNumericColumn.NumericType;
			this.DbValueFactor = gridNumericColumn.DbValueFactor;
			this.AllowRounding = gridNumericColumn.AllowRounding;
			this.KeepNotRoundedValue = gridNumericColumn.KeepNotRoundedValue;
			this.DecimalDigits = gridNumericColumn.DecimalDigits;
			this.MaxValue = gridNumericColumn.MaxValue;
			this.MinValue = gridNumericColumn.MinValue;
			this.ShowSpinButtons = gridNumericColumn.ShowSpinButtons;
			this.AllowOutOfRangeAutoCorrect = gridNumericColumn.AllowOutOfRangeAutoCorrect;
			this.NumericDataType = gridNumericColumn.NumericDataType;
		}

		// Token: 0x0601015D RID: 65885 RVA: 0x0039D4F7 File Offset: 0x0039B6F7
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				return new GridMobileNumericColumnEditor(this);
			}
			return new GridNumericColumnEditor(this);
		}

		// Token: 0x0601015E RID: 65886 RVA: 0x0039D540 File Offset: 0x0039B740
		protected override void SetupFilterControls(TableCell cell)
		{
			if (this.FilterTemplate != null)
			{
				this.FilterTemplate.InstantiateIn(cell);
				return;
			}
			RadNumericTextBox radNumericTextBox = new RadNumericTextBox();
			radNumericTextBox.RenderMode = base.Owner.OwnerGrid.RenderMode;
			radNumericTextBox.ID = string.Format("RNTBF_{0}", this.UniqueName);
			cell.Controls.Add(radNumericTextBox);
			radNumericTextBox.Attributes["alt"] = this.FilterControlAltText;
			radNumericTextBox.ToolTip = this.FilterControlToolTip;
			radNumericTextBox.Type = this.NumericType;
			radNumericTextBox.NumberFormat.AllowRounding = this.AllowRounding;
			radNumericTextBox.NumberFormat.KeepNotRoundedValue = this.KeepNotRoundedValue;
			radNumericTextBox.NumberFormat.DecimalDigits = this.DecimalDigits;
			radNumericTextBox.MaxValue = this.MaxValue;
			radNumericTextBox.MinValue = this.MinValue;
			radNumericTextBox.ShowSpinButtons = this.ShowSpinButtons;
			radNumericTextBox.AllowOutOfRangeAutoCorrect = this.AllowOutOfRangeAutoCorrect;
			radNumericTextBox.DbValueFactor = this.DbValueFactor;
			radNumericTextBox.DataType = this.NumericDataType;
			radNumericTextBox.EnableEmbeddedSkins = base.Owner.OwnerGrid.EnableEmbeddedSkins;
			radNumericTextBox.EnableAriaSupport = base.Owner.OwnerGrid.EnableAriaSupport;
			if (radNumericTextBox.EnableAriaSupport)
			{
				radNumericTextBox.Attributes.Add("aria-label", this.HeaderText);
			}
			radNumericTextBox.PreRender += delegate(object sender, EventArgs e)
			{
				((RadNumericTextBox)sender).Skin = base.Owner.OwnerGrid.RuntimeSkin;
			};
			if (!this.FilterControlWidth.IsEmpty)
			{
				radNumericTextBox.Width = this.FilterControlWidth;
			}
			int? filterDelay;
			if (this.FilterDelay > 0)
			{
				filterDelay = this.FilterDelay;
			}
			else
			{
				filterDelay = new int?(0);
			}
			string format = "$find(\"{0}\")._filterOnKey{1}WithDelay(event,\"{2}\",\"{3}\",\"{4}\", true)";
			string text = string.Format("$find(\"{0}\")._filterNoDelay(\"{1}\",\"{2}\", true)", base.Owner.ClientID, radNumericTextBox.ClientID, this.UniqueName);
			if (this.AutoPostBackOnFilter)
			{
				radNumericTextBox.ClientEvents.OnValueChanged = "function(sender, args){" + text + "}";
			}
			if (this.FilterDelay != null)
			{
				radNumericTextBox.Attributes["onkeydown"] = string.Format("{0}", string.Format(format, new object[]
				{
					base.Owner.ClientID,
					"Down",
					radNumericTextBox.ClientID,
					this.UniqueName,
					filterDelay
				}));
				radNumericTextBox.Attributes["onkeypress"] = string.Format("{0}", string.Format(format, new object[]
				{
					base.Owner.ClientID,
					"Press",
					radNumericTextBox.ClientID,
					this.UniqueName,
					filterDelay
				}));
			}
			else if (this.AutoPostBackOnFilter)
			{
				radNumericTextBox.Attributes["onkeypress"] = string.Format("if(event.keyCode == 13){{ this.blur(); event.cancelBubble = true; event.returnValue = false; if (event.stopPropagation){{ event.stopPropagation(); event.preventDefault();}} {0} }}", text);
			}
			else
			{
				radNumericTextBox.ClientEvents.OnKeyPress = "Telerik.Web.UI.RadInputControl.CancelRawEventOnEnterKey";
			}
			if (this.ShowFilterIcon)
			{
				if (base.Owner.OwnerGrid.ShouldRenderImg(this.FilterImageUrl))
				{
					Image image = new Image();
					image.ImageUrl = this.FilterImageUrl;
					image.AlternateText = this.FilterImageToolTip;
					image.ToolTip = this.FilterImageToolTip;
					image.BorderWidth = Unit.Empty;
					image.ID = string.Format("Filter_{0}", this.UniqueName);
					image.Style["vertical-align"] = "middle";
					cell.Controls.Add(image);
					return;
				}
				if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					ElasticButton elasticButton = new ElasticButton
					{
						CssClass = "t-button rgActionButton ",
						FirstSpanClass = "t-font-icon rgIcon rgFilterIcon"
					};
					ElasticButton elasticButton2 = elasticButton;
					elasticButton2.CssClass += "rgFilter";
					RadGrid.ToggleColumnFilteredClass(elasticButton, this);
					elasticButton.ToolTip = this.FilterImageToolTip;
					elasticButton.Text = this.FilterImageToolTip;
					elasticButton.ID = string.Format("Filter_{0}", this.UniqueName);
					if (radNumericTextBox.EnableAriaSupport)
					{
						elasticButton.Attributes.Add("aria-label", this.FilterImageToolTip);
					}
					cell.Controls.Add(elasticButton);
					return;
				}
				Button button = new Button();
				button.CssClass = "rgFilter";
				RadGrid.ToggleColumnFilteredClass(button, this);
				button.ToolTip = this.FilterImageToolTip;
				button.ID = string.Format("Filter_{0}", this.UniqueName);
				cell.Controls.Add(button);
			}
		}

		// Token: 0x0601015F RID: 65887 RVA: 0x0039D9E4 File Offset: 0x0039BBE4
		internal override void SetCurrentFilterValueFromFilterCommand(string value)
		{
			double num = 0.0;
			if (double.TryParse(value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num))
			{
				base.SetCurrentFilterValueFromFilterCommand(num.ToString());
				return;
			}
			base.SetCurrentFilterValueFromFilterCommand(value);
		}

		// Token: 0x06010160 RID: 65888 RVA: 0x0039DA24 File Offset: 0x0039BC24
		protected override void SetCurrentFilterValueToControl(TableCell cell)
		{
			base.SetCurrentFilterValueToControl(cell);
			if (!string.IsNullOrEmpty(this.CurrentFilterValue))
			{
				if (this.IsBetweenFilter)
				{
					int num = 0;
					string[] array = new string[2];
					array = this.CurrentFilterValue.Split(new char[]
					{
						' '
					}, 2);
					using (IEnumerator enumerator = cell.Controls.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Control control = (Control)obj;
							RadNumericTextBox radNumericTextBox = control as RadNumericTextBox;
							if (radNumericTextBox != null)
							{
								radNumericTextBox.DbValue = array[num];
								num++;
								if (num == 2)
								{
									break;
								}
							}
						}
						return;
					}
				}
				foreach (object obj2 in cell.Controls)
				{
					Control control2 = (Control)obj2;
					RadNumericTextBox radNumericTextBox2 = control2 as RadNumericTextBox;
					if (radNumericTextBox2 != null)
					{
						radNumericTextBox2.DbValue = this.CurrentFilterValue;
						break;
					}
				}
			}
		}

		// Token: 0x06010161 RID: 65889 RVA: 0x0039DB44 File Offset: 0x0039BD44
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Double.ToString")]
		protected override string GetCurrentFilterValueFromControl(TableCell cell)
		{
			if (this.IsBetweenFilter)
			{
				double? num = null;
				double? num2 = null;
				int num3 = 0;
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					RadNumericTextBox radNumericTextBox = control as RadNumericTextBox;
					if (radNumericTextBox != null)
					{
						if (num3 == 0)
						{
							num = radNumericTextBox.Value;
						}
						else if (num3 == 1)
						{
							num2 = radNumericTextBox.Value;
						}
						num3++;
						if (num3 == 2)
						{
							break;
						}
					}
				}
				if (num == null || num2 == null)
				{
					return string.Empty;
				}
				if (base.Owner.OwnerGrid.EnableLinqExpressions)
				{
					return num.Value.ToString() + " " + num2.Value.ToString();
				}
				return num.Value.ToString(CultureInfo.InvariantCulture) + " " + num2.Value.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				double? num4 = null;
				foreach (object obj2 in cell.Controls)
				{
					Control control2 = (Control)obj2;
					RadNumericTextBox radNumericTextBox2 = control2 as RadNumericTextBox;
					if (radNumericTextBox2 != null)
					{
						num4 = radNumericTextBox2.Value;
						break;
					}
				}
				if (num4 == null)
				{
					return string.Empty;
				}
				if (base.Owner.OwnerGrid.EnableLinqExpressions)
				{
					return num4.Value.ToString();
				}
				return num4.Value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06010162 RID: 65890 RVA: 0x0039DD24 File Offset: 0x0039BF24
		protected virtual string GetCurrentFilterValueFromControlCultureSpecific(TableCell cell)
		{
			if (this.IsBetweenFilter)
			{
				double? num = null;
				double? num2 = null;
				RadNumericTextBox radNumericTextBox = null;
				RadNumericTextBox radNumericTextBox2 = null;
				int num3 = 0;
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					RadNumericTextBox radNumericTextBox3 = control as RadNumericTextBox;
					if (radNumericTextBox3 != null)
					{
						if (num3 == 0)
						{
							num = radNumericTextBox3.Value;
							radNumericTextBox = radNumericTextBox3;
						}
						else if (num3 == 1)
						{
							num2 = radNumericTextBox3.Value;
							radNumericTextBox2 = radNumericTextBox3;
						}
						num3++;
						if (num3 == 2)
						{
							break;
						}
					}
				}
				if (num == null || num2 == null || radNumericTextBox == null || radNumericTextBox2 == null)
				{
					return string.Empty;
				}
				return num.Value.ToString(radNumericTextBox.Culture.NumberFormat) + " " + num2.Value.ToString(radNumericTextBox2.Culture.NumberFormat);
			}
			else
			{
				double? num4 = null;
				RadNumericTextBox radNumericTextBox4 = null;
				foreach (object obj2 in cell.Controls)
				{
					Control control2 = (Control)obj2;
					radNumericTextBox4 = (control2 as RadNumericTextBox);
					if (radNumericTextBox4 != null)
					{
						num4 = radNumericTextBox4.Value;
						break;
					}
				}
				if (num4 == null)
				{
					return string.Empty;
				}
				return num4.Value.ToString(radNumericTextBox4.Culture.NumberFormat);
			}
		}

		// Token: 0x06010163 RID: 65891 RVA: 0x0039DED0 File Offset: 0x0039C0D0
		public override void RefreshCurrentFilterValue(GridFilteringItem filteringItem)
		{
			TableCell cell = filteringItem[this.UniqueName];
			string currentFilterValueFromControlCultureSpecific = this.GetCurrentFilterValueFromControlCultureSpecific(cell);
			this.CurrentFilterValue = currentFilterValueFromControlCultureSpecific;
		}

		// Token: 0x06010164 RID: 65892 RVA: 0x0039DEFC File Offset: 0x0039C0FC
		public override void RefreshCurrentFilterValue(GridFilteringItem filteringItem, string functionName)
		{
			TableCell cell = filteringItem[this.UniqueName];
			string currentFilterValueFromControlCultureSpecific = this.GetCurrentFilterValueFromControlCultureSpecific(cell);
			this.CurrentFilterValue = currentFilterValueFromControlCultureSpecific;
			try
			{
				this.CurrentFilterFunction = (GridKnownFunction)Enum.Parse(typeof(GridKnownFunction), functionName);
			}
			catch (Exception)
			{
				throw new GridException(string.Format("{0} is not supported filter function for {1}. Custom filter functions must be handled in the ItemCommand event handler. Set e.Canceled=true to stop the built-in filtering.", functionName, this.ColumnType));
			}
		}

		// Token: 0x17004DA4 RID: 19876
		// (get) Token: 0x06010165 RID: 65893 RVA: 0x0039DF6C File Offset: 0x0039C16C
		// (set) Token: 0x06010166 RID: 65894 RVA: 0x0039DFA2 File Offset: 0x0039C1A2
		[Category("Behavior")]
		[DefaultValue(1.0)]
		[NotifyParentProperty(true)]
		public double DbValueFactor
		{
			get
			{
				object obj = base.ViewState["DbValueFactor"];
				if (obj == null)
				{
					obj = 1.0;
				}
				return (double)obj;
			}
			set
			{
				base.ViewState["DbValueFactor"] = value;
			}
		}

		// Token: 0x17004DA5 RID: 19877
		// (get) Token: 0x06010167 RID: 65895 RVA: 0x0039DFBC File Offset: 0x0039C1BC
		// (set) Token: 0x06010168 RID: 65896 RVA: 0x0039DFEA File Offset: 0x0039C1EA
		[DefaultValue(typeof(NumericType), "Number")]
		[NotifyParentProperty(true)]
		public NumericType NumericType
		{
			get
			{
				object obj = base.ViewState["NumericType"];
				if (obj == null)
				{
					obj = NumericType.Number;
				}
				return (NumericType)obj;
			}
			set
			{
				base.ViewState["NumericType"] = value;
			}
		}

		// Token: 0x17004DA6 RID: 19878
		// (get) Token: 0x06010169 RID: 65897 RVA: 0x0039E004 File Offset: 0x0039C204
		// (set) Token: 0x0601016A RID: 65898 RVA: 0x0039E032 File Offset: 0x0039C232
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool AllowRounding
		{
			get
			{
				object obj = base.ViewState["AllowRounding"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["AllowRounding"] = value;
			}
		}

		// Token: 0x17004DA7 RID: 19879
		// (get) Token: 0x0601016B RID: 65899 RVA: 0x0039E04C File Offset: 0x0039C24C
		// (set) Token: 0x0601016C RID: 65900 RVA: 0x0039E07A File Offset: 0x0039C27A
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool KeepNotRoundedValue
		{
			get
			{
				object obj = base.ViewState["KeepNotRoundedValue"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["KeepNotRoundedValue"] = value;
			}
		}

		// Token: 0x17004DA8 RID: 19880
		// (get) Token: 0x0601016D RID: 65901 RVA: 0x0039E094 File Offset: 0x0039C294
		// (set) Token: 0x0601016E RID: 65902 RVA: 0x0039E103 File Offset: 0x0039C303
		[NotifyParentProperty(true)]
		public int DecimalDigits
		{
			get
			{
				object obj = base.ViewState["DecimalDigits"];
				if (obj != null)
				{
					return (int)obj;
				}
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				switch (this.NumericType)
				{
				case NumericType.Currency:
					return currentCulture.NumberFormat.CurrencyDecimalDigits;
				case NumericType.Percent:
					return currentCulture.NumberFormat.PercentDecimalDigits;
				default:
					return currentCulture.NumberFormat.NumberDecimalDigits;
				}
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("DecimalDigits", "Valid values are between 0 and 99, inclusive.");
				}
				base.ViewState["DecimalDigits"] = value;
			}
		}

		// Token: 0x17004DA9 RID: 19881
		// (get) Token: 0x0601016F RID: 65903 RVA: 0x0039E134 File Offset: 0x0039C334
		// (set) Token: 0x06010170 RID: 65904 RVA: 0x0039E167 File Offset: 0x0039C367
		[NotifyParentProperty(true)]
		[Description("Gets or sets the largest possible value of a GridNumericColumn.")]
		[DefaultValue(70368744177664.0)]
		[Category("Behavior")]
		public virtual double MaxValue
		{
			get
			{
				if (base.ViewState["MaxValue"] == null)
				{
					return 70368744177664.0;
				}
				return (double)base.ViewState["MaxValue"];
			}
			set
			{
				base.ViewState["MaxValue"] = value;
			}
		}

		// Token: 0x17004DAA RID: 19882
		// (get) Token: 0x06010171 RID: 65905 RVA: 0x0039E17F File Offset: 0x0039C37F
		// (set) Token: 0x06010172 RID: 65906 RVA: 0x0039E1B2 File Offset: 0x0039C3B2
		[DefaultValue(-70368744177664.0)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the smallest possible value of a GridNumericColumn.")]
		public virtual double MinValue
		{
			get
			{
				if (base.ViewState["MinValue"] == null)
				{
					return -70368744177664.0;
				}
				return (double)base.ViewState["MinValue"];
			}
			set
			{
				base.ViewState["MinValue"] = value;
			}
		}

		// Token: 0x17004DAB RID: 19883
		// (get) Token: 0x06010173 RID: 65907 RVA: 0x0039E1CA File Offset: 0x0039C3CA
		// (set) Token: 0x06010174 RID: 65908 RVA: 0x0039E1F5 File Offset: 0x0039C3F5
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Whether the button is displayed")]
		public virtual bool ShowSpinButtons
		{
			get
			{
				return base.ViewState["ShowSpinButtons"] != null && (bool)base.ViewState["ShowSpinButtons"];
			}
			set
			{
				base.ViewState["ShowSpinButtons"] = value;
			}
		}

		// Token: 0x17004DAC RID: 19884
		// (get) Token: 0x06010175 RID: 65909 RVA: 0x0039E20D File Offset: 0x0039C40D
		// (set) Token: 0x06010176 RID: 65910 RVA: 0x0039E238 File Offset: 0x0039C438
		[Description("Gets or sets whether the GridNumericColumn should autocorrect out of range values to valid values")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public bool AllowOutOfRangeAutoCorrect
		{
			get
			{
				return base.ViewState["AllowOutOfRangeAutoCorrect"] == null || (bool)base.ViewState["AllowOutOfRangeAutoCorrect"];
			}
			set
			{
				base.ViewState["AllowOutOfRangeAutoCorrect"] = value;
			}
		}

		// Token: 0x17004DAD RID: 19885
		// (get) Token: 0x06010177 RID: 65911 RVA: 0x0039E250 File Offset: 0x0039C450
		// (set) Token: 0x06010178 RID: 65912 RVA: 0x0039E282 File Offset: 0x0039C482
		[DefaultValue(typeof(double))]
		[NotifyParentProperty(true)]
		public Type NumericDataType
		{
			get
			{
				object obj = base.ViewState["NumericDataType"];
				if (obj == null)
				{
					obj = typeof(double);
				}
				return (Type)obj;
			}
			set
			{
				base.ViewState["NumericDataType"] = value;
			}
		}

		// Token: 0x17004DAE RID: 19886
		// (get) Token: 0x06010179 RID: 65913 RVA: 0x0039E295 File Offset: 0x0039C495
		// (set) Token: 0x0601017A RID: 65914 RVA: 0x0039E29D File Offset: 0x0039C49D
		[DefaultValue(false)]
		internal bool IsBetweenFilter { get; set; }

		// Token: 0x0601017B RID: 65915 RVA: 0x0039E2A6 File Offset: 0x0039C4A6
		protected override string FormatDataValue(object dataValue, GridItem item)
		{
			return this.FormatDataValue(dataValue, item, false);
		}

		// Token: 0x0601017C RID: 65916 RVA: 0x0039E2B4 File Offset: 0x0039C4B4
		protected override string FormatDataValue(object dataValue, GridItem item, bool formatEvenIfReadOnly)
		{
			if (!formatEvenIfReadOnly && item.IsInEditMode && base.IsReadOnly(item) && dataValue != null && dataValue != DBNull.Value)
			{
				return dataValue.ToString();
			}
			if (this.NumericType == NumericType.Currency && string.IsNullOrEmpty(this.DataFormatString))
			{
				this.formatting = "{0:c2}";
			}
			return base.FormatDataValue(dataValue, item, formatEvenIfReadOnly);
		}
	}
}
