using System;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Linq;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000047 RID: 71
	public class ResolveCriteria
	{
		// Token: 0x0600036E RID: 878 RVA: 0x00009E0E File Offset: 0x0000800E
		public ResolveCriteria() : this(new EndpointAddress(EndpointAddress.AnonymousUri, new AddressHeader[0]))
		{
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00009E26 File Offset: 0x00008026
		public ResolveCriteria(EndpointAddress address)
		{
			if (address == null)
			{
				throw FxTrace.Exception.ArgumentNull("address");
			}
			this.endpointAddress = address;
			this.duration = ResolveCriteria.defaultDuration;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00009E59 File Offset: 0x00008059
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00009E61 File Offset: 0x00008061
		public EndpointAddress Address
		{
			get
			{
				return this.endpointAddress;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.endpointAddress = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00009E83 File Offset: 0x00008083
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00009E8B File Offset: 0x0000808B
		public TimeSpan Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				if (value.CompareTo(TimeSpan.Zero) <= 0)
				{
					throw FxTrace.Exception.ArgumentOutOfRange("value", value, SR.DiscoveryResolveDurationLessThanZero);
				}
				this.duration = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00009EBE File Offset: 0x000080BE
		public Collection<XElement> Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new NonNullItemCollection<XElement>();
				}
				return this.extensions;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009EDC File Offset: 0x000080DC
		internal void ReadFrom(DiscoveryVersion discoveryVersion, XmlReader reader)
		{
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			if (reader == null)
			{
				throw FxTrace.Exception.ArgumentNull("reader");
			}
			reader.MoveToContent();
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			int depth = reader.Depth;
			reader.ReadStartElement();
			this.endpointAddress = SerializationUtility.ReadEndpointAddress(discoveryVersion, reader);
			this.extensions = null;
			this.duration = TimeSpan.MaxValue;
			for (;;)
			{
				reader.MoveToContent();
				if (reader.NodeType == XmlNodeType.EndElement && reader.Depth == depth)
				{
					break;
				}
				if (reader.IsStartElement("Duration", "http://schemas.microsoft.com/ws/2008/06/discovery"))
				{
					this.duration = SerializationUtility.ReadDuration(reader);
				}
				else if (reader.IsStartElement())
				{
					XElement item = XNode.ReadFrom(reader) as XElement;
					this.Extensions.Add(item);
				}
				else
				{
					reader.Read();
				}
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009FB8 File Offset: 0x000081B8
		internal void WriteTo(DiscoveryVersion discoveryVersion, XmlWriter writer)
		{
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			if (writer == null)
			{
				throw FxTrace.Exception.ArgumentNull("writer");
			}
			SerializationUtility.WriteEndPointAddress(discoveryVersion, this.endpointAddress, writer);
			if (this.duration != TimeSpan.MaxValue)
			{
				writer.WriteElementString("Duration", "http://schemas.microsoft.com/ws/2008/06/discovery", XmlConvert.ToString(this.duration));
			}
			if (this.extensions != null)
			{
				foreach (XElement xelement in this.Extensions)
				{
					xelement.WriteTo(writer);
				}
			}
		}

		// Token: 0x040000EA RID: 234
		private static TimeSpan defaultDuration = TimeSpan.FromSeconds(20.0);

		// Token: 0x040000EB RID: 235
		private EndpointAddress endpointAddress;

		// Token: 0x040000EC RID: 236
		private TimeSpan duration;

		// Token: 0x040000ED RID: 237
		private NonNullItemCollection<XElement> extensions;
	}
}
