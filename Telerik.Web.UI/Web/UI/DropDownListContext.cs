using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B13 RID: 2835
	[DataContract]
	public class DropDownListContext
	{
		// Token: 0x170022B3 RID: 8883
		// (get) Token: 0x060069EF RID: 27119 RVA: 0x0018DF66 File Offset: 0x0018C166
		// (set) Token: 0x060069F0 RID: 27120 RVA: 0x0018DF6E File Offset: 0x0018C16E
		[DataMember]
		public int ItemsCount
		{
			get
			{
				return this._itemsCount;
			}
			set
			{
				this._itemsCount = value;
			}
		}

		// Token: 0x170022B4 RID: 8884
		// (get) Token: 0x060069F1 RID: 27121 RVA: 0x0018DF77 File Offset: 0x0018C177
		// (set) Token: 0x060069F2 RID: 27122 RVA: 0x0018DF7F File Offset: 0x0018C17F
		[DataMember]
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
			set
			{
				this._startIndex = value;
			}
		}

		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x060069F3 RID: 27123 RVA: 0x0018DF88 File Offset: 0x0018C188
		// (set) Token: 0x060069F4 RID: 27124 RVA: 0x0018DF90 File Offset: 0x0018C190
		[DataMember]
		public IDictionary UserContext
		{
			get
			{
				return this._userContext;
			}
			set
			{
				this._userContext = value;
			}
		}

		// Token: 0x04001CB5 RID: 7349
		private int _itemsCount;

		// Token: 0x04001CB6 RID: 7350
		private int _startIndex;

		// Token: 0x04001CB7 RID: 7351
		private IDictionary _userContext;
	}
}
