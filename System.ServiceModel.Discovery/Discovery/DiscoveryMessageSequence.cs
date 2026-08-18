using System;
using System.Globalization;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000021 RID: 33
	public class DiscoveryMessageSequence : IComparable<DiscoveryMessageSequence>, IEquatable<DiscoveryMessageSequence>
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00006351 File Offset: 0x00004551
		internal DiscoveryMessageSequence()
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000063EE File Offset: 0x000045EE
		internal DiscoveryMessageSequence(long instanceId, Uri sequenceId, long messageNumber)
		{
			this.InstanceId = instanceId;
			this.SequenceId = sequenceId;
			this.MessageNumber = messageNumber;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000640B File Offset: 0x0000460B
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00006413 File Offset: 0x00004613
		public long InstanceId { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000641C File Offset: 0x0000461C
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00006424 File Offset: 0x00004624
		public Uri SequenceId { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000642D File Offset: 0x0000462D
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00006435 File Offset: 0x00004635
		public long MessageNumber { get; private set; }

		// Token: 0x0600018C RID: 396 RVA: 0x0000643E File Offset: 0x0000463E
		public static bool operator ==(DiscoveryMessageSequence messageSequence1, DiscoveryMessageSequence messageSequence2)
		{
			return (messageSequence1 == null && messageSequence2 == null) || (messageSequence1 != null && messageSequence2 != null && messageSequence1.Equals(messageSequence2));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006457 File Offset: 0x00004657
		public static bool operator !=(DiscoveryMessageSequence messageSequence1, DiscoveryMessageSequence messageSequence2)
		{
			return !(messageSequence1 == messageSequence2);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006464 File Offset: 0x00004664
		public override bool Equals(object obj)
		{
			DiscoveryMessageSequence other = obj as DiscoveryMessageSequence;
			return this.Equals(other);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006480 File Offset: 0x00004680
		public bool Equals(DiscoveryMessageSequence other)
		{
			return other != null && (this == other || (object.Equals(this.InstanceId, other.InstanceId) && object.Equals(this.SequenceId, other.SequenceId) && object.Equals(this.MessageNumber, other.MessageNumber)));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000064E5 File Offset: 0x000046E5
		public override string ToString()
		{
			return SR.DiscoveryMessageSequenceToString(this.InstanceId, this.SequenceId, this.MessageNumber);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006508 File Offset: 0x00004708
		public bool CanCompareTo(DiscoveryMessageSequence other)
		{
			return other != null && (this.InstanceId != other.InstanceId || object.Equals(this.SequenceId, other.SequenceId));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006530 File Offset: 0x00004730
		public override int GetHashCode()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", new object[]
			{
				this.InstanceId,
				this.SequenceId,
				this.MessageNumber
			}).GetHashCode();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000657C File Offset: 0x0000477C
		public int CompareTo(DiscoveryMessageSequence other)
		{
			if (other == null)
			{
				throw FxTrace.Exception.ArgumentNull("other");
			}
			int num = this.InstanceId.CompareTo(other.InstanceId);
			if (num == 0)
			{
				if (!object.Equals(this.SequenceId, other.SequenceId))
				{
					throw FxTrace.Exception.Argument("other", SR.DiscoveryIncompatibleMessageSequence);
				}
				num = this.MessageNumber.CompareTo(other.MessageNumber);
			}
			return num;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000065F4 File Offset: 0x000047F4
		internal void ReadFrom(XmlReader reader)
		{
			if (reader == null)
			{
				throw FxTrace.Exception.ArgumentNull("reader");
			}
			string attribute = reader.GetAttribute("InstanceId");
			this.InstanceId = SerializationUtility.ReadUInt(attribute, SR.DiscoveryXmlMissingAppSequenceInstanceId, SR.DiscoveryXmlInvalidAppSequenceInstanceId);
			string attribute2 = reader.GetAttribute("SequenceId");
			if (attribute2 != null)
			{
				try
				{
					this.SequenceId = new Uri(attribute2, UriKind.RelativeOrAbsolute);
				}
				catch (FormatException innerException)
				{
					throw FxTrace.Exception.AsError(new XmlException(SR.DiscoveryXmlUriFormatError(attribute2), innerException));
				}
			}
			string attribute3 = reader.GetAttribute("MessageNumber");
			this.MessageNumber = SerializationUtility.ReadUInt(attribute3, SR.DiscoveryXmlMissingAppSequenceMessageNumber, SR.DiscoveryXmlInvalidAppSequenceMessageNumber);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000066A0 File Offset: 0x000048A0
		internal void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw FxTrace.Exception.ArgumentNull("writer");
			}
			writer.WriteAttributeString("InstanceId", this.InstanceId.ToString(CultureInfo.InvariantCulture));
			if (this.SequenceId != null)
			{
				writer.WriteAttributeString("SequenceId", this.SequenceId.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
			}
			writer.WriteAttributeString("MessageNumber", this.MessageNumber.ToString(CultureInfo.InvariantCulture));
		}
	}
}
