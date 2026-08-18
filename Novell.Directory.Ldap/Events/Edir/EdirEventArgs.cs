using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x0200007C RID: 124
	public class EdirEventArgs : DirectoryEventArgs
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00014794 File Offset: 0x00013794
		public EdirEventIntermediateResponse IntermediateResponse
		{
			get
			{
				EdirEventIntermediateResponse result;
				if (this.ldap_message is EdirEventIntermediateResponse)
				{
					result = (EdirEventIntermediateResponse)this.ldap_message;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000147C4 File Offset: 0x000137C4
		public EdirEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification) : base(sourceMessage, aClassification)
		{
		}
	}
}
