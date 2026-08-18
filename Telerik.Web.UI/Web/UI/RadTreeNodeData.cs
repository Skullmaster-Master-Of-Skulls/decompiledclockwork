using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B67 RID: 7015
	[DataContract]
	[Serializable]
	public class RadTreeNodeData : ControlItemData
	{
		// Token: 0x06010F97 RID: 69527 RVA: 0x003C14A4 File Offset: 0x003BF6A4
		public RadTreeNodeData()
		{
			this._expandMode = TreeNodeExpandMode.ClientSide;
			this._navigateUrl = string.Empty;
			this._postBack = true;
			this._cssClass = string.Empty;
			this._disabledCssClass = string.Empty;
			this._selectedCssClass = string.Empty;
			this._hoveredCssClass = string.Empty;
			this._imageUrl = string.Empty;
			this._hoveredCssClass = string.Empty;
			this._disabledImageUrl = string.Empty;
			this._expandedImageUrl = string.Empty;
			this._checked = false;
		}

		// Token: 0x170052D6 RID: 21206
		// (get) Token: 0x06010F98 RID: 69528 RVA: 0x003C152F File Offset: 0x003BF72F
		// (set) Token: 0x06010F99 RID: 69529 RVA: 0x003C1537 File Offset: 0x003BF737
		[DataMember]
		public TreeNodeExpandMode ExpandMode
		{
			get
			{
				return this._expandMode;
			}
			set
			{
				this._expandMode = value;
			}
		}

		// Token: 0x170052D7 RID: 21207
		// (get) Token: 0x06010F9A RID: 69530 RVA: 0x003C1540 File Offset: 0x003BF740
		// (set) Token: 0x06010F9B RID: 69531 RVA: 0x003C1548 File Offset: 0x003BF748
		[DataMember]
		public string NavigateUrl
		{
			get
			{
				return this._navigateUrl;
			}
			set
			{
				this._navigateUrl = value;
			}
		}

		// Token: 0x170052D8 RID: 21208
		// (get) Token: 0x06010F9C RID: 69532 RVA: 0x003C1551 File Offset: 0x003BF751
		// (set) Token: 0x06010F9D RID: 69533 RVA: 0x003C1559 File Offset: 0x003BF759
		[DataMember]
		public bool PostBack
		{
			get
			{
				return this._postBack;
			}
			set
			{
				this._postBack = value;
			}
		}

		// Token: 0x170052D9 RID: 21209
		// (get) Token: 0x06010F9E RID: 69534 RVA: 0x003C1562 File Offset: 0x003BF762
		// (set) Token: 0x06010F9F RID: 69535 RVA: 0x003C156A File Offset: 0x003BF76A
		[DataMember]
		public string CssClass
		{
			get
			{
				return this._cssClass;
			}
			set
			{
				this._cssClass = value;
			}
		}

		// Token: 0x170052DA RID: 21210
		// (get) Token: 0x06010FA0 RID: 69536 RVA: 0x003C1573 File Offset: 0x003BF773
		// (set) Token: 0x06010FA1 RID: 69537 RVA: 0x003C157B File Offset: 0x003BF77B
		[DataMember]
		public string DisabledCssClass
		{
			get
			{
				return this._disabledCssClass;
			}
			set
			{
				this._disabledCssClass = value;
			}
		}

		// Token: 0x170052DB RID: 21211
		// (get) Token: 0x06010FA2 RID: 69538 RVA: 0x003C1584 File Offset: 0x003BF784
		// (set) Token: 0x06010FA3 RID: 69539 RVA: 0x003C158C File Offset: 0x003BF78C
		[DataMember]
		public string SelectedCssClass
		{
			get
			{
				return this._selectedCssClass;
			}
			set
			{
				this._selectedCssClass = value;
			}
		}

		// Token: 0x170052DC RID: 21212
		// (get) Token: 0x06010FA4 RID: 69540 RVA: 0x003C1595 File Offset: 0x003BF795
		// (set) Token: 0x06010FA5 RID: 69541 RVA: 0x003C159D File Offset: 0x003BF79D
		[DataMember]
		public string ContentCssClass
		{
			get
			{
				return this._contentCssClass;
			}
			set
			{
				this._contentCssClass = value;
			}
		}

		// Token: 0x170052DD RID: 21213
		// (get) Token: 0x06010FA6 RID: 69542 RVA: 0x003C15A6 File Offset: 0x003BF7A6
		// (set) Token: 0x06010FA7 RID: 69543 RVA: 0x003C15AE File Offset: 0x003BF7AE
		[DataMember]
		public string HoveredCssClass
		{
			get
			{
				return this._hoveredCssClass;
			}
			set
			{
				this._hoveredCssClass = value;
			}
		}

		// Token: 0x170052DE RID: 21214
		// (get) Token: 0x06010FA8 RID: 69544 RVA: 0x003C15B7 File Offset: 0x003BF7B7
		// (set) Token: 0x06010FA9 RID: 69545 RVA: 0x003C15BF File Offset: 0x003BF7BF
		[DataMember]
		public string ImageUrl
		{
			get
			{
				return this._imageUrl;
			}
			set
			{
				this._imageUrl = value;
			}
		}

		// Token: 0x170052DF RID: 21215
		// (get) Token: 0x06010FAA RID: 69546 RVA: 0x003C15C8 File Offset: 0x003BF7C8
		// (set) Token: 0x06010FAB RID: 69547 RVA: 0x003C15D0 File Offset: 0x003BF7D0
		[DataMember]
		public string HoveredImageUrl
		{
			get
			{
				return this._hoveredImageUrl;
			}
			set
			{
				this._hoveredImageUrl = value;
			}
		}

		// Token: 0x170052E0 RID: 21216
		// (get) Token: 0x06010FAC RID: 69548 RVA: 0x003C15D9 File Offset: 0x003BF7D9
		// (set) Token: 0x06010FAD RID: 69549 RVA: 0x003C15E1 File Offset: 0x003BF7E1
		[DataMember]
		public string DisabledImageUrl
		{
			get
			{
				return this._disabledImageUrl;
			}
			set
			{
				this._disabledImageUrl = value;
			}
		}

		// Token: 0x170052E1 RID: 21217
		// (get) Token: 0x06010FAE RID: 69550 RVA: 0x003C15EA File Offset: 0x003BF7EA
		// (set) Token: 0x06010FAF RID: 69551 RVA: 0x003C15F2 File Offset: 0x003BF7F2
		[DataMember]
		public string ExpandedImageUrl
		{
			get
			{
				return this._expandedImageUrl;
			}
			set
			{
				this._expandedImageUrl = value;
			}
		}

		// Token: 0x170052E2 RID: 21218
		// (get) Token: 0x06010FB0 RID: 69552 RVA: 0x003C15FB File Offset: 0x003BF7FB
		// (set) Token: 0x06010FB1 RID: 69553 RVA: 0x003C1603 File Offset: 0x003BF803
		[DataMember]
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

		// Token: 0x170052E3 RID: 21219
		// (get) Token: 0x06010FB2 RID: 69554 RVA: 0x003C160C File Offset: 0x003BF80C
		// (set) Token: 0x06010FB3 RID: 69555 RVA: 0x003C1614 File Offset: 0x003BF814
		[DataMember]
		public bool Checked
		{
			get
			{
				return this._checked;
			}
			set
			{
				this._checked = value;
			}
		}

		// Token: 0x04004BFC RID: 19452
		private TreeNodeExpandMode _expandMode;

		// Token: 0x04004BFD RID: 19453
		private string _navigateUrl;

		// Token: 0x04004BFE RID: 19454
		private bool _postBack;

		// Token: 0x04004BFF RID: 19455
		private string _cssClass;

		// Token: 0x04004C00 RID: 19456
		private string _disabledCssClass;

		// Token: 0x04004C01 RID: 19457
		private string _selectedCssClass;

		// Token: 0x04004C02 RID: 19458
		private string _contentCssClass;

		// Token: 0x04004C03 RID: 19459
		private string _hoveredCssClass;

		// Token: 0x04004C04 RID: 19460
		private string _imageUrl;

		// Token: 0x04004C05 RID: 19461
		private string _hoveredImageUrl;

		// Token: 0x04004C06 RID: 19462
		private string _disabledImageUrl;

		// Token: 0x04004C07 RID: 19463
		private string _expandedImageUrl;

		// Token: 0x04004C08 RID: 19464
		private string _contextMenuID;

		// Token: 0x04004C09 RID: 19465
		private bool _checked;
	}
}
