using System;

namespace System.Xml.Schema
{
	// Token: 0x02000181 RID: 385
	internal class ForwardAxis
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x000572D6 File Offset: 0x000562D6
		internal DoubleLinkAxis RootNode
		{
			get
			{
				return this.rootNode;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x000572DE File Offset: 0x000562DE
		internal DoubleLinkAxis TopNode
		{
			get
			{
				return this.topNode;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x000572E6 File Offset: 0x000562E6
		internal bool IsAttribute
		{
			get
			{
				return this.isAttribute;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x000572EE File Offset: 0x000562EE
		internal bool IsDss
		{
			get
			{
				return this.isDss;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x000572F6 File Offset: 0x000562F6
		internal bool IsSelfAxis
		{
			get
			{
				return this.isSelfAxis;
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00057300 File Offset: 0x00056300
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

		// Token: 0x04000C61 RID: 3169
		private DoubleLinkAxis topNode;

		// Token: 0x04000C62 RID: 3170
		private DoubleLinkAxis rootNode;

		// Token: 0x04000C63 RID: 3171
		private bool isAttribute;

		// Token: 0x04000C64 RID: 3172
		private bool isDss;

		// Token: 0x04000C65 RID: 3173
		private bool isSelfAxis;
	}
}
