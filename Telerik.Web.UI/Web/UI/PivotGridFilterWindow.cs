using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DB8 RID: 3512
	public class PivotGridFilterWindow : PivotGridWindowBase
	{
		// Token: 0x06008325 RID: 33573 RVA: 0x001DE5B8 File Offset: 0x001DC7B8
		public PivotGridFilterWindow(RadPivotGrid ownerPivotGrid) : base(ownerPivotGrid)
		{
		}

		// Token: 0x06008326 RID: 33574 RVA: 0x001DE5C1 File Offset: 0x001DC7C1
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.Title = this.GetFilterLocalizedValue("FilterWindow");
			this.CreateContainerPanel();
			this.CreateFilterMenu();
			this.CreateSetOptionsList();
			this.CreateSetBox();
			this.CreateOKButton();
			this.CreateCancelButton();
		}

		// Token: 0x06008327 RID: 33575 RVA: 0x001DE5FF File Offset: 0x001DC7FF
		private void CreateContainerPanel()
		{
			this.ContainerPanel.CssClass = "rpgFilterWindowContainer";
			base.ContentContainer.Controls.Add(this.ContainerPanel);
		}

		// Token: 0x06008328 RID: 33576 RVA: 0x001DE628 File Offset: 0x001DC828
		private void CreateOKButton()
		{
			if (this.ownerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				this.OKButton.Text = this.GetFilterLocalizedValue("FiltersWindowOKButton");
				this.OKButton.ToolTip = this.GetFilterLocalizedValue("FiltersWindowOKButton");
				this.OKButton.OnClientClick = "return false;";
				this.OKButton.CssClass = "rpgFilterButtonOk";
				this.ContainerPanel.Controls.Add(this.OKButton);
				return;
			}
			ElasticButton elasticButton = new ElasticButton(string.Empty, "rpgButtonText");
			elasticButton.ID = "btnOK";
			elasticButton.Text = this.GetFilterLocalizedValue("FiltersWindowOKButton");
			elasticButton.ToolTip = this.GetFilterLocalizedValue("FiltersWindowOKButton");
			elasticButton.OnClientClick = "return false;";
			elasticButton.CssClass = "rpgActionButton rpgFilterButtonOk";
			elasticButton.PostBackUrl = "#";
			this.ContainerPanel.Controls.Add(elasticButton);
		}

		// Token: 0x06008329 RID: 33577 RVA: 0x001DE718 File Offset: 0x001DC918
		private void CreateCancelButton()
		{
			if (this.ownerPivotGrid.ResolvedRenderMode == RenderMode.Classic)
			{
				this.CancelButton.Text = this.GetFilterLocalizedValue("FilterWindowCancelButton");
				this.CancelButton.ToolTip = this.GetFilterLocalizedValue("FilterWindowCancelButton");
				this.CancelButton.OnClientClick = "return false;";
				this.CancelButton.CssClass = "rpgFilterButtonCancel";
				this.ContainerPanel.Controls.Add(this.CancelButton);
				return;
			}
			ElasticButton elasticButton = new ElasticButton(string.Empty, "rpgButtonText");
			elasticButton.ID = "btnCancel";
			elasticButton.Text = this.GetFilterLocalizedValue("FilterWindowCancelButton");
			elasticButton.ToolTip = this.GetFilterLocalizedValue("FilterWindowCancelButton");
			elasticButton.OnClientClick = "return false;";
			elasticButton.CssClass = "rpgActionButton rpgFilterButtonCancel";
			elasticButton.PostBackUrl = "#";
			this.ContainerPanel.Controls.Add(elasticButton);
		}

		// Token: 0x0600832A RID: 33578 RVA: 0x001DE844 File Offset: 0x001DCA44
		private void CreateSetBox()
		{
			this.ContainerPanel.Controls.Add(this.SetBox);
			this.SetBox.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			this.SetBox.PreRender += delegate(object sender, EventArgs e)
			{
				RadListBox radListBox = sender as RadListBox;
				if (radListBox != null)
				{
					radListBox.Skin = this.ownerPivotGrid.RuntimeSkin;
					radListBox.EnableEmbeddedSkins = this.ownerPivotGrid.EnableEmbeddedSkins;
				}
			};
		}

		// Token: 0x0600832B RID: 33579 RVA: 0x001DE894 File Offset: 0x001DCA94
		private void CreateSetOptionsList()
		{
			this.SetOptions.RepeatLayout = RepeatLayout.Flow;
			this.SetOptions.RepeatDirection = RepeatDirection.Horizontal;
			this.SetOptions.CssClass = "rpgFilterCheckboxes";
			this.SetOptions.Items.Add(new ListItem(this.GetFilterLocalizedValue("Includes"))
			{
				Selected = true
			});
			this.SetOptions.Items.Add(new ListItem(this.GetFilterLocalizedValue("Excludes")));
			this.ContainerPanel.Controls.Add(this.SetOptions);
		}

		// Token: 0x0600832C RID: 33580 RVA: 0x001DE964 File Offset: 0x001DCB64
		private void CreateFilterMenu()
		{
			this.ContainerPanel.Controls.Add(this.FilterMenu);
			this.FilterMenu.RenderMode = this.ownerPivotGrid.ResolvedRenderMode;
			this.FilterMenu.PreRender += delegate(object sender, EventArgs e)
			{
				RadMenu radMenu = sender as RadMenu;
				if (radMenu != null)
				{
					radMenu.Skin = this.ownerPivotGrid.RuntimeSkin;
					radMenu.EnableEmbeddedSkins = this.ownerPivotGrid.EnableEmbeddedSkins;
				}
			};
			this.FilterMenu.Items.Clear();
			this.FilterMenu.Flow = ItemFlow.Vertical;
			this.FilterMenu.EnableScreenBoundaryDetection = false;
			RadMenuItem radMenuItem = new RadMenuItem();
			radMenuItem.Text = this.GetFilterLocalizedValue("ClearFilterFrom");
			radMenuItem.Value = "ClearFiltersFrom";
			this.FilterMenu.Items.Add(radMenuItem);
			this.CreateLabelFiltersMenu();
			this.CreateValueFiltersMenu();
		}

		// Token: 0x0600832D RID: 33581 RVA: 0x001DEA1C File Offset: 0x001DCC1C
		private void CreateLabelFiltersMenu()
		{
			PivotGridFilterFunction[] source = new PivotGridFilterFunction[]
			{
				PivotGridFilterFunction.Equals,
				PivotGridFilterFunction.BeginsWith,
				PivotGridFilterFunction.Contains,
				PivotGridFilterFunction.IsGreaterThan,
				PivotGridFilterFunction.IsBetween
			};
			PivotGridFilterFunction[] source2 = new PivotGridFilterFunction[]
			{
				PivotGridFilterFunction.Top,
				PivotGridFilterFunction.Bottom,
				PivotGridFilterFunction.Includes,
				PivotGridFilterFunction.DoesNotInclude
			};
			PivotGridFilterFunction[] source3 = new PivotGridFilterFunction[]
			{
				PivotGridFilterFunction.Contains,
				PivotGridFilterFunction.Equals,
				PivotGridFilterFunction.DoesNotEqual,
				PivotGridFilterFunction.IsBetween,
				PivotGridFilterFunction.DoesNotContain
			};
			int num = 0;
			PivotGridFilterFunction pivotGridFilterFunction = PivotGridFilterFunction.ClearFilters;
			RadMenuItem radMenuItem = new RadMenuItem();
			this.filterMenu.Items.Add(radMenuItem);
			radMenuItem.Text = this.GetFilterLocalizedValue("LabelFilters");
			radMenuItem.Value = "LabelFilters";
			foreach (string original in Enum.GetNames(typeof(PivotGridFilterFunction)))
			{
				if (source.Contains(pivotGridFilterFunction))
				{
					radMenuItem.Items.Add(new RadMenuItem
					{
						IsSeparator = true
					});
				}
				if (!source2.Contains(pivotGridFilterFunction))
				{
					RadMenuItem radMenuItem2 = new RadMenuItem(this.GetFilterLocalizedValue(original));
					radMenuItem2.Value = num.ToString();
					radMenuItem2.ID = string.Format("Item{0}", num);
					radMenuItem2.PostBack = false;
					radMenuItem.Items.Add(radMenuItem2);
					if (this.ownerPivotGrid.IsBoundToOlap && !source3.Contains(pivotGridFilterFunction))
					{
						radMenuItem2.Enabled = false;
					}
				}
				num++;
				pivotGridFilterFunction++;
			}
		}

		// Token: 0x0600832E RID: 33582 RVA: 0x001DEBA8 File Offset: 0x001DCDA8
		private void CreateValueFiltersMenu()
		{
			PivotGridFilterFunction[] source = new PivotGridFilterFunction[]
			{
				PivotGridFilterFunction.Equals,
				PivotGridFilterFunction.IsGreaterThan,
				PivotGridFilterFunction.IsBetween,
				PivotGridFilterFunction.Top
			};
			PivotGridFilterFunction[] source2 = new PivotGridFilterFunction[]
			{
				PivotGridFilterFunction.BeginsWith,
				PivotGridFilterFunction.DoesNotBeginWith,
				PivotGridFilterFunction.EndsWith,
				PivotGridFilterFunction.DoesNotEndWith,
				PivotGridFilterFunction.Contains,
				PivotGridFilterFunction.DoesNotContain,
				PivotGridFilterFunction.Bottom,
				PivotGridFilterFunction.Includes,
				PivotGridFilterFunction.DoesNotInclude
			};
			int num = 0;
			PivotGridFilterFunction pivotGridFilterFunction = PivotGridFilterFunction.ClearFilters;
			RadMenuItem radMenuItem = new RadMenuItem();
			this.FilterMenu.Items.Add(radMenuItem);
			radMenuItem.Text = this.GetFilterLocalizedValue("ValueFilters");
			radMenuItem.Value = "ValueFilters";
			foreach (string original in Enum.GetNames(typeof(PivotGridFilterFunction)))
			{
				if (source.Contains(pivotGridFilterFunction))
				{
					radMenuItem.Items.Add(new RadMenuItem
					{
						IsSeparator = true
					});
				}
				if (!source2.Contains(pivotGridFilterFunction))
				{
					RadMenuItem radMenuItem2 = new RadMenuItem(this.GetFilterLocalizedValue(original));
					radMenuItem2.Value = num.ToString();
					radMenuItem2.ID = string.Format("Item{0}", num);
					radMenuItem2.PostBack = false;
					radMenuItem.Items.Add(radMenuItem2);
					if (this.ownerPivotGrid.IsBoundToOlap && (pivotGridFilterFunction == PivotGridFilterFunction.Top || pivotGridFilterFunction == PivotGridFilterFunction.Bottom))
					{
						radMenuItem2.Enabled = false;
					}
				}
				num++;
				pivotGridFilterFunction++;
			}
		}

		// Token: 0x0600832F RID: 33583 RVA: 0x001DED1F File Offset: 0x001DCF1F
		internal string GetFilterLocalizedValue(string original)
		{
			return this.ownerPivotGrid.Localization.GetString(string.Format("{0}Text", original));
		}

		// Token: 0x1700296F RID: 10607
		// (get) Token: 0x06008330 RID: 33584 RVA: 0x001DED3C File Offset: 0x001DCF3C
		// (set) Token: 0x06008331 RID: 33585 RVA: 0x001DED53 File Offset: 0x001DCF53
		public string CustomType
		{
			get
			{
				return this.ViewState["CustomType"].ToString();
			}
			set
			{
				this.ViewState["CustomType"] = value;
			}
		}

		// Token: 0x17002970 RID: 10608
		// (get) Token: 0x06008332 RID: 33586 RVA: 0x001DED68 File Offset: 0x001DCF68
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

		// Token: 0x17002971 RID: 10609
		// (get) Token: 0x06008333 RID: 33587 RVA: 0x001DED9C File Offset: 0x001DCF9C
		// (set) Token: 0x06008334 RID: 33588 RVA: 0x001DEDCF File Offset: 0x001DCFCF
		public RadMenu FilterMenu
		{
			get
			{
				if (this.filterMenu == null)
				{
					this.filterMenu = new RadMenu
					{
						ID = "FilterMenu"
					};
				}
				return this.filterMenu;
			}
			set
			{
				this.filterMenu = value;
			}
		}

		// Token: 0x17002972 RID: 10610
		// (get) Token: 0x06008335 RID: 33589 RVA: 0x001DEDD8 File Offset: 0x001DCFD8
		public RadListBox SetBox
		{
			get
			{
				if (this.setBox == null)
				{
					this.setBox = new RadListBox
					{
						ID = "rlbFilterSet"
					};
				}
				return this.setBox;
			}
		}

		// Token: 0x17002973 RID: 10611
		// (get) Token: 0x06008336 RID: 33590 RVA: 0x001DEE0C File Offset: 0x001DD00C
		public RadioButtonList SetOptions
		{
			get
			{
				if (this.rblIncludes == null)
				{
					this.rblIncludes = new RadioButtonList
					{
						ID = "rblIncludes"
					};
				}
				return this.rblIncludes;
			}
		}

		// Token: 0x17002974 RID: 10612
		// (get) Token: 0x06008337 RID: 33591 RVA: 0x001DEE40 File Offset: 0x001DD040
		public Button CancelButton
		{
			get
			{
				if (this.btnCancel == null)
				{
					this.btnCancel = new Button
					{
						ID = "btnCancel"
					};
				}
				return this.btnCancel;
			}
		}

		// Token: 0x17002975 RID: 10613
		// (get) Token: 0x06008338 RID: 33592 RVA: 0x001DEE74 File Offset: 0x001DD074
		public Button OKButton
		{
			get
			{
				if (this.btnOK == null)
				{
					this.btnOK = new Button
					{
						ID = "btnOK"
					};
				}
				return this.btnOK;
			}
		}

		// Token: 0x17002976 RID: 10614
		// (get) Token: 0x06008339 RID: 33593 RVA: 0x001DEEA7 File Offset: 0x001DD0A7
		public Button FilterButton
		{
			get
			{
				return this.btnOK;
			}
		}

		// Token: 0x17002977 RID: 10615
		// (get) Token: 0x0600833A RID: 33594 RVA: 0x001DEEAF File Offset: 0x001DD0AF
		public bool IsReportFilter
		{
			get
			{
				return this.ownerPivotGrid.Fields.GetFieldByUniqueName(this.ownerPivotGrid.FilteringManager.FieldUniqueName) is PivotGridReportFilterField;
			}
		}

		// Token: 0x17002978 RID: 10616
		// (get) Token: 0x0600833B RID: 33595 RVA: 0x001DEED9 File Offset: 0x001DD0D9
		// (set) Token: 0x0600833C RID: 33596 RVA: 0x001DEEE1 File Offset: 0x001DD0E1
		[DefaultValue(false)]
		internal bool IsInAllFieldsZone
		{
			get
			{
				return this.isInAllFieldsZone;
			}
			set
			{
				this.isInAllFieldsZone = value;
			}
		}

		// Token: 0x17002979 RID: 10617
		// (get) Token: 0x0600833D RID: 33597 RVA: 0x001DEEEA File Offset: 0x001DD0EA
		internal PivotGridFieldZoneType ZoneType
		{
			get
			{
				return this.ownerPivotGrid.Fields.GetFieldByUniqueName(this.ownerPivotGrid.FilteringManager.FieldUniqueName).ZoneType;
			}
		}

		// Token: 0x0600833E RID: 33598 RVA: 0x001DEF14 File Offset: 0x001DD114
		public void InitializeControls()
		{
			if (!string.IsNullOrEmpty(this.ownerPivotGrid.FilteringManager.FieldUniqueName))
			{
				if (this.IsReportFilter)
				{
					this.FilterMenu.Visible = false;
				}
				else
				{
					this.FilterMenu.Visible = true;
				}
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				int index = -1;
				PivotGridField pivotGridField = this.ownerPivotGrid.Fields.GetFieldByUniqueName(this.ownerPivotGrid.FilteringManager.FieldUniqueName);
				if (pivotGridField == null)
				{
					try
					{
						pivotGridField = this.ownerPivotGrid.Fields.GetFieldByUniqueNameOutChildIndex(this.ownerPivotGrid.FilteringManager.FieldUniqueName, out index);
					}
					catch (Exception)
					{
					}
				}
				this.ownerPivotGrid.FilteringManager.FieldUniqueName = pivotGridField.UniqueName;
				pivotGridField.GetLevel();
				this.SetBox.CheckBoxes = true;
				this.OKButton.Enabled = true;
				this.CancelButton.Enabled = true;
				this.SetBox.Enabled = true;
				this.SetOptions.Enabled = true;
				IEnumerable<object> enumerable = null;
				if (pivotGridField is PivotGridGroupField)
				{
					enumerable = (pivotGridField as PivotGridGroupField).GetUniqueFilterItems(index);
				}
				else if (pivotGridField is PivotGridReportFilterField)
				{
					enumerable = (pivotGridField as PivotGridReportFilterField).GetUniqueFilterItems();
				}
				this.SetBox.Items.Clear();
				if (enumerable != null)
				{
					this.SetBox.Items.Add(new RadListBoxItem(this.GetFilterLocalizedValue("SelectAll"), "SelectAll")
					{
						Checked = true
					});
					int num = 0;
					foreach (object obj in enumerable)
					{
						RadListBoxItem radListBoxItem = new RadListBoxItem(obj.ToString(), javaScriptSerializer.Serialize(obj));
						if (num == 0)
						{
							string assemblyQualifiedName = obj.GetType().AssemblyQualifiedName;
							radListBoxItem.Attributes.Add("AQN", assemblyQualifiedName);
							num++;
						}
						radListBoxItem.Checked = true;
						this.SetBox.Items.Add(radListBoxItem);
					}
				}
			}
		}

		// Token: 0x04002458 RID: 9304
		private Panel pnlContainer;

		// Token: 0x04002459 RID: 9305
		private RadListBox setBox;

		// Token: 0x0400245A RID: 9306
		private Button btnOK;

		// Token: 0x0400245B RID: 9307
		private Button btnCancel;

		// Token: 0x0400245C RID: 9308
		private RadMenu filterMenu;

		// Token: 0x0400245D RID: 9309
		private RadioButtonList rblIncludes;

		// Token: 0x0400245E RID: 9310
		private bool isInAllFieldsZone;
	}
}
