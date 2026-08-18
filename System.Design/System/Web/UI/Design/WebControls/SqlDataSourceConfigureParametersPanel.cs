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
	// Token: 0x020004BF RID: 1215
	internal class SqlDataSourceConfigureParametersPanel : WizardPanel
	{
		// Token: 0x06002BEC RID: 11244 RVA: 0x000F34D7 File Offset: 0x000F24D7
		public SqlDataSourceConfigureParametersPanel(SqlDataSourceDesigner sqlDataSourceDesigner)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
			this.InitializeComponent();
			this.InitializeUI();
			this._parameterEditorUserControl.SetAllowCollectionChanges(false);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000F3500 File Offset: 0x000F2500
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

		// Token: 0x06002BEE RID: 11246 RVA: 0x000F3568 File Offset: 0x000F2568
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

		// Token: 0x06002BEF RID: 11247 RVA: 0x000F37E5 File Offset: 0x000F27E5
		public void InitializeParameters(Parameter[] selectParameters)
		{
			this._parameterEditorUserControl.AddParameters(selectParameters);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000F37F3 File Offset: 0x000F27F3
		private void InitializeUI()
		{
			base.Caption = SR.GetString("SqlDataSourceConfigureParametersPanel_PanelCaption");
			this._helpLabel.Text = SR.GetString("SqlDataSourceConfigureParametersPanel_HelpLabel");
			this._previewLabel.Text = SR.GetString("SqlDataSource_General_PreviewLabel");
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000F3830 File Offset: 0x000F2830
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

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000F388C File Offset: 0x000F288C
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

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000F3907 File Offset: 0x000F2907
		private void OnParameterEditorUserControlParametersChanged(object sender, EventArgs e)
		{
			this.UpdateUI();
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000F390F File Offset: 0x000F290F
		public override void OnPrevious()
		{
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000F3911 File Offset: 0x000F2911
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (base.Visible)
			{
				this.UpdateUI();
				base.ParentWizard.FinishButton.Enabled = false;
			}
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000F393C File Offset: 0x000F293C
		private static bool ParametersMatch(Parameter parameter1, Parameter parameter2)
		{
			return string.Equals(parameter1.Name, parameter2.Name, StringComparison.OrdinalIgnoreCase) && parameter1.Direction == parameter2.Direction && parameter1.DbType == parameter2.DbType && (((parameter1.Type == TypeCode.Object || parameter1.Type == TypeCode.Empty) && (parameter2.Type == TypeCode.Object || parameter2.Type == TypeCode.Empty)) || parameter1.Type == parameter2.Type);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000F39B1 File Offset: 0x000F29B1
		public void ResetUI()
		{
			this._parameterEditorUserControl.ClearParameters();
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000F39C0 File Offset: 0x000F29C0
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

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000F3A59 File Offset: 0x000F2A59
		private void UpdateUI()
		{
			base.ParentWizard.NextButton.Enabled = this._parameterEditorUserControl.ParametersConfigured;
		}

		// Token: 0x04001DDA RID: 7642
		private System.Windows.Forms.TextBox _previewTextBox;

		// Token: 0x04001DDB RID: 7643
		private System.Windows.Forms.Label _previewLabel;

		// Token: 0x04001DDC RID: 7644
		private System.Windows.Forms.Label _helpLabel;

		// Token: 0x04001DDD RID: 7645
		private ParameterEditorUserControl _parameterEditorUserControl;

		// Token: 0x04001DDE RID: 7646
		private SqlDataSourceDesigner _sqlDataSourceDesigner;

		// Token: 0x04001DDF RID: 7647
		private DesignerDataConnection _dataConnection;

		// Token: 0x04001DE0 RID: 7648
		private SqlDataSourceQuery _selectQuery;

		// Token: 0x04001DE1 RID: 7649
		private SqlDataSourceQuery _insertQuery;

		// Token: 0x04001DE2 RID: 7650
		private SqlDataSourceQuery _updateQuery;

		// Token: 0x04001DE3 RID: 7651
		private SqlDataSourceQuery _deleteQuery;
	}
}
