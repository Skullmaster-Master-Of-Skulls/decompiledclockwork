using System;
using BinaryComponents.SuperList;
using BinaryComponents.SuperList.Sections;

namespace ClockWorkAPI
{
	// Token: 0x020000A6 RID: 166
	public class MySectionFactory : SectionFactory
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00031C68 File Offset: 0x00030C68
		public override RowSection CreateRowSection(ListControl listControl, RowIdentifier rowIdentifier, HeaderSection headerSection, int position)
		{
			return new MyRowSection(listControl, rowIdentifier, headerSection, position);
		}
	}
}
