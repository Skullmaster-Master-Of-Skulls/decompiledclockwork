using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018FC RID: 6396
	public class GridRatingColumn : GridEditableColumn
	{
		// Token: 0x17004A49 RID: 19017
		// (get) Token: 0x0600F6CC RID: 63180 RVA: 0x00380247 File Offset: 0x0037E447
		// (set) Token: 0x0600F6CD RID: 63181 RVA: 0x00380267 File Offset: 0x0037E467
		[NotifyParentProperty(true)]
		[Description("DataField")]
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataField
		{
			get
			{
				return (string)(base.ViewState["DataField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataField"] = value;
				base.UpdateUniqueNameIfDefault(value);
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600F6CE RID: 63182 RVA: 0x00380287 File Offset: 0x0037E487
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x17004A4A RID: 19018
		// (get) Token: 0x0600F6CF RID: 63183 RVA: 0x0038028F File Offset: 0x0037E48F
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x0600F6D0 RID: 63184 RVA: 0x00380297 File Offset: 0x0037E497
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataField) && this.AllowSorting)
			{
				return this.DataField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x17004A4B RID: 19019
		// (get) Token: 0x0600F6D1 RID: 63185 RVA: 0x003802C8 File Offset: 0x0037E4C8
		// (set) Token: 0x0600F6D2 RID: 63186 RVA: 0x003802E9 File Offset: 0x0037E4E9
		[DefaultValue(5)]
		[NotifyParentProperty(true)]
		[Description("ItemCount")]
		[Category("Behavior")]
		public virtual int ItemCount
		{
			get
			{
				return (int)(base.ViewState["ItemCount"] ?? 5);
			}
			set
			{
				base.ViewState["ItemCount"] = value;
			}
		}

		// Token: 0x17004A4C RID: 19020
		// (get) Token: 0x0600F6D3 RID: 63187 RVA: 0x00380301 File Offset: 0x0037E501
		// (set) Token: 0x0600F6D4 RID: 63188 RVA: 0x00380322 File Offset: 0x0037E522
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("SelectionMode")]
		[DefaultValue(RatingSelectionMode.Continuous)]
		public virtual RatingSelectionMode SelectionMode
		{
			get
			{
				return (RatingSelectionMode)(base.ViewState["SelectionMode"] ?? RatingSelectionMode.Continuous);
			}
			set
			{
				base.ViewState["SelectionMode"] = value;
			}
		}

		// Token: 0x17004A4D RID: 19021
		// (get) Token: 0x0600F6D5 RID: 63189 RVA: 0x0038033A File Offset: 0x0037E53A
		// (set) Token: 0x0600F6D6 RID: 63190 RVA: 0x0038035B File Offset: 0x0037E55B
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(RatingPrecision.Item)]
		[Description("Precision")]
		public virtual RatingPrecision Precision
		{
			get
			{
				return (RatingPrecision)(base.ViewState["Precision"] ?? RatingPrecision.Item);
			}
			set
			{
				base.ViewState["Precision"] = value;
			}
		}

		// Token: 0x17004A4E RID: 19022
		// (get) Token: 0x0600F6D7 RID: 63191 RVA: 0x00380373 File Offset: 0x0037E573
		// (set) Token: 0x0600F6D8 RID: 63192 RVA: 0x00380394 File Offset: 0x0037E594
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("IsDirectionReversed")]
		[Category("Behavior")]
		public virtual bool IsDirectionReversed
		{
			get
			{
				return (bool)(base.ViewState["IsDirectionReversed"] ?? false);
			}
			set
			{
				base.ViewState["IsDirectionReversed"] = value;
			}
		}

		// Token: 0x17004A4F RID: 19023
		// (get) Token: 0x0600F6D9 RID: 63193 RVA: 0x003803AC File Offset: 0x0037E5AC
		// (set) Token: 0x0600F6DA RID: 63194 RVA: 0x003803CD File Offset: 0x0037E5CD
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("AllowRatingInViewMode")]
		public virtual bool AllowRatingInViewMode
		{
			get
			{
				return (bool)(base.ViewState["AllowRatingInViewMode"] ?? false);
			}
			set
			{
				base.ViewState["AllowRatingInViewMode"] = value;
			}
		}

		// Token: 0x17004A50 RID: 19024
		// (get) Token: 0x0600F6DB RID: 63195 RVA: 0x003803E5 File Offset: 0x0037E5E5
		// (set) Token: 0x0600F6DC RID: 63196 RVA: 0x003803ED File Offset: 0x0037E5ED
		[DefaultValue(false)]
		internal bool IsBetweenFilter { get; set; }

		// Token: 0x0600F6DD RID: 63197 RVA: 0x003803F8 File Offset: 0x0037E5F8
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			if (inItem.IsDataBound)
			{
				this.CurrentColumnEditor.InitializeInControl(cell);
				if (base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile || !base.UseNativeEditorsInMobileMode)
				{
					RadRating ratingControl = ((GridRatingColumnEditor)this.CurrentColumnEditor).RatingControl;
					ratingControl.ReadOnly = (base.IsReadOnly(inItem) || (!inItem.IsInEditMode && !this.AllowRatingInViewMode));
				}
				else if (!inItem.IsInEditMode)
				{
					((GridMobileRatingColumnEditor)this.CurrentColumnEditor).TextBoxControl.Attributes.Add("readonly", "true");
				}
				if (!string.IsNullOrEmpty(this.DataField))
				{
					inItem.CellDataBound += this.OnCellDataBound;
				}
			}
		}

		// Token: 0x0600F6DE RID: 63198 RVA: 0x003804C8 File Offset: 0x0037E6C8
		protected virtual void OnCellDataBound(object sender, GridCellDataBoundEventArgs args)
		{
			if (args.Column != this)
			{
				return;
			}
			GridItem gridItem = (GridItem)sender;
			if (base.DesignMode || !gridItem.IsDataBound || gridItem.DataItem == null || args.Cell == null || string.IsNullOrEmpty(this.DataField))
			{
				return;
			}
			this.CurrentColumnEditor.InitializeFromControl(args.Cell);
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				((GridMobileRatingColumnEditor)this.CurrentColumnEditor).TextBoxControl.Text = this.ExtractValueFromDataItem<object>(gridItem.DataItem, this.DataField).ToString();
				return;
			}
			((GridRatingColumnEditor)this.CurrentColumnEditor).RatingControl.DbValue = this.ExtractValueFromDataItem<object>(gridItem.DataItem, this.DataField);
		}

		// Token: 0x0600F6DF RID: 63199 RVA: 0x00380594 File Offset: 0x0037E794
		private T ExtractValueFromDataItem<T>(object dataItem, string dataFieldName)
		{
			object obj = null;
			if (dataFieldName.IndexOf(".") > -1)
			{
				try
				{
					obj = DataBinder.GetPropertyValue(dataItem, dataFieldName);
					goto IL_44;
				}
				catch
				{
					try
					{
						obj = DataBinder.Eval(dataItem, dataFieldName);
					}
					catch
					{
						if (obj != null && !GridBaseDataList.IsBindableType(obj.GetType()))
						{
							obj = null;
						}
					}
					goto IL_44;
				}
			}
			obj = DataBinder.Eval(dataItem, dataFieldName);
			IL_44:
			if (obj == null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataItem).Find(dataFieldName, true);
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(dataItem);
				}
			}
			if (obj == null || !(obj is T))
			{
				obj = default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x17004A51 RID: 19025
		// (get) Token: 0x0600F6E0 RID: 63200 RVA: 0x00380640 File Offset: 0x0037E840
		public override bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x0600F6E1 RID: 63201 RVA: 0x0038064C File Offset: 0x0037E84C
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				GridMobileRatingColumnEditor gridMobileRatingColumnEditor = (GridMobileRatingColumnEditor)editableItem.EditManager.GetColumnEditor(this);
				newValues[this.DataField] = gridMobileRatingColumnEditor.Value;
				return;
			}
			GridRatingColumnEditor gridRatingColumnEditor = (GridRatingColumnEditor)editableItem.EditManager.GetColumnEditor(this);
			newValues[this.DataField] = gridRatingColumnEditor.Value;
		}

		// Token: 0x0600F6E2 RID: 63202 RVA: 0x003806C7 File Offset: 0x0037E8C7
		protected virtual string GenerateRatingID()
		{
			return string.Format("Rating_{0}", this.UniqueName);
		}

		// Token: 0x0600F6E3 RID: 63203 RVA: 0x003806D9 File Offset: 0x0037E8D9
		public override bool IsBoundToFieldName(string name)
		{
			return string.Compare(this.DataField, name, true) == 0;
		}

		// Token: 0x0600F6E4 RID: 63204 RVA: 0x003806EB File Offset: 0x0037E8EB
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataField);
		}

		// Token: 0x0600F6E5 RID: 63205 RVA: 0x003806F9 File Offset: 0x0037E8F9
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				return new GridMobileRatingColumnEditor(this);
			}
			return new GridRatingColumnEditor(this);
		}

		// Token: 0x0600F6E6 RID: 63206 RVA: 0x00380724 File Offset: 0x0037E924
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if ((base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile || !base.UseNativeEditorsInMobileMode) && !(newValue is GridRatingColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type " + typeof(GridRatingColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x0600F6E7 RID: 63207 RVA: 0x00380780 File Offset: 0x0037E980
		protected override string GetFilterDataField()
		{
			return this.DataField;
		}

		// Token: 0x17004A52 RID: 19026
		// (get) Token: 0x0600F6E8 RID: 63208 RVA: 0x00380788 File Offset: 0x0037E988
		// (set) Token: 0x0600F6E9 RID: 63209 RVA: 0x0038079E File Offset: 0x0037E99E
		public override int? FilterDelay
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x0600F6EA RID: 63210 RVA: 0x003807A0 File Offset: 0x0037E9A0
		protected override void SetupFilterControls(TableCell cell)
		{
			if (this.FilterTemplate != null)
			{
				this.FilterTemplate.InstantiateIn(cell);
				return;
			}
			this.CurrentColumnEditor.InitializeInControl(cell);
			if (base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile || !base.UseNativeEditorsInMobileMode)
			{
				RadRating ratingControl = ((GridRatingColumnEditor)this.CurrentColumnEditor).RatingControl;
				if (!this.FilterControlWidth.IsEmpty)
				{
					ratingControl.Width = this.FilterControlWidth;
				}
				ratingControl.ToolTip = this.FilterControlToolTip;
				if (this.AutoPostBackOnFilter)
				{
					string onClientRated = string.Format("function(s,e){{setTimeout(function(){{$find(\"{0}\").filter(\"{1}\", s.get_value());}},{2});}}", base.Owner.ClientID, this.UniqueName, 0);
					ratingControl.OnClientRated = onClientRated;
				}
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
						elasticButton.Attributes.Add("aria-label", this.HeaderText);
					}
					cell.Controls.Add(elasticButton);
					return;
				}
				Button button = new Button();
				button.CssClass = "rgFilter";
				RadGrid.ToggleColumnFilteredClass(button, this);
				button.ToolTip = this.FilterImageToolTip;
				button.Text = " ";
				button.ID = string.Format("Filter_{0}", this.UniqueName);
				cell.Controls.Add(button);
			}
		}

		// Token: 0x0600F6EB RID: 63211 RVA: 0x003809E0 File Offset: 0x0037EBE0
		protected override void SetCurrentFilterValueToControl(TableCell cell)
		{
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
							RadRating radRating = control as RadRating;
							if (radRating != null)
							{
								decimal value;
								if (decimal.TryParse(array[num], out value))
								{
									radRating.Value = value;
								}
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
				RadRating ratingControlFromCell = this.GetRatingControlFromCell(cell);
				if (ratingControlFromCell != null)
				{
					ratingControlFromCell.Value = Convert.ToDecimal(this.CurrentFilterValue, NumberFormatInfo.InvariantInfo);
				}
			}
		}

		// Token: 0x0600F6EC RID: 63212 RVA: 0x00380AC8 File Offset: 0x0037ECC8
		protected override string GetCurrentFilterValueFromControl(TableCell cell)
		{
			if (this.IsBetweenFilter)
			{
				RadRating radRating = null;
				RadRating radRating2 = null;
				int num = 0;
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					RadRating radRating3 = control as RadRating;
					if (radRating3 != null)
					{
						if (num == 0)
						{
							radRating = radRating3;
						}
						else if (num == 1)
						{
							radRating2 = radRating3;
						}
						num++;
						if (num == 2)
						{
							break;
						}
					}
				}
				if (radRating == null || radRating2 == null)
				{
					return string.Empty;
				}
				return radRating.Value.ToString(NumberFormatInfo.InvariantInfo) + " " + radRating2.Value.ToString(NumberFormatInfo.InvariantInfo);
			}
			else
			{
				RadRating ratingControlFromCell = this.GetRatingControlFromCell(cell);
				if (ratingControlFromCell == null)
				{
					return string.Empty;
				}
				return ratingControlFromCell.Value.ToString(NumberFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x0600F6ED RID: 63213 RVA: 0x00380BBC File Offset: 0x0037EDBC
		private RadRating GetRatingControlFromCell(TableCell cell)
		{
			((GridRatingColumnEditor)this.CurrentColumnEditor).GetRatingControlID();
			RadRating radRating = null;
			foreach (object obj in cell.Controls)
			{
				Control control = (Control)obj;
				radRating = (control as RadRating);
				if (radRating != null)
				{
					return radRating;
				}
			}
			return radRating;
		}

		// Token: 0x0600F6EE RID: 63214 RVA: 0x00380C38 File Offset: 0x0037EE38
		public override GridColumn Clone()
		{
			GridRatingColumn gridRatingColumn = new GridRatingColumn();
			gridRatingColumn.CopyBaseProperties(this);
			return gridRatingColumn;
		}

		// Token: 0x0600F6EF RID: 63215 RVA: 0x00380C54 File Offset: 0x0037EE54
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridRatingColumn gridRatingColumn = (GridRatingColumn)fromColumn;
			this.DataField = gridRatingColumn.DataField;
			this.ItemCount = gridRatingColumn.ItemCount;
			this.SelectionMode = gridRatingColumn.SelectionMode;
			this.Precision = gridRatingColumn.Precision;
			this.IsDirectionReversed = gridRatingColumn.IsDirectionReversed;
			this.AllowRatingInViewMode = gridRatingColumn.AllowRatingInViewMode;
		}
	}
}
