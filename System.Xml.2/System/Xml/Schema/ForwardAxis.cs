using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DB RID: 475
	internal class ForwardAxis
	{
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x000AB54A File Offset: 0x000A974A
		internal DoubleLinkAxis RootNode
		{
			get
			{
				return this.rootNode;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x000AB552 File Offset: 0x000A9752
		internal DoubleLinkAxis TopNode
		{
			get
			{
				return this.topNode;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x000AB55A File Offset: 0x000A975A
		internal bool IsAttribute
		{
			get
			{
				return this.isAttribute;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x000AB562 File Offset: 0x000A9762
		internal bool IsDss
		{
			get
			{
				return this.isDss;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x000AB56A File Offset: 0x000A976A
		internal bool IsSelfAxis
		{
			get
			{
				return this.isSelfAxis;
			}
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000AB574 File Offset: 0x000A9774
		public ForwardAxis(DoubleLinkAxis axis, bool isdesorself)
		{
			this.isDss = isdesorself;
			this.isAttribute = Asttree.IsAttribute(axis);
			this.topNode = axis;
			this.rootNode = axis;
			while (this.rootNode.Input != null)
			{
				this.rootNode = (DoubleLinkAxis)this.rootNode.Input;
			}
			this.isSelfAxis = Asttree.IsSelf(this.topNode);
		}

		// Token: 0x04000D5B RID: 3419
		private DoubleLinkAxis topNode;

		// Token: 0x04000D5C RID: 3420
		private DoubleLinkAxis rootNode;

		// Token: 0x04000D5D RID: 3421
		private bool isAttribute;

		// Token: 0x04000D5E RID: 3422
		private bool isDss;

		// Token: 0x04000D5F RID: 3423
		private bool isSelfAxis;
	}
}
