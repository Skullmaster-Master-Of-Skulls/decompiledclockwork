using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200034D RID: 845
	internal class ToolStripCodeDomSerializer : ControlCodeDomSerializer
	{
		// Token: 0x0600216D RID: 8557 RVA: 0x000CBC10 File Offset: 0x000C9E10
		protected override bool HasSitedNonReadonlyChildren(Control parent)
		{
			ToolStrip toolStrip = parent as ToolStrip;
			if (toolStrip == null)
			{
				return false;
			}
			if (toolStrip.Items.Count == 0)
			{
				return false;
			}
			foreach (object obj in toolStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Site != null && toolStrip.Site != null && toolStripItem.Site.Container == toolStrip.Site.Container)
				{
					InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(toolStripItem)[typeof(InheritanceAttribute)];
					if (inheritanceAttribute != null && inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
