using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x020004C5 RID: 1221
	[ComVisible(true)]
	[Serializable]
	public class GenericIdentity : IIdentity
	{
		// Token: 0x060030E8 RID: 12520 RVA: 0x000A7B58 File Offset: 0x000A6B58
		public GenericIdentity(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = name;
			this.m_type = "";
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000A7B80 File Offset: 0x000A6B80
		public GenericIdentity(string name, string type)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.m_name = name;
			this.m_type = type;
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000A7BB2 File Offset: 0x000A6BB2
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000A7BBA File Offset: 0x000A6BBA
		public virtual string AuthenticationType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x000A7BC2 File Offset: 0x000A6BC2
		public virtual bool IsAuthenticated
		{
			get
			{
				return !this.m_name.Equals("");
			}
		}

		// Token: 0x0400187D RID: 6269
		private string m_name;

		// Token: 0x0400187E RID: 6270
		private string m_type;
	}
}
