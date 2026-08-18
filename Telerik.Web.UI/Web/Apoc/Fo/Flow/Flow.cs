using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.Fo.Pagination;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013D7 RID: 5079
	internal class Flow : FObj
	{
		// Token: 0x0600D1E0 RID: 53728 RVA: 0x002E7E3E File Offset: 0x002E603E
		public new static FObj.Maker GetMaker()
		{
			return new Flow.Maker();
		}

		// Token: 0x0600D1E1 RID: 53729 RVA: 0x002E7E48 File Offset: 0x002E6048
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected Flow(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = this.GetElementName();
			if (!parent.GetName().Equals("fo:page-sequence"))
			{
				throw new ApocException("flow must be child of page-sequence, not " + parent.GetName());
			}
			this.pageSequence = (PageSequence)parent;
			this.SetFlowName(this.GetProperty("flow-name").GetString());
			if (!this.pageSequence.IsFlowSet)
			{
				this.pageSequence.AddFlow(this);
				return;
			}
			if (this.name.Equals("fo:flow"))
			{
				throw new ApocException("Only a single fo:flow permitted per fo:page-sequence");
			}
			throw new ApocException(this.name + " not allowed after fo:flow");
		}

		// Token: 0x0600D1E2 RID: 53730 RVA: 0x002E7F0D File Offset: 0x002E610D
		protected virtual void SetFlowName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				ApocDriver.ActiveDriver.FireApocWarning("A 'flow-name' is required for " + this.GetElementName() + ". This constraint will be enforced in future versions of Apoc");
				this._flowName = "xsl-region-body";
				return;
			}
			this._flowName = name;
		}

		// Token: 0x0600D1E3 RID: 53731 RVA: 0x002E7F49 File Offset: 0x002E6149
		public string GetFlowName()
		{
			return this._flowName;
		}

		// Token: 0x0600D1E4 RID: 53732 RVA: 0x002E7F51 File Offset: 0x002E6151
		public override Status Layout(Area area)
		{
			return this.Layout(area, null);
		}

		// Token: 0x0600D1E5 RID: 53733 RVA: 0x002E7F5C File Offset: 0x002E615C
		public virtual Status Layout(Area area, Region region)
		{
			if (this.marker == -1000)
			{
				this.marker = 0;
			}
			BodyAreaContainer bodyAreaContainer = (BodyAreaContainer)area;
			bool flag = false;
			this.getMarkerSnapshot(new ArrayList());
			int count = this.children.Count;
			if (count == 0)
			{
				throw new ApocException("fo:flow must contain block-level children");
			}
			for (int i = this.marker; i < count; i++)
			{
				FObj fobj = (FObj)this.children[i];
				if (bodyAreaContainer.isBalancingRequired(fobj))
				{
					bodyAreaContainer.resetSpanArea();
					this.Rollback(this.markerSnapshot);
					i = this.marker - 1;
				}
				else
				{
					Area nextArea = bodyAreaContainer.getNextArea(fobj);
					nextArea.setIDReferences(bodyAreaContainer.getIDReferences());
					if (bodyAreaContainer.isNewSpanArea())
					{
						this.marker = i;
						this.markerSnapshot = this.getMarkerSnapshot(new ArrayList());
					}
					this.SetContentWidth(nextArea.getContentWidth());
					this._status = fobj.Layout(nextArea);
					if (this._status.isIncomplete())
					{
						if (flag && this._status.laidOutNone())
						{
							this.marker = i - 1;
							FObj fobj2 = (FObj)this.children[this.marker];
							fobj2.RemoveAreas();
							fobj2.ResetMarker();
							fobj2.RemoveID(area.getIDReferences());
							this._status = new Status(3);
							return this._status;
						}
						if (bodyAreaContainer.isLastColumn())
						{
							if (this._status.getCode() == 7)
							{
								this.marker = i;
								this._status = new Status(4);
								return this._status;
							}
							this.marker = i;
							return this._status;
						}
						else
						{
							if (this._status.isPageBreak())
							{
								this.marker = i;
								return this._status;
							}
							((ColumnArea)nextArea).incrementSpanIndex();
							i--;
						}
					}
					flag = (this._status.getCode() == 8);
				}
			}
			return this._status;
		}

		// Token: 0x0600D1E6 RID: 53734 RVA: 0x002E8145 File Offset: 0x002E6345
		protected void SetContentWidth(int contentWidth)
		{
			this.contentWidth = contentWidth;
		}

		// Token: 0x0600D1E7 RID: 53735 RVA: 0x002E814E File Offset: 0x002E634E
		public override int GetContentWidth()
		{
			return this.contentWidth;
		}

		// Token: 0x0600D1E8 RID: 53736 RVA: 0x002E8156 File Offset: 0x002E6356
		protected virtual string GetElementName()
		{
			return "fo:flow";
		}

		// Token: 0x0600D1E9 RID: 53737 RVA: 0x002E815D File Offset: 0x002E635D
		public Status getStatus()
		{
			return this._status;
		}

		// Token: 0x0600D1EA RID: 53738 RVA: 0x002E8165 File Offset: 0x002E6365
		public override bool GeneratesReferenceAreas()
		{
			return true;
		}

		// Token: 0x04003880 RID: 14464
		private PageSequence pageSequence;

		// Token: 0x04003881 RID: 14465
		private ArrayList markerSnapshot;

		// Token: 0x04003882 RID: 14466
		private string _flowName;

		// Token: 0x04003883 RID: 14467
		private int contentWidth;

		// Token: 0x04003884 RID: 14468
		private Status _status = new Status(2);

		// Token: 0x020013D8 RID: 5080
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1EB RID: 53739 RVA: 0x002E8168 File Offset: 0x002E6368
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Flow(parent, propertyList);
			}
		}
	}
}
