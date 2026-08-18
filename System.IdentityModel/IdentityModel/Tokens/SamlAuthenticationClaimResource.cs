using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000152 RID: 338
	[DataContract]
	public class SamlAuthenticationClaimResource
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x0002E23F File Offset: 0x0002C43F
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (string.IsNullOrEmpty(this.authenticationMethod))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authenticationMethod");
			}
			if (this.authorityBindings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authorityBindings");
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002E278 File Offset: 0x0002C478
		public SamlAuthenticationClaimResource(DateTime authenticationInstant, string authenticationMethod, string dnsAddress, string ipAddress)
		{
			if (string.IsNullOrEmpty(authenticationMethod))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authenticationMethod");
			}
			this.authenticationInstant = authenticationInstant;
			this.authenticationMethod = authenticationMethod;
			this.dnsAddress = dnsAddress;
			this.ipAddress = ipAddress;
			this.authorityBindings = new List<SamlAuthorityBinding>().AsReadOnly();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002E2D0 File Offset: 0x0002C4D0
		public SamlAuthenticationClaimResource(DateTime authenticationInstant, string authenticationMethod, string dnsAddress, string ipAddress, IEnumerable<SamlAuthorityBinding> authorityBindings) : this(authenticationInstant, authenticationMethod, dnsAddress, ipAddress)
		{
			if (authorityBindings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("authorityBindings"));
			}
			List<SamlAuthorityBinding> list = new List<SamlAuthorityBinding>();
			foreach (SamlAuthorityBinding samlAuthorityBinding in authorityBindings)
			{
				if (samlAuthorityBinding != null)
				{
					list.Add(samlAuthorityBinding);
				}
			}
			this.authorityBindings = list.AsReadOnly();
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002E354 File Offset: 0x0002C554
		public SamlAuthenticationClaimResource(DateTime authenticationInstant, string authenticationMethod, string dnsAddress, string ipAddress, ReadOnlyCollection<SamlAuthorityBinding> authorityBindings) : this(authenticationInstant, authenticationMethod, dnsAddress, ipAddress)
		{
			if (authorityBindings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("authorityBindings"));
			}
			this.authorityBindings = authorityBindings;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002E382 File Offset: 0x0002C582
		public DateTime AuthenticationInstant
		{
			get
			{
				return this.authenticationInstant;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0002E38A File Offset: 0x0002C58A
		public string AuthenticationMethod
		{
			get
			{
				return this.authenticationMethod;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002E392 File Offset: 0x0002C592
		public ReadOnlyCollection<SamlAuthorityBinding> AuthorityBindings
		{
			get
			{
				return this.authorityBindings;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002E39C File Offset: 0x0002C59C
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		[DataMember]
		private List<SamlAuthorityBinding> SamlAuthorityBindings
		{
			get
			{
				List<SamlAuthorityBinding> list = new List<SamlAuthorityBinding>();
				for (int i = 0; i < this.authorityBindings.Count; i++)
				{
					list.Add(this.authorityBindings[i]);
				}
				return list;
			}
			set
			{
				if (value != null)
				{
					this.authorityBindings = value.AsReadOnly();
				}
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002E3E9 File Offset: 0x0002C5E9
		public string IPAddress
		{
			get
			{
				return this.ipAddress;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002E3F1 File Offset: 0x0002C5F1
		public string DnsAddress
		{
			get
			{
				return this.dnsAddress;
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002E3FC File Offset: 0x0002C5FC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			SamlAuthenticationClaimResource samlAuthenticationClaimResource = obj as SamlAuthenticationClaimResource;
			if (samlAuthenticationClaimResource == null)
			{
				return false;
			}
			if (this.AuthenticationInstant != samlAuthenticationClaimResource.AuthenticationInstant || this.AuthenticationMethod != samlAuthenticationClaimResource.AuthenticationMethod || this.AuthorityBindings.Count != samlAuthenticationClaimResource.AuthorityBindings.Count || this.IPAddress != samlAuthenticationClaimResource.IPAddress || this.DnsAddress != samlAuthenticationClaimResource.DnsAddress)
			{
				return false;
			}
			for (int i = 0; i < this.AuthorityBindings.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < samlAuthenticationClaimResource.AuthorityBindings.Count; j++)
				{
					if (this.AuthorityBindings[i].AuthorityKind == samlAuthenticationClaimResource.AuthorityBindings[j].AuthorityKind && this.AuthorityBindings[i].Binding == samlAuthenticationClaimResource.AuthorityBindings[j].Binding && this.AuthorityBindings[i].Location == samlAuthenticationClaimResource.AuthorityBindings[j].Location)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002E547 File Offset: 0x0002C747
		public override int GetHashCode()
		{
			return this.authenticationInstant.GetHashCode() ^ this.authenticationMethod.GetHashCode();
		}

		// Token: 0x04000BA9 RID: 2985
		[DataMember]
		private DateTime authenticationInstant;

		// Token: 0x04000BAA RID: 2986
		[DataMember]
		private string authenticationMethod;

		// Token: 0x04000BAB RID: 2987
		private ReadOnlyCollection<SamlAuthorityBinding> authorityBindings;

		// Token: 0x04000BAC RID: 2988
		[DataMember]
		private string dnsAddress;

		// Token: 0x04000BAD RID: 2989
		[DataMember]
		private string ipAddress;
	}
}
