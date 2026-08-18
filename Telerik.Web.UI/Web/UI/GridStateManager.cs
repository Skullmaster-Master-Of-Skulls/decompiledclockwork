using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001176 RID: 4470
	[Serializable]
	public class GridStateManager : IStateManager
	{
		// Token: 0x0600B643 RID: 46659 RVA: 0x00281A22 File Offset: 0x0027FC22
		public void AddManager(IStateManager manager, string Key)
		{
			this.list[Key] = manager;
			if (this.IsTrackingViewState)
			{
				manager.TrackViewState();
			}
		}

		// Token: 0x17003AEE RID: 15086
		// (get) Token: 0x0600B644 RID: 46660 RVA: 0x00281A3F File Offset: 0x0027FC3F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public StateBag ViewState
		{
			get
			{
				return this._viewState;
			}
		}

		// Token: 0x0600B645 RID: 46661 RVA: 0x00281A48 File Offset: 0x0027FC48
		public void LoadViewState(object state)
		{
			((IStateManager)this._viewState).LoadViewState(state);
			foreach (object obj in this.list)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string key = (string)dictionaryEntry.Key;
				IStateManager stateManager = (IStateManager)dictionaryEntry.Value;
				stateManager.LoadViewState(this._viewState[key]);
			}
		}

		// Token: 0x0600B646 RID: 46662 RVA: 0x00281AD8 File Offset: 0x0027FCD8
		public object SaveViewState()
		{
			foreach (object obj in this.list)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string key = (string)dictionaryEntry.Key;
				IStateManager stateManager = (IStateManager)dictionaryEntry.Value;
				this._viewState[key] = stateManager.SaveViewState();
			}
			return ((IStateManager)this._viewState).SaveViewState();
		}

		// Token: 0x0600B647 RID: 46663 RVA: 0x00281B64 File Offset: 0x0027FD64
		public void TrackViewState()
		{
			this._isTrackingViewState = true;
			((IStateManager)this._viewState).TrackViewState();
			foreach (object obj in this.list)
			{
				IStateManager stateManager = (IStateManager)((DictionaryEntry)obj).Value;
				stateManager.TrackViewState();
			}
		}

		// Token: 0x17003AEF RID: 15087
		// (get) Token: 0x0600B648 RID: 46664 RVA: 0x00281BDC File Offset: 0x0027FDDC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600B649 RID: 46665 RVA: 0x00281BE4 File Offset: 0x0027FDE4
		public string ViewStateGetString(string key)
		{
			return this.ViewStateGetString(key, "");
		}

		// Token: 0x0600B64A RID: 46666 RVA: 0x00281BF4 File Offset: 0x0027FDF4
		public string ViewStateGetString(string key, string defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj != null)
			{
				return (string)obj;
			}
			return defaultValue;
		}

		// Token: 0x0600B64B RID: 46667 RVA: 0x00281C19 File Offset: 0x0027FE19
		public int ViewStateGetInt(string key)
		{
			return this.ViewStateGetInt(key, 0);
		}

		// Token: 0x0600B64C RID: 46668 RVA: 0x00281C24 File Offset: 0x0027FE24
		public int ViewStateGetInt(string key, int defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj != null)
			{
				return (int)obj;
			}
			return defaultValue;
		}

		// Token: 0x0600B64D RID: 46669 RVA: 0x00281C49 File Offset: 0x0027FE49
		public bool ViewStateGetBool(string key)
		{
			return this.ViewStateGetBool(key, false);
		}

		// Token: 0x0600B64E RID: 46670 RVA: 0x00281C54 File Offset: 0x0027FE54
		public bool ViewStateGetBool(string key, bool defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj != null)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x0600B64F RID: 46671 RVA: 0x00281C7C File Offset: 0x0027FE7C
		public object ViewStateGetObject(string key, object defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj != null)
			{
				return obj;
			}
			return defaultValue;
		}

		// Token: 0x04003004 RID: 12292
		private Hashtable list = new Hashtable();

		// Token: 0x04003005 RID: 12293
		private bool _isTrackingViewState;

		// Token: 0x04003006 RID: 12294
		private StateBag _viewState = new StateBag();
	}
}
