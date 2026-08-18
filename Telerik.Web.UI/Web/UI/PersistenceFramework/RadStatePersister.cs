using System;
using System.Web.UI;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000194 RID: 404
	public abstract class RadStatePersister : IStatePersister
	{
		// Token: 0x06000DBC RID: 3516 RVA: 0x000340D7 File Offset: 0x000322D7
		public RadStatePersister()
		{
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000340DF File Offset: 0x000322DF
		public RadStatePersister(IStateStorageProvider storageProvider, IStateSerializer stateSerializer)
		{
			this._storageProvider = storageProvider;
			this._stateSerializer = stateSerializer;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000340F8 File Offset: 0x000322F8
		public void SaveState(Control ctrl)
		{
			this.ReadSettings(ctrl);
			StatePersisterEventArgs e = new StatePersisterEventArgs(ctrl, this.currentState);
			this.OnStateSave(e);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00034120 File Offset: 0x00032320
		public void SaveState(Control ctrl, string key)
		{
			this.ReadSettings(ctrl);
			StatePersisterEventArgs e = new StatePersisterEventArgs(ctrl, this.currentState);
			this.OnStateSave(e);
			if (this._stateSerializer != null)
			{
				string serializedState = this._stateSerializer.Serialize(this.currentState);
				this._storageProvider.SaveStateToStorage(key, serializedState);
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00034170 File Offset: 0x00032370
		public void LoadState(Control ctrl, string key)
		{
			if (!object.Equals(null, this._stateSerializer) && !object.Equals(null, this._storageProvider))
			{
				string stateData = this._storageProvider.LoadStateFromStorage(key);
				this.currentState = this._stateSerializer.Deserialize(stateData);
			}
			StatePersisterEventArgs e = new StatePersisterEventArgs(ctrl, this.currentState);
			this.OnStateLoad(e);
			this.ApplySettings(ctrl);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000341D4 File Offset: 0x000323D4
		public void LoadState(Control ctrl, RadControlState state)
		{
			this.currentState = state;
			StatePersisterEventArgs e = new StatePersisterEventArgs(ctrl, state);
			this.OnStateLoad(e);
			this.ApplySettings(ctrl);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x000341FE File Offset: 0x000323FE
		public virtual void ReadSettings(Control ctrl)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00034205 File Offset: 0x00032405
		public virtual void ApplySettings(Control ctrl)
		{
			throw new NotImplementedException();
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000DC4 RID: 3524 RVA: 0x0003420C File Offset: 0x0003240C
		// (remove) Token: 0x06000DC5 RID: 3525 RVA: 0x00034244 File Offset: 0x00032444
		public event StateSaveEventHandler StateSave;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000DC6 RID: 3526 RVA: 0x0003427C File Offset: 0x0003247C
		// (remove) Token: 0x06000DC7 RID: 3527 RVA: 0x000342B4 File Offset: 0x000324B4
		public event StateLoadEventHandler StateLoad;

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000342E9 File Offset: 0x000324E9
		protected virtual void OnStateSave(StatePersisterEventArgs e)
		{
			if (this.StateSave != null)
			{
				this.StateSave(this, e);
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00034300 File Offset: 0x00032500
		protected virtual void OnStateLoad(StatePersisterEventArgs e)
		{
			if (this.StateLoad != null)
			{
				this.StateLoad(this, e);
			}
		}

		// Token: 0x040003F2 RID: 1010
		private readonly IStateStorageProvider _storageProvider;

		// Token: 0x040003F3 RID: 1011
		private readonly IStateSerializer _stateSerializer;

		// Token: 0x040003F4 RID: 1012
		protected RadControlState currentState;
	}
}
