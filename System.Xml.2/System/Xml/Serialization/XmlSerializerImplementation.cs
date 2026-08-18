using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020001B9 RID: 441
	public abstract class XmlSerializerImplementation
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x000A7911 File Offset: 0x000A5B11
		public virtual XmlSerializationReader Reader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x000A7918 File Offset: 0x000A5B18
		public virtual XmlSerializationWriter Writer
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x000A791F File Offset: 0x000A5B1F
		public virtual Hashtable ReadMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x000A7926 File Offset: 0x000A5B26
		public virtual Hashtable WriteMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x000A792D File Offset: 0x000A5B2D
		public virtual Hashtable TypedSerializers
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000A7934 File Offset: 0x000A5B34
		public virtual bool CanSerialize(Type type)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000A793B File Offset: 0x000A5B3B
		public virtual XmlSerializer GetSerializer(Type type)
		{
			throw new NotSupportedException();
		}
	}
}
