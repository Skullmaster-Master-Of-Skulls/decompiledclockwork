using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001AD RID: 429
	internal class DesignerActionVerbList : DesignerActionList
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x00059CEE File Offset: 0x00057EEE
		public DesignerActionVerbList(DesignerVerb[] verbs) : base(null)
		{
			this._verbs = verbs;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0000445B File Offset: 0x0000265B
		public override bool AutoShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00059D00 File Offset: 0x00057F00
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			for (int i = 0; i < this._verbs.Length; i++)
			{
				if (this._verbs[i].Visible && this._verbs[i].Enabled && this._verbs[i].Supported)
				{
					designerActionItemCollection.Add(new DesignerActionVerbItem(this._verbs[i]));
				}
			}
			return designerActionItemCollection;
		}

		// Token: 0x04000934 RID: 2356
		private DesignerVerb[] _verbs;
	}
}
