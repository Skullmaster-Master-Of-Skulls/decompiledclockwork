using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200120D RID: 4621
	public class TreeListCreateColumnEditorEventArgs : EventArgs
	{
		// Token: 0x0600BF12 RID: 48914 RVA: 0x002A54A1 File Offset: 0x002A36A1
		public TreeListCreateColumnEditorEventArgs(TreeListEditableColumn column, ITreeListColumnEditor defaultEditor)
		{
			this.Column = column;
			this.DefaultEditor = defaultEditor;
		}

		// Token: 0x17003DA8 RID: 15784
		// (get) Token: 0x0600BF13 RID: 48915 RVA: 0x002A54B7 File Offset: 0x002A36B7
		// (set) Token: 0x0600BF14 RID: 48916 RVA: 0x002A54BF File Offset: 0x002A36BF
		public TreeListEditableColumn Column { get; private set; }

		// Token: 0x17003DA9 RID: 15785
		// (get) Token: 0x0600BF15 RID: 48917 RVA: 0x002A54C8 File Offset: 0x002A36C8
		// (set) Token: 0x0600BF16 RID: 48918 RVA: 0x002A54D0 File Offset: 0x002A36D0
		public ITreeListColumnEditor DefaultEditor { get; private set; }

		// Token: 0x17003DAA RID: 15786
		// (get) Token: 0x0600BF17 RID: 48919 RVA: 0x002A54D9 File Offset: 0x002A36D9
		// (set) Token: 0x0600BF18 RID: 48920 RVA: 0x002A54E1 File Offset: 0x002A36E1
		public TreeListCreateCustomEditorDelegate CustomEditorInitializer { get; set; }

		// Token: 0x04003227 RID: 12839
		internal static readonly TreeListCreateCustomEditorDelegate EmptyInitializer = () => null;
	}
}
