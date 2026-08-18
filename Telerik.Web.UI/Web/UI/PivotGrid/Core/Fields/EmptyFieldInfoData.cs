using System;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB1 RID: 3249
	internal class EmptyFieldInfoData : IFieldInfoData
	{
		// Token: 0x060079A3 RID: 31139 RVA: 0x001BF04C File Offset: 0x001BD24C
		public EmptyFieldInfoData()
		{
			this.rootFieldInfo = ContainerNode.CreateRootNode();
		}

		// Token: 0x17002730 RID: 10032
		// (get) Token: 0x060079A4 RID: 31140 RVA: 0x001BF05F File Offset: 0x001BD25F
		public ContainerNode RootFieldInfo
		{
			get
			{
				return this.rootFieldInfo;
			}
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x001BF067 File Offset: 0x001BD267
		public IPivotFieldInfo GetFieldDescriptionByMember(string name)
		{
			return null;
		}

		// Token: 0x04002147 RID: 8519
		private readonly ContainerNode rootFieldInfo;
	}
}
