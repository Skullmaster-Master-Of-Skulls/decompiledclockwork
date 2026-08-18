using System;
using System.ComponentModel;
using System.IO;
using System.Web.UI.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x020000D3 RID: 211
	public class DesignerWithMapPath : ControlDesigner
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x00010068 File Offset: 0x0000E268
		public string MapPath(string originalPath)
		{
			string text = null;
			ISite site = base.Component.Site;
			if (site != null)
			{
				IWebApplication webApplication = (IWebApplication)site.GetService(typeof(IWebApplication));
				if (webApplication != null)
				{
					string text2 = originalPath.Replace("/", "\\");
					bool flag = false;
					while (text2.Length > 0 && (text2.Substring(0, 1) == "\\" || text2.Substring(0, 1) == "~"))
					{
						flag = true;
						text2 = text2.Substring(1);
						if (text2.Length == 0)
						{
							break;
						}
					}
					string physicalPath = webApplication.RootProjectItem.PhysicalPath;
					if (flag)
					{
						text = Path.Combine(physicalPath, text2);
					}
					else
					{
						string text3 = Path.GetDirectoryName(base.RootDesigner.DocumentUrl).Replace("/", "\\");
						while (text3.Length > 0 && (text3.Substring(0, 1) == "\\" || text3.Substring(0, 1) == "~"))
						{
							text3 = text3.Substring(1);
							if (text3.Length == 0)
							{
								break;
							}
						}
						text = Path.Combine(Path.Combine(physicalPath, text3), text2);
					}
					text = base.RootDesigner.ResolveUrl(text).Substring(8).Replace("/", "\\");
					if (text.IndexOf(physicalPath, StringComparison.OrdinalIgnoreCase) != 0)
					{
						text = null;
					}
				}
			}
			return text;
		}
	}
}
