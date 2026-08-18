using System;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001840 RID: 6208
	public class LayoutBuilderCellCollection : StronglyTypedStateManagedCollection<LayoutBuilderCell>
	{
		// Token: 0x0600F137 RID: 61751 RVA: 0x0036D5E8 File Offset: 0x0036B7E8
		public virtual void Add(string id, string colSpan, string rowSpan, string width, string height, string content)
		{
			this.Add(new LayoutBuilderCell
			{
				ID = id,
				ColSpan = colSpan,
				RowSpan = rowSpan,
				Width = width,
				Height = height,
				Content = content
			});
		}

		// Token: 0x0600F138 RID: 61752 RVA: 0x0036D630 File Offset: 0x0036B830
		internal object[] GetItemsCollection()
		{
			object[] array = new object[base.Count];
			for (int i = 0; i < base.Count; i++)
			{
				array[i] = new object[]
				{
					this.GetItemID(this[i]),
					this.GetItemColSpan(this[i]),
					this.GetItemRowSpan(this[i]),
					this.GetItemRowWidht(this[i]),
					this.GetItemRowHeight(this[i]),
					this.GetItemRowContent(this[i])
				};
			}
			return array;
		}

		// Token: 0x0600F139 RID: 61753 RVA: 0x0036D6C6 File Offset: 0x0036B8C6
		protected override void SetDirtyObject(object o)
		{
			((StateManager)o).SetDirty();
		}

		// Token: 0x0600F13A RID: 61754 RVA: 0x0036D6D4 File Offset: 0x0036B8D4
		internal string Serialize(JavaScriptSerializer serializer)
		{
			object[] itemsCollection = this.GetItemsCollection();
			return serializer.Serialize(itemsCollection);
		}

		// Token: 0x0600F13B RID: 61755 RVA: 0x0036D6EF File Offset: 0x0036B8EF
		internal virtual object GetItemRowContent(LayoutBuilderCell item)
		{
			return item.Content;
		}

		// Token: 0x0600F13C RID: 61756 RVA: 0x0036D6F7 File Offset: 0x0036B8F7
		internal virtual object GetItemID(LayoutBuilderCell item)
		{
			return item.ID;
		}

		// Token: 0x0600F13D RID: 61757 RVA: 0x0036D6FF File Offset: 0x0036B8FF
		internal virtual object GetItemColSpan(LayoutBuilderCell item)
		{
			return item.ColSpan;
		}

		// Token: 0x0600F13E RID: 61758 RVA: 0x0036D707 File Offset: 0x0036B907
		internal virtual object GetItemRowSpan(LayoutBuilderCell item)
		{
			return item.RowSpan;
		}

		// Token: 0x0600F13F RID: 61759 RVA: 0x0036D70F File Offset: 0x0036B90F
		internal virtual object GetItemRowWidht(LayoutBuilderCell item)
		{
			return item.RowSpan;
		}

		// Token: 0x0600F140 RID: 61760 RVA: 0x0036D717 File Offset: 0x0036B917
		internal virtual object GetItemRowHeight(LayoutBuilderCell item)
		{
			return item.RowSpan;
		}
	}
}
