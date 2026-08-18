using System;
using System.Collections;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002E2 RID: 738
	internal class FlowPanelDesigner : PanelDesigner
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool ParticipatesWithSnapLines
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x000B3724 File Offset: 0x000B1924
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = (ArrayList)base.SnapLines;
				ArrayList arrayList2 = new ArrayList(4);
				foreach (object obj in arrayList)
				{
					SnapLine snapLine = (SnapLine)obj;
					if (snapLine.Filter != null && snapLine.Filter.Contains("Padding"))
					{
						arrayList2.Add(snapLine);
					}
				}
				foreach (object obj2 in arrayList2)
				{
					SnapLine obj3 = (SnapLine)obj2;
					arrayList.Remove(obj3);
				}
				return arrayList;
			}
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000B37F8 File Offset: 0x000B19F8
		internal override void AddChildControl(Control newChild)
		{
			this.Control.Controls.Add(newChild);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000B380C File Offset: 0x000B1A0C
		protected override void OnDragDrop(DragEventArgs de)
		{
			base.OnDragDrop(de);
			SelectionManager selectionManager = this.GetService(typeof(SelectionManager)) as SelectionManager;
			if (selectionManager != null)
			{
				selectionManager.Refresh();
			}
		}
	}
}
