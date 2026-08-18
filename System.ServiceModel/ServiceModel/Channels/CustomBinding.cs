using System;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Windows.Markup;

namespace System.ServiceModel.Channels
{
	// Token: 0x020006FA RID: 1786
	[ContentProperty("Elements")]
	[__DynamicallyInvokable]
	public class CustomBinding : Binding
	{
		// Token: 0x06004471 RID: 17521 RVA: 0x00102227 File Offset: 0x00100427
		[__DynamicallyInvokable]
		public CustomBinding()
		{
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x0010223A File Offset: 0x0010043A
		public CustomBinding(string configurationName)
		{
			this.ApplyConfiguration(configurationName);
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x00102254 File Offset: 0x00100454
		[__DynamicallyInvokable]
		public CustomBinding(params BindingElement[] bindingElementsInTopDownChannelStackOrder)
		{
			if (bindingElementsInTopDownChannelStackOrder == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElements");
			}
			foreach (BindingElement item in bindingElementsInTopDownChannelStackOrder)
			{
				this.bindingElements.Add(item);
			}
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x001022A8 File Offset: 0x001004A8
		[__DynamicallyInvokable]
		public CustomBinding(string name, string ns, params BindingElement[] bindingElementsInTopDownChannelStackOrder) : base(name, ns)
		{
			if (bindingElementsInTopDownChannelStackOrder == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElements");
			}
			foreach (BindingElement item in bindingElementsInTopDownChannelStackOrder)
			{
				this.bindingElements.Add(item);
			}
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x001022FC File Offset: 0x001004FC
		[__DynamicallyInvokable]
		public CustomBinding(IEnumerable<BindingElement> bindingElementsInTopDownChannelStackOrder)
		{
			if (bindingElementsInTopDownChannelStackOrder == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElements");
			}
			foreach (BindingElement item in bindingElementsInTopDownChannelStackOrder)
			{
				this.bindingElements.Add(item);
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x00102370 File Offset: 0x00100570
		internal CustomBinding(BindingElementCollection bindingElements)
		{
			if (bindingElements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElements");
			}
			for (int i = 0; i < bindingElements.Count; i++)
			{
				this.bindingElements.Add(bindingElements[i]);
			}
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x001023C4 File Offset: 0x001005C4
		[__DynamicallyInvokable]
		public CustomBinding(Binding binding) : this(binding, CustomBinding.SafeCreateBindingElements(binding))
		{
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x001023D3 File Offset: 0x001005D3
		private static BindingElementCollection SafeCreateBindingElements(Binding binding)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			return binding.CreateBindingElements();
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x001023F0 File Offset: 0x001005F0
		internal CustomBinding(Binding binding, BindingElementCollection elements)
		{
			if (binding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("binding");
			}
			if (elements == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("elements");
			}
			base.Name = binding.Name;
			base.Namespace = binding.Namespace;
			base.CloseTimeout = binding.CloseTimeout;
			base.OpenTimeout = binding.OpenTimeout;
			base.ReceiveTimeout = binding.ReceiveTimeout;
			base.SendTimeout = binding.SendTimeout;
			for (int i = 0; i < elements.Count; i++)
			{
				this.bindingElements.Add(elements[i]);
			}
		}

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x0010249F File Offset: 0x0010069F
		[__DynamicallyInvokable]
		public BindingElementCollection Elements
		{
			[__DynamicallyInvokable]
			get
			{
				return this.bindingElements;
			}
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x001024A7 File Offset: 0x001006A7
		[__DynamicallyInvokable]
		public override BindingElementCollection CreateBindingElements()
		{
			return this.bindingElements.Clone();
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x001024B4 File Offset: 0x001006B4
		private void ApplyConfiguration(string configurationName)
		{
			CustomBindingCollectionElement bindingCollectionElement = CustomBindingCollectionElement.GetBindingCollectionElement();
			CustomBindingElement customBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (customBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"customBinding"
				})));
			}
			customBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x0600447D RID: 17533 RVA: 0x0010250C File Offset: 0x0010070C
		[__DynamicallyInvokable]
		public override string Scheme
		{
			[__DynamicallyInvokable]
			get
			{
				TransportBindingElement transportBindingElement = this.bindingElements.Find<TransportBindingElement>();
				if (transportBindingElement == null)
				{
					return string.Empty;
				}
				return transportBindingElement.Scheme;
			}
		}

		// Token: 0x04002D35 RID: 11573
		private BindingElementCollection bindingElements = new BindingElementCollection();
	}
}
