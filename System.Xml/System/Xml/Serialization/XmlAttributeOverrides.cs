using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000304 RID: 772
	public class XmlAttributeOverrides
	{
		// Token: 0x0600241F RID: 9247 RVA: 0x000AA6A5 File Offset: 0x000A96A5
		public void Add(Type type, XmlAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000AA6B4 File Offset: 0x000A96B4
		public void Add(Type type, string member, XmlAttributes attributes)
		{
			Hashtable hashtable = (Hashtable)this.types[type];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.types.Add(type, hashtable);
			}
			else if (hashtable[member] != null)
			{
				throw new InvalidOperationException(Res.GetString("XmlAttributeSetAgain", new object[]
				{
					type.FullName,
					member
				}));
			}
			hashtable.Add(member, attributes);
		}

		// Token: 0x170008DC RID: 2268
		public XmlAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		// Token: 0x170008DD RID: 2269
		public XmlAttributes this[Type type, string member]
		{
			get
			{
				Hashtable hashtable = (Hashtable)this.types[type];
				if (hashtable == null)
				{
					return null;
				}
				return (XmlAttributes)hashtable[member];
			}
		}

		// Token: 0x04001553 RID: 5459
		private Hashtable types = new Hashtable();
	}
}
