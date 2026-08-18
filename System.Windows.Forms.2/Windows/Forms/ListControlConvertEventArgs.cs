using System;

namespace System.Windows.Forms
{
	// Token: 0x020002CF RID: 719
	public class ListControlConvertEventArgs : ConvertEventArgs
	{
		// Token: 0x06002CB0 RID: 11440 RVA: 0x000C89B3 File Offset: 0x000C6BB3
		public ListControlConvertEventArgs(object value, Type desiredType, object listItem) : base(value, desiredType)
		{
			this.listItem = listItem;
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x000C89C4 File Offset: 0x000C6BC4
		public object ListItem
		{
			get
			{
				return this.listItem;
			}
		}

		// Token: 0x0400129A RID: 4762
		private object listItem;
	}
}
