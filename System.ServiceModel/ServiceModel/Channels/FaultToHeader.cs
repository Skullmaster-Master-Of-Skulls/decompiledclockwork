using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A9 RID: 2473
	internal class FaultToHeader : AddressingHeader
	{
		// Token: 0x060060F4 RID: 24820 RVA: 0x0016A164 File Offset: 0x00168364
		private FaultToHeader(EndpointAddress faultTo, AddressingVersion version) : base(version)
		{
			this.faultTo = faultTo;
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x060060F5 RID: 24821 RVA: 0x0016A174 File Offset: 0x00168374
		public EndpointAddress FaultTo
		{
			get
			{
				return this.faultTo;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x0016A17C File Offset: 0x0016837C
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.AddressingDictionary.FaultTo;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x060060F7 RID: 24823 RVA: 0x0016A188 File Offset: 0x00168388
		public override bool MustUnderstand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x0016A18B File Offset: 0x0016838B
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.faultTo.WriteContentsTo(base.Version, writer);
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x0016A19F File Offset: 0x0016839F
		public static FaultToHeader Create(EndpointAddress faultTo, AddressingVersion addressingVersion)
		{
			if (faultTo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("faultTo"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			return new FaultToHeader(faultTo, addressingVersion);
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x0016A1DC File Offset: 0x001683DC
		public static FaultToHeader ReadHeader(XmlDictionaryReader reader, AddressingVersion version, string actor, bool mustUnderstand, bool relay)
		{
			EndpointAddress endpointAddress = FaultToHeader.ReadHeaderValue(reader, version);
			if (actor.Length == 0 && !mustUnderstand && !relay)
			{
				return new FaultToHeader(endpointAddress, version);
			}
			return new FaultToHeader.FullFaultToHeader(endpointAddress, actor, mustUnderstand, relay, version);
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x0016A213 File Offset: 0x00168413
		public static EndpointAddress ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion version)
		{
			return EndpointAddress.ReadFrom(version, reader);
		}

		// Token: 0x040038B0 RID: 14512
		private EndpointAddress faultTo;

		// Token: 0x040038B1 RID: 14513
		private const bool mustUnderstandValue = false;

		// Token: 0x02000E36 RID: 3638
		private class FullFaultToHeader : FaultToHeader
		{
			// Token: 0x06008293 RID: 33427 RVA: 0x001E30C6 File Offset: 0x001E12C6
			public FullFaultToHeader(EndpointAddress faultTo, string actor, bool mustUnderstand, bool relay, AddressingVersion version) : base(faultTo, version)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
			}

			// Token: 0x17001CCF RID: 7375
			// (get) Token: 0x06008294 RID: 33428 RVA: 0x001E30E7 File Offset: 0x001E12E7
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CD0 RID: 7376
			// (get) Token: 0x06008295 RID: 33429 RVA: 0x001E30EF File Offset: 0x001E12EF
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CD1 RID: 7377
			// (get) Token: 0x06008296 RID: 33430 RVA: 0x001E30F7 File Offset: 0x001E12F7
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x04004A21 RID: 18977
			private string actor;

			// Token: 0x04004A22 RID: 18978
			private bool mustUnderstand;

			// Token: 0x04004A23 RID: 18979
			private bool relay;
		}
	}
}
