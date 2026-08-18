using System;
using System.Collections;
using System.ComponentModel.Design.Data;
using System.Design;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200010C RID: 268
	internal abstract class SqlDataSourceConnectionPanel : WizardPanel
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x0003B8A4 File Offset: 0x00039AA4
		protected SqlDataSourceConnectionPanel(SqlDataSourceDesigner sqlDataSourceDesigner)
		{
			this._sqlDataSourceDesigner = sqlDataSourceDesigner;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009A3 RID: 2467
		public abstract DesignerDataConnection DataConnection { get; }

		// Token: 0x060009A4 RID: 2468 RVA: 0x0003B8B4 File Offset: 0x00039AB4
		protected bool CheckValidProvider()
		{
			DesignerDataConnection dataConnection = this.DataConnection;
			bool result;
			try
			{
				SqlDataSourceDesigner.GetDbProviderFactory(dataConnection.ProviderName);
				result = true;
			}
			catch (Exception ex)
			{
				UIServiceHelper.ShowError(base.ServiceProvider, ex, SR.GetString("SqlDataSourceConnectionPanel_ProviderNotFound", new object[]
				{
					dataConnection.ProviderName
				}));
				result = false;
			}
			return result;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0003B914 File Offset: 0x00039B14
		internal static WizardPanel CreateCommandPanel(SqlDataSourceWizardForm wizard, DesignerDataConnection dataConnection, WizardPanel nextPanel)
		{
			IDataEnvironment dataEnvironment = null;
			IServiceProvider site = wizard.SqlDataSourceDesigner.Component.Site;
			if (site != null)
			{
				dataEnvironment = (IDataEnvironment)site.GetService(typeof(IDataEnvironment));
			}
			bool flag = false;
			if (dataEnvironment != null)
			{
				try
				{
					IDesignerDataSchema connectionSchema = dataEnvironment.GetConnectionSchema(dataConnection);
					if (connectionSchema != null)
					{
						flag = connectionSchema.SupportsSchemaClass(DesignerDataSchemaClass.Tables);
						if (flag)
						{
							connectionSchema.GetSchemaItems(DesignerDataSchemaClass.Tables);
						}
						else
						{
							flag = connectionSchema.SupportsSchemaClass(DesignerDataSchemaClass.Views);
							connectionSchema.GetSchemaItems(DesignerDataSchemaClass.Views);
						}
					}
				}
				catch (Exception ex)
				{
					UIServiceHelper.ShowError(site, ex, SR.GetString("SqlDataSourceConnectionPanel_CouldNotGetConnectionSchema"));
					return null;
				}
			}
			if (nextPanel != null)
			{
				if (flag)
				{
					if (!(nextPanel is SqlDataSourceConfigureSelectPanel))
					{
						return wizard.GetConfigureSelectPanel();
					}
				}
				else if (!(nextPanel is SqlDataSourceCustomCommandPanel))
				{
					return SqlDataSourceConnectionPanel.CreateCustomCommandPanel(wizard, dataConnection);
				}
				return nextPanel;
			}
			if (flag)
			{
				return wizard.GetConfigureSelectPanel();
			}
			return SqlDataSourceConnectionPanel.CreateCustomCommandPanel(wizard, dataConnection);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003BA00 File Offset: 0x00039C00
		private static WizardPanel CreateCustomCommandPanel(SqlDataSourceWizardForm wizard, DesignerDataConnection dataConnection)
		{
			SqlDataSource sqlDataSource = (SqlDataSource)wizard.SqlDataSourceDesigner.Component;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = new ArrayList();
			wizard.SqlDataSourceDesigner.CopyList(sqlDataSource.SelectParameters, arrayList);
			wizard.SqlDataSourceDesigner.CopyList(sqlDataSource.InsertParameters, arrayList2);
			wizard.SqlDataSourceDesigner.CopyList(sqlDataSource.UpdateParameters, arrayList3);
			wizard.SqlDataSourceDesigner.CopyList(sqlDataSource.DeleteParameters, arrayList4);
			SqlDataSourceCustomCommandPanel customCommandPanel = wizard.GetCustomCommandPanel();
			customCommandPanel.SetQueries(dataConnection, new SqlDataSourceQuery(sqlDataSource.SelectCommand, sqlDataSource.SelectCommandType, arrayList), new SqlDataSourceQuery(sqlDataSource.InsertCommand, sqlDataSource.InsertCommandType, arrayList2), new SqlDataSourceQuery(sqlDataSource.UpdateCommand, sqlDataSource.UpdateCommandType, arrayList3), new SqlDataSourceQuery(sqlDataSource.DeleteCommand, sqlDataSource.DeleteCommandType, arrayList4));
			return customCommandPanel;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0003BADC File Offset: 0x00039CDC
		public override bool OnNext()
		{
			if (!this.CheckValidProvider())
			{
				return false;
			}
			WizardPanel wizardPanel = SqlDataSourceConnectionPanel.CreateCommandPanel((SqlDataSourceWizardForm)base.ParentWizard, this.DataConnection, base.NextPanel);
			if (wizardPanel == null)
			{
				return false;
			}
			base.NextPanel = wizardPanel;
			return true;
		}

		// Token: 0x040005C9 RID: 1481
		private SqlDataSourceDesigner _sqlDataSourceDesigner;
	}
}
