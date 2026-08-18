using System;
using System.Security.Util;

namespace System.Security
{
	// Token: 0x02000678 RID: 1656
	internal struct PermissionSetEnumeratorInternal
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000CC4B6 File Offset: 0x000CB4B6
		public object Current
		{
			get
			{
				return this.enm.Current;
			}
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x000CC4C3 File Offset: 0x000CB4C3
		internal PermissionSetEnumeratorInternal(PermissionSet permSet)
		{
			this.m_permSet = permSet;
			this.enm = new TokenBasedSetEnumerator(permSet.m_permSet);
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000CC4DD File Offset: 0x000CB4DD
		public int GetCurrentIndex()
		{
			return this.enm.Index;
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000CC4EA File Offset: 0x000CB4EA
		public void Reset()
		{
			this.enm.Reset();
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000CC4F8 File Offset: 0x000CB4F8
		public bool MoveNext()
		{
			while (this.enm.MoveNext())
			{
				object current = this.enm.Current;
				IPermission permission = current as IPermission;
				if (permission != null)
				{
					this.enm.Current = permission;
					return true;
				}
				SecurityElement securityElement = current as SecurityElement;
				if (securityElement != null)
				{
					permission = this.m_permSet.CreatePermission(securityElement, this.enm.Index);
					if (permission != null)
					{
						this.enm.Current = permission;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04001EDF RID: 7903
		private PermissionSet m_permSet;

		// Token: 0x04001EE0 RID: 7904
		private TokenBasedSetEnumerator enm;
	}
}
