using System;
using System.ComponentModel.Design.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000097 RID: 151
	internal partial class AccessDataSourceWizardForm : SqlDataSourceWizardForm
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x00014666 File Offset: 0x00012866
		public AccessDataSourceWizardForm(IServiceProvider serviceProvider, AccessDataSourceDesigner accessDataSourceDesigner, IDataEnvironment dataEnvironment) : base(serviceProvider, accessDataSourceDesigner, dataEnvironment)
		{
			base.Glyph = BitmapSelector.CreateBitmap(typeof(AccessDataSourceWizardForm), "datasourcewizard.bmp");
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001468B File Offset: 0x0001288B
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.AccessDataSource.ConfigureDataSource";
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00014694 File Offset: 0x00012894
		protected override SqlDataSourceConnectionPanel CreateConnectionPanel()
		{
			AccessDataSourceDesigner accessDataSourceDesigner = (AccessDataSourceDesigner)base.SqlDataSourceDesigner;
			AccessDataSource accessDataSource = (AccessDataSource)accessDataSourceDesigner.Component;
			return new AccessDataSourceConnectionChooserPanel(accessDataSourceDesigner, accessDataSource);
		}
	}
}
