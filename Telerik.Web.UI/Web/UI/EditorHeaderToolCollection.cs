using System;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x02000290 RID: 656
	public sealed class EditorHeaderToolCollection : GenericEditorToolBaseCollection<EditorHeaderTool>
	{
		// Token: 0x06001776 RID: 6006 RVA: 0x0004EB00 File Offset: 0x0004CD00
		protected override Type[] GetKnownTypes()
		{
			return new Type[]
			{
				typeof(EditorHeaderTool)
			};
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0004EB22 File Offset: 0x0004CD22
		protected override object CreateKnownType(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return new EditorHeaderTool();
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0004EB30 File Offset: 0x0004CD30
		public bool Contains(string toolName)
		{
			foreach (object obj in base.List)
			{
				EditorHeaderTool editorHeaderTool = (EditorHeaderTool)obj;
				if (editorHeaderTool.Name.Equals(toolName, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
	}
}
