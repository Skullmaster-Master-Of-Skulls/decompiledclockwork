using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B6E RID: 7022
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TreeViewPostBackArguments
	{
		// Token: 0x1700530E RID: 21262
		// (get) Token: 0x06011014 RID: 69652 RVA: 0x003C2151 File Offset: 0x003C0351
		// (set) Token: 0x06011015 RID: 69653 RVA: 0x003C2159 File Offset: 0x003C0359
		public TreeViewPostBackCommand CommandName
		{
			get
			{
				return this._commandName;
			}
			set
			{
				this._commandName = value;
			}
		}

		// Token: 0x1700530F RID: 21263
		// (get) Token: 0x06011016 RID: 69654 RVA: 0x003C2162 File Offset: 0x003C0362
		// (set) Token: 0x06011017 RID: 69655 RVA: 0x003C216A File Offset: 0x003C036A
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

		// Token: 0x17005310 RID: 21264
		// (get) Token: 0x06011018 RID: 69656 RVA: 0x003C2173 File Offset: 0x003C0373
		// (set) Token: 0x06011019 RID: 69657 RVA: 0x003C217B File Offset: 0x003C037B
		public string DestIndex
		{
			get
			{
				return this._destIndex;
			}
			set
			{
				this._destIndex = value;
			}
		}

		// Token: 0x17005311 RID: 21265
		// (get) Token: 0x0601101A RID: 69658 RVA: 0x003C2184 File Offset: 0x003C0384
		// (set) Token: 0x0601101B RID: 69659 RVA: 0x003C218C File Offset: 0x003C038C
		public List<string> SourceNodesIndices
		{
			get
			{
				return this._sourceNodesIndices;
			}
			set
			{
				this._sourceNodesIndices = value;
			}
		}

		// Token: 0x17005312 RID: 21266
		// (get) Token: 0x0601101C RID: 69660 RVA: 0x003C2195 File Offset: 0x003C0395
		// (set) Token: 0x0601101D RID: 69661 RVA: 0x003C219D File Offset: 0x003C039D
		public TreeViewClientState ClientState
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

		// Token: 0x17005313 RID: 21267
		// (get) Token: 0x0601101E RID: 69662 RVA: 0x003C21A6 File Offset: 0x003C03A6
		// (set) Token: 0x0601101F RID: 69663 RVA: 0x003C21AE File Offset: 0x003C03AE
		public string HtmlElementId
		{
			get
			{
				return this._htmlElementId;
			}
			set
			{
				this._htmlElementId = value;
			}
		}

		// Token: 0x17005314 RID: 21268
		// (get) Token: 0x06011020 RID: 69664 RVA: 0x003C21B7 File Offset: 0x003C03B7
		// (set) Token: 0x06011021 RID: 69665 RVA: 0x003C21BF File Offset: 0x003C03BF
		public string TreeId
		{
			get
			{
				return this._treeId;
			}
			set
			{
				this._treeId = value;
			}
		}

		// Token: 0x17005315 RID: 21269
		// (get) Token: 0x06011022 RID: 69666 RVA: 0x003C21C8 File Offset: 0x003C03C8
		// (set) Token: 0x06011023 RID: 69667 RVA: 0x003C21D0 File Offset: 0x003C03D0
		public RadTreeViewDropPosition DropPosition
		{
			get
			{
				return this._dropPosition;
			}
			set
			{
				this._dropPosition = value;
			}
		}

		// Token: 0x17005316 RID: 21270
		// (get) Token: 0x06011024 RID: 69668 RVA: 0x003C21D9 File Offset: 0x003C03D9
		// (set) Token: 0x06011025 RID: 69669 RVA: 0x003C21E1 File Offset: 0x003C03E1
		public string MenuItemIndex
		{
			get
			{
				return this._menuItemIndex;
			}
			set
			{
				this._menuItemIndex = value;
			}
		}

		// Token: 0x17005317 RID: 21271
		// (get) Token: 0x06011026 RID: 69670 RVA: 0x003C21EA File Offset: 0x003C03EA
		// (set) Token: 0x06011027 RID: 69671 RVA: 0x003C21F2 File Offset: 0x003C03F2
		public string ContextMenuID
		{
			get
			{
				return this._contextMenuID;
			}
			set
			{
				this._contextMenuID = value;
			}
		}

		// Token: 0x17005318 RID: 21272
		// (get) Token: 0x06011028 RID: 69672 RVA: 0x003C21FB File Offset: 0x003C03FB
		// (set) Token: 0x06011029 RID: 69673 RVA: 0x003C2203 File Offset: 0x003C0403
		public string NodeEditText
		{
			get
			{
				return this._nodeEditText;
			}
			set
			{
				this._nodeEditText = value;
			}
		}

		// Token: 0x17005319 RID: 21273
		// (get) Token: 0x0601102A RID: 69674 RVA: 0x003C220C File Offset: 0x003C040C
		// (set) Token: 0x0601102B RID: 69675 RVA: 0x003C2214 File Offset: 0x003C0414
		public IDictionary<string, object> Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x04004C1B RID: 19483
		private TreeViewPostBackCommand _commandName;

		// Token: 0x04004C1C RID: 19484
		private string _index;

		// Token: 0x04004C1D RID: 19485
		private string _destIndex;

		// Token: 0x04004C1E RID: 19486
		private List<string> _sourceNodesIndices;

		// Token: 0x04004C1F RID: 19487
		private TreeViewClientState _clientState;

		// Token: 0x04004C20 RID: 19488
		private string _htmlElementId;

		// Token: 0x04004C21 RID: 19489
		private string _treeId;

		// Token: 0x04004C22 RID: 19490
		private RadTreeViewDropPosition _dropPosition;

		// Token: 0x04004C23 RID: 19491
		private string _menuItemIndex;

		// Token: 0x04004C24 RID: 19492
		private string _contextMenuID;

		// Token: 0x04004C25 RID: 19493
		private string _nodeEditText;

		// Token: 0x04004C26 RID: 19494
		private IDictionary<string, object> _data;
	}
}
