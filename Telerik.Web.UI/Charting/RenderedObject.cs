using System;
using System.ComponentModel;

namespace Telerik.Charting
{
	// Token: 0x020016DD RID: 5853
	public abstract class RenderedObject : StateManagedObject, IOrdering
	{
		// Token: 0x0600E2F1 RID: 58097 RVA: 0x003249A2 File Offset: 0x00322BA2
		public RenderedObject(IContainer container)
		{
			this.objectContainer = container;
		}

		// Token: 0x17004559 RID: 17753
		// (get) Token: 0x0600E2F2 RID: 58098 RVA: 0x003249B1 File Offset: 0x00322BB1
		// (set) Token: 0x0600E2F3 RID: 58099 RVA: 0x003249B9 File Offset: 0x00322BB9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		public IContainer Container
		{
			get
			{
				return this.objectContainer;
			}
			set
			{
				this.objectContainer = value;
			}
		}

		// Token: 0x0600E2F4 RID: 58100 RVA: 0x003249C2 File Offset: 0x00322BC2
		public int GetOrder()
		{
			return this.objectContainer.GetOrder(this);
		}

		// Token: 0x0600E2F5 RID: 58101 RVA: 0x003249D0 File Offset: 0x00322BD0
		public void SetOrder(int index)
		{
			this.objectContainer.OrderList[this.GetOrder()] = null;
			this.objectContainer.Insert(index, this);
		}

		// Token: 0x0600E2F6 RID: 58102 RVA: 0x003249F6 File Offset: 0x00322BF6
		public void Remove()
		{
			this.objectContainer.Remove(this);
		}

		// Token: 0x0600E2F7 RID: 58103 RVA: 0x00324A04 File Offset: 0x00322C04
		public void BringForward()
		{
			int order = this.GetOrder();
			this.objectContainer.OrderList[order] = null;
			this.objectContainer.Insert(order + 1, this);
		}

		// Token: 0x0600E2F8 RID: 58104 RVA: 0x00324A39 File Offset: 0x00322C39
		public void BringToFront()
		{
			this.objectContainer.OrderList[this.GetOrder()] = null;
			this.objectContainer.Insert(this.objectContainer.NextPosition, this);
		}

		// Token: 0x0600E2F9 RID: 58105 RVA: 0x00324A6C File Offset: 0x00322C6C
		public void SendBackward()
		{
			int order = this.GetOrder();
			this.objectContainer.OrderList[order] = null;
			this.objectContainer.Insert(order - 1, this);
		}

		// Token: 0x0600E2FA RID: 58106 RVA: 0x00324AA1 File Offset: 0x00322CA1
		public void SendToBack()
		{
			this.objectContainer.ReIndex();
			this.objectContainer.OrderList[this.GetOrder()] = null;
			this.objectContainer.Insert(0, this);
		}

		// Token: 0x140001BE RID: 446
		// (add) Token: 0x0600E2FB RID: 58107 RVA: 0x00324AD4 File Offset: 0x00322CD4
		// (remove) Token: 0x0600E2FC RID: 58108 RVA: 0x00324B0C File Offset: 0x00322D0C
		internal event EventHandler<EventArgs> RenderEventHandler;

		// Token: 0x0600E2FD RID: 58109 RVA: 0x00324B41 File Offset: 0x00322D41
		internal void OnRender()
		{
			if (this.RenderEventHandler != null)
			{
				this.RenderEventHandler(this, null);
			}
		}

		// Token: 0x0400417B RID: 16763
		protected IContainer objectContainer;
	}
}
