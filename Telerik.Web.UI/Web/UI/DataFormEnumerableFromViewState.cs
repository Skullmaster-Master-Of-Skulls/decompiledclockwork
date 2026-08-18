using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020001EA RID: 490
	internal class DataFormEnumerableFromViewState : DataFormEnumerableBase
	{
		// Token: 0x06001154 RID: 4436 RVA: 0x0003F076 File Offset: 0x0003D276
		public DataFormEnumerableFromViewState(DataFormControlStateManager viewState)
		{
			this._viewState = viewState;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0003F085 File Offset: 0x0003D285
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0003F088 File Offset: 0x0003D288
		public override IEnumerable RawEnumerable()
		{
			return new DataFormEnumerableFromViewState.DataFormDummyDataSource(this.Count);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0003F095 File Offset: 0x0003D295
		protected override void TransformEnumerable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0003F09C File Offset: 0x0003D29C
		public override int DataSourceCount
		{
			get
			{
				object obj = this._viewState["_!DSIC"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0003F0C8 File Offset: 0x0003D2C8
		public override int Count
		{
			get
			{
				object obj = this._viewState["_!ItemCount"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x040004FC RID: 1276
		private readonly DataFormControlStateManager _viewState;

		// Token: 0x020001EB RID: 491
		internal class DataFormDummyDataSource : ICollection, IEnumerable
		{
			// Token: 0x0600115A RID: 4442 RVA: 0x0003F0F1 File Offset: 0x0003D2F1
			public DataFormDummyDataSource(int itemsCount)
			{
				this._itemsCount = itemsCount;
			}

			// Token: 0x0600115B RID: 4443 RVA: 0x0003F198 File Offset: 0x0003D398
			public IEnumerator GetEnumerator()
			{
				for (int i = 0; i < this._itemsCount; i++)
				{
					yield return null;
				}
				yield break;
			}

			// Token: 0x0600115C RID: 4444 RVA: 0x0003F1B4 File Offset: 0x0003D3B4
			public void CopyTo(Array array, int index)
			{
				foreach (object value in this)
				{
					array.SetValue(value, index++);
				}
			}

			// Token: 0x170005D8 RID: 1496
			// (get) Token: 0x0600115D RID: 4445 RVA: 0x0003F20C File Offset: 0x0003D40C
			public int Count
			{
				get
				{
					return this._itemsCount;
				}
			}

			// Token: 0x170005D9 RID: 1497
			// (get) Token: 0x0600115E RID: 4446 RVA: 0x0003F214 File Offset: 0x0003D414
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170005DA RID: 1498
			// (get) Token: 0x0600115F RID: 4447 RVA: 0x0003F217 File Offset: 0x0003D417
			public object SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x040004FD RID: 1277
			private readonly int _itemsCount;
		}
	}
}
