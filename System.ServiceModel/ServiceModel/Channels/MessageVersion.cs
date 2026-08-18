using System;
using System.ComponentModel;
using System.Globalization;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D5 RID: 2517
	[TypeConverter(typeof(MessageVersionConverter))]
	[__DynamicallyInvokable]
	public sealed class MessageVersion
	{
		// Token: 0x0600636D RID: 25453 RVA: 0x0017387D File Offset: 0x00171A7D
		private MessageVersion(EnvelopeVersion envelopeVersion, AddressingVersion addressingVersion)
		{
			this.envelope = envelopeVersion;
			this.addressing = addressingVersion;
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x00173893 File Offset: 0x00171A93
		[__DynamicallyInvokable]
		public static MessageVersion CreateVersion(EnvelopeVersion envelopeVersion)
		{
			return MessageVersion.CreateVersion(envelopeVersion, AddressingVersion.WSAddressing10);
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x001738A0 File Offset: 0x00171AA0
		[__DynamicallyInvokable]
		public static MessageVersion CreateVersion(EnvelopeVersion envelopeVersion, AddressingVersion addressingVersion)
		{
			if (envelopeVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("envelopeVersion");
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			if (envelopeVersion == EnvelopeVersion.Soap12)
			{
				if (addressingVersion == AddressingVersion.WSAddressing10)
				{
					return MessageVersion.soap12Addressing10;
				}
				if (addressingVersion == AddressingVersion.WSAddressingAugust2004)
				{
					return MessageVersion.soap12Addressing200408;
				}
				if (addressingVersion == AddressingVersion.None)
				{
					return MessageVersion.soap12;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("addressingVersion", SR.GetString("AddressingVersionNotSupported", new object[]
				{
					addressingVersion
				}));
			}
			else if (envelopeVersion == EnvelopeVersion.Soap11)
			{
				if (addressingVersion == AddressingVersion.WSAddressing10)
				{
					return MessageVersion.soap11Addressing10;
				}
				if (addressingVersion == AddressingVersion.WSAddressingAugust2004)
				{
					return MessageVersion.soap11Addressing200408;
				}
				if (addressingVersion == AddressingVersion.None)
				{
					return MessageVersion.soap11;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("addressingVersion", SR.GetString("AddressingVersionNotSupported", new object[]
				{
					addressingVersion
				}));
			}
			else
			{
				if (envelopeVersion != EnvelopeVersion.None)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("envelopeVersion", SR.GetString("EnvelopeVersionNotSupported", new object[]
					{
						envelopeVersion
					}));
				}
				if (addressingVersion == AddressingVersion.None)
				{
					return MessageVersion.none;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("addressingVersion", SR.GetString("AddressingVersionNotSupported", new object[]
				{
					addressingVersion
				}));
			}
		}

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06006370 RID: 25456 RVA: 0x001739DC File Offset: 0x00171BDC
		[__DynamicallyInvokable]
		public AddressingVersion Addressing
		{
			[__DynamicallyInvokable]
			get
			{
				return this.addressing;
			}
		}

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x06006371 RID: 25457 RVA: 0x001739E4 File Offset: 0x00171BE4
		[__DynamicallyInvokable]
		public static MessageVersion Default
		{
			[__DynamicallyInvokable]
			get
			{
				return MessageVersion.soap12Addressing10;
			}
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06006372 RID: 25458 RVA: 0x001739EB File Offset: 0x00171BEB
		[__DynamicallyInvokable]
		public EnvelopeVersion Envelope
		{
			[__DynamicallyInvokable]
			get
			{
				return this.envelope;
			}
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x001739F3 File Offset: 0x00171BF3
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return this == obj;
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x001739FC File Offset: 0x00171BFC
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = 0;
			if (this.Envelope == EnvelopeVersion.Soap11)
			{
				num++;
			}
			if (this.Addressing == AddressingVersion.WSAddressingAugust2004)
			{
				num += 2;
			}
			return num;
		}

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x06006375 RID: 25461 RVA: 0x00173A2E File Offset: 0x00171C2E
		[__DynamicallyInvokable]
		public static MessageVersion None
		{
			[__DynamicallyInvokable]
			get
			{
				return MessageVersion.none;
			}
		}

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x06006376 RID: 25462 RVA: 0x00173A35 File Offset: 0x00171C35
		[__DynamicallyInvokable]
		public static MessageVersion Soap12WSAddressing10
		{
			[__DynamicallyInvokable]
			get
			{
				return MessageVersion.soap12Addressing10;
			}
		}

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x06006377 RID: 25463 RVA: 0x00173A3C File Offset: 0x00171C3C
		public static MessageVersion Soap11WSAddressing10
		{
			get
			{
				return MessageVersion.soap11Addressing10;
			}
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x06006378 RID: 25464 RVA: 0x00173A43 File Offset: 0x00171C43
		public static MessageVersion Soap12WSAddressingAugust2004
		{
			get
			{
				return MessageVersion.soap12Addressing200408;
			}
		}

		// Token: 0x17001800 RID: 6144
		// (get) Token: 0x06006379 RID: 25465 RVA: 0x00173A4A File Offset: 0x00171C4A
		public static MessageVersion Soap11WSAddressingAugust2004
		{
			get
			{
				return MessageVersion.soap11Addressing200408;
			}
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x0600637A RID: 25466 RVA: 0x00173A51 File Offset: 0x00171C51
		[__DynamicallyInvokable]
		public static MessageVersion Soap11
		{
			[__DynamicallyInvokable]
			get
			{
				return MessageVersion.soap11;
			}
		}

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x0600637B RID: 25467 RVA: 0x00173A58 File Offset: 0x00171C58
		public static MessageVersion Soap12
		{
			get
			{
				return MessageVersion.soap12;
			}
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x00173A5F File Offset: 0x00171C5F
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return SR.GetString("MessageVersionToStringFormat", new object[]
			{
				this.envelope.ToString(),
				this.addressing.ToString()
			});
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x00173A90 File Offset: 0x00171C90
		internal bool IsMatch(MessageVersion messageVersion)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageVersion");
			}
			if (this.addressing == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "MessageVersion.Addressing cannot be null", new object[0])));
			}
			return this.envelope == messageVersion.Envelope && !(this.addressing.Namespace != messageVersion.Addressing.Namespace);
		}

		// Token: 0x04003968 RID: 14696
		private EnvelopeVersion envelope;

		// Token: 0x04003969 RID: 14697
		private AddressingVersion addressing;

		// Token: 0x0400396A RID: 14698
		private static MessageVersion none = new MessageVersion(EnvelopeVersion.None, AddressingVersion.None);

		// Token: 0x0400396B RID: 14699
		private static MessageVersion soap11 = new MessageVersion(EnvelopeVersion.Soap11, AddressingVersion.None);

		// Token: 0x0400396C RID: 14700
		private static MessageVersion soap12 = new MessageVersion(EnvelopeVersion.Soap12, AddressingVersion.None);

		// Token: 0x0400396D RID: 14701
		private static MessageVersion soap11Addressing10 = new MessageVersion(EnvelopeVersion.Soap11, AddressingVersion.WSAddressing10);

		// Token: 0x0400396E RID: 14702
		private static MessageVersion soap12Addressing10 = new MessageVersion(EnvelopeVersion.Soap12, AddressingVersion.WSAddressing10);

		// Token: 0x0400396F RID: 14703
		private static MessageVersion soap11Addressing200408 = new MessageVersion(EnvelopeVersion.Soap11, AddressingVersion.WSAddressingAugust2004);

		// Token: 0x04003970 RID: 14704
		private static MessageVersion soap12Addressing200408 = new MessageVersion(EnvelopeVersion.Soap12, AddressingVersion.WSAddressingAugust2004);
	}
}
