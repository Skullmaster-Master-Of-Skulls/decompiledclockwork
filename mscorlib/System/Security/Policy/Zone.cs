using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020004BC RID: 1212
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06003065 RID: 12389 RVA: 0x000A621C File Offset: 0x000A521C
		internal Zone()
		{
			this.m_url = null;
			this.m_zone = SecurityZone.NoZone;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000A6232 File Offset: 0x000A5232
		public Zone(SecurityZone zone)
		{
			if (zone < SecurityZone.NoZone || zone > SecurityZone.Untrusted)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
			}
			this.m_url = null;
			this.m_zone = zone;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000A6260 File Offset: 0x000A5260
		private Zone(string url)
		{
			this.m_url = url;
			this.m_zone = SecurityZone.NoZone;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000A6276 File Offset: 0x000A5276
		public static Zone CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			return new Zone(url);
		}

		// Token: 0x06003069 RID: 12393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SecurityZone _CreateFromUrl(string url);

		// Token: 0x0600306A RID: 12394 RVA: 0x000A628C File Offset: 0x000A528C
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.SecurityZone);
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000A6299 File Offset: 0x000A5299
		public SecurityZone SecurityZone
		{
			get
			{
				if (this.m_url != null)
				{
					this.m_zone = Zone._CreateFromUrl(this.m_url);
				}
				return this.m_zone;
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x000A62BC File Offset: 0x000A52BC
		public override bool Equals(object o)
		{
			if (o is Zone)
			{
				Zone zone = (Zone)o;
				return this.SecurityZone == zone.SecurityZone;
			}
			return false;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000A62E8 File Offset: 0x000A52E8
		public override int GetHashCode()
		{
			return (int)this.SecurityZone;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000A62F0 File Offset: 0x000A52F0
		public object Copy()
		{
			return new Zone
			{
				m_zone = this.m_zone,
				m_url = this.m_url
			};
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000A631C File Offset: 0x000A531C
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			if (this.SecurityZone != SecurityZone.NoZone)
			{
				securityElement.AddChild(new SecurityElement("Zone", Zone.s_names[(int)this.SecurityZone]));
			}
			else
			{
				securityElement.AddChild(new SecurityElement("Zone", Zone.s_names[Zone.s_names.Length - 1]));
			}
			return securityElement;
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000A638B File Offset: 0x000A538B
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position] = '\u0003';
			BuiltInEvidenceHelper.CopyIntToCharArray((int)this.SecurityZone, buffer, position + 1);
			return position + 3;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000A63A3 File Offset: 0x000A53A3
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 3;
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000A63A6 File Offset: 0x000A53A6
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			this.m_url = null;
			this.m_zone = (SecurityZone)BuiltInEvidenceHelper.GetIntFromCharArray(buffer, position);
			return position + 2;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x000A63BF File Offset: 0x000A53BF
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x000A63CC File Offset: 0x000A53CC
		internal object Normalize()
		{
			return Zone.s_names[(int)this.SecurityZone];
		}

		// Token: 0x0400186A RID: 6250
		[OptionalField(VersionAdded = 2)]
		private string m_url;

		// Token: 0x0400186B RID: 6251
		private SecurityZone m_zone;

		// Token: 0x0400186C RID: 6252
		private static readonly string[] s_names = new string[]
		{
			"MyComputer",
			"Intranet",
			"Trusted",
			"Internet",
			"Untrusted",
			"NoZone"
		};
	}
}
