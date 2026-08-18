using System;

namespace AjaxControlToolkit
{
	// Token: 0x0200000B RID: 11
	public class AccordionItemEventArgs : EventArgs
	{
		// Token: 0x06000093 RID: 147 RVA: 0x0000397C File Offset: 0x00001B7C
		public AccordionItemEventArgs(AccordionContentPanel item, AccordionItemType type)
		{
			this._item = item;
			this._type = type;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003992 File Offset: 0x00001B92
		public AccordionContentPanel AccordionItem
		{
			get
			{
				return this._item;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000399A File Offset: 0x00001B9A
		public AccordionItemType ItemType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000039A2 File Offset: 0x00001BA2
		public object Item
		{
			get
			{
				return this._item.DataItem;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000039AF File Offset: 0x00001BAF
		public int ItemIndex
		{
			get
			{
				return this._item.DataItemIndex;
			}
		}

		// Token: 0x04000026 RID: 38
		private AccordionContentPanel _item;

		// Token: 0x04000027 RID: 39
		private AccordionItemType _type;
	}
}
