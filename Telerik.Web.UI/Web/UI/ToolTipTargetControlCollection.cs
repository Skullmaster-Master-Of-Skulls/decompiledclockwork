using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200133F RID: 4927
	public class ToolTipTargetControlCollection : StronglyTypedStateManagedCollection<ToolTipTargetControl>
	{
		// Token: 0x0600CD72 RID: 52594 RVA: 0x002DBE35 File Offset: 0x002DA035
		public new virtual void Add(ToolTipTargetControl control)
		{
			base.Add(control);
		}

		// Token: 0x0600CD73 RID: 52595 RVA: 0x002DBE40 File Offset: 0x002DA040
		public virtual void Add(string controlID)
		{
			ToolTipTargetControl item = new ToolTipTargetControl(controlID);
			base.Add(item);
		}

		// Token: 0x0600CD74 RID: 52596 RVA: 0x002DBE5C File Offset: 0x002DA05C
		public virtual void Add(string controlID, bool isClientID)
		{
			ToolTipTargetControl item = new ToolTipTargetControl(controlID, isClientID);
			base.Add(item);
		}

		// Token: 0x0600CD75 RID: 52597 RVA: 0x002DBE78 File Offset: 0x002DA078
		public virtual void Add(string controlID, string val, bool isClientID)
		{
			ToolTipTargetControl item = new ToolTipTargetControl(controlID, val, isClientID);
			base.Add(item);
		}

		// Token: 0x0600CD76 RID: 52598 RVA: 0x002DBE98 File Offset: 0x002DA098
		public virtual void Add(string controlID, string val)
		{
			ToolTipTargetControl item = new ToolTipTargetControl(controlID, val);
			base.Add(item);
		}

		// Token: 0x0600CD77 RID: 52599 RVA: 0x002DBEB4 File Offset: 0x002DA0B4
		protected override void SetDirtyObject(object o)
		{
			if (o is ToolTipTargetControl)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}
