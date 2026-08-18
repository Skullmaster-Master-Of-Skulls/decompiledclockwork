using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020002A3 RID: 675
	public class XmlSchemaObjectCollection : CollectionBase
	{
		// Token: 0x06002752 RID: 10066 RVA: 0x000CF74A File Offset: 0x000CD94A
		public XmlSchemaObjectCollection()
		{
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000CF752 File Offset: 0x000CD952
		public XmlSchemaObjectCollection(XmlSchemaObject parent)
		{
			this.parent = parent;
		}

		// Token: 0x17000904 RID: 2308
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

		// Token: 0x06002756 RID: 10070 RVA: 0x000CF783 File Offset: 0x000CD983
		public new XmlSchemaObjectEnumerator GetEnumerator()
		{
			return new XmlSchemaObjectEnumerator(base.InnerList.GetEnumerator());
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000CF795 File Offset: 0x000CD995
		public int Add(XmlSchemaObject item)
		{
			return base.List.Add(item);
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000CF7A3 File Offset: 0x000CD9A3
		public void Insert(int index, XmlSchemaObject item)
		{
			base.List.Insert(index, item);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000CF7B2 File Offset: 0x000CD9B2
		public int IndexOf(XmlSchemaObject item)
		{
			return base.List.IndexOf(item);
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000CF7C0 File Offset: 0x000CD9C0
		public bool Contains(XmlSchemaObject item)
		{
			return base.List.Contains(item);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000CF7CE File Offset: 0x000CD9CE
		public void Remove(XmlSchemaObject item)
		{
			base.List.Remove(item);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000CF7DC File Offset: 0x000CD9DC
		public void CopyTo(XmlSchemaObject[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000CF7EB File Offset: 0x000CD9EB
		protected override void OnInsert(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnAdd(this, item);
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000CF802 File Offset: 0x000CDA02
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, oldValue);
				this.parent.OnAdd(this, newValue);
			}
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000CF826 File Offset: 0x000CDA26
		protected override void OnClear()
		{
			if (this.parent != null)
			{
				this.parent.OnClear(this);
			}
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000CF83C File Offset: 0x000CDA3C
		protected override void OnRemove(int index, object item)
		{
			if (this.parent != null)
			{
				this.parent.OnRemove(this, item);
			}
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x000CF854 File Offset: 0x000CDA54
		internal XmlSchemaObjectCollection Clone()
		{
			return new XmlSchemaObjectCollection
			{
				this
			};
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000CF86F File Offset: 0x000CDA6F
		private void Add(XmlSchemaObjectCollection collToAdd)
		{
			base.InnerList.InsertRange(0, collToAdd);
		}

		// Token: 0x04001124 RID: 4388
		private XmlSchemaObject parent;
	}
}
