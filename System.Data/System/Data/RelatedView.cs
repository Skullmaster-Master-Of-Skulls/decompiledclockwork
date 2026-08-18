using System;

namespace System.Data
{
	// Token: 0x020000D5 RID: 213
	internal sealed class RelatedView : DataView, IFilter
	{
		// Token: 0x06000CEC RID: 3308 RVA: 0x00212A28 File Offset: 0x00211E28
		public RelatedView(DataColumn[] columns, object[] values) : base(columns[0].Table, false)
		{
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			this.key = new DataKey(columns, true);
			this.values = values;
			base.ResetRowViewCache();
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00212A78 File Offset: 0x00211E78
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			object[] keyValues = row.GetKeyValues(this.key, version);
			bool flag = true;
			if (keyValues.Length != this.values.Length)
			{
				flag = false;
			}
			else
			{
				for (int i = 0; i < keyValues.Length; i++)
				{
					if (!keyValues[i].Equals(this.values[i]))
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

		// Token: 0x06000CEE RID: 3310 RVA: 0x00212AE8 File Offset: 0x00211EE8
		internal override IFilter GetFilter()
		{
			return this;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00212AF8 File Offset: 0x00211EF8
		public override DataRowView AddNew()
		{
			DataRowView dataRowView = base.AddNew();
			dataRowView.Row.SetKeyValues(this.key, this.values);
			return dataRowView;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00212B28 File Offset: 0x00211F28
		internal override void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			base.SetIndex2(newSort, newRowStates, newRowFilter, false);
			base.Reset();
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00212B48 File Offset: 0x00211F48
		public override bool Equals(DataView dv)
		{
			return dv is RelatedView && base.Equals(dv) && (this.CompareArray(this.key.ColumnsReference, ((RelatedView)dv).key.ColumnsReference) || this.CompareArray(this.values, ((RelatedView)dv).values));
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00212BB8 File Offset: 0x00211FB8
		private bool CompareArray(object[] value1, object[] value2)
		{
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

		// Token: 0x040008F1 RID: 2289
		private readonly DataKey key;

		// Token: 0x040008F2 RID: 2290
		private object[] values;
	}
}
