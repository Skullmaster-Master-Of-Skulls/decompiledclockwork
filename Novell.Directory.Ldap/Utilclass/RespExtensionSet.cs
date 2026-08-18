using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F8 RID: 248
	public class RespExtensionSet : SupportClass.AbstractSetSupport
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001D498 File Offset: 0x0001C498
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001D4B4 File Offset: 0x0001C4B4
		public RespExtensionSet()
		{
			this.map = new Hashtable();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001D4D4 File Offset: 0x0001C4D4
		public void registerResponseExtension(string oid, Type extClass)
		{
			lock (this)
			{
				if (!this.map.ContainsKey(oid))
				{
					this.map.Add(oid, extClass);
				}
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001D52C File Offset: 0x0001C52C
		public override IEnumerator GetEnumerator()
		{
			return this.map.Values.GetEnumerator();
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001D550 File Offset: 0x0001C550
		public Type findResponseExtension(string searchOID)
		{
			Type result;
			lock (this)
			{
				if (this.map.ContainsKey(searchOID))
				{
					result = (Type)this.map[searchOID];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04000493 RID: 1171
		private Hashtable map;
	}
}
