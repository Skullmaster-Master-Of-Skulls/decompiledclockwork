using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DEE RID: 3566
	public class PivotGridConfigurationPanel : CompositeControl
	{
		// Token: 0x0600847A RID: 33914 RVA: 0x001E31EC File Offset: 0x001E13EC
		public PivotGridConfigurationPanel(RadPivotGrid ownerPivotGrid)
		{
			this.ownerPivotGrid = ownerPivotGrid;
			if (!base.DesignMode)
			{
				this.EnableTheming = ownerPivotGrid.EnableTheming;
			}
			else
			{
				this.OwnerPivotGrid.Controls.Add(this);
			}
			this.Visible = !base.DesignMode;
			this.olapFieldsList = new PivotGridOlapHierarhicalFieldsList(this);
		}

		// Token: 0x170029E5 RID: 10725
		// (get) Token: 0x0600847B RID: 33915 RVA: 0x001E3248 File Offset: 0x001E1448
		private bool IsUsingOLAP
		{
			get
			{
				return this.OwnerPivotGrid.IsBoundToAdomd || this.OwnerPivotGrid.IsBoundToXmla;
			}
		}

		// Token: 0x170029E6 RID: 10726
		// (get) Token: 0x0600847C RID: 33916 RVA: 0x001E3264 File Offset: 0x001E1464
		public RadPivotGrid OwnerPivotGrid
		{
			get
			{
				return this.ownerPivotGrid;
			}
		}

		// Token: 0x170029E7 RID: 10727
		// (get) Token: 0x0600847D RID: 33917 RVA: 0x001E326C File Offset: 0x001E146C
		[DefaultValue(typeof(PivotGridConfigurationPanelPosition), "FieldsWindow")]
		public PivotGridConfigurationPanelPosition Position
		{
			get
			{
				return this.OwnerPivotGrid.ConfigurationPanelSettings.Position;
			}
		}

		// Token: 0x170029E8 RID: 10728
		// (get) Token: 0x0600847E RID: 33918 RVA: 0x001E327E File Offset: 0x001E147E
		public PivotGridConfigurationPanelLayoutType LayoutType
		{
			get
			{
				return this.OwnerPivotGrid.ConfigurationPanelSettings.LayoutType;
			}
		}

		// Token: 0x170029E9 RID: 10729
		// (get) Token: 0x0600847F RID: 33919 RVA: 0x001E3290 File Offset: 0x001E1490
		public CheckBox DeferLayoutUpdateCheckBox
		{
			get
			{
				if (this.deferLayoutUpdateCheckBox == null)
				{
					this.deferLayoutUpdateCheckBox = new CheckBox();
				}
				return this.deferLayoutUpdateCheckBox;
			}
		}

		// Token: 0x170029EA RID: 10730
		// (get) Token: 0x06008480 RID: 33920 RVA: 0x001E32AB File Offset: 0x001E14AB
		public Button ChangeLayoutButton
		{
			get
			{
				if (this.changeLayoutButton == null)
				{
					this.changeLayoutButton = new Button();
				}
				return this.changeLayoutButton;
			}
		}

		// Token: 0x170029EB RID: 10731
		// (get) Token: 0x06008481 RID: 33921 RVA: 0x001E32C6 File Offset: 0x001E14C6
		public HtmlAnchor ChangeLayoutLightweightButton
		{
			get
			{
				if (this.changeLayoutLightweightButton == null)
				{
					this.changeLayoutLightweightButton = new HtmlAnchor();
				}
				return this.changeLayoutLightweightButton;
			}
		}

		// Token: 0x170029EC RID: 10732
		// (get) Token: 0x06008482 RID: 33922 RVA: 0x001E32E1 File Offset: 0x001E14E1
		public Button UpdateButton
		{
			get
			{
				if (this.updateButton == null)
				{
					this.updateButton = new Button();
				}
				return this.updateButton;
			}
		}

		// Token: 0x170029ED RID: 10733
		// (get) Token: 0x06008483 RID: 33923 RVA: 0x001E32FC File Offset: 0x001E14FC
		public RadTreeView TreeView
		{
			get
			{
				if (this.treeView == null)
				{
					this.treeView = new RadTreeView();
				}
				return this.treeView;
			}
		}

		// Token: 0x170029EE RID: 10734
		// (get) Token: 0x06008484 RID: 33924 RVA: 0x001E3317 File Offset: 0x001E1517
		internal List<PivotGridFieldRenderingControl> RenderingControls
		{
			get
			{
				return this.renderingControls;
			}
		}

		// Token: 0x170029EF RID: 10735
		// (get) Token: 0x06008485 RID: 33925 RVA: 0x001E331F File Offset: 0x001E151F
		private PivotGridStrings Localization
		{
			get
			{
				return this.ownerPivotGrid.Localization;
			}
		}

		// Token: 0x170029F0 RID: 10736
		// (get) Token: 0x06008486 RID: 33926 RVA: 0x001E332C File Offset: 0x001E152C
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x170029F1 RID: 10737
		// (get) Token: 0x06008487 RID: 33927 RVA: 0x001E333A File Offset: 0x001E153A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06008488 RID: 33928 RVA: 0x001E333E File Offset: 0x001E153E
		protected override void CreateChildControls()
		{
			this.Initialize();
			base.CreateChildControls();
		}

		// Token: 0x06008489 RID: 33929 RVA: 0x001E334C File Offset: 0x001E154C
		internal void Initialize()
		{
			this.Controls.Clear();
			if (this.IsUsingOLAP)
			{
				if (this.FlatOlapUncategoriezedFields != this.OwnerPivotGrid.ConfigurationPanelSettings.FlattenOlapUncategoriezedFields)
				{
					this.TreeView.Nodes.Clear();
				}
				this.Controls.Add(this.TreeView);
				this.TreeView.ID = "RadTreeViewOLAP";
				this.TreeView.Height = Unit.Percentage(100.0);
				this.TreeView.RenderMode = this.OwnerPivotGrid.ResolvedRenderMode;
				this.olapFieldsList.Initialize();
				this.FlatOlapUncategoriezedFields = this.OwnerPivotGrid.ConfigurationPanelSettings.FlattenOlapUncategoriezedFields;
			}
			this.GenerateRenderingControls();
			if (this.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.Controls.Add(this.ChangeLayoutLightweightButton);
				this.ChangeLayoutButton.Attributes["onclick"] = "return false;";
				this.ChangeLayoutLightweightButton.ID = "ChangeLayoutButton";
				this.ChangeLayoutLightweightButton.Attributes["class"] = "rpgIcon rpgChangeLayoutIcon";
				this.ChangeLayoutLightweightButton.Title = this.Localization.ConfigurationPanelChangeLayoutButtonText;
			}
			else
			{
				this.Controls.Add(this.ChangeLayoutButton);
				this.ChangeLayoutButton.Attributes["onclick"] = "return false;";
				this.ChangeLayoutButton.ID = "ChangeLayoutButton";
				this.ChangeLayoutButton.CssClass = "rpgChangeLayoutButton";
				this.ChangeLayoutButton.Text = this.Localization.ConfigurationPanelChangeLayoutButtonText;
				this.ChangeLayoutButton.ToolTip = this.Localization.ConfigurationPanelChangeLayoutButtonText;
			}
			this.Controls.Add(this.DeferLayoutUpdateCheckBox);
			this.DeferLayoutUpdateCheckBox.ID = "DeferLayoutUpdateCheckBox";
			this.DeferLayoutUpdateCheckBox.Text = this.Localization.ConfigurationPanelDeferLayoutUpdateCheckBoxText;
			if (!base.ChildControlsCreated)
			{
				this.DeferLayoutUpdateCheckBox.Checked = this.OwnerPivotGrid.ConfigurationPanelSettings.DefaultDeferedLayoutUpdate;
			}
			if (this.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.lightUpdateButton = new ElasticButton(string.Empty, "rpgButtonText");
				this.Controls.Add(this.lightUpdateButton);
				this.lightUpdateButton.Text = this.Localization.ConfigurationPanelUpdateButtonText;
				this.lightUpdateButton.ToolTip = this.Localization.ConfigurationPanelUpdateButtonText;
				this.lightUpdateButton.CssClass = "rpgUpdateButton";
				this.lightUpdateButton.PostBackUrl = "#";
				this.lightUpdateButton.ID = "UpdateButton";
				this.lightUpdateButton.OnClientClick = string.Format("$find('{0}').update(false);return false;", this.ClientID);
				this.lightUpdateButton.Attributes.Add("disabled", "disabled");
				return;
			}
			this.Controls.Add(this.UpdateButton);
			this.UpdateButton.Text = this.Localization.ConfigurationPanelUpdateButtonText;
			this.UpdateButton.ToolTip = this.Localization.ConfigurationPanelUpdateButtonText;
			this.UpdateButton.CssClass = "rpgUpdateButton";
			this.UpdateButton.ID = "UpdateButton";
			this.UpdateButton.OnClientClick = string.Format("$find('{0}').update(false);return false;", this.ClientID);
			this.UpdateButton.Attributes.Add("disabled", "disabled");
		}

		// Token: 0x0600848A RID: 33930 RVA: 0x001E36B8 File Offset: 0x001E18B8
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string str = "rpgConfigurationPanel " + string.Format("rpg{0}ConfigurationPanel", this.Position) + " " + string.Format("rpg{0}ConfigurationPanel", this.LayoutType);
			string cssClass = this.CssClass;
			this.CssClass = (str + " " + cssClass).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x0600848B RID: 33931 RVA: 0x001E37D4 File Offset: 0x001E19D4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.ChangeLayoutLightweightButton.RenderControl(writer);
			}
			else
			{
				this.ChangeLayoutButton.RenderControl(writer);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpgFieldsWrapper");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			this.RenderFieldsContainer(writer, "All", from f in this.RenderingControls
			where f.AllowShowHide
			select f);
			this.RenderFieldsContainer(writer, PivotGridFieldZoneType.Filter.ToString(), from f in this.RenderingControls
			where f.OwnerField.ZoneType == PivotGridFieldZoneType.Filter && !f.AllowShowHide
			orderby f.OwnerField.ZoneIndex
			select f);
			this.RenderFieldsContainer(writer, PivotGridFieldZoneType.Row.ToString(), from f in this.RenderingControls
			where f.OwnerField.ZoneType == PivotGridFieldZoneType.Row && !f.AllowShowHide
			orderby f.OwnerField.ZoneIndex
			select f);
			this.RenderFieldsContainer(writer, PivotGridFieldZoneType.Column.ToString(), from f in this.RenderingControls
			where f.OwnerField.ZoneType == PivotGridFieldZoneType.Column && !f.AllowShowHide
			orderby f.OwnerField.ZoneIndex
			select f);
			this.RenderFieldsContainer(writer, PivotGridFieldZoneType.Aggregate.ToString(), from f in this.RenderingControls
			where f.OwnerField.ZoneType == PivotGridFieldZoneType.Aggregate && !f.AllowShowHide
			orderby f.OwnerField.ZoneIndex
			select f);
			if (this.OwnerPivotGrid.ResolvedRenderMode != RenderMode.Lightweight)
			{
				writer.RenderEndTag();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpgUpdate");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.DeferLayoutUpdateCheckBox.RenderControl(writer);
			if (this.ownerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.lightUpdateButton.RenderControl(writer);
			}
			else
			{
				this.UpdateButton.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600848C RID: 33932 RVA: 0x001E3A28 File Offset: 0x001E1C28
		private void RenderFieldsContainer(HtmlTextWriter writer, string name, IEnumerable<PivotGridFieldRenderingControl> fields)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rpg{0}FieldsContainer", name));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}FieldsIcon", name));
			string str = (this.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight) ? "rpgIcon " : string.Empty;
			writer.AddAttribute(HtmlTextWriterAttribute.Class, str + "rpg" + string.Format("{0}FieldsIcon", name));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			string @string = this.OwnerPivotGrid.Localization.GetString("ConfigurationPanel" + name + "FieldsText");
			writer.Write(@string);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpgFieldsContainer");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this.IsUsingOLAP && name.Equals("All"))
			{
				this.TreeView.RenderControl(writer);
			}
			else
			{
				int num = 0;
				bool flag = false;
				foreach (PivotGridFieldRenderingControl pivotGridFieldRenderingControl in fields)
				{
					if (!flag && this.ShouldRenderValuesField(name, num))
					{
						this.RenderValuesField(writer);
						flag = true;
					}
					pivotGridFieldRenderingControl.RenderControl(writer);
					num++;
				}
				if (!flag && this.ShouldRenderValuesField(name, this.OwnerPivotGrid.AggregatesLevel))
				{
					this.RenderValuesField(writer);
				}
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600848D RID: 33933 RVA: 0x001E3B94 File Offset: 0x001E1D94
		private bool ShouldRenderValuesField(string zoneTypeName, int index)
		{
			PivotGridFieldZoneType pivotGridFieldZoneType = PivotGridFieldZoneType.Aggregate;
			try
			{
				pivotGridFieldZoneType = (PivotGridFieldZoneType)Enum.Parse(typeof(PivotGridFieldZoneType), zoneTypeName);
			}
			catch
			{
				return false;
			}
			return this.OwnerPivotGrid.AggregatesLevel == index && ((pivotGridFieldZoneType == PivotGridFieldZoneType.Row && this.OwnerPivotGrid.AggregatesPosition == PivotGridAxis.Rows) || (pivotGridFieldZoneType == PivotGridFieldZoneType.Column && this.OwnerPivotGrid.AggregatesPosition == PivotGridAxis.Columns));
		}

		// Token: 0x0600848E RID: 33934 RVA: 0x001E3C20 File Offset: 0x001E1E20
		private void RenderValuesField(HtmlTextWriter writer)
		{
			int num = this.OwnerPivotGrid.Fields.Count((PivotGridField f) => f.ZoneType == PivotGridFieldZoneType.Aggregate && !f.IsHidden);
			if (num < 2)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Aggregate");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rpgFieldItem rpgValues");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("Values");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600848F RID: 33935 RVA: 0x001E3CB8 File Offset: 0x001E1EB8
		private void GenerateRenderingControls()
		{
			this.renderingControls = new List<PivotGridFieldRenderingControl>();
			foreach (PivotGridField pivotGridField in this.OwnerPivotGrid.Fields)
			{
				PivotGridFieldRenderingControl pivotGridFieldRenderingControl;
				if (!this.IsUsingOLAP)
				{
					pivotGridFieldRenderingControl = new PivotGridFieldRenderingControl(pivotGridField, -1);
					this.Controls.Add(pivotGridFieldRenderingControl);
					pivotGridFieldRenderingControl.IsConfigurationPanelField = true;
					pivotGridFieldRenderingControl.AllowShowHide = true;
					pivotGridFieldRenderingControl.ID = pivotGridField.UniqueName + "_Hidden";
					this.renderingControls.Add(pivotGridFieldRenderingControl);
				}
				pivotGridFieldRenderingControl = new PivotGridFieldRenderingControl(pivotGridField, -1);
				this.Controls.Add(pivotGridFieldRenderingControl);
				pivotGridFieldRenderingControl.IsConfigurationPanelField = true;
				pivotGridFieldRenderingControl.ID = pivotGridField.UniqueName;
				if (pivotGridFieldRenderingControl.OwnerField.IsHidden && !pivotGridFieldRenderingControl.AllowShowHide)
				{
					pivotGridFieldRenderingControl.Style[HtmlTextWriterStyle.Display] = "none";
				}
				else
				{
					pivotGridFieldRenderingControl.Style.Remove("display");
				}
				this.renderingControls.Add(pivotGridFieldRenderingControl);
				int num = 0;
				foreach (string id in pivotGridField.FlatChildOlapInfoNames)
				{
					pivotGridFieldRenderingControl = new PivotGridFieldRenderingControl(pivotGridField, num);
					this.Controls.Add(pivotGridFieldRenderingControl);
					pivotGridFieldRenderingControl.IsConfigurationPanelField = true;
					pivotGridFieldRenderingControl.ID = id;
					if (pivotGridFieldRenderingControl.OwnerField.IsHidden && !pivotGridFieldRenderingControl.AllowShowHide)
					{
						pivotGridFieldRenderingControl.Style[HtmlTextWriterStyle.Display] = "none";
					}
					else
					{
						pivotGridFieldRenderingControl.Style.Remove("display");
					}
					this.renderingControls.Add(pivotGridFieldRenderingControl);
					num++;
				}
			}
			if (this.renderingControls.Count == 0)
			{
				PivotGridField pivotGridField2 = new PivotGridRowField();
				pivotGridField2.SetOwner(this.OwnerPivotGrid);
				pivotGridField2.UniqueName = "FakeField";
				pivotGridField2.DataField = "FakeField";
				pivotGridField2.IsHidden = true;
				PivotGridFieldRenderingControl pivotGridFieldRenderingControl = new PivotGridFieldRenderingControl(pivotGridField2, -1);
				this.Controls.Add(pivotGridFieldRenderingControl);
				pivotGridFieldRenderingControl.ID = "FakeField";
				pivotGridFieldRenderingControl.IsConfigurationPanelField = true;
				pivotGridFieldRenderingControl.Style[HtmlTextWriterStyle.Display] = "none";
				this.renderingControls.Add(pivotGridFieldRenderingControl);
			}
		}

		// Token: 0x06008490 RID: 33936 RVA: 0x001E3F18 File Offset: 0x001E2118
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			if (args is CommandEventArgs && !(args is PivotGridCommandEventArgs) && this.OwnerPivotGrid.Items.Count > 0)
			{
				this.CheckTargetZone(source);
				PivotGridCommandEventArgs args2 = PivotGridCommandEventArgsFactory.CreateCommandEventArgs(this.OwnerPivotGrid.Items[0], source, args as CommandEventArgs);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x06008491 RID: 33937 RVA: 0x001E3F80 File Offset: 0x001E2180
		private void CheckTargetZone(object source)
		{
			Control control = source as Control;
			if (control != null)
			{
				Control parent = control.Parent;
				if (parent != null && parent.ID.EndsWith("_Hidden"))
				{
					this.OwnerPivotGrid.FilterWindow.IsInAllFieldsZone = true;
				}
			}
		}

		// Token: 0x040024CB RID: 9419
		private const string ConfigPanelFieldsWrapper = "rpgFieldsWrapper";

		// Token: 0x040024CC RID: 9420
		private const string ClassName = "rpgConfigurationPanel";

		// Token: 0x040024CD RID: 9421
		private const string ChangeLayoutButtonClassName = "rpgChangeLayoutButton";

		// Token: 0x040024CE RID: 9422
		private const string ChangeLayoutMenuClassName = "rpgMenuChangeLayout";

		// Token: 0x040024CF RID: 9423
		private const string DeferLayoutUpdatePanelClassName = "rpgUpdate";

		// Token: 0x040024D0 RID: 9424
		private const string FieldPanelFormatStringClassName = "rpg{0}FieldsContainer";

		// Token: 0x040024D1 RID: 9425
		private const string FieldIconFormatStringClassName = "{0}FieldsIcon";

		// Token: 0x040024D2 RID: 9426
		private const string FieldsPanelClassName = "rpgFieldsContainer";

		// Token: 0x040024D3 RID: 9427
		private const string UpdateButtonClassName = "rpgUpdateButton";

		// Token: 0x040024D4 RID: 9428
		private RadPivotGrid ownerPivotGrid;

		// Token: 0x040024D5 RID: 9429
		private CheckBox deferLayoutUpdateCheckBox;

		// Token: 0x040024D6 RID: 9430
		private Button changeLayoutButton;

		// Token: 0x040024D7 RID: 9431
		private HtmlAnchor changeLayoutLightweightButton;

		// Token: 0x040024D8 RID: 9432
		private Button updateButton;

		// Token: 0x040024D9 RID: 9433
		private ElasticButton lightUpdateButton;

		// Token: 0x040024DA RID: 9434
		private RadTreeView treeView;

		// Token: 0x040024DB RID: 9435
		private List<PivotGridFieldRenderingControl> renderingControls;

		// Token: 0x040024DC RID: 9436
		private PivotGridOlapHierarhicalFieldsList olapFieldsList;

		// Token: 0x040024DD RID: 9437
		private bool FlatOlapUncategoriezedFields;
	}
}
