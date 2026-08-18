using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;

namespace Telerik.Web.UI
{
	// Token: 0x020010B3 RID: 4275
	public class GridDateTimeColumn : GridBoundColumn
	{
		// Token: 0x0600AE2A RID: 44586 RVA: 0x002589B1 File Offset: 0x00256BB1
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public GridDateTimeColumn()
		{
			this.CurrentFilterFunction = GridKnownFunction.NoFilter;
			this.FilterListOptions = GridFilterListOptions.VaryByDataType;
		}

		// Token: 0x0600AE2B RID: 44587 RVA: 0x002589C8 File Offset: 0x00256BC8
		protected override ArrayList GetFilterFunctionsList(GridFilterListOptions options, ArrayList sourceList)
		{
			sourceList = base.GetFilterFunctionsList(options, sourceList);
			if (this.EnableRangeFiltering)
			{
				sourceList.Remove(GridKnownFunction.Contains);
				sourceList.Remove(GridKnownFunction.Custom);
				sourceList.Remove(GridKnownFunction.DoesNotContain);
				sourceList.Remove(GridKnownFunction.EndsWith);
				sourceList.Remove(GridKnownFunction.EqualTo);
				sourceList.Remove(GridKnownFunction.GreaterThan);
				sourceList.Remove(GridKnownFunction.GreaterThanOrEqualTo);
				sourceList.Remove(GridKnownFunction.IsEmpty);
				sourceList.Remove(GridKnownFunction.LessThan);
				sourceList.Remove(GridKnownFunction.LessThanOrEqualTo);
				sourceList.Remove(GridKnownFunction.NotEqualTo);
				sourceList.Remove(GridKnownFunction.NotIsEmpty);
				sourceList.Remove(GridKnownFunction.StartsWith);
			}
			else
			{
				if (options == GridFilterListOptions.VaryByDataType)
				{
					sourceList.Remove(GridKnownFunction.Custom.ToString());
					if (base.DataType == typeof(DateTime))
					{
						sourceList.Remove(GridKnownFunction.Between.ToString());
						sourceList.Remove(GridKnownFunction.NotBetween.ToString());
					}
					return sourceList;
				}
				if (options == GridFilterListOptions.VaryByDataTypeAllowCustom && base.DataType == typeof(DateTime))
				{
					sourceList.Remove(GridKnownFunction.Between.ToString());
					sourceList.Remove(GridKnownFunction.NotBetween.ToString());
				}
			}
			return sourceList;
		}

		// Token: 0x0600AE2C RID: 44588 RVA: 0x00258B24 File Offset: 0x00256D24
		public override GridColumn Clone()
		{
			GridDateTimeColumn gridDateTimeColumn = new GridDateTimeColumn();
			gridDateTimeColumn.CopyBaseProperties(this);
			return gridDateTimeColumn;
		}

		// Token: 0x0600AE2D RID: 44589 RVA: 0x00258B40 File Offset: 0x00256D40
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridDateTimeColumn gridDateTimeColumn = (GridDateTimeColumn)fromColumn;
			this.PickerType = gridDateTimeColumn.PickerType;
			this.MaxDate = gridDateTimeColumn.MaxDate;
			this.MinDate = gridDateTimeColumn.MinDate;
			this.EditDataFormatString = gridDateTimeColumn.EditDataFormatString;
			this.FilterDateFormat = gridDateTimeColumn.FilterDateFormat;
			this.EnableRangeFiltering = gridDateTimeColumn.EnableRangeFiltering;
			this.EnableTimeIndependentFiltering = gridDateTimeColumn.EnableTimeIndependentFiltering;
		}

