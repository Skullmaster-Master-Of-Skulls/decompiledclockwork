using System;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000123 RID: 291
	[__DynamicallyInvokable]
	public sealed class EnvelopeVersion
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0002074C File Offset: 0x0001E94C
		private EnvelopeVersion(string ultimateReceiverActor, string nextDestinationActorValue, string ns, XmlDictionaryString dictionaryNs, string actor, XmlDictionaryString dictionaryActor, string toStringFormat, string senderFaultName, string receiverFaultName)
		{
			this.toStringFormat = toStringFormat;
			this.ultimateDestinationActor = ultimateReceiverActor;
			this.nextDestinationActorValue = nextDestinationActorValue;
			this.ns = ns;
			this.dictionaryNs = dictionaryNs;
			this.actor = actor;
			this.dictionaryActor = dictionaryActor;
			this.senderFaultName = senderFaultName;
			this.receiverFaultName = receiverFaultName;
			if (ultimateReceiverActor != null)
			{
				if (ultimateReceiverActor.Length == 0)
				{
					this.mustUnderstandActorValues = new string[]
					{
						"",
						nextDestinationActorValue
					};
					this.ultimateDestinationActorValues = new string[]
					{
						"",
						nextDestinationActorValue
					};
					return;
				}
				this.mustUnderstandActorValues = new string[]
				{
					"",
					ultimateReceiverActor,
					nextDestinationActorValue
				};
				this.ultimateDestinationActorValues = new string[]
				{
					"",
					ultimateReceiverActor,
					nextDestinationActorValue
				};
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00020818 File Offset: 0x0001EA18
		internal string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00020820 File Offset: 0x0001EA20
		internal XmlDictionaryString DictionaryActor
		{
			get
			{
				return this.dictionaryActor;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00020828 File Offset: 0x0001EA28
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00020830 File Offset: 0x0001EA30
		internal XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return this.dictionaryNs;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00020838 File Offset: 0x0001EA38
		[__DynamicallyInvokable]
		public string NextDestinationActorValue
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nextDestinationActorValue;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00020840 File Offset: 0x0001EA40
		[__DynamicallyInvokable]
		public static EnvelopeVersion None
		{
			[__DynamicallyInvokable]
			get
			{
				return EnvelopeVersion.none;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00020847 File Offset: 0x0001EA47
		[__DynamicallyInvokable]
		public static EnvelopeVersion Soap11
		{
			[__DynamicallyInvokable]
			get
			{
				return EnvelopeVersion.soap11;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0002084E File Offset: 0x0001EA4E
		[__DynamicallyInvokable]
		public static EnvelopeVersion Soap12
		{
			[__DynamicallyInvokable]
			get
			{
				return EnvelopeVersion.soap12;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00020855 File Offset: 0x0001EA55
		internal string ReceiverFaultName
		{
			get
			{
				return this.receiverFaultName;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0002085D File Offset: 0x0001EA5D
		internal string SenderFaultName
		{
			get
			{
				return this.senderFaultName;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00020865 File Offset: 0x0001EA65
		internal string[] MustUnderstandActorValues
		{
			get
			{
				return this.mustUnderstandActorValues;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0002086D File Offset: 0x0001EA6D
		internal string UltimateDestinationActor
		{
			get
			{
				return this.ultimateDestinationActor;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00020875 File Offset: 0x0001EA75
		[__DynamicallyInvokable]
		public string[] GetUltimateDestinationActorValues()
		{
			return (string[])this.ultimateDestinationActorValues.Clone();
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00020887 File Offset: 0x0001EA87
		internal string[] UltimateDestinationActorValues
		{
			get
			{
				return this.ultimateDestinationActorValues;
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002088F File Offset: 0x0001EA8F
		internal bool IsUltimateDestinationActor(string actor)
		{
			return actor.Length == 0 || actor == this.ultimateDestinationActor || actor == this.nextDestinationActorValue;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x000208B5 File Offset: 0x0001EAB5
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return SR.GetString(this.toStringFormat, new object[]
			{
				this.Namespace
			});
		}

		// Token: 0x04000AE2 RID: 2786
		private string ultimateDestinationActor;

		// Token: 0x04000AE3 RID: 2787
		private string[] ultimateDestinationActorValues;

		// Token: 0x04000AE4 RID: 2788
		private string nextDestinationActorValue;

		// Token: 0x04000AE5 RID: 2789
		private string ns;

		// Token: 0x04000AE6 RID: 2790
		private XmlDictionaryString dictionaryNs;

		// Token: 0x04000AE7 RID: 2791
		private string actor;

		// Token: 0x04000AE8 RID: 2792
		private XmlDictionaryString dictionaryActor;

		// Token: 0x04000AE9 RID: 2793
		private string toStringFormat;

		// Token: 0x04000AEA RID: 2794
		private string[] mustUnderstandActorValues;

		// Token: 0x04000AEB RID: 2795
		private string senderFaultName;

		// Token: 0x04000AEC RID: 2796
		private string receiverFaultName;

		// Token: 0x04000AED RID: 2797
		private static EnvelopeVersion soap11 = new EnvelopeVersion("", "http://schemas.xmlsoap.org/soap/actor/next", "http://schemas.xmlsoap.org/soap/envelope/", XD.Message11Dictionary.Namespace, "actor", XD.Message11Dictionary.Actor, "Soap11ToStringFormat", "Client", "Server");

		// Token: 0x04000AEE RID: 2798
		private static EnvelopeVersion soap12 = new EnvelopeVersion("http://www.w3.org/2003/05/soap-envelope/role/ultimateReceiver", "http://www.w3.org/2003/05/soap-envelope/role/next", "http://www.w3.org/2003/05/soap-envelope", XD.Message12Dictionary.Namespace, "role", XD.Message12Dictionary.Role, "Soap12ToStringFormat", "Sender", "Receiver");

		// Token: 0x04000AEF RID: 2799
		private static EnvelopeVersion none = new EnvelopeVersion(null, null, "http://schemas.microsoft.com/ws/2005/05/envelope/none", XD.MessageDictionary.Namespace, null, null, "EnvelopeNoneToStringFormat", "Sender", "Receiver");
	}
}
