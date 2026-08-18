using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200033C RID: 828
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class XmlSerializerVersionAttribute : Attribute
	{
		// Token: 0x0600288C RID: 10380 RVA: 0x000D1C70 File Offset: 0x000D0C70
		public XmlSerializerVersionAttribute()
		{
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000D1C78 File Offset: 0x000D0C78
		public XmlSerializerVersionAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x000D1C87 File Offset: 0x000D0C87
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x000D1C8F File Offset: 0x000D0C8F
		public string ParentAssemblyId
		{
			get
			{
				return this.mvid;
			}
			set
			{
				this.mvid = value;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x000D1C98 File Offset: 0x000D0C98
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x000D1CA0 File Offset: 0x000D0CA0
		public string Version
		{
			get
			{
				return this.serializerVersion;
			}
			set
			{
				this.serializerVersion = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x000D1CA9 File Offset: 0x000D0CA9
		// (set) Token: 0x06002893 RID: 10387 RVA: 0x000D1CB1 File Offset: 0x000D0CB1
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000D1CBA File Offset: 0x000D0CBA
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x000D1CC2 File Offset: 0x000D0CC2
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04001683 RID: 5763
		private string mvid;

		// Token: 0x04001684 RID: 5764
		private string serializerVersion;

		// Token: 0x04001685 RID: 5765
		private string ns;

		// Token: 0x04001686 RID: 5766
		private Type type;
	}
}
