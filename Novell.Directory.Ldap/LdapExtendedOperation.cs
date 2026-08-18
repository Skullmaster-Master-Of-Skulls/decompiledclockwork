using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000031 RID: 49
	public class LdapExtendedOperation : ICloneable
	{
		// Token: 0x06000214 RID: 532 RVA: 0x0000B190 File Offset: 0x0000A190
		[CLSCompliant(false)]
		public LdapExtendedOperation(string oid, sbyte[] vals)
		{
			this.oid = oid;
			this.vals = vals;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B1B4 File Offset: 0x0000A1B4
		public object Clone()
		{
			object result;
			try
			{
				object obj = base.MemberwiseClone();
				Array.Copy(this.vals, 0, ((LdapExtendedOperation)obj).vals, 0, this.vals.Length);
				result = obj;
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return result;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B218 File Offset: 0x0000A218
		public virtual string getID()
		{
			return this.oid;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000B230 File Offset: 0x0000A230
		[CLSCompliant(false)]
		public virtual sbyte[] getValue()
		{
			return this.vals;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B248 File Offset: 0x0000A248
		[CLSCompliant(false)]
		protected internal virtual void setValue(sbyte[] newVals)
		{
			this.vals = newVals;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B260 File Offset: 0x0000A260
		protected internal virtual void setID(string newoid)
		{
			this.oid = newoid;
		}

		// Token: 0x04000109 RID: 265
		private string oid;

		// Token: 0x0400010A RID: 266
		private sbyte[] vals;
	}
}
