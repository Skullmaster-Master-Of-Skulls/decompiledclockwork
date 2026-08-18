using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001896 RID: 6294
	[PersistChildren(false)]
	public class RadFilterDataFieldEditorCollection : IList, ICollection, IList<RadFilterDataFieldEditor>, ICollection<RadFilterDataFieldEditor>, IEnumerable<RadFilterDataFieldEditor>, IEnumerable, IStateManager
	{
		// Token: 0x0600F365 RID: 62309 RVA: 0x00376098 File Offset: 0x00374298
		public RadFilterDataFieldEditorCollection(RadFilter owner)
		{
			this._editors = new List<RadFilterDataFieldEditor>();
			this._owner = owner;
			this._notTrackedEditorFields = new List<string>();
		}

		// Token: 0x0600F366 RID: 62310 RVA: 0x003760BD File Offset: 0x003742BD
		internal RadFilterDataFieldEditorCollection() : this(null)
		{
		}

		// Token: 0x0600F367 RID: 62311 RVA: 0x003760C8 File Offset: 0x003742C8
		internal RadFilterDataFieldEditor RetrieveEditorForFieldName(string fieldName)
		{
			RadFilterDataFieldEditor baseEditor = this.FindEditorForFieldName(fieldName);
			return RadFilterDataFieldEditor.CreateEditorFrom(baseEditor);
		}

		// Token: 0x0600F368 RID: 62312 RVA: 0x003760E4 File Offset: 0x003742E4
		internal Type RetrieveTypeForEditor(string fieldName)
		{
			RadFilterDataFieldEditor radFilterDataFieldEditor = this.FindEditorForFieldName(fieldName);
			return radFilterDataFieldEditor.DataType;
		}

		// Token: 0x0600F369 RID: 62313 RVA: 0x0037611C File Offset: 0x0037431C
		internal RadFilterDataFieldEditor FindEditorForFieldName(string fieldName)
		{
			if (string.IsNullOrEmpty(fieldName))
			{
				throw new ArgumentException("Parameter cannot be null or empty.", "fieldName");
			}
			return this._editors.Find((RadFilterDataFieldEditor editor) => editor.FieldName == fieldName);
		}

		// Token: 0x17004960 RID: 18784
		public RadFilterDataFieldEditor this[int index]
		{
			get
			{
				return ((IList<RadFilterDataFieldEditor>)this)[index];
			}
		}

		// Token: 0x0600F36B RID: 62315 RVA: 0x00376174 File Offset: 0x00374374
		private void InsertInternal(int index, RadFilterDataFieldEditor item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			item.SetOwner(this._owner);
			if (this._isTrackingViewState)
			{
				((IStateManager)item).TrackViewState();
			}
			if (index < 0 || index > this.Count)
			{
				this._editors.Add(item);
				return;
			}
			this._editors.Insert(index, item);
		}

		// Token: 0x0600F36C RID: 62316 RVA: 0x003761D0 File Offset: 0x003743D0
		private bool RemoveInternal(int index, RadFilterDataFieldEditor item)
		{
			bool result;
			if (index < 0)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item", "Value cannot be null.");
				}
				result = this._editors.Remove(item);
			}
			else
			{
				this._editors.RemoveAt(index);
				result = true;
			}
			return result;
		}

		// Token: 0x0600F36D RID: 62317 RVA: 0x00376214 File Offset: 0x00374414
		public void Add(RadFilterDataFieldEditor item)
		{
			this.InsertInternal(-1, item);
		}

		// Token: 0x0600F36E RID: 62318 RVA: 0x0037621E File Offset: 0x0037441E
		public void Clear()
		{
			this._editors.Clear();
		}

		// Token: 0x0600F36F RID: 62319 RVA: 0x0037622B File Offset: 0x0037442B
		public bool Contains(RadFilterDataFieldEditor item)
		{
			return this._editors.Contains(item);
		}

		// Token: 0x0600F370 RID: 62320 RVA: 0x00376239 File Offset: 0x00374439
		public void CopyTo(RadFilterDataFieldEditor[] array, int arrayIndex)
		{
			this._editors.CopyTo(array, arrayIndex);
		}

		// Token: 0x17004961 RID: 18785
		// (get) Token: 0x0600F371 RID: 62321 RVA: 0x00376248 File Offset: 0x00374448
		public int Count
		{
			get
			{
				return this._editors.Count;
			}
		}

		// Token: 0x17004962 RID: 18786
		// (get) Token: 0x0600F372 RID: 62322 RVA: 0x00376255 File Offset: 0x00374455
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600F373 RID: 62323 RVA: 0x00376258 File Offset: 0x00374458
		public bool Remove(RadFilterDataFieldEditor item)
		{
			return this.RemoveInternal(-1, item);
		}

		// Token: 0x0600F374 RID: 62324 RVA: 0x00376262 File Offset: 0x00374462
		public IEnumerator<RadFilterDataFieldEditor> GetEnumerator()
		{
			return this._editors.GetEnumerator();
		}

		// Token: 0x0600F375 RID: 62325 RVA: 0x00376274 File Offset: 0x00374474
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600F376 RID: 62326 RVA: 0x0037627C File Offset: 0x0037447C
		public int IndexOf(RadFilterDataFieldEditor item)
		{
			return this._editors.IndexOf(item);
		}

		// Token: 0x0600F377 RID: 62327 RVA: 0x0037628A File Offset: 0x0037448A
		public void Insert(int index, RadFilterDataFieldEditor item)
		{
			this.InsertInternal(index, item);
		}

		// Token: 0x0600F378 RID: 62328 RVA: 0x00376294 File Offset: 0x00374494
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x17004963 RID: 18787
		RadFilterDataFieldEditor IList<RadFilterDataFieldEditor>.this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this._editors.Count == 0)
				{
					throw new NullReferenceException("FieldEditors collection is empty.");
				}
				return this._editors[index];
			}
			set
			{
				this._editors[index] = value;
			}
		}

		// Token: 0x0600F37B RID: 62331 RVA: 0x003762E0 File Offset: 0x003744E0
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17004964 RID: 18788
		// (get) Token: 0x0600F37C RID: 62332 RVA: 0x00376310 File Offset: 0x00374510
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17004965 RID: 18789
		// (get) Token: 0x0600F37D RID: 62333 RVA: 0x00376318 File Offset: 0x00374518
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004966 RID: 18790
		// (get) Token: 0x0600F37E RID: 62334 RVA: 0x0037631B File Offset: 0x0037451B
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600F37F RID: 62335 RVA: 0x0037631E File Offset: 0x0037451E
		int IList.Add(object value)
		{
			this.InsertInternal(-1, (RadFilterDataFieldEditor)value);
			return this.Count - 1;
		}

		// Token: 0x0600F380 RID: 62336 RVA: 0x00376335 File Offset: 0x00374535
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600F381 RID: 62337 RVA: 0x0037633D File Offset: 0x0037453D
		bool IList.Contains(object value)
		{
			return this.Contains((RadFilterDataFieldEditor)value);
		}

		// Token: 0x0600F382 RID: 62338 RVA: 0x0037634B File Offset: 0x0037454B
		int IList.IndexOf(object value)
		{
			return this.IndexOf((RadFilterDataFieldEditor)value);
		}

		// Token: 0x0600F383 RID: 62339 RVA: 0x00376359 File Offset: 0x00374559
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (RadFilterDataFieldEditor)value);
		}

		// Token: 0x17004967 RID: 18791
		// (get) Token: 0x0600F384 RID: 62340 RVA: 0x00376368 File Offset: 0x00374568
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004968 RID: 18792
		// (get) Token: 0x0600F385 RID: 62341 RVA: 0x0037636B File Offset: 0x0037456B
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x0600F386 RID: 62342 RVA: 0x00376373 File Offset: 0x00374573
		void IList.Remove(object value)
		{
			this.Remove((RadFilterDataFieldEditor)value);
		}

		// Token: 0x0600F387 RID: 62343 RVA: 0x00376382 File Offset: 0x00374582
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17004969 RID: 18793
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				((IList<RadFilterDataFieldEditor>)this)[index] = (RadFilterDataFieldEditor)value;
			}
		}

		// Token: 0x1700496A RID: 18794
		// (get) Token: 0x0600F38A RID: 62346 RVA: 0x003763A3 File Offset: 0x003745A3
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x0600F38B RID: 62347 RVA: 0x003763AC File Offset: 0x003745AC
		void IStateManager.LoadViewState(object state)
		{
			object[] array = state as object[];
			if (array != null && array.Length > 0)
			{
				int num = (int)((Pair)array[0]).First;
				List<string> list = (List<string>)((Pair)array[0]).Second;
				int num2 = 0;
				while (num2 < num && num2 < array.Length)
				{
					Triplet triplet = array[num2 + 1] as Triplet;
					if (triplet != null)
					{
						string text = triplet.First as string;
						string text2 = triplet.Second as string;
						object third = triplet.Third;
						if (!string.IsNullOrEmpty(text2) && list.Contains(text2))
						{
							RadFilterDataFieldEditor radFilterDataFieldEditor = this.FindEditorForFieldName(text2);
							if (radFilterDataFieldEditor != null)
							{
								((IStateManager)radFilterDataFieldEditor).LoadViewState(third);
							}
						}
						else if (string.IsNullOrEmpty(text2) && num2 < this.Count && this[num2].GetType().Name == text)
						{
							((IStateManager)this[num2]).LoadViewState(third);
						}
						else if (!string.IsNullOrEmpty(text))
						{
							RadFilterDataFieldEditor radFilterDataFieldEditor2 = this.CreateEditorFromTypeName(text);
							if (radFilterDataFieldEditor2 != null)
							{
								this.Insert(num2, radFilterDataFieldEditor2);
								((IStateManager)radFilterDataFieldEditor2).LoadViewState(third);
							}
						}
					}
					num2++;
				}
			}
		}

		// Token: 0x0600F38C RID: 62348 RVA: 0x003764D6 File Offset: 0x003746D6
		protected virtual RadFilterDataFieldEditor CreateEditorFromTypeName(string typeInfo)
		{
			return RadFilterDataFieldEditor.CreateEditorFromTypeName(typeInfo, this._owner);
		}

		// Token: 0x0600F38D RID: 62349 RVA: 0x003764F4 File Offset: 0x003746F4
		object IStateManager.SaveViewState()
		{
			List<RadFilterDataFieldEditor> list = (from e in this
			where ((IStateManager)e).SaveViewState() != null
			select e).ToList<RadFilterDataFieldEditor>();
			if (list.Count == 0)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(list.Count, this._notTrackedEditorFields));
			foreach (RadFilterDataFieldEditor radFilterDataFieldEditor in list)
			{
				arrayList.Add(new Triplet
				{
					First = radFilterDataFieldEditor.GetType().Name,
					Second = radFilterDataFieldEditor.FieldName,
					Third = ((IStateManager)radFilterDataFieldEditor).SaveViewState()
				});
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600F38E RID: 62350 RVA: 0x003765F5 File Offset: 0x003747F5
		void IStateManager.TrackViewState()
		{
			if (this._isMarked)
			{
				return;
			}
			this._isMarked = true;
			this._isTrackingViewState = true;
			this._editors.ForEach(delegate(RadFilterDataFieldEditor editor)
			{
				((IStateManager)editor).TrackViewState();
				this._notTrackedEditorFields.Add(editor.FieldName);
			});
		}

		// Token: 0x040045D3 RID: 17875
		private RadFilter _owner;

		// Token: 0x040045D4 RID: 17876
		private List<RadFilterDataFieldEditor> _editors;

		// Token: 0x040045D5 RID: 17877
		private bool _isTrackingViewState;

		// Token: 0x040045D6 RID: 17878
		private bool _isMarked;

		// Token: 0x040045D7 RID: 17879
		private List<string> _notTrackedEditorFields;
	}
}
