using System;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200022A RID: 554
	public class DiagramItemDataBoundEventArgs : EventArgs
	{
		// Token: 0x0600144B RID: 5195 RVA: 0x000469D8 File Offset: 0x00044BD8
		internal DiagramItemDataBoundEventArgs(object item, object dataItem)
		{
			this.Item = item;
			this.DataItem = dataItem;
			if (item.GetType() == typeof(DiagramShape))
			{
				this.ItemType = "Shape";
				return;
			}
			if (item.GetType() == typeof(DiagramConnection))
			{
				this.ItemType = "Connection";
				return;
			}
			throw new ArgumentException("Invalid parameter. The provided parameter must be of type DiagramShape or DiagramConnection.");
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00046A49 File Offset: 0x00044C49
		// (set) Token: 0x0600144D RID: 5197 RVA: 0x00046A51 File Offset: 0x00044C51
		public object DataItem { get; private set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x00046A5A File Offset: 0x00044C5A
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x00046A62 File Offset: 0x00044C62
		public object Item { get; private set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00046A6B File Offset: 0x00044C6B
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x00046A73 File Offset: 0x00044C73
		public string ItemType { get; set; }
	}
}
