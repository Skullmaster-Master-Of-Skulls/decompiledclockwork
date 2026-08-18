using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.Extensions
{
	// Token: 0x02001397 RID: 5015
	internal class Label : ExtensionObj
	{
		// Token: 0x0600D0EF RID: 53487 RVA: 0x002E3F89 File Offset: 0x002E2189
		public new static FObj.Maker GetMaker()
		{
			return new Label.Maker();
		}

		// Token: 0x0600D0F0 RID: 53488 RVA: 0x002E3F90 File Offset: 0x002E2190
		public Label(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
		}

		// Token: 0x0600D0F1 RID: 53489 RVA: 0x002E3FA5 File Offset: 0x002E21A5
		protected internal override void AddCharacters(char[] data, int start, int end)
		{
			this.label += new string(data, start, end - start);
		}

		// Token: 0x0600D0F2 RID: 53490 RVA: 0x002E3FC2 File Offset: 0x002E21C2
		public string toString()
		{
			return this.label;
		}

		// Token: 0x04003814 RID: 14356
		private string label = "";

		// Token: 0x02001398 RID: 5016
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D0F3 RID: 53491 RVA: 0x002E3FCA File Offset: 0x002E21CA
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Label(parent, propertyList);
			}
		}
	}
}
