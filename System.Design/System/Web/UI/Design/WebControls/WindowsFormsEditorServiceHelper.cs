using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.Design.Util;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020004FE RID: 1278
	internal sealed class WindowsFormsEditorServiceHelper : IWindowsFormsEditorService, IServiceProvider
	{
		// Token: 0x06002DB3 RID: 11699 RVA: 0x0010359A File Offset: 0x0010259A
		public WindowsFormsEditorServiceHelper(ComponentDesigner componentDesigner)
		{
			this._componentDesigner = componentDesigner;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x001035A9 File Offset: 0x001025A9
		void IWindowsFormsEditorService.CloseDropDown()
		{
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x001035AB File Offset: 0x001025AB
		void IWindowsFormsEditorService.DropDownControl(Control control)
		{
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x001035AD File Offset: 0x001025AD
		DialogResult IWindowsFormsEditorService.ShowDialog(Form dialog)
		{
			return UIServiceHelper.ShowDialog(this, dialog);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x001035B8 File Offset: 0x001025B8
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

		// Token: 0x04001F11 RID: 7953
		private ComponentDesigner _componentDesigner;
	}
}
