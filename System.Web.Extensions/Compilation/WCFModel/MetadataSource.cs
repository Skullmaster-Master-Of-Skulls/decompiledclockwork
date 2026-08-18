using System;
using System.Web.Resources;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200001B RID: 27
	internal class MetadataSource
	{
		// Token: 0x0600011F RID: 287 RVA: 0x00004899 File Offset: 0x00002A99
		public MetadataSource()
		{
			this.m_Address = string.Empty;
			this.m_Protocol = string.Empty;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000048B8 File Offset: 0x00002AB8
		public MetadataSource(string protocol, string address, int sourceId)
		{
			if (protocol == null)
			{
				throw new ArgumentNullException("protocol");
			}
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (protocol.Length == 0)
			{
				throw new ArgumentException(WCFModelStrings.ReferenceGroup_EmptyProtocol);
			}
			if (address == null)
			{
				throw new ArgumentException(WCFModelStrings.ReferenceGroup_EmptyAddress);
			}
			this.m_Protocol = protocol;
			this.m_Address = address;
			if (sourceId < 0)
			{
				throw new ArgumentException(WCFModelStrings.ReferenceGroup_InvalidSourceId);
			}
			this.m_SourceId = sourceId;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000492C File Offset: 0x00002B2C
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00004934 File Offset: 0x00002B34
		[XmlAttribute]
		public string Address
		{
			get
			{
				return this.m_Address;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Address = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000494B File Offset: 0x00002B4B
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004953 File Offset: 0x00002B53
		[XmlAttribute]
		public string Protocol
		{
			get
			{
				return this.m_Protocol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Protocol = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000496A File Offset: 0x00002B6A
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00004972 File Offset: 0x00002B72
		[XmlAttribute]
		public int SourceId
		{
			get
			{
				return this.m_SourceId;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(WCFModelStrings.ReferenceGroup_InvalidSourceId);
				}
				this.m_SourceId = value;
			}
		}

		// Token: 0x04000055 RID: 85
		private string m_Address;

		// Token: 0x04000056 RID: 86
		private string m_Protocol;

		// Token: 0x04000057 RID: 87
		private int m_SourceId;
	}
}
