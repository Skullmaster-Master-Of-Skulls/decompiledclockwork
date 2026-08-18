using System;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000342 RID: 834
	internal class TabPageCollectionEditor : CollectionEditor
	{
		// Token: 0x06002126 RID: 8486 RVA: 0x000CADBC File Offset: 0x000C8FBC
		public TabPageCollectionEditor() : base(typeof(TabControl.TabPageCollection))
		{
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000CADD0 File Offset: 0x000C8FD0
		protected override object SetItems(object editValue, object[] value)
		{
			TabControl tabControl = base.Context.Instance as TabControl;
			if (tabControl != null)
			{
				tabControl.SuspendLayout();
			}
			object result = base.SetItems(editValue, value);
			if (tabControl != null)
			{
				tabControl.ResumeLayout();
			}
			return result;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000CAE0C File Offset: 0x000C900C
		protected override object CreateInstance(Type itemType)
		{
			object obj = base.CreateInstance(itemType);
			TabPage tabPage = obj as TabPage;
			tabPage.UseVisualStyleBackColor = true;
			return tabPage;
		}
	}
}
