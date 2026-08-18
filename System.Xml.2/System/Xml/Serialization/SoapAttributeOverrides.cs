using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200016E RID: 366
	public class SoapAttributeOverrides
	{
		// Token: 0x06001887 RID: 6279 RVA: 0x0006BF81 File Offset: 0x0006A181
		public void Add(Type type, SoapAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0006BF90 File Offset: 0x0006A190
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

		// Token: 0x17000541 RID: 1345
		public SoapAttributes this[Type type]
		{
			get
			{
				return this[type, string.Empty];
			}
		}

		// Token: 0x17000542 RID: 1346
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

		// Token: 0x04000B40 RID: 2880
		private Hashtable types = new Hashtable();
	}
}
