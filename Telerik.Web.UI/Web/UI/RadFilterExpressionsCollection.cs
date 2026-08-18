using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001888 RID: 6280
	public class RadFilterExpressionsCollection : IList<RadFilterExpression>, ICollection<RadFilterExpression>, IEnumerable<RadFilterExpression>, IEnumerable, IStateManager
	{
		// Token: 0x0600F318 RID: 62232 RVA: 0x003758B9 File Offset: 0x00373AB9
		public RadFilterExpressionsCollection()
		{
			this._expressions = new List<RadFilterExpression>();
		}

		// Token: 0x0600F319 RID: 62233 RVA: 0x003758CC File Offset: 0x00373ACC
		private void InsertInternal(int index, RadFilterExpression item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this._isTrackingViewState)
			{
				((IStateManager)item).TrackViewState();
			}
			if (index < 0)
			{
				this._expressions.Add(item);
				return;
			}
			this._expressions.Insert(index, item);
		}

		// Token: 0x0600F31A RID: 62234 RVA: 0x00375908 File Offset: 0x00373B08
		private bool RemoveInternal(int index, RadFilterExpression item)
		{
			bool result;
			if (index < 0)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item", "Value cannot be null.");
				}
				result = this._expressions.Remove(item);
			}
			else
			{
				this._expressions.RemoveAt(index);
				result = true;
			}
			return result;
		}

		// Token: 0x0600F31B RID: 62235 RVA: 0x0037594C File Offset: 0x00373B4C
		public int IndexOf(RadFilterExpression item)
		{
			return this._expressions.IndexOf(item);
		}

		// Token: 0x0600F31C RID: 62236 RVA: 0x0037595A File Offset: 0x00373B5A
		public void Insert(int index, RadFilterExpression item)
		{
			this.InsertInternal(index, item);
		}

		// Token: 0x0600F31D RID: 62237 RVA: 0x00375964 File Offset: 0x00373B64
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x1700494A RID: 18762
		public RadFilterExpression this[int index]
		{
			get
			{
				return this._expressions[index];
			}
			set
			{
				this._expressions[index] = value;
			}
		}

		// Token: 0x0600F320 RID: 62240 RVA: 0x0037598C File Offset: 0x00373B8C
		public void Add(RadFilterExpression item)
		{
			this.InsertInternal(-1, item);
		}

		// Token: 0x0600F321 RID: 62241 RVA: 0x00375996 File Offset: 0x00373B96
		public void Clear()
		{
			this._expressions.Clear();
		}

		// Token: 0x0600F322 RID: 62242 RVA: 0x003759A3 File Offset: 0x00373BA3
		public bool Contains(RadFilterExpression item)
		{
			return this._expressions.Contains(item);
		}

		// Token: 0x0600F323 RID: 62243 RVA: 0x003759B1 File Offset: 0x00373BB1
		public void CopyTo(RadFilterExpression[] array, int arrayIndex)
		{
			this._expressions.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700494B RID: 18763
		// (get) Token: 0x0600F324 RID: 62244 RVA: 0x003759C0 File Offset: 0x00373BC0
		public int Count
		{
			get
			{
				return this._expressions.Count;
			}
		}

		// Token: 0x1700494C RID: 18764
		// (get) Token: 0x0600F325 RID: 62245 RVA: 0x003759CD File Offset: 0x00373BCD
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600F326 RID: 62246 RVA: 0x003759D0 File Offset: 0x00373BD0
		public bool Remove(RadFilterExpression item)
		{
			return this.RemoveInternal(-1, item);
		}

		// Token: 0x0600F327 RID: 62247 RVA: 0x003759DA File Offset: 0x00373BDA
		public IEnumerator<RadFilterExpression> GetEnumerator()
		{
			return this._expressions.GetEnumerator();
		}

		// Token: 0x0600F328 RID: 62248 RVA: 0x003759EC File Offset: 0x00373BEC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700494D RID: 18765
		// (get) Token: 0x0600F329 RID: 62249 RVA: 0x003759F4 File Offset: 0x00373BF4
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600F32A RID: 62250 RVA: 0x003759FC File Offset: 0x00373BFC
		void IStateManager.LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				int num = (int)((Pair)array[0]).First;
				int num2 = (int)((Pair)array[0]).Second;
				int num3 = 0;
				while (num3 < num2 && num3 < array.Length)
				{
					Pair pair = array[num3 + 1] as Pair;
					if (pair != null)
					{
						RadFilterExpression radFilterExpression = this[num3];
						((IStateManager)radFilterExpression).LoadViewState(pair.Second);
					}
					num3++;
				}
				int num4 = num2;
				while (num4 < num && num4 < array.Length)
				{
					Pair pair2 = array[num4 + 1] as Pair;
					if (pair2 != null)
					{
						Pair pair3 = pair2.First as Pair;
						if (pair3 != null)
						{
							RadFilterExpression radFilterExpression2 = this.CreateExpressionFromTypeName((string)pair3.First, (string)pair3.Second);
							this.Add(radFilterExpression2);
							((IStateManager)radFilterExpression2).LoadViewState(pair2.Second);
						}
					}
					num4++;
				}
			}
		}

		// Token: 0x0600F32B RID: 62251 RVA: 0x00375AF0 File Offset: 0x00373CF0
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.Count, this._notTrackedExpressionCount));
			bool flag = false;
			foreach (RadFilterExpression radFilterExpression in this)
			{
				string y = string.Empty;
				if (radFilterExpression.FilterFunction != RadFilterFunction.Group)
				{
					y = ((RadFilterNonGroupExpression)radFilterExpression).FieldType.FullName;
				}
				flag = true;
				arrayList.Add(new Pair(new Pair(radFilterExpression.GetType().Name, y), ((IStateManager)radFilterExpression).SaveViewState()));
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600F32C RID: 62252 RVA: 0x00375BC4 File Offset: 0x00373DC4
		void IStateManager.TrackViewState()
		{
			if (this._isMarked)
			{
				return;
			}
			this._isMarked = true;
			this._notTrackedExpressionCount = this.Count;
			this._isTrackingViewState = true;
			this._expressions.ForEach(delegate(RadFilterExpression item)
			{
				((IStateManager)item).TrackViewState();
			});
		}

		// Token: 0x0600F32D RID: 62253 RVA: 0x00375C1C File Offset: 0x00373E1C
		protected virtual RadFilterExpression CreateExpressionFromTypeName(string expressionTypeName, string expressionFieldType)
		{
			return RadFilterExpression.CreateExpressionFromTypeName(expressionTypeName, expressionFieldType);
		}

		// Token: 0x040045CB RID: 17867
		private List<RadFilterExpression> _expressions;

		// Token: 0x040045CC RID: 17868
		private bool _isTrackingViewState;

		// Token: 0x040045CD RID: 17869
		private bool _isMarked;

		// Token: 0x040045CE RID: 17870
		private int _notTrackedExpressionCount;
	}
}
