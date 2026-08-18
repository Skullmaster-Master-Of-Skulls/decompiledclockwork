using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001440 RID: 5184
	internal class RepeatablePageMasterReference : PageMasterReference, SubSequenceSpecifier
	{
		// Token: 0x0600D38E RID: 54158 RVA: 0x002EF3F9 File Offset: 0x002ED5F9
		public new static FObj.Maker GetMaker()
		{
			return new RepeatablePageMasterReference.Maker();
		}

		// Token: 0x0600D38F RID: 54159 RVA: 0x002EF400 File Offset: 0x002ED600
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RepeatablePageMasterReference(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
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

		// Token: 0x0600D390 RID: 54160 RVA: 0x002EF468 File Offset: 0x002ED668
		public override string GetNextPageMaster(int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
		{
			string result = base.MasterName;
			if (this.getMaximumRepeats() != -1)
			{
				if (this.numberConsumed < this.getMaximumRepeats())
				{
					this.numberConsumed++;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600D391 RID: 54161 RVA: 0x002EF4A6 File Offset: 0x002ED6A6
		private void setMaximumRepeats(int maximumRepeats)
		{
			if (maximumRepeats == -1)
			{
				this.maximumRepeats = maximumRepeats;
				return;
			}
			this.maximumRepeats = ((maximumRepeats < 0) ? 0 : maximumRepeats);
		}

		// Token: 0x0600D392 RID: 54162 RVA: 0x002EF4C2 File Offset: 0x002ED6C2
		private int getMaximumRepeats()
		{
			return this.maximumRepeats;
		}

		// Token: 0x0600D393 RID: 54163 RVA: 0x002EF4CA File Offset: 0x002ED6CA
		protected override string GetElementName()
		{
			return "fo:repeatable-page-master-reference";
		}

		// Token: 0x0600D394 RID: 54164 RVA: 0x002EF4D1 File Offset: 0x002ED6D1
		public override void Reset()
		{
			this.numberConsumed = 0;
		}

		// Token: 0x04003965 RID: 14693
		private const int INFINITE = -1;

		// Token: 0x04003966 RID: 14694
		private int maximumRepeats;

		// Token: 0x04003967 RID: 14695
		private int numberConsumed;

		// Token: 0x02001441 RID: 5185
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D395 RID: 54165 RVA: 0x002EF4DA File Offset: 0x002ED6DA
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new RepeatablePageMasterReference(parent, propertyList);
			}
		}
	}
}
