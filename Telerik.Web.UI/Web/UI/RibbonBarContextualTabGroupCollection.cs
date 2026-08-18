using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E5E RID: 3678
	public class RibbonBarContextualTabGroupCollection : List<RibbonBarContextualTabGroup>, IRibbonBarSubComponent, IStateManager
	{
		// Token: 0x06008B99 RID: 35737 RVA: 0x001FBE28 File Offset: 0x001FA028
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array.Length != base.Count)
				{
					return;
				}
				for (int i = 0; i < base.Count; i++)
				{
					((IStateManager)base[i]).LoadViewState(array[i]);
				}
			}
		}

		// Token: 0x06008B9A RID: 35738 RVA: 0x001FBE6C File Offset: 0x001FA06C
		object IStateManager.SaveViewState()
		{
			if (base.Count > 0)
			{
				object[] array = new object[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = ((IStateManager)base[i]).SaveViewState();
				}
				return array;
			}
			return null;
		}

		// Token: 0x06008B9B RID: 35739 RVA: 0x001FBEB4 File Offset: 0x001FA0B4
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			foreach (IStateManager stateManager in this)
			{
				stateManager.TrackViewState();
			}
		}

		// Token: 0x17002C23 RID: 11299
		// (get) Token: 0x06008B9C RID: 35740 RVA: 0x001FBF08 File Offset: 0x001FA108
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x17002C24 RID: 11300
		// (get) Token: 0x06008B9D RID: 35741 RVA: 0x001FBF10 File Offset: 0x001FA110
		// (set) Token: 0x06008B9E RID: 35742 RVA: 0x001FBF18 File Offset: 0x001FA118
		public RadRibbonBar RibbonBar
		{
			get
			{
				return this._ribbonBar;
			}
			internal set
			{
				this._ribbonBar = value;
			}
		}

		// Token: 0x17002C25 RID: 11301
		// (get) Token: 0x06008B9F RID: 35743 RVA: 0x001FBF21 File Offset: 0x001FA121
		// (set) Token: 0x06008BA0 RID: 35744 RVA: 0x001FBF29 File Offset: 0x001FA129
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
			}
		}

		// Token: 0x06008BA1 RID: 35745 RVA: 0x001FBF32 File Offset: 0x001FA132
		public new void Add(RibbonBarContextualTabGroup contextualTabGroup)
		{
			base.Add(contextualTabGroup);
			this.OnContextualTabGroupAdded(contextualTabGroup);
			if (this._isTrackingViewState)
			{
				((IStateManager)contextualTabGroup).TrackViewState();
			}
		}

		// Token: 0x06008BA2 RID: 35746 RVA: 0x001FBF50 File Offset: 0x001FA150
		public new void Insert(int index, RibbonBarContextualTabGroup contextualTabGroup)
		{
			base.Insert(index, contextualTabGroup);
			if (this._isTrackingViewState)
			{
				((IStateManager)contextualTabGroup).TrackViewState();
			}
		}

		// Token: 0x06008BA3 RID: 35747 RVA: 0x001FBF68 File Offset: 0x001FA168
		private void OnContextualTabGroupAdded(RibbonBarContextualTabGroup contextualTabGroup)
		{
			contextualTabGroup.Container = this;
			contextualTabGroup.ParentWebControl = this.ParentWebControl;
			contextualTabGroup.Tabs.RibbonBar = this.RibbonBar;
		}

		// Token: 0x06008BA4 RID: 35748 RVA: 0x001FBF8E File Offset: 0x001FA18E
		public new void Remove(RibbonBarContextualTabGroup contextualTabGroup)
		{
			base.Remove(contextualTabGroup);
			this.OnContextualTabGroupRemoved(contextualTabGroup);
		}

		// Token: 0x06008BA5 RID: 35749 RVA: 0x001FBF9F File Offset: 0x001FA19F
		private void OnContextualTabGroupRemoved(RibbonBarContextualTabGroup contextualTabGroup)
		{
			contextualTabGroup.Container = null;
			contextualTabGroup.ParentWebControl = null;
		}

		// Token: 0x04002717 RID: 10007
		private bool _isTrackingViewState;

		// Token: 0x04002718 RID: 10008
		private RadRibbonBar _ribbonBar;

		// Token: 0x04002719 RID: 10009
		private WebControl _parentWebControl;
	}
}
