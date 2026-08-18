using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200143E RID: 5182
	internal class RepeatablePageMasterAlternatives : FObj, SubSequenceSpecifier
	{
		// Token: 0x0600D384 RID: 54148 RVA: 0x002EF248 File Offset: 0x002ED448
		public new static FObj.Maker GetMaker()
		{
			return new RepeatablePageMasterAlternatives.Maker();
		}

		// Token: 0x0600D385 RID: 54149 RVA: 0x002EF250 File Offset: 0x002ED450
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RepeatablePageMasterAlternatives(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:repeatable-page-master-alternatives";
			this.conditionalPageMasterRefs = new ArrayList();
			if (!parent.GetName().Equals("fo:page-sequence-master"))
			{
				throw new ApocException("fo:repeatable-page-master-alternativesmust be child of fo:page-sequence-master, not " + parent.GetName());
			}
			this.pageSequenceMaster = (PageSequenceMaster)parent;
			this.pageSequenceMaster.AddSubsequenceSpecifier(this);
			string @string = this.GetProperty("maximum-repeats").GetString();
			if (@string.Equals("no-limit"))
			{
				this.setMaximumRepeats(-1);
				return;
			}
			try
			{
				this.setMaximumRepeats(int.Parse(@string));
			}
			catch (FormatException)
			{
				throw new ApocException("Invalid number for 'maximum-repeats' property");
			}
		}

		// Token: 0x0600D386 RID: 54150 RVA: 0x002EF310 File Offset: 0x002ED510
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public string GetNextPageMaster(int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
		{
			string result = null;
			if (this.getMaximumRepeats() != -1)
			{
				if (this.numberConsumed >= this.getMaximumRepeats())
				{
					return null;
				}
				this.numberConsumed++;
			}
			foreach (object obj in this.conditionalPageMasterRefs)
			{
				ConditionalPageMasterReference conditionalPageMasterReference = (ConditionalPageMasterReference)obj;
				if (conditionalPageMasterReference.isValid(currentPageNumber + 1, thisIsFirstPage, isEmptyPage))
				{
					result = conditionalPageMasterReference.GetMasterName();
					break;
				}
			}
			return result;
		}

		// Token: 0x0600D387 RID: 54151 RVA: 0x002EF3A4 File Offset: 0x002ED5A4
		private void setMaximumRepeats(int maximumRepeats)
		{
			if (maximumRepeats == -1)
			{
				this.maximumRepeats = maximumRepeats;
				return;
			}
			this.maximumRepeats = ((maximumRepeats < 0) ? 0 : maximumRepeats);
		}

		// Token: 0x0600D388 RID: 54152 RVA: 0x002EF3C0 File Offset: 0x002ED5C0
		private int getMaximumRepeats()
		{
			return this.maximumRepeats;
		}

		// Token: 0x0600D389 RID: 54153 RVA: 0x002EF3C8 File Offset: 0x002ED5C8
		public void addConditionalPageMasterReference(ConditionalPageMasterReference cpmr)
		{
			this.conditionalPageMasterRefs.Add(cpmr);
		}

		// Token: 0x0600D38A RID: 54154 RVA: 0x002EF3D7 File Offset: 0x002ED5D7
		public void Reset()
		{
			this.numberConsumed = 0;
		}

		// Token: 0x0600D38B RID: 54155 RVA: 0x002EF3E0 File Offset: 0x002ED5E0
		protected PageSequenceMaster getPageSequenceMaster()
		{
			return this.pageSequenceMaster;
		}

		// Token: 0x04003960 RID: 14688
		private const int INFINITE = -1;

		// Token: 0x04003961 RID: 14689
		private PageSequenceMaster pageSequenceMaster;

		// Token: 0x04003962 RID: 14690
		private int maximumRepeats;

		// Token: 0x04003963 RID: 14691
		private int numberConsumed;

		// Token: 0x04003964 RID: 14692
		private ArrayList conditionalPageMasterRefs;

		// Token: 0x0200143F RID: 5183
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D38C RID: 54156 RVA: 0x002EF3E8 File Offset: 0x002ED5E8
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RepeatablePageMasterAlternatives(parent, propertyList);
			}
		}
	}
}
