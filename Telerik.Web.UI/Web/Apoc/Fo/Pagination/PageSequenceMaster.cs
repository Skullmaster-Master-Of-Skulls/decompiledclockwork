using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001431 RID: 5169
	internal class PageSequenceMaster : FObj
	{
		// Token: 0x0600D346 RID: 54086 RVA: 0x002EEAC5 File Offset: 0x002ECCC5
		public new static FObj.Maker GetMaker()
		{
			return new PageSequenceMaster.Maker();
		}

		// Token: 0x0600D347 RID: 54087 RVA: 0x002EEACC File Offset: 0x002ECCCC
		protected PageSequenceMaster(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:page-sequence-master";
			this.subSequenceSpecifiers = new ArrayList();
			if (!parent.GetName().Equals("fo:layout-master-set"))
			{
				throw new ApocException("fo:page-sequence-master must be child of fo:layout-master-set, not " + parent.GetName());
			}
			this.layoutMasterSet = (LayoutMasterSet)parent;
			string @string = this.properties.GetProperty("master-name").GetString();
			if (@string == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning("page-sequence-master does not have a page-master-name and so is being ignored");
				return;
			}
			this.layoutMasterSet.addPageSequenceMaster(@string, this);
		}

		// Token: 0x0600D348 RID: 54088 RVA: 0x002EEB61 File Offset: 0x002ECD61
		protected internal void AddSubsequenceSpecifier(SubSequenceSpecifier pageMasterReference)
		{
			this.subSequenceSpecifiers.Add(pageMasterReference);
		}

		// Token: 0x0600D349 RID: 54089 RVA: 0x002EEB70 File Offset: 0x002ECD70
		protected internal SubSequenceSpecifier getSubSequenceSpecifier(int sequenceNumber)
		{
			if (sequenceNumber >= 0 && sequenceNumber < this.GetSubSequenceSpecifierCount())
			{
				return (SubSequenceSpecifier)this.subSequenceSpecifiers[sequenceNumber];
			}
			return null;
		}

		// Token: 0x0600D34A RID: 54090 RVA: 0x002EEB92 File Offset: 0x002ECD92
		protected internal int GetSubSequenceSpecifierCount()
		{
			return this.subSequenceSpecifiers.Count;
		}

		// Token: 0x0600D34B RID: 54091 RVA: 0x002EEBA0 File Offset: 0x002ECDA0
		public void Reset()
		{
			foreach (object obj in this.subSequenceSpecifiers)
			{
				SubSequenceSpecifier subSequenceSpecifier = (SubSequenceSpecifier)obj;
				subSequenceSpecifier.Reset();
			}
		}

		// Token: 0x04003954 RID: 14676
		private LayoutMasterSet layoutMasterSet;

		// Token: 0x04003955 RID: 14677
		private ArrayList subSequenceSpecifiers;

		// Token: 0x02001432 RID: 5170
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D34C RID: 54092 RVA: 0x002EEBF8 File Offset: 0x002ECDF8
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new PageSequenceMaster(parent, propertyList);
			}
		}
	}
}
