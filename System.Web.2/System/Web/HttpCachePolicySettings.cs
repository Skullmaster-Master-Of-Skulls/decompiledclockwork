using System;
using System.Runtime.Serialization;
using System.Web.Compilation;
using System.Web.UI;

namespace System.Web
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	internal sealed class HttpCachePolicySettings
	{
		// Token: 0x0600081B RID: 2075 RVA: 0x000114F8 File Offset: 0x0000F6F8
		internal HttpCachePolicySettings(bool isModified, ValidationCallbackInfo[] validationCallbackInfo, bool hasSetCookieHeader, bool noServerCaching, string cacheExtension, bool noTransforms, bool ignoreRangeRequests, string[] varyByContentEncodings, string[] varyByHeaderValues, string[] varyByParamValues, string varyByCustom, HttpCacheability cacheability, bool noStore, string[] privateFields, string[] noCacheFields, DateTime utcExpires, bool isExpiresSet, TimeSpan maxAge, bool isMaxAgeSet, TimeSpan proxyMaxAge, bool isProxyMaxAgeSet, int slidingExpiration, TimeSpan slidingDelta, DateTime utcTimestampCreated, int validUntilExpires, int allowInHistory, HttpCacheRevalidation revalidation, DateTime utcLastModified, bool isLastModifiedSet, string etag, bool generateLastModifiedFromFiles, bool generateEtagFromFiles, int omitVaryStar, HttpResponseHeader headerCacheControl, HttpResponseHeader headerPragma, HttpResponseHeader headerExpires, HttpResponseHeader headerLastModified, HttpResponseHeader headerEtag, HttpResponseHeader headerVaryBy, bool hasUserProvidedDependencies)
		{
			this._isModified = isModified;
			this._validationCallbackInfo = validationCallbackInfo;
			this._hasSetCookieHeader = hasSetCookieHeader;
			this._noServerCaching = noServerCaching;
			this._cacheExtension = cacheExtension;
			this._noTransforms = noTransforms;
			this._ignoreRangeRequests = ignoreRangeRequests;
			this._varyByContentEncodings = varyByContentEncodings;
			this._varyByHeaderValues = varyByHeaderValues;
			this._varyByParamValues = varyByParamValues;
			this._varyByCustom = varyByCustom;
			this._cacheability = cacheability;
			this._noStore = noStore;
			this._privateFields = privateFields;
			this._noCacheFields = noCacheFields;
			this._utcExpires = utcExpires;
			this._isExpiresSet = isExpiresSet;
			this._maxAge = maxAge;
			this._isMaxAgeSet = isMaxAgeSet;
			this._proxyMaxAge = proxyMaxAge;
			this._isProxyMaxAgeSet = isProxyMaxAgeSet;
			this._slidingExpiration = slidingExpiration;
			this._slidingDelta = slidingDelta;
			this._utcTimestampCreated = utcTimestampCreated;
			this._validUntilExpires = validUntilExpires;
			this._allowInHistory = allowInHistory;
			this._revalidation = revalidation;
			this._utcLastModified = utcLastModified;
			this._isLastModifiedSet = isLastModifiedSet;
			this._etag = etag;
			this._generateLastModifiedFromFiles = generateLastModifiedFromFiles;
			this._generateEtagFromFiles = generateEtagFromFiles;
			this._omitVaryStar = omitVaryStar;
			this._headerCacheControl = headerCacheControl;
			this._headerPragma = headerPragma;
			this._headerExpires = headerExpires;
			this._headerLastModified = headerLastModified;
			this._headerEtag = headerEtag;
			this._headerVaryBy = headerVaryBy;
			this._hasUserProvidedDependencies = hasUserProvidedDependencies;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00011648 File Offset: 0x0000F848
		[OnSerializing]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this._validationCallbackInfo == null)
			{
				return;
			}
			string[] array = new string[this._validationCallbackInfo.Length * 2];
			for (int i = 0; i < this._validationCallbackInfo.Length; i++)
			{
				HttpCacheValidateHandler handler = this._validationCallbackInfo[i].handler;
				string assemblyQualifiedTypeName = Util.GetAssemblyQualifiedTypeName(handler.Method.ReflectedType);
				string name = handler.Method.Name;
				array[2 * i] = assemblyQualifiedTypeName;
				array[2 * i + 1] = name;
			}
			this._validationCallbackInfoForSerialization = array;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x000116C4 File Offset: 0x0000F8C4
		[OnDeserialized]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this._validationCallbackInfoForSerialization == null)
			{
				return;
			}
			ValidationCallbackInfo[] array = new ValidationCallbackInfo[this._validationCallbackInfoForSerialization.Length / 2];
			for (int i = 0; i < this._validationCallbackInfoForSerialization.Length; i += 2)
			{
				string text = this._validationCallbackInfoForSerialization[i];
				string method = this._validationCallbackInfoForSerialization[i + 1];
				Type type = null;
				if (!string.IsNullOrEmpty(text))
				{
					type = BuildManager.GetType(text, true, false);
				}
				if (type == null)
				{
					throw new SerializationException(SR.GetString("Type_cannot_be_resolved", new object[]
					{
						text
					}));
				}
				HttpCacheValidateHandler handler = (HttpCacheValidateHandler)Delegate.CreateDelegate(typeof(HttpCacheValidateHandler), type, method);
				array[i / 2] = new ValidationCallbackInfo(handler, null);
			}
			this._validationCallbackInfo = array;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001177A File Offset: 0x0000F97A
		internal bool IsModified
		{
			get
			{
				return this._isModified;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00011782 File Offset: 0x0000F982
		internal ValidationCallbackInfo[] ValidationCallbackInfo
		{
			get
			{
				return this._validationCallbackInfo;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001178A File Offset: 0x0000F98A
		internal HttpResponseHeader HeaderCacheControl
		{
			get
			{
				return this._headerCacheControl;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00011792 File Offset: 0x0000F992
		internal HttpResponseHeader HeaderPragma
		{
			get
			{
				return this._headerPragma;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001179A File Offset: 0x0000F99A
		internal HttpResponseHeader HeaderExpires
		{
			get
			{
				return this._headerExpires;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000117A2 File Offset: 0x0000F9A2
		internal HttpResponseHeader HeaderLastModified
		{
			get
			{
				return this._headerLastModified;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x000117AA File Offset: 0x0000F9AA
		internal HttpResponseHeader HeaderEtag
		{
			get
			{
				return this._headerEtag;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x000117B2 File Offset: 0x0000F9B2
		internal HttpResponseHeader HeaderVaryBy
		{
			get
			{
				return this._headerVaryBy;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x000117BA File Offset: 0x0000F9BA
		internal bool hasSetCookieHeader
		{
			get
			{
				return this._hasSetCookieHeader;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x000117C2 File Offset: 0x0000F9C2
		internal bool NoServerCaching
		{
			get
			{
				return this._noServerCaching;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x000117CA File Offset: 0x0000F9CA
		internal string CacheExtension
		{
			get
			{
				return this._cacheExtension;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x000117D2 File Offset: 0x0000F9D2
		internal bool NoTransforms
		{
			get
			{
				return this._noTransforms;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x000117DA File Offset: 0x0000F9DA
		internal bool IgnoreRangeRequests
		{
			get
			{
				return this._ignoreRangeRequests;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x000117E2 File Offset: 0x0000F9E2
		internal string[] VaryByContentEncodings
		{
			get
			{
				if (this._varyByContentEncodings != null)
				{
					return (string[])this._varyByContentEncodings.Clone();
				}
				return null;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x000117FE File Offset: 0x0000F9FE
		internal string[] VaryByHeaders
		{
			get
			{
				if (this._varyByHeaderValues != null)
				{
					return (string[])this._varyByHeaderValues.Clone();
				}
				return null;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001181A File Offset: 0x0000FA1A
		internal string[] VaryByParams
		{
			get
			{
				if (this._varyByParamValues != null)
				{
					return (string[])this._varyByParamValues.Clone();
				}
				return null;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00011836 File Offset: 0x0000FA36
		internal bool IgnoreParams
		{
			get
			{
				return this._varyByParamValues != null && this._varyByParamValues[0].Length == 0;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00011852 File Offset: 0x0000FA52
		internal HttpCacheability CacheabilityInternal
		{
			get
			{
				return this._cacheability;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001185A File Offset: 0x0000FA5A
		internal bool NoStore
		{
			get
			{
				return this._noStore;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x00011862 File Offset: 0x0000FA62
		internal string[] PrivateFields
		{
			get
			{
				if (this._privateFields != null)
				{
					return (string[])this._privateFields.Clone();
				}
				return null;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001187E File Offset: 0x0000FA7E
		internal string[] NoCacheFields
		{
			get
			{
				if (this._noCacheFields != null)
				{
					return (string[])this._noCacheFields.Clone();
				}
				return null;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x0001189A File Offset: 0x0000FA9A
		internal DateTime UtcExpires
		{
			get
			{
				return this._utcExpires;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000118A2 File Offset: 0x0000FAA2
		internal bool IsExpiresSet
		{
			get
			{
				return this._isExpiresSet;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x000118AA File Offset: 0x0000FAAA
		internal TimeSpan MaxAge
		{
			get
			{
				return this._maxAge;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x000118B2 File Offset: 0x0000FAB2
		internal bool IsMaxAgeSet
		{
			get
			{
				return this._isMaxAgeSet;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x000118BA File Offset: 0x0000FABA
		internal TimeSpan ProxyMaxAge
		{
			get
			{
				return this._proxyMaxAge;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x000118C2 File Offset: 0x0000FAC2
		internal bool IsProxyMaxAgeSet
		{
			get
			{
				return this._isProxyMaxAgeSet;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x000118CA File Offset: 0x0000FACA
		internal int SlidingExpirationInternal
		{
			get
			{
				return this._slidingExpiration;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x000118D2 File Offset: 0x0000FAD2
		internal bool SlidingExpiration
		{
			get
			{
				return this._slidingExpiration == 1;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x000118DD File Offset: 0x0000FADD
		internal TimeSpan SlidingDelta
		{
			get
			{
				return this._slidingDelta;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x000118E5 File Offset: 0x0000FAE5
		internal DateTime UtcTimestampCreated
		{
			get
			{
				return this._utcTimestampCreated;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x000118ED File Offset: 0x0000FAED
		internal int ValidUntilExpiresInternal
		{
			get
			{
				return this._validUntilExpires;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x000118F5 File Offset: 0x0000FAF5
		internal bool ValidUntilExpires
		{
			get
			{
				return this._validUntilExpires == 1 && !this.SlidingExpiration && !this.GenerateLastModifiedFromFiles && !this.GenerateEtagFromFiles && this.ValidationCallbackInfo == null;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00011923 File Offset: 0x0000FB23
		internal int AllowInHistoryInternal
		{
			get
			{
				return this._allowInHistory;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001192B File Offset: 0x0000FB2B
		internal HttpCacheRevalidation Revalidation
		{
			get
			{
				return this._revalidation;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00011933 File Offset: 0x0000FB33
		internal DateTime UtcLastModified
		{
			get
			{
				return this._utcLastModified;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001193B File Offset: 0x0000FB3B
		internal bool IsLastModifiedSet
		{
			get
			{
				return this._isLastModifiedSet;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00011943 File Offset: 0x0000FB43
		internal string ETag
		{
			get
			{
				return this._etag;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001194B File Offset: 0x0000FB4B
		internal bool GenerateLastModifiedFromFiles
		{
			get
			{
				return this._generateLastModifiedFromFiles;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x00011953 File Offset: 0x0000FB53
		internal bool GenerateEtagFromFiles
		{
			get
			{
				return this._generateEtagFromFiles;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001195B File Offset: 0x0000FB5B
		internal string VaryByCustom
		{
			get
			{
				return this._varyByCustom;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00011963 File Offset: 0x0000FB63
		internal bool HasUserProvidedDependencies
		{
			get
			{
				return this._hasUserProvidedDependencies;
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001196C File Offset: 0x0000FB6C
		internal bool IsValidationCallbackSerializable()
		{
			if (this._validationCallbackInfo != null)
			{
				foreach (ValidationCallbackInfo validationCallbackInfo2 in this._validationCallbackInfo)
				{
					if (validationCallbackInfo2.data != null || !validationCallbackInfo2.handler.Method.IsStatic)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000119B7 File Offset: 0x0000FBB7
		internal bool HasValidationPolicy()
		{
			return this.ValidUntilExpires || this.GenerateLastModifiedFromFiles || this.GenerateEtagFromFiles || this.ValidationCallbackInfo != null;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x000119DC File Offset: 0x0000FBDC
		internal int OmitVaryStarInternal
		{
			get
			{
				return this._omitVaryStar;
			}
		}

		// Token: 0x040002C2 RID: 706
		internal readonly bool _isModified;

		// Token: 0x040002C3 RID: 707
		[NonSerialized]
		internal ValidationCallbackInfo[] _validationCallbackInfo;

		// Token: 0x040002C4 RID: 708
		private string[] _validationCallbackInfoForSerialization;

		// Token: 0x040002C5 RID: 709
		internal readonly HttpResponseHeader _headerCacheControl;

		// Token: 0x040002C6 RID: 710
		internal readonly HttpResponseHeader _headerPragma;

		// Token: 0x040002C7 RID: 711
		internal readonly HttpResponseHeader _headerExpires;

		// Token: 0x040002C8 RID: 712
		internal readonly HttpResponseHeader _headerLastModified;

		// Token: 0x040002C9 RID: 713
		internal readonly HttpResponseHeader _headerEtag;

		// Token: 0x040002CA RID: 714
		internal readonly HttpResponseHeader _headerVaryBy;

		// Token: 0x040002CB RID: 715
		internal readonly bool _hasSetCookieHeader;

		// Token: 0x040002CC RID: 716
		internal readonly bool _noServerCaching;

		// Token: 0x040002CD RID: 717
		internal readonly string _cacheExtension;

		// Token: 0x040002CE RID: 718
		internal readonly bool _noTransforms;

		// Token: 0x040002CF RID: 719
		internal readonly bool _ignoreRangeRequests;

		// Token: 0x040002D0 RID: 720
		internal readonly string[] _varyByContentEncodings;

		// Token: 0x040002D1 RID: 721
		internal readonly string[] _varyByHeaderValues;

		// Token: 0x040002D2 RID: 722
		internal readonly string[] _varyByParamValues;

		// Token: 0x040002D3 RID: 723
		internal readonly string _varyByCustom;

		// Token: 0x040002D4 RID: 724
		internal readonly HttpCacheability _cacheability;

		// Token: 0x040002D5 RID: 725
		internal readonly bool _noStore;

		// Token: 0x040002D6 RID: 726
		internal readonly string[] _privateFields;

		// Token: 0x040002D7 RID: 727
		internal readonly string[] _noCacheFields;

		// Token: 0x040002D8 RID: 728
		internal readonly DateTime _utcExpires;

		// Token: 0x040002D9 RID: 729
		internal readonly bool _isExpiresSet;

		// Token: 0x040002DA RID: 730
		internal readonly TimeSpan _maxAge;

		// Token: 0x040002DB RID: 731
		internal readonly bool _isMaxAgeSet;

		// Token: 0x040002DC RID: 732
		internal readonly TimeSpan _proxyMaxAge;

		// Token: 0x040002DD RID: 733
		internal readonly bool _isProxyMaxAgeSet;

		// Token: 0x040002DE RID: 734
		internal readonly int _slidingExpiration;

		// Token: 0x040002DF RID: 735
		internal readonly TimeSpan _slidingDelta;

		// Token: 0x040002E0 RID: 736
		internal readonly DateTime _utcTimestampCreated;

		// Token: 0x040002E1 RID: 737
		internal readonly int _validUntilExpires;

		// Token: 0x040002E2 RID: 738
		internal readonly int _allowInHistory;

		// Token: 0x040002E3 RID: 739
		internal readonly HttpCacheRevalidation _revalidation;

		// Token: 0x040002E4 RID: 740
		internal readonly DateTime _utcLastModified;

		// Token: 0x040002E5 RID: 741
		internal readonly bool _isLastModifiedSet;

		// Token: 0x040002E6 RID: 742
		internal readonly string _etag;

		// Token: 0x040002E7 RID: 743
		internal readonly bool _generateLastModifiedFromFiles;

		// Token: 0x040002E8 RID: 744
		internal readonly bool _generateEtagFromFiles;

		// Token: 0x040002E9 RID: 745
		internal readonly int _omitVaryStar;

		// Token: 0x040002EA RID: 746
		internal readonly bool _hasUserProvidedDependencies;
	}
}
