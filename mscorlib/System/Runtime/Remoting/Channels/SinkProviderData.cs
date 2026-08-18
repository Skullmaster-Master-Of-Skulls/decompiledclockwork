using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x020006F0 RID: 1776
	[ComVisible(true)]
	public class SinkProviderData
	{
		// Token: 0x06003F64 RID: 16228 RVA: 0x000D86E3 File Offset: 0x000D76E3
		public SinkProviderData(string name)
		{
			this._name = name;
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003F65 RID: 16229 RVA: 0x000D870D File Offset: 0x000D770D
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x000D8715 File Offset: 0x000D7715
		public IDictionary Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003F67 RID: 16231 RVA: 0x000D871D File Offset: 0x000D771D
		public IList Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x0400201C RID: 8220
		private string _name;

		// Token: 0x0400201D RID: 8221
		private Hashtable _properties = new Hashtable(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x0400201E RID: 8222
		private ArrayList _children = new ArrayList();
	}
}
