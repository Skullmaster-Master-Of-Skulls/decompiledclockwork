using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000269 RID: 617
	public class XmlSchemaObjectCollection : CollectionBase
	{
		// Token: 0x06001CB0 RID: 7344 RVA: 0x00083571 File Offset: 0x00082571
		public XmlSchemaObjectCollection()
		{
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00083579 File Offset: 0x00082579
		public XmlSchemaObjectCollection(XmlSchemaObject parent)
		{
			this.parent = parent;
		}

		// Token: 0x17000765 RID: 1893
		public virtual XmlSchemaObject this[int index]
		{
			get
			{
				return (XmlSchemaObject)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000835AA File Offset: 0x000825AA
		public new XmlSchemaObjectEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectEnumerator(base.InnerList.GetEnumerator());
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000835BC File Offset: 0x000825BC
		public int Add(XmlSchemaObject item)
		{
			return base.List.Add(item);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000835CA File Offset: 0x000825CA
		public void Insert(int index, XmlSchemaObject item)
		{
			base.List.Insert(index, item);
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000835D9 File Offset: 0x000825D9
		public int IndexOf(XmlSchemaObject item)
		{
			return base.List.IndexOf(item);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000835E7 File Offset: 0x000825E7
		public bool Contains(XmlSchemaObject item)
		{
			return base.List.Contains(item);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000835F5 File Offset: 0x000825F5
		public void Remove(XmlSchemaObject item)
		{
			base.List.Remove(item);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00083603 File Offset: 0x00082603
		public void CopyTo(XmlSchemaObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00083612 File Offset: 0x00082612
		protected override void OnInsert(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnAdd(this, item);
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x00083629 File Offset: 0x00082629
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, oldValue);
				this.parent.OnAdd(this, newValue);
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0008364D File Offset: 0x0008264D
		protected override void OnClear()
		{
			if (this.parent != null)
			{
				this.parent.OnClear(this);
			}
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00083663 File Offset: 0x00082663
		protected override void OnRemove(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, item);
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0008367C File Offset: 0x0008267C
		internal XmlSchemaObjectCollection Clone()
		{
			return new XmlSchemaObjectCollection
			{
				this
			};
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00083697 File Offset: 0x00082697
		private void Add(XmlSchemaObjectCollection collToAdd)
		{
			base.InnerList.InsertRange(0, collToAdd);
		}

		// Token: 0x040011A2 RID: 4514
		private XmlSchemaObject parent;
	}
}
