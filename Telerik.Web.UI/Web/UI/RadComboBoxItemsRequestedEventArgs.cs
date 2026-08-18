using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001AED RID: 6893
	public class RadComboBoxItemsRequestedEventArgs : EventArgs
	{
		// Token: 0x17005128 RID: 20776
		// (get) Token: 0x06010AF3 RID: 68339 RVA: 0x003B78B9 File Offset: 0x003B5AB9
		// (set) Token: 0x06010AF4 RID: 68340 RVA: 0x003B78C1 File Offset: 0x003B5AC1
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x17005129 RID: 20777
		// (get) Token: 0x06010AF5 RID: 68341 RVA: 0x003B78CA File Offset: 0x003B5ACA
		// (set) Token: 0x06010AF6 RID: 68342 RVA: 0x003B78D2 File Offset: 0x003B5AD2
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700512A RID: 20778
		// (get) Token: 0x06010AF7 RID: 68343 RVA: 0x003B78DB File Offset: 0x003B5ADB
		// (set) Token: 0x06010AF8 RID: 68344 RVA: 0x003B78E3 File Offset: 0x003B5AE3
		public int NumberOfItems
		{
			get
			{
				return this._numberOfItems;
			}
			set
			{
				this._numberOfItems = value;
			}
		}

		// Token: 0x1700512B RID: 20779
		// (get) Token: 0x06010AF9 RID: 68345 RVA: 0x003B78EC File Offset: 0x003B5AEC
		// (set) Token: 0x06010AFA RID: 68346 RVA: 0x003B78F4 File Offset: 0x003B5AF4
		public bool EndOfItems
		{
			get
			{
				return this._endOfItems;
			}
			set
			{
				this._endOfItems = value;
			}
		}

		// Token: 0x1700512C RID: 20780
		// (get) Token: 0x06010AFB RID: 68347 RVA: 0x003B78FD File Offset: 0x003B5AFD
		// (set) Token: 0x06010AFC RID: 68348 RVA: 0x003B7905 File Offset: 0x003B5B05
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}

		// Token: 0x1700512D RID: 20781
		// (get) Token: 0x06010AFD RID: 68349 RVA: 0x003B790E File Offset: 0x003B5B0E
		// (set) Token: 0x06010AFE RID: 68350 RVA: 0x003B7916 File Offset: 0x003B5B16
		public IDictionary<string, object> Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x04004A72 RID: 19058
		private string _text = string.Empty;

		// Token: 0x04004A73 RID: 19059
		private string _value = string.Empty;

		// Token: 0x04004A74 RID: 19060
		private int _numberOfItems;

		// Token: 0x04004A75 RID: 19061
		private string _message = string.Empty;

		// Token: 0x04004A76 RID: 19062
		private bool _endOfItems;

		// Token: 0x04004A77 RID: 19063
		private IDictionary<string, object> _context = new Dictionary<string, object>();
	}
}
