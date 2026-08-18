using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200038C RID: 908
	internal class GridMobileGroupView : GridMobileView
	{
		// Token: 0x06001F5F RID: 8031 RVA: 0x00062FA4 File Offset: 0x000611A4
		public GridMobileGroupView(GridTableView tableView) : base(tableView)
		{
			this.CssClass = "rgMobileGroupForm";
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001F60 RID: 8032 RVA: 0x00062FC3 File Offset: 0x000611C3
		// (set) Token: 0x06001F61 RID: 8033 RVA: 0x00062FEE File Offset: 0x000611EE
		[Description("RadGrid Mobile View Groups Text")]
		[Category("Grouping")]
		[Localizable(true)]
		[DefaultValue("View Groups")]
		[NotifyParentProperty(true)]
		public string ViewGroupsText
		{
			get
			{
				if (!string.IsNullOrEmpty(this._viewGroupsText))
				{
					return this._viewGroupsText;
				}
				return base.TableView.OwnerGrid.Localization.MobileViewGroupsText;
			}
			set
			{
				this._viewGroupsText = value;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x00062FF7 File Offset: 0x000611F7
		public override GridMobileViewType Type
		{
			get
			{
				return GridMobileViewType.Group;
			}
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00062FFA File Offset: 0x000611FA
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.CreateBody();
			base.ChildControlsCreated = true;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x00063014 File Offset: 0x00061214
		protected override void CreateContent(HtmlGenericControl container)
		{
			foreach (GridGroupByExpression gridGroupByExpression in base.TableView.GroupByExpressions)
			{
				GridGroupByField gridGroupByField;
				if (gridGroupByExpression.SelectFields.Count > 0)
				{
					gridGroupByField = gridGroupByExpression.SelectFields[0];
				}
				else
				{
					gridGroupByField = gridGroupByExpression.GroupByFields[0];
				}
				string headerText = gridGroupByField.HeaderText;
				string innerText = (string.IsNullOrEmpty(gridGroupByField.FieldAlias) || gridGroupByField.FieldAlias == gridGroupByField.FieldName) ? (string.IsNullOrEmpty(headerText) ? gridGroupByField.FieldName : headerText) : gridGroupByField.FieldAlias;
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("label");
				htmlGenericControl.Attributes.Add("class", "rgLabel rgColumnItem");
				if (base.TableView.OwnerGrid.ClientSettings.AllowDragToGroup)
				{
					HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
					htmlGenericControl2.Attributes.Add("class", "rgDrag");
					htmlGenericControl.Controls.Add(htmlGenericControl2);
				}
				HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
				htmlGenericControl3.InnerText = innerText;
				string value = (gridGroupByField.SortOrder == GridSortOrder.Ascending) ? "rgSortAsc" : ((gridGroupByField.SortOrder == GridSortOrder.Descending) ? "rgSortDesc" : string.Empty);
				htmlGenericControl3.Attributes.Add("class", value);
				htmlGenericControl.Controls.Add(htmlGenericControl3);
				if (base.TableView.OwnerGrid.GroupingSettings.ShowUnGroupButton)
				{
					ElasticButton elasticButton = new ElasticButton();
					elasticButton.CssClass = "rgActionButton rgUngroup";
					elasticButton.ToolTip = "Ungroup";
					elasticButton.Text = "Ungroup";
					elasticButton.FirstSpanClass = "rgIcon rgUngroupIcon";
					htmlGenericControl.Controls.Add(elasticButton);
				}
				container.Controls.Add(htmlGenericControl);
			}
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x00063218 File Offset: 0x00061418
		protected override void DescribeProperties(ScriptControlDescriptor descriptor)
		{
			new JavaScriptSerializer();
			List<string> list = new List<string>();
			foreach (GridGroupByExpression gridGroupByExpression in base.TableView.GroupByExpressions)
			{
				GridGroupByField gridGroupByField;
				if (gridGroupByExpression.SelectFields.Count > 0)
				{
					gridGroupByField = gridGroupByExpression.SelectFields[0];
				}
				else
				{
					gridGroupByField = gridGroupByExpression.GroupByFields[0];
				}
				string fieldName = gridGroupByField.FieldName;
				list.Add(fieldName);
			}
			descriptor.AddProperty("_groupFieldNames", list);
		}

		// Token: 0x04000806 RID: 2054
		private string _viewGroupsText = string.Empty;
	}
}
