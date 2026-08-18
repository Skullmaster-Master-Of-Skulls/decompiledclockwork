using System;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x020012A8 RID: 4776
	public sealed class EditorToolCollection : GenericEditorToolBaseCollection<EditorTool>
	{
		// Token: 0x0600C802 RID: 51202 RVA: 0x002C8FAA File Offset: 0x002C71AA
		internal EditorToolCollection()
		{
		}

		// Token: 0x0600C803 RID: 51203 RVA: 0x002C8FB4 File Offset: 0x002C71B4
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete(index, value);
			EditorTool editorTool = (EditorTool)value;
			editorTool.InToolStrip = true;
		}

		// Token: 0x0600C804 RID: 51204 RVA: 0x002C8FD8 File Offset: 0x002C71D8
		public void AddRange(params string[] items)
		{
			foreach (string name in items)
			{
				this.Add(new EditorTool(name));
			}
		}

		// Token: 0x0600C805 RID: 51205 RVA: 0x002C9008 File Offset: 0x002C7208
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
			EditorTool editorTool = (EditorTool)value;
			editorTool.InToolStrip = false;
		}
	}
}
