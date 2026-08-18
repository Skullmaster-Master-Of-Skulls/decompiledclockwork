using System;
using System.Collections;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001393 RID: 5011
	internal abstract class FONode
	{
		// Token: 0x0600D0CB RID: 53451 RVA: 0x002E39E6 File Offset: 0x002E1BE6
		protected FONode(FObj parent)
		{
			this.parent = parent;
			if (parent != null)
			{
				this.areaClass = parent.areaClass;
			}
		}

		// Token: 0x0600D0CC RID: 53452 RVA: 0x002E3A28 File Offset: 0x002E1C28
		public virtual void SetIsInTableCell()
		{
			this.isInTableCell = true;
			foreach (object obj in this.children)
			{
				FONode fonode = (FONode)obj;
				fonode.SetIsInTableCell();
			}
		}

		// Token: 0x0600D0CD RID: 53453 RVA: 0x002E3A88 File Offset: 0x002E1C88
		public virtual void ForceStartOffset(int offset)
		{
			this.forcedStartOffset = offset;
			foreach (object obj in this.children)
			{
				FONode fonode = (FONode)obj;
				fonode.ForceStartOffset(offset);
			}
		}

		// Token: 0x0600D0CE RID: 53454 RVA: 0x002E3AE8 File Offset: 0x002E1CE8
		public virtual void ForceWidth(int width)
		{
			this.forcedWidth = width;
			foreach (object obj in this.children)
			{
				FONode fonode = (FONode)obj;
				fonode.ForceWidth(width);
			}
		}

		// Token: 0x0600D0CF RID: 53455 RVA: 0x002E3B48 File Offset: 0x002E1D48
		public virtual void ResetMarker()
		{
			this.marker = -1000;
			foreach (object obj in this.children)
			{
				FONode fonode = (FONode)obj;
				fonode.ResetMarker();
			}
		}

		// Token: 0x0600D0D0 RID: 53456 RVA: 0x002E3BAC File Offset: 0x002E1DAC
		public void SetWidows(int wid)
		{
			this.widows = wid;
		}

		// Token: 0x0600D0D1 RID: 53457 RVA: 0x002E3BB5 File Offset: 0x002E1DB5
		public void SetOrphans(int orph)
		{
			this.orphans = orph;
		}

		// Token: 0x0600D0D2 RID: 53458 RVA: 0x002E3BBE File Offset: 0x002E1DBE
		public void RemoveAreas()
		{
		}

		// Token: 0x0600D0D3 RID: 53459 RVA: 0x002E3BC0 File Offset: 0x002E1DC0
		protected internal virtual void AddChild(FONode child)
		{
			this.children.Add(child);
		}

		// Token: 0x0600D0D4 RID: 53460 RVA: 0x002E3BCF File Offset: 0x002E1DCF
		public FObj getParent()
		{
			return this.parent;
		}

		// Token: 0x0600D0D5 RID: 53461 RVA: 0x002E3BD8 File Offset: 0x002E1DD8
		public virtual void SetLinkSet(LinkSet linkSet)
		{
			this.linkSet = linkSet;
			foreach (object obj in this.children)
			{
				FONode fonode = (FONode)obj;
				fonode.SetLinkSet(linkSet);
			}
		}

		// Token: 0x0600D0D6 RID: 53462 RVA: 0x002E3C38 File Offset: 0x002E1E38
		public virtual LinkSet GetLinkSet()
		{
			return this.linkSet;
		}

		// Token: 0x0600D0D7 RID: 53463
		public abstract Status Layout(Area area);

		// Token: 0x0600D0D8 RID: 53464 RVA: 0x002E3C40 File Offset: 0x002E1E40
		public virtual Property GetProperty(string name)
		{
			return null;
		}

		// Token: 0x0600D0D9 RID: 53465 RVA: 0x002E3C44 File Offset: 0x002E1E44
		public virtual ArrayList getMarkerSnapshot(ArrayList snapshot)
		{
			snapshot.Add(this.marker);
			if (this.marker < 0)
			{
				return snapshot;
			}
			if (this.children.Count == 0)
			{
				return snapshot;
			}
			return ((FONode)this.children[this.marker]).getMarkerSnapshot(snapshot);
		}

		// Token: 0x0600D0DA RID: 53466 RVA: 0x002E3C9C File Offset: 0x002E1E9C
		public virtual void Rollback(ArrayList snapshot)
		{
			this.marker = (int)snapshot[0];
			snapshot.RemoveAt(0);
			if (this.marker == -1000)
			{
				this.ResetMarker();
				return;
			}
			if (this.marker == -1 || this.children.Count == 0)
			{
				return;
			}
			if (this.marker <= -1000)
			{
				return;
			}
			int count = this.children.Count;
			for (int i = this.marker + 1; i < count; i++)
			{
				FONode fonode = (FONode)this.children[i];
				fonode.ResetMarker();
			}
			((FONode)this.children[this.marker]).Rollback(snapshot);
		}

		// Token: 0x0600D0DB RID: 53467 RVA: 0x002E3D4E File Offset: 0x002E1F4E
		public virtual bool MayPrecedeMarker()
		{
			return false;
		}

		// Token: 0x04003803 RID: 14339
		public const int MarkerStart = -1000;

		// Token: 0x04003804 RID: 14340
		public const int MarkerBreakAfter = -1001;

		// Token: 0x04003805 RID: 14341
		protected FObj parent;

		// Token: 0x04003806 RID: 14342
		protected string areaClass = AreaClass.UNASSIGNED;

		// Token: 0x04003807 RID: 14343
		protected ArrayList children = new ArrayList();

		// Token: 0x04003808 RID: 14344
		protected int marker = -1000;

		// Token: 0x04003809 RID: 14345
		protected bool isInTableCell;

		// Token: 0x0400380A RID: 14346
		protected int forcedStartOffset;

		// Token: 0x0400380B RID: 14347
		protected int forcedWidth;

		// Token: 0x0400380C RID: 14348
		protected int widows;

		// Token: 0x0400380D RID: 14349
		protected int orphans;

		// Token: 0x0400380E RID: 14350
		protected LinkSet linkSet;

		// Token: 0x0400380F RID: 14351
		public int areasGenerated;
	}
}
