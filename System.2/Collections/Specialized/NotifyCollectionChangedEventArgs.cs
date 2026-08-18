using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	// Token: 0x020003B2 RID: 946
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[__DynamicallyInvokable]
	public class NotifyCollectionChangedEventArgs : EventArgs
	{
		// Token: 0x0600237D RID: 9085 RVA: 0x000A7F70 File Offset: 0x000A6170
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
		{
			if (action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[]
				{
					NotifyCollectionChangedAction.Reset
				}), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000A7FC4 File Offset: 0x000A61C4
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[]
				{
					changedItem
				}, -1);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000A8040 File Offset: 0x000A6240
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[]
				{
					changedItem
				}, index);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
			}
			if (index != -1)
			{
				throw new ArgumentException(SR.GetString("ResetActionRequiresIndexMinus1"), "action");
			}
			this.InitializeAdd(action, null, -1);
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x000A80D4 File Offset: 0x000A62D4
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				this.InitializeAddOrRemove(action, changedItems, -1);
				return;
			}
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x000A8154 File Offset: 0x000A6354
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException(SR.GetString("MustBeResetAddOrRemoveActionForCtor"), "action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException(SR.GetString("ResetActionRequiresNullItem"), "action");
				}
				if (startingIndex != -1)
				{
					throw new ArgumentException(SR.GetString("ResetActionRequiresIndexMinus1"), "action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				if (startingIndex < -1)
				{
					throw new ArgumentException(SR.GetString("IndexCannotBeNegative"), "startingIndex");
				}
				this.InitializeAddOrRemove(action, changedItems, startingIndex);
				return;
			}
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x000A8204 File Offset: 0x000A6404
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[]
				{
					NotifyCollectionChangedAction.Replace
				}), "action");
			}
			this.InitializeMoveOrReplace(action, new object[]
			{
				newItem
			}, new object[]
			{
				oldItem
			}, -1, -1);
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x000A826C File Offset: 0x000A646C
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[]
				{
					NotifyCollectionChangedAction.Replace
				}), "action");
			}
			this.InitializeMoveOrReplace(action, new object[]
			{
				newItem
			}, new object[]
			{
				oldItem
			}, index, index);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000A82D8 File Offset: 0x000A64D8
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[]
				{
					NotifyCollectionChangedAction.Replace
				}), "action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x000A8348 File Offset: 0x000A6548
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[]
				{
					NotifyCollectionChangedAction.Replace
				}), "action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x000A83BC File Offset: 0x000A65BC
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[]
				{
					NotifyCollectionChangedAction.Move
				}), "action");
			}
			if (index < 0)
			{
				throw new ArgumentException(SR.GetString("IndexCannotBeNegative"), "index");
			}
			object[] array = new object[]
			{
				changedItem
			};
			this.InitializeMoveOrReplace(action, array, array, index, oldIndex);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x000A8438 File Offset: 0x000A6638
		[__DynamicallyInvokable]
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException(SR.GetString("WrongActionForCtor", new object[]
				{
					NotifyCollectionChangedAction.Move
				}), "action");
			}
			if (index < 0)
			{
				throw new ArgumentException(SR.GetString("IndexCannotBeNegative"), "index");
			}
			this.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x000A84A8 File Offset: 0x000A66A8
		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int newIndex, int oldIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : ArrayList.ReadOnly(newItems));
			this._oldItems = ((oldItems == null) ? null : ArrayList.ReadOnly(oldItems));
			this._newStartingIndex = newIndex;
			this._oldStartingIndex = oldIndex;
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x000A8504 File Offset: 0x000A6704
		private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action == NotifyCollectionChangedAction.Add)
			{
				this.InitializeAdd(action, changedItems, startingIndex);
				return;
			}
			if (action == NotifyCollectionChangedAction.Remove)
			{
				this.InitializeRemove(action, changedItems, startingIndex);
			}
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000A8520 File Offset: 0x000A6720
		private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : ArrayList.ReadOnly(newItems));
			this._newStartingIndex = newStartingIndex;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000A8542 File Offset: 0x000A6742
		private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
		{
			this._action = action;
			this._oldItems = ((oldItems == null) ? null : ArrayList.ReadOnly(oldItems));
			this._oldStartingIndex = oldStartingIndex;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000A8564 File Offset: 0x000A6764
		private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
		{
			this.InitializeAdd(action, newItems, startingIndex);
			this.InitializeRemove(action, oldItems, oldStartingIndex);
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000A857A File Offset: 0x000A677A
		[__DynamicallyInvokable]
		public NotifyCollectionChangedAction Action
		{
			[__DynamicallyInvokable]
			get
			{
				return this._action;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x000A8582 File Offset: 0x000A6782
		[__DynamicallyInvokable]
		public IList NewItems
		{
			[__DynamicallyInvokable]
			get
			{
				return this._newItems;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x000A858A File Offset: 0x000A678A
		[__DynamicallyInvokable]
		public IList OldItems
		{
			[__DynamicallyInvokable]
			get
			{
				return this._oldItems;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x000A8592 File Offset: 0x000A6792
		[__DynamicallyInvokable]
		public int NewStartingIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return this._newStartingIndex;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000A859A File Offset: 0x000A679A
		[__DynamicallyInvokable]
		public int OldStartingIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return this._oldStartingIndex;
			}
		}

		// Token: 0x04001FE5 RID: 8165
		private NotifyCollectionChangedAction _action;

		// Token: 0x04001FE6 RID: 8166
		private IList _newItems;

		// Token: 0x04001FE7 RID: 8167
		private IList _oldItems;

		// Token: 0x04001FE8 RID: 8168
		private int _newStartingIndex = -1;

		// Token: 0x04001FE9 RID: 8169
		private int _oldStartingIndex = -1;
	}
}
