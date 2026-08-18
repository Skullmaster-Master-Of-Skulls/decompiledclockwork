using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000092 RID: 146
	public class DataPagerFieldCollection : StateManagedCollection
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000675 RID: 1653 RVA: 0x0001C180 File Offset: 0x0001A380
		// (remove) Token: 0x06000676 RID: 1654 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		public event EventHandler FieldsChanged;

		// Token: 0x06000677 RID: 1655 RVA: 0x0001C1ED File Offset: 0x0001A3ED
		public DataPagerFieldCollection(DataPager dataPager)
		{
			this._dataPager = dataPager;
		}

		// Token: 0x170001D3 RID: 467
		[Browsable(false)]
		public DataPagerField this[int index]
		{
			get
			{
				return ((IList)this)[index] as DataPagerField;
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001C20A File Offset: 0x0001A40A
		public void Add(DataPagerField field)
		{
			((IList)this).Add(field);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001C214 File Offset: 0x0001A414
		public DataPagerFieldCollection CloneFields(DataPager pager)
		{
			DataPagerFieldCollection dataPagerFieldCollection = new DataPagerFieldCollection(pager);
			foreach (object obj in this)
			{
				DataPagerField dataPagerField = (DataPagerField)obj;
				dataPagerFieldCollection.Add(dataPagerField.CloneField());
			}
			return dataPagerFieldCollection;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001C278 File Offset: 0x0001A478
		public bool Contains(DataPagerField field)
		{
			return ((IList)this).Contains(field);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001C281 File Offset: 0x0001A481
		public void CopyTo(DataPagerField[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001C28B File Offset: 0x0001A48B
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new NextPreviousPagerField();
			case 1:
				return new NumericPagerField();
			case 2:
				return new TemplatePagerField();
			default:
				throw new ArgumentOutOfRangeException(AtlasWeb.PagerFieldCollection_InvalidTypeIndex);
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001C2BD File Offset: 0x0001A4BD
		protected override Type[] GetKnownTypes()
		{
			return DataPagerFieldCollection.knownTypes;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public int IndexOf(DataPagerField field)
		{
			return ((IList)this).IndexOf(field);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001C2CD File Offset: 0x0001A4CD
		public void Insert(int index, DataPagerField field)
		{
			((IList)this).Insert(index, field);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001C2D7 File Offset: 0x0001A4D7
		protected override void OnClearComplete()
		{
			this.OnFieldsChanged();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001C2D7 File Offset: 0x0001A4D7
		private void OnFieldChanged(object sender, EventArgs e)
		{
			this.OnFieldsChanged();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001C2DF File Offset: 0x0001A4DF
		private void OnFieldsChanged()
		{
			if (this.FieldsChanged != null)
			{
				this.FieldsChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		protected override void OnInsertComplete(int index, object value)
		{
			DataPagerField dataPagerField = value as DataPagerField;
			if (dataPagerField != null)
			{
				dataPagerField.FieldChanged += this.OnFieldChanged;
			}
			dataPagerField.SetDataPager(this._dataPager);
			this.OnFieldsChanged();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001C338 File Offset: 0x0001A538
		protected override void OnRemoveComplete(int index, object value)
		{
			DataPagerField dataPagerField = value as DataPagerField;
			if (dataPagerField != null)
			{
				dataPagerField.FieldChanged -= this.OnFieldChanged;
			}
			this.OnFieldsChanged();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001C367 File Offset: 0x0001A567
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is DataPagerField))
			{
				throw new ArgumentException(AtlasWeb.PagerFieldCollection_InvalidType);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001C383 File Offset: 0x0001A583
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001C38C File Offset: 0x0001A58C
		public void Remove(DataPagerField field)
		{
			((IList)this).Remove(field);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001C395 File Offset: 0x0001A595
		protected override void SetDirtyObject(object o)
		{
			((DataPagerField)o).SetDirty();
		}

		// Token: 0x0400024D RID: 589
		private DataPager _dataPager;

		// Token: 0x0400024E RID: 590
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(NextPreviousPagerField),
			typeof(NumericPagerField),
			typeof(TemplatePagerField)
		};
	}
}
