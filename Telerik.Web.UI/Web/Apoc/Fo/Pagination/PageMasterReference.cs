using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200142D RID: 5165
	internal abstract class PageMasterReference : FObj, SubSequenceSpecifier
	{
		// Token: 0x0600D321 RID: 54049 RVA: 0x002EDAA9 File Offset: 0x002EBCA9
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public PageMasterReference(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = this.GetElementName();
			if (this.GetProperty("master-reference") != null)
			{
				this.SetMasterName(this.GetProperty("master-reference").GetString());
			}
			this.validateParent(parent);
		}

		// Token: 0x0600D322 RID: 54050 RVA: 0x002EDAE9 File Offset: 0x002EBCE9
		protected void SetMasterName(string masterName)
		{
			this._masterName = masterName;
		}

		// Token: 0x170042F0 RID: 17136
		// (get) Token: 0x0600D323 RID: 54051 RVA: 0x002EDAF2 File Offset: 0x002EBCF2
		public string MasterName
		{
			get
			{
				return this._masterName;
			}
		}

		// Token: 0x170042F1 RID: 17137
		// (get) Token: 0x0600D324 RID: 54052 RVA: 0x002EDAFA File Offset: 0x002EBCFA
		// (set) Token: 0x0600D325 RID: 54053 RVA: 0x002EDB02 File Offset: 0x002EBD02
		protected PageSequenceMaster PageSequenceMaster
		{
			get
			{
				return this._pageSequenceMaster;
			}
			set
			{
				this._pageSequenceMaster = value;
			}
		}

		// Token: 0x0600D326 RID: 54054
		public abstract string GetNextPageMaster(int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage);

		// Token: 0x0600D327 RID: 54055
		protected abstract string GetElementName();

		// Token: 0x0600D328 RID: 54056 RVA: 0x002EDB0C File Offset: 0x002EBD0C
		protected void validateParent(FObj parent)
		{
			if (!parent.GetName().Equals("fo:page-sequence-master"))
			{
				throw new ApocException(this.GetElementName() + " must bechild of fo:page-sequence-master, not " + parent.GetName());
			}
			this._pageSequenceMaster = (PageSequenceMaster)parent;
			if (this.MasterName == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning(this.GetElementName() + " does not have a master-reference and so is being ignored");
				return;
			}
			this._pageSequenceMaster.AddSubsequenceSpecifier(this);
		}

		// Token: 0x0600D329 RID: 54057
		public abstract void Reset();

		// Token: 0x04003931 RID: 14641
		private string _masterName;

		// Token: 0x04003932 RID: 14642
		private PageSequenceMaster _pageSequenceMaster;
	}
}
