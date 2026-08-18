using System;
using System.DirectoryServices;
using System.IdentityModel.Claims;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000BA RID: 186
	[__DynamicallyInvokable]
	public class SpnEndpointIdentity : EndpointIdentity
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0001240F File Offset: 0x0001060F
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00012416 File Offset: 0x00010616
		[__DynamicallyInvokable]
		public static TimeSpan SpnLookupTime
		{
			[__DynamicallyInvokable]
			get
			{
				return SpnEndpointIdentity.spnLookupTime;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value.Ticks < 0L)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value.Ticks, SR.GetString("ValueMustBeNonNegative")));
				}
				SpnEndpointIdentity.spnLookupTime = value;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00012454 File Offset: 0x00010654
		[__DynamicallyInvokable]
		public SpnEndpointIdentity(string spnName)
		{
			if (spnName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("spnName");
			}
			base.Initialize(Claim.CreateSpnClaim(spnName));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00012488 File Offset: 0x00010688
		public SpnEndpointIdentity(Claim identity)
		{
			if (identity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("identity");
			}
			if (!identity.ClaimType.Equals(ClaimTypes.Spn))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("UnrecognizedClaimTypeForIdentity", new object[]
				{
					identity.ClaimType,
					ClaimTypes.Spn
				}));
			}
			base.Initialize(identity);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000124FE File Offset: 0x000106FE
		internal override void WriteContentsTo(XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteElementString(XD.AddressingDictionary.Spn, XD.AddressingDictionary.IdentityExtensionNamespace, (string)base.IdentityClaim.Resource);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00012540 File Offset: 0x00010740
		internal SecurityIdentifier GetSpnSid()
		{
			if (!this.hasSpnSidBeenComputed)
			{
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.hasSpnSidBeenComputed)
					{
						string text = null;
						try
						{
							if (ClaimTypes.Dns.Equals(base.IdentityClaim.ClaimType))
							{
								text = "host/" + (string)base.IdentityClaim.Resource;
							}
							else
							{
								text = (string)base.IdentityClaim.Resource;
							}
							if (text != null)
							{
								text = text.Replace("*", "\\*").Replace("(", "\\(").Replace(")", "\\)");
							}
							DirectoryEntry searchRoot = SpnEndpointIdentity.GetDirectoryEntry();
							using (DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot))
							{
								directorySearcher.CacheResults = true;
								directorySearcher.ClientTimeout = SpnEndpointIdentity.SpnLookupTime;
								directorySearcher.Filter = "(&(objectCategory=Computer)(objectClass=computer)(servicePrincipalName=" + text + "))";
								directorySearcher.PropertiesToLoad.Add("objectSid");
								SearchResult searchResult = directorySearcher.FindOne();
								if (searchResult != null)
								{
									byte[] binaryForm = (byte[])searchResult.Properties["objectSid"][0];
									this.spnSid = new SecurityIdentifier(binaryForm, 0);
								}
								else
								{
									SecurityTraceRecordHelper.TraceSpnToSidMappingFailure(text, null);
								}
							}
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							if (ex is NullReferenceException || ex is SEHException)
							{
								throw;
							}
							SecurityTraceRecordHelper.TraceSpnToSidMappingFailure(text, ex);
						}
						finally
						{
							this.hasSpnSidBeenComputed = true;
						}
					}
				}
			}
			return this.spnSid;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0001273C File Offset: 0x0001093C
		private static DirectoryEntry GetDirectoryEntry()
		{
			if (SpnEndpointIdentity.directoryEntry == null)
			{
				object obj = SpnEndpointIdentity.typeLock;
				lock (obj)
				{
					if (SpnEndpointIdentity.directoryEntry == null)
					{
						DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + SecurityUtils.GetPrimaryDomain());
						directoryEntry.RefreshCache(new string[]
						{
							"name"
						});
						SpnEndpointIdentity.directoryEntry = directoryEntry;
					}
				}
			}
			return SpnEndpointIdentity.directoryEntry;
		}

		// Token: 0x0400096A RID: 2410
		private static TimeSpan spnLookupTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400096B RID: 2411
		private SecurityIdentifier spnSid;

		// Token: 0x0400096C RID: 2412
		private volatile bool hasSpnSidBeenComputed;

		// Token: 0x0400096D RID: 2413
		private object thisLock = new object();

		// Token: 0x0400096E RID: 2414
		private static object typeLock = new object();

		// Token: 0x0400096F RID: 2415
		private static volatile DirectoryEntry directoryEntry;
	}
}
