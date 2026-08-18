using System;
using System.Text;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x0200008F RID: 143
	public class LdapEventArgs : DirectoryEventArgs
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00015444 File Offset: 0x00014444
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001545C File Offset: 0x0001445C
		public LdapEventType EventType
		{
			get
			{
				return this.eType;
			}
			set
			{
				this.eType = value;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00015470 File Offset: 0x00014470
		public LdapEventArgs(LdapMessage sourceMessage, EventClassifiers aClassification, LdapEventType aType) : base(sourceMessage, aClassification)
		{
			this.eType = aType;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001548C File Offset: 0x0001448C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.AppendFormat("{0}:", base.GetType());
			stringBuilder.AppendFormat("(Classification={0})", this.eClassification);
			stringBuilder.AppendFormat("(Type={0})", this.eType);
			stringBuilder.AppendFormat("(EventInformation:{0})", this.ldap_message);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000330 RID: 816
		protected LdapEventType eType;
	}
}
