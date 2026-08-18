using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200142A RID: 5162
	internal class LayoutMasterSet : FObj
	{
		// Token: 0x0600D313 RID: 54035 RVA: 0x002ED71A File Offset: 0x002EB91A
		public new static FObj.Maker GetMaker()
		{
			return new LayoutMasterSet.Maker();
		}

		// Token: 0x0600D314 RID: 54036 RVA: 0x002ED724 File Offset: 0x002EB924
		protected internal LayoutMasterSet(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:layout-master-set";
			this.simplePageMasters = new Hashtable();
			this.pageSequenceMasters = new Hashtable();
			if (parent.GetName().Equals("fo:root"))
			{
				this.root = (Root)parent;
				this.root.setLayoutMasterSet(this);
				this.allRegions = new Hashtable();
				return;
			}
			throw new ApocException("fo:layout-master-set must be child of fo:root, not " + parent.GetName());
		}

		// Token: 0x0600D315 RID: 54037 RVA: 0x002ED7A7 File Offset: 0x002EB9A7
		protected internal void addSimplePageMaster(SimplePageMaster simplePageMaster)
		{
			if (this.existsName(simplePageMaster.GetMasterName()))
			{
				throw new ApocException("'master-name' (" + simplePageMaster.GetMasterName() + ") must be unique across page-masters and page-sequence-masters");
			}
			this.simplePageMasters.Add(simplePageMaster.GetMasterName(), simplePageMaster);
		}

		// Token: 0x0600D316 RID: 54038 RVA: 0x002ED7E4 File Offset: 0x002EB9E4
		protected internal SimplePageMaster getSimplePageMaster(string masterName)
		{
			return (SimplePageMaster)this.simplePageMasters[masterName];
		}

		// Token: 0x0600D317 RID: 54039 RVA: 0x002ED7F7 File Offset: 0x002EB9F7
		protected internal void addPageSequenceMaster(string masterName, PageSequenceMaster pageSequenceMaster)
		{
			if (this.existsName(masterName))
			{
				throw new ApocException("'master-name' (" + masterName + ") must be unique across page-masters and page-sequence-masters");
			}
			this.pageSequenceMasters.Add(masterName, pageSequenceMaster);
		}

		// Token: 0x0600D318 RID: 54040 RVA: 0x002ED825 File Offset: 0x002EBA25
		protected internal PageSequenceMaster getPageSequenceMaster(string masterName)
		{
			return (PageSequenceMaster)this.pageSequenceMasters[masterName];
		}

		// Token: 0x0600D319 RID: 54041 RVA: 0x002ED838 File Offset: 0x002EBA38
		private bool existsName(string masterName)
		{
			return this.simplePageMasters.ContainsKey(masterName) || this.pageSequenceMasters.ContainsKey(masterName);
		}

		// Token: 0x0600D31A RID: 54042 RVA: 0x002ED85C File Offset: 0x002EBA5C
		protected internal void resetPageMasters()
		{
			foreach (object obj in this.pageSequenceMasters.Values)
			{
				PageSequenceMaster pageSequenceMaster = (PageSequenceMaster)obj;
				pageSequenceMaster.Reset();
			}
		}

		// Token: 0x0600D31B RID: 54043 RVA: 0x002ED8BC File Offset: 0x002EBABC
		protected internal void checkRegionNames()
		{
			foreach (object obj in this.simplePageMasters.Values)
			{
				SimplePageMaster simplePageMaster = (SimplePageMaster)obj;
				foreach (object obj2 in simplePageMaster.getRegions().Values)
				{
					Region region = (Region)obj2;
					if (this.allRegions.ContainsKey(region.getRegionName()))
					{
						string text = (string)this.allRegions[region.getRegionName()];
						if (!text.Equals(region.GetRegionClass()))
						{
							throw new ApocException(string.Concat(new string[]
							{
								"Duplicate region-names (",
								region.getRegionName(),
								") must map to the same region-class (",
								text,
								"!=",
								region.GetRegionClass(),
								")"
							}));
						}
					}
					this.allRegions[region.getRegionName()] = region.GetRegionClass();
				}
			}
		}

		// Token: 0x0600D31C RID: 54044 RVA: 0x002EDA28 File Offset: 0x002EBC28
		protected internal bool regionNameExists(string regionName)
		{
			bool flag = false;
			foreach (object obj in this.simplePageMasters.Values)
			{
				SimplePageMaster simplePageMaster = (SimplePageMaster)obj;
				flag = simplePageMaster.regionNameExists(regionName);
				if (flag)
				{
					return flag;
				}
			}
			return flag;
		}

		// Token: 0x0400392D RID: 14637
		private Hashtable simplePageMasters;

		// Token: 0x0400392E RID: 14638
		private Hashtable pageSequenceMasters;

		// Token: 0x0400392F RID: 14639
		private Hashtable allRegions;

		// Token: 0x04003930 RID: 14640
		private Root root;

		// Token: 0x0200142B RID: 5163
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D31D RID: 54045 RVA: 0x002EDA98 File Offset: 0x002EBC98
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new LayoutMasterSet(parent, propertyList);
			}
		}
	}
}
