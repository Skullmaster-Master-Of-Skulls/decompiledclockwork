using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001815 RID: 6165
	[DataContract]
	[Serializable]
	public class RadComboBoxData
	{
		// Token: 0x170048A2 RID: 18594
		// (get) Token: 0x0600F017 RID: 61463 RVA: 0x0036A290 File Offset: 0x00368490
		// (set) Token: 0x0600F018 RID: 61464 RVA: 0x0036A298 File Offset: 0x00368498
		[DataMember]
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

		// Token: 0x170048A3 RID: 18595
		// (get) Token: 0x0600F019 RID: 61465 RVA: 0x0036A2A1 File Offset: 0x003684A1
		// (set) Token: 0x0600F01A RID: 61466 RVA: 0x0036A2A9 File Offset: 0x003684A9
		[DataMember]
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

		// Token: 0x170048A4 RID: 18596
		// (get) Token: 0x0600F01B RID: 61467 RVA: 0x0036A2B2 File Offset: 0x003684B2
		// (set) Token: 0x0600F01C RID: 61468 RVA: 0x0036A2BA File Offset: 0x003684BA
		[DataMember]
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

		// Token: 0x170048A5 RID: 18597
		// (get) Token: 0x0600F01D RID: 61469 RVA: 0x0036A2C3 File Offset: 0x003684C3
		// (set) Token: 0x0600F01E RID: 61470 RVA: 0x0036A2CB File Offset: 0x003684CB
		[DataMember]
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

		// Token: 0x170048A6 RID: 18598
		// (get) Token: 0x0600F01F RID: 61471 RVA: 0x0036A2D4 File Offset: 0x003684D4
		// (set) Token: 0x0600F020 RID: 61472 RVA: 0x0036A2DC File Offset: 0x003684DC
		[DataMember]
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

		// Token: 0x170048A7 RID: 18599
		// (get) Token: 0x0600F021 RID: 61473 RVA: 0x0036A2E5 File Offset: 0x003684E5
		// (set) Token: 0x0600F022 RID: 61474 RVA: 0x0036A2ED File Offset: 0x003684ED
		[DataMember]
		public Dictionary<string, object> Context
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

		// Token: 0x170048A8 RID: 18600
		// (get) Token: 0x0600F023 RID: 61475 RVA: 0x0036A2F6 File Offset: 0x003684F6
		// (set) Token: 0x0600F024 RID: 61476 RVA: 0x0036A2FE File Offset: 0x003684FE
		[DataMember]
		public RadComboBoxItemData[] Items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		// Token: 0x04004535 RID: 17717
		private string _text = string.Empty;

		// Token: 0x04004536 RID: 17718
		private string _value = string.Empty;

		// Token: 0x04004537 RID: 17719
		private int _numberOfItems;

		// Token: 0x04004538 RID: 17720
		private string _message = string.Empty;

		// Token: 0x04004539 RID: 17721
		private bool _endOfItems;

		// Token: 0x0400453A RID: 17722
		private Dictionary<string, object> _context = new Dictionary<string, object>();

		// Token: 0x0400453B RID: 17723
		private RadComboBoxItemData[] _items;
	}
}
