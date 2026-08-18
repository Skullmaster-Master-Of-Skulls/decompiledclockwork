using System;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009CE RID: 2510
	internal class XmlObjectSerializerHeader : MessageHeader
	{
		// Token: 0x060062B5 RID: 25269 RVA: 0x0016F804 File Offset: 0x0016DA04
		private XmlObjectSerializerHeader(XmlObjectSerializer serializer, bool mustUnderstand, string actor, bool relay)
		{
			if (actor == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("actor");
			}
			this.mustUnderstand = mustUnderstand;
			this.relay = relay;
			this.serializer = serializer;
			this.actor = actor;
			if (actor == EnvelopeVersion.Soap12.UltimateDestinationActor)
			{
				this.isOneOneSupported = false;
				this.isOneTwoSupported = true;
				return;
			}
			if (actor == EnvelopeVersion.Soap12.NextDestinationActorValue)
			{
				this.isOneOneSupported = false;
				this.isOneTwoSupported = true;
				return;
			}
			if (actor == EnvelopeVersion.Soap11.NextDestinationActorValue)
			{
				this.isOneOneSupported = true;
				this.isOneTwoSupported = false;
				return;
			}
			this.isOneOneSupported = true;
			this.isOneTwoSupported = true;
			this.isNoneSupported = true;
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x0016F8CC File Offset: 0x0016DACC
		public XmlObjectSerializerHeader(string name, string ns, object objectToSerialize, XmlObjectSerializer serializer, bool mustUnderstand, string actor, bool relay) : this(serializer, mustUnderstand, actor, relay)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (name.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFXHeaderNameCannotBeNullOrEmpty"), "name"));
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			if (ns.Length > 0)
			{
				NamingHelper.CheckUriParameter(ns, "ns");
			}
			this.objectToSerialize = objectToSerialize;
			this.name = name;
			this.ns = ns;
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x0016F964 File Offset: 0x0016DB64
		public override bool IsMessageVersionSupported(MessageVersion messageVersion)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageVersion");
			}
			if (messageVersion.Envelope == EnvelopeVersion.Soap12)
			{
				return this.isOneTwoSupported;
			}
			if (messageVersion.Envelope == EnvelopeVersion.Soap11)
			{
				return this.isOneOneSupported;
			}
			if (messageVersion.Envelope == EnvelopeVersion.None)
			{
				return this.isNoneSupported;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("EnvelopeVersionUnknown", new object[]
			{
				messageVersion.Envelope.ToString()
			})));
		}

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x060062B8 RID: 25272 RVA: 0x0016F9ED File Offset: 0x0016DBED
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x060062B9 RID: 25273 RVA: 0x0016F9F5 File Offset: 0x0016DBF5
		public override string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x0016F9FD File Offset: 0x0016DBFD
		public override bool MustUnderstand
		{
			get
			{
				return this.mustUnderstand;
			}
		}

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x060062BB RID: 25275 RVA: 0x0016FA05 File Offset: 0x0016DC05
		public override bool Relay
		{
			get
			{
				return this.relay;
			}
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x060062BC RID: 25276 RVA: 0x0016FA0D File Offset: 0x0016DC0D
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x0016FA18 File Offset: 0x0016DC18
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.serializer == null)
				{
					this.serializer = DataContractSerializerDefaults.CreateSerializer((this.objectToSerialize == null) ? typeof(object) : this.objectToSerialize.GetType(), this.Name, this.Namespace, int.MaxValue);
				}
				this.serializer.WriteObjectContent(writer, this.objectToSerialize);
			}
		}

		// Token: 0x0400392A RID: 14634
		private XmlObjectSerializer serializer;

		// Token: 0x0400392B RID: 14635
		private bool mustUnderstand;

		// Token: 0x0400392C RID: 14636
		private bool relay;

		// Token: 0x0400392D RID: 14637
		private bool isOneTwoSupported;

		// Token: 0x0400392E RID: 14638
		private bool isOneOneSupported;

		// Token: 0x0400392F RID: 14639
		private bool isNoneSupported;

		// Token: 0x04003930 RID: 14640
		private object objectToSerialize;

		// Token: 0x04003931 RID: 14641
		private string name;

		// Token: 0x04003932 RID: 14642
		private string ns;

		// Token: 0x04003933 RID: 14643
		private string actor;

		// Token: 0x04003934 RID: 14644
		private object syncRoot = new object();
	}
}
