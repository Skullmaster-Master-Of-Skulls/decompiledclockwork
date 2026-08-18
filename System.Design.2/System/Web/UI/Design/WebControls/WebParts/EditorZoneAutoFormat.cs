using System;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200014B RID: 331
	internal sealed class EditorZoneAutoFormat : ReflectionBasedAutoFormat
	{
		// Token: 0x06000BBF RID: 3007 RVA: 0x0004B0CE File Offset: 0x000492CE
		public EditorZoneAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Height = 275;
			base.Style.Width = 300;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0004B104 File Offset: 0x00049304
		public override Control GetPreviewControl(Control runtimeControl)
		{
			EditorZone editorZone = (EditorZone)base.GetPreviewControl(runtimeControl);
			if (editorZone != null && editorZone.EditorParts.Count == 0)
			{
				editorZone.ZoneTemplate = new EditorZoneAutoFormat.AutoFormatTemplate();
			}
			editorZone.ID = "AutoFormatPreviewControl";
			return editorZone;
		}

		// Token: 0x04000710 RID: 1808
		internal const string PreviewControlID = "AutoFormatPreviewControl";

		// Token: 0x0200045C RID: 1116
		private sealed class AutoFormatTemplate : ITemplate
		{
			// Token: 0x06002981 RID: 10625 RVA: 0x000FAAAC File Offset: 0x000F8CAC
			public void InstantiateIn(Control container)
			{
				LayoutEditorPart layoutEditorPart = new LayoutEditorPart();
				layoutEditorPart.ID = "LayoutEditorPart";
				container.Controls.Add(layoutEditorPart);
			}
		}
	}
}
