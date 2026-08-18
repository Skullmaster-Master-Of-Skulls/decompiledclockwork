using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Policy;
using System.Security.Claims;
using System.Security.Principal;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000110 RID: 272
	internal class AuthorizationPolicy : IAuthorizationPolicy, IAuthorizationComponent
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x0001F694 File Offset: 0x0001D894
		public AuthorizationPolicy()
		{
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		public AuthorizationPolicy(ClaimsIdentity identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			this._identityCollection.Add(identity);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001F714 File Offset: 0x0001D914
		public AuthorizationPolicy(IEnumerable<ClaimsIdentity> identityCollection)
		{
			if (identityCollection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identityCollection");
			}
			List<ClaimsIdentity> list = new List<ClaimsIdentity>();
			foreach (ClaimsIdentity item in identityCollection)
			{
				list.Add(item);
			}
			this._identityCollection = list;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
		public ReadOnlyCollection<ClaimsIdentity> IdentityCollection
		{
			get
			{
				return this._identityCollection.AsReadOnly();
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		public bool Evaluate(EvaluationContext evaluationContext, ref object state)
		{
			if (evaluationContext == null || evaluationContext.Properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("evaluationContext");
			}
			if (this._identityCollection.Count == 0)
			{
				return true;
			}
			object obj = null;
			if (!evaluationContext.Properties.TryGetValue("ClaimsPrincipal", out obj))
			{
				ClaimsPrincipal claimsPrincipal = AuthorizationPolicy.CreateClaimsPrincipalFromIdentities(this._identityCollection);
				evaluationContext.Properties.Add("ClaimsPrincipal", claimsPrincipal);
				if (DiagnosticUtility.ShouldTrace(TraceEventType.Information))
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 786438, SR.GetString("TraceSetPrincipalOnEvaluationContext"), new ClaimsPrincipalTraceRecord(claimsPrincipal), null, null);
				}
			}
			else
			{
				ClaimsPrincipal claimsPrincipal2 = obj as ClaimsPrincipal;
				if (claimsPrincipal2 != null && claimsPrincipal2.Identities != null)
				{
					claimsPrincipal2.AddIdentities(this._identityCollection);
				}
				else if (DiagnosticUtility.ShouldTrace(TraceEventType.Error))
				{
					TraceUtility.TraceString(TraceEventType.Error, SR.GetString("ID8004", new object[]
					{
						"ClaimsPrincipal"
					}), new object[0]);
				}
			}
			object obj2 = null;
			if (!evaluationContext.Properties.TryGetValue("Identities", out obj2))
			{
				List<ClaimsIdentity> list = new List<ClaimsIdentity>();
				foreach (ClaimsIdentity item in this._identityCollection)
				{
					list.Add(item);
				}
				evaluationContext.Properties.Add("Identities", list);
			}
			else
			{
				List<ClaimsIdentity> list2 = obj2 as List<ClaimsIdentity>;
				foreach (ClaimsIdentity item2 in this._identityCollection)
				{
					list2.Add(item2);
				}
			}
			return true;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001F95C File Offset: 0x0001DB5C
		private static ClaimsPrincipal CreateClaimsPrincipalFromIdentities(IEnumerable<ClaimsIdentity> identities)
		{
			ClaimsIdentity claimsIdentity = AuthorizationPolicy.SelectPrimaryIdentity(identities);
			if (claimsIdentity == null)
			{
				return new ClaimsPrincipal(new ClaimsIdentity());
			}
			ClaimsPrincipal claimsPrincipal = AuthorizationPolicy.CreateFromIdentity(claimsIdentity);
			foreach (ClaimsIdentity claimsIdentity2 in identities)
			{
				if (claimsIdentity2 != claimsIdentity)
				{
					claimsPrincipal.AddIdentity(claimsIdentity2);
				}
			}
			return claimsPrincipal;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
		private static ClaimsPrincipal CreateFromIdentity(IIdentity identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			WindowsIdentity windowsIdentity = identity as WindowsIdentity;
			if (windowsIdentity != null)
			{
				return new WindowsPrincipal(windowsIdentity);
			}
			WindowsIdentity windowsIdentity2 = identity as WindowsIdentity;
			if (windowsIdentity2 != null)
			{
				return new WindowsPrincipal(windowsIdentity2);
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				return new ClaimsPrincipal(claimsIdentity);
			}
			return new ClaimsPrincipal(new ClaimsIdentity(identity));
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001FA28 File Offset: 0x0001DC28
		private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identities");
			}
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in identities)
			{
				if (claimsIdentity2 is WindowsIdentity)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
				if (claimsIdentity2.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa") != null)
				{
					if (claimsIdentity == null)
					{
						claimsIdentity = claimsIdentity2;
					}
				}
				else if (claimsIdentity == null)
				{
					claimsIdentity = claimsIdentity2;
				}
			}
			return claimsIdentity;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		public ClaimSet Issuer
		{
			get
			{
				return this._issuer;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001FAAC File Offset: 0x0001DCAC
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x04000ABE RID: 2750
		public const string ClaimsPrincipalKey = "ClaimsPrincipal";

		// Token: 0x04000ABF RID: 2751
		public const string IdentitiesKey = "Identities";

		// Token: 0x04000AC0 RID: 2752
		private List<ClaimsIdentity> _identityCollection = new List<ClaimsIdentity>();

		// Token: 0x04000AC1 RID: 2753
		private ClaimSet _issuer = ClaimSet.System;

		// Token: 0x04000AC2 RID: 2754
		private string _id = UniqueId.CreateUniqueId();
	}
}
