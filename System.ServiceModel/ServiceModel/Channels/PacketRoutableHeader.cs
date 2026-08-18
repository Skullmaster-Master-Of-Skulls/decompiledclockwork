using System;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000891 RID: 2193
	internal class PacketRoutableHeader : DictionaryHeader
	{
		// Token: 0x06005354 RID: 21332 RVA: 0x001331BA File Offset: 0x001313BA
		private PacketRoutableHeader()
		{
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x001331C4 File Offset: 0x001313C4
		public static void AddHeadersTo(Message message, MessageHeader header)
		{
			int num = message.Headers.FindHeader("PacketRoutable", "http://schemas.microsoft.com/ws/2005/05/routing");
			if (num == -1)
			{
				if (header == null)
				{
					header = PacketRoutableHeader.Create();
				}
				message.Headers.Add(header);
			}
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x00133201 File Offset: 0x00131401
		public static void ValidateMessage(Message message)
		{
			if (!PacketRoutableHeader.TryValidateMessage(message))
			{
				throw TraceUtility.ThrowHelperError(new ProtocolException(SR.GetString("OneWayHeaderNotFound")), message);
			}
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00133224 File Offset: 0x00131424
		public static bool TryValidateMessage(Message message)
		{
			int num = message.Headers.FindHeader("PacketRoutable", "http://schemas.microsoft.com/ws/2005/05/routing");
			return num != -1;
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x0013324E File Offset: 0x0013144E
		public static PacketRoutableHeader Create()
		{
			return new PacketRoutableHeader();
		}

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x00133255 File Offset: 0x00131455
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.DotNetOneWayDictionary.HeaderName;
			}
		}

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x0600535A RID: 21338 RVA: 0x00133261 File Offset: 0x00131461
		public override XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return XD.DotNetOneWayDictionary.Namespace;
			}
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0013326D File Offset: 0x0013146D
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
		}
	}
}
