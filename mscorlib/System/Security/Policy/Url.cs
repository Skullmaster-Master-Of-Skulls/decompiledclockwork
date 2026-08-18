using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x020004BA RID: 1210
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06003046 RID: 12358 RVA: 0x000A5C56 File Offset: 0x000A4C56
		internal Url()
		{
			this.m_url = null;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000A5C65 File Offset: 0x000A4C65
		internal Url(SerializationInfo info, StreamingContext context)
		{
			this.m_url = new URLString((string)info.GetValue("Url", typeof(string)));
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000A5C92 File Offset: 0x000A4C92
		internal Url(string name, bool parsed)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_url = new URLString(name, parsed);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000A5CB5 File Offset: 0x000A4CB5
		public Url(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_url = new URLString(name);
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000A5CD7 File Offset: 0x000A4CD7
		public string Value
		{
			get
			{
				if (this.m_url == null)
				{
					return null;
				}
				return this.m_url.ToString();
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000A5CEE File Offset: 0x000A4CEE
		internal URLString GetURLString()
		{
			return this.m_url;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000A5CF6 File Offset: 0x000A4CF6
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.m_url);
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000A5D04 File Offset: 0x000A4D04
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (!(o is Url))
			{
				return false;
			}
			Url url = (Url)o;
			if (this.m_url == null)
			{
				return url.m_url == null;
			}
			return url.m_url != null && this.m_url.Equals(url.m_url);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000A5D54 File Offset: 0x000A4D54
		public override int GetHashCode()
		{
			if (this.m_url == null)
			{
				return 0;
			}
			return this.m_url.GetHashCode();
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000A5D6C File Offset: 0x000A4D6C
		public object Copy()
		{
			return new Url
			{
				m_url = this.m_url
			};
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000A5D8C File Offset: 0x000A4D8C
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			if (this.m_url != null)
			{
				securityElement.AddChild(new SecurityElement("Url", this.m_url.ToString()));
			}
			return securityElement;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000A5DD8 File Offset: 0x000A4DD8
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000A5DE8 File Offset: 0x000A4DE8
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\u0004';
			string value = this.Value;
			int length = value.Length;
			if (verbose)
			{
				BuiltInEvidenceHelper.CopyIntToCharArray(length, buffer, position);
				position += 2;
			}
			value.CopyTo(0, buffer, position, length);
			return length + position;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000A5E29 File Offset: 0x000A4E29
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			if (verbose)
			{
				return this.Value.Length + 3;
			}
			return this.Value.Length + 1;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000A5E4C File Offset: 0x000A4E4C
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			int intFromCharArray = BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			position += 2;
			this.m_url = new URLString(new string(buffer, position, intFromCharArray));
			return position + intFromCharArray;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000A5E7C File Offset: 0x000A4E7C
		internal object Normalize()
		{
			return this.m_url.NormalizeUrl();
		}

		// Token: 0x04001867 RID: 6247
		private URLString m_url;
	}
}
