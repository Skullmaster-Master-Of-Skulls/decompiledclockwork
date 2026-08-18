using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000825 RID: 2085
	internal class UnrecognizedAssertionsBindingElement : BindingElement
	{
		// Token: 0x06004DF3 RID: 19955 RVA: 0x0011CF5A File Offset: 0x0011B15A
		protected internal UnrecognizedAssertionsBindingElement(XmlQualifiedName wsdlBinding, ICollection<XmlElement> bindingAsserions)
		{
			this.wsdlBinding = wsdlBinding;
			this.bindingAsserions = bindingAsserions;
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x0011CF70 File Offset: 0x0011B170
		internal XmlQualifiedName WsdlBinding
		{
			get
			{
				return this.wsdlBinding;
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x0011CF78 File Offset: 0x0011B178
		internal ICollection<XmlElement> BindingAsserions
		{
			get
			{
				if (this.bindingAsserions == null)
				{
					this.bindingAsserions = new Collection<XmlElement>();
				}
				return this.bindingAsserions;
			}
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x0011CF93 File Offset: 0x0011B193
		internal IDictionary<OperationDescription, ICollection<XmlElement>> OperationAssertions
		{
			get
			{
				if (this.operationAssertions == null)
				{
					this.operationAssertions = new Dictionary<OperationDescription, ICollection<XmlElement>>();
				}
				return this.operationAssertions;
			}
		}

		// Token: 0x17001381 RID: 4993
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x0011CFAE File Offset: 0x0011B1AE
		internal IDictionary<MessageDescription, ICollection<XmlElement>> MessageAssertions
		{
			get
			{
				if (this.messageAssertions == null)
				{
					this.messageAssertions = new Dictionary<MessageDescription, ICollection<XmlElement>>();
				}
				return this.messageAssertions;
			}
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0011CFCC File Offset: 0x0011B1CC
		internal void Add(OperationDescription operation, ICollection<XmlElement> assertions)
		{
			ICollection<XmlElement> collection;
			if (!this.OperationAssertions.TryGetValue(operation, out collection))
			{
				this.OperationAssertions.Add(operation, assertions);
				return;
			}
			foreach (XmlElement item in assertions)
			{
				collection.Add(item);
			}
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x0011D034 File Offset: 0x0011B234
		internal void Add(MessageDescription message, ICollection<XmlElement> assertions)
		{
			ICollection<XmlElement> collection;
			if (!this.MessageAssertions.TryGetValue(message, out collection))
			{
				this.MessageAssertions.Add(message, assertions);
				return;
			}
			foreach (XmlElement item in assertions)
			{
				collection.Add(item);
			}
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x0011D09C File Offset: 0x0011B29C
		protected UnrecognizedAssertionsBindingElement(UnrecognizedAssertionsBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.wsdlBinding = elementToBeCloned.wsdlBinding;
			this.bindingAsserions = elementToBeCloned.bindingAsserions;
			this.operationAssertions = elementToBeCloned.operationAssertions;
			this.messageAssertions = elementToBeCloned.messageAssertions;
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x0011D0D5 File Offset: 0x0011B2D5
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x0011D0F0 File Offset: 0x0011B2F0
		public override BindingElement Clone()
		{
			return new UnrecognizedAssertionsBindingElement(new XmlQualifiedName(this.wsdlBinding.Name, this.wsdlBinding.Namespace), null);
		}

		// Token: 0x040030BE RID: 12478
		private XmlQualifiedName wsdlBinding;

		// Token: 0x040030BF RID: 12479
		private ICollection<XmlElement> bindingAsserions;

		// Token: 0x040030C0 RID: 12480
		private IDictionary<OperationDescription, ICollection<XmlElement>> operationAssertions;

		// Token: 0x040030C1 RID: 12481
		private IDictionary<MessageDescription, ICollection<XmlElement>> messageAssertions;
	}
}
