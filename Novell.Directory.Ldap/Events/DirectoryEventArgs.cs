using System;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x0200007B RID: 123
	public class DirectoryEventArgs : BaseEventArgs
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001474C File Offset: 0x0001374C
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00014764 File Offset: 0x00013764
		public EventClassifiers EventClassification
		{
			get
			{
				return this.eClassification;
			}
			set
			{
				this.eClassification = value;
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014778 File Offset: 0x00013778
		public DirectoryEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification) : base(sourceMessage)
		{
			this.eClassification = aClassification;
		}

		// Token: 0x04000216 RID: 534
		protected EventClassifiers eClassification;
	}
}
