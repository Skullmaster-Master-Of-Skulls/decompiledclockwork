using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001428 RID: 5160
	internal class ConditionalPageMasterReference : FObj
	{
		// Token: 0x0600D304 RID: 54020 RVA: 0x002ED501 File Offset: 0x002EB701
		public new static FObj.Maker GetMaker()
		{
			return new ConditionalPageMasterReference.Maker();
		}

		// Token: 0x0600D305 RID: 54021 RVA: 0x002ED508 File Offset: 0x002EB708
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ConditionalPageMasterReference(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = this.GetElementName();
			if (this.GetProperty("master-reference") != null)
			{
				this.SetMasterName(this.GetProperty("master-reference").GetString());
			}
			this.validateParent(parent);
			this.setPagePosition(this.properties.GetProperty("page-position").GetEnum());
			this.setOddOrEven(this.properties.GetProperty("odd-or-even").GetEnum());
			this.setBlankOrNotBlank(this.properties.GetProperty("blank-or-not-blank").GetEnum());
		}

		// Token: 0x0600D306 RID: 54022 RVA: 0x002ED5A4 File Offset: 0x002EB7A4
		protected internal void SetMasterName(string masterName)
		{
			this.masterName = masterName;
		}

		// Token: 0x0600D307 RID: 54023 RVA: 0x002ED5AD File Offset: 0x002EB7AD
		public string GetMasterName()
		{
			return this.masterName;
		}

		// Token: 0x0600D308 RID: 54024 RVA: 0x002ED5B8 File Offset: 0x002EB7B8
		protected internal bool isValid(int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
		{
			bool flag = true;
			int num = this.getPagePosition();
			if (num <= 29)
			{
				if (num != 6)
				{
					if (num == 29)
					{
						if (!thisIsFirstPage)
						{
							flag = false;
						}
					}
				}
				else
				{
					flag = true;
				}
			}
			else if (num != 38)
			{
				if (num == 62)
				{
					if (thisIsFirstPage)
					{
						flag = false;
					}
				}
			}
			else
			{
				ApocDriver.ActiveDriver.FireApocInfo("Last page position not known");
				flag = true;
			}
			bool flag2 = true;
			int num2 = this.getOddOrEven();
			bool flag3 = currentPageNumber % 2 == 1;
			if (54 == num2 && !flag3)
			{
				flag2 = false;
			}
			if (25 == num2 && flag3)
			{
				flag2 = false;
			}
			bool flag4 = true;
			int num3 = this.getBlankOrNotBlank();
			if (10 == num3 && !isEmptyPage)
			{
				flag4 = false;
			}
			else if (53 == num3 && isEmptyPage)
			{
				flag4 = false;
			}
			return flag2 && flag && flag4;
		}

		// Token: 0x0600D309 RID: 54025 RVA: 0x002ED66A File Offset: 0x002EB86A
		protected internal void setPagePosition(int pagePosition)
		{
			this.pagePosition = pagePosition;
		}

		// Token: 0x0600D30A RID: 54026 RVA: 0x002ED673 File Offset: 0x002EB873
		protected internal int getPagePosition()
		{
			return this.pagePosition;
		}

		// Token: 0x0600D30B RID: 54027 RVA: 0x002ED67B File Offset: 0x002EB87B
		protected internal void setOddOrEven(int oddOrEven)
		{
			this.oddOrEven = oddOrEven;
		}

		// Token: 0x0600D30C RID: 54028 RVA: 0x002ED684 File Offset: 0x002EB884
		protected internal int getOddOrEven()
		{
			return this.oddOrEven;
		}

		// Token: 0x0600D30D RID: 54029 RVA: 0x002ED68C File Offset: 0x002EB88C
		protected internal void setBlankOrNotBlank(int blankOrNotBlank)
		{
			this.blankOrNotBlank = blankOrNotBlank;
		}

		// Token: 0x0600D30E RID: 54030 RVA: 0x002ED695 File Offset: 0x002EB895
		protected internal int getBlankOrNotBlank()
		{
			return this.blankOrNotBlank;
		}

		// Token: 0x0600D30F RID: 54031 RVA: 0x002ED69D File Offset: 0x002EB89D
		protected internal string GetElementName()
		{
			return "fo:conditional-page-master-reference";
		}

		// Token: 0x0600D310 RID: 54032 RVA: 0x002ED6A4 File Offset: 0x002EB8A4
		protected internal void validateParent(FObj parent)
		{
			if (!parent.GetName().Equals("fo:repeatable-page-master-alternatives"))
			{
				throw new ApocException("fo:conditional-page-master-reference must be child of fo:repeatable-page-master-alternatives, not " + parent.GetName());
			}
			this.repeatablePageMasterAlternatives = (RepeatablePageMasterAlternatives)parent;
			if (this.GetMasterName() == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning("single-page-master-referencedoes not have a master-reference and so is being ignored");
				return;
			}
			this.repeatablePageMasterAlternatives.addConditionalPageMasterReference(this);
		}

		// Token: 0x04003928 RID: 14632
		private RepeatablePageMasterAlternatives repeatablePageMasterAlternatives;

		// Token: 0x04003929 RID: 14633
		private string masterName;

		// Token: 0x0400392A RID: 14634
		private int pagePosition;

		// Token: 0x0400392B RID: 14635
		private int oddOrEven;

		// Token: 0x0400392C RID: 14636
		private int blankOrNotBlank;

		// Token: 0x02001429 RID: 5161
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D311 RID: 54033 RVA: 0x002ED709 File Offset: 0x002EB909
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new ConditionalPageMasterReference(parent, propertyList);
			}
		}
	}
}
