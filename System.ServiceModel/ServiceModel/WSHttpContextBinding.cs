using System;
using System.ComponentModel;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x0200003C RID: 60
	[TypeForwardedFrom("System.WorkflowServices, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class WSHttpContextBinding : WSHttpBinding
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000946F File Offset: 0x0000766F
		public WSHttpContextBinding()
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009485 File Offset: 0x00007685
		public WSHttpContextBinding(SecurityMode securityMode) : base(securityMode)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000949C File Offset: 0x0000769C
		public WSHttpContextBinding(string configName)
		{
			if (configName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("configName");
			}
			this.ApplyConfiguration(configName);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000094CC File Offset: 0x000076CC
		public WSHttpContextBinding(SecurityMode securityMode, bool reliableSessionEnabled) : base(securityMode, reliableSessionEnabled)
		{
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000094E4 File Offset: 0x000076E4
		private WSHttpContextBinding(WSHttpBinding wsHttpBinding)
		{
			WSHttpContextBinding.WSHttpContextBindingPropertyTransferHelper wshttpContextBindingPropertyTransferHelper = new WSHttpContextBinding.WSHttpContextBindingPropertyTransferHelper();
			wshttpContextBindingPropertyTransferHelper.InitializeFrom(wsHttpBinding);
			wshttpContextBindingPropertyTransferHelper.SetBindingElementType(typeof(WSHttpContextBinding));
			wshttpContextBindingPropertyTransferHelper.ApplyConfiguration(this);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009529 File Offset: 0x00007729
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00009531 File Offset: 0x00007731
		[DefaultValue(null)]
		public Uri ClientCallbackAddress { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000953A File Offset: 0x0000773A
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00009542 File Offset: 0x00007742
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000954B File Offset: 0x0000774B
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00009553 File Offset: 0x00007753
		[DefaultValue(ProtectionLevel.Sign)]
		public ProtectionLevel ContextProtectionLevel
		{
			get
			{
				return this.contextProtectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.contextProtectionLevel = value;
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000957C File Offset: 0x0000777C
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection;
			if (base.AllowCookies)
			{
				try
				{
					base.AllowCookies = false;
					bindingElementCollection = base.CreateBindingElements();
				}
				finally
				{
					base.AllowCookies = true;
				}
				bindingElementCollection.Insert(0, new ContextBindingElement(this.ContextProtectionLevel, ContextExchangeMechanism.HttpCookie, this.ClientCallbackAddress, this.ContextManagementEnabled));
			}
			else
			{
				bindingElementCollection = base.CreateBindingElements();
				bindingElementCollection.Insert(0, new ContextBindingElement(this.ContextProtectionLevel, ContextExchangeMechanism.ContextSoapHeader, this.ClientCallbackAddress, this.ContextManagementEnabled));
			}
			return bindingElementCollection;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009604 File Offset: 0x00007804
		internal new static bool TryCreate(BindingElementCollection bindingElements, out Binding binding)
		{
			if (bindingElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElements");
			}
			binding = null;
			ContextBindingElement contextBindingElement = bindingElements.Find<ContextBindingElement>();
			if (contextBindingElement != null)
			{
				BindingElementCollection bindingElementCollection = new BindingElementCollection(bindingElements);
				bindingElementCollection.Remove<ContextBindingElement>();
				Binding binding2;
				if (WSHttpBindingBase.TryCreate(bindingElementCollection, out binding2))
				{
					bool allowCookies = ((WSHttpBinding)binding2).AllowCookies;
					if ((allowCookies && contextBindingElement.ContextExchangeMechanism == ContextExchangeMechanism.HttpCookie) || (!allowCookies && contextBindingElement.ContextExchangeMechanism == ContextExchangeMechanism.ContextSoapHeader))
					{
						binding = new WSHttpContextBinding((WSHttpBinding)binding2)
						{
							ContextProtectionLevel = contextBindingElement.ProtectionLevel,
							ContextManagementEnabled = contextBindingElement.ContextManagementEnabled
						};
					}
				}
			}
			return binding != null;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000969C File Offset: 0x0000789C
		private void ApplyConfiguration(string configurationName)
		{
			WSHttpContextBindingCollectionElement bindingCollectionElement = WSHttpContextBindingCollectionElement.GetBindingCollectionElement();
			WSHttpContextBindingElement wshttpContextBindingElement = bindingCollectionElement.Bindings[configurationName];
			wshttpContextBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x040001B8 RID: 440
		private ProtectionLevel contextProtectionLevel = ProtectionLevel.Sign;

		// Token: 0x040001B9 RID: 441
		private bool contextManagementEnabled = true;

		// Token: 0x02000ACD RID: 2765
		private class WSHttpContextBindingPropertyTransferHelper : WSHttpBindingElement
		{
			// Token: 0x170019B8 RID: 6584
			// (get) Token: 0x06006E45 RID: 28229 RVA: 0x0019BA56 File Offset: 0x00199C56
			protected override Type BindingElementType
			{
				get
				{
					return this.bindingElementType;
				}
			}

			// Token: 0x06006E46 RID: 28230 RVA: 0x0019BA5E File Offset: 0x00199C5E
			public void SetBindingElementType(Type bindingElementType)
			{
				this.bindingElementType = bindingElementType;
			}

			// Token: 0x04003F09 RID: 16137
			private Type bindingElementType = typeof(WSHttpBinding);
		}
	}
}
