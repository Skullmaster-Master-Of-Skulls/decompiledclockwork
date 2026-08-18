using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000109 RID: 265
	internal class SqlDataSourceConfigureParametersPanel : WizardPanel
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x00037538 File Offset: 0x00035738
		public SqlDataSourceConfigureParametersPanel(SqlDataSourceDesigner sqlDataSourceDesigner)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this.InitializeComponent();
			this.InitializeUI();
			this._parameterEditorUserControl.SetAllowCollectionChanges(false);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00037560 File Offset: 0x00035760
		private static Parameter CreateMergedParameter(Parameter parameter, List<Parameter> unusedOldParameters)
		{
			Parameter parameter2 = null;
			foreach (Parameter parameter3 in unusedOldParameters)
			{
				if (SqlDataSourceConfigureParametersPanel.ParametersMatch(parameter, parameter3))
				{
					parameter2 = parameter3;
					break;
				}
			}
			if (parameter2 != null)
			{
				unusedOldParameters.Remove(parameter2);
			}
			else
			{
				parameter2 = parameter;
			}
			return parameter2;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000375C8 File Offset: 0x000357C8
		private void InitializeComponent()
		{
			this._previewTextBox = new System.Windows.Forms.TextBox();
			this._previewLabel = new System.Windows.Forms.Label();
			this._helpLabel = new System.Windows.Forms.Label();
			this._parameterEditorUserControl = new ParameterEditorUserControl(this._sqlDataSourceDesigner.Component.Site, (SqlDataSource)this._sqlDataSourceDesigner.Component);
			base.SuspendLayout();
			this._helpLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this._helpLabel.Location = new Point(0, 0);
			this._helpLabel.Name = "_helpLabel";
			this._helpLabel.Size = new Size(544, 32);
			this._helpLabel.TabIndex = 10;
			this._parameterEditorUserControl.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._parameterEditorUserControl.Location = new Point(0, 38);
			this._parameterEditorUserControl.Name = "_parameterEditorUserControl";
			this._parameterEditorUserControl.Size = new Size(544, 152);
			this._parameterEditorUserControl.TabIndex = 20;
			this._parameterEditorUserControl.ParametersChanged += this.OnParameterEditorUserControlParametersChanged;
			this._previewLabel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._previewLabel.Location = new Point(0, 214);
			this._previewLabel.Name = "_previewLabel";
			this._previewLabel.Size = new Size(544, 16);
			this._previewLabel.TabIndex = 30;
			this._previewTextBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._previewTextBox.BackColor = SystemColors.Control;
			this._previewTextBox.Location = new Point(0, 232);
			this._previewTextBox.Multiline = true;
			this._previewTextBox.Name = "_previewTextBox";
			this._previewTextBox.ReadOnly = true;
			this._previewTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._previewTextBox.Size = new Size(544, 42);
			this._previewTextBox.TabIndex = 40;
			this._previewTextBox.Text = "";
			base.Controls.Add(this._parameterEditorUserControl);
			base.Controls.Add(this._helpLabel);
			base.Controls.Add(this._previewLabel);
			base.Controls.Add(this._previewTextBox);
			base.Name = "SqlDataSourceConfigureParametersPanel";
			base.Size = new Size(544, 274);
			base.ResumeLayout(false);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00037845 File Offset: 0x00035A45
		public void InitializeParameters(Parameter[] selectParameters)
		{
			this._parameterEditorUserControl.AddParameters(selectParameters);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00037853 File Offset: 0x00035A53
		private void InitializeUI()
		{
			base.Caption = SR.GetString("SqlDataSourceConfigureParametersPanel_PanelCaption");
			this._helpLabel.Text = SR.GetString("SqlDataSourceConfigureParametersPanel_HelpLabel");
			this._previewLabel.Text = SR.GetString("SqlDataSource_General_PreviewLabel");
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00037890 File Offset: 0x00035A90
		private static Parameter[] MergeParameters(Parameter[] oldParameters, Parameter[] newParameters)
		{
			Parameter[] array = new Parameter[newParameters.Length];
			List<Parameter> list = new List<Parameter>();
			foreach (Parameter item in oldParameters)
			{
				list.Add(item);
			}
			for (int j = 0; j < newParameters.Length; j++)
			{
				array[j] = SqlDataSourceConfigureParametersPanel.CreateMergedParameter(newParameters[j], list);
			}
			return array;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000378EC File Offset: 0x00035AEC
		public override bool OnNext()
		{
			SqlDataSourceQuery selectQuery = new SqlDataSourceQuery(this._selectQuery.Command, this._selectQuery.CommandType, this._parameterEditorUserControl.GetParameters());
			SqlDataSourceSummaryPanel sqlDataSourceSummaryPanel = base.NextPanel as SqlDataSourceSummaryPanel;
			if (sqlDataSourceSummaryPanel == null)
			{
				sqlDataSourceSummaryPanel = ((SqlDataSourceWizardForm)base.ParentWizard).GetSummaryPanel();
				base.NextPanel = sqlDataSourceSummaryPanel;
			}
			sqlDataSourceSummaryPanel.SetQueries(this._dataConnection, selectQuery, this._insertQuery, this._updateQuery, this._deleteQuery);
			return true;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00037967 File Offset: 0x00035B67
		private void OnParameterEditorUserControlParametersChanged(object sender, EventArgs e)
		{
			this.UpdateUI();
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00003937 File Offset: 0x00001B37
		public override void OnPrevious()
		{
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0003796F File Offset: 0x00035B6F
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				this.UpdateUI();
				base.ParentWizard.FinishButton.Enabled = false;
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00037998 File Offset: 0x00035B98
		private static bool ParametersMatch(Parameter parameter1, Parameter parameter2)
		{
			return string.Equals(parameter1.Name, parameter2.Name, StringComparison.OrdinalIgnoreCase) && parameter1.Direction == parameter2.Direction && parameter1.DbType == parameter2.DbType && (((parameter1.Type == TypeCode.Object || parameter1.Type == TypeCode.Empty) && (parameter2.Type == TypeCode.Object || parameter2.Type == TypeCode.Empty)) || parameter1.Type == parameter2.Type);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00037A0D File Offset: 0x00035C0D
		public void ResetUI()
		{
			this._parameterEditorUserControl.ClearParameters();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00037A1C File Offset: 0x00035C1C
		public void SetQueries(DesignerDataConnection dataConnection, SqlDataSourceQuery selectQuery, SqlDataSourceQuery insertQuery, SqlDataSourceQuery updateQuery, SqlDataSourceQuery deleteQuery)
		{
			this._dataConnection = dataConnection;
			this._selectQuery = selectQuery;
			this._insertQuery = insertQuery;
			this._updateQuery = updateQuery;
			this._deleteQuery = deleteQuery;
			this._previewTextBox.Text = this._selectQuery.Command;
			Parameter[] array = new Parameter[this._selectQuery.Parameters.Count];
			this._selectQuery.Parameters.CopyTo(array, 0);
			Parameter[] parameters = SqlDataSourceConfigureParametersPanel.MergeParameters(this._parameterEditorUserControl.GetParameters(), array);
			this._parameterEditorUserControl.ClearParameters();
			this._parameterEditorUserControl.AddParameters(parameters);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00037AB5 File Offset: 0x00035CB5
		private void UpdateUI()
		{
			base.ParentWizard.NextButton.Enabled = this._parameterEditorUserControl.ParametersConfigured;
		}

		// Token: 0x0400058F RID: 1423
		private System.Windows.Forms.TextBox _previewTextBox;

		// Token: 0x04000590 RID: 1424
		private System.Windows.Forms.Label _previewLabel;

		// Token: 0x04000591 RID: 1425
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x04000592 RID: 1426
		private ParameterEditorUserControl _parameterEditorUserControl;

		// Token: 0x04000593 RID: 1427
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04000594 RID: 1428
		private DesignerDataConnection _dataConnection;

		// Token: 0x04000595 RID: 1429
		private SqlDataSourceQuery _selectQuery;

		// Token: 0x04000596 RID: 1430
		private SqlDataSourceQuery _insertQuery;

		// Token: 0x04000597 RID: 1431
		private SqlDataSourceQuery _updateQuery;

		// Token: 0x04000598 RID: 1432
		private SqlDataSourceQuery _deleteQuery;
	}
}
