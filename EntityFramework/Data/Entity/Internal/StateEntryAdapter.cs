using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000783 RID: 1923
	internal class StateEntryAdapter : IEntityStateEntry
	{
		// Token: 0x06005718 RID: 22296 RVA: 0x00178237 File Offset: 0x00176437
		public StateEntryAdapter(ObjectStateEntry stateEntry)
		{
			this._stateEntry = stateEntry;
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005719 RID: 22297 RVA: 0x00178246 File Offset: 0x00176446
		public object Entity
		{
			get
			{
				return this._stateEntry.Entity;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x0600571A RID: 22298 RVA: 0x00178253 File Offset: 0x00176453
		public EntityState State
		{
			get
			{
				return this._stateEntry.State;
			}
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x00178260 File Offset: 0x00176460
		public void ChangeState(EntityState state)
		{
			this._stateEntry.ChangeState(state);
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x0600571C RID: 22300 RVA: 0x0017826E File Offset: 0x0017646E
		public DbUpdatableDataRecord CurrentValues
		{
			get
			{
				return this._stateEntry.CurrentValues;
			}
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x0017827B File Offset: 0x0017647B
		public DbUpdatableDataRecord GetUpdatableOriginalValues()
		{
			return this._stateEntry.GetUpdatableOriginalValues();
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x0600571E RID: 22302 RVA: 0x00178288 File Offset: 0x00176488
		public EntitySetBase EntitySet
		{
			get
			{
				return this._stateEntry.EntitySet;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x0600571F RID: 22303 RVA: 0x00178295 File Offset: 0x00176495
		public EntityKey EntityKey
		{
			get
			{
				return this._stateEntry.EntityKey;
			}
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x001782A2 File Offset: 0x001764A2
		public IEnumerable<string> GetModifiedProperties()
		{
			return this._stateEntry.GetModifiedProperties();
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x001782AF File Offset: 0x001764AF
		public void SetModifiedProperty(string propertyName)
		{
			this._stateEntry.SetModifiedProperty(propertyName);
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x001782BD File Offset: 0x001764BD
		public void RejectPropertyChanges(string propertyName)
		{
			this._stateEntry.RejectPropertyChanges(propertyName);
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x001782CB File Offset: 0x001764CB
		public bool IsPropertyChanged(string propertyName)
		{
			return this._stateEntry.IsPropertyChanged(propertyName);
		}

		// Token: 0x04002321 RID: 8993
		private readonly ObjectStateEntry _stateEntry;
	}
}
