using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015C7 RID: 5575
	internal class UnknownXMLObj : XMLObj
	{
		// Token: 0x0600D965 RID: 55653 RVA: 0x002FB5CE File Offset: 0x002F97CE
		public static FObj.Maker GetMaker(string space, string tag)
		{
			return new UnknownXMLObj.Maker(space, tag);
		}

		// Token: 0x0600D966 RID: 55654 RVA: 0x002FB5D8 File Offset: 0x002F97D8
		protected UnknownXMLObj(FObj parent, PropertyList propertyList, string space, string tag) : base(parent, propertyList, tag)
		{
			this.nmspace = space;
			if (!string.IsNullOrEmpty(space))
			{
				this.name = this.nmspace + ":" + tag;
				return;
			}
			this.name = "(none):" + tag;
		}

		// Token: 0x0600D967 RID: 55655 RVA: 0x002FB629 File Offset: 0x002F9829
		public override string GetNameSpace()
		{
			return this.nmspace;
		}

		// Token: 0x0600D968 RID: 55656 RVA: 0x002FB631 File Offset: 0x002F9831
		protected internal override void AddChild(FONode child)
		{
			if (this.doc == null)
			{
				base.CreateBasicDocument();
			}
			base.AddChild(child);
		}

		// Token: 0x0600D969 RID: 55657 RVA: 0x002FB649 File Offset: 0x002F9849
		protected internal override void AddCharacters(char[] data, int start, int length)
		{
			if (this.doc == null)
			{
				base.CreateBasicDocument();
			}
			base.AddCharacters(data, start, length);
		}

		// Token: 0x0600D96A RID: 55658 RVA: 0x002FB663 File Offset: 0x002F9863
		public override Status Layout(Area area)
		{
			return new Status(1);
		}

		// Token: 0x04003C1B RID: 15387
		private string nmspace;

		// Token: 0x020015C8 RID: 5576
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D96B RID: 55659 RVA: 0x002FB66B File Offset: 0x002F986B
			internal Maker(string sp, string t)
			{
				this.space = sp;
				this.tag = t;
			}

			// Token: 0x0600D96C RID: 55660 RVA: 0x002FB681 File Offset: 0x002F9881
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new UnknownXMLObj(parent, propertyList, this.space, this.tag);
			}

			// Token: 0x04003C1C RID: 15388
			private string space;

			// Token: 0x04003C1D RID: 15389
			private string tag;
		}
	}
}
