using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	// Token: 0x0200078B RID: 1931
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapQName : ISoapXsd
	{
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x000EB1EE File Offset: 0x000EA1EE
		public static string XsdType
		{
			get
			{
				return "QName";
			}
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x000EB1F5 File Offset: 0x000EA1F5
		public string GetXsdType()
		{
			return SoapQName.XsdType;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x000EB1FC File Offset: 0x000EA1FC
		public SoapQName()
		{
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x000EB204 File Offset: 0x000EA204
		public SoapQName(string value)
		{
			this._name = value;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x000EB213 File Offset: 0x000EA213
		public SoapQName(string key, string name)
		{
			this._name = name;
			this._key = key;
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x000EB229 File Offset: 0x000EA229
		public SoapQName(string key, string name, string namespaceValue)
		{
			this._name = name;
			this._namespace = namespaceValue;
			this._key = key;
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x060044D1 RID: 17617 RVA: 0x000EB246 File Offset: 0x000EA246
		// (set) Token: 0x060044D2 RID: 17618 RVA: 0x000EB24E File Offset: 0x000EA24E
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x000EB257 File Offset: 0x000EA257
		// (set) Token: 0x060044D4 RID: 17620 RVA: 0x000EB25F File Offset: 0x000EA25F
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x060044D5 RID: 17621 RVA: 0x000EB268 File Offset: 0x000EA268
		// (set) Token: 0x060044D6 RID: 17622 RVA: 0x000EB270 File Offset: 0x000EA270
		public string Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x000EB279 File Offset: 0x000EA279
		public override string ToString()
		{
			if (this._key == null || this._key.Length == 0)
			{
				return this._name;
			}
			return this._key + ":" + this._name;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x000EB2B0 File Offset: 0x000EA2B0
		public static SoapQName Parse(string value)
		{
			if (value == null)
			{
				return new SoapQName();
			}
			string key = "";
			string name = value;
			int num = value.IndexOf(':');
			if (num > 0)
			{
				key = value.Substring(0, num);
				name = value.Substring(num + 1);
			}
			return new SoapQName(key, name);
		}

		// Token: 0x0400225C RID: 8796
		private string _name;

		// Token: 0x0400225D RID: 8797
		private string _namespace;

		// Token: 0x0400225E RID: 8798
		private string _key;
	}
}
