using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002B1 RID: 689
	public class ShapeEditable : StateManager, IDefaultCheck
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600183F RID: 6207 RVA: 0x000501D6 File Offset: 0x0004E3D6
		// (set) Token: 0x06001840 RID: 6208 RVA: 0x000501F7 File Offset: 0x0004E3F7
		[DefaultValue(false)]
		public bool Connect
		{
			get
			{
				return (bool)(base.ViewState["Connect"] ?? false);
			}
			set
			{
				base.ViewState["Connect"] = value;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0005020F File Offset: 0x0004E40F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramShapeEditableToolsCollection ToolsCollection
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new DiagramShapeEditableToolsCollection();
				}
				return this._tools;
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0005022A File Offset: 0x0004E42A
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ToolsCollection.SetDirty();
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00050240 File Offset: 0x0004E440
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ToolsCollection).LoadViewState(array[num++]);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00050278 File Offset: 0x0004E478
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ToolsCollection).SaveViewState()
			};
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000502A6 File Offset: 0x0004E4A6
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ToolsCollection).TrackViewState();
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x000502B9 File Offset: 0x0004E4B9
		public bool IsDefault
		{
			get
			{
				return !this.Connect && this.ToolsCollection.ItemsList.Count == 0;
			}
		}

		// Token: 0x04000674 RID: 1652
		private DiagramShapeEditableToolsCollection _tools;
	}
}
