using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x020002F5 RID: 757
	public abstract class DbParameterCollection : MarshalByRefObject, IDataParameterCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x0600304D RID: 12365
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract int Count { get; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x0012E3C4 File Offset: 0x0012D7C4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x0012E3D4 File Offset: 0x0012D7D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x0012E3E4 File Offset: 0x0012D7E4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06003051 RID: 12369
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract object SyncRoot { get; }

		// Token: 0x170007E3 RID: 2019
		object IList.this[int index]
		{
			get
			{
				return this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, (DbParameter)value);
			}
		}

		// Token: 0x170007E4 RID: 2020
		object IDataParameterCollection.this[string parameterName]
		{
			get
			{
				return this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, (DbParameter)value);
			}
		}

		// Token: 0x170007E5 RID: 2021
		public DbParameter this[int index]
		{
			get
			{
				return this.GetParameter(index);
			}
			set
			{
				this.SetParameter(index, value);
			}
		}

		// Token: 0x170007E6 RID: 2022
		public DbParameter this[string parameterName]
		{
			get
			{
				return this.GetParameter(parameterName);
			}
			set
			{
				this.SetParameter(parameterName, value);
			}
		}

		// Token: 0x0600305A RID: 12378
		public abstract int Add(object value);

		// Token: 0x0600305B RID: 12379
		public abstract void AddRange(Array values);

		// Token: 0x0600305C RID: 12380
		public abstract bool Contains(object value);

		// Token: 0x0600305D RID: 12381
		public abstract bool Contains(string value);

		// Token: 0x0600305E RID: 12382
		public abstract void CopyTo(Array array, int index);

		// Token: 0x0600305F RID: 12383
		public abstract void Clear();

		// Token: 0x06003060 RID: 12384
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		// Token: 0x06003061 RID: 12385
		protected abstract DbParameter GetParameter(int index);

		// Token: 0x06003062 RID: 12386
		protected abstract DbParameter GetParameter(string parameterName);

		// Token: 0x06003063 RID: 12387
		public abstract int IndexOf(object value);

		// Token: 0x06003064 RID: 12388
		public abstract int IndexOf(string parameterName);

		// Token: 0x06003065 RID: 12389
		public abstract void Insert(int index, object value);

		// Token: 0x06003066 RID: 12390
		public abstract void Remove(object value);

		// Token: 0x06003067 RID: 12391
		public abstract void RemoveAt(int index);

		// Token: 0x06003068 RID: 12392
		public abstract void RemoveAt(string parameterName);

		// Token: 0x06003069 RID: 12393
		protected abstract void SetParameter(int index, DbParameter value);

		// Token: 0x0600306A RID: 12394
		protected abstract void SetParameter(string parameterName, DbParameter value);
	}
}
