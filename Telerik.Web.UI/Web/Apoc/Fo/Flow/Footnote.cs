using System;
using System.Collections;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013D9 RID: 5081
	internal class Footnote : FObj
	{
		// Token: 0x0600D1ED RID: 53741 RVA: 0x002E8179 File Offset: 0x002E6379
		public new static FObj.Maker GetMaker()
		{
			return new Footnote.Maker();
		}

		// Token: 0x0600D1EE RID: 53742 RVA: 0x002E8180 File Offset: 0x002E6380
		public Footnote(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:footnote";
		}

		// Token: 0x0600D1EF RID: 53743 RVA: 0x002E8198 File Offset: 0x002E6398
		public override Status Layout(Area area)
		{
			FONode fonode = null;
			FONode fonode2 = null;
			if (this.marker == -1000)
			{
				this.marker = 0;
			}
			BlockArea blockArea = area as BlockArea;
			int count = this.children.Count;
			for (int i = this.marker; i < count; i++)
			{
				FONode fonode3 = (FONode)this.children[i];
				if (fonode3 is Inline)
				{
					fonode = fonode3;
					Status result = fonode3.Layout(area);
					if (result.isIncomplete())
					{
						return result;
					}
				}
				else
				{
					FootnoteBody footnoteBody = fonode3 as FootnoteBody;
					if (fonode != null && footnoteBody != null)
					{
						fonode2 = fonode3;
						if (blockArea != null)
						{
							blockArea.addFootnote(footnoteBody);
						}
						else
						{
							Page page = area.getPage();
							Footnote.LayoutFootnote(page, footnoteBody, area);
						}
					}
				}
			}
			if (fonode2 == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning("No footnote-body in footnote");
			}
			return new Status(1);
		}

		// Token: 0x0600D1F0 RID: 53744 RVA: 0x002E8268 File Offset: 0x002E6468
		public static bool LayoutFootnote(Page p, FootnoteBody fb, Area area)
		{
			try
			{
				BodyAreaContainer body = p.getBody();
				AreaContainer footnoteReferenceArea = body.getFootnoteReferenceArea();
				footnoteReferenceArea.setIDReferences(body.getIDReferences());
				int num = footnoteReferenceArea.GetCurrentYPosition() - footnoteReferenceArea.GetHeight();
				int height = footnoteReferenceArea.GetHeight();
				if (area != null)
				{
					footnoteReferenceArea.setMaxHeight(area.getMaxHeight() - area.GetHeight() + footnoteReferenceArea.GetHeight());
				}
				else
				{
					footnoteReferenceArea.setMaxHeight(body.getMaxHeight() + footnoteReferenceArea.GetHeight());
				}
				if (fb.Layout(footnoteReferenceArea).isIncomplete())
				{
					return false;
				}
				if (area != null)
				{
					area.setMaxHeight(area.getMaxHeight() - footnoteReferenceArea.GetHeight() + height);
				}
				if (body.getFootnoteState() == 0)
				{
					Area mainReferenceArea = body.getMainReferenceArea();
					Footnote.DecreaseMaxHeight(mainReferenceArea, footnoteReferenceArea.GetHeight() - height);
					footnoteReferenceArea.setYPosition(num + footnoteReferenceArea.GetHeight());
				}
			}
			catch (ApocException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600D1F1 RID: 53745 RVA: 0x002E8350 File Offset: 0x002E6550
		protected static void DecreaseMaxHeight(Area ar, int change)
		{
			ar.setMaxHeight(ar.getMaxHeight() - change);
			ArrayList children = ar.getChildren();
			foreach (object obj in children)
			{
				Area area = obj as Area;
				if (area != null)
				{
					Footnote.DecreaseMaxHeight(area, change);
				}
			}
		}

		// Token: 0x020013DA RID: 5082
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1F2 RID: 53746 RVA: 0x002E83C4 File Offset: 0x002E65C4
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Footnote(parent, propertyList);
			}
		}
	}
}
