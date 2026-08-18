using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200003A RID: 58
	internal sealed class ReferenceIdentity
	{
		// Token: 0x06000118 RID: 280 RVA: 0x0000700C File Offset: 0x0000520C
		internal ReferenceIdentity(IReferenceIdentity i)
		{
			if (i == null)
			{
				throw new ArgumentNullException();
			}
			this._id = i;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007024 File Offset: 0x00005224
		private string GetAttribute(string ns, string n)
		{
			return this._id.GetAttribute(ns, n);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007033 File Offset: 0x00005233
		private string GetAttribute(string n)
		{
			return this._id.GetAttribute(null, n);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007042 File Offset: 0x00005242
		private void SetAttribute(string ns, string n, string v)
		{
			this._id.SetAttribute(ns, n, v);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007052 File Offset: 0x00005252
		private void SetAttribute(string n, string v)
		{
			this.SetAttribute(null, n, v);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000705D File Offset: 0x0000525D
		private void DeleteAttribute(string ns, string n)
		{
			this.SetAttribute(ns, n, null);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007068 File Offset: 0x00005268
		private void DeleteAttribute(string n)
		{
			this.SetAttribute(null, n, null);
		}

		// Token: 0x0400013C RID: 316
		internal IReferenceIdentity _id;
	}
}
