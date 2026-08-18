using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public sealed class HostAuthenticationAttribute : Attribute, IAuthenticationFilter, IFilter
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000030B1 File Offset: 0x000012B1
		public HostAuthenticationAttribute(string authenticationType) : this(new HostAuthenticationFilter(authenticationType))
		{
			this._authenticationType = authenticationType;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030C6 File Offset: 0x000012C6
		internal HostAuthenticationAttribute(IAuthenticationFilter innerFilter)
		{
			if (innerFilter == null)
			{
				throw new ArgumentNullException("innerFilter");
			}
			this._innerFilter = innerFilter;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000030E3 File Offset: 0x000012E3
		public bool AllowMultiple
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000030E6 File Offset: 0x000012E6
		public string AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000030EE File Offset: 0x000012EE
		internal IAuthenticationFilter InnerFilter
		{
			get
			{
				return this._innerFilter;
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030F6 File Offset: 0x000012F6
		public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			return this._innerFilter.AuthenticateAsync(context, cancellationToken);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003105 File Offset: 0x00001305
		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			return this._innerFilter.ChallengeAsync(context, cancellationToken);
		}

		// Token: 0x0400000D RID: 13
		private readonly IAuthenticationFilter _innerFilter;

		// Token: 0x0400000E RID: 14
		private readonly string _authenticationType;
	}
}
