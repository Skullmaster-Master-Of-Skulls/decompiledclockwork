using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x0200014A RID: 330
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class EditorPartDesigner : PartDesigner
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x0004B04C File Offset: 0x0004924C
		protected override Control CreateViewControl()
		{
			Control control = base.CreateViewControl();
			IDictionary designModeState = ((IControlDesignerAccessor)this._editorPart).GetDesignModeState();
			((IControlDesignerAccessor)control).SetDesignModeState(designModeState);
			return control;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0004B074 File Offset: 0x00049274
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(EditorPart));
			this._editorPart = (EditorPart)component;
			base.Initialize(component);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0004B099 File Offset: 0x00049299
		public override string GetDesignTimeHtml()
		{
			if (!(this._editorPart.Parent is EditorZoneBase))
			{
				return base.CreateInvalidParentDesignTimeHtml(typeof(EditorPart), typeof(EditorZoneBase));
			}
			return base.GetDesignTimeHtml();
		}

		// Token: 0x0400070F RID: 1807
		private EditorPart _editorPart;
	}
}
