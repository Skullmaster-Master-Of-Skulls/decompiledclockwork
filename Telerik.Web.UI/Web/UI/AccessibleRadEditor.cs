using System;
using System.ComponentModel;
using System.Xml;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000F6D RID: 3949
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Data Editing")]
	public sealed class AccessibleRadEditor : RadEditor
	{
		// Token: 0x06009764 RID: 38756 RVA: 0x0021EE8C File Offset: 0x0021D08C
		public static void MakeAccessible(RadEditor editor)
		{
			editor.IsInAccessibleMode = true;
			editor.EditModes = EditModes.Design;
			if (!string.IsNullOrEmpty(editor.ToolsFile))
			{
				editor.ToolsFile = string.Empty;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(new XmlTextReader(typeof(RadEditor).Assembly.GetManifestResourceStream("Telerik.Web.UI.Editor.AccessibleEditor.ToolsFile.xml")));
			editor.LoadToolsFile(xmlDocument);
		}

		// Token: 0x06009765 RID: 38757 RVA: 0x0021EEF0 File Offset: 0x0021D0F0
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				AccessibleRadEditor.MakeAccessible(this);
			}
		}
	}
}
