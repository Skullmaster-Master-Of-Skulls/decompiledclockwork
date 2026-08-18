using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B0 RID: 2224
	internal class HttpAnonymousUriPrefixMatcher : IAnonymousUriPrefixMatcher
	{
		// Token: 0x060054C8 RID: 21704 RVA: 0x00137F63 File Offset: 0x00136163
		internal HttpAnonymousUriPrefixMatcher()
		{
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x00137F6B File Offset: 0x0013616B
		internal HttpAnonymousUriPrefixMatcher(HttpAnonymousUriPrefixMatcher objectToClone) : this()
		{
			if (objectToClone.anonymousUriPrefixes != null)
			{
				this.anonymousUriPrefixes = new UriPrefixTable<Uri>(objectToClone.anonymousUriPrefixes);
			}
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x00137F8C File Offset: 0x0013618C
		public void Register(Uri anonymousUriPrefix)
		{
			if (anonymousUriPrefix == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("anonymousUriPrefix");
			}
			if (!anonymousUriPrefix.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("anonymousUriPrefix", SR.GetString("UriMustBeAbsolute"));
			}
			if (this.anonymousUriPrefixes == null)
			{
				this.anonymousUriPrefixes = new UriPrefixTable<Uri>(true);
			}
			if (!this.anonymousUriPrefixes.IsRegistered(new BaseUriWithWildcard(anonymousUriPrefix, HostNameComparisonMode.Exact)))
			{
				this.anonymousUriPrefixes.RegisterUri(anonymousUriPrefix, HostNameComparisonMode.Exact, anonymousUriPrefix);
			}
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0013800C File Offset: 0x0013620C
		internal bool IsAnonymousUri(Uri to)
		{
			Uri uri;
			return this.anonymousUriPrefixes != null && this.anonymousUriPrefixes.TryLookupUri(to, HostNameComparisonMode.Exact, out uri);
		}

		// Token: 0x04003344 RID: 13124
		private UriPrefixTable<Uri> anonymousUriPrefixes;
	}
}
