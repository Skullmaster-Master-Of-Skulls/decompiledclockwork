using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x0200013C RID: 316
	public abstract class DbParameterCollection : MarshalByRefObject, IDataParameterCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060014AB RID: 5291
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract int Count { get; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060014AC RID: 5292
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public abstract bool IsFixedSize { get; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060014AD RID: 5293
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract bool IsReadOnly { get; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060014AE RID: 5294
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract bool IsSynchronized { get; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060014AF RID: 5295
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract object SyncRoot { get; }

		// Token: 0x170002E7 RID: 743
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

		// Token: 0x170002E8 RID: 744
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

		// Token: 0x170002E9 RID: 745
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

		// Token: 0x170002EA RID: 746
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

		// Token: 0x060014B8 RID: 5304
		public abstract int Add(object value);

		// Token: 0x060014B9 RID: 5305
		public abstract void AddRange(Array values);

		// Token: 0x060014BA RID: 5306
		public abstract bool Contains(object value);

		// Token: 0x060014BB RID: 5307
		public abstract bool Contains(string value);

		// Token: 0x060014BC RID: 5308
		public abstract void CopyTo(Array array, int index);

		// Token: 0x060014BD RID: 5309
		public abstract void Clear();

		// Token: 0x060014BE RID: 5310
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		// Token: 0x060014BF RID: 5311
		protected abstract DbParameter GetParameter(int index);

		// Token: 0x060014C0 RID: 5312
		protected abstract DbParameter GetParameter(string parameterName);

		// Token: 0x060014C1 RID: 5313
		public abstract int IndexOf(object value);

		// Token: 0x060014C2 RID: 5314
		public abstract int IndexOf(string parameterName);

		// Token: 0x060014C3 RID: 5315
		public abstract void Insert(int index, object value);

		// Token: 0x060014C4 RID: 5316
		public abstract void Remove(object value);

		// Token: 0x060014C5 RID: 5317
		public abstract void RemoveAt(int index);

		// Token: 0x060014C6 RID: 5318
		public abstract void RemoveAt(string parameterName);

		// Token: 0x060014C7 RID: 5319
		protected abstract void SetParameter(int index, DbParameter value);

		// Token: 0x060014C8 RID: 5320
		protected abstract void SetParameter(string parameterName, DbParameter value);
	}
}
