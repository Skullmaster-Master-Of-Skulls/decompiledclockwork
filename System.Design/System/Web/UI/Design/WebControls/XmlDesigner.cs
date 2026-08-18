using System;
using System.ComponentModel;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000507 RID: 1287
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDesigner : ControlDesigner
	{
		// Token: 0x06002DFC RID: 11772 RVA: 0x00104E4E File Offset: 0x00103E4E
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.xml = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00104E61 File Offset: 0x00103E61
		public override string GetDesignTimeHtml()
		{
			return this.GetEmptyDesignTimeHtml();
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x00104E69 File Offset: 0x00103E69
		protected override string GetEmptyDesignTimeHtml()
		{
			return base.CreatePlaceHolderDesignTimeHtml(SR.GetString("Xml_Inst"));
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x00104E7B File Offset: 0x00103E7B
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Xml));
			this.xml = (Xml)component;
			base.Initialize(component);
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x00104EA0 File Offset: 0x00103EA0
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

		// Token: 0x04001F4F RID: 8015
		private Xml xml;
	}
}
