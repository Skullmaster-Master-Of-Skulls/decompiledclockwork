using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA4 RID: 3492
	[PersistChildren(false)]
	public class PivotGridFieldsCollection : IList, ICollection, IList<PivotGridField>, ICollection<PivotGridField>, IEnumerable<PivotGridField>, IEnumerable, IStateManager
	{
		// Token: 0x1700293B RID: 10555
		// (get) Token: 0x06008257 RID: 33367 RVA: 0x001DB70F File Offset: 0x001D990F
		// (set) Token: 0x06008258 RID: 33368 RVA: 0x001DB717 File Offset: 0x001D9917
		public RadPivotGrid Owner { get; internal set; }

		// Token: 0x06008259 RID: 33369 RVA: 0x001DB720 File Offset: 0x001D9920
		public PivotGridFieldsCollection(RadPivotGrid owner)
		{
			this.fields = new List<PivotGridField>();
			this.Owner = owner;
		}

		// Token: 0x0600825A RID: 33370 RVA: 0x001DB73A File Offset: 0x001D993A
		internal PivotGridFieldsCollection() : this(null)
		{
		}

		// Token: 0x1700293C RID: 10556
		public PivotGridField this[int index]
		{
			get
			{
				return ((IList<PivotGridField>)this)[index];
			}
		}

		// Token: 0x1700293D RID: 10557
		public PivotGridField this[string fieldUniqueName]
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i].UniqueName.Equals(fieldUniqueName))
					{
						return this[i];
					}
				}
				return null;
			}
		}

		// Token: 0x0600825D RID: 33373 RVA: 0x001DB788 File Offset: 0x001D9988
		private void InsertInternal(int index, PivotGridField item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			item.SetOwner(this.Owner);
			if (this.isTrackingViewState)
			{
				((IStateManager)item).TrackViewState();
			}
			if (index < 0)
			{
				this.fields.Add(item);
				return;
			}
			this.fields.Insert(index, item);
		}

		// Token: 0x0600825E RID: 33374 RVA: 0x001DB7DC File Offset: 0x001D99DC
		private bool RemoveInternal(int index, PivotGridField item)
		{
			bool result;
			if (index < 0)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item", "Value cannot be null.");
				}
				result = this.fields.Remove(item);
			}
			else
			{
				this.fields.RemoveAt(index);
				result = true;
			}
			return result;
		}

		// Token: 0x0600825F RID: 33375 RVA: 0x001DB820 File Offset: 0x001D9A20
		public int IndexOf(PivotGridField item)
		{
			return this.fields.IndexOf(item);
		}

		// Token: 0x06008260 RID: 33376 RVA: 0x001DB82E File Offset: 0x001D9A2E
		public void Insert(int index, PivotGridField item)
		{
			this.InsertInternal(index, item);
		}

		// Token: 0x06008261 RID: 33377 RVA: 0x001DB838 File Offset: 0x001D9A38
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x1700293E RID: 10558
		PivotGridField IList<PivotGridField>.this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.fields.Count == 0)
				{
					throw new NullReferenceException("Fields collection is empty.");
				}
				return this.fields[index];
			}
			set
			{
				this.fields[index] = value;
			}
		}

		// Token: 0x06008264 RID: 33380 RVA: 0x001DB882 File Offset: 0x001D9A82
		public void Add(PivotGridField item)
		{
			this.InsertInternal(-1, item);
		}

		// Token: 0x06008265 RID: 33381 RVA: 0x001DB88C File Offset: 0x001D9A8C
		public void Clear()
		{
			this.fields.Clear();
		}

		// Token: 0x06008266 RID: 33382 RVA: 0x001DB899 File Offset: 0x001D9A99
		public bool Contains(PivotGridField item)
		{
			return this.fields.Contains(item);
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x001DB8A7 File Offset: 0x001D9AA7
		public void CopyTo(PivotGridField[] array, int arrayIndex)
		{
			this.fields.CopyTo(array, arrayIndex);
		}

		// Token: 0x1700293F RID: 10559
		// (get) Token: 0x06008268 RID: 33384 RVA: 0x001DB8B6 File Offset: 0x001D9AB6
		public int Count
		{
			get
			{
				return this.fields.Count;
			}
		}

		// Token: 0x17002940 RID: 10560
		// (get) Token: 0x06008269 RID: 33385 RVA: 0x001DB8C3 File Offset: 0x001D9AC3
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600826A RID: 33386 RVA: 0x001DB8C6 File Offset: 0x001D9AC6
		public bool Remove(PivotGridField item)
		{
			return this.RemoveInternal(-1, item);
		}

		// Token: 0x0600826B RID: 33387 RVA: 0x001DB8D0 File Offset: 0x001D9AD0
		public IEnumerator<PivotGridField> GetEnumerator()
		{
			return this.fields.GetEnumerator();
		}

		// Token: 0x0600826C RID: 33388 RVA: 0x001DB8E2 File Offset: 0x001D9AE2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600826D RID: 33389 RVA: 0x001DB8EA File Offset: 0x001D9AEA
		int IList.Add(object value)
		{
			this.InsertInternal(-1, (PivotGridField)value);
			return this.Count - 1;
		}

		// Token: 0x0600826E RID: 33390 RVA: 0x001DB901 File Offset: 0x001D9B01
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600826F RID: 33391 RVA: 0x001DB909 File Offset: 0x001D9B09
		bool IList.Contains(object value)
		{
			return this.Contains((PivotGridField)value);
		}

		// Token: 0x06008270 RID: 33392 RVA: 0x001DB917 File Offset: 0x001D9B17
		int IList.IndexOf(object value)
		{
			return this.IndexOf((PivotGridField)value);
		}

		// Token: 0x06008271 RID: 33393 RVA: 0x001DB925 File Offset: 0x001D9B25
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (PivotGridField)value);
		}

		// Token: 0x17002941 RID: 10561
		// (get) Token: 0x06008272 RID: 33394 RVA: 0x001DB934 File Offset: 0x001D9B34
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002942 RID: 10562
		// (get) Token: 0x06008273 RID: 33395 RVA: 0x001DB937 File Offset: 0x001D9B37
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06008274 RID: 33396 RVA: 0x001DB93F File Offset: 0x001D9B3F
		void IList.Remove(object value)
		{
			this.Remove((PivotGridField)value);
		}

		// Token: 0x06008275 RID: 33397 RVA: 0x001DB94E File Offset: 0x001D9B4E
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17002943 RID: 10563
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				((IList<PivotGridField>)this)[index] = (PivotGridField)value;
			}
		}

		// Token: 0x06008278 RID: 33400 RVA: 0x001DB970 File Offset: 0x001D9B70
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x17002944 RID: 10564
		// (get) Token: 0x06008279 RID: 33401 RVA: 0x001DB9A0 File Offset: 0x001D9BA0
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17002945 RID: 10565
		// (get) Token: 0x0600827A RID: 33402 RVA: 0x001DB9A8 File Offset: 0x001D9BA8
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002946 RID: 10566
		// (get) Token: 0x0600827B RID: 33403 RVA: 0x001DB9AB File Offset: 0x001D9BAB
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002947 RID: 10567
		// (get) Token: 0x0600827C RID: 33404 RVA: 0x001DB9AE File Offset: 0x001D9BAE
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		// Token: 0x0600827D RID: 33405 RVA: 0x001DB9B8 File Offset: 0x001D9BB8
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
						string text = (string)pair.First;
						PivotGridField pivotGridField;
						if (this[num3].FieldType != text)
						{
							pivotGridField = this.Owner.CreateFieldByType(text);
							pivotGridField.CopyBaseProperties(this[num3]);
							this.RemoveAt(num3);
							this.Insert(num3, pivotGridField);
						}
						else
						{
							pivotGridField = this[num3];
						}
						((IStateManager)pivotGridField).LoadViewState(pair.Second);
					}
					num3++;
				}
				int num4 = num2;
				while (num4 < num && num4 < array.Length)
				{
					Pair pair2 = array[num4 + 1] as Pair;
					if (pair2 != null)
					{
						object first = pair2.First;
						if (first != null)
						{
							PivotGridField pivotGridField2 = this.Owner.CreateFieldByType((string)first);
							if (pivotGridField2 != null)
							{
								this.Add(pivotGridField2);
								((IStateManager)pivotGridField2).LoadViewState(pair2.Second);
							}
						}
					}
					num4++;
				}
			}
		}

		// Token: 0x0600827E RID: 33406 RVA: 0x001DBAF8 File Offset: 0x001D9CF8
		object IStateManager.SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(new Pair(this.Count, this._notTrackedColumnsCount));
			bool flag = false;
			foreach (PivotGridField pivotGridField in this)
			{
				arrayList.Add(new Pair
				{
					First = pivotGridField.FieldType,
					Second = ((IStateManager)pivotGridField).SaveViewState()
				});
				flag = true;
			}
			if (!flag)
			{
				return null;
			}
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600827F RID: 33407 RVA: 0x001DBBB0 File Offset: 0x001D9DB0
		void IStateManager.TrackViewState()
		{
			if (this.isMarked)
			{
				return;
			}
			this.isMarked = true;
			this._notTrackedColumnsCount = this.Count;
			this.isTrackingViewState = true;
			this.fields.ForEach(delegate(PivotGridField item)
			{
				((IStateManager)item).TrackViewState();
			});
		}

		// Token: 0x06008280 RID: 33408 RVA: 0x001DBC08 File Offset: 0x001D9E08
		public void ClearRenderControls()
		{
			foreach (PivotGridField pivotGridField in this.fields)
			{
				pivotGridField.ClearRenderControl();
			}
		}

		// Token: 0x06008281 RID: 33409 RVA: 0x001DBC7A File Offset: 0x001D9E7A
		public void ClearGroupDescriptors()
		{
			this.fields.ForEach(delegate(PivotGridField f)
			{
				PivotGridGroupField pivotGridGroupField = f as PivotGridGroupField;
				if (pivotGridGroupField != null)
				{
					pivotGridGroupField.GroupDescription = null;
				}
			});
		}

		// Token: 0x06008282 RID: 33410 RVA: 0x001DBCC2 File Offset: 0x001D9EC2
		public void ClearAggregateDescriptors()
		{
			this.fields.ForEach(delegate(PivotGridField f)
			{
				PivotGridAggregateField pivotGridAggregateField = f as PivotGridAggregateField;
				if (pivotGridAggregateField != null)
				{
					pivotGridAggregateField.GroupDescription = null;
				}
			});
		}

		// Token: 0x06008283 RID: 33411 RVA: 0x001DBD08 File Offset: 0x001D9F08
		public List<PivotGridField> GetFieldsByType(string fieldType)
		{
			return (from f in this.fields
			where f.FieldType == fieldType
			select f).ToList<PivotGridField>();
		}

		// Token: 0x06008284 RID: 33412 RVA: 0x001DBD5C File Offset: 0x001D9F5C
		public PivotGridField GetFieldByUniqueName(string uniqueName)
		{
			return this.fields.SingleOrDefault((PivotGridField f) => f.UniqueName == uniqueName);
		}

		// Token: 0x06008285 RID: 33413 RVA: 0x001DBDA8 File Offset: 0x001D9FA8
		public PivotGridField GetFieldByUniqueNameOutChildIndex(string uniqueName, out int childIndex)
		{
			childIndex = -1;
			PivotGridField pivotGridField = this.fields.SingleOrDefault((PivotGridField f) => uniqueName.Contains(f.UniqueName));
			for (int i = 0; i < pivotGridField.FlatChildOlapInfoNames.Count; i++)
			{
				if (pivotGridField.FlatChildOlapInfoNames[i].Replace(" ", string.Empty) == uniqueName)
				{
					childIndex = i;
					break;
				}
			}
			return pivotGridField;
		}

		// Token: 0x06008286 RID: 33414 RVA: 0x001DBE29 File Offset: 0x001DA029
		public void EnsureUniqueNames()
		{
			this.fields.ForEach(delegate(PivotGridField field)
			{
				field.EnsureUniqueName();
			});
		}

		// Token: 0x06008287 RID: 33415 RVA: 0x001DBE5B File Offset: 0x001DA05B
		public void EnsureGroupDescriptions()
		{
			this.fields.ForEach(delegate(PivotGridField field)
			{
				field.SetDescriptionInfo();
			});
		}

		// Token: 0x06008288 RID: 33416 RVA: 0x001DBE88 File Offset: 0x001DA088
		public void RemoveGroupDescriptionsParent()
		{
			foreach (PivotGridField pivotGridField in this.fields)
			{
				PivotGridGroupField pivotGridGroupField = pivotGridField as PivotGridGroupField;
				PivotGridAggregateField pivotGridAggregateField = pivotGridField as PivotGridAggregateField;
				PivotGridReportFilterField pivotGridReportFilterField = pivotGridField as PivotGridReportFilterField;
				if (pivotGridGroupField != null)
				{
					if (pivotGridGroupField.GroupDescription != null)
					{
						pivotGridGroupField.GroupDescription.Parent = null;
					}
				}
				else if (pivotGridAggregateField != null)
				{
					if (pivotGridAggregateField.GroupDescription != null)
					{
						pivotGridAggregateField.GroupDescription.Parent = null;
					}
				}
				else if (pivotGridReportFilterField != null && pivotGridReportFilterField.FilterDescription != null)
				{
					pivotGridReportFilterField.FilterDescription.Parent = null;
				}
			}
		}

		// Token: 0x06008289 RID: 33417 RVA: 0x001DBF7C File Offset: 0x001DA17C
		internal void EnsureZoneIndexes()
		{
			using (List<PivotGridField>.Enumerator enumerator = this.fields.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PivotGridField field = enumerator.Current;
					IEnumerable<PivotGridField> enumerable = from f in this.fields
					where f.ZoneType == field.ZoneType && f.ZoneIndex == field.ZoneIndex && f.UniqueName != field.UniqueName
					select f;
					int num = 1;
					foreach (PivotGridField pivotGridField in enumerable)
					{
						pivotGridField.ZoneIndex += num;
						num++;
					}
				}
			}
		}

		// Token: 0x0600828A RID: 33418 RVA: 0x001DC040 File Offset: 0x001DA240
		internal PivotGridField AddField(string dataField, string uniqueName)
		{
			PivotGridField pivotGridField = new PivotGridRowField();
			this.Add(pivotGridField);
			pivotGridField.IsHidden = true;
			pivotGridField.DataField = dataField;
			pivotGridField.UniqueName = uniqueName;
			PivotGridFieldCreatedEventArgs e = new PivotGridFieldCreatedEventArgs(pivotGridField);
			this.Owner.FireFieldCreated(e);
			return pivotGridField;
		}

		// Token: 0x0600828B RID: 33419 RVA: 0x001DC23C File Offset: 0x001DA43C
		internal void AddMissingHiddenFieldsFromDataSource()
		{
			Dictionary<string, PivotGridField> dataFields = new Dictionary<string, PivotGridField>();
			foreach (PivotGridField pivotGridField in this)
			{
				if (!dataFields.ContainsKey(pivotGridField.DataField))
				{
					dataFields.Add(pivotGridField.DataField, pivotGridField);
				}
			}
			int zoneIndex = 0;
			IEnumerable<PivotGridField> source = from f in this
			where f.ZoneType == PivotGridFieldZoneType.Row
			select f;
			if (source.Count<PivotGridField>() > 0)
			{
				zoneIndex = source.Max((PivotGridField f) => f.ZoneIndex) + 1;
			}
			this.LoopPivotEngineFieldInfos(delegate(FieldInfoNode fieldInfoNode)
			{
				PivotGridField pivotGridField2 = null;
				string text = Regex.Replace(fieldInfoNode.FieldInfo.Name, "\\s", string.Empty);
				if (dataFields.ContainsKey(fieldInfoNode.FieldInfo.Name))
				{
					pivotGridField2 = dataFields[fieldInfoNode.FieldInfo.Name];
					if (string.IsNullOrEmpty(pivotGridField2.Caption))
					{
						pivotGridField2.Caption = fieldInfoNode.Caption;
					}
				}
				else if (!this.Owner.IsBoundToOlap)
				{
					pivotGridField2 = this.AddField(fieldInfoNode, zoneIndex++, dataFields);
				}
				else if (text != fieldInfoNode.FieldInfo.Name && dataFields.ContainsKey(text))
				{
					pivotGridField2 = dataFields[text];
					if (string.IsNullOrEmpty(pivotGridField2.Caption))
					{
						pivotGridField2.Caption = fieldInfoNode.Caption;
					}
					pivotGridField2.DataField = fieldInfoNode.FieldInfo.Name;
					if (this.Owner.PromissedFieldsForCreation.Contains(text))
					{
						this.Owner.PromissedFieldsForCreation.Remove(text);
					}
				}
				else if (this.Owner.PromissedFieldsForCreation.Contains(text))
				{
					pivotGridField2 = this.AddField(fieldInfoNode, zoneIndex++, null);
					pivotGridField2.Show();
					this.Owner.PromissedFieldsForCreation.Remove(text);
				}
				if (pivotGridField2 != null)
				{
					pivotGridField2.FieldInfoNode = fieldInfoNode;
				}
			});
			this.EnsureZoneIndexes();
		}

		// Token: 0x0600828C RID: 33420 RVA: 0x001DC334 File Offset: 0x001DA534
		private void LoopPivotEngineFieldInfos(Action<FieldInfoNode> callback)
		{
			IDataProvider provider = this.Owner.provider;
			if (provider == null)
			{
				return;
			}
			IFieldInfoData fieldInfos = provider.FieldInfos;
			if (fieldInfos != null)
			{
				this.GetFieldsInfoData(fieldInfos, callback);
			}
		}

		// Token: 0x0600828D RID: 33421 RVA: 0x001DC364 File Offset: 0x001DA564
		private void GetFieldsInfoData(IFieldInfoData fieldInfoData, Action<FieldInfoNode> callback)
		{
			ContainerNode rootFieldInfo = fieldInfoData.RootFieldInfo;
			this.AddFields(rootFieldInfo, rootFieldInfo, callback);
			this.EnsureUniqueNames();
			this.EnsureZoneIndexes();
		}

		// Token: 0x0600828E RID: 33422 RVA: 0x001DC390 File Offset: 0x001DA590
		private void AddFields(ContainerNode node, ContainerNode parentNode, Action<FieldInfoNode> callback)
		{
			foreach (ContainerNode containerNode in node.Children)
			{
				FieldInfoNode fieldInfoNode = containerNode as FieldInfoNode;
				if (fieldInfoNode != null)
				{
					callback(fieldInfoNode);
				}
				this.AddFields(containerNode, node, callback);
			}
		}

		// Token: 0x0600828F RID: 33423 RVA: 0x001DC3F0 File Offset: 0x001DA5F0
		private PivotGridField AddField(FieldInfoNode fieldInfoNode, int zoneIndex, Dictionary<string, PivotGridField> dataFields = null)
		{
			PivotGridField pivotGridField = this.CreateFieldFromFieldRoles(fieldInfoNode.FieldInfo.PreferredRole);
			PivotGridGroupField pivotGridGroupField = pivotGridField as PivotGridGroupField;
			bool flag = false;
			string text = string.Empty;
			PivotGridGroupInterval groupInterval = PivotGridGroupInterval.Default;
			if (pivotGridGroupField != null && dataFields != null && fieldInfoNode.FieldInfo.DataType == typeof(DateTime))
			{
				string[] array = fieldInfoNode.FieldInfo.Name.Split(new char[]
				{
					'.'
				});
				if (array.Length == 2)
				{
					try
					{
						groupInterval = (PivotGridGroupInterval)Enum.Parse(typeof(PivotGridGroupInterval), array[1], true);
						flag = true;
						text = array[0];
						if (dataFields.ContainsKey(text))
						{
							pivotGridField = dataFields[text];
							if (string.IsNullOrEmpty(pivotGridField.Caption))
							{
								pivotGridField.Caption = fieldInfoNode.Caption;
							}
							return pivotGridField;
						}
					}
					catch (Exception)
					{
						flag = false;
					}
				}
			}
			this.Add(pivotGridField);
			pivotGridField.IsHidden = true;
			if (flag)
			{
				pivotGridGroupField.DataField = text;
				pivotGridGroupField.GroupInterval = groupInterval;
			}
			else
			{
				pivotGridField.DataField = fieldInfoNode.FieldInfo.Name;
			}
			pivotGridField.Caption = fieldInfoNode.Caption;
			pivotGridField.ZoneIndex = zoneIndex;
			PivotGridFieldCreatedEventArgs e = new PivotGridFieldCreatedEventArgs(pivotGridField);
			this.Owner.FireFieldCreated(e);
			return pivotGridField;
		}

		// Token: 0x06008290 RID: 33424 RVA: 0x001DC53C File Offset: 0x001DA73C
		private PivotGridField CreateFieldFromFieldRoles(FieldRoles fieldRoles)
		{
			PivotGridField result = null;
			if (fieldRoles == FieldRoles.All || fieldRoles == FieldRoles.None || fieldRoles == FieldRoles.Row)
			{
				result = new PivotGridRowField();
			}
			else if (fieldRoles == FieldRoles.Column)
			{
				result = new PivotGridColumnField();
			}
			else if (fieldRoles == FieldRoles.Filter)
			{
				result = new PivotGridReportFilterField();
			}
			else if (fieldRoles == FieldRoles.Value)
			{
				result = new PivotGridAggregateField();
			}
			return result;
		}

		// Token: 0x040023F0 RID: 9200
		private List<PivotGridField> fields;

		// Token: 0x040023F1 RID: 9201
		private bool isTrackingViewState;

		// Token: 0x040023F2 RID: 9202
		private int _notTrackedColumnsCount;

		// Token: 0x040023F3 RID: 9203
		private bool isMarked;
	}
}
