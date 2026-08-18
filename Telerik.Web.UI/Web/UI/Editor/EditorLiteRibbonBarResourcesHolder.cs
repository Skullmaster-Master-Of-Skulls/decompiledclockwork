using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000294 RID: 660
	[ToolboxItem(false)]
	[EmbeddedSkin("EditorLiteRibbonBar")]
	internal class EditorLiteRibbonBarResourcesHolder : RadWebControl
	{
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0004F17E File Offset: 0x0004D37E
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0004F181 File Offset: 0x0004D381
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0004F185 File Offset: 0x0004D385
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddStyleAttribute("display", "none");
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0004F19E File Offset: 0x0004D39E
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return new List<ScriptDescriptor>();
		}
	}
}
