using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200183F RID: 6207
	public class LayoutBuilderCell : StateManager, IAttributeAccessor
	{
		// Token: 0x170048DC RID: 18652
		// (get) Token: 0x0600F127 RID: 61735 RVA: 0x0036D3E5 File Offset: 0x0036B5E5
		// (set) Token: 0x0600F128 RID: 61736 RVA: 0x0036D414 File Offset: 0x0036B614
		public virtual string ID
		{
			get
			{
				if (base.ViewState["ID"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ID"];
			}
			set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x170048DD RID: 18653
		// (get) Token: 0x0600F129 RID: 61737 RVA: 0x0036D427 File Offset: 0x0036B627
		// (set) Token: 0x0600F12A RID: 61738 RVA: 0x0036D456 File Offset: 0x0036B656
		public virtual string Content
		{
			get
			{
				if (base.ViewState["Content"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Content"];
			}
			set
			{
				base.ViewState["Content"] = value;
			}
		}

		// Token: 0x170048DE RID: 18654
		// (get) Token: 0x0600F12B RID: 61739 RVA: 0x0036D469 File Offset: 0x0036B669
		// (set) Token: 0x0600F12C RID: 61740 RVA: 0x0036D498 File Offset: 0x0036B698
		public virtual string ColSpan
		{
			get
			{
				if (base.ViewState["ColSpan"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ColSpan"];
			}
			set
			{
				base.ViewState["ColSpan"] = value;
			}
		}

		// Token: 0x170048DF RID: 18655
		// (get) Token: 0x0600F12D RID: 61741 RVA: 0x0036D4AB File Offset: 0x0036B6AB
		// (set) Token: 0x0600F12E RID: 61742 RVA: 0x0036D4DA File Offset: 0x0036B6DA
		public virtual string RowSpan
		{
			get
			{
				if (base.ViewState["RowSpan"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["RowSpan"];
			}
			set
			{
				base.ViewState["RowSpan"] = value;
			}
		}

		// Token: 0x170048E0 RID: 18656
		// (get) Token: 0x0600F12F RID: 61743 RVA: 0x0036D4ED File Offset: 0x0036B6ED
		// (set) Token: 0x0600F130 RID: 61744 RVA: 0x0036D51C File Offset: 0x0036B71C
		public virtual string Width
		{
			get
			{
				if (base.ViewState["Width"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Width"];
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170048E1 RID: 18657
		// (get) Token: 0x0600F131 RID: 61745 RVA: 0x0036D52F File Offset: 0x0036B72F
		// (set) Token: 0x0600F132 RID: 61746 RVA: 0x0036D55E File Offset: 0x0036B75E
		public virtual string Height
		{
			get
			{
				if (base.ViewState["Height"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["Height"];
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x0600F133 RID: 61747 RVA: 0x0036D571 File Offset: 0x0036B771
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this._attributeState == null)
			{
				return null;
			}
			return this.Attributes[key];
		}

		// Token: 0x0600F134 RID: 61748 RVA: 0x0036D589 File Offset: 0x0036B789
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x170048E2 RID: 18658
		// (get) Token: 0x0600F135 RID: 61749 RVA: 0x0036D598 File Offset: 0x0036B798
		public virtual LayoutBuilderAttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					this._attributes = new LayoutBuilderAttributeCollection(this.AttributeState);
				}
				return this._attributes;
			}
		}

		// Token: 0x170048E3 RID: 18659
		// (get) Token: 0x0600F136 RID: 61750 RVA: 0x0036D5B9 File Offset: 0x0036B7B9
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

		// Token: 0x04004566 RID: 17766
		private LayoutBuilderAttributeCollection _attributes;

		// Token: 0x04004567 RID: 17767
		private StateBag _attributeState;
	}
}
