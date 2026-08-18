using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200036D RID: 877
	internal class WebBrowserDesigner : AxDesigner
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060023EC RID: 9196 RVA: 0x000E038B File Offset: 0x000DE58B
		// (set) Token: 0x060023ED RID: 9197 RVA: 0x000E03A2 File Offset: 0x000DE5A2
		public Uri Url
		{
			get
			{
				return (Uri)base.ShadowProperties["Url"];
			}
			set
			{
				base.ShadowProperties["Url"] = value;
			}
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000E03B8 File Offset: 0x000DE5B8
		public override void Initialize(IComponent c)
		{
			WebBrowser webBrowser = c as WebBrowser;
			this.Url = webBrowser.Url;
			webBrowser.Url = new Uri("about:blank");
			base.Initialize(c);
			webBrowser.Url = null;
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000E03F8 File Offset: 0x000DE5F8
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			WebBrowser webBrowser = (WebBrowser)base.Component;
			if (webBrowser != null)
			{
				webBrowser.MinimumSize = new Size(20, 20);
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000CE610 File Offset: 0x000CC810
		protected override InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (base.InheritanceAttribute == InheritanceAttribute.Inherited)
				{
					return InheritanceAttribute.InheritedReadOnly;
				}
				return base.InheritanceAttribute;
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000E042C File Offset: 0x000DE62C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"Url"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(WebBrowserDesigner), propertyDescriptor, attributes);
				}
			}
		}
	}
}
