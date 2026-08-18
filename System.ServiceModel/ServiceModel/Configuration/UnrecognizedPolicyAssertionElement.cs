using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E0 RID: 1760
	internal sealed class UnrecognizedPolicyAssertionElement : BindingElementExtensionElement
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x001007F9 File Offset: 0x000FE9F9
		public override Type BindingElementType
		{
			get
			{
				return typeof(UnrecognizedAssertionsBindingElement);
			}
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x00100808 File Offset: 0x000FEA08
		public override void CopyFrom(ServiceModelExtensionElement from)
		{
			base.CopyFrom(from);
			UnrecognizedPolicyAssertionElement unrecognizedPolicyAssertionElement = (UnrecognizedPolicyAssertionElement)from;
			this.wsdlBinding = unrecognizedPolicyAssertionElement.wsdlBinding;
			this.bindingAsserions = unrecognizedPolicyAssertionElement.bindingAsserions;
			this.operationAssertions = unrecognizedPolicyAssertionElement.operationAssertions;
			this.messageAssertions = unrecognizedPolicyAssertionElement.messageAssertions;
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x00100853 File Offset: 0x000FEA53
		protected internal override BindingElement CreateBindingElement()
		{
			return new UnrecognizedAssertionsBindingElement(XmlQualifiedName.Empty, null);
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x00100860 File Offset: 0x000FEA60
		protected internal override void InitializeFrom(BindingElement bindingElement)
		{
			base.InitializeFrom(bindingElement);
			UnrecognizedAssertionsBindingElement unrecognizedAssertionsBindingElement = (UnrecognizedAssertionsBindingElement)bindingElement;
			this.wsdlBinding = unrecognizedAssertionsBindingElement.WsdlBinding;
			this.bindingAsserions = unrecognizedAssertionsBindingElement.BindingAsserions;
			this.operationAssertions = unrecognizedAssertionsBindingElement.OperationAssertions;
			this.messageAssertions = unrecognizedAssertionsBindingElement.MessageAssertions;
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x001008AC File Offset: 0x000FEAAC
		protected override bool SerializeToXmlElement(XmlWriter writer, string elementName)
		{
			XmlDocument document = new XmlDocument();
			if (writer == null || this.bindingAsserions == null || this.bindingAsserions.Count <= 0)
			{
				return false;
			}
			int num = 1;
			XmlWriterSettings settings = this.WriterSettings(writer);
			this.WriteComment(SR.GetString("UnrecognizedBindingAssertions1", new object[]
			{
				this.wsdlBinding.Namespace
			}), num, writer, settings);
			this.WriteComment(string.Format(CultureInfo.InvariantCulture, "<wsdl:binding name='{0}'>", new object[]
			{
				this.wsdlBinding.Name
			}), num, writer, settings);
			num++;
			foreach (XmlElement e in this.bindingAsserions)
			{
				this.WriteComment(this.ToString(e, document), num, writer, settings);
			}
			if (this.operationAssertions == null || this.operationAssertions.Count == 0)
			{
				return true;
			}
			foreach (OperationDescription operationDescription in this.operationAssertions.Keys)
			{
				this.WriteComment(string.Format(CultureInfo.InvariantCulture, "<wsdl:operation name='{0}'>", new object[]
				{
					operationDescription.Name
				}), num, writer, settings);
				num++;
				foreach (XmlElement e2 in this.operationAssertions[operationDescription])
				{
					this.WriteComment(this.ToString(e2, document), num, writer, settings);
				}
				if (this.messageAssertions == null || this.messageAssertions.Count == 0)
				{
					return true;
				}
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					ICollection<XmlElement> collection;
					if (this.messageAssertions.TryGetValue(messageDescription, out collection))
					{
						if (messageDescription.Direction == MessageDirection.Input)
						{
							this.WriteComment("<wsdl:input>", num, writer, settings);
						}
						else if (messageDescription.Direction == MessageDirection.Output)
						{
							this.WriteComment("<wsdl:output>", num, writer, settings);
						}
						foreach (XmlElement e3 in collection)
						{
							this.WriteComment(this.ToString(e3, document), num + 1, writer, settings);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x00100B9C File Offset: 0x000FED9C
		protected override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			if (sourceElement is UnrecognizedPolicyAssertionElement)
			{
				this.wsdlBinding = ((UnrecognizedPolicyAssertionElement)sourceElement).wsdlBinding;
				this.bindingAsserions = ((UnrecognizedPolicyAssertionElement)sourceElement).bindingAsserions;
				this.operationAssertions = ((UnrecognizedPolicyAssertionElement)sourceElement).operationAssertions;
				this.messageAssertions = ((UnrecognizedPolicyAssertionElement)sourceElement).messageAssertions;
			}
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x00100C00 File Offset: 0x000FEE00
		private string ToString(XmlElement e, XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement(e.Prefix, e.LocalName, e.NamespaceURI);
			xmlElement.InsertBefore(document.CreateTextNode(".."), null);
			return xmlElement.OuterXml;
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x00100C40 File Offset: 0x000FEE40
		private void WriteComment(string text, int indent, XmlWriter writer, XmlWriterSettings settings)
		{
			if (settings.Indent)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < indent; i++)
				{
					stringBuilder.Append(settings.IndentChars);
				}
				stringBuilder.Append(text);
				stringBuilder.Append(settings.IndentChars);
				text = stringBuilder.ToString();
			}
			writer.WriteComment(text);
		}

		// Token: 0x060043F3 RID: 17395 RVA: 0x00100C9C File Offset: 0x000FEE9C
		private XmlWriterSettings WriterSettings(XmlWriter writer)
		{
			if (writer.Settings == null)
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				XmlTextWriter xmlTextWriter = writer as XmlTextWriter;
				if (xmlTextWriter != null)
				{
					xmlWriterSettings.Indent = (xmlTextWriter.Formatting == Formatting.Indented);
					if (xmlWriterSettings.Indent && xmlTextWriter.Indentation > 0)
					{
						StringBuilder stringBuilder = new StringBuilder(xmlTextWriter.Indentation);
						for (int i = 0; i < xmlTextWriter.Indentation; i++)
						{
							stringBuilder.Append(xmlTextWriter.IndentChar);
						}
						xmlWriterSettings.IndentChars = stringBuilder.ToString();
					}
				}
				return xmlWriterSettings;
			}
			return writer.Settings;
		}

		// Token: 0x04002D29 RID: 11561
		private XmlQualifiedName wsdlBinding;

		// Token: 0x04002D2A RID: 11562
		private ICollection<XmlElement> bindingAsserions;

		// Token: 0x04002D2B RID: 11563
		private IDictionary<OperationDescription, ICollection<XmlElement>> operationAssertions;

		// Token: 0x04002D2C RID: 11564
		private IDictionary<MessageDescription, ICollection<XmlElement>> messageAssertions;
	}
}
