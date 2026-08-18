using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x0200028F RID: 655
	public abstract class GenericEditorToolBaseCollection<ItemType> : StateManagedCollection where ItemType : EditorToolBase
	{
		// Token: 0x170007FD RID: 2045
		public virtual ItemType this[int index]
		{
			get
			{
				return (ItemType)((object)this.List[index]);
			}
			set
			{
				this.List[index] = value;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0004E9AC File Offset: 0x0004CBAC
		protected IList List
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0004E9AF File Offset: 0x0004CBAF
		public virtual void Add(ItemType item)
		{
			this.List.Add(item);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0004E9C3 File Offset: 0x0004CBC3
		public virtual bool Contains(ItemType item)
		{
			return this.List.Contains(item);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0004E9D6 File Offset: 0x0004CBD6
		public virtual void CopyTo(ItemType[] array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0004E9E8 File Offset: 0x0004CBE8
		public virtual void AddRange(IEnumerable<ItemType> items)
		{
			foreach (ItemType item in items)
			{
				this.Add(item);
			}
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0004EA30 File Offset: 0x0004CC30
		public virtual int IndexOf(ItemType item)
		{
			return this.List.IndexOf(item);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0004EA43 File Offset: 0x0004CC43
		public virtual void Insert(int index, ItemType item)
		{
			this.List.Insert(index, item);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0004EA57 File Offset: 0x0004CC57
		public virtual void Remove(ItemType item)
		{
			this.List.Remove(item);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0004EA6A File Offset: 0x0004CC6A
		public virtual void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0004EA78 File Offset: 0x0004CC78
		protected override Type[] GetKnownTypes()
		{
			return new Type[]
			{
				typeof(EditorTool),
				typeof(EditorSeparator),
				typeof(EditorToolStrip)
			};
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0004EAB4 File Offset: 0x0004CCB4
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new EditorTool();
			case 1:
				return new EditorSeparator();
			case 2:
				return new EditorToolStrip();
			default:
				return null;
			}
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0004EAEA File Offset: 0x0004CCEA
		protected override void SetDirtyObject(object o)
		{
			((EditorToolBase)o).SetDirty();
		}
	}
}
