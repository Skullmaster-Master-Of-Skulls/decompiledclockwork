using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x02001433 RID: 5171
	internal abstract class Region : FObj
	{
		// Token: 0x0600D34E RID: 54094 RVA: 0x002EEC0C File Offset: 0x002ECE0C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected Region(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = this.GetElementName();
			if (this.properties.GetProperty("region-name") == null)
			{
				this.setRegionName(this.GetDefaultRegionName());
			}
			else if (string.IsNullOrEmpty(this.properties.GetProperty("region-name").GetString()))
			{
				this.setRegionName(this.GetDefaultRegionName());
			}
			else
			{
				this.setRegionName(this.properties.GetProperty("region-name").GetString());
				if (this.isReserved(this.getRegionName()) && !this.getRegionName().Equals(this.GetDefaultRegionName()))
				{
					throw new ApocException(string.Concat(new string[]
					{
						"region-name '",
						this._regionName,
						"' for ",
						this.name,
						" not permitted."
					}));
				}
			}
			if (parent.GetName().Equals("fo:simple-page-master"))
			{
				this._layoutMaster = (SimplePageMaster)parent;
				this.getPageMaster().addRegion(this);
				return;
			}
			throw new ApocException(this.GetElementName() + " must be child of simple-page-master, not " + parent.GetName());
		}

		// Token: 0x0600D34F RID: 54095
		public abstract RegionArea MakeRegionArea(int allocationRectangleXPosition, int allocationRectangleYPosition, int allocationRectangleWidth, int allocationRectangleHeight);

		// Token: 0x0600D350 RID: 54096
		protected abstract string GetDefaultRegionName();

		// Token: 0x0600D351 RID: 54097
		protected abstract string GetElementName();

		// Token: 0x0600D352 RID: 54098
		public abstract string GetRegionClass();

		// Token: 0x0600D353 RID: 54099 RVA: 0x002EED39 File Offset: 0x002ECF39
		public string getRegionName()
		{
			return this._regionName;
		}

		// Token: 0x0600D354 RID: 54100 RVA: 0x002EED41 File Offset: 0x002ECF41
		private void setRegionName(string name)
		{
			this._regionName = name;
		}

		// Token: 0x0600D355 RID: 54101 RVA: 0x002EED4A File Offset: 0x002ECF4A
		protected SimplePageMaster getPageMaster()
		{
			return this._layoutMaster;
		}

		// Token: 0x0600D356 RID: 54102 RVA: 0x002EED54 File Offset: 0x002ECF54
		protected bool isReserved(string name)
		{
			return name.Equals("xsl-region-before") || name.Equals("xsl-region-start") || name.Equals("xsl-region-end") || name.Equals("xsl-region-after") || name.Equals("xsl-before-float-separator") || name.Equals("xsl-footnote-separator");
		}

		// Token: 0x0600D357 RID: 54103 RVA: 0x002EEDAF File Offset: 0x002ECFAF
		public override bool GeneratesReferenceAreas()
		{
			return true;
		}

		// Token: 0x04003956 RID: 14678
		public const string PROP_REGION_NAME = "region-name";

		// Token: 0x04003957 RID: 14679
		private SimplePageMaster _layoutMaster;

		// Token: 0x04003958 RID: 14680
		private string _regionName;
	}
}
