using System;
using System.Collections;
using System.Diagnostics;
using System.Web.UI;

namespace Telerik.Web.UI.PageLayout
{
	// Token: 0x0200063B RID: 1595
	public class ControlCollectionBase : StateManagedCollection
	{
		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000BE3B1 File Offset: 0x000BC5B1
		public Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000BE3B9 File Offset: 0x000BC5B9
		public IList List
		{
			[DebuggerStepThrough]
			get
			{
				return this;
			}
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000BE3BC File Offset: 0x000BC5BC
		public ControlCollectionBase()
		{
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000BE3C4 File Offset: 0x000BC5C4
		public ControlCollectionBase(Control parent)
		{
			this._parent = parent;
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000BE3D3 File Offset: 0x000BC5D3
		protected override void OnInsertComplete(int index, object value)
		{
			this._parent.Controls.Add(value as Control);
			base.OnInsertComplete(index, value);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000BE3F3 File Offset: 0x000BC5F3
		protected override void SetDirtyObject(object o)
		{
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000BE3F8 File Offset: 0x000BC5F8
		public virtual void ForEach(Action<Control> action)
		{
			foreach (object obj in this)
			{
				Control obj2 = (Control)obj;
				action(obj2);
			}
		}

		// Token: 0x04000F8E RID: 3982
		private Control _parent;
	}
}
