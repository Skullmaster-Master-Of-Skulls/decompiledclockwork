using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.Design.Util;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000132 RID: 306
	internal sealed class WindowsFormsEditorServiceHelper : IWindowsFormsEditorService, IServiceProvider
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x00047852 File Offset: 0x00045A52
		public WindowsFormsEditorServiceHelper(ComponentDesigner componentDesigner)
		{
			this._componentDesigner = componentDesigner;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.CloseDropDown()
		{
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00003937 File Offset: 0x00001B37
		void IWindowsFormsEditorService.DropDownControl(Control control)
		{
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00047861 File Offset: 0x00045A61
		DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
		{
			return UIServiceHelper.ShowDialog(this, dialog);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0004786C File Offset: 0x00045A6C
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			IComponent component = this._componentDesigner.Component;
			if (component != null)
			{
				ISite site = this._componentDesigner.Component.Site;
				if (site != null)
				{
					return site.GetService(serviceType);
				}
			}
			return null;
		}

		// Token: 0x04000699 RID: 1689
		private ComponentDesigner _componentDesigner;
	}
}
