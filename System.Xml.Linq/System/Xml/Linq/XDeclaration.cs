using System;
using System.Text;

namespace System.Xml.Linq
{
	// Token: 0x02000028 RID: 40
	[__DynamicallyInvokable]
	public class XDeclaration
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00008847 File Offset: 0x00006A47
		[__DynamicallyInvokable]
		public XDeclaration(string version, string encoding, string standalone)
		{
			this.version = version;
			this.encoding = encoding;
			this.standalone = standalone;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008864 File Offset: 0x00006A64
		[__DynamicallyInvokable]
		public XDeclaration(XDeclaration other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.version = other.version;
			this.encoding = other.encoding;
			this.standalone = other.standalone;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000088A0 File Offset: 0x00006AA0
		internal XDeclaration(XmlReader r)
		{
			this.version = r.GetAttribute("version");
			this.encoding = r.GetAttribute("encoding");
			this.standalone = r.GetAttribute("standalone");
			r.Read();
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000088ED File Offset: 0x00006AED
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000088F5 File Offset: 0x00006AF5
		[__DynamicallyInvokable]
		public string Encoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoding;
			}
			[__DynamicallyInvokable]
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000088FE File Offset: 0x00006AFE
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00008906 File Offset: 0x00006B06
		[__DynamicallyInvokable]
		public string Standalone
		{
			[__DynamicallyInvokable]
			get
			{
				return this.standalone;
			}
			[__DynamicallyInvokable]
			set
			{
				this.standalone = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000890F File Offset: 0x00006B0F
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00008917 File Offset: 0x00006B17
		[__DynamicallyInvokable]
		public string Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.version;
			}
			[__DynamicallyInvokable]
			set
			{
				this.version = value;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008920 File Offset: 0x00006B20
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("<?xml");
			if (this.version != null)
			{
				stringBuilder.Append(" version=\"");
				stringBuilder.Append(this.version);
				stringBuilder.Append("\"");
			}
			if (this.encoding != null)
			{
				stringBuilder.Append(" encoding=\"");
				stringBuilder.Append(this.encoding);
				stringBuilder.Append("\"");
			}
			if (this.standalone != null)
			{
				stringBuilder.Append(" standalone=\"");
				stringBuilder.Append(this.standalone);
				stringBuilder.Append("\"");
			}
			stringBuilder.Append("?>");
			return stringBuilder.ToString();
		}

		// Token: 0x040000A5 RID: 165
		private string version;

		// Token: 0x040000A6 RID: 166
		private string encoding;

		// Token: 0x040000A7 RID: 167
		private string standalone;
	}
}
