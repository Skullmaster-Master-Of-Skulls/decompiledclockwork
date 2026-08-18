using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200106F RID: 4207
	[ParseChildren(true, "Tools")]
	public class EditorContextMenu : StateManager
	{
		// Token: 0x1700367D RID: 13949
		// (get) Token: 0x0600A9A8 RID: 43432 RVA: 0x0024D8AF File Offset: 0x0024BAAF
		// (set) Token: 0x0600A9A9 RID: 43433 RVA: 0x0024D8DE File Offset: 0x0024BADE
		[DefaultValue("*")]
		[NotifyParentProperty(true)]
		public virtual string TagName
		{
			get
			{
				if (base.ViewState["TagName"] == null)
				{
					return "*";
				}
				return (string)base.ViewState["TagName"];
			}
			set
			{
				base.ViewState["TagName"] = value;
			}
		}

		// Token: 0x1700367E RID: 13950
		// (get) Token: 0x0600A9AA RID: 43434 RVA: 0x0024D8F1 File Offset: 0x0024BAF1
		// (set) Token: 0x0600A9AB RID: 43435 RVA: 0x0024D91C File Offset: 0x0024BB1C
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				return base.ViewState["Enabled"] == null || (bool)base.ViewState["Enabled"];
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x1700367F RID: 13951
		// (get) Token: 0x0600A9AC RID: 43436 RVA: 0x0024D934 File Offset: 0x0024BB34
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
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

		// Token: 0x0600A9AD RID: 43437 RVA: 0x0024D964 File Offset: 0x0024BB64
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Tools).LoadViewState(array[1]);
		}

		// Token: 0x0600A9AE RID: 43438 RVA: 0x0024D990 File Offset: 0x0024BB90
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Tools).SaveViewState()
			};
		}

		// Token: 0x0600A9AF RID: 43439 RVA: 0x0024D9BE File Offset: 0x0024BBBE
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Tools).TrackViewState();
		}

		// Token: 0x0600A9B0 RID: 43440 RVA: 0x0024D9D1 File Offset: 0x0024BBD1
		internal override void SetDirty()
		{
			base.SetDirty();
			this.Tools.SetDirty();
		}

		// Token: 0x04002DB8 RID: 11704
		private EditorToolCollection _tools;
	}
}
