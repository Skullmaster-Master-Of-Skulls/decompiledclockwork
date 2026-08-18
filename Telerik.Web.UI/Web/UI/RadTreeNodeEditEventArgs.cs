using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B63 RID: 7011
	public class RadTreeNodeEditEventArgs : RadTreeNodeEventArgs
	{
		// Token: 0x170052D5 RID: 21205
		// (get) Token: 0x06010F88 RID: 69512 RVA: 0x003C1483 File Offset: 0x003BF683
		// (set) Token: 0x06010F89 RID: 69513 RVA: 0x003C148B File Offset: 0x003BF68B
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

		// Token: 0x06010F8A RID: 69514 RVA: 0x003C1494 File Offset: 0x003BF694
		public RadTreeNodeEditEventArgs(RadTreeNode node, string text) : base(node)
		{
			this._text = text;
		}

		// Token: 0x04004BFB RID: 19451
		private string _text;
	}
}
