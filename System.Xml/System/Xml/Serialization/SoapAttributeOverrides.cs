using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002E8 RID: 744
	public class SoapAttributeOverrides
	{
		// Token: 0x060022D0 RID: 8912 RVA: 0x000A3B21 File Offset: 0x000A2B21
		public void Add(Type type, SoapAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000A3B30 File Offset: 0x000A2B30
		public void Add(Type type, string member, SoapAttributes attributes)
		{
			Hashtable hashtable = (Hashtable)this.types[type];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.types.Add(type, hashtable);
			}
			else if (hashtable[member] != null)
			{
				throw new InvalidOperationException(Res.GetString("XmlMultipleAttributeOverrides", new object[]
				{
					type.FullName,
					member
				}));
			}
			hashtable.Add(member, attributes);
		}

		// Token: 0x1700087D RID: 2173
		public SoapAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		// Token: 0x1700087E RID: 2174
		public SoapAttributes this[Type type, string member]
		{
			get
			{
				Hashtable hashtable = (Hashtable)this.types[type];
				if (hashtable == null)
				{
					return null;
				}
				return (SoapAttributes)hashtable[member];
			}
		}

		// Token: 0x040014D4 RID: 5332
		private Hashtable types = new Hashtable();
	}
}
