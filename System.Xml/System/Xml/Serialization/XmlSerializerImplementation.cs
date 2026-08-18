using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000337 RID: 823
	public abstract class XmlSerializerImplementation
	{
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x000D05C0 File Offset: 0x000CF5C0
		public virtual XmlSerializationReader Reader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000D05C7 File Offset: 0x000CF5C7
		public virtual XmlSerializationWriter Writer
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x000D05CE File Offset: 0x000CF5CE
		public virtual Hashtable ReadMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x000D05D5 File Offset: 0x000CF5D5
		public virtual Hashtable WriteMethods
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x000D05DC File Offset: 0x000CF5DC
		public virtual Hashtable TypedSerializers
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000D05E3 File Offset: 0x000CF5E3
		public virtual bool CanSerialize(Type type)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000D05EA File Offset: 0x000CF5EA
		public virtual XmlSerializer GetSerializer(Type type)
		{
			throw new NotSupportedException();
		}
	}
}
