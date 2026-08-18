using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000336 RID: 822
	[ComVisible(true)]
	public class PropertyValueChangedEventArgs : EventArgs
	{
		// Token: 0x06003568 RID: 13672 RVA: 0x000F28A1 File Offset: 0x000F0AA1
		public PropertyValueChangedEventArgs(GridItem changedItem, object oldValue)
		{
			this.changedItem = changedItem;
			this.oldValue = oldValue;
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000F28B7 File Offset: 0x000F0AB7
		public GridItem ChangedItem
		{
			get
			{
				return this.changedItem;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600356A RID: 13674 RVA: 0x000F28BF File Offset: 0x000F0ABF
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		// Token: 0x04001F55 RID: 8021
		private readonly GridItem changedItem;

		// Token: 0x04001F56 RID: 8022
		private object oldValue;
	}
}
