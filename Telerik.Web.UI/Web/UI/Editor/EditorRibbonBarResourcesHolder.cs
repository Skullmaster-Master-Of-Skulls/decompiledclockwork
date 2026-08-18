using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000E87 RID: 3719
	[ToolboxItem(false)]
	[EmbeddedSkin("Common.EditorRibbonBar")]
	internal class EditorRibbonBarResourcesHolder : RadWebControl
	{
		// Token: 0x17002C86 RID: 11398
		// (get) Token: 0x06008CFA RID: 36090 RVA: 0x0020017A File Offset: 0x001FE37A
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002C87 RID: 11399
		// (get) Token: 0x06008CFB RID: 36091 RVA: 0x0020017D File Offset: 0x001FE37D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x06008CFC RID: 36092 RVA: 0x00200181 File Offset: 0x001FE381
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddStyleAttribute("display", "none");
		}

		// Token: 0x06008CFD RID: 36093 RVA: 0x0020019A File Offset: 0x001FE39A
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return new List<ScriptDescriptor>();
		}
	}
}
