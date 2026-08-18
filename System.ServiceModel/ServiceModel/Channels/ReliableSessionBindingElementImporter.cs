using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200097F RID: 2431
	public sealed class ReliableSessionBindingElementImporter : IPolicyImportExtension
	{
		// Token: 0x06005DF3 RID: 24051 RVA: 0x0015B544 File Offset: 0x00159744
		void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
		{
			if (importer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("importer");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			bool flag = false;
			XmlElement xmlElement = PolicyConversionContext.FindAssertion(context.GetBindingAssertions(), "RMAssertion", "http://schemas.xmlsoap.org/ws/2005/02/rm/policy", true);
			if (xmlElement != null)
			{
				ReliableSessionBindingElementImporter.ProcessReliableSessionFeb2005Assertion(xmlElement, ReliableSessionBindingElementImporter.GetReliableSessionBindingElement(context));
				flag = true;
			}
			xmlElement = PolicyConversionContext.FindAssertion(context.GetBindingAssertions(), "RMAssertion", "http://docs.oasis-open.org/ws-rx/wsrmp/200702", true);
			if (xmlElement != null)
			{
				if (flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("MultipleVersionsFoundInPolicy", new object[]
					{
						"RMAssertion"
					})));
				}
				ReliableSessionBindingElementImporter.ProcessReliableSession11Assertion(importer, xmlElement, ReliableSessionBindingElementImporter.GetReliableSessionBindingElement(context));
			}
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x0015B5F4 File Offset: 0x001597F4
		private static ReliableSessionBindingElement GetReliableSessionBindingElement(PolicyConversionContext context)
		{
			ReliableSessionBindingElement reliableSessionBindingElement = context.BindingElements.Find<ReliableSessionBindingElement>();
			if (reliableSessionBindingElement == null)
			{
				reliableSessionBindingElement = new ReliableSessionBindingElement();
				context.BindingElements.Add(reliableSessionBindingElement);
			}
			return reliableSessionBindingElement;
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x0015B623 File Offset: 0x00159823
		private static bool Is11Assertion(XmlNode node, string assertion)
		{
			return ReliableSessionBindingElementImporter.IsElement(node, "http://schemas.microsoft.com/ws-rx/wsrmp/200702", assertion);
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x0015B631 File Offset: 0x00159831
		private static bool IsElement(XmlNode node, string ns, string assertion)
		{
			if (assertion == null)
			{
				throw Fx.AssertAndThrow("Argument assertion cannot be null.");
			}
			return node != null && node.NodeType == XmlNodeType.Element && node.NamespaceURI == ns && node.LocalName == assertion;
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x0015B669 File Offset: 0x00159869
		private static bool IsFeb2005Assertion(XmlNode node, string assertion)
		{
			return ReliableSessionBindingElementImporter.IsElement(node, "http://schemas.xmlsoap.org/ws/2005/02/rm/policy", assertion);
		}

		// Token: 0x06005DF8 RID: 24056 RVA: 0x0015B678 File Offset: 0x00159878
		private static void ProcessReliableSession11Assertion(MetadataImporter importer, XmlElement element, ReliableSessionBindingElement settings)
		{
			settings.ReliableMessagingVersion = ReliableMessagingVersion.WSReliableMessaging11;
			IEnumerator enumerator = element.ChildNodes.GetEnumerator();
			XmlNode xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
			ReliableSessionBindingElementImporter.ProcessWsrm11Policy(importer, xmlNode, settings);
			xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
			ReliableSessionBindingElementImporter.State state = ReliableSessionBindingElementImporter.State.InactivityTimeout;
			while (xmlNode != null)
			{
				if (state == ReliableSessionBindingElementImporter.State.InactivityTimeout && ReliableSessionBindingElementImporter.Is11Assertion(xmlNode, "InactivityTimeout"))
				{
					ReliableSessionBindingElementImporter.SetInactivityTimeout(settings, ReliableSessionBindingElementImporter.ReadMillisecondsAttribute(xmlNode, true), xmlNode.LocalName);
					state = ReliableSessionBindingElementImporter.State.AcknowledgementInterval;
					xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
				}
				else
				{
					if (ReliableSessionBindingElementImporter.Is11Assertion(xmlNode, "AcknowledgementInterval"))
					{
						ReliableSessionBindingElementImporter.SetAcknowledgementInterval(settings, ReliableSessionBindingElementImporter.ReadMillisecondsAttribute(xmlNode, true), xmlNode.LocalName);
						return;
					}
					if (state == ReliableSessionBindingElementImporter.State.AcknowledgementInterval)
					{
						break;
					}
					xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
				}
			}
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x0015B714 File Offset: 0x00159914
		private static void ProcessReliableSessionFeb2005Assertion(XmlElement element, ReliableSessionBindingElement settings)
		{
			settings.ReliableMessagingVersion = ReliableMessagingVersion.WSReliableMessagingFebruary2005;
			IEnumerator enumerator = element.ChildNodes.GetEnumerator();
			XmlNode xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
			if (ReliableSessionBindingElementImporter.IsFeb2005Assertion(xmlNode, "InactivityTimeout"))
			{
				ReliableSessionBindingElementImporter.SetInactivityTimeout(settings, ReliableSessionBindingElementImporter.ReadMillisecondsAttribute(xmlNode, true), xmlNode.LocalName);
				xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
			}
			if (ReliableSessionBindingElementImporter.IsFeb2005Assertion(xmlNode, "BaseRetransmissionInterval"))
			{
				ReliableSessionBindingElementImporter.ReadMillisecondsAttribute(xmlNode, false);
				xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
			}
			if (ReliableSessionBindingElementImporter.IsFeb2005Assertion(xmlNode, "ExponentialBackoff"))
			{
				xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
			}
			if (ReliableSessionBindingElementImporter.IsFeb2005Assertion(xmlNode, "AcknowledgementInterval"))
			{
				ReliableSessionBindingElementImporter.SetAcknowledgementInterval(settings, ReliableSessionBindingElementImporter.ReadMillisecondsAttribute(xmlNode, true), xmlNode.LocalName);
			}
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x0015B7B8 File Offset: 0x001599B8
		private static void ProcessWsrm11Policy(MetadataImporter importer, XmlNode node, ReliableSessionBindingElement settings)
		{
			XmlElement xmlElement = ReliableSessionBindingElementImporter.ThrowIfNotPolicyElement(node, ReliableMessagingVersion.WSReliableMessaging11);
			IEnumerable<IEnumerable<XmlElement>> enumerable = importer.NormalizePolicy(new XmlElement[]
			{
				xmlElement
			});
			List<ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative> list = new List<ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative>();
			foreach (IEnumerable<XmlElement> alternative in enumerable)
			{
				ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative item = ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative.ImportAlternative(importer, alternative);
				list.Add(item);
			}
			if (list.Count == 0)
			{
				return;
			}
			foreach (ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative wsrm11PolicyAlternative in list)
			{
				if (wsrm11PolicyAlternative.HasValidPolicy)
				{
					wsrm11PolicyAlternative.TransferSettings(settings);
					return;
				}
			}
			ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative.ThrowInvalidBindingException();
		}

		// Token: 0x06005DFB RID: 24059 RVA: 0x0015B888 File Offset: 0x00159A88
		private static TimeSpan ReadMillisecondsAttribute(XmlNode wsrmNode, bool convertToTimeSpan)
		{
			XmlAttribute xmlAttribute = wsrmNode.Attributes["Milliseconds"];
			if (xmlAttribute == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("RequiredAttributeIsMissing", new object[]
				{
					"Milliseconds",
					wsrmNode.LocalName,
					"RMAssertion"
				})));
			}
			ulong value = 0UL;
			Exception ex = null;
			try
			{
				value = XmlConvert.ToUInt64(xmlAttribute.Value);
			}
			catch (FormatException ex2)
			{
				ex = ex2;
			}
			catch (OverflowException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("RequiredMillisecondsAttributeIncorrect", new object[]
				{
					wsrmNode.LocalName
				}), ex));
			}
			if (convertToTimeSpan)
			{
				TimeSpan result;
				try
				{
					result = TimeSpan.FromMilliseconds(Convert.ToDouble(value));
				}
				catch (OverflowException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("MillisecondsNotConvertibleToBindingRange", new object[]
					{
						wsrmNode.LocalName
					}), innerException));
				}
				return result;
			}
			return default(TimeSpan);
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0015B9A4 File Offset: 0x00159BA4
		private static void SetInactivityTimeout(ReliableSessionBindingElement settings, TimeSpan inactivityTimeout, string localName)
		{
			try
			{
				settings.InactivityTimeout = inactivityTimeout;
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("MillisecondsNotConvertibleToBindingRange", new object[]
				{
					localName
				}), innerException));
			}
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x0015B9F0 File Offset: 0x00159BF0
		private static void SetAcknowledgementInterval(ReliableSessionBindingElement settings, TimeSpan acknowledgementInterval, string localName)
		{
			try
			{
				settings.AcknowledgementInterval = acknowledgementInterval;
			}
			catch (ArgumentOutOfRangeException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("MillisecondsNotConvertibleToBindingRange", new object[]
				{
					localName
				}), innerException));
			}
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0015BA3C File Offset: 0x00159C3C
		private static bool ShouldSkipNodeType(XmlNodeType type)
		{
			return type == XmlNodeType.Comment || type == XmlNodeType.SignificantWhitespace || type == XmlNodeType.Whitespace || type == XmlNodeType.Notation;
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x0015BA54 File Offset: 0x00159C54
		private static XmlNode SkipToNode(IEnumerator nodes)
		{
			while (nodes.MoveNext())
			{
				object obj = nodes.Current;
				XmlNode xmlNode = (XmlNode)obj;
				if (!ReliableSessionBindingElementImporter.ShouldSkipNodeType(xmlNode.NodeType))
				{
					return xmlNode;
				}
			}
			return null;
		}

		// Token: 0x06005E00 RID: 24064 RVA: 0x0015BA88 File Offset: 0x00159C88
		private static XmlElement ThrowIfNotPolicyElement(XmlNode node, ReliableMessagingVersion reliableMessagingVersion)
		{
			string assertion = "Policy";
			if (!ReliableSessionBindingElementImporter.IsElement(node, "http://schemas.xmlsoap.org/ws/2004/09/policy", assertion) && !ReliableSessionBindingElementImporter.IsElement(node, "http://www.w3.org/ns/ws-policy", assertion))
			{
				string text = (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005) ? "wsrm" : "wsrmp";
				string message = (node == null) ? SR.GetString("ElementRequired", new object[]
				{
					text,
					"RMAssertion",
					"wsp",
					"Policy"
				}) : SR.GetString("ElementFound", new object[]
				{
					text,
					"RMAssertion",
					"wsp",
					"Policy",
					node.LocalName,
					node.NamespaceURI
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(message));
			}
			return (XmlElement)node;
		}

		// Token: 0x02000DF0 RID: 3568
		private class Wsrm11PolicyAlternative
		{
			// Token: 0x17001C7F RID: 7295
			// (get) Token: 0x060080E6 RID: 32998 RVA: 0x001DE809 File Offset: 0x001DCA09
			public bool HasValidPolicy
			{
				get
				{
					return this.hasValidPolicy;
				}
			}

			// Token: 0x060080E7 RID: 32999 RVA: 0x001DE814 File Offset: 0x001DCA14
			public static ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative ImportAlternative(MetadataImporter importer, IEnumerable<XmlElement> alternative)
			{
				ReliableSessionBindingElementImporter.State state = ReliableSessionBindingElementImporter.State.Security;
				ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative wsrm11PolicyAlternative = new ReliableSessionBindingElementImporter.Wsrm11PolicyAlternative();
				foreach (XmlElement xmlElement in alternative)
				{
					if (state == ReliableSessionBindingElementImporter.State.Security)
					{
						state = ReliableSessionBindingElementImporter.State.DeliveryAssurance;
						if (wsrm11PolicyAlternative.TryImportSequenceSTR(xmlElement))
						{
							continue;
						}
					}
					if (state == ReliableSessionBindingElementImporter.State.DeliveryAssurance)
					{
						state = ReliableSessionBindingElementImporter.State.Done;
						if (wsrm11PolicyAlternative.TryImportDeliveryAssurance(importer, xmlElement))
						{
							continue;
						}
					}
					string @string = SR.GetString("UnexpectedXmlChildNode", new object[]
					{
						xmlElement.LocalName,
						xmlElement.NodeType,
						"RMAssertion"
					});
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(@string));
				}
				return wsrm11PolicyAlternative;
			}

			// Token: 0x060080E8 RID: 33000 RVA: 0x001DE8C0 File Offset: 0x001DCAC0
			public static void ThrowInvalidBindingException()
			{
				string @string = SR.GetString("AssertionNotSupported", new object[]
				{
					"wsrmp",
					"SequenceTransportSecurity"
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(@string));
			}

			// Token: 0x060080E9 RID: 33001 RVA: 0x001DE8FE File Offset: 0x001DCAFE
			public void TransferSettings(ReliableSessionBindingElement settings)
			{
				settings.Ordered = this.isOrdered;
			}

			// Token: 0x060080EA RID: 33002 RVA: 0x001DE90C File Offset: 0x001DCB0C
			private bool TryImportSequenceSTR(XmlElement node)
			{
				string ns = "http://docs.oasis-open.org/ws-rx/wsrmp/200702";
				if (ReliableSessionBindingElementImporter.IsElement(node, ns, "SequenceSTR"))
				{
					return true;
				}
				if (ReliableSessionBindingElementImporter.IsElement(node, ns, "SequenceTransportSecurity"))
				{
					this.hasValidPolicy = false;
					return true;
				}
				return false;
			}

			// Token: 0x060080EB RID: 33003 RVA: 0x001DE948 File Offset: 0x001DCB48
			private bool TryImportDeliveryAssurance(MetadataImporter importer, XmlElement node)
			{
				string text = "http://docs.oasis-open.org/ws-rx/wsrmp/200702";
				if (!ReliableSessionBindingElementImporter.IsElement(node, text, "DeliveryAssurance"))
				{
					return false;
				}
				IEnumerator enumerator = node.ChildNodes.GetEnumerator();
				XmlNode xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
				XmlElement xmlElement = ReliableSessionBindingElementImporter.ThrowIfNotPolicyElement(xmlNode, ReliableMessagingVersion.WSReliableMessaging11);
				IEnumerable<IEnumerable<XmlElement>> enumerable = importer.NormalizePolicy(new XmlElement[]
				{
					xmlElement
				});
				foreach (IEnumerable<XmlElement> enumerable2 in enumerable)
				{
					ReliableSessionBindingElementImporter.State state = ReliableSessionBindingElementImporter.State.Assurance;
					foreach (XmlElement xmlElement2 in enumerable2)
					{
						if (state != ReliableSessionBindingElementImporter.State.Assurance)
						{
							if (state == ReliableSessionBindingElementImporter.State.Order)
							{
								state = ReliableSessionBindingElementImporter.State.Done;
								if (ReliableSessionBindingElementImporter.IsElement(xmlElement2, text, "InOrder"))
								{
									if (!this.isOrdered)
									{
										this.isOrdered = true;
										continue;
									}
									continue;
								}
							}
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("UnexpectedXmlChildNode", new object[]
							{
								xmlElement2.LocalName,
								xmlElement2.NodeType,
								"DeliveryAssurance"
							})));
						}
						state = ReliableSessionBindingElementImporter.State.Order;
						if (!ReliableSessionBindingElementImporter.IsElement(xmlElement2, text, "ExactlyOnce") && !ReliableSessionBindingElementImporter.IsElement(xmlElement2, text, "AtMostOnce") && !ReliableSessionBindingElementImporter.IsElement(xmlElement2, text, "AtMostOnce"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("DeliveryAssuranceRequired", new object[]
							{
								text,
								xmlElement2.LocalName,
								xmlElement2.NamespaceURI
							})));
						}
					}
					if (state == ReliableSessionBindingElementImporter.State.Assurance)
					{
						string @string = SR.GetString("DeliveryAssuranceRequiredNothingFound", new object[]
						{
							text
						});
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(@string));
					}
				}
				xmlNode = ReliableSessionBindingElementImporter.SkipToNode(enumerator);
				if (xmlNode != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidChannelBindingException(SR.GetString("UnexpectedXmlChildNode", new object[]
					{
						xmlNode.LocalName,
						xmlNode.NodeType,
						node.LocalName
					})));
				}
				return true;
			}

			// Token: 0x0400497E RID: 18814
			private bool hasValidPolicy = true;

			// Token: 0x0400497F RID: 18815
			private bool isOrdered;
		}

		// Token: 0x02000DF1 RID: 3569
		private enum State
		{
			// Token: 0x04004981 RID: 18817
			Security,
			// Token: 0x04004982 RID: 18818
			DeliveryAssurance,
			// Token: 0x04004983 RID: 18819
			Assurance,
			// Token: 0x04004984 RID: 18820
			Order,
			// Token: 0x04004985 RID: 18821
			InactivityTimeout,
			// Token: 0x04004986 RID: 18822
			AcknowledgementInterval,
			// Token: 0x04004987 RID: 18823
			Done
		}
	}
}
