using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000364 RID: 868
	internal class NotifyIconDesigner : ComponentDesigner
	{
		// Token: 0x060023C3 RID: 9155 RVA: 0x000DFD78 File Offset: 0x000DDF78
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			NotifyIcon notifyIcon = (NotifyIcon)base.Component;
			notifyIcon.Visible = true;
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000DFD9F File Offset: 0x000DDF9F
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new NotifyIconActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x04001A3B RID: 6715
		private DesignerActionListCollection _actionLists;
	}
}
