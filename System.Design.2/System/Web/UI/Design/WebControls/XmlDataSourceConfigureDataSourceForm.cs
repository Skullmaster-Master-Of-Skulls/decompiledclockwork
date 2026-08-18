using System;
using System.ComponentModel;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200013B RID: 315
	internal sealed partial class XmlDataSourceConfigureDataSourceForm : DesignerForm
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x00049708 File Offset: 0x00047908
		public XmlDataSourceConfigureDataSourceForm(IServiceProvider serviceProvider, XmlDataSource xmlDataSource) : base(serviceProvider)
		{
			this._xmlDataSource = xmlDataSource;
			this.InitializeComponent();
			this.InitializeUI();
			this.DataFile = this._xmlDataSource.DataFile;
			this.TransformFile = this._xmlDataSource.TransformFile;
			this.XPath = this._xmlDataSource.XPath;
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00049762 File Offset: 0x00047962
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0004976F File Offset: 0x0004796F
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

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0004978A File Offset: 0x0004798A
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.XmlDataSource.ConfigureDataSource";
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00049791 File Offset: 0x00047991
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0004979E File Offset: 0x0004799E
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

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000497B9 File Offset: 0x000479B9
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x000497C6 File Offset: 0x000479C6
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

		// Token: 0x06000B5F RID: 2911 RVA: 0x00049E64 File Offset: 0x00048064
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

		// Token: 0x06000B60 RID: 2912 RVA: 0x00049F94 File Offset: 0x00048194
		private void OnChooseDataFileButtonClick(object sender, EventArgs e)
		{
			string text = UrlBuilder.BuildUrl(this._xmlDataSource, this, this.DataFile, SR.GetString("XmlDataFileEditor_Caption"), SR.GetString("XmlDataFileEditor_Filter"));
			if (text != null)
			{
				this.DataFile = text;
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00049FD4 File Offset: 0x000481D4
		private void OnChooseTransformFileButtonClick(object sender, EventArgs e)
		{
			string text = UrlBuilder.BuildUrl(this._xmlDataSource, this, this.TransformFile, SR.GetString("XslTransformFileEditor_Caption"), SR.GetString("XslTransformFileEditor_Filter"));
			if (text != null)
			{
				this.TransformFile = text;
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0004A014 File Offset: 0x00048214
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

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x040006F7 RID: 1783
		private XmlDataSource _xmlDataSource;
	}
}
