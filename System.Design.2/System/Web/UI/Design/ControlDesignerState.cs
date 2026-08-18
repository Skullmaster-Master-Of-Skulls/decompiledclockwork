using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000018 RID: 24
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class ControlDesignerState
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00005DC0 File Offset: 0x00003FC0
		internal ControlDesignerState(IComponent component)
		{
			this._component = component;
		}

		// Token: 0x1700002B RID: 43
		public object this[string key]
		{
			get
			{
				if (this._designerState == null)
				{
					if (this._component != null && this._component.Site != null)
					{
						IComponentDesignerStateService componentDesignerStateService = (IComponentDesignerStateService)this._component.Site.GetService(typeof(IComponentDesignerStateService));
						if (componentDesignerStateService != null)
						{
							return componentDesignerStateService.GetState(this._component, key);
						}
					}
					this._designerState = new Hashtable();
				}
				return this._designerState[key];
			}
			set
			{
				if (this._designerState == null)
				{
					if (this._component != null && this._component.Site != null)
					{
						IComponentDesignerStateService componentDesignerStateService = (IComponentDesignerStateService)this._component.Site.GetService(typeof(IComponentDesignerStateService));
						if (componentDesignerStateService != null)
						{
							componentDesignerStateService.SetState(this._component, key, value);
							return;
						}
					}
					this._designerState = new Hashtable();
				}
				this._designerState[key] = value;
			}
		}

		// Token: 0x040000D6 RID: 214
		private IDictionary _designerState;

		// Token: 0x040000D7 RID: 215
		private IComponent _component;
	}
}
