using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EFF RID: 3839
	public class TargetControlCollection : StronglyTypedStateManagedCollection<TargetControl>
	{
		// Token: 0x060091C2 RID: 37314 RVA: 0x0020CCD5 File Offset: 0x0020AED5
		public new virtual void Add(TargetControl control)
		{
			base.Add(control);
		}

		// Token: 0x060091C3 RID: 37315 RVA: 0x0020CCE0 File Offset: 0x0020AEE0
		public virtual void Add(string id)
		{
			TargetControl item = new TargetControl(id);
			base.Add(item);
		}

		// Token: 0x060091C4 RID: 37316 RVA: 0x0020CCFC File Offset: 0x0020AEFC
		public virtual void Add(string id, string value)
		{
			TargetControl item = new TargetControl(id, value);
			base.Add(item);
		}

		// Token: 0x060091C5 RID: 37317 RVA: 0x0020CD18 File Offset: 0x0020AF18
		public virtual void Add(string id, string value, bool enabled)
		{
			TargetControl item = new TargetControl(id, value, enabled);
			base.Add(item);
		}

		// Token: 0x060091C6 RID: 37318 RVA: 0x0020CD38 File Offset: 0x0020AF38
		public virtual void Add(ControlTypeToApplySkin type, string skin)
		{
			TargetControl item = new TargetControl(type, skin);
			base.Add(item);
		}

		// Token: 0x060091C7 RID: 37319 RVA: 0x0020CD54 File Offset: 0x0020AF54
		public string ContainsType(ISkinnableControl skinnableControl)
		{
			string text = string.Empty;
			foreach (object obj in base.List)
			{
				TargetControl targetControl = (TargetControl)obj;
				text = targetControl.ShouldApplySkinToControlType(skinnableControl);
				if (!string.IsNullOrEmpty(text))
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x060091C8 RID: 37320 RVA: 0x0020CDC0 File Offset: 0x0020AFC0
		protected override void SetDirtyObject(object o)
		{
			if (o is TargetControl)
			{
				((StateManager)o).SetDirty();
			}
		}
	}
}
