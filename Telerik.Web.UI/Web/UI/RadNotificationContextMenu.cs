using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000EC0 RID: 3776
	[ToolboxItem(false)]
	[XmlRoot("Menu")]
	public class RadNotificationContextMenu : RadContextMenu
	{
		// Token: 0x06009034 RID: 36916 RVA: 0x002074AC File Offset: 0x002056AC
		public bool titleTargetIsAdded(string targetID)
		{
			foreach (object obj in this.Targets)
			{
				ContextMenuTarget contextMenuTarget = (ContextMenuTarget)obj;
				if (contextMenuTarget is ContextMenuElementTarget && ((ContextMenuElementTarget)contextMenuTarget).ElementID == targetID)
				{
					return true;
				}
			}
			return false;
		}
	}
}
