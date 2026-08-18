using System;
using System.Text;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x02000096 RID: 150
	public class SearchResultEventArgs : LdapEventArgs
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0001584C File Offset: 0x0001484C
		public SearchResultEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType) : base(sourceMessage, EventClassifiers.CLASSIFICATION_LDAP_PSEARCH, aType)
		{
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00015864 File Offset: 0x00014864
		public LdapEntry Entry
		{
			get
			{
				return ((LdapSearchResult)this.ldap_message).Entry;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015888 File Offset: 0x00014888
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[{0}:", base.GetType());
			stringBuilder.AppendFormat("(Classification={0})", this.eClassification);
			stringBuilder.AppendFormat("(Type={0})", this.getChangeTypeString());
			stringBuilder.AppendFormat("(EventInformation:{0})", this.getStringRepresentaionOfEventInformation());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00015900 File Offset: 0x00014900
		private string getStringRepresentaionOfEventInformation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			LdapSearchResult ldapSearchResult = (LdapSearchResult)this.ldap_message;
			stringBuilder.AppendFormat("(Entry={0})", ldapSearchResult.Entry);
			LdapControl[] controls = ldapSearchResult.Controls;
			if (controls != null)
			{
				stringBuilder.Append("(Controls=");
				int num = 0;
				foreach (LdapControl ldapControl in controls)
				{
					stringBuilder.AppendFormat("(Control{0}={1})", ++num, ldapControl.ToString());
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001599C File Offset: 0x0001499C
		private string getChangeTypeString()
		{
			LdapEventType eType = this.eType;
			switch (eType)
			{
			case LdapEventType.LDAP_PSEARCH_ADD:
				return "ADD";
			case LdapEventType.LDAP_PSEARCH_DELETE:
				return "DELETE";
			case (LdapEventType)3:
				break;
			case LdapEventType.LDAP_PSEARCH_MODIFY:
				return "MODIFY";
			default:
				if (eType == LdapEventType.LDAP_PSEARCH_MODDN)
				{
					return "MODDN";
				}
				break;
			}
			return "No change type: " + this.eType;
		}
	}
}
