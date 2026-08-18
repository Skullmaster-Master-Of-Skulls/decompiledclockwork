using System;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Layout.Inline;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015C9 RID: 5577
	internal class XMLElement : XMLObj
	{
		// Token: 0x0600D96D RID: 55661 RVA: 0x002FB696 File Offset: 0x002F9896
		public static FObj.Maker GetMaker(string tag)
		{
			return new XMLElement.Maker(tag);
		}

		// Token: 0x0600D96E RID: 55662 RVA: 0x002FB69E File Offset: 0x002F989E
		public XMLElement(FObj parent, PropertyList propertyList, string tag) : base(parent, propertyList, tag)
		{
			this.Init();
		}

		// Token: 0x0600D96F RID: 55663 RVA: 0x002FB6BA File Offset: 0x002F98BA
		public override Status Layout(Area area)
		{
			if (!(area is ForeignObjectArea))
			{
				throw new ApocException("XML not in fo:instream-foreign-object");
			}
			return new Status(1);
		}

		// Token: 0x0600D970 RID: 55664 RVA: 0x002FB6D5 File Offset: 0x002F98D5
		private void Init()
		{
			base.CreateBasicDocument();
		}

		// Token: 0x0600D971 RID: 55665 RVA: 0x002FB6DE File Offset: 0x002F98DE
		public override string GetNameSpace()
		{
			return this.nmspace;
		}

		// Token: 0x04003C1E RID: 15390
		private string nmspace = string.Empty;

		// Token: 0x020015CA RID: 5578
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D972 RID: 55666 RVA: 0x002FB6E6 File Offset: 0x002F98E6
			internal Maker(string t)
			{
				this.tag = t;
			}

			// Token: 0x0600D973 RID: 55667 RVA: 0x002FB6F5 File Offset: 0x002F98F5
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new XMLElement(parent, propertyList, this.tag);
			}

			// Token: 0x04003C1F RID: 15391
			private string tag;
		}
	}
}