		// Token: 0x0600AE2E RID: 44590 RVA: 0x00258BAF File Offset: 0x00256DAF
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				return new GridMobileDateTimeColumnEditor(this);
			}
			return new GridDateTimeColumnEditor(this);
		}

		// Token: 0x0600AE2F RID: 44591 RVA: 0x00258BD9 File Offset: 0x00256DD9
		private void SetAltFilterText(RadDateInput filterControl)
		{
			filterControl.Attributes["alt"] = this.FilterControlAltText;
		}

		// Token: 0x0600AE30 RID: 44592 RVA: 0x00258BF4 File Offset: 0x00256DF4
		protected override void SetupFilterControls(TableCell cell)
		{
			if (this.FilterTemplate != null)
			{
				this.FilterTemplate.InstantiateIn(cell);
				return;
			}
			if (this.EnableRangeFiltering && this.FilterControlWidth.IsEmpty)
			{
				this.FilterControlWidth = 120;
			}
			this.CreateControl(cell, GridDateTimeColumn._defaultRadDatePickerId, GridDateTimeColumn._defaultRadDateInputId);
			if (this.EnableRangeFiltering)
			{
				cell.Controls.AddAt(0, new LiteralControl
				{
					Text = base.Owner.OwnerGrid.Localization.RangeFilteringFromText
				});
				cell.Controls.Add(new LiteralControl
				{
					Text = base.Owner.OwnerGrid.Localization.RangeFilteringToText
				});
				this.CreateControl(cell, GridDateTimeColumn._rangeRadDatePickerId, GridDateTimeColumn._rangeRadDateInputId);
				this.SetRangeFilteringClientEvents(cell, GridDateTimeColumn._defaultRadDatePickerId, GridDateTimeColumn._defaultRadDateInputId);
				this.SetRangeFilteringClientEvents(cell, GridDateTimeColumn._rangeRadDatePickerId, GridDateTimeColumn._rangeRadDateInputId);
			}
			if (this.ShowFilterIcon)
			{
				if (base.Owner.OwnerGrid.ShouldRenderImg(this.FilterImageUrl))
				{
					Image image = new Image();
					image.ImageUrl = this.FilterImageUrl;
					image.AlternateText = this.FilterImageToolTip;
					image.ToolTip = this.FilterImageToolTip;
					image.BorderWidth = Unit.Pixel(0);
					image.ID = string.Format("Filter_{0}", this.UniqueName);
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
					if (base.Owner.OwnerGrid.EnableAriaSupport)
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

		// Token: 0x0600AE31 RID: 44593 RVA: 0x00258E68 File Offset: 0x00257068
		private void CreateControl(TableCell cell, string radDatePickerId, string radDateInputId)
		{
			RadDatePicker radDatePicker = GridDateTimeColumnHelper.InstantiatePickerFactory(this.PickerType);
			radDatePicker.ID = string.Format("{0}{1}", radDatePickerId, this.UniqueName);
			radDatePicker.RenderMode = base.Owner.OwnerGrid.RenderMode;
			radDatePicker.DateInput.ToolTip = this.FilterControlToolTip;
			cell.Controls.Add(radDatePicker);
			this.SetAltFilterText(radDatePicker.DateInput);
			radDatePicker.Visible = false;
			radDatePicker.EnableEmbeddedSkins = base.Owner.OwnerGrid.EnableEmbeddedSkins;
			radDatePicker.EnableAriaSupport = base.Owner.OwnerGrid.EnableAriaSupport;
			if (radDatePicker.EnableAriaSupport)
			{
				radDatePicker.DatePopupButton.Attributes.Add("aria-label", radDatePicker.DatePopupButton.ToolTip);
			}
			radDatePicker.PreRender += this.SkinnableControl_PreRender;
			if (!this.FilterControlWidth.IsEmpty)
			{
				radDatePicker.Width = this.FilterControlWidth;
			}
			else
			{
				radDatePicker.Width = Unit.Percentage(85.0);
			}
			RadDateInput radDateInput = new RadDateInput();
			radDateInput.ID = string.Format("{0}{1}", radDateInputId, this.UniqueName);
			radDateInput.RenderMode = base.Owner.OwnerGrid.RenderMode;
			radDateInput.ToolTip = this.FilterControlToolTip;
			cell.Controls.Add(radDateInput);
			this.SetAltFilterText(radDateInput);
			radDateInput.EnableEmbeddedSkins = base.Owner.OwnerGrid.EnableEmbeddedSkins;
			radDateInput.EnableAriaSupport = base.Owner.OwnerGrid.EnableAriaSupport;
			radDateInput.PreRender += this.SkinnableControl_PreRender;
			radDateInput.Visible = false;
			if (!this.FilterControlWidth.IsEmpty)
			{
				radDateInput.Width = this.FilterControlWidth;
			}
			else
			{
				radDateInput.Width = Unit.Percentage(90.0);
			}
			radDateInput.MinDate = this.MinDate;
			radDateInput.MaxDate = this.MaxDate;
			if (this.PickerType != GridDateTimeColumnPickerType.None)
			{
				radDatePicker.Visible = true;
				radDatePicker.MinDate = this.MinDate;
				radDatePicker.MaxDate = this.MaxDate;
				this.ConfigureDataInputPostbackFilter(radDatePicker.DateInput);
				if (this.PickerType == GridDateTimeColumnPickerType.TimePicker)
				{
					RadTimePicker radTimePicker = radDatePicker as RadTimePicker;
					radTimePicker.TimeView.TimeFormat = this.GetDateTimeFormat(radTimePicker.TimeView.TimeFormat);
					radTimePicker.SharedTimeView = this.GetSharedTimeView();
					if (base.DataTypeIsSet && base.DataType == typeof(TimeSpan))
					{
						radTimePicker.UseTimeSpanForBinding = true;
					}
				}
				if (this.PickerType == GridDateTimeColumnPickerType.DateTimePicker)
				{
					RadDateTimePicker radDateTimePicker = radDatePicker as RadDateTimePicker;
					radDateTimePicker.TimeView.TimeFormat = this.GetDateTimeFormat(radDateTimePicker.TimeView.TimeFormat);
					radDateTimePicker.SharedTimeView = this.GetSharedTimeView();
				}
				else
				{
					radDatePicker.DateInput.DateFormat = this.GetDateTimeFormat(radDatePicker.DateInput.DateFormat);
				}
				if (!string.IsNullOrEmpty(this.FilterDateFormat))
				{
					radDatePicker.DateInput.DateFormat = this.FilterDateFormat;
					radDatePicker.DateInput.DisplayDateFormat = this.FilterDateFormat;
				}
				radDatePicker.SharedCalendar = this.GetSharedCalendar();
				return;
			}
			radDateInput.Visible = true;
			this.ConfigureDataInputPostbackFilter(radDateInput);
			if (string.IsNullOrEmpty(this.FilterDateFormat))
			{
				radDateInput.DateFormat = this.GetDateTimeFormat(radDateInput.DateFormat);
			}
			else
			{
				radDateInput.DateFormat = this.FilterDateFormat;
				radDateInput.DisplayDateFormat = this.FilterDateFormat;
			}
			if (!this.FilterControlWidth.IsEmpty)
			{
				radDateInput.Width = this.FilterControlWidth;
				return;
			}
			radDateInput.Width = Unit.Percentage(80.0);
		}

		// Token: 0x0600AE32 RID: 44594 RVA: 0x002591F8 File Offset: 0x002573F8
		private void ConfigureDataInputPostbackFilter(RadDateInput radDateInput)
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
			{
				return;
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
			string arg = string.Format("$find(\"{0}\")._handleAutoPostBackOnFilterWithDelay(event,\"{1}\",\"{2}\",{3}); ", new object[]
			{
				base.Owner.ClientID,
				radDateInput.ClientID,
				this.UniqueName,
				filterDelay
			});
			string text = string.Format(" $find(\"{0}\")._handleAutoPostBackOnFilterWithoutDelay(\"{1}\",\"{2}\",{3}); ", new object[]
			{
				base.Owner.ClientID,
				radDateInput.ClientID,
				this.UniqueName,
				0
			});
			if (this.AutoPostBackOnFilter)
			{
				radDateInput.ClientEvents.OnValueChanged = "function(sender, args){" + text + "}";
			}
			if (this.AutoPostBackOnFilter)
			{
				radDateInput.Attributes["onkeypress"] = string.Format("if(event.keyCode == 13){{ this.blur(); event.cancelBubble = true; event.returnValue = false; if (event.stopPropagation){{ event.stopPropagation(); event.preventDefault();}} {0} }}", text);
			}
			else
			{
				radDateInput.ClientEvents.OnKeyPress = "Telerik.Web.UI.RadInputControl.CancelRawEventOnEnterKey";
			}
			if (this.FilterDelay != null)
			{
				radDateInput.Attributes["onkeydown"] = string.Format("{0}", arg);
			}
		}

		// Token: 0x0600AE33 RID: 44595 RVA: 0x00259350 File Offset: 0x00257550
		private void SetRangeFilteringClientEvents(TableCell cell, string radDatePickerId, string radDateInputId)
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
			{
				return;
			}
			Control filterControlFromCell = this.GetFilterControlFromCell(cell, radDatePickerId, radDateInputId);
			RadDateInput dateInputFromControl = GridDateTimeColumnHelper.GetDateInputFromControl(filterControlFromCell);
			string str = string.Format("$find(\"{0}\")._rangeValidationHandler(sender, args, '{1}')", base.Owner.ClientID, this.UniqueName);
			string text = "function(sender, args){" + str + "}";
			dateInputFromControl.ClientEvents.OnValueChanging = text;
			RadDatePicker radDatePicker = filterControlFromCell as RadDatePicker;
			if (radDatePicker != null)
			{
				radDatePicker.ClientEvents.OnPopupOpening = text;
				radDatePicker.ClientEvents.OnPopupClosing = text;
			}
		}

		// Token: 0x0600AE34 RID: 44596 RVA: 0x002593E4 File Offset: 0x002575E4
		private DateTime? GetDateTimeForRangeFiltering(Control control, string value)
		{
			RadDateInput dateInputFromControl = GridDateTimeColumnHelper.GetDateInputFromControl(control);
			string format = dateInputFromControl.DateFormat.Replace(' ', ',');
			DateTime value2;
			if (DateTime.TryParseExact(value, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out value2))
			{
				return new DateTime?(value2);
			}
			if (DateTime.TryParse(value, out value2))
			{
				return new DateTime?(value2);
			}
			return null;
		}

		// Token: 0x0600AE35 RID: 44597 RVA: 0x0025943A File Offset: 0x0025763A
		private void SkinnableControl_PreRender(object sender, EventArgs e)
		{
			((ISkinnableControl)sender).Skin = base.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x0600AE36 RID: 44598 RVA: 0x00259458 File Offset: 0x00257658
		private void radDatePicker_SelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
		{
			base.Owner.OwnerGrid.EditIndexes.Clear();
			GridFilteringItem gridFilteringItem = (GridFilteringItem)((RadDatePicker)sender).Parent.Parent;
			gridFilteringItem.FireCommandEvent("Filter", new Pair(this.CurrentFilterFunction.ToString(), this.UniqueName));
		}

		// Token: 0x0600AE37 RID: 44599 RVA: 0x002594B8 File Offset: 0x002576B8
		protected override void SetCurrentFilterValueToControl(TableCell cell)
		{
			base.SetCurrentFilterValueToControl(cell);
			if (!string.IsNullOrEmpty(this.CurrentFilterValue))
			{
				if (this.EnableRangeFiltering && this.FilterTemplate == null)
				{
					Control filterControlFromCell = this.GetFilterControlFromCell(cell, GridDateTimeColumn._defaultRadDatePickerId, GridDateTimeColumn._defaultRadDateInputId);
					Control filterControlFromCell2 = this.GetFilterControlFromCell(cell, GridDateTimeColumn._rangeRadDatePickerId, GridDateTimeColumn._rangeRadDateInputId);
					if (filterControlFromCell != null && filterControlFromCell2 != null)
					{
						string[] array = this.CurrentFilterValue.Split(new char[]
						{
							' '
						});
						if (array.Length != 2)
						{
							return;
						}
						DateTime? dateTimeForRangeFiltering = this.GetDateTimeForRangeFiltering(filterControlFromCell, array[0]);
						DateTime? dateTimeForRangeFiltering2 = this.GetDateTimeForRangeFiltering(filterControlFromCell2, array[1]);
						if (dateTimeForRangeFiltering == null || dateTimeForRangeFiltering2 == null)
						{
							return;
						}
						GridDateTimeColumnHelper.SetDataInputControlValue(filterControlFromCell, this.PickerType, dateTimeForRangeFiltering.Value.ToString());
						GridDateTimeColumnHelper.SetDataInputControlValue(filterControlFromCell2, this.PickerType, dateTimeForRangeFiltering2.Value.ToString());
						return;
					}
				}
				else if (this.EnableTimeIndependentFiltering && this.FilterTemplate == null)
				{
					Control filterControlFromCell3 = this.GetFilterControlFromCell(cell, GridDateTimeColumn._defaultRadDatePickerId, GridDateTimeColumn._defaultRadDateInputId);
					if (filterControlFromCell3 != null)
					{
						DateTime? dateTimeForRangeFiltering3 = this.GetDateTimeForRangeFiltering(filterControlFromCell3, this.CurrentFilterValue);
						if (dateTimeForRangeFiltering3 == null)
						{
							return;
						}
						if (this.FilterTemplate != null)
						{
							GridDateTimeColumnHelper.SetFilterTemplateValue(filterControlFromCell3, this.CurrentFilterValue);
							return;
						}
						GridDateTimeColumnHelper.SetDataInputControlValue(filterControlFromCell3, this.PickerType, dateTimeForRangeFiltering3.Value.ToString());
						return;
					}
				}
				else if (this.IsBetweenFilter || this.CurrentFilterFunction == GridKnownFunction.Between || this.CurrentFilterFunction == GridKnownFunction.NotBetween)
				{
					List<Control> filterControls = this.GetFilterControls(cell);
					if (filterControls.Count == 2)
					{
						string[] array2 = new string[2];
						array2 = this.CurrentFilterValue.Split(new char[]
						{
							' '
						}, 2);
						GridDateTimeColumnHelper.SetFilterTemplateValue(filterControls[0], array2[0]);
						GridDateTimeColumnHelper.SetFilterTemplateValue(filterControls[1], array2[1]);
						return;
					}
				}
				else
				{
					Control filterControlFromCell4 = this.GetFilterControlFromCell(cell, GridDateTimeColumn._defaultRadDatePickerId, GridDateTimeColumn._defaultRadDateInputId);
					if (filterControlFromCell4 != null)
					{
						if (this.FilterTemplate != null)
						{
							GridDateTimeColumnHelper.SetFilterTemplateValue(filterControlFromCell4, this.CurrentFilterValue);
							return;
						}
						GridDateTimeColumnHelper.SetDataInputControlValue(filterControlFromCell4, this.PickerType, this.CurrentFilterValue);
					}
				}
			}
		}

		// Token: 0x0600AE38 RID: 44600 RVA: 0x002596F0 File Offset: 0x002578F0
		protected List<Control> GetFilterControls(TableCell cell)
		{
			List<Control> list = new List<Control>();
			foreach (object obj in cell.Controls)
			{
				Control control = (Control)obj;
				if (control is RadDateTimePicker || control is RadTimePicker || control is RadDateInput || control is RadDatePicker)
				{
					list.Add(control);
					if (list.Count == 2)
					{
						return list;
					}
				}
			}
			return list;
		}

		// Token: 0x0600AE39 RID: 44601 RVA: 0x00259784 File Offset: 0x00257984
		protected Control GetFilterControlFromCell(TableCell cell, string radDatePickerId, string radDateInputId)
		{
			Control control = cell.FindControl(string.Format("{0}{1}", radDateInputId, this.UniqueName));
			if (this.PickerType != GridDateTimeColumnPickerType.None)
			{
				control = cell.FindControl(string.Format("{0}{1}", radDatePickerId, this.UniqueName));
			}
			if (control == null)
			{
				foreach (object obj in cell.Controls)
				{
					Control control2 = (Control)obj;
					if (control2 is RadDateTimePicker || control2 is RadTimePicker || control2 is RadDateInput || control2 is RadDatePicker)
					{
						return control2;
					}
				}
				return control;
			}
			return control;
		}

		// Token: 0x0600AE3A RID: 44602 RVA: 0x0025983C File Offset: 0x00257A3C
		protected override string GetCurrentFilterValueFromControl(TableCell cell)
		{
			object obj = null;
			if (this.EnableRangeFiltering && this.FilterTemplate == null)
			{
				Control filterControlFromCell = this.GetFilterControlFromCell(cell, GridDateTimeColumn._defaultRadDatePickerId, GridDateTimeColumn._defaultRadDateInputId);
				Control filterControlFromCell2 = this.GetFilterControlFromCell(cell, GridDateTimeColumn._rangeRadDatePickerId, GridDateTimeColumn._rangeRadDateInputId);
				object dataInputControlValue = GridDateTimeColumnHelper.GetDataInputControlValue(filterControlFromCell, this.PickerType);
				object dataInputControlValue2 = GridDateTimeColumnHelper.GetDataInputControlValue(filterControlFromCell2, this.PickerType);
				if (dataInputControlValue != null && dataInputControlValue2 != null)
				{
					obj = string.Format("{0} {1}", dataInputControlValue.ToString().Replace(' ', ','), dataInputControlValue2.ToString().Replace(' ', ','));
				}
			}
			else if (this.IsBetweenFilter)
			{
				List<Control> filterControls = this.GetFilterControls(cell);
				if (filterControls.Count == 2)
				{
					obj = GridDateTimeColumnHelper.GetFilterTemplateValue(filterControls[0]).ToString() + " " + GridDateTimeColumnHelper.GetFilterTemplateValue(filterControls[1]).ToString();
				}
			}
			else
			{
				Control filterControlFromCell3 = this.GetFilterControlFromCell(cell, GridDateTimeColumn._defaultRadDatePickerId, GridDateTimeColumn._defaultRadDateInputId);
				if (filterControlFromCell3 == null)
				{
					return string.Empty;
				}
				if (this.FilterTemplate == null)
				{
					obj = GridDateTimeColumnHelper.GetDataInputControlValue(filterControlFromCell3, this.PickerType);
				}
				else
				{
					obj = GridDateTimeColumnHelper.GetFilterTemplateValue(filterControlFromCell3);
				}
			}
			if (this.EnableTimeIndependentFiltering && obj != null && this.filterExpressionEvaluationInProcess)
			{
				DateTime dateTime;
				if (DateTime.TryParse(obj.ToString(), out dateTime))
				{
					obj = GridDateTimeColumnHelper.GetTimeIndependentFilterValue(this.CurrentFilterFunction, obj);
				}
				else
				{
					string[] array = obj.ToString().Split(new char[]
					{
						' '
					});
					if (array.Length >= 2)
					{
						object timeIndependentFilterValue = GridDateTimeColumnHelper.GetTimeIndependentFilterValue(GridKnownFunction.GreaterThanOrEqualTo, array[0]);
						object timeIndependentFilterValue2 = GridDateTimeColumnHelper.GetTimeIndependentFilterValue(GridKnownFunction.LessThanOrEqualTo, array[1]);
						obj = string.Format("{0} {1}", timeIndependentFilterValue.ToString().Replace(' ', ','), timeIndependentFilterValue2.ToString().Replace(' ', ','));
					}
				}
			}
			if (obj == null || obj == DBNull.Value)
			{
				return string.Empty;
			}
			return obj.ToString();
		}

		// Token: 0x0600AE3B RID: 44603 RVA: 0x00259A1C File Offset: 0x00257C1C
		public override string EvaluateFilterExpression()
		{
			return this.EvaluateFilterExpression(null);
		}

		// Token: 0x0600AE3C RID: 44604 RVA: 0x00259A28 File Offset: 0x00257C28
		public override string EvaluateFilterExpression(GridFilteringItem filteringItem)
		{
			this.filterExpressionEvaluationInProcess = true;
			DateTime dateTime;
			DateTime dateTime2;
			if (this.EnableRangeFiltering && this.EnableTimeIndependentFiltering && filteringItem == null && DateTime.TryParse(this.CurrentFilterValue, out dateTime) && DateTime.TryParse(this.AndCurrentFilterValue, out dateTime2))
			{
				string currentFilterValue = this.CurrentFilterValue;
				string andCurrentFilterValue = this.AndCurrentFilterValue;
				this.CurrentFilterValue = GridDateTimeColumnHelper.GetTimeIndependentFilterValue(this.CurrentFilterFunction, this.CurrentFilterValue).ToString();
				this.AndCurrentFilterValue = GridDateTimeColumnHelper.GetTimeIndependentFilterValue(this.AndCurrentFilterFunction, this.AndCurrentFilterValue).ToString();
				string result = base.EvaluateFilterExpression();
				this.CurrentFilterValue = currentFilterValue;
				this.AndCurrentFilterValue = andCurrentFilterValue;
				return result;
			}
			if (this.EnableTimeIndependentFiltering)
			{
				GridKnownFunction currentFilterFunction = this.CurrentFilterFunction;
				this.CurrentFilterFunction = GridDateTimeColumnHelper.GetCorrespondingTimeIndependentFilterFunction(this.CurrentFilterFunction);
				string result2;
				if (filteringItem == null)
				{
					string currentFilterValue2 = this.CurrentFilterValue;
					this.CurrentFilterValue = GridDateTimeColumnHelper.GetTimeIndependentFilterValue(this.CurrentFilterFunction, this.CurrentFilterValue).ToString();
					result2 = base.EvaluateFilterExpression();
					this.CurrentFilterValue = currentFilterValue2;
				}
				else
				{
					result2 = base.EvaluateFilterExpression(filteringItem);
				}
				this.CurrentFilterFunction = currentFilterFunction;
				return result2;
			}
			if (filteringItem == null)
			{
				return base.EvaluateFilterExpression();
			}
			this.filterExpressionEvaluationInProcess = false;
			return base.EvaluateFilterExpression(filteringItem);
		}

		// Token: 0x0600AE3D RID: 44605 RVA: 0x00259B58 File Offset: 0x00257D58
		internal string GetDateTimeFormat(string format)
		{
			if (!string.IsNullOrEmpty(this.DataFormatString))
			{
				return format;
			}
			if (this.PickerType == GridDateTimeColumnPickerType.DatePicker)
			{
				return "d";
			}
			if (this.PickerType == GridDateTimeColumnPickerType.TimePicker)
			{
				return "t";
			}
			return "G";
		}

		// Token: 0x17003848 RID: 14408
		// (get) Token: 0x0600AE3E RID: 44606 RVA: 0x00259B8C File Offset: 0x00257D8C
		// (set) Token: 0x0600AE3F RID: 44607 RVA: 0x00259BAC File Offset: 0x00257DAC
		[DefaultValue("")]
		[Localizable(true)]
		[Description("GridBoundColumn_EditDataFormatString")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual string EditDataFormatString
		{
			get
			{
				return (string)(base.ViewState["EditDataFormatString"] ?? string.Empty);
			}
			set
			{
				base.ViewState["EditDataFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003849 RID: 14409
		// (get) Token: 0x0600AE40 RID: 44608 RVA: 0x00259BC8 File Offset: 0x00257DC8
		// (set) Token: 0x0600AE41 RID: 44609 RVA: 0x00259BF6 File Offset: 0x00257DF6
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridDateTimeColumnPickerType), "DatePicker")]
		public GridDateTimeColumnPickerType PickerType
		{
			get
			{
				object obj = base.ViewState["PickerType"];
				if (obj == null)
				{
					obj = GridDateTimeColumnPickerType.DatePicker;
				}
				return (GridDateTimeColumnPickerType)obj;
			}
			set
			{
				base.ViewState["PickerType"] = value;
			}
		}

		// Token: 0x1700384A RID: 14410
		// (get) Token: 0x0600AE42 RID: 44610 RVA: 0x00259C10 File Offset: 0x00257E10
		// (set) Token: 0x0600AE43 RID: 44611 RVA: 0x00259C42 File Offset: 0x00257E42
		[DefaultValue(typeof(DateTime), "1/1/1900")]
		[NotifyParentProperty(true)]
		public DateTime MinDate
		{
			get
			{
				object obj = base.ViewState["MinDate"] ?? GridDateTimeColumnHelper.DefaultMinDateTimeValue;
				return (DateTime)obj;
			}
			set
			{
				base.ViewState["MinDate"] = value;
			}
		}

		// Token: 0x1700384B RID: 14411
		// (get) Token: 0x0600AE44 RID: 44612 RVA: 0x00259C5C File Offset: 0x00257E5C
		// (set) Token: 0x0600AE45 RID: 44613 RVA: 0x00259C8E File Offset: 0x00257E8E
		[DefaultValue(typeof(DateTime), "12/31/2099")]
		[NotifyParentProperty(true)]
		public DateTime MaxDate
		{
			get
			{
				object obj = base.ViewState["MaxDate"] ?? GridDateTimeColumnHelper.DefaultMaxDateTimeValue;
				return (DateTime)obj;
			}
			set
			{
				base.ViewState["MaxDate"] = value;
			}
		}

		// Token: 0x1700384C RID: 14412
		// (get) Token: 0x0600AE46 RID: 44614 RVA: 0x00259CA8 File Offset: 0x00257EA8
		// (set) Token: 0x0600AE47 RID: 44615 RVA: 0x00259CD5 File Offset: 0x00257ED5
		[DefaultValue("")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[Description("GridBoundColumn_FilterDateFormat")]
		public virtual string FilterDateFormat
		{
			get
			{
				object obj = base.ViewState["FilterDateFormat"] ?? string.Empty;
				return (string)obj;
			}
			set
			{
				base.ViewState["FilterDateFormat"] = value;
			}
		}

		// Token: 0x1700384D RID: 14413
		// (get) Token: 0x0600AE48 RID: 44616 RVA: 0x00259CE8 File Offset: 0x00257EE8
		// (set) Token: 0x0600AE49 RID: 44617 RVA: 0x00259D16 File Offset: 0x00257F16
		[Description("GridBoundColumn_EnableRanageFiltering")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool EnableRangeFiltering
		{
			get
			{
				object obj = base.ViewState["EnableRangeFiltering"] ?? false;
				return (bool)obj;
			}
			set
			{
				base.ViewState["EnableRangeFiltering"] = value;
			}
		}

		// Token: 0x1700384E RID: 14414
		// (get) Token: 0x0600AE4A RID: 44618 RVA: 0x00259D30 File Offset: 0x00257F30
		// (set) Token: 0x0600AE4B RID: 44619 RVA: 0x00259D5E File Offset: 0x00257F5E
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("GridBoundColumn_EnableTimeIndependentFiltering")]
		[DefaultValue(false)]
		public virtual bool EnableTimeIndependentFiltering
		{
			get
			{
				object obj = base.ViewState["EnableTimeIndependentFiltering"] ?? false;
				return (bool)obj;
			}
			set
			{
				base.ViewState["EnableTimeIndependentFiltering"] = value;
			}
		}

		// Token: 0x0600AE4C RID: 44620 RVA: 0x00259D78 File Offset: 0x00257F78
		internal RadTimeView GetSharedTimeView()
		{
			RadTimeView radTimeView = base.Owner.OwnerGrid.FindControl(GridDateTimeColumn._sharedTimeViewName) as RadTimeView;
			if (radTimeView == null)
			{
				Panel panel = new Panel();
				panel.ID = "SharedTimeViewContainer";
				base.Owner.OwnerGrid.Controls.Add(panel);
				radTimeView = new RadTimeView();
				radTimeView.ID = GridDateTimeColumn._sharedTimeViewName;
				radTimeView.RenderMode = base.Owner.OwnerGrid.RenderMode;
				panel.Controls.Add(radTimeView);
				radTimeView.EnableEmbeddedSkins = base.Owner.OwnerGrid.EnableEmbeddedSkins;
				radTimeView.EnableAriaSupport = base.Owner.OwnerGrid.EnableAriaSupport;
				radTimeView.PreRender += this.sharedTimeView_PreRender;
				panel.Style["display"] = "none";
				radTimeView.Visible = !base.DesignMode;
			}
			return radTimeView;
		}

		// Token: 0x0600AE4D RID: 44621 RVA: 0x00259E63 File Offset: 0x00258063
		private void sharedTimeView_PreRender(object sender, EventArgs e)
		{
			((RadTimeView)sender).Skin = base.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x0600AE4E RID: 44622 RVA: 0x00259E80 File Offset: 0x00258080
		internal RadCalendar GetSharedCalendar()
		{
			RadCalendar radCalendar = base.Owner.OwnerGrid.FindControl(GridDateTimeColumn._sharedCalendarName) as RadCalendar;
			if (radCalendar != null)
			{
				radCalendar.RangeMinDate = this.MinDate;
				radCalendar.RangeMaxDate = this.MaxDate;
				radCalendar.Visible = true;
			}
			return radCalendar;
		}

		// Token: 0x1700384F RID: 14415
		// (get) Token: 0x0600AE4F RID: 44623 RVA: 0x00259ECB File Offset: 0x002580CB
		// (set) Token: 0x0600AE50 RID: 44624 RVA: 0x00259ED3 File Offset: 0x002580D3
		[DefaultValue(false)]
		internal bool IsBetweenFilter { get; set; }

		// Token: 0x04002E02 RID: 11778
		private static readonly string _defaultRadDatePickerId = "RDIPF";

		// Token: 0x04002E03 RID: 11779
		private static readonly string _defaultRadDateInputId = "RDIF";

		// Token: 0x04002E04 RID: 11780
		private static readonly string _rangeRadDatePickerId = "RDIPF2";

		// Token: 0x04002E05 RID: 11781
		private static readonly string _rangeRadDateInputId = "RDIF2";

		// Token: 0x04002E06 RID: 11782
		private static readonly string _sharedTimeViewName = "gdtcSharedTimeView";

		// Token: 0x04002E07 RID: 11783
		internal static readonly string _sharedCalendarName = "gdtcSharedCalendar";

		// Token: 0x04002E08 RID: 11784
		private bool filterExpressionEvaluationInProcess;
	}
}
