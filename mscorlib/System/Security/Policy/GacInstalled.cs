using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x020004BE RID: 1214
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : IIdentityPermissionFactory, IBuiltInEvidence
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x000A67D6 File Offset: 0x000A57D6
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000A67DD File Offset: 0x000A57DD
		public override bool Equals(object o)
		{
			return o is GacInstalled;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000A67EA File Offset: 0x000A57EA
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000A67ED File Offset: 0x000A57ED
		public object Copy()
		{
			return new GacInstalled();
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000A67F4 File Offset: 0x000A57F4
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000A6823 File Offset: 0x000A5823
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position] = '\t';
			return position + 1;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000A682D File Offset: 0x000A582D
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 1;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000A6830 File Offset: 0x000A5830
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return position;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000A6833 File Offset: 0x000A5833
		public override string ToString()
		{
			return this.ToXml().ToString();
		}
	}
}
