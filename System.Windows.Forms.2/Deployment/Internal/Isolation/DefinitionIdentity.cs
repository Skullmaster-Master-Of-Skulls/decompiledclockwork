using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200003D RID: 61
	internal sealed class DefinitionIdentity
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00007073 File Offset: 0x00005273
		internal DefinitionIdentity(IDefinitionIdentity i)
		{
			if (i == null)
			{
				throw new ArgumentNullException();
			}
			this._id = i;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000708B File Offset: 0x0000528B
		private string GetAttribute(string ns, string n)
		{
			return this._id.GetAttribute(ns, n);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000709A File Offset: 0x0000529A
		private string GetAttribute(string n)
		{
			return this._id.GetAttribute(null, n);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000070A9 File Offset: 0x000052A9
		private void SetAttribute(string ns, string n, string v)
		{
			this._id.SetAttribute(ns, n, v);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000070B9 File Offset: 0x000052B9
		private void SetAttribute(string n, string v)
		{
			this.SetAttribute(null, n, v);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000070C4 File Offset: 0x000052C4
		private void DeleteAttribute(string ns, string n)
		{
			this.SetAttribute(ns, n, null);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000070CF File Offset: 0x000052CF
		private void DeleteAttribute(string n)
		{
			this.SetAttribute(null, n, null);
		}

		// Token: 0x0400013D RID: 317
		internal IDefinitionIdentity _id;
	}
}
