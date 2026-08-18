using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000154 RID: 340
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WebPartZoneBaseDesigner : WebZoneDesigner
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0004B55E File Offset: 0x0004975E
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebPartZoneBase));
			base.Initialize(component);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0004B578 File Offset: 0x00049778
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			Attribute[] attributes = new Attribute[]
			{
				new BrowsableAttribute(false),
				new EditorBrowsableAttribute(EditorBrowsableState.Never),
				new ThemeableAttribute(false)
			};
			string key = "VerbStyle";
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[key];
			if (propertyDescriptor != null)
			{
				properties[key] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, attributes);
			}
		}
	}
}
