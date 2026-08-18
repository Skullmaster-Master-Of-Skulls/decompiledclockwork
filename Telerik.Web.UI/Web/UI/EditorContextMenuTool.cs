using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200028D RID: 653
	public class EditorContextMenuTool : EditorTool
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x0004E7AB File Offset: 0x0004C9AB
		private StateBag AttributeState
		{
			get
			{
				if (this._attributeState == null)
				{
					this._attributeState = new StateBag(true);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._attributeState).TrackViewState();
					}
				}
				return this._attributeState;
			}
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0004E7DA File Offset: 0x0004C9DA
		public EditorContextMenuTool()
		{
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0004E7E2 File Offset: 0x0004C9E2
		public EditorContextMenuTool(string name) : base(name)
		{
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x0004E7EB File Offset: 0x0004C9EB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual EditorToolCollection Tools
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new EditorToolCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._tools).TrackViewState();
					}
				}
				return this._tools;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0004E819 File Offset: 0x0004CA19
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x0004E848 File Offset: 0x0004CA48
		[DefaultValue("")]
		public string IconCssClass
		{
			get
			{
				if (base.ViewState["IconCssClass"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["IconCssClass"];
			}
			set
			{
				base.ViewState["IconCssClass"] = value;
			}
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0004E85C File Offset: 0x0004CA5C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Tools).LoadViewState(array[1]);
			if (array.Length > 1 && array[2] != null)
			{
				((IStateManager)this.AttributeState).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0004E8A0 File Offset: 0x0004CAA0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Tools).SaveViewState(),
				(this._attributeState == null) ? null : ((IStateManager)this._attributeState).SaveViewState()
			};
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0004E8E7 File Offset: 0x0004CAE7
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Tools).TrackViewState();
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0004E8FA File Offset: 0x0004CAFA
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Tools.SetDirty();
			if (this._attributeState != null)
			{
				this._attributeState.SetDirty(true);
			}
		}

		// Token: 0x0400061A RID: 1562
		private StateBag _attributeState;

		// Token: 0x0400061B RID: 1563
		private EditorToolCollection _tools;
	}
}
