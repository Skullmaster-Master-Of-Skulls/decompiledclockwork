using System;
using System.ComponentModel;
using System.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms.Design;

namespace System.Web.UI.Design
{
	// Token: 0x0200007E RID: 126
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class UrlEditor : UITypeEditor
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00012919 File Offset: 0x00010B19
		protected virtual string Caption
		{
			get
			{
				return SR.GetString("UrlPicker_DefaultCaption");
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000445B File Offset: 0x0000265B
		protected virtual UrlBuilderOptions Options
		{
			get
			{
				return UrlBuilderOptions.None;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012928 File Offset: 0x00010B28
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					string text = (string)value;
					string caption = this.Caption;
					string filter = this.Filter;
					text = UrlBuilder.BuildUrl(provider, null, text, caption, filter, this.Options);
					if (text != null)
					{
						value = text;
					}
				}
			}
			return value;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001297E File Offset: 0x00010B7E
		protected virtual string Filter
		{
			get
			{
				return SR.GetString("UrlPicker_DefaultFilter");
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00009D4C File Offset: 0x00007F4C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
