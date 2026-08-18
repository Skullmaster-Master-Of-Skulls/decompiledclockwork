using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000FD RID: 253
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class RegexTypeEditor : UITypeEditor
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x00033C84 File Offset: 0x00031E84
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					ISite site = null;
					if (context.Instance is IComponent)
					{
						site = ((IComponent)context.Instance).Site;
					}
					else if (context.Instance is object[])
					{
						object[] array = (object[])context.Instance;
						if (array[0] is IComponent)
						{
							site = ((IComponent)array[0]).Site;
						}
					}
					RegexEditorDialog regexEditorDialog = new RegexEditorDialog(site);
					regexEditorDialog.RegularExpression = value.ToString();
					if (regexEditorDialog.ShowDialog() == DialogResult.OK)
					{
						value = regexEditorDialog.RegularExpression;
					}
				}
			}
			return value;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
