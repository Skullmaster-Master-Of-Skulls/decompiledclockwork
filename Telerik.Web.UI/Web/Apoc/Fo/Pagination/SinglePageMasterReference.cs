using System;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001446 RID: 5190
	internal class SinglePageMasterReference : PageMasterReference, SubSequenceSpecifier
	{
		// Token: 0x0600D3AD RID: 54189 RVA: 0x002EF991 File Offset: 0x002EDB91
		public new static FObj.Maker GetMaker()
		{
			return new SinglePageMasterReference.Maker();
		}

		// Token: 0x0600D3AE RID: 54190 RVA: 0x002EF998 File Offset: 0x002EDB98
		public SinglePageMasterReference(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.state = 0;
		}

		// Token: 0x0600D3AF RID: 54191 RVA: 0x002EF9A9 File Offset: 0x002EDBA9
		public override string GetNextPageMaster(int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
		{
			if (this.state == 0)
			{
				this.state = 1;
				return base.MasterName;
			}
			return null;
		}

		// Token: 0x0600D3B0 RID: 54192 RVA: 0x002EF9C2 File Offset: 0x002EDBC2
		public override void Reset()
		{
			this.state = 0;
		}

		// Token: 0x0600D3B1 RID: 54193 RVA: 0x002EF9CB File Offset: 0x002EDBCB
		protected override string GetElementName()
		{
			return "fo:single-page-master-reference";
		}

		// Token: 0x04003973 RID: 14707
		private const int FIRST = 0;

		// Token: 0x04003974 RID: 14708
		private const int DONE = 1;

		// Token: 0x04003975 RID: 14709
		private int state;

		// Token: 0x02001447 RID: 5191
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D3B2 RID: 54194 RVA: 0x002EF9D2 File Offset: 0x002EDBD2
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new SinglePageMasterReference(parent, propertyList);
			}
		}
	}
}
