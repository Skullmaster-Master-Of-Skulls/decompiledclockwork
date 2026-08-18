using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F6 RID: 246
	public class RespControlVector : ArrayList
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x0001D360 File Offset: 0x0001C360
		public RespControlVector(int cap, int incr) : base(cap)
		{
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001D378 File Offset: 0x0001C378
		public void registerResponseControl(string oid, Type controlClass)
		{
			lock (this)
			{
				this.Add(new RespControlVector.RegisteredControl(this, oid, controlClass));
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001D3C4 File Offset: 0x0001C3C4
		public Type findResponseControl(string searchOID)
		{
			Type result;
			lock (this)
			{
				for (int i = 0; i < this.Count; i++)
				{
					RespControlVector.RegisteredControl registeredControl;
					if ((registeredControl = (RespControlVector.RegisteredControl)this.ToArray()[i]) == null)
					{
						throw new FieldAccessException();
					}
					if (registeredControl.myOID.CompareTo(searchOID) == 0)
					{
						return registeredControl.myClass;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x020000F7 RID: 247
		private class RegisteredControl
		{
			// Token: 0x06000609 RID: 1545 RVA: 0x0001D444 File Offset: 0x0001C444
			private void InitBlock(RespControlVector enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001D458 File Offset: 0x0001C458
			public RespControlVector Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0001D470 File Offset: 0x0001C470
			public RegisteredControl(RespControlVector enclosingInstance, string oid, Type controlClass)
			{
				this.InitBlock(enclosingInstance);
				this.myOID = oid;
				this.myClass = controlClass;
			}

			// Token: 0x04000490 RID: 1168
			private RespControlVector enclosingInstance;

			// Token: 0x04000491 RID: 1169
			public string myOID;

			// Token: 0x04000492 RID: 1170
			public Type myClass;
		}
	}
}
