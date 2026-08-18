using System;
using System.ComponentModel;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200013D RID: 317
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesigner : ControlDesigner
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0004A5B8 File Offset: 0x000487B8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.xml = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00043368 File Offset: 0x00041568
		public override string GetDesignTimeHtml()
		{
			return this.GetEmptyDesignTimeHtml();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0004A5CB File Offset: 0x000487CB
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Xml_Inst"));
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0004A5DD File Offset: 0x000487DD
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Xml));
			this.xml = (Xml)component;
			base.Initialize(component);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0004A604 File Offset: 0x00048804
		internal override string GetPersistInnerHtmlInternal()
		{
			Xml xml = (Xml)base.Component;
			string text = (string)((IControlDesignerAccessor)xml).GetDesignModeState()["OriginalContent"];
			if (text != null)
			{
				return text;
			}
			return xml.DocumentContent;
		}

		// Token: 0x040006FD RID: 1789
		private Xml xml;
	}
}
