using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000548 RID: 1352
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class WebPartZoneBaseDesigner : WebZoneDesigner
	{
		// Token: 0x06002F7D RID: 12157 RVA: 0x0010E7AD File Offset: 0x0010D7AD
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(WebPartZoneBase));
			base.Initialize(component);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x0010E7C8 File Offset: 0x0010D7C8
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
