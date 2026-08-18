using System;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009AE RID: 2478
	[__DynamicallyInvokable]
	public sealed class AddressingVersion
	{
		// Token: 0x06006125 RID: 24869 RVA: 0x0016A6DC File Offset: 0x001688DC
		private AddressingVersion(string ns, XmlDictionaryString dictionaryNs, string toStringFormat, MessagePartSpecification signedMessageParts, string anonymous, XmlDictionaryString dictionaryAnonymous, string none, string faultAction, string defaultFaultAction)
		{
			this.ns = ns;
			this.dictionaryNs = dictionaryNs;
			this.toStringFormat = toStringFormat;
			this.signedMessageParts = signedMessageParts;
			this.anonymous = anonymous;
			this.dictionaryAnonymous = dictionaryAnonymous;
			if (anonymous != null)
			{
				this.anonymousUri = new Uri(anonymous);
			}
			if (none != null)
			{
				this.noneUri = new Uri(none);
			}
			this.faultAction = faultAction;
			this.defaultFaultAction = defaultFaultAction;
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06006126 RID: 24870 RVA: 0x0016A74E File Offset: 0x0016894E
		public static AddressingVersion WSAddressingAugust2004
		{
			get
			{
				return AddressingVersion.addressing200408;
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x06006127 RID: 24871 RVA: 0x0016A755 File Offset: 0x00168955
		[__DynamicallyInvokable]
		public static AddressingVersion WSAddressing10
		{
			[__DynamicallyInvokable]
			get
			{
				return AddressingVersion.addressing10;
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06006128 RID: 24872 RVA: 0x0016A75C File Offset: 0x0016895C
		[__DynamicallyInvokable]
		public static AddressingVersion None
		{
			[__DynamicallyInvokable]
			get
			{
				return AddressingVersion.none;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06006129 RID: 24873 RVA: 0x0016A763 File Offset: 0x00168963
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x0600612A RID: 24874 RVA: 0x0016A76C File Offset: 0x0016896C
		private static MessagePartSpecification Addressing10SignedMessageParts
		{
			get
			{
				if (AddressingVersion.addressing10SignedMessageParts == null)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification(new XmlQualifiedName[]
					{
						new XmlQualifiedName("To", "http://www.w3.org/2005/08/addressing"),
						new XmlQualifiedName("From", "http://www.w3.org/2005/08/addressing"),
						new XmlQualifiedName("FaultTo", "http://www.w3.org/2005/08/addressing"),
						new XmlQualifiedName("ReplyTo", "http://www.w3.org/2005/08/addressing"),
						new XmlQualifiedName("MessageID", "http://www.w3.org/2005/08/addressing"),
						new XmlQualifiedName("RelatesTo", "http://www.w3.org/2005/08/addressing"),
						new XmlQualifiedName("Action", "http://www.w3.org/2005/08/addressing")
					});
					messagePartSpecification.MakeReadOnly();
					AddressingVersion.addressing10SignedMessageParts = messagePartSpecification;
				}
				return AddressingVersion.addressing10SignedMessageParts;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x0016A820 File Offset: 0x00168A20
		private static MessagePartSpecification Addressing200408SignedMessageParts
		{
			get
			{
				if (AddressingVersion.addressing200408SignedMessageParts == null)
				{
					MessagePartSpecification messagePartSpecification = new MessagePartSpecification(new XmlQualifiedName[]
					{
						new XmlQualifiedName("To", "http://schemas.xmlsoap.org/ws/2004/08/addressing"),
						new XmlQualifiedName("From", "http://schemas.xmlsoap.org/ws/2004/08/addressing"),
						new XmlQualifiedName("FaultTo", "http://schemas.xmlsoap.org/ws/2004/08/addressing"),
						new XmlQualifiedName("ReplyTo", "http://schemas.xmlsoap.org/ws/2004/08/addressing"),
						new XmlQualifiedName("MessageID", "http://schemas.xmlsoap.org/ws/2004/08/addressing"),
						new XmlQualifiedName("RelatesTo", "http://schemas.xmlsoap.org/ws/2004/08/addressing"),
						new XmlQualifiedName("Action", "http://schemas.xmlsoap.org/ws/2004/08/addressing")
					});
					messagePartSpecification.MakeReadOnly();
					AddressingVersion.addressing200408SignedMessageParts = messagePartSpecification;
				}
				return AddressingVersion.addressing200408SignedMessageParts;
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x0016A8D2 File Offset: 0x00168AD2
		internal XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return this.dictionaryNs;
			}
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x0600612D RID: 24877 RVA: 0x0016A8DA File Offset: 0x00168ADA
		internal string Anonymous
		{
			get
			{
				return this.anonymous;
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x0600612E RID: 24878 RVA: 0x0016A8E2 File Offset: 0x00168AE2
		internal XmlDictionaryString DictionaryAnonymous
		{
			get
			{
				return this.dictionaryAnonymous;
			}
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x0600612F RID: 24879 RVA: 0x0016A8EA File Offset: 0x00168AEA
		internal Uri AnonymousUri
		{
			get
			{
				return this.anonymousUri;
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06006130 RID: 24880 RVA: 0x0016A8F2 File Offset: 0x00168AF2
		internal Uri NoneUri
		{
			get
			{
				return this.noneUri;
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06006131 RID: 24881 RVA: 0x0016A8FA File Offset: 0x00168AFA
		internal string FaultAction
		{
			get
			{
				return this.faultAction;
			}
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06006132 RID: 24882 RVA: 0x0016A902 File Offset: 0x00168B02
		internal string DefaultFaultAction
		{
			get
			{
				return this.defaultFaultAction;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06006133 RID: 24883 RVA: 0x0016A90A File Offset: 0x00168B0A
		internal MessagePartSpecification SignedMessageParts
		{
			get
			{
				return this.signedMessageParts;
			}
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x0016A912 File Offset: 0x00168B12
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return SR.GetString(this.toStringFormat, new object[]
			{
				this.Namespace
			});
		}

		// Token: 0x040038BF RID: 14527
		private string ns;

		// Token: 0x040038C0 RID: 14528
		private XmlDictionaryString dictionaryNs;

		// Token: 0x040038C1 RID: 14529
		private MessagePartSpecification signedMessageParts;

		// Token: 0x040038C2 RID: 14530
		private string toStringFormat;

		// Token: 0x040038C3 RID: 14531
		private string anonymous;

		// Token: 0x040038C4 RID: 14532
		private XmlDictionaryString dictionaryAnonymous;

		// Token: 0x040038C5 RID: 14533
		private Uri anonymousUri;

		// Token: 0x040038C6 RID: 14534
		private Uri noneUri;

		// Token: 0x040038C7 RID: 14535
		private string faultAction;

		// Token: 0x040038C8 RID: 14536
		private string defaultFaultAction;

		// Token: 0x040038C9 RID: 14537
		private static AddressingVersion none = new AddressingVersion("http://schemas.microsoft.com/ws/2005/05/addressing/none", XD.AddressingNoneDictionary.Namespace, "AddressingNoneToStringFormat", new MessagePartSpecification(), null, null, null, null, null);

		// Token: 0x040038CA RID: 14538
		private static AddressingVersion addressing10 = new AddressingVersion("http://www.w3.org/2005/08/addressing", XD.Addressing10Dictionary.Namespace, "Addressing10ToStringFormat", AddressingVersion.Addressing10SignedMessageParts, "http://www.w3.org/2005/08/addressing/anonymous", XD.Addressing10Dictionary.Anonymous, "http://www.w3.org/2005/08/addressing/none", "http://www.w3.org/2005/08/addressing/fault", "http://www.w3.org/2005/08/addressing/soap/fault");

		// Token: 0x040038CB RID: 14539
		private static MessagePartSpecification addressing10SignedMessageParts;

		// Token: 0x040038CC RID: 14540
		private static AddressingVersion addressing200408 = new AddressingVersion("http://schemas.xmlsoap.org/ws/2004/08/addressing", XD.Addressing200408Dictionary.Namespace, "Addressing200408ToStringFormat", AddressingVersion.Addressing200408SignedMessageParts, "http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous", XD.Addressing200408Dictionary.Anonymous, null, "http://schemas.xmlsoap.org/ws/2004/08/addressing/fault", "http://schemas.xmlsoap.org/ws/2004/08/addressing/fault");

		// Token: 0x040038CD RID: 14541
		private static MessagePartSpecification addressing200408SignedMessageParts;
	}
}
