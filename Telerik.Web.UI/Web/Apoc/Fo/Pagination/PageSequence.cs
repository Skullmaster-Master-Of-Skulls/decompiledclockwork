using System;
using System.Collections;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Pagination
{
	// Token: 0x0200142F RID: 5167
	internal class PageSequence : FObj
	{
		// Token: 0x0600D32E RID: 54062 RVA: 0x002EDEDA File Offset: 0x002EC0DA
		public new static FObj.Maker GetMaker()
		{
			return new PageSequence.Maker();
		}

		// Token: 0x0600D32F RID: 54063 RVA: 0x002EDEE4 File Offset: 0x002EC0E4
		protected PageSequence(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:page-sequence";
			if (parent.GetName().Equals("fo:root"))
			{
				this.root = (Root)parent;
				this.layoutMasterSet = this.root.getLayoutMasterSet();
				this.layoutMasterSet.checkRegionNames();
				this._flowMap = new Hashtable();
				this.thisIsFirstPage = true;
				this.ipnValue = this.properties.GetProperty("initial-page-number").GetString();
				if (this.ipnValue.Equals("auto"))
				{
					this.pageNumberType = 1;
				}
				else if (this.ipnValue.Equals("auto-even"))
				{
					this.pageNumberType = 2;
				}
				else if (this.ipnValue.Equals("auto-odd"))
				{
					this.pageNumberType = 3;
				}
				else
				{
					this.pageNumberType = 0;
					try
					{
						int num = int.Parse(this.ipnValue);
						this.currentPageNumber = ((num > 0) ? (num - 1) : 0);
					}
					catch (FormatException)
					{
						throw new ApocException("\"" + this.ipnValue + "\" is not a valid value for initial-page-number");
					}
				}
				this.masterName = this.properties.GetProperty("master-reference").GetString();
				this.pageNumberGenerator = new PageNumberGenerator(this.properties.GetProperty("format").GetString(), this.properties.GetProperty("grouping-separator").GetCharacter(), this.properties.GetProperty("grouping-size").GetNumber().IntValue(), this.properties.GetProperty("letter-value").GetEnum());
				this.forcePageCount = this.properties.GetProperty("force-page-count").GetEnum();
				return;
			}
			throw new ApocException("page-sequence must be child of root, not " + parent.GetName());
		}

		// Token: 0x0600D330 RID: 54064 RVA: 0x002EE0C8 File Offset: 0x002EC2C8
		public void AddFlow(Flow flow)
		{
			if (this._flowMap.ContainsKey(flow.GetFlowName()))
			{
				throw new ApocException("flow-names must be unique within an fo:page-sequence");
			}
			if (!this.layoutMasterSet.regionNameExists(flow.GetFlowName()))
			{
				ApocDriver.ActiveDriver.FireApocError("region-name '" + flow.GetFlowName() + "' doesn't exist in the layout-master-set.");
			}
			this._flowMap.Add(flow.GetFlowName(), flow);
			this.IsFlowSet = true;
		}

		// Token: 0x0600D331 RID: 54065 RVA: 0x002EE140 File Offset: 0x002EC340
		public void Format(AreaTree areaTree)
		{
			Status status = new Status(1);
			this.layoutMasterSet.resetPageMasters();
			int runningPageNumberCounter;
			for (;;)
			{
				runningPageNumberCounter = this.root.getRunningPageNumberCounter();
				bool isFirstPage = false;
				if (this.thisIsFirstPage)
				{
					isFirstPage = this.thisIsFirstPage;
					if (this.pageNumberType == 1)
					{
						this.currentPageNumber = this.root.getRunningPageNumberCounter();
					}
					else if (this.pageNumberType == 3)
					{
						this.currentPageNumber = this.root.getRunningPageNumberCounter();
						if (this.currentPageNumber % 2 == 1)
						{
							this.currentPageNumber++;
						}
					}
					else if (this.pageNumberType == 2)
					{
						this.currentPageNumber = this.root.getRunningPageNumberCounter();
						if (this.currentPageNumber % 2 == 0)
						{
							this.currentPageNumber++;
						}
					}
					this.thisIsFirstPage = false;
				}
				this.currentPageNumber++;
				bool isEmptyPage = (status.getCode() == 5 && this.currentPageNumber % 2 == 1) || (status.getCode() == 6 && this.currentPageNumber % 2 == 0);
				this.currentPage = this.MakePage(areaTree, runningPageNumberCounter, isFirstPage, isEmptyPage);
				this.currentPage.setNumber(this.currentPageNumber);
				string formattedNumber = this.pageNumberGenerator.makeFormattedPageNumber(this.currentPageNumber);
				this.currentPage.setFormattedNumber(formattedNumber);
				this.root.setRunningPageNumberCounter(this.currentPageNumber);
				ApocDriver.ActiveDriver.FireApocInfo("[" + this.currentPageNumber + "]");
				if ((status.getCode() != 5 || this.currentPageNumber % 2 != 1) && (status.getCode() != 6 || this.currentPageNumber % 2 != 0))
				{
					BodyAreaContainer body = this.currentPage.getBody();
					body.setIDReferences(areaTree.getIDReferences());
					Flow currentFlow = this.GetCurrentFlow("body");
					if (currentFlow == null)
					{
						break;
					}
					status = currentFlow.Layout(body);
				}
				this.currentPage.setPageSequence(this);
				this.FormatStaticContent(areaTree);
				areaTree.addPage(this.currentPage);
				this.pageCount++;
				if (!this.FlowsAreIncomplete())
				{
					goto IL_22F;
				}
			}
			ApocDriver.ActiveDriver.FireApocError("No flow found for region-body in page-master '" + this.currentPageMasterName + "'");
			IL_22F:
			this.ForcePage(areaTree, runningPageNumberCounter);
			this.currentPage = null;
		}

		// Token: 0x0600D332 RID: 54066 RVA: 0x002EE38C File Offset: 0x002EC58C
		private Page MakePage(AreaTree areaTree, int firstAvailPageNumber, bool isFirstPage, bool isEmptyPage)
		{
			PageMaster nextPageMaster = this.GetNextPageMaster(this.masterName, firstAvailPageNumber, isFirstPage, isEmptyPage);
			if (nextPageMaster == null)
			{
				throw new ApocException("page masters exhausted. Cannot recover.");
			}
			Page page = nextPageMaster.makePage(areaTree);
			if (this.currentPage != null)
			{
				ArrayList pendingFootnotes = this.currentPage.getPendingFootnotes();
				page.setPendingFootnotes(pendingFootnotes);
			}
			return page;
		}

		// Token: 0x0600D333 RID: 54067 RVA: 0x002EE3DC File Offset: 0x002EC5DC
		private void FormatStaticContent(AreaTree areaTree)
		{
			SimplePageMaster currentSimplePageMaster = this.GetCurrentSimplePageMaster();
			if (currentSimplePageMaster.getRegion("before") != null && this.currentPage.getBefore() != null)
			{
				Flow flow = (Flow)this._flowMap[currentSimplePageMaster.getRegion("before").getRegionName()];
				if (flow != null)
				{
					AreaContainer before = this.currentPage.getBefore();
					before.setIDReferences(areaTree.getIDReferences());
					this.LayoutStaticContent(flow, currentSimplePageMaster.getRegion("before"), before);
				}
			}
			if (currentSimplePageMaster.getRegion("after") != null && this.currentPage.getAfter() != null)
			{
				Flow flow2 = (Flow)this._flowMap[currentSimplePageMaster.getRegion("after").getRegionName()];
				if (flow2 != null)
				{
					AreaContainer after = this.currentPage.getAfter();
					after.setIDReferences(areaTree.getIDReferences());
					this.LayoutStaticContent(flow2, currentSimplePageMaster.getRegion("after"), after);
				}
			}
			if (currentSimplePageMaster.getRegion("start") != null && this.currentPage.getStart() != null)
			{
				Flow flow3 = (Flow)this._flowMap[currentSimplePageMaster.getRegion("start").getRegionName()];
				if (flow3 != null)
				{
					AreaContainer start = this.currentPage.getStart();
					start.setIDReferences(areaTree.getIDReferences());
					this.LayoutStaticContent(flow3, currentSimplePageMaster.getRegion("start"), start);
				}
			}
			if (currentSimplePageMaster.getRegion("end") != null && this.currentPage.getEnd() != null)
			{
				Flow flow4 = (Flow)this._flowMap[currentSimplePageMaster.getRegion("end").getRegionName()];
				if (flow4 != null)
				{
					AreaContainer end = this.currentPage.getEnd();
					end.setIDReferences(areaTree.getIDReferences());
					this.LayoutStaticContent(flow4, currentSimplePageMaster.getRegion("end"), end);
				}
			}
		}

		// Token: 0x0600D334 RID: 54068 RVA: 0x002EE5A4 File Offset: 0x002EC7A4
		private void LayoutStaticContent(Flow flow, Region region, AreaContainer area)
		{
			StaticContent staticContent = flow as StaticContent;
			if (staticContent != null)
			{
				staticContent.Layout(area, region);
				return;
			}
			ApocDriver.ActiveDriver.FireApocError(region.GetName() + " only supports static-content flows currently. Cannot use flow named '" + flow.GetFlowName() + "'");
		}

		// Token: 0x0600D335 RID: 54069 RVA: 0x002EE5EA File Offset: 0x002EC7EA
		private SubSequenceSpecifier GetNextSubsequence(PageSequenceMaster master)
		{
			if (master.GetSubSequenceSpecifierCount() > this.currentSubsequenceNumber + 1)
			{
				this.currentSubsequence = master.getSubSequenceSpecifier(this.currentSubsequenceNumber + 1);
				this.currentSubsequenceNumber++;
				return this.currentSubsequence;
			}
			return null;
		}

		// Token: 0x0600D336 RID: 54070 RVA: 0x002EE628 File Offset: 0x002EC828
		private SimplePageMaster GetNextSimplePageMaster(PageSequenceMaster sequenceMaster, int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
		{
			if (this.isForcing)
			{
				return this.layoutMasterSet.getSimplePageMaster(this.GetNextPageMasterName(sequenceMaster, currentPageNumber, false, true));
			}
			string nextPageMasterName = this.GetNextPageMasterName(sequenceMaster, currentPageNumber, thisIsFirstPage, isEmptyPage);
			return this.layoutMasterSet.getSimplePageMaster(nextPageMasterName);
		}

		// Token: 0x0600D337 RID: 54071 RVA: 0x002EE66C File Offset: 0x002EC86C
		private string GetNextPageMasterName(PageSequenceMaster sequenceMaster, int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
		{
			if (this.currentSubsequence == null)
			{
				this.currentSubsequence = this.GetNextSubsequence(sequenceMaster);
			}
			string nextPageMaster = this.currentSubsequence.GetNextPageMaster(currentPageNumber, thisIsFirstPage, isEmptyPage);
			if (nextPageMaster == null || this.IsFlowForMasterNameDone(this.currentPageMasterName))
			{
				SubSequenceSpecifier nextSubsequence = this.GetNextSubsequence(sequenceMaster);
				if (nextSubsequence == null)
				{
					ApocDriver.ActiveDriver.FireApocError("Page subsequences exhausted. Using previous subsequence.");
					thisIsFirstPage = true;
					this.currentSubsequence.Reset();
				}
				else
				{
					this.currentSubsequence = nextSubsequence;
				}
				nextPageMaster = this.currentSubsequence.GetNextPageMaster(currentPageNumber, thisIsFirstPage, isEmptyPage);
			}
			this.currentPageMasterName = nextPageMaster;
			return nextPageMaster;
		}

		// Token: 0x0600D338 RID: 54072 RVA: 0x002EE6F8 File Offset: 0x002EC8F8
		private SimplePageMaster GetCurrentSimplePageMaster()
		{
			return this.layoutMasterSet.getSimplePageMaster(this.currentPageMasterName);
		}

		// Token: 0x0600D339 RID: 54073 RVA: 0x002EE70B File Offset: 0x002EC90B
		private string GetCurrentPageMasterName()
		{
			return this.currentPageMasterName;
		}

		// Token: 0x0600D33A RID: 54074 RVA: 0x002EE714 File Offset: 0x002EC914
		private PageMaster GetNextPageMaster(string pageSequenceName, int currentPageNumber, bool thisIsFirstPage, bool isEmptyPage)
		{
			PageSequenceMaster pageSequenceMaster = this.layoutMasterSet.getPageSequenceMaster(pageSequenceName);
			PageMaster result;
			if (pageSequenceMaster != null)
			{
				result = this.GetNextSimplePageMaster(pageSequenceMaster, currentPageNumber, thisIsFirstPage, isEmptyPage).getPageMaster();
			}
			else
			{
				SimplePageMaster simplePageMaster = this.layoutMasterSet.getSimplePageMaster(pageSequenceName);
				if (simplePageMaster == null)
				{
					throw new ApocException("'master-reference' for 'fo:page-sequence'matches no 'simple-page-master' or 'page-sequence-master'");
				}
				this.currentPageMasterName = pageSequenceName;
				result = simplePageMaster.GetNextPageMaster();
			}
			return result;
		}

		// Token: 0x0600D33B RID: 54075 RVA: 0x002EE770 File Offset: 0x002EC970
		private bool FlowsAreIncomplete()
		{
			bool flag = false;
			foreach (object obj in this._flowMap.Values)
			{
				Flow flow = (Flow)obj;
				if (!(flow is StaticContent))
				{
					flag |= flow.getStatus().isIncomplete();
				}
			}
			return flag;
		}

		// Token: 0x0600D33C RID: 54076 RVA: 0x002EE7E8 File Offset: 0x002EC9E8
		private Flow GetCurrentFlow(string regionClass)
		{
			Region region = this.GetCurrentSimplePageMaster().getRegion(regionClass);
			if (region != null)
			{
				return (Flow)this._flowMap[region.getRegionName()];
			}
			ApocDriver.ActiveDriver.FireApocInfo(string.Concat(new object[]
			{
				"flow is null. regionClass = '",
				regionClass,
				"' currentSPM = ",
				this.GetCurrentSimplePageMaster()
			}));
			return null;
		}

		// Token: 0x0600D33D RID: 54077 RVA: 0x002EE854 File Offset: 0x002ECA54
		private bool IsFlowForMasterNameDone(string masterName)
		{
			if (this.isForcing)
			{
				return false;
			}
			if (masterName != null)
			{
				SimplePageMaster simplePageMaster = this.layoutMasterSet.getSimplePageMaster(masterName);
				Region region = simplePageMaster.getRegion("body");
				Flow flow = (Flow)this._flowMap[region.getRegionName()];
				return flow != null && !flow.getStatus().isIncomplete();
			}
			return false;
		}

		// Token: 0x170042F2 RID: 17138
		// (get) Token: 0x0600D33E RID: 54078 RVA: 0x002EE8B6 File Offset: 0x002ECAB6
		// (set) Token: 0x0600D33F RID: 54079 RVA: 0x002EE8BE File Offset: 0x002ECABE
		public bool IsFlowSet
		{
			get
			{
				return this._isFlowSet;
			}
			set
			{
				this._isFlowSet = value;
			}
		}

		// Token: 0x170042F3 RID: 17139
		// (get) Token: 0x0600D340 RID: 54080 RVA: 0x002EE8C7 File Offset: 0x002ECAC7
		public string IpnValue
		{
			get
			{
				return this.ipnValue;
			}
		}

		// Token: 0x170042F4 RID: 17140
		// (get) Token: 0x0600D341 RID: 54081 RVA: 0x002EE8CF File Offset: 0x002ECACF
		public int CurrentPageNumber
		{
			get
			{
				return this.currentPageNumber;
			}
		}

		// Token: 0x170042F5 RID: 17141
		// (get) Token: 0x0600D342 RID: 54082 RVA: 0x002EE8D7 File Offset: 0x002ECAD7
		public int PageCount
		{
			get
			{
				return this.pageCount;
			}
		}

		// Token: 0x0600D343 RID: 54083 RVA: 0x002EE8E0 File Offset: 0x002ECAE0
		private void ForcePage(AreaTree areaTree, int firstAvailPageNumber)
		{
			bool flag = false;
			if (this.forcePageCount == 7)
			{
				PageSequence succeedingPageSequence = this.root.getSucceedingPageSequence(this);
				if (succeedingPageSequence != null && !succeedingPageSequence.IpnValue.Equals("auto"))
				{
					if (succeedingPageSequence.IpnValue.Equals("auto-odd"))
					{
						if (firstAvailPageNumber % 2 == 0)
						{
							flag = true;
						}
					}
					else if (succeedingPageSequence.IpnValue.Equals("auto-even"))
					{
						if (firstAvailPageNumber % 2 != 0)
						{
							flag = true;
						}
					}
					else
					{
						int num = succeedingPageSequence.CurrentPageNumber;
						if (num % 2 == 0 && firstAvailPageNumber % 2 == 0)
						{
							flag = true;
						}
						else if (num % 2 != 0 && firstAvailPageNumber % 2 != 0)
						{
							flag = true;
						}
					}
				}
			}
			else if (this.forcePageCount == 25 && this.pageCount % 2 != 0)
			{
				flag = true;
			}
			else if (this.forcePageCount == 54 && this.pageCount % 2 == 0)
			{
				flag = true;
			}
			else if (this.forcePageCount == 23 && firstAvailPageNumber % 2 == 0)
			{
				flag = true;
			}
			else if (this.forcePageCount == 24 && firstAvailPageNumber % 2 != 0)
			{
				flag = true;
			}
			else
			{
				int num2 = this.forcePageCount;
			}
			if (flag)
			{
				try
				{
					this.isForcing = true;
					this.currentPageNumber++;
					firstAvailPageNumber = this.currentPageNumber;
					this.currentPage = this.MakePage(areaTree, firstAvailPageNumber, false, true);
					string formattedNumber = this.pageNumberGenerator.makeFormattedPageNumber(this.currentPageNumber);
					this.currentPage.setFormattedNumber(formattedNumber);
					this.currentPage.setPageSequence(this);
					this.FormatStaticContent(areaTree);
					ApocDriver.ActiveDriver.FireApocInfo("[forced-" + firstAvailPageNumber + "]");
					areaTree.addPage(this.currentPage);
					this.root.setRunningPageNumberCounter(this.currentPageNumber);
					this.isForcing = false;
				}
				catch (ApocException)
				{
					ApocDriver.ActiveDriver.FireApocInfo("'force-page-count' failure");
				}
			}
		}

		// Token: 0x0400393F RID: 14655
		private const int EXPLICIT = 0;

		// Token: 0x04003940 RID: 14656
		private const int AUTO = 1;

		// Token: 0x04003941 RID: 14657
		private const int AUTO_EVEN = 2;

		// Token: 0x04003942 RID: 14658
		private const int AUTO_ODD = 3;

		// Token: 0x04003943 RID: 14659
		private Root root;

		// Token: 0x04003944 RID: 14660
		private LayoutMasterSet layoutMasterSet;

		// Token: 0x04003945 RID: 14661
		private Hashtable _flowMap;

		// Token: 0x04003946 RID: 14662
		private string masterName;

		// Token: 0x04003947 RID: 14663
		private bool _isFlowSet;

		// Token: 0x04003948 RID: 14664
		private Page currentPage;

		// Token: 0x04003949 RID: 14665
		private string ipnValue;

		// Token: 0x0400394A RID: 14666
		private int currentPageNumber;

		// Token: 0x0400394B RID: 14667
		private PageNumberGenerator pageNumberGenerator;

		// Token: 0x0400394C RID: 14668
		private int forcePageCount;

		// Token: 0x0400394D RID: 14669
		private int pageCount;

		// Token: 0x0400394E RID: 14670
		private bool isForcing;

		// Token: 0x0400394F RID: 14671
		private int pageNumberType;

		// Token: 0x04003950 RID: 14672
		private bool thisIsFirstPage;

		// Token: 0x04003951 RID: 14673
		private SubSequenceSpecifier currentSubsequence;

		// Token: 0x04003952 RID: 14674
		private int currentSubsequenceNumber = -1;

		// Token: 0x04003953 RID: 14675
		private string currentPageMasterName;

		// Token: 0x02001430 RID: 5168
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D344 RID: 54084 RVA: 0x002EEAB4 File Offset: 0x002ECCB4
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new PageSequence(parent, propertyList);
			}
		}
	}
}
