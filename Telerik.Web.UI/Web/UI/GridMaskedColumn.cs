using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019F5 RID: 6645
	public class GridMaskedColumn : GridBoundColumn
	{
		// Token: 0x0601014C RID: 65868 RVA: 0x0039CCAF File Offset: 0x0039AEAF
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public GridMaskedColumn()
		{
			this.CurrentFilterFunction = GridKnownFunction.NoFilter;
			this.FilterListOptions = GridFilterListOptions.VaryByDataType;
		}

		// Token: 0x0601014D RID: 65869 RVA: 0x0039CCC8 File Offset: 0x0039AEC8
		protected override ArrayList GetFilterFunctionsList(GridFilterListOptions options, ArrayList sourceList)
		{
			sourceList = base.GetFilterFunctionsList(options, sourceList);
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
			return sourceList;
		}

		// Token: 0x0601014E RID: 65870 RVA: 0x0039CD74 File Offset: 0x0039AF74
		public override GridColumn Clone()
		{
			GridMaskedColumn gridMaskedColumn = new GridMaskedColumn();
			gridMaskedColumn.CopyBaseProperties(this);
			return gridMaskedColumn;
		}

		// Token: 0x0601014F RID: 65871 RVA: 0x0039CD90 File Offset: 0x0039AF90
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridMaskedColumn gridMaskedColumn = (GridMaskedColumn)fromColumn;
			this.Mask = gridMaskedColumn.Mask;
			this.DisplayMask = gridMaskedColumn.DisplayMask;
		}

		// Token: 0x06010150 RID: 65872 RVA: 0x0039CDC3 File Offset: 0x0039AFC3
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			if (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && base.UseNativeEditorsInMobileMode)
			{
				return new GridMobileMaskedColumnEditor(this);
			}
			return new GridMaskedColumnEditor(this);
		}

		// Token: 0x06010151 RID: 65873 RVA: 0x0039CE0C File Offset: 0x0039B00C
		protected override void SetupFilterControls(TableCell cell)
		{
			if (this.FilterTemplate != null)
			{
				this.FilterTemplate.InstantiateIn(cell);
				return;
			}
			RadMaskedTextBox radMaskedTextBox = new RadMaskedTextBox();
			radMaskedTextBox.ID = string.Format("RDMTBF_{0}", this.UniqueName);
			radMaskedTextBox.RenderMode = base.Owner.OwnerGrid.RenderMode;
			cell.Controls.Add(radMaskedTextBox);
			radMaskedTextBox.Attributes["alt"] = this.FilterControlAltText;
			radMaskedTextBox.ToolTip = this.FilterControlToolTip;
			radMaskedTextBox.Mask = this.Mask;
			if (!string.IsNullOrEmpty(this.DisplayMask))
			{
				radMaskedTextBox.DisplayMask = this.DisplayMask;
			}
			if (!this.FilterControlWidth.IsEmpty)
			{
				radMaskedTextBox.Width = this.FilterControlWidth;
			}
			radMaskedTextBox.AllowEmptyEnumerations = true;
			radMaskedTextBox.EnableEmbeddedSkins = base.Owner.OwnerGrid.EnableEmbeddedSkins;
			radMaskedTextBox.EnableAriaSupport = base.Owner.OwnerGrid.EnableAriaSupport;
			if (radMaskedTextBox.EnableAriaSupport)
			{
				radMaskedTextBox.Attributes.Add("aria-label", this.HeaderText);
			}
			radMaskedTextBox.PreRender += delegate(object sender, EventArgs e)
			{
				((RadMaskedTextBox)sender).Skin = base.Owner.OwnerGrid.RuntimeSkin;
			};
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
			string text = string.Format("$find(\"{0}\")._filterNoDelay(\"{1}\",\"{2}\", true)", base.Owner.ClientID, radMaskedTextBox.ClientID, this.UniqueName);
			if (this.AutoPostBackOnFilter)
			{
				radMaskedTextBox.ClientEvents.OnValueChanged = "function(sender, args){" + text + "}";
			}
			if (this.FilterDelay != null)
			{
				radMaskedTextBox.Attributes["onkeydown"] = string.Format("{0}", string.Format(format, new object[]
				{
					base.Owner.ClientID,
					"Down",
					radMaskedTextBox.ClientID,
					this.UniqueName,
					filterDelay
				}));
				radMaskedTextBox.Attributes["onkeypress"] = string.Format("{0}", string.Format(format, new object[]
				{
					base.Owner.ClientID,
					"Press",
					radMaskedTextBox.ClientID,
					this.UniqueName,
					filterDelay
				}));
			}
			else if (this.AutoPostBackOnFilter)
			{
				radMaskedTextBox.Attributes["onkeypress"] = string.Format("if(event.keyCode == 13){{ this.blur(); event.cancelBubble = true; event.returnValue = false; if (event.stopPropagation){{ event.stopPropagation(); event.preventDefault();}} {0} }}", text);
			}
			else
			{
				radMaskedTextBox.ClientEvents.OnKeyPress = "Telerik.Web.UI.RadInputControl.CancelRawEventOnEnterKey";
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
				button.ID = string.Format("Filter_{0}", this.UniqueName);
				cell.Controls.Add(button);
			}
		}

		// Token: 0x06010152 RID: 65874 RVA: 0x0039D260 File Offset: 0x0039B460
		private void radMaskedTextBox_TextChanged(object sender, EventArgs e)
		{
			base.Owner.OwnerGrid.EditIndexes.Clear();
			GridFilteringItem gridFilteringItem = (GridFilteringItem)((RadMaskedTextBox)sender).Parent.Parent;
			gridFilteringItem.FireCommandEvent("Filter", new Pair(this.CurrentFilterFunction.ToString(), this.UniqueName));
		}

		// Token: 0x06010153 RID: 65875 RVA: 0x0039D2C0 File Offset: 0x0039B4C0
		protected override void SetCurrentFilterValueToControl(TableCell cell)
		{
			base.SetCurrentFilterValueToControl(cell);
			if (!string.IsNullOrEmpty(this.CurrentFilterValue))
			{
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					RadMaskedTextBox radMaskedTextBox = control as RadMaskedTextBox;
					if (radMaskedTextBox != null)
					{
						radMaskedTextBox.Text = this.CurrentFilterValue;
						break;
					}
				}
			}
		}

		// Token: 0x06010154 RID: 65876 RVA: 0x0039D340 File Offset: 0x0039B540
		protected override string GetCurrentFilterValueFromControl(TableCell cell)
		{
			string result = string.Empty;
			foreach (object obj in cell.Controls)
			{
				Control control = (Control)obj;
				RadMaskedTextBox radMaskedTextBox = control as RadMaskedTextBox;
				if (radMaskedTextBox != null)
				{
					result = radMaskedTextBox.Text;
					break;
				}
			}
			return result;
		}

		// Token: 0x17004DA2 RID: 19874
		// (get) Token: 0x06010155 RID: 65877 RVA: 0x0039D3B0 File Offset: 0x0039B5B0
		// (set) Token: 0x06010156 RID: 65878 RVA: 0x0039D3DD File Offset: 0x0039B5DD
		[NotifyParentProperty(true)]
		public string Mask
		{
			get
			{
				object obj = base.ViewState["Mask"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["Mask"] = value;
			}
		}

		// Token: 0x17004DA3 RID: 19875
		// (get) Token: 0x06010157 RID: 65879 RVA: 0x0039D3F0 File Offset: 0x0039B5F0
		// (set) Token: 0x06010158 RID: 65880 RVA: 0x0039D41D File Offset: 0x0039B61D
		[NotifyParentProperty(true)]
		public string DisplayMask
		{
			get
			{
				object obj = base.ViewState["DisplayMask"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["DisplayMask"] = value;
			}
		}
	}
}
