using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Security
{
	// Token: 0x02000338 RID: 824
	public sealed class IssuedTokenClientCredential
	{
		// Token: 0x06001DCE RID: 7630 RVA: 0x0006E78B File Offset: 0x0006C98B
		internal IssuedTokenClientCredential()
		{
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0006E7B4 File Offset: 0x0006C9B4
		internal IssuedTokenClientCredential(IssuedTokenClientCredential other)
		{
			this.defaultKeyEntropyMode = other.defaultKeyEntropyMode;
			this.cacheIssuedTokens = other.cacheIssuedTokens;
			this.issuedTokenRenewalThresholdPercentage = other.issuedTokenRenewalThresholdPercentage;
			this.maxIssuedTokenCachingTime = other.maxIssuedTokenCachingTime;
			this.localIssuerAddress = other.localIssuerAddress;
			this.localIssuerBinding = ((other.localIssuerBinding != null) ? new CustomBinding(other.localIssuerBinding) : null);
			if (other.localIssuerChannelBehaviors != null)
			{
				this.localIssuerChannelBehaviors = this.GetBehaviorCollection(other.localIssuerChannelBehaviors);
			}
			if (other.issuerChannelBehaviors != null)
			{
				this.issuerChannelBehaviors = new Dictionary<Uri, KeyedByTypeCollection<IEndpointBehavior>>();
				foreach (Uri key in other.issuerChannelBehaviors.Keys)
				{
					this.issuerChannelBehaviors.Add(key, this.GetBehaviorCollection(other.issuerChannelBehaviors[key]));
				}
			}
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x0006E8DC File Offset: 0x0006CADC
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x0006E8E4 File Offset: 0x0006CAE4
		public EndpointAddress LocalIssuerAddress
		{
			get
			{
				return this.localIssuerAddress;
			}
			set
			{
				this.ThrowIfImmutable();
				this.localIssuerAddress = value;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0006E8F3 File Offset: 0x0006CAF3
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x0006E8FB File Offset: 0x0006CAFB
		public Binding LocalIssuerBinding
		{
			get
			{
				return this.localIssuerBinding;
			}
			set
			{
				this.ThrowIfImmutable();
				this.localIssuerBinding = value;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x0006E90A File Offset: 0x0006CB0A
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x0006E912 File Offset: 0x0006CB12
		public SecurityKeyEntropyMode DefaultKeyEntropyMode
		{
			get
			{
				return this.defaultKeyEntropyMode;
			}
			set
			{
				SecurityKeyEntropyModeHelper.Validate(value);
				this.ThrowIfImmutable();
				this.defaultKeyEntropyMode = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x0006E927 File Offset: 0x0006CB27
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x0006E92F File Offset: 0x0006CB2F
		public bool CacheIssuedTokens
		{
			get
			{
				return this.cacheIssuedTokens;
			}
			set
			{
				this.ThrowIfImmutable();
				this.cacheIssuedTokens = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x0006E93E File Offset: 0x0006CB3E
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x0006E946 File Offset: 0x0006CB46
		public int IssuedTokenRenewalThresholdPercentage
		{
			get
			{
				return this.issuedTokenRenewalThresholdPercentage;
			}
			set
			{
				this.ThrowIfImmutable();
				this.issuedTokenRenewalThresholdPercentage = value;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x0006E955 File Offset: 0x0006CB55
		public Dictionary<Uri, KeyedByTypeCollection<IEndpointBehavior>> IssuerChannelBehaviors
		{
			get
			{
				if (this.issuerChannelBehaviors == null)
				{
					this.issuerChannelBehaviors = new Dictionary<Uri, KeyedByTypeCollection<IEndpointBehavior>>();
				}
				return this.issuerChannelBehaviors;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0006E970 File Offset: 0x0006CB70
		public KeyedByTypeCollection<IEndpointBehavior> LocalIssuerChannelBehaviors
		{
			get
			{
				if (this.localIssuerChannelBehaviors == null)
				{
					this.localIssuerChannelBehaviors = new KeyedByTypeCollection<IEndpointBehavior>();
				}
				return this.localIssuerChannelBehaviors;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0006E98B File Offset: 0x0006CB8B
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x0006E994 File Offset: 0x0006CB94
		public TimeSpan MaxIssuedTokenCachingTime
		{
			get
			{
				return this.maxIssuedTokenCachingTime;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRange0")));
				}
				if (TimeoutHelper.IsTooLarge(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
				}
				this.ThrowIfImmutable();
				this.maxIssuedTokenCachingTime = value;
			}
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0006EA10 File Offset: 0x0006CC10
		private KeyedByTypeCollection<IEndpointBehavior> GetBehaviorCollection(KeyedByTypeCollection<IEndpointBehavior> behaviors)
		{
			KeyedByTypeCollection<IEndpointBehavior> keyedByTypeCollection = new KeyedByTypeCollection<IEndpointBehavior>();
			foreach (IEndpointBehavior item in behaviors)
			{
				keyedByTypeCollection.Add(item);
			}
			return keyedByTypeCollection;
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0006EA60 File Offset: 0x0006CC60
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0006EA69 File Offset: 0x0006CC69
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E3C RID: 7740
		private SecurityKeyEntropyMode defaultKeyEntropyMode = SecurityKeyEntropyMode.CombinedEntropy;

		// Token: 0x04001E3D RID: 7741
		private KeyedByTypeCollection<IEndpointBehavior> localIssuerChannelBehaviors;

		// Token: 0x04001E3E RID: 7742
		private Dictionary<Uri, KeyedByTypeCollection<IEndpointBehavior>> issuerChannelBehaviors;

		// Token: 0x04001E3F RID: 7743
		private bool cacheIssuedTokens = true;

		// Token: 0x04001E40 RID: 7744
		private TimeSpan maxIssuedTokenCachingTime = IssuanceTokenProviderBase<SspiNegotiationTokenProviderState>.DefaultClientMaxTokenCachingTime;

		// Token: 0x04001E41 RID: 7745
		private EndpointAddress localIssuerAddress;

		// Token: 0x04001E42 RID: 7746
		private Binding localIssuerBinding;

		// Token: 0x04001E43 RID: 7747
		private int issuedTokenRenewalThresholdPercentage = 60;

		// Token: 0x04001E44 RID: 7748
		private bool isReadOnly;
	}
}
