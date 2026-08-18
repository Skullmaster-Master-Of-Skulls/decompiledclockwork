using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace System
{
	// Token: 0x0200000E RID: 14
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UriTemplateTable
	{
		// Token: 0x06000057 RID: 87 RVA: 0x0000402E File Offset: 0x0000222E
		public UriTemplateTable() : this(null, null, true)
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004039 File Offset: 0x00002239
		public UriTemplateTable(IEnumerable<KeyValuePair<UriTemplate, object>> keyValuePairs) : this(null, keyValuePairs, true)
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004044 File Offset: 0x00002244
		public UriTemplateTable(Uri baseAddress) : this(baseAddress, null, true)
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000404F File Offset: 0x0000224F
		internal UriTemplateTable(Uri baseAddress, bool addTrailingSlashToBaseAddress) : this(baseAddress, null, addTrailingSlashToBaseAddress)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000405A File Offset: 0x0000225A
		public UriTemplateTable(Uri baseAddress, IEnumerable<KeyValuePair<UriTemplate, object>> keyValuePairs) : this(baseAddress, keyValuePairs, true)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004068 File Offset: 0x00002268
		internal UriTemplateTable(Uri baseAddress, IEnumerable<KeyValuePair<UriTemplate, object>> keyValuePairs, bool addTrailingSlashToBaseAddress)
		{
			if (baseAddress != null && !baseAddress.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("baseAddress", SR.GetString("UTTMustBeAbsolute"));
			}
			this.addTrailingSlashToBaseAddress = addTrailingSlashToBaseAddress;
			this.originalUncanonicalizedBaseAddress = baseAddress;
			if (keyValuePairs != null)
			{
				this.templates = new UriTemplateTable.UriTemplatesCollection(keyValuePairs);
			}
			else
			{
				this.templates = new UriTemplateTable.UriTemplatesCollection();
			}
			this.thisLock = new object();
			this.baseAddress = baseAddress;
			this.NormalizeBaseAddress();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000040E8 File Offset: 0x000022E8
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000040F0 File Offset: 0x000022F0
		public Uri BaseAddress
		{
			get
			{
				return this.baseAddress;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				object obj = this.thisLock;
				lock (obj)
				{
					if (this.IsReadOnly)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTCannotChangeBaseAddress")));
					}
					if (!value.IsAbsoluteUri)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("UTTBaseAddressMustBeAbsolute"));
					}
					this.originalUncanonicalizedBaseAddress = value;
					this.baseAddress = value;
					this.NormalizeBaseAddress();
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000419C File Offset: 0x0000239C
		public Uri OriginalBaseAddress
		{
			get
			{
				return this.originalUncanonicalizedBaseAddress;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000041A4 File Offset: 0x000023A4
		public bool IsReadOnly
		{
			get
			{
				return this.templates.IsFrozen;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000041B1 File Offset: 0x000023B1
		public IList<KeyValuePair<UriTemplate, object>> KeyValuePairs
		{
			get
			{
				return this.templates;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000041BC File Offset: 0x000023BC
		public void MakeReadOnly(bool allowDuplicateEquivalentUriTemplates)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.IsReadOnly)
				{
					this.templates.Freeze();
					this.Validate(allowDuplicateEquivalentUriTemplates);
					this.ConstructFastPathTable();
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004218 File Offset: 0x00002418
		public Collection<UriTemplateMatch> Match(Uri uri)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			if (!uri.IsAbsoluteUri)
			{
				return UriTemplateTable.None();
			}
			this.MakeReadOnly(true);
			Collection<string> relativePathSegments;
			IList<UriTemplateTableMatchCandidate> list;
			if (!this.FastComputeRelativeSegmentsAndLookup(uri, out relativePathSegments, out list))
			{
				return UriTemplateTable.None();
			}
			NameValueCollection nameValueCollection = null;
			if (!this.noTemplateHasQueryPart && UriTemplateTable.AtLeastOneCandidateHasQueryPart(list))
			{
				Collection<UriTemplateTableMatchCandidate> collection = new Collection<UriTemplateTableMatchCandidate>();
				nameValueCollection = UriTemplateHelpers.ParseQueryString(uri.Query);
				bool mustBeEspeciallyInteresting = UriTemplateTable.NoCandidateHasQueryLiteralRequirementsAndThereIsAnEmptyFallback(list);
				for (int i = 0; i < list.Count; i++)
				{
					if (UriTemplateHelpers.CanMatchQueryInterestingly(list[i].Template, nameValueCollection, mustBeEspeciallyInteresting))
					{
						collection.Add(list[i]);
					}
				}
				int count = collection.Count;
				if (collection.Count == 0)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (UriTemplateHelpers.CanMatchQueryTrivially(list[j].Template))
						{
							collection.Add(list[j]);
						}
					}
				}
				if (collection.Count == 0)
				{
					return UriTemplateTable.None();
				}
				int count2 = collection.Count;
				list = collection;
			}
			if (UriTemplateTable.NotAllCandidatesArePathFullyEquivalent(list))
			{
				Collection<UriTemplateTableMatchCandidate> collection2 = new Collection<UriTemplateTableMatchCandidate>();
				int num = -1;
				for (int k = 0; k < list.Count; k++)
				{
					UriTemplateTableMatchCandidate item = list[k];
					if (num == -1)
					{
						num = item.Template.segments.Count;
						collection2.Add(item);
					}
					else if (item.Template.segments.Count < num)
					{
						num = item.Template.segments.Count;
						collection2.Clear();
						collection2.Add(item);
					}
					else if (item.Template.segments.Count == num)
					{
						collection2.Add(item);
					}
				}
				list = collection2;
			}
			Collection<UriTemplateMatch> collection3 = new Collection<UriTemplateMatch>();
			for (int l = 0; l < list.Count; l++)
			{
				UriTemplateTableMatchCandidate uriTemplateTableMatchCandidate = list[l];
				UriTemplateMatch item2 = uriTemplateTableMatchCandidate.Template.CreateUriTemplateMatch(this.originalUncanonicalizedBaseAddress, uri, uriTemplateTableMatchCandidate.Data, uriTemplateTableMatchCandidate.SegmentsCount, relativePathSegments, nameValueCollection);
				collection3.Add(item2);
			}
			return collection3;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004450 File Offset: 0x00002650
		public UriTemplateMatch MatchSingle(Uri uri)
		{
			Collection<UriTemplateMatch> collection = this.Match(uri);
			if (collection.Count == 0)
			{
				return null;
			}
			if (collection.Count == 1)
			{
				return collection[0];
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new UriTemplateMatchException(SR.GetString("UTTMultipleMatches")));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000449C File Offset: 0x0000269C
		private static bool AllEquivalent(IList<UriTemplateTableMatchCandidate> list, int a, int b)
		{
			for (int i = a; i < b - 1; i++)
			{
				if (!list[i].Template.IsPathPartiallyEquivalentAt(list[i + 1].Template, list[i].SegmentsCount))
				{
					return false;
				}
				if (!list[i].Template.IsQueryEquivalent(list[i + 1].Template))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000451C File Offset: 0x0000271C
		private static bool AtLeastOneCandidateHasQueryPart(IList<UriTemplateTableMatchCandidate> candidates)
		{
			for (int i = 0; i < candidates.Count; i++)
			{
				if (!UriTemplateHelpers.CanMatchQueryTrivially(candidates[i].Template))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004554 File Offset: 0x00002754
		private static bool NoCandidateHasQueryLiteralRequirementsAndThereIsAnEmptyFallback(IList<UriTemplateTableMatchCandidate> candidates)
		{
			bool result = false;
			for (int i = 0; i < candidates.Count; i++)
			{
				if (UriTemplateHelpers.HasQueryLiteralRequirements(candidates[i].Template))
				{
					return false;
				}
				if (candidates[i].Template.queries.Count == 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000045AA File Offset: 0x000027AA
		private static Collection<UriTemplateMatch> None()
		{
			return new Collection<UriTemplateMatch>();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000045B4 File Offset: 0x000027B4
		private static bool NotAllCandidatesArePathFullyEquivalent(IList<UriTemplateTableMatchCandidate> candidates)
		{
			if (candidates.Count <= 1)
			{
				return false;
			}
			int num = -1;
			for (int i = 0; i < candidates.Count; i++)
			{
				if (num == -1)
				{
					num = candidates[i].Template.segments.Count;
				}
				else if (num != candidates[i].Template.segments.Count)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004620 File Offset: 0x00002820
		private bool ComputeRelativeSegmentsAndLookup(Uri uri, ICollection<string> relativePathSegments, ICollection<UriTemplateTableMatchCandidate> candidates)
		{
			string[] segments = uri.Segments;
			int num = segments.Length - this.numSegmentsInBaseAddress;
			UriTemplateLiteralPathSegment[] array = new UriTemplateLiteralPathSegment[num];
			for (int i = 0; i < num; i++)
			{
				string text = segments[i + this.numSegmentsInBaseAddress];
				UriTemplateLiteralPathSegment uriTemplateLiteralPathSegment = UriTemplateLiteralPathSegment.CreateFromWireData(text);
				array[i] = uriTemplateLiteralPathSegment;
				string text2 = Uri.UnescapeDataString(text);
				if (uriTemplateLiteralPathSegment.EndsWithSlash)
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				relativePathSegments.Add(text2);
			}
			return this.rootNode.Match(array, candidates);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000046A8 File Offset: 0x000028A8
		private void ConstructFastPathTable()
		{
			this.noTemplateHasQueryPart = true;
			foreach (KeyValuePair<UriTemplate, object> keyValuePair in this.templates)
			{
				UriTemplate key = keyValuePair.Key;
				if (!UriTemplateHelpers.CanMatchQueryTrivially(key))
				{
					this.noTemplateHasQueryPart = false;
				}
				if (key.HasNoVariables && !key.HasWildcard)
				{
					if (this.fastPathTable == null)
					{
						this.fastPathTable = new Dictionary<string, UriTemplateTable.FastPathInfo>();
					}
					Uri uri = key.BindByPosition(this.originalUncanonicalizedBaseAddress, new string[0]);
					string uriPath = UriTemplateHelpers.GetUriPath(uri);
					if (!this.fastPathTable.ContainsKey(uriPath))
					{
						UriTemplateTable.FastPathInfo fastPathInfo = new UriTemplateTable.FastPathInfo();
						if (this.ComputeRelativeSegmentsAndLookup(uri, fastPathInfo.RelativePathSegments, fastPathInfo.Candidates))
						{
							fastPathInfo.Freeze();
							this.fastPathTable.Add(uriPath, fastPathInfo);
						}
					}
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004794 File Offset: 0x00002994
		private bool FastComputeRelativeSegmentsAndLookup(Uri uri, out Collection<string> relativePathSegments, out IList<UriTemplateTableMatchCandidate> candidates)
		{
			string uriPath = UriTemplateHelpers.GetUriPath(uri);
			UriTemplateTable.FastPathInfo fastPathInfo = null;
			if (this.fastPathTable != null && this.fastPathTable.TryGetValue(uriPath, out fastPathInfo))
			{
				relativePathSegments = fastPathInfo.RelativePathSegments;
				candidates = fastPathInfo.Candidates;
				return true;
			}
			relativePathSegments = new Collection<string>();
			candidates = new Collection<UriTemplateTableMatchCandidate>();
			return this.SlowComputeRelativeSegmentsAndLookup(uri, uriPath, relativePathSegments, candidates);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000047F0 File Offset: 0x000029F0
		private void NormalizeBaseAddress()
		{
			if (this.baseAddress != null)
			{
				UriBuilder uriBuilder = new UriBuilder(this.baseAddress);
				if (this.addTrailingSlashToBaseAddress && !uriBuilder.Path.EndsWith("/", StringComparison.Ordinal))
				{
					uriBuilder.Path += "/";
				}
				uriBuilder.Host = "localhost";
				uriBuilder.Port = -1;
				uriBuilder.UserName = null;
				uriBuilder.Password = null;
				uriBuilder.Path = uriBuilder.Path.ToUpperInvariant();
				uriBuilder.Scheme = Uri.UriSchemeHttp;
				this.baseAddress = uriBuilder.Uri;
				this.basePath = UriTemplateHelpers.GetUriPath(this.baseAddress);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000048A4 File Offset: 0x00002AA4
		private bool SlowComputeRelativeSegmentsAndLookup(Uri uri, string uriPath, Collection<string> relativePathSegments, ICollection<UriTemplateTableMatchCandidate> candidates)
		{
			return uriPath.Length >= this.basePath.Length && uriPath.StartsWith(this.basePath, StringComparison.OrdinalIgnoreCase) && (uriPath.Length <= this.basePath.Length || this.basePath.EndsWith("/", StringComparison.Ordinal) || uriPath[this.basePath.Length] == '/') && this.ComputeRelativeSegmentsAndLookup(uri, relativePathSegments, candidates);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004920 File Offset: 0x00002B20
		private void Validate(bool allowDuplicateEquivalentUriTemplates)
		{
			if (this.baseAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTBaseAddressNotSet")));
			}
			this.numSegmentsInBaseAddress = this.baseAddress.Segments.Length;
			if (this.templates.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTTEmptyKeyValuePairs")));
			}
			this.rootNode = UriTemplateTrieNode.Make(this.templates, allowDuplicateEquivalentUriTemplates);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000049A4 File Offset: 0x00002BA4
		[Conditional("DEBUG")]
		private void VerifyThatFastPathAndSlowPathHaveSameResults(Uri uri, Collection<string> fastPathRelativePathSegments, IList<UriTemplateTableMatchCandidate> fastPathCandidates)
		{
			Collection<string> collection = new Collection<string>();
			List<UriTemplateTableMatchCandidate> list = new List<UriTemplateTableMatchCandidate>();
			this.SlowComputeRelativeSegmentsAndLookup(uri, UriTemplateHelpers.GetUriPath(uri), collection, list);
			int count = fastPathRelativePathSegments.Count;
			int count2 = collection.Count;
			for (int i = 0; i < fastPathRelativePathSegments.Count; i++)
			{
				fastPathRelativePathSegments[i] != collection[i];
			}
			int count3 = fastPathCandidates.Count;
			int count4 = list.Count;
			for (int j = 0; j < fastPathCandidates.Count; j++)
			{
				list.Contains(fastPathCandidates[j]);
			}
		}

		// Token: 0x04000072 RID: 114
		private Uri baseAddress;

		// Token: 0x04000073 RID: 115
		private string basePath;

		// Token: 0x04000074 RID: 116
		private Dictionary<string, UriTemplateTable.FastPathInfo> fastPathTable;

		// Token: 0x04000075 RID: 117
		private bool noTemplateHasQueryPart;

		// Token: 0x04000076 RID: 118
		private int numSegmentsInBaseAddress;

		// Token: 0x04000077 RID: 119
		private Uri originalUncanonicalizedBaseAddress;

		// Token: 0x04000078 RID: 120
		private UriTemplateTrieNode rootNode;

		// Token: 0x04000079 RID: 121
		private UriTemplateTable.UriTemplatesCollection templates;

		// Token: 0x0400007A RID: 122
		private object thisLock;

		// Token: 0x0400007B RID: 123
		private bool addTrailingSlashToBaseAddress;

		// Token: 0x02000AB7 RID: 2743
		private class FastPathInfo
		{
			// Token: 0x06006DFC RID: 28156 RVA: 0x0019B268 File Offset: 0x00199468
			public FastPathInfo()
			{
				this.relativePathSegments = new FreezableCollection<string>();
				this.candidates = new FreezableCollection<UriTemplateTableMatchCandidate>();
			}

			// Token: 0x170019AA RID: 6570
			// (get) Token: 0x06006DFD RID: 28157 RVA: 0x0019B286 File Offset: 0x00199486
			public Collection<UriTemplateTableMatchCandidate> Candidates
			{
				get
				{
					return this.candidates;
				}
			}

			// Token: 0x170019AB RID: 6571
			// (get) Token: 0x06006DFE RID: 28158 RVA: 0x0019B28E File Offset: 0x0019948E
			public Collection<string> RelativePathSegments
			{
				get
				{
					return this.relativePathSegments;
				}
			}

			// Token: 0x06006DFF RID: 28159 RVA: 0x0019B296 File Offset: 0x00199496
			public void Freeze()
			{
				this.relativePathSegments.Freeze();
				this.candidates.Freeze();
			}

			// Token: 0x04003EE5 RID: 16101
			private FreezableCollection<UriTemplateTableMatchCandidate> candidates;

			// Token: 0x04003EE6 RID: 16102
			private FreezableCollection<string> relativePathSegments;
		}

		// Token: 0x02000AB8 RID: 2744
		private class UriTemplatesCollection : FreezableCollection<KeyValuePair<UriTemplate, object>>
		{
			// Token: 0x06006E00 RID: 28160 RVA: 0x0019B2AE File Offset: 0x001994AE
			public UriTemplatesCollection()
			{
			}

			// Token: 0x06006E01 RID: 28161 RVA: 0x0019B2B8 File Offset: 0x001994B8
			public UriTemplatesCollection(IEnumerable<KeyValuePair<UriTemplate, object>> keyValuePairs)
			{
				foreach (KeyValuePair<UriTemplate, object> item in keyValuePairs)
				{
					UriTemplateTable.UriTemplatesCollection.ThrowIfInvalid(item.Key, "keyValuePairs");
					base.Add(item);
				}
			}

			// Token: 0x06006E02 RID: 28162 RVA: 0x0019B318 File Offset: 0x00199518
			protected override void InsertItem(int index, KeyValuePair<UriTemplate, object> item)
			{
				UriTemplateTable.UriTemplatesCollection.ThrowIfInvalid(item.Key, "item");
				base.InsertItem(index, item);
			}

			// Token: 0x06006E03 RID: 28163 RVA: 0x0019B333 File Offset: 0x00199533
			protected override void SetItem(int index, KeyValuePair<UriTemplate, object> item)
			{
				UriTemplateTable.UriTemplatesCollection.ThrowIfInvalid(item.Key, "item");
				base.SetItem(index, item);
			}

			// Token: 0x06006E04 RID: 28164 RVA: 0x0019B350 File Offset: 0x00199550
			private static void ThrowIfInvalid(UriTemplate template, string argName)
			{
				if (template == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(argName, SR.GetString("UTTNullTemplateKey"));
				}
				if (template.IgnoreTrailingSlash)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(argName, SR.GetString("UTTInvalidTemplateKey", new object[]
					{
						template
					}));
				}
			}
		}
	}
}
