using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Telerik.Licensing
{
	// Token: 0x0200042B RID: 1067
	internal class SessionName
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x0007DA12 File Offset: 0x0007BC12
		public SessionName(string name, bool existing = false)
		{
			this._name = (existing ? name : this.InitSessionName(name));
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x0007DA2D File Offset: 0x0007BC2D
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0007DA35 File Offset: 0x0007BC35
		public static SessionName FromExisting(string name)
		{
			return new SessionName(name, false);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0007DA54 File Offset: 0x0007BC54
		private string InitSessionName(string name)
		{
			name = Convert.ToBase64String(Encoding.UTF8.GetBytes(name));
			IEnumerable<char> enumerable = from ch in Path.GetInvalidFileNameChars()
			where name.Contains(ch)
			select ch;
			foreach (char c in enumerable)
			{
				name = name.Replace(c.ToString(), string.Empty);
			}
			return name;
		}

		// Token: 0x040009CB RID: 2507
		private readonly string _name;
	}
}
