using System;
using System.Collections;
using System.Design;
using System.Drawing;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F6 RID: 246
	internal partial class ParameterCollectionEditorForm : DesignerForm
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x00030310 File Offset: 0x0002E510
		public ParameterCollectionEditorForm(IServiceProvider serviceProvider, ParameterCollection parameters, ControlDesigner designer) : base(serviceProvider)
		{
			this._parameters = parameters;
			if (designer != null)
			{
				this._control = (designer.Component as Control);
			}
			this.InitializeComponent();
			this.InitializeUI();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in parameters)
			{
				ICloneable cloneable = (ICloneable)obj;
				object obj2 = cloneable.Clone();
				if (designer != null)
				{
					designer.RegisterClone(cloneable, obj2);
				}
				arrayList.Add(obj2);
			}
			this._parameterEditorUserControl.AddParameters((Parameter[])arrayList.ToArray(typeof(Parameter)));
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x000303D0 File Offset: 0x0002E5D0
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.Parameter.CollectionEditor";
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00030578 File Offset: 0x0002E778
		private void InitializeUI()
		{
			this._okButton.Text = SR.GetString("OK");
			this._cancelButton.Text = SR.GetString("Cancel");
			this.Text = SR.GetString("ParameterCollectionEditorForm_Caption");
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000305B4 File Offset: 0x0002E7B4
		private void OnOkButtonClick(object sender, EventArgs e)
		{
			Parameter[] parameters = this._parameterEditorUserControl.GetParameters();
			this._parameters.Clear();
			foreach (Parameter parameter in parameters)
			{
				this._parameters.Add(parameter);
			}
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002AF61 File Offset: 0x00029161
		private void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x040004F3 RID: 1267
		private ParameterCollection _parameters;
	}
}
