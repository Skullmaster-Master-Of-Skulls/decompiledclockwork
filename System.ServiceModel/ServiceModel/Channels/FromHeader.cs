using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A8 RID: 2472
	internal class FromHeader : AddressingHeader
	{
		// Token: 0x060060EC RID: 24812 RVA: 0x0016A0AF File Offset: 0x001682AF
		private FromHeader(EndpointAddress from, AddressingVersion version) : base(version)
		{
			this.from = from;
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x060060ED RID: 24813 RVA: 0x0016A0BF File Offset: 0x001682BF
		public EndpointAddress From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x060060EE RID: 24814 RVA: 0x0016A0C7 File Offset: 0x001682C7
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.AddressingDictionary.From;
			}
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x060060EF RID: 24815 RVA: 0x0016A0D3 File Offset: 0x001682D3
		public override bool MustUnderstand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x0016A0D6 File Offset: 0x001682D6
		public static FromHeader Create(EndpointAddress from, AddressingVersion addressingVersion)
		{
			if (from == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("from"));
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			return new FromHeader(from, addressingVersion);
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x0016A110 File Offset: 0x00168310
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			this.from.WriteContentsTo(base.Version, writer);
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x0016A124 File Offset: 0x00168324
		public static FromHeader ReadHeader(XmlDictionaryReader reader, AddressingVersion version, string actor, bool mustUnderstand, bool relay)
		{
			EndpointAddress endpointAddress = FromHeader.ReadHeaderValue(reader, version);
			if (actor.Length == 0 && !mustUnderstand && !relay)
			{
				return new FromHeader(endpointAddress, version);
			}
			return new FromHeader.FullFromHeader(endpointAddress, actor, mustUnderstand, relay, version);
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x0016A15B File Offset: 0x0016835B
		public static EndpointAddress ReadHeaderValue(XmlDictionaryReader reader, AddressingVersion addressingVersion)
		{
			return EndpointAddress.ReadFrom(addressingVersion, reader);
		}

		// Token: 0x040038AE RID: 14510
		private EndpointAddress from;

		// Token: 0x040038AF RID: 14511
		private const bool mustUnderstandValue = false;

		// Token: 0x02000E35 RID: 3637
		private class FullFromHeader : FromHeader
		{
			// Token: 0x0600828F RID: 33423 RVA: 0x001E308D File Offset: 0x001E128D
			public FullFromHeader(EndpointAddress from, string actor, bool mustUnderstand, bool relay, AddressingVersion version) : base(from, version)
			{
				this.actor = actor;
				this.mustUnderstand = mustUnderstand;
				this.relay = relay;
			}

			// Token: 0x17001CCC RID: 7372
			// (get) Token: 0x06008290 RID: 33424 RVA: 0x001E30AE File Offset: 0x001E12AE
			public override string Actor
			{
				get
				{
					return this.actor;
				}
			}

			// Token: 0x17001CCD RID: 7373
			// (get) Token: 0x06008291 RID: 33425 RVA: 0x001E30B6 File Offset: 0x001E12B6
			public override bool MustUnderstand
			{
				get
				{
					return this.mustUnderstand;
				}
			}

			// Token: 0x17001CCE RID: 7374
			// (get) Token: 0x06008292 RID: 33426 RVA: 0x001E30BE File Offset: 0x001E12BE
			public override bool Relay
			{
				get
				{
					return this.relay;
				}
			}

			// Token: 0x04004A1E RID: 18974
			private string actor;

			// Token: 0x04004A1F RID: 18975
			private bool mustUnderstand;

			// Token: 0x04004A20 RID: 18976
			private bool relay;
		}
	}
}
