using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Web.UI.Design
{
	// Token: 0x0200007C RID: 124
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class UrlBuilder
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000362F File Offset: 0x0000182F
		private UrlBuilder()
		{
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012842 File Offset: 0x00010A42
		public static string BuildUrl(IComponent component, Control owner, string initialUrl, string caption, string filter)
		{
			return UrlBuilder.BuildUrl(component, owner, initialUrl, caption, filter, UrlBuilderOptions.None);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012850 File Offset: 0x00010A50
		public static string BuildUrl(IComponent component, Control owner, string initialUrl, string caption, string filter, UrlBuilderOptions options)
		{
			ISite site = component.Site;
			if (site == null)
			{
				return null;
			}
			return UrlBuilder.BuildUrl(site, owner, initialUrl, caption, filter, options);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012878 File Offset: 0x00010A78
		public static string BuildUrl(IServiceProvider serviceProvider, Control owner, string initialUrl, string caption, string filter, UrlBuilderOptions options)
		{
			string text = string.Empty;
			string result = null;
			IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				WebFormsRootDesigner webFormsRootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as WebFormsRootDesigner;
				if (webFormsRootDesigner != null)
				{
					text = webFormsRootDesigner.DocumentUrl;
				}
			}
			if (text.Length == 0)
			{
				IWebFormsDocumentService webFormsDocumentService = (IWebFormsDocumentService)serviceProvider.GetService(typeof(IWebFormsDocumentService));
				if (webFormsDocumentService != null)
				{
					text = webFormsDocumentService.DocumentUrl;
				}
			}
			IWebFormsBuilderUIService webFormsBuilderUIService = (IWebFormsBuilderUIService)serviceProvider.GetService(typeof(IWebFormsBuilderUIService));
			if (webFormsBuilderUIService != null)
			{
				result = webFormsBuilderUIService.BuildUrl(owner, initialUrl, text, caption, filter, options);
			}
			return result;
		}
	}
}
