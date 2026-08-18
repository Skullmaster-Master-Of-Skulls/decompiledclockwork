using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200095A RID: 2394
	public abstract class TreeListColumnEditor : ITreeListColumnEditor
	{
		// Token: 0x06005B21 RID: 23329 RVA: 0x001154BB File Offset: 0x001136BB
		public TreeListColumnEditor(TreeListEditableColumn column)
		{
			this.Column = column;
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06005B22 RID: 23330 RVA: 0x001154CA File Offset: 0x001136CA
		// (set) Token: 0x06005B23 RID: 23331 RVA: 0x001154D2 File Offset: 0x001136D2
		public TreeListEditableColumn Column { get; private set; }

		// Token: 0x06005B24 RID: 23332 RVA: 0x001154DB File Offset: 0x001136DB
		protected virtual string GenerateControlID()
		{
			return this.Column.UniqueName + "Editor";
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x001154F4 File Offset: 0x001136F4
		public static object GetFirstValueFromEnumerable(IEnumerable enumerable)
		{
			if (enumerable != null)
			{
				IEnumerator enumerator = enumerable.GetEnumerator();
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x0011551B File Offset: 0x0011371B
		public virtual object GetFirstValue()
		{
			return TreeListColumnEditor.GetFirstValueFromEnumerable(this.GetValues());
		}

		// Token: 0x06005B27 RID: 23335
		public abstract void Initialize(TreeListEditableItem editItem, Control container);

		// Token: 0x06005B28 RID: 23336
		public abstract void SetValues(IEnumerable values);

		// Token: 0x06005B29 RID: 23337
		public abstract IEnumerable GetValues();
	}
}
