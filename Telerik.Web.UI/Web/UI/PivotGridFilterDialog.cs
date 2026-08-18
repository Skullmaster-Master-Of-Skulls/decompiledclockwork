using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DB6 RID: 3510
	public class PivotGridFilterDialog : PivotGridWindowBase
	{
		// Token: 0x06008302 RID: 33538 RVA: 0x001DDB10 File Offset: 0x001DBD10
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.CreateContainerPanel();
			this.CreateTopOperatorCombo();
			this.CreateTopNumBox();
			this.CreateTopAggregateCombo();
			this.CreateByLabel();
			this.CreateAggregatesCombo();
			this.CreateOperatorsCombo();
			this.CreateValue1TextBox();
			this.CreateAndLabel();
			this.CreateValue2TextBox();
			this.CreateIgnoreCaseCheckBox();
			this.AddBrHTMLElement();
			this.CreateOKButton();
			this.CreateCancelButton();
		}

		// Token: 0x17002964 RID: 10596
		// (get) Token: 0x06008303 RID: 33539 RVA: 0x001DDB78 File Offset: 0x001DBD78
		public Panel ContainerPanel
		{
			get
			{
				if (this.pnlContainer == null)
				{
					this.pnlContainer = new Panel
					{
						ID = "fwContainerPanel"
					};
				}
				return this.pnlContainer;
			}
		}

		// Token: 0x17002965 RID: 10597
		// (get) Token: 0x06008304 RID: 33540 RVA: 0x001DDBAC File Offset: 0x001DBDAC
		public RadComboBox AggregatesCombo
		{
			get
			{
				if (this.rcmbAggregates == null)
				{
					this.rcmbAggregates = new RadComboBox
					{
						ID = "rcmbAggregates",
						EnableAriaSupport = this.ownerPivotGrid.EnableAriaSupport
					};
				}
				return this.rcmbAggregates;
			}
		}

		// Token: 0x17002966 RID: 10598
		// (get) Token: 0x06008305 RID: 33541 RVA: 0x001DDBF0 File Offset: 0x001DBDF0
		public RadComboBox AggregateOperatorsCombo
		{
			get
			{
				if (this.rcmbOperators == null)
				{
					this.rcmbOperators = new RadComboBox
					{
						ID = "rcmbOperators",
						EnableAriaSupport = this.ownerPivotGrid.EnableAriaSupport
					};
				}
				return this.rcmbOperators;
			}
		}

		// Token: 0x17002967 RID: 10599
		// (get) Token: 0x06008306 RID: 33542 RVA: 0x001DDC34 File Offset: 0x001DBE34
		public TextBox FilterValue1Box
		{
			get
			{
				if (this.rtbValue1 == null)
				{
					this.rtbValue1 = new TextBox
					{
						ID = "rtbValue1"
					};
				}
				return this.rtbValue1;
			}
		}

		// Token: 0x17002968 RID: 10600
		// (get) Token: 0x06008307 RID: 33543 RVA: 0x001DDC68 File Offset: 0x001DBE68
		public TextBox FilterValue2Box
		{
			get
			{
				if (this.rtbValue2 == null)
				{
					this.rtbValue2 = new TextBox
					{
						ID = "rtbValue2"
					};
				}
				return this.rtbValue2;
			}
		}

		// Token: 0x17002969 RID: 10601
		// (get) Token: 0x06008308 RID: 33544 RVA: 0x001DDC9C File Offset: 0x001DBE9C
		public Label AndLabel
		{
			get
			{
				if (this.lblAnd == null)
				{
					this.lblAnd = new Label
					{
						ID = "lblAnd"
					};
				}
				return this.lblAnd;
			}
		}

		// Token: 0x1700296A RID: 10602
		// (get) Token: 0x06008309 RID: 33545 RVA: 0x001DDCD0 File Offset: 0x001DBED0
		public Label ByLabel
		{
			get
			{
				if (this.lblBy == null)
				{
					this.lblBy = new Label
					{
						ID = "lblBy"
					};
				}
				return this.lblBy;
			}
		}

		// Token: 0x1700296B RID: 10603
		// (get) Token: 0x0600830A RID: 33546 RVA: 0x001DDD04 File Offset: 0x001DBF04
		public RadComboBox SortedListSelectionCombo
		{
			get
			{
				if (this.rcmbSortedListSelection == null)
				{
					this.rcmbSortedListSelection = new RadComboBox
					{
						ID = "rcmbSortedListSelection",
						EnableAriaSupport = this.ownerPivotGrid.EnableAriaSupport
					};
				}
				return this.rcmbSortedListSelection;
			}
		}

		// Token: 0x1700296C RID: 10604
		// (get) Token: 0x0600830B RID: 33547 RVA: 0x001DDD48 File Offset: 0x001DBF48
		public RadComboBox SortedListAggregateOperatorCombo
		{
			get
			{
				if (this.rcmbSortedListAggregateOperator == null)
				{
					this.rcmbSortedListAggregateOperator = new RadComboBox
					{
						ID = "rcmbslAggregateOperator",
						EnableAriaSupport = this.ownerPivotGrid.EnableAriaSupport
					};
				}
				return this.rcmbSortedListAggregateOperator;
			}
		}

		// Token: 0x1700296D RID: 10605
		// (get) Token: 0x0600830C RID: 33548 RVA: 0x001DDD8C File Offset: 0x001DBF8C
		public RadNumericTextBox SortedListFilterValueBox
		{
			get
			{
				if (this.rntbSorteListFilterValue == null)
				{
					this.rntbSorteListFilterValue = new RadNumericTextBox
					{
						ID = "rntbslFilterValueBox",
						EnableAriaSupport = this.ownerPivotGrid.EnableAriaSupport
					};
				}
				return this.rntbSorteListFilterValue;
			}
		}

		// Token: 0x0600830D RID: 33549 RVA: 0x001DDDD0 File Offset: 0x001DBFD0
		public PivotGridFilterDialog(RadPivotGrid ownerPivotGrid) : base(ownerPivotGrid)
		{
		}

		// Token: 0x0600830E RID: 33550 RVA: 0x001DDDD9 File Offset: 0x001DBFD9
		private void CreateContainerPanel()
		{
			this.ContainerPanel.CssClass = "rpgFilterWindowContainer";
			base.ContentContainer.Controls.Add(this.ContainerPanel);
		}

		// Token: 0x0600830F RID: 33551 RVA: 0x001DDE04 File Offset: 0x001DC004
		private void AddBrHTMLElement()
		{
			HtmlGenericControl child = new HtmlGenericControl("br");
			this.ContainerPanel.Controls.Add(child);
		}

		// Token: 0x06008310 RID: 33552 RVA: 0x001DDE6C File Offset: 0x001DC06C
		private void CreateTopOperatorCombo()
		{
			this.SortedListSelectionCombo.Items.Add(new RadComboBoxItem(this.GetFilterMenuItemText("Top"), "15"));
			this.SortedListSelectionCombo.Items.Add(new RadComboBoxItem(this.GetFilterMenuItemText("Bottom"), "16"));
			this.SortedListSelectionCombo.PreRender += delegate(object sender, EventArgs e)
			{
				RadComboBox radComboBox = sender as RadComboBox;
				if (radComboBox != null)
				{
					radComboBox.Skin = this.ownerPivotGrid.RuntimeSkin;
					radComboBox.EnableEmbeddedSkins = this.ownerPivotGrid.EnableEmbeddedSkins;
				}
			};
			this.SortedListSelectionCombo.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			this.ContainerPanel.Controls.Add(this.SortedListSelectionCombo);
		}

		// Token: 0x06008311 RID: 33553 RVA: 0x001DDF44 File Offset: 0x001DC144
		private void CreateTopNumBox()
		{
			this.SortedListFilterValueBox.ShowSpinButtons = true;
			this.SortedListFilterValueBox.PreRender += delegate(object sender, EventArgs e)
			{
				RadNumericTextBox radNumericTextBox = sender as RadNumericTextBox;
				if (radNumericTextBox != null)
				{
					radNumericTextBox.Skin = this.ownerPivotGrid.RuntimeSkin;
					radNumericTextBox.EnableEmbeddedSkins = this.ownerPivotGrid.EnableEmbeddedSkins;
				}
			};
			this.SortedListFilterValueBox.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			this.ContainerPanel.Controls.Add(this.SortedListFilterValueBox);
		}

		// Token: 0x06008312 RID: 33554 RVA: 0x001DDFDC File Offset: 0x001DC1DC
		private void CreateTopAggregateCombo()
		{
			this.SortedListAggregateOperatorCombo.Items.Add(new RadComboBoxItem(this.GetFilterMenuItemText("Items"), "Items"));
			this.SortedListAggregateOperatorCombo.Items.Add(new RadComboBoxItem(this.GetFilterMenuItemText("Percent"), "Percent"));
			this.SortedListAggregateOperatorCombo.Items.Add(new RadComboBoxItem(this.GetFilterMenuItemText("Sum"), "Sum"));
			this.SortedListAggregateOperatorCombo.PreRender += delegate(object sender, EventArgs e)
			{
				RadComboBox radComboBox = sender as RadComboBox;
				if (radComboBox != null)
				{
					radComboBox.Skin = this.ownerPivotGrid.RuntimeSkin;
					radComboBox.EnableEmbeddedSkins = this.ownerPivotGrid.EnableEmbeddedSkins;
				}
			};
			this.SortedListAggregateOperatorCombo.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			this.ContainerPanel.Controls.Add(this.SortedListAggregateOperatorCombo);
		}

		// Token: 0x06008313 RID: 33555 RVA: 0x001DE09B File Offset: 0x001DC29B
		private void CreateByLabel()
		{
			this.ByLabel.Text = this.ownerPivotGrid.Localization.GetString("FilterDialogByLabelText");
			this.ContainerPanel.Controls.Add(this.ByLabel);
		}

		// Token: 0x06008314 RID: 33556 RVA: 0x001DE110 File Offset: 0x001DC310
		private void CreateAggregatesCombo()
		{
			this.AggregatesCombo.PreRender += delegate(object sender, EventArgs e)
			{
				RadComboBox radComboBox = sender as RadComboBox;
				if (radComboBox != null)
				{
					radComboBox.Skin = this.ownerPivotGrid.RuntimeSkin;
					radComboBox.EnableEmbeddedSkins = this.ownerPivotGrid.EnableEmbeddedSkins;
				}
			};
			this.AggregatesCombo.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			this.ContainerPanel.Controls.Add(this.AggregatesCombo);
		}

		// Token: 0x06008315 RID: 33557 RVA: 0x001DE19C File Offset: 0x001DC39C
		private void CreateOperatorsCombo()
		{
			this.AggregateOperatorsCombo.PreRender += delegate(object sender, EventArgs e)
			{
				RadComboBox radComboBox = sender as RadComboBox;
				if (radComboBox != null)
				{
					radComboBox.Skin = this.ownerPivotGrid.RuntimeSkin;
					radComboBox.EnableEmbeddedSkins = this.ownerPivotGrid.EnableEmbeddedSkins;
				}
			};
			this.AggregateOperatorsCombo.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			this.ContainerPanel.Controls.Add(this.AggregateOperatorsCombo);
			int num = 0;
			foreach (string filterFunctionName in Enum.GetNames(typeof(PivotGridFilterFunction)))
			{
				RadComboBoxItem radComboBoxItem = new RadComboBoxItem();
				radComboBoxItem.Text = this.GetFilterMenuItemText(filterFunctionName).TrimEnd(new char[]
				{
					'.'
				});
				radComboBoxItem.Value = num.ToString();
				this.AggregateOperatorsCombo.Items.Add(radComboBoxItem);
				num++;
			}
		}

		// Token: 0x06008316 RID: 33558 RVA: 0x001DE25F File Offset: 0x001DC45F
		private void CreateValue1TextBox()
		{
			this.ContainerPanel.Controls.Add(this.FilterValue1Box);
		}

		// Token: 0x06008317 RID: 33559 RVA: 0x001DE277 File Offset: 0x001DC477
		private void CreateAndLabel()
		{
			this.AndLabel.Text = this.ownerPivotGrid.Localization.GetString("FilterDialogAndLabelText");
			this.ContainerPanel.Controls.Add(this.AndLabel);
		}

		// Token: 0x06008318 RID: 33560 RVA: 0x001DE2AF File Offset: 0x001DC4AF
		private void CreateValue2TextBox()
		{
			this.ContainerPanel.Controls.Add(this.FilterValue2Box);
		}

		// Token: 0x06008319 RID: 33561 RVA: 0x001DE2C8 File Offset: 0x001DC4C8
		private void CreateIgnoreCaseCheckBox()
		{
			this.IgnoreCaseCheckBox.Text = this.ownerPivotGrid.Localization.GetString("FiltersWindowIgnoreCaseCheckBoxText");
			this.IgnoreCaseCheckBox.ToolTip = this.ownerPivotGrid.Localization.GetString("FiltersWindowIgnoreCaseCheckBoxText");
			this.IgnoreCaseCheckBox.CssClass = "rpgFilterCheckBoxIgnoreCase";
			this.ContainerPanel.Controls.Add(this.IgnoreCaseCheckBox);
		}

		// Token: 0x1700296E RID: 10606
		// (get) Token: 0x0600831A RID: 33562 RVA: 0x001DE33C File Offset: 0x001DC53C
		public CheckBox IgnoreCaseCheckBox
		{
			get
			{
				if (this.ignoreCaseCheckBox == null)
				{
					this.ignoreCaseCheckBox = new CheckBox
					{
						ID = "cbIgnoreCase"
					};
				}
				return this.ignoreCaseCheckBox;
			}
		}

		// Token: 0x0600831B RID: 33563 RVA: 0x001DE370 File Offset: 0x001DC570
		private void CreateOKButton()
		{
			Button button;
			if (this.ownerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				button = new Button();
				button.CssClass = string.Empty;
			}
			else
			{
				button = new ElasticButton(string.Empty, "rpgButtonText");
				button.CssClass = "rpgActionButton ";
			}
			button.ID = "btnOK";
			Button button2 = button;
			button2.CssClass += "rpgFilterButtonOk";
			button.OnClientClick = "return false;";
			this.ContainerPanel.Controls.Add(button);
			button.Text = this.ownerPivotGrid.Localization.GetString("FilterDialogOKButtonText");
		}

		// Token: 0x0600831C RID: 33564 RVA: 0x001DE414 File Offset: 0x001DC614
		private void CreateCancelButton()
		{
			Button button;
			if (this.ownerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				button = new Button();
				button.CssClass = string.Empty;
			}
			else
			{
				button = new ElasticButton(string.Empty, "rpgButtonText");
				button.CssClass = "rpgActionButton ";
			}
			button.ID = "btnCancel";
			Button button2 = button;
			button2.CssClass += "rpgFilterButtonCancel";
			button.OnClientClick = "return false;";
			this.ContainerPanel.Controls.Add(button);
			button.Text = this.ownerPivotGrid.Localization.GetString("FilterDialogCancelButtonText");
		}

		// Token: 0x0600831D RID: 33565 RVA: 0x001DE4B5 File Offset: 0x001DC6B5
		private string GetFilterMenuItemText(string filterFunctionName)
		{
			return this.ownerPivotGrid.Localization.GetString(string.Format("{0}Text", filterFunctionName));
		}

		// Token: 0x0600831E RID: 33566 RVA: 0x001DE4E8 File Offset: 0x001DC6E8
		public void InitializeControls()
		{
			this.AggregatesCombo.Items.Clear();
			IEnumerable<PivotGridField> enumerable = from field in this.ownerPivotGrid.Fields
			where field is PivotGridAggregateField && !field.IsHidden
			select field;
			foreach (PivotGridField pivotGridField in enumerable)
			{
				PivotGridAggregateField pivotGridAggregateField = (PivotGridAggregateField)pivotGridField;
				string text = pivotGridAggregateField.Aggregate.ToString() + " of " + pivotGridAggregateField.DataField.ToString();
				RadComboBoxItem item = new RadComboBoxItem(text, pivotGridAggregateField.UniqueName);
				this.AggregatesCombo.Items.Add(item);
			}
		}

		// Token: 0x04002438 RID: 9272
		private Panel pnlContainer;

		// Token: 0x04002439 RID: 9273
		private RadComboBox rcmbAggregates;

		// Token: 0x0400243A RID: 9274
		private RadComboBox rcmbOperators;

		// Token: 0x0400243B RID: 9275
		private TextBox rtbValue1;

		// Token: 0x0400243C RID: 9276
		private TextBox rtbValue2;

		// Token: 0x0400243D RID: 9277
		private Label lblAnd;

		// Token: 0x0400243E RID: 9278
		private Label lblBy;

		// Token: 0x0400243F RID: 9279
		private RadComboBox rcmbSortedListSelection;

		// Token: 0x04002440 RID: 9280
		private RadComboBox rcmbSortedListAggregateOperator;

		// Token: 0x04002441 RID: 9281
		private RadNumericTextBox rntbSorteListFilterValue;

		// Token: 0x04002442 RID: 9282
		private CheckBox ignoreCaseCheckBox;
	}
}
