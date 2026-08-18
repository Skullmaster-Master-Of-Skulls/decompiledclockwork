using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.Util
{
	// Token: 0x0200016B RID: 363
	internal class UrlPath
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x0000362F File Offset: 0x0000182F
		private UrlPath()
		{
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00052A24 File Offset: 0x00050C24
		private static bool IsAbsolutePhysicalPath(string path)
		{
			return path != null && path.Length >= 3 && (path.StartsWith("\\\\", StringComparison.Ordinal) || (char.IsLetter(path[0]) && path[1] == ':' && path[2] == '\\'));
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00052A78 File Offset: 0x00050C78
		internal static string MapPath(IServiceProvider serviceProvider, string path)
		{
			if (path.Length == 0)
			{
				return null;
			}
			if (UrlPath.IsAbsolutePhysicalPath(path))
			{
				return path;
			}
			if (serviceProvider != null)
			{
				IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null && designerHost.RootComponent != null)
				{
					WebFormsRootDesigner webFormsRootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as WebFormsRootDesigner;
					if (webFormsRootDesigner != null)
					{
						string appRelativeUrl = webFormsRootDesigner.ResolveUrl(path);
						IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
						if (webApplication != null)
						{
							IProjectItem projectItemFromUrl = webApplication.GetProjectItemFromUrl(appRelativeUrl);
							if (projectItemFromUrl != null)
							{
								return projectItemFromUrl.PhysicalPath;
							}
						}
					}
				}
			}
			return null;
		}
	}
}
