using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200018B RID: 395
	[__DynamicallyInvokable]
	public class XmlAttributeOverrides
	{
		// Token: 0x060019EB RID: 6635 RVA: 0x000734D4 File Offset: 0x000716D4
		[__DynamicallyInvokable]
		public void Add(Type type, XmlAttributes attributes)
		{
			this.Add(type, string.Empty, attributes);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000734E4 File Offset: 0x000716E4
		[__DynamicallyInvokable]
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

		// Token: 0x170005A0 RID: 1440
		[__DynamicallyInvokable]
		public XmlAttributes this[Type type]
		{
			[__DynamicallyInvokable]
			get
			{
				return this[type, string.Empty];
			}
		}

		// Token: 0x170005A1 RID: 1441
		[__DynamicallyInvokable]
		public XmlAttributes this[Type type, string member]
		{
			[__DynamicallyInvokable]
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

		// Token: 0x060019EF RID: 6639 RVA: 0x00073590 File Offset: 0x00071790
		[__DynamicallyInvokable]
		public XmlAttributeOverrides()
		{
		}

		// Token: 0x04000BC7 RID: 3015
		private Hashtable types = new Hashtable();
	}
}
