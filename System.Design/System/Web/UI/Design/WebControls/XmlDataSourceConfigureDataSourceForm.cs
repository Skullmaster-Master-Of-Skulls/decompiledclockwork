using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000505 RID: 1285
	internal sealed partial class XmlDataSourceConfigureDataSourceForm : DesignerForm
	{
		// Token: 0x06002DC9 RID: 11721 RVA: 0x00103F0C File Offset: 0x00102F0C
		public XmlDataSourceConfigureDataSourceForm(IServiceProvider serviceProvider, XmlDataSource xmlDataSource) : base(serviceProvider)
		{
			this._xmlDataSource = xmlDataSource;
			this.InitializeComponent();
			this.InitializeUI();
			this.DataFile = this._xmlDataSource.DataFile;
			this.TransformFile = this._xmlDataSource.TransformFile;
			this.XPath = this._xmlDataSource.XPath;
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x00103F66 File Offset: 0x00102F66
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x00103F73 File Offset: 0x00102F73
		private string DataFile
		{
			get
			{
				return this._dataFileTextBox.Text;
			}
			set
			{
				this._dataFileTextBox.Text = value;
				this._dataFileTextBox.Select(0, 0);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002DCC RID: 11724 RVA: 0x00103F8E File Offset: 0x00102F8E
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.XmlDataSource.ConfigureDataSource";
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x00103F95 File Offset: 0x00102F95
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x00103FA2 File Offset: 0x00102FA2
		private string TransformFile
		{
			get
			{
				return this._transformFileTextBox.Text;
			}
			set
			{
				this._transformFileTextBox.Text = value;
				this._transformFileTextBox.Select(0, 0);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x00103FBD File Offset: 0x00102FBD
		// (set) Token: 0x06002DD0 RID: 11728 RVA: 0x00103FCA File Offset: 0x00102FCA
		private string XPath
		{
			get
			{
				return this._xpathExpressionTextBox.Text;
			}
			set
			{
				this._xpathExpressionTextBox.Text = value;
				this._xpathExpressionTextBox.Select(0, 0);
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x00104668 File Offset: 0x00103668
		private void InitializeUI()
		{
			this._dataFileLabel.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_DataFileLabel");
			this._transformFileLabel.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_TransformFileLabel");
			this._xpathExpressionLabel.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_XPathExpressionLabel");
			this._transformFileHelpLabel.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_TransformFileHelpLabel");
			this._xpathExpressionHelpLabel.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_XPathExpressionHelpLabel");
			this._chooseDataFileButton.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_Browse");
			this._chooseTransformFileButton.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_Browse");
			this._helpLabel.Text = SR.GetString("XmlDataSourceConfigureDataSourceForm_HelpLabel");
			this._okButton.Text = SR.GetString("OK");
			this._cancelButton.Text = SR.GetString("Cancel");
			this._chooseDataFileButton.AccessibleDescription = SR.GetString("XmlDataFileEditor_Ellipses");
			this._chooseTransformFileButton.AccessibleDescription = SR.GetString("XslTransformFileEditor_Ellipses");
			this.Text = SR.GetString("ConfigureDataSource_Title", new object[]
			{
				this._xmlDataSource.ID
			});
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x00104798 File Offset: 0x00103798
		private void OnChooseDataFileButtonClick(object sender, EventArgs e)
		{
			string text = UrlBuilder.BuildUrl(this._xmlDataSource, this, this.DataFile, SR.GetString("XmlDataFileEditor_Caption"), SR.GetString("XmlDataFileEditor_Filter"));
			if (text != null)
			{
				this.DataFile = text;
			}
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x001047D8 File Offset: 0x001037D8
		private void OnChooseTransformFileButtonClick(object sender, EventArgs e)
		{
			string text = UrlBuilder.BuildUrl(this._xmlDataSource, this, this.TransformFile, SR.GetString("XslTransformFileEditor_Caption"), SR.GetString("XslTransformFileEditor_Filter"));
			if (text != null)
			{
				this.TransformFile = text;
			}
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x00104818 File Offset: 0x00103818
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			if (this._xmlDataSource.DataFile != this.DataFile)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._xmlDataSource)["DataFile"];
				propertyDescriptor.ResetValue(this._xmlDataSource);
				propertyDescriptor.SetValue(this._xmlDataSource, this.DataFile);
			}
			if (this._xmlDataSource.TransformFile != this.TransformFile)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._xmlDataSource)["TransformFile"];
				propertyDescriptor.ResetValue(this._xmlDataSource);
				propertyDescriptor.SetValue(this._xmlDataSource, this.TransformFile);
			}
			if (this._xmlDataSource.XPath != this.XPath)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._xmlDataSource)["XPath"];
				propertyDescriptor.ResetValue(this._xmlDataSource);
				propertyDescriptor.SetValue(this._xmlDataSource, this.XPath);
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x00104916 File Offset: 0x00103916
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x04001F49 RID: 8009
		private XmlDataSource _xmlDataSource;
	}
}
