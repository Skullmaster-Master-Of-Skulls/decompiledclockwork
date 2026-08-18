using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x0200102E RID: 4142
	[ClientScriptResource("Telerik.Web.UI.Dialogs.DialogInitializer", "Telerik.Web.UI.Dialogs.DialogInitializerScripts.js")]
	[ToolboxItem(false)]
	public class DialogControlInitializer : RadWebControl
	{
		// Token: 0x17003380 RID: 13184
		// (get) Token: 0x0600A332 RID: 41778 RVA: 0x0024529C File Offset: 0x0024349C
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600A333 RID: 41779 RVA: 0x002452A0 File Offset: 0x002434A0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			descriptor.AddScriptProperty("parameterConsumers", javaScriptSerializer.Serialize(this.GetParameterConsumerIDsRecursive(this)));
		}

		// Token: 0x17003381 RID: 13185
		// (get) Token: 0x0600A334 RID: 41780 RVA: 0x002452D2 File Offset: 0x002434D2
		internal override bool ShouldRegisterCssReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600A335 RID: 41781 RVA: 0x002452D8 File Offset: 0x002434D8
		private List<string> GetParameterConsumerIDsRecursive(Control parent)
		{
			List<string> list = new List<string>();
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control is IClientParameterConsumer)
				{
					list.Add(control.ClientID);
				}
				list.AddRange(this.GetParameterConsumerIDsRecursive(control));
			}
			return list;
		}
	}
}
