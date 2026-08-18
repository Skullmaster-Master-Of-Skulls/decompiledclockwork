using System;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000B4 RID: 180
	public abstract class DataControlFieldDesigner
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600059E RID: 1438
		public abstract string DefaultNodeText { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001C9C2 File Offset: 0x0001ABC2
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001C9CA File Offset: 0x0001ABCA
		internal DesignerForm DesignerForm
		{
			get
			{
				return this._designerForm;
			}
			set
			{
				this._designerForm = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001C9D3 File Offset: 0x0001ABD3
		protected IServiceProvider ServiceProvider
		{
			get
			{
				return this._designerForm.ServiceProvider;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005A2 RID: 1442
		public abstract bool UsesSchema { get; }

		// Token: 0x060005A3 RID: 1443
		public abstract DataControlField CreateField();

		// Token: 0x060005A4 RID: 1444
		public abstract DataControlField CreateField(IDataSourceFieldSchema fieldSchema);

		// Token: 0x060005A5 RID: 1445
		public abstract TemplateField CreateTemplateField(DataControlField dataControlField, DataBoundControl dataBoundControl);

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		protected string GetNewDataSourceName(Type controlType, DataBoundControlMode mode)
		{
			DataControlFieldsEditor dataControlFieldsEditor = this.DesignerForm as DataControlFieldsEditor;
			if (dataControlFieldsEditor != null)
			{
				return dataControlFieldsEditor.GetNewDataSourceName(controlType, mode);
			}
			return string.Empty;
		}

		// Token: 0x060005A7 RID: 1447
		public abstract string GetNodeText(DataControlField dataControlField);

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001CA0A File Offset: 0x0001AC0A
		protected object GetService(Type serviceType)
		{
			if (this.ServiceProvider != null)
			{
				return this.ServiceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001CA22 File Offset: 0x0001AC22
		protected ITemplate GetTemplate(DataBoundControl control, string templateContent)
		{
			return DataControlFieldHelper.GetTemplate(control, templateContent);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001CA2B File Offset: 0x0001AC2B
		protected TemplateField GetTemplateField(DataControlField dataControlField, DataBoundControl dataBoundControl)
		{
			return DataControlFieldHelper.GetTemplateField(dataControlField, dataBoundControl);
		}

		// Token: 0x060005AB RID: 1451
		public abstract bool IsEnabled(DataBoundControl parent);

		// Token: 0x040002F1 RID: 753
		private DesignerForm _designerForm;
	}
}
