using System;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F1 RID: 241
	internal sealed partial class ObjectDataSourceWizardForm : WizardForm
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
		public ObjectDataSourceWizardForm(IServiceProvider serviceProvider, ObjectDataSourceDesigner objectDataSourceDesigner) : base(serviceProvider)
		{
			base.Glyph = BitmapSelector.CreateBitmap(typeof(SqlDataSourceWizardForm), "datasourcewizard.bmp");
			this._objectDataSourceDesigner = objectDataSourceDesigner;
			this._objectDataSource = (ObjectDataSource)this._objectDataSourceDesigner.Component;
			this.Text = SR.GetString("ConfigureDataSource_Title", new object[]
			{
				this._objectDataSource.ID
			});
			ObjectDataSourceChooseTypePanel objectDataSourceChooseTypePanel = new ObjectDataSourceChooseTypePanel(this._objectDataSourceDesigner);
			ObjectDataSourceChooseMethodsPanel objectDataSourceChooseMethodsPanel = new ObjectDataSourceChooseMethodsPanel(this._objectDataSourceDesigner);
			base.SetPanels(new WizardPanel[]
			{
				objectDataSourceChooseTypePanel,
				objectDataSourceChooseMethodsPanel
			});
			this._parametersPanel = new ObjectDataSourceConfigureParametersPanel(this._objectDataSourceDesigner);
			base.RegisterPanel(this._parametersPanel);
			base.Size += new Size(0, 40);
			this.MinimumSize = base.Size;
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0002FD7D File Offset: 0x0002DF7D
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.ObjectDataSource.ConfigureDataSource";
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002FD84 File Offset: 0x0002DF84
		internal ObjectDataSourceConfigureParametersPanel GetParametersPanel()
		{
			this._parametersPanel.ResetUI();
			return this._parametersPanel;
		}

		// Token: 0x040004ED RID: 1261
		private ObjectDataSourceDesigner _objectDataSourceDesigner;

		// Token: 0x040004EE RID: 1262
		private ObjectDataSource _objectDataSource;

		// Token: 0x040004EF RID: 1263
		private ObjectDataSourceConfigureParametersPanel _parametersPanel;
	}
}
