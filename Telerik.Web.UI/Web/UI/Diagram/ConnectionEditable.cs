using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200024C RID: 588
	public class ConnectionEditable : StateManager, IDefaultCheck
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00049B1A File Offset: 0x00047D1A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramConnectionEditableToolsCollection ToolsCollection
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new DiagramConnectionEditableToolsCollection();
				}
				return this._tools;
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00049B35 File Offset: 0x00047D35
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ToolsCollection.SetDirty();
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00049B48 File Offset: 0x00047D48
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ToolsCollection).LoadViewState(array[num++]);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00049B80 File Offset: 0x00047D80
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ToolsCollection).SaveViewState()
			};
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00049BAE File Offset: 0x00047DAE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ToolsCollection).TrackViewState();
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00049BC1 File Offset: 0x00047DC1
		public bool IsDefault
		{
			get
			{
				return this.ToolsCollection.ItemsList.Count == 0;
			}
		}

		// Token: 0x040005B8 RID: 1464
		private DiagramConnectionEditableToolsCollection _tools;
	}
}
