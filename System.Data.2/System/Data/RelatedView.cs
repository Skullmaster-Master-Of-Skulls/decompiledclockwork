using System;

namespace System.Data
{
	// Token: 0x02000120 RID: 288
	internal sealed class RelatedView : DataView, IFilter
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x000858B4 File Offset: 0x00084CB4
		public RelatedView(DataColumn[] columns, object[] values) : base(columns[0].Table, false)
		{
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			this.parentRowView = null;
			this.parentKey = null;
			this.childKey = new DataKey(columns, true);
			this.filterValues = values;
			base.ResetRowViewCache();
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0008590C File Offset: 0x00084D0C
		public RelatedView(DataRowView parentRowView, DataKey parentKey, DataColumn[] childKeyColumns) : base(childKeyColumns[0].Table, false)
		{
			this.filterValues = null;
			this.parentRowView = parentRowView;
			this.parentKey = new DataKey?(parentKey);
			this.childKey = new DataKey(childKeyColumns, true);
			base.ResetRowViewCache();
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00085958 File Offset: 0x00084D58
		private object[] GetParentValues()
		{
			if (this.filterValues != null)
			{
				return this.filterValues;
			}
			if (!this.parentRowView.HasRecord())
			{
				return null;
			}
			return this.parentKey.Value.GetKeyValues(this.parentRowView.GetRecord());
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x000859A4 File Offset: 0x00084DA4
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			object[] parentValues = this.GetParentValues();
			if (parentValues == null)
			{
				return false;
			}
			object[] keyValues = row.GetKeyValues(this.childKey, version);
			bool flag = true;
			if (keyValues.Length != parentValues.Length)
			{
				flag = false;
			}
			else
			{
				for (int i = 0; i < keyValues.Length; i++)
				{
					if (!keyValues[i].Equals(parentValues[i]))
					{
						flag = false;
						break;
					}
				}
			}
			IFilter filter = base.GetFilter();
			if (filter != null)
			{
				flag &= filter.Invoke(row, version);
			}
			return flag;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00085A14 File Offset: 0x00084E14
		internal override IFilter GetFilter()
		{
			return this;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00085A24 File Offset: 0x00084E24
		public override DataRowView AddNew()
		{
			DataRowView dataRowView = base.AddNew();
			dataRowView.Row.SetKeyValues(this.childKey, this.GetParentValues());
			return dataRowView;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00085A50 File Offset: 0x00084E50
		internal override void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			base.SetIndex2(newSort, newRowStates, newRowFilter, false);
			base.Reset();
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00085A70 File Offset: 0x00084E70
		public override bool Equals(DataView dv)
		{
			RelatedView relatedView = dv as RelatedView;
			if (relatedView == null)
			{
				return false;
			}
			if (!base.Equals(dv))
			{
				return false;
			}
			object[] columnsReference;
			if (this.filterValues != null)
			{
				columnsReference = this.childKey.ColumnsReference;
				object[] value = columnsReference;
				columnsReference = relatedView.childKey.ColumnsReference;
				return this.CompareArray(value, columnsReference) && this.CompareArray(this.filterValues, relatedView.filterValues);
			}
			if (relatedView.filterValues != null)
			{
				return false;
			}
			columnsReference = this.childKey.ColumnsReference;
			object[] value2 = columnsReference;
			columnsReference = relatedView.childKey.ColumnsReference;
			if (this.CompareArray(value2, columnsReference))
			{
				columnsReference = this.parentKey.Value.ColumnsReference;
				object[] value3 = columnsReference;
				columnsReference = this.parentKey.Value.ColumnsReference;
				if (this.CompareArray(value3, columnsReference))
				{
					return this.parentRowView.Equals(relatedView.parentRowView);
				}
			}
			return false;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00085B58 File Offset: 0x00084F58
		private bool CompareArray(object[] value1, object[] value2)
		{
			if (value1 == null || value2 == null)
			{
				return value1 == value2;
			}
			if (value1.Length != value2.Length)
			{
				return false;
			}
			for (int i = 0; i < value1.Length; i++)
			{
				if (value1[i] != value2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040005CC RID: 1484
		private readonly DataKey? parentKey;

		// Token: 0x040005CD RID: 1485
		private readonly DataKey childKey;

		// Token: 0x040005CE RID: 1486
		private readonly DataRowView parentRowView;

		// Token: 0x040005CF RID: 1487
		private readonly object[] filterValues;
	}
}
