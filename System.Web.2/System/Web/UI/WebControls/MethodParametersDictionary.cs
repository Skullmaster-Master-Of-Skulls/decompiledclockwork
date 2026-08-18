using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000478 RID: 1144
	internal sealed class MethodParametersDictionary : IDictionary<string, MethodParameterValue>, ICollection<KeyValuePair<string, MethodParameterValue>>, IEnumerable<KeyValuePair<string, MethodParameterValue>>, IEnumerable, IStateManager
	{
		// Token: 0x0600386E RID: 14446 RVA: 0x000B7E6A File Offset: 0x000B606A
		internal void CallOnParametersChanged()
		{
			this.OnParametersChanged(EventArgs.Empty);
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000B7E77 File Offset: 0x000B6077
		private void OnParametersChanged(EventArgs e)
		{
			if (this._parametersChangedHandler != null)
			{
				this._parametersChangedHandler(this, e);
			}
		}

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x06003870 RID: 14448 RVA: 0x000B7E8E File Offset: 0x000B608E
		// (remove) Token: 0x06003871 RID: 14449 RVA: 0x000B7EA7 File Offset: 0x000B60A7
		public event EventHandler ParametersChanged
		{
			add
			{
				this._parametersChangedHandler = (EventHandler)Delegate.Combine(this._parametersChangedHandler, value);
			}
			remove
			{
				this._parametersChangedHandler = (EventHandler)Delegate.Remove(this._parametersChangedHandler, value);
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000B7EC0 File Offset: 0x000B60C0
		public int Count
		{
			get
			{
				return this.InnerDictionary.Count;
			}
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x000B7ED0 File Offset: 0x000B60D0
		private void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				string[] array = (string[])pair.First;
				object[] array2 = (object[])pair.Second;
				for (int i = 0; i < array.Length; i++)
				{
					string key = array[i];
					MethodParameterValue methodParameterValue = new MethodParameterValue();
					this.Add(key, methodParameterValue);
					((IStateManager)methodParameterValue).LoadViewState(array2[i]);
				}
			}
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000B7F30 File Offset: 0x000B6130
		private object SaveViewState()
		{
			bool flag = false;
			int count = this.Count;
			string[] array = new string[count];
			object[] array2 = new object[count];
			int num = 0;
			foreach (KeyValuePair<string, MethodParameterValue> keyValuePair in this.InnerDictionary)
			{
				array[num] = keyValuePair.Key;
				array2[num] = ((IStateManager)keyValuePair.Value).SaveViewState();
				if (array2[num] != null)
				{
					flag = true;
				}
				num++;
			}
			if (!flag)
			{
				return null;
			}
			return new Pair(array, array2);
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000B7FD0 File Offset: 0x000B61D0
		private void TrackViewState()
		{
			this._tracking = true;
			foreach (MethodParameterValue methodParameterValue in this.InnerDictionary.Values)
			{
				((IStateManager)methodParameterValue).TrackViewState();
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000B8030 File Offset: 0x000B6230
		private Dictionary<string, MethodParameterValue> InnerDictionary
		{
			get
			{
				if (this._innerDictionary == null)
				{
					this._innerDictionary = new Dictionary<string, MethodParameterValue>();
				}
				return this._innerDictionary;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000B804B File Offset: 0x000B624B
		public ICollection<string> Keys
		{
			get
			{
				return this.InnerDictionary.Keys;
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x000B8058 File Offset: 0x000B6258
		public ICollection<MethodParameterValue> Values
		{
			get
			{
				return this.InnerDictionary.Values;
			}
		}

		// Token: 0x1700108C RID: 4236
		public MethodParameterValue this[string key]
		{
			get
			{
				return this.InnerDictionary[key];
			}
			set
			{
				this.InnerDictionary[key] = value;
				if (value != null)
				{
					value.SetOwner(this);
				}
			}
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x000B808C File Offset: 0x000B628C
		public void Add(string key, MethodParameterValue value)
		{
			this.InnerDictionary.Add(key, value);
			if (value != null)
			{
				value.SetOwner(this);
				if (this._tracking)
				{
					((IStateManager)value).TrackViewState();
				}
			}
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000B80B3 File Offset: 0x000B62B3
		public bool ContainsKey(string key)
		{
			return this.InnerDictionary.ContainsKey(key);
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000B80C4 File Offset: 0x000B62C4
		public bool Remove(string key)
		{
			if (this.InnerDictionary.ContainsKey(key))
			{
				MethodParameterValue methodParameterValue = this.InnerDictionary[key];
				if (methodParameterValue != null)
				{
					methodParameterValue.SetOwner(null);
				}
			}
			return this.InnerDictionary.Remove(key);
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000B8102 File Offset: 0x000B6302
		public bool TryGetValue(string key, out MethodParameterValue value)
		{
			return this.InnerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000B8111 File Offset: 0x000B6311
		void ICollection<KeyValuePair<string, MethodParameterValue>>.Clear()
		{
			this.InnerDictionary.Clear();
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000B811E File Offset: 0x000B631E
		bool ICollection<KeyValuePair<string, MethodParameterValue>>.IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, MethodParameterValue>>)this.InnerDictionary).IsReadOnly;
			}
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000B812B File Offset: 0x000B632B
		void ICollection<KeyValuePair<string, MethodParameterValue>>.Add(KeyValuePair<string, MethodParameterValue> item)
		{
			this.Add(item.Key, item.Value);
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000B8141 File Offset: 0x000B6341
		bool ICollection<KeyValuePair<string, MethodParameterValue>>.Contains(KeyValuePair<string, MethodParameterValue> item)
		{
			return ((ICollection<KeyValuePair<string, MethodParameterValue>>)this.InnerDictionary).Contains(item);
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000B814F File Offset: 0x000B634F
		void ICollection<KeyValuePair<string, MethodParameterValue>>.CopyTo(KeyValuePair<string, MethodParameterValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, MethodParameterValue>>)this.InnerDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000B815E File Offset: 0x000B635E
		bool ICollection<KeyValuePair<string, MethodParameterValue>>.Remove(KeyValuePair<string, MethodParameterValue> item)
		{
			return ((ICollection<KeyValuePair<string, MethodParameterValue>>)this.InnerDictionary).Remove(item);
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000B816C File Offset: 0x000B636C
		public IEnumerator<KeyValuePair<string, MethodParameterValue>> GetEnumerator()
		{
			return this.InnerDictionary.GetEnumerator();
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000B817E File Offset: 0x000B637E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000B8186 File Offset: 0x000B6386
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000B818E File Offset: 0x000B638E
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000B8197 File Offset: 0x000B6397
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000B819F File Offset: 0x000B639F
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x04002288 RID: 8840
		private bool _tracking;

		// Token: 0x04002289 RID: 8841
		private EventHandler _parametersChangedHandler;

		// Token: 0x0400228A RID: 8842
		private Dictionary<string, MethodParameterValue> _innerDictionary;
	}
}
