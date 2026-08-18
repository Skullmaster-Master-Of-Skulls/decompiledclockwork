using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020004B5 RID: 1205
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	internal class GridBatchEditingHelper
	{
		// Token: 0x06002ADA RID: 10970 RVA: 0x0008AD4C File Offset: 0x00088F4C
		internal GridBatchEditingHelper(GridTableView tableView)
		{
			this.tableView = tableView;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0008AD5C File Offset: 0x00088F5C
		internal IGridColumnEditor GetColumnEditor(GridColumn column)
		{
			GridEditableColumn gridEditableColumn = column as GridEditableColumn;
			if (gridEditableColumn != null)
			{
				gridEditableColumn.CurrentColumnEditor.InitializeFromControl(this.GetEditorContainer(column));
				return gridEditableColumn.CurrentColumnEditor;
			}
			return null;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0008AD8D File Offset: 0x00088F8D
		internal IGridColumnEditor GetColumnEditor(string columnUniqueName)
		{
			return this.GetColumnEditor(this.tableView.GetColumnSafe(columnUniqueName));
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0008ADA1 File Offset: 0x00088FA1
		internal Panel GetEditorContainer(GridColumn column)
		{
			return this.GetEditorContainer(column.UniqueName);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0008ADAF File Offset: 0x00088FAF
		internal Panel GetEditorContainer(string columnUniqueName)
		{
			this.GetContainer(columnUniqueName);
			return this.GetContainer(columnUniqueName);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0008ADC0 File Offset: 0x00088FC0
		internal void CreateColumnEditorsPanel()
		{
			this.uploads = new Dictionary<string, RadAsyncUpload>();
			Panel panel = this.tableView.OwnerGrid.FindControl("BatchEditingContainer_" + this.tableView.ClientID) as Panel;
			int count = this.tableView.OwnerGrid.Controls.Count;
			if (panel == null)
			{
				panel = new Panel();
				panel.ID = "BatchEditingContainer_" + this.tableView.ClientID;
				this.tableView.OwnerGrid.Controls.AddAt(count, panel);
			}
			else
			{
				panel.Controls.Clear();
			}
			this.CreateEditors(panel);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0008AF40 File Offset: 0x00089140
		internal void CreateEditors(Panel tableViewPanel)
		{
			GridColumn[] renderColumns = this.tableView.RenderColumns;
			for (int i = 0; i < renderColumns.Length; i++)
			{
				GridBatchEditingHelper.<>c__DisplayClass3 CS$<>8__locals2 = new GridBatchEditingHelper.<>c__DisplayClass3();
				CS$<>8__locals2.column = renderColumns[i];
				GridEditableColumn editableColumn = CS$<>8__locals2.column as GridEditableColumn;
				if (editableColumn != null && editableColumn.Visible && (editableColumn.IsEditable || !editableColumn.ReadOnly || editableColumn.InsertVisiblityMode == GridColumnVisibilityMode.AlwaysVisible))
				{
					GridBatchEditingHelper.CreateFakeEditableItem(this.tableView, delegate(GridEditableItem dataItem)
					{
						GridBatchEditingHelper.PanelNamingContainer panelNamingContainer = new GridBatchEditingHelper.PanelNamingContainer();
						TableCell tableCell = new TableCell();
						dataItem.Cells.Clear();
						dataItem.Cells.Add(tableCell);
						tableCell.Controls.Add(panelNamingContainer);
						panelNamingContainer.ID = string.Format("{0}_{1}", this.tableView.ClientID, editableColumn.UniqueName);
						panelNamingContainer.CssClass = "rgBatchContainer";
						panelNamingContainer.Style.Add("display", "none");
						this.InitializeEditorControl(CS$<>8__locals2.column, panelNamingContainer);
						tableViewPanel.Controls.Add(panelNamingContainer);
					});
				}
			}
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0008B018 File Offset: 0x00089218
		internal RadAsyncUpload GetAsyncUpload(string name)
		{
			return this.uploads[name];
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0008B026 File Offset: 0x00089226
		private Panel GetContainer(string columnUniqueName)
		{
			return this.tableView.OwnerGrid.FindControl(string.Format("{0}_{1}", this.tableView.ClientID, columnUniqueName)) as Panel;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0008B054 File Offset: 0x00089254
		private void InitializeEditorControl(GridColumn column, WebControl container)
		{
			GridTemplateColumn gridTemplateColumn = column as GridTemplateColumn;
			GridBinaryImageColumn gridBinaryImageColumn = column as GridBinaryImageColumn;
			GridAttachmentColumn gridAttachmentColumn = column as GridAttachmentColumn;
			GridEditableColumn gridEditableColumn = column as GridEditableColumn;
			if (gridTemplateColumn != null)
			{
				if (gridTemplateColumn.EditItemTemplate != null && gridTemplateColumn.InsertItemTemplate != null)
				{
					GridBatchEditingHelper.PanelNamingContainer panelNamingContainer = new GridBatchEditingHelper.PanelNamingContainer();
					panelNamingContainer.ID = "Edit";
					panelNamingContainer.CssClass = "rgBatchContainer";
					panelNamingContainer.Style.Add("display", "none");
					GridBatchEditingHelper.PanelNamingContainer panelNamingContainer2 = new GridBatchEditingHelper.PanelNamingContainer();
					panelNamingContainer2.ID = "Insert";
					panelNamingContainer2.CssClass = "rgBatchContainer";
					panelNamingContainer2.Style.Add("display", "none");
					container.Controls.Add(panelNamingContainer);
					container.Controls.Add(panelNamingContainer2);
					gridTemplateColumn.EditItemTemplate.InstantiateIn(panelNamingContainer);
					gridTemplateColumn.InsertItemTemplate.InstantiateIn(panelNamingContainer2);
				}
				else if (gridTemplateColumn.EditItemTemplate != null)
				{
					gridTemplateColumn.EditItemTemplate.InstantiateIn(container);
				}
				else if (gridTemplateColumn.InsertItemTemplate != null)
				{
					gridTemplateColumn.InsertItemTemplate.InstantiateIn(container);
				}
			}
			else if (gridAttachmentColumn != null || gridBinaryImageColumn != null)
			{
				RadAsyncUpload radAsyncUpload = new RadAsyncUpload();
				container.Controls.Add(radAsyncUpload);
				radAsyncUpload.ID = "BatchEditingAsyncUpload";
				radAsyncUpload.RenderMode = column.Owner.OwnerGrid.RenderMode;
				radAsyncUpload.DisablePlugins = true;
				if (gridBinaryImageColumn != null)
				{
					radAsyncUpload.AllowedFileExtensions = new string[]
					{
						".gif",
						".jpg",
						".jpeg",
						".bmp",
						".png",
						".GIF",
						".JPG",
						".JPEG",
						".BMP",
						".PNG"
					};
				}
				this.uploads.Add(this.tableView.ClientID + column.UniqueName, radAsyncUpload);
			}
			else if (gridEditableColumn != null)
			{
				gridEditableColumn.CurrentColumnEditor.InitializeInControl(container);
				container.DataBind();
			}
			this.SetupValidation(container);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0008B26C File Offset: 0x0008946C
		private void SetupValidation(Control container)
		{
			foreach (object obj in container.Controls)
			{
				Control control = (Control)obj;
				BaseValidator baseValidator = control as BaseValidator;
				if (baseValidator != null)
				{
					baseValidator.ValidationGroup = GridBatchEditingHelper.ValidationGroupName;
				}
				this.SetupValidation(control);
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0008B2DC File Offset: 0x000894DC
		internal static bool IsBatchEditingEnabled(GridTableView tableView)
		{
			if (tableView.EditMode == GridEditMode.Batch)
			{
				return true;
			}
			foreach (GridTableView gridTableView in tableView.DetailTables)
			{
				if (gridTableView.EditMode == GridEditMode.Batch)
				{
					return true;
				}
				if (GridBatchEditingHelper.IsBatchEditingEnabled(gridTableView))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0008B350 File Offset: 0x00089550
		internal static bool IsHighlightingForDeletedRowsEnabled(GridTableView tableView)
		{
			if (tableView.BatchEditingSettings.HighlightDeletedRows)
			{
				return true;
			}
			foreach (GridTableView gridTableView in tableView.DetailTables)
			{
				if (tableView.BatchEditingSettings.HighlightDeletedRows)
				{
					return true;
				}
				if (GridBatchEditingHelper.IsHighlightingForDeletedRowsEnabled(gridTableView))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0008B3CC File Offset: 0x000895CC
		internal static string GenerateClientScript(GridTableView tableView, string functionName, params string[] functionParameters)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Format("$find('{0}').get_batchEditingManager().{1}(", tableView.OwnerGrid.ClientID, functionName));
			for (int i = 0; i < functionParameters.Length; i++)
			{
				stringBuilder.Append(string.Format("'{0}',", functionParameters[i]));
			}
			if (functionParameters.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append("); return false;");
			return stringBuilder.ToString();
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0008B440 File Offset: 0x00089640
		internal static bool IsColumnEditable(GridColumn column)
		{
			GridTemplateColumn gridTemplateColumn = column as GridTemplateColumn;
			return (column is GridEditableColumn && column.IsEditable) || (gridTemplateColumn != null && !gridTemplateColumn.ReadOnly && (gridTemplateColumn.EditItemTemplate != null || gridTemplateColumn.InsertItemTemplate != null));
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x0008B48C File Offset: 0x0008968C
		internal static void CreateFakeEditableItem(GridTableView tableView, Action<GridEditableItem> callback)
		{
			GridEditableItem gridEditableItem = new GridDataItem(tableView, 0, 0, GridItemType.EditItem);
			tableView.Controls.Add(gridEditableItem);
			gridEditableItem.SetTempIndexHierarchical("0");
			gridEditableItem.Initialize(tableView.RenderColumns);
			callback(gridEditableItem);
			tableView.Controls.Remove(gridEditableItem);
		}

		// Token: 0x04000B38 RID: 2872
		private readonly GridTableView tableView;

		// Token: 0x04000B39 RID: 2873
		private Dictionary<string, RadAsyncUpload> uploads;

		// Token: 0x04000B3A RID: 2874
		internal static readonly string ValidationGroupName = "BatchEditingValidationGroup";

		// Token: 0x020004B6 RID: 1206
		private class PanelNamingContainer : Panel, INamingContainer
		{
		}
	}
}
