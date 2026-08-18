using System;
using System.ComponentModel;
using System.Configuration;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x0200003A RID: 58
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class BasicHttpContextBinding : BasicHttpBinding
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0000911F File Offset: 0x0000731F
		public BasicHttpContextBinding()
		{
			base.AllowCookies = true;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009135 File Offset: 0x00007335
		public BasicHttpContextBinding(BasicHttpSecurityMode securityMode) : base(securityMode)
		{
			base.AllowCookies = true;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000914C File Offset: 0x0000734C
		public BasicHttpContextBinding(string configName)
		{
			if (configName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configName");
			}
			BasicHttpContextBindingCollectionElement bindingCollectionElement = BasicHttpContextBindingCollectionElement.GetBindingCollectionElement();
			BasicHttpContextBindingElement basicHttpContextBindingElement = bindingCollectionElement.Bindings[configName];
			basicHttpContextBindingElement.ApplyConfiguration(this);
			if (basicHttpContextBindingElement.ElementInformation.Properties["allowCookies"].ValueOrigin == PropertyValueOrigin.Default)
			{
				base.AllowCookies = true;
				return;
			}
			if (!base.AllowCookies)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("BasicHttpContextBindingRequiresAllowCookie", new object[]
				{
					base.Namespace,
					base.Name
				}));
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000091EC File Offset: 0x000073EC
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x000091F4 File Offset: 0x000073F4
		[DefaultValue(true)]
		public bool ContextManagementEnabled
		{
			get
			{
				return this.contextManagementEnabled;
			}
			set
			{
				this.contextManagementEnabled = value;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009200 File Offset: 0x00007400
		public override BindingElementCollection CreateBindingElements()
		{
			if (!base.AllowCookies)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BasicHttpContextBindingRequiresAllowCookie", new object[]
				{
					base.Namespace,
					base.Name
				})));
			}
			BindingElementCollection bindingElementCollection;
			try
			{
				base.AllowCookies = false;
				bindingElementCollection = base.CreateBindingElements();
			}
			finally
			{
				base.AllowCookies = true;
			}
			bindingElementCollection.Insert(0, new ContextBindingElement(ProtectionLevel.None, ContextExchangeMechanism.HttpCookie, null, this.ContextManagementEnabled));
			return bindingElementCollection;
		}

		// Token: 0x040001B4 RID: 436
		private bool contextManagementEnabled = true;
	}
}
