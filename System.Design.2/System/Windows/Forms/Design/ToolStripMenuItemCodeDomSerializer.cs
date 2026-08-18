using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200035F RID: 863
	internal class ToolStripMenuItemCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06002307 RID: 8967 RVA: 0x000D981B File Offset: 0x000D7A1B
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			return this.GetBaseSerializer(manager).Deserialize(manager, codeObject);
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000D982B File Offset: 0x000D7A2B
		private CodeDomSerializer GetBaseSerializer(IDesignerSerializationManager manager)
		{
			return (CodeDomSerializer)manager.GetSerializer(typeof(Component), typeof(CodeDomSerializer));
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000D984C File Offset: 0x000D7A4C
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			ToolStripMenuItem toolStripMenuItem = value as ToolStripMenuItem;
			ToolStrip currentParent = toolStripMenuItem.GetCurrentParent();
			if (toolStripMenuItem != null && !toolStripMenuItem.IsOnDropDown && currentParent != null && currentParent.Site == null)
			{
				return null;
			}
			CodeDomSerializer codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(typeof(ImageList).BaseType, typeof(CodeDomSerializer));
			return codeDomSerializer.Serialize(manager, value);
		}
	}
}
