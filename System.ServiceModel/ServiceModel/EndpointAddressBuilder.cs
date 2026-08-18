using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x02000120 RID: 288
	[__DynamicallyInvokable]
	public class EndpointAddressBuilder
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0002004A File Offset: 0x0001E24A
		[__DynamicallyInvokable]
		public EndpointAddressBuilder()
		{
			this.headers = new Collection<AddressHeader>();
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00020060 File Offset: 0x0001E260
		[__DynamicallyInvokable]
		public EndpointAddressBuilder(EndpointAddress address)
		{
			if (address == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("address");
			}
			this.epr = address;
			this.uri = address.Uri;
			this.identity = address.Identity;
			this.headers = new Collection<AddressHeader>();
			for (int i = 0; i < address.Headers.Count; i++)
			{
				this.headers.Add(address.Headers[i]);
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000200E3 File Offset: 0x0001E2E3
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x000200EB File Offset: 0x0001E2EB
		[__DynamicallyInvokable]
		public Uri Uri
		{
			[__DynamicallyInvokable]
			get
			{
				return this.uri;
			}
			[__DynamicallyInvokable]
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000200F4 File Offset: 0x0001E2F4
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x000200FC File Offset: 0x0001E2FC
		[__DynamicallyInvokable]
		public EndpointIdentity Identity
		{
			[__DynamicallyInvokable]
			get
			{
				return this.identity;
			}
			[__DynamicallyInvokable]
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00020105 File Offset: 0x0001E305
		[__DynamicallyInvokable]
		public Collection<AddressHeader> Headers
		{
			[__DynamicallyInvokable]
			get
			{
				return this.headers;
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00020110 File Offset: 0x0001E310
		public XmlDictionaryReader GetReaderAtMetadata()
		{
			if (!this.hasMetadata)
			{
				if (!(this.epr == null))
				{
					return this.epr.GetReaderAtMetadata();
				}
				return null;
			}
			else
			{
				if (this.metadataBuffer == null)
				{
					return null;
				}
				XmlDictionaryReader reader = this.metadataBuffer.GetReader(0);
				reader.MoveToContent();
				reader.Read();
				return reader;
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00020168 File Offset: 0x0001E368
		public void SetMetadataReader(XmlDictionaryReader reader)
		{
			this.hasMetadata = true;
			this.metadataBuffer = null;
			if (reader != null)
			{
				this.metadataBuffer = new XmlBuffer(32767);
				XmlDictionaryWriter xmlDictionaryWriter = this.metadataBuffer.OpenSection(reader.Quotas);
				xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
				EndpointAddress.Copy(xmlDictionaryWriter, reader);
				this.metadataBuffer.CloseSection();
				this.metadataBuffer.Close();
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000201D8 File Offset: 0x0001E3D8
		public XmlDictionaryReader GetReaderAtExtensions()
		{
			if (!this.hasExtension)
			{
				if (!(this.epr == null))
				{
					return this.epr.GetReaderAtExtensions();
				}
				return null;
			}
			else
			{
				if (this.extensionBuffer == null)
				{
					return null;
				}
				XmlDictionaryReader reader = this.extensionBuffer.GetReader(0);
				reader.MoveToContent();
				reader.Read();
				return reader;
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00020230 File Offset: 0x0001E430
		public void SetExtensionReader(XmlDictionaryReader reader)
		{
			this.hasExtension = true;
			EndpointIdentity endpointIdentity;
			int num;
			this.extensionBuffer = EndpointAddress.ReadExtensions(reader, null, null, out endpointIdentity, out num);
			if (this.extensionBuffer != null)
			{
				this.extensionBuffer.Close();
			}
			if (endpointIdentity != null)
			{
				this.identity = endpointIdentity;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00020274 File Offset: 0x0001E474
		[__DynamicallyInvokable]
		public EndpointAddress ToEndpointAddress()
		{
			return new EndpointAddress(this.uri, this.identity, new AddressHeaderCollection(this.headers), this.GetReaderAtMetadata(), this.GetReaderAtExtensions(), (this.epr == null) ? null : this.epr.GetReaderAtPsp());
		}

		// Token: 0x04000AD4 RID: 2772
		private Uri uri;

		// Token: 0x04000AD5 RID: 2773
		private EndpointIdentity identity;

		// Token: 0x04000AD6 RID: 2774
		private Collection<AddressHeader> headers;

		// Token: 0x04000AD7 RID: 2775
		private XmlBuffer extensionBuffer;

		// Token: 0x04000AD8 RID: 2776
		private XmlBuffer metadataBuffer;

		// Token: 0x04000AD9 RID: 2777
		private bool hasExtension;

		// Token: 0x04000ADA RID: 2778
		private bool hasMetadata;

		// Token: 0x04000ADB RID: 2779
		private EndpointAddress epr;
	}
}
