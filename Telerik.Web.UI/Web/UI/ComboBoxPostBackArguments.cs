using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001AEF RID: 6895
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ComboBoxPostBackArguments
	{
		// Token: 0x17005132 RID: 20786
		// (get) Token: 0x06010B08 RID: 68360 RVA: 0x003B7988 File Offset: 0x003B5B88
		// (set) Token: 0x06010B09 RID: 68361 RVA: 0x003B7990 File Offset: 0x003B5B90
		public string Command
		{
			get
			{
				return this._command;
			}
			set
			{
				this._command = value;
			}
		}

		// Token: 0x17005133 RID: 20787
		// (get) Token: 0x06010B0A RID: 68362 RVA: 0x003B7999 File Offset: 0x003B5B99
		// (set) Token: 0x06010B0B RID: 68363 RVA: 0x003B79A1 File Offset: 0x003B5BA1
		public string Index
		{
			get
			{
				return this._index;
			}
			set
			{
				this._index = value;
			}
		}

		// Token: 0x17005134 RID: 20788
		// (get) Token: 0x06010B0C RID: 68364 RVA: 0x003B79AA File Offset: 0x003B5BAA
		// (set) Token: 0x06010B0D RID: 68365 RVA: 0x003B79B2 File Offset: 0x003B5BB2
		public RadComboBoxClientState ClientState
		{
			get
			{
				return this._clientState;
			}
			set
			{
				this._clientState = value;
			}
		}

		// Token: 0x17005135 RID: 20789
		// (get) Token: 0x06010B0E RID: 68366 RVA: 0x003B79BB File Offset: 0x003B5BBB
		// (set) Token: 0x06010B0F RID: 68367 RVA: 0x003B79C3 File Offset: 0x003B5BC3
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

		// Token: 0x17005136 RID: 20790
		// (get) Token: 0x06010B10 RID: 68368 RVA: 0x003B79CC File Offset: 0x003B5BCC
		// (set) Token: 0x06010B11 RID: 68369 RVA: 0x003B79D4 File Offset: 0x003B5BD4
		public RadComboBoxContext Context
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

		// Token: 0x17005137 RID: 20791
		// (get) Token: 0x06010B12 RID: 68370 RVA: 0x003B79DD File Offset: 0x003B5BDD
		// (set) Token: 0x06010B13 RID: 68371 RVA: 0x003B79E5 File Offset: 0x003B5BE5
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

		// Token: 0x17005138 RID: 20792
		// (get) Token: 0x06010B14 RID: 68372 RVA: 0x003B79EE File Offset: 0x003B5BEE
		// (set) Token: 0x06010B15 RID: 68373 RVA: 0x003B79F6 File Offset: 0x003B5BF6
		public bool CheckAllChecked { get; set; }

		// Token: 0x04004A7C RID: 19068
		private string _command;

		// Token: 0x04004A7D RID: 19069
		private string _index;

		// Token: 0x04004A7E RID: 19070
		private RadComboBoxClientState _clientState;

		// Token: 0x04004A7F RID: 19071
		private string _text;

		// Token: 0x04004A80 RID: 19072
		private RadComboBoxContext _context = new RadComboBoxContext();

		// Token: 0x04004A81 RID: 19073
		private int _numberOfItems;
	}
}
