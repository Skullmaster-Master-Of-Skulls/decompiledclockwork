using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x02000009 RID: 9
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UriTemplate
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000029D9 File Offset: 0x00000BD9
		public UriTemplate(string template) : this(template, false)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000029E3 File Offset: 0x00000BE3
		public UriTemplate(string template, bool ignoreTrailingSlash) : this(template, ignoreTrailingSlash, null)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000029EE File Offset: 0x00000BEE
		public UriTemplate(string template, IDictionary<string, string> additionalDefaults) : this(template, false, additionalDefaults)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029FC File Offset: 0x00000BFC
		public UriTemplate(string template, bool ignoreTrailingSlash, IDictionary<string, string> additionalDefaults)
		{
			if (template == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("template");
			}
			this.originalTemplate = template;
			this.ignoreTrailingSlash = ignoreTrailingSlash;
			this.segments = new List<UriTemplatePathSegment>();
			this.queries = new Dictionary<string, UriTemplateQueryValue>(StringComparer.OrdinalIgnoreCase);
			if (template.StartsWith("/", StringComparison.Ordinal))
			{
				template = template.Substring(1);
			}
			int num = template.IndexOf('#');
			if (num == -1)
			{
				this.fragment = "";
			}
			else
			{
				this.fragment = template.Substring(num + 1);
				template = template.Substring(0, num);
			}
			int num2 = template.IndexOf('?');
			string text;
			string text2;
			if (num2 == -1)
			{
				text = string.Empty;
				text2 = template;
			}
			else
			{
				text = template.Substring(num2 + 1);
				text2 = template.Substring(0, num2);
			}
			template = null;
			if (!string.IsNullOrEmpty(text2))
			{
				int i = 0;
				while (i < text2.Length)
				{
					int num3 = text2.IndexOf('/', i);
					string segment;
					if (num3 != -1)
					{
						segment = text2.Substring(i, num3 + 1 - i);
						i = num3 + 1;
					}
					else
					{
						segment = text2.Substring(i);
						i = text2.Length;
					}
					UriTemplatePartType uriTemplatePartType;
					if (i == text2.Length && UriTemplateHelpers.IsWildcardSegment(segment, out uriTemplatePartType))
					{
						if (uriTemplatePartType != UriTemplatePartType.Literal)
						{
							if (uriTemplatePartType == UriTemplatePartType.Variable)
							{
								this.wildcard = new UriTemplate.WildcardInfo(this, segment);
							}
						}
						else
						{
							this.wildcard = new UriTemplate.WildcardInfo(this);
						}
					}
					else
					{
						this.segments.Add(UriTemplatePathSegment.CreateFromUriTemplate(segment, this));
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				int j = 0;
				while (j < text.Length)
				{
					int num4 = text.IndexOf('&', j);
					int num5 = j;
					int num6;
					if (num4 != -1)
					{
						num6 = num4;
						j = num4 + 1;
						if (j >= text.Length)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTQueryCannotEndInAmpersand", new object[]
							{
								this.originalTemplate
							})));
						}
					}
					else
					{
						num6 = text.Length;
						j = text.Length;
					}
					int num7 = text.IndexOf('=', num5, num6 - num5);
					string text3;
					string value;
					if (num7 >= 0)
					{
						text3 = text.Substring(num5, num7 - num5);
						value = text.Substring(num7 + 1, num6 - num7 - 1);
					}
					else
					{
						text3 = text.Substring(num5, num6 - num5);
						value = null;
					}
					if (string.IsNullOrEmpty(text3))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTQueryCannotHaveEmptyName", new object[]
						{
							this.originalTemplate
						})));
					}
					if (UriTemplateHelpers.IdentifyPartType(text3) != UriTemplatePartType.Literal)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("template", SR.GetString("UTQueryMustHaveLiteralNames", new object[]
						{
							this.originalTemplate
						}));
					}
					text3 = UrlUtility.UrlDecode(text3, Encoding.UTF8);
					if (this.queries.ContainsKey(text3))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTQueryNamesMustBeUnique", new object[]
						{
							this.originalTemplate
						})));
					}
					this.queries.Add(text3, UriTemplateQueryValue.CreateFromUriTemplate(value, this));
				}
			}
			if (additionalDefaults != null)
			{
				if (this.variables == null)
				{
					if (additionalDefaults.Count > 0)
					{
						this.additionalDefaults = new Dictionary<string, string>(additionalDefaults, StringComparer.OrdinalIgnoreCase);
					}
				}
				else
				{
					foreach (KeyValuePair<string, string> keyValuePair in additionalDefaults)
					{
						string text4 = keyValuePair.Key.ToUpperInvariant();
						if (this.variables.DefaultValues != null && this.variables.DefaultValues.ContainsKey(text4))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("additionalDefaults", SR.GetString("UTAdditionalDefaultIsInvalid", new object[]
							{
								keyValuePair.Key,
								this.originalTemplate
							}));
						}
						if (this.variables.PathSegmentVariableNames.Contains(text4))
						{
							this.variables.AddDefaultValue(text4, keyValuePair.Value);
						}
						else
						{
							if (this.variables.QueryValueVariableNames.Contains(text4))
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTDefaultValueToQueryVarFromAdditionalDefaults", new object[]
								{
									this.originalTemplate,
									text4
								})));
							}
							if (string.Compare(keyValuePair.Value, "null", StringComparison.OrdinalIgnoreCase) == 0)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTNullableDefaultAtAdditionalDefaults", new object[]
								{
									this.originalTemplate,
									text4
								})));
							}
							if (this.additionalDefaults == null)
							{
								this.additionalDefaults = new Dictionary<string, string>(additionalDefaults.Count, StringComparer.OrdinalIgnoreCase);
							}
							this.additionalDefaults.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
			}
			if (this.variables != null && this.variables.DefaultValues != null)
			{
				this.variables.ValidateDefaults(out this.firstOptionalSegment);
				return;
			}
			this.firstOptionalSegment = this.segments.Count;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002EFC File Offset: 0x000010FC
		public IDictionary<string, string> Defaults
		{
			get
			{
				if (this.defaults == null)
				{
					Interlocked.CompareExchange<IDictionary<string, string>>(ref this.defaults, new UriTemplate.UriTemplateDefaults(this), null);
				}
				return this.defaults;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002F1F File Offset: 0x0000111F
		public bool IgnoreTrailingSlash
		{
			get
			{
				return this.ignoreTrailingSlash;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002F27 File Offset: 0x00001127
		public ReadOnlyCollection<string> PathSegmentVariableNames
		{
			get
			{
				if (this.variables == null)
				{
					return UriTemplate.VariablesCollection.EmptyCollection;
				}
				return this.variables.PathSegmentVariableNames;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002F42 File Offset: 0x00001142
		public ReadOnlyCollection<string> QueryValueVariableNames
		{
			get
			{
				if (this.variables == null)
				{
					return UriTemplate.VariablesCollection.EmptyCollection;
				}
				return this.variables.QueryValueVariableNames;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002F5D File Offset: 0x0000115D
		internal bool HasNoVariables
		{
			get
			{
				return this.variables == null;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002F68 File Offset: 0x00001168
		internal bool HasWildcard
		{
			get
			{
				return this.wildcard != null;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002F73 File Offset: 0x00001173
		public Uri BindByName(Uri baseAddress, IDictionary<string, string> parameters)
		{
			return this.BindByName(baseAddress, parameters, false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002F80 File Offset: 0x00001180
		public Uri BindByName(Uri baseAddress, IDictionary<string, string> parameters, bool omitDefaults)
		{
			if (baseAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseAddress");
			}
			if (!baseAddress.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("baseAddress", SR.GetString("UTBadBaseAddress"));
			}
			UriTemplate.BindInformation bindInfo;
			if (this.variables == null)
			{
				bindInfo = this.PrepareBindInformation(parameters, omitDefaults);
			}
			else
			{
				bindInfo = this.variables.PrepareBindInformation(parameters, omitDefaults);
			}
			return this.Bind(baseAddress, bindInfo, omitDefaults);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002FF2 File Offset: 0x000011F2
		public Uri BindByName(Uri baseAddress, NameValueCollection parameters)
		{
			return this.BindByName(baseAddress, parameters, false);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003000 File Offset: 0x00001200
		public Uri BindByName(Uri baseAddress, NameValueCollection parameters, bool omitDefaults)
		{
			if (baseAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseAddress");
			}
			if (!baseAddress.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("baseAddress", SR.GetString("UTBadBaseAddress"));
			}
			UriTemplate.BindInformation bindInfo;
			if (this.variables == null)
			{
				bindInfo = this.PrepareBindInformation(parameters, omitDefaults);
			}
			else
			{
				bindInfo = this.variables.PrepareBindInformation(parameters, omitDefaults);
			}
			return this.Bind(baseAddress, bindInfo, omitDefaults);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003074 File Offset: 0x00001274
		public Uri BindByPosition(Uri baseAddress, params string[] values)
		{
			if (baseAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseAddress");
			}
			if (!baseAddress.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("baseAddress", SR.GetString("UTBadBaseAddress"));
			}
			UriTemplate.BindInformation bindInfo;
			if (this.variables == null)
			{
				if (values.Length != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTBindByPositionNoVariables", new object[]
					{
						this.originalTemplate,
						values.Length
					})));
				}
				bindInfo = new UriTemplate.BindInformation(this.additionalDefaults);
			}
			else
			{
				bindInfo = this.variables.PrepareBindInformation(values);
			}
			return this.Bind(baseAddress, bindInfo, false);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003121 File Offset: 0x00001321
		public bool IsEquivalentTo(UriTemplate other)
		{
			return other != null && other.segments != null && other.queries != null && this.IsPathFullyEquivalent(other) && this.IsQueryEquivalent(other);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003154 File Offset: 0x00001354
		public UriTemplateMatch Match(Uri baseAddress, Uri candidate)
		{
			if (baseAddress == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("baseAddress");
			}
			if (!baseAddress.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("baseAddress", SR.GetString("UTBadBaseAddress"));
			}
			if (candidate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("candidate");
			}
			if (!candidate.IsAbsoluteUri)
			{
				return null;
			}
			string uriPath = UriTemplateHelpers.GetUriPath(baseAddress);
			string uriPath2 = UriTemplateHelpers.GetUriPath(candidate);
			if (uriPath2.Length < uriPath.Length)
			{
				return null;
			}
			if (!uriPath2.StartsWith(uriPath, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			int numSegmentsInBaseAddress = baseAddress.Segments.Length;
			string[] candidateSegments = candidate.Segments;
			int numMatchedSegments;
			Collection<string> relativePathSegments;
			if (!this.IsCandidatePathMatch(numSegmentsInBaseAddress, candidateSegments, out numMatchedSegments, out relativePathSegments))
			{
				return null;
			}
			NameValueCollection nameValueCollection = null;
			if (!UriTemplateHelpers.CanMatchQueryTrivially(this))
			{
				nameValueCollection = UriTemplateHelpers.ParseQueryString(candidate.Query);
				if (!UriTemplateHelpers.CanMatchQueryInterestingly(this, nameValueCollection, false))
				{
					return null;
				}
			}
			return this.CreateUriTemplateMatch(baseAddress, candidate, null, numMatchedSegments, relativePathSegments, nameValueCollection);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000323D File Offset: 0x0000143D
		public override string ToString()
		{
			return this.originalTemplate;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003248 File Offset: 0x00001448
		internal string AddPathVariable(UriTemplatePartType sourceNature, string varDeclaration)
		{
			bool flag;
			return this.AddPathVariable(sourceNature, varDeclaration, out flag);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000325F File Offset: 0x0000145F
		internal string AddPathVariable(UriTemplatePartType sourceNature, string varDeclaration, out bool hasDefaultValue)
		{
			if (this.variables == null)
			{
				this.variables = new UriTemplate.VariablesCollection(this);
			}
			return this.variables.AddPathVariable(sourceNature, varDeclaration, out hasDefaultValue);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003283 File Offset: 0x00001483
		internal string AddQueryVariable(string varDeclaration)
		{
			if (this.variables == null)
			{
				this.variables = new UriTemplate.VariablesCollection(this);
			}
			return this.variables.AddQueryVariable(varDeclaration);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000032A8 File Offset: 0x000014A8
		internal UriTemplateMatch CreateUriTemplateMatch(Uri baseUri, Uri uri, object data, int numMatchedSegments, Collection<string> relativePathSegments, NameValueCollection uriQuery)
		{
			UriTemplateMatch uriTemplateMatch = new UriTemplateMatch();
			uriTemplateMatch.RequestUri = uri;
			uriTemplateMatch.BaseUri = baseUri;
			if (uriQuery != null)
			{
				uriTemplateMatch.SetQueryParameters(uriQuery);
			}
			uriTemplateMatch.SetRelativePathSegments(relativePathSegments);
			uriTemplateMatch.Data = data;
			uriTemplateMatch.Template = this;
			for (int i = 0; i < numMatchedSegments; i++)
			{
				this.segments[i].Lookup(uriTemplateMatch.RelativePathSegments[i], uriTemplateMatch.BoundVariables);
			}
			if (this.wildcard != null)
			{
				this.wildcard.Lookup(numMatchedSegments, uriTemplateMatch.RelativePathSegments, uriTemplateMatch.BoundVariables);
			}
			else if (numMatchedSegments < this.segments.Count)
			{
				this.BindTerminalDefaults(numMatchedSegments, uriTemplateMatch.BoundVariables);
			}
			if (this.queries.Count > 0)
			{
				foreach (KeyValuePair<string, UriTemplateQueryValue> keyValuePair in this.queries)
				{
					keyValuePair.Value.Lookup(uriTemplateMatch.QueryParameters[keyValuePair.Key], uriTemplateMatch.BoundVariables);
				}
			}
			if (this.additionalDefaults != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair2 in this.additionalDefaults)
				{
					uriTemplateMatch.BoundVariables.Add(keyValuePair2.Key, this.UnescapeDefaultValue(keyValuePair2.Value));
				}
			}
			uriTemplateMatch.SetWildcardPathSegmentsStart(numMatchedSegments);
			return uriTemplateMatch;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000343C File Offset: 0x0000163C
		internal bool IsPathPartiallyEquivalentAt(UriTemplate other, int segmentsCount)
		{
			for (int i = 0; i < segmentsCount; i++)
			{
				if (!this.segments[i].IsEquivalentTo(other.segments[i], i == segmentsCount - 1 && (this.ignoreTrailingSlash || other.ignoreTrailingSlash)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003494 File Offset: 0x00001694
		internal bool IsQueryEquivalent(UriTemplate other)
		{
			if (this.queries.Count != other.queries.Count)
			{
				return false;
			}
			foreach (string key in this.queries.Keys)
			{
				UriTemplateQueryValue uriTemplateQueryValue = this.queries[key];
				UriTemplateQueryValue other2;
				if (!other.queries.TryGetValue(key, out other2))
				{
					return false;
				}
				if (!uriTemplateQueryValue.IsEquivalentTo(other2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003534 File Offset: 0x00001734
		internal static Uri RewriteUri(Uri uri, string host)
		{
			if (!string.IsNullOrEmpty(host))
			{
				string a = uri.Host + ((!uri.IsDefaultPort) ? (":" + uri.Port.ToString(CultureInfo.InvariantCulture)) : string.Empty);
				if (!string.Equals(a, host, StringComparison.OrdinalIgnoreCase))
				{
					Uri uri2 = new Uri(string.Format(CultureInfo.InvariantCulture, "{0}://{1}", new object[]
					{
						uri.Scheme,
						host
					}));
					return new UriBuilder(uri)
					{
						Host = uri2.Host,
						Port = uri2.Port
					}.Uri;
				}
			}
			return uri;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000035DC File Offset: 0x000017DC
		private Uri Bind(Uri baseAddress, UriTemplate.BindInformation bindInfo, bool omitDefaults)
		{
			UriBuilder uriBuilder = new UriBuilder(baseAddress);
			int i = 0;
			int num = (this.variables == null) ? -1 : (this.variables.PathSegmentVariableNames.Count - 1);
			int num2;
			if (num == -1)
			{
				num2 = -1;
			}
			else if (omitDefaults)
			{
				num2 = bindInfo.LastNonDefaultPathParameter;
			}
			else
			{
				num2 = bindInfo.LastNonNullablePathParameter;
			}
			string[] normalizedParameters = bindInfo.NormalizedParameters;
			IDictionary<string, string> additionalParameters = bindInfo.AdditionalParameters;
			StringBuilder stringBuilder = new StringBuilder(uriBuilder.Path);
			if (stringBuilder[stringBuilder.Length - 1] != '/')
			{
				stringBuilder.Append('/');
			}
			if (num2 < num)
			{
				int index = 0;
				while (i <= num2)
				{
					this.segments[index++].Bind(normalizedParameters, ref i, stringBuilder);
				}
				while (this.segments[index].Nature == UriTemplatePartType.Literal)
				{
					this.segments[index++].Bind(normalizedParameters, ref i, stringBuilder);
				}
				i = num + 1;
			}
			else if (this.segments.Count > 0 || this.wildcard != null)
			{
				for (int j = 0; j < this.segments.Count; j++)
				{
					this.segments[j].Bind(normalizedParameters, ref i, stringBuilder);
				}
				if (this.wildcard != null)
				{
					this.wildcard.Bind(normalizedParameters, ref i, stringBuilder);
				}
			}
			if (this.ignoreTrailingSlash && stringBuilder[stringBuilder.Length - 1] == '/')
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			uriBuilder.Path = stringBuilder.ToString();
			if (this.queries.Count != 0 || additionalParameters != null)
			{
				StringBuilder stringBuilder2 = new StringBuilder("");
				foreach (string text in this.queries.Keys)
				{
					this.queries[text].Bind(text, normalizedParameters, ref i, stringBuilder2);
				}
				if (additionalParameters != null)
				{
					foreach (string text2 in additionalParameters.Keys)
					{
						if (this.queries.ContainsKey(text2.ToUpperInvariant()))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("parameters", SR.GetString("UTBothLiteralAndNameValueCollectionKey", new object[]
							{
								text2
							}));
						}
						string text3 = additionalParameters[text2];
						string arg = string.IsNullOrEmpty(text3) ? string.Empty : UrlUtility.UrlEncode(text3, Encoding.UTF8);
						stringBuilder2.AppendFormat("&{0}={1}", UrlUtility.UrlEncode(text2, Encoding.UTF8), arg);
					}
				}
				if (stringBuilder2.Length != 0)
				{
					stringBuilder2.Remove(0, 1);
				}
				uriBuilder.Query = stringBuilder2.ToString();
			}
			if (this.fragment != null)
			{
				uriBuilder.Fragment = this.fragment;
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000038EC File Offset: 0x00001AEC
		private void BindTerminalDefaults(int numMatchedSegments, NameValueCollection boundParameters)
		{
			for (int i = numMatchedSegments; i < this.segments.Count; i++)
			{
				UriTemplatePartType nature = this.segments[i].Nature;
				if (nature == UriTemplatePartType.Variable)
				{
					UriTemplateVariablePathSegment uriTemplateVariablePathSegment = this.segments[i] as UriTemplateVariablePathSegment;
					this.variables.LookupDefault(uriTemplateVariablePathSegment.VarName, boundParameters);
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000394C File Offset: 0x00001B4C
		private bool IsCandidatePathMatch(int numSegmentsInBaseAddress, string[] candidateSegments, out int numMatchedSegments, out Collection<string> relativeSegments)
		{
			int num = candidateSegments.Length - numSegmentsInBaseAddress;
			relativeSegments = new Collection<string>();
			bool flag = true;
			int i = 0;
			while (flag && i < num)
			{
				string text = candidateSegments[i + numSegmentsInBaseAddress];
				if (i < this.segments.Count)
				{
					bool flag2 = this.ignoreTrailingSlash && i == num - 1;
					UriTemplateLiteralPathSegment uriTemplateLiteralPathSegment = UriTemplateLiteralPathSegment.CreateFromWireData(text);
					if (!this.segments[i].IsMatch(uriTemplateLiteralPathSegment, flag2))
					{
						flag = false;
						break;
					}
					string text2 = Uri.UnescapeDataString(text);
					if (uriTemplateLiteralPathSegment.EndsWithSlash)
					{
						text2 = text2.Substring(0, text2.Length - 1);
					}
					relativeSegments.Add(text2);
					i++;
				}
				else
				{
					if (!this.HasWildcard)
					{
						flag = false;
						break;
					}
					break;
				}
			}
			if (flag)
			{
				numMatchedSegments = i;
				if (i < num)
				{
					while (i < num)
					{
						string text3 = Uri.UnescapeDataString(candidateSegments[i + numSegmentsInBaseAddress]);
						if (text3.EndsWith("/", StringComparison.Ordinal))
						{
							text3 = text3.Substring(0, text3.Length - 1);
						}
						relativeSegments.Add(text3);
						i++;
					}
				}
				else if (numMatchedSegments < this.firstOptionalSegment)
				{
					flag = false;
				}
			}
			else
			{
				numMatchedSegments = 0;
			}
			return flag;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003A64 File Offset: 0x00001C64
		private bool IsPathFullyEquivalent(UriTemplate other)
		{
			if (this.HasWildcard != other.HasWildcard)
			{
				return false;
			}
			if (this.segments.Count != other.segments.Count)
			{
				return false;
			}
			for (int i = 0; i < this.segments.Count; i++)
			{
				if (!this.segments[i].IsEquivalentTo(other.segments[i], i == this.segments.Count - 1 && !this.HasWildcard && (this.ignoreTrailingSlash || other.ignoreTrailingSlash)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003B00 File Offset: 0x00001D00
		private UriTemplate.BindInformation PrepareBindInformation(IDictionary<string, string> parameters, bool omitDefaults)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			IDictionary<string, string> dictionary = new Dictionary<string, string>(UriTemplateHelpers.GetQueryKeyComparer());
			foreach (KeyValuePair<string, string> item in parameters)
			{
				if (string.IsNullOrEmpty(item.Key))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("parameters", SR.GetString("UTBindByNameCalledWithEmptyKey"));
				}
				dictionary.Add(item);
			}
			UriTemplate.BindInformation result;
			this.ProcessDefaultsAndCreateBindInfo(omitDefaults, dictionary, out result);
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003B9C File Offset: 0x00001D9C
		private UriTemplate.BindInformation PrepareBindInformation(NameValueCollection parameters, bool omitDefaults)
		{
			if (parameters == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
			}
			IDictionary<string, string> dictionary = new Dictionary<string, string>(UriTemplateHelpers.GetQueryKeyComparer());
			foreach (string text in parameters.AllKeys)
			{
				if (string.IsNullOrEmpty(text))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("parameters", SR.GetString("UTBindByNameCalledWithEmptyKey"));
				}
				dictionary.Add(text, parameters[text]);
			}
			UriTemplate.BindInformation result;
			this.ProcessDefaultsAndCreateBindInfo(omitDefaults, dictionary, out result);
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003C20 File Offset: 0x00001E20
		private void ProcessDefaultsAndCreateBindInfo(bool omitDefaults, IDictionary<string, string> extraParameters, out UriTemplate.BindInformation bindInfo)
		{
			if (this.additionalDefaults != null)
			{
				if (omitDefaults)
				{
					using (Dictionary<string, string>.Enumerator enumerator = this.additionalDefaults.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, string> keyValuePair = enumerator.Current;
							string strA;
							if (extraParameters.TryGetValue(keyValuePair.Key, out strA) && string.Compare(strA, keyValuePair.Value, StringComparison.Ordinal) == 0)
							{
								extraParameters.Remove(keyValuePair.Key);
							}
						}
						goto IL_BF;
					}
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in this.additionalDefaults)
				{
					if (!extraParameters.ContainsKey(keyValuePair2.Key))
					{
						extraParameters.Add(keyValuePair2.Key, keyValuePair2.Value);
					}
				}
			}
			IL_BF:
			if (extraParameters.Count == 0)
			{
				extraParameters = null;
			}
			bindInfo = new UriTemplate.BindInformation(extraParameters);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003D20 File Offset: 0x00001F20
		private string UnescapeDefaultValue(string escapedValue)
		{
			if (string.IsNullOrEmpty(escapedValue))
			{
				return escapedValue;
			}
			if (this.unescapedDefaults == null)
			{
				this.unescapedDefaults = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
			}
			return this.unescapedDefaults.GetOrAdd(escapedValue, new Func<string, string>(Uri.UnescapeDataString));
		}

		// Token: 0x04000056 RID: 86
		internal readonly int firstOptionalSegment;

		// Token: 0x04000057 RID: 87
		internal readonly string originalTemplate;

		// Token: 0x04000058 RID: 88
		internal readonly Dictionary<string, UriTemplateQueryValue> queries;

		// Token: 0x04000059 RID: 89
		internal readonly List<UriTemplatePathSegment> segments;

		// Token: 0x0400005A RID: 90
		internal const string WildcardPath = "*";

		// Token: 0x0400005B RID: 91
		private readonly Dictionary<string, string> additionalDefaults;

		// Token: 0x0400005C RID: 92
		private readonly string fragment;

		// Token: 0x0400005D RID: 93
		private readonly bool ignoreTrailingSlash;

		// Token: 0x0400005E RID: 94
		private const string NullableDefault = "null";

		// Token: 0x0400005F RID: 95
		private readonly UriTemplate.WildcardInfo wildcard;

		// Token: 0x04000060 RID: 96
		private IDictionary<string, string> defaults;

		// Token: 0x04000061 RID: 97
		private ConcurrentDictionary<string, string> unescapedDefaults;

		// Token: 0x04000062 RID: 98
		private UriTemplate.VariablesCollection variables;

		// Token: 0x02000AB3 RID: 2739
		private struct BindInformation
		{
			// Token: 0x06006DCB RID: 28107 RVA: 0x0019A0F7 File Offset: 0x001982F7
			public BindInformation(string[] normalizedParameters, int lastNonDefaultPathParameter, int lastNonNullablePathParameter, IDictionary<string, string> additionalParameters)
			{
				this.normalizedParameters = normalizedParameters;
				this.lastNonDefaultPathParameter = lastNonDefaultPathParameter;
				this.lastNonNullablePathParameter = lastNonNullablePathParameter;
				this.additionalParameters = additionalParameters;
			}

			// Token: 0x06006DCC RID: 28108 RVA: 0x0019A116 File Offset: 0x00198316
			public BindInformation(IDictionary<string, string> additionalParameters)
			{
				this.normalizedParameters = null;
				this.lastNonDefaultPathParameter = -1;
				this.lastNonNullablePathParameter = -1;
				this.additionalParameters = additionalParameters;
			}

			// Token: 0x1700199C RID: 6556
			// (get) Token: 0x06006DCD RID: 28109 RVA: 0x0019A134 File Offset: 0x00198334
			public IDictionary<string, string> AdditionalParameters
			{
				get
				{
					return this.additionalParameters;
				}
			}

			// Token: 0x1700199D RID: 6557
			// (get) Token: 0x06006DCE RID: 28110 RVA: 0x0019A13C File Offset: 0x0019833C
			public int LastNonDefaultPathParameter
			{
				get
				{
					return this.lastNonDefaultPathParameter;
				}
			}

			// Token: 0x1700199E RID: 6558
			// (get) Token: 0x06006DCF RID: 28111 RVA: 0x0019A144 File Offset: 0x00198344
			public int LastNonNullablePathParameter
			{
				get
				{
					return this.lastNonNullablePathParameter;
				}
			}

			// Token: 0x1700199F RID: 6559
			// (get) Token: 0x06006DD0 RID: 28112 RVA: 0x0019A14C File Offset: 0x0019834C
			public string[] NormalizedParameters
			{
				get
				{
					return this.normalizedParameters;
				}
			}

			// Token: 0x04003ED3 RID: 16083
			private IDictionary<string, string> additionalParameters;

			// Token: 0x04003ED4 RID: 16084
			private int lastNonDefaultPathParameter;

			// Token: 0x04003ED5 RID: 16085
			private int lastNonNullablePathParameter;

			// Token: 0x04003ED6 RID: 16086
			private string[] normalizedParameters;
		}

		// Token: 0x02000AB4 RID: 2740
		private class UriTemplateDefaults : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
		{
			// Token: 0x06006DD1 RID: 28113 RVA: 0x0019A154 File Offset: 0x00198354
			public UriTemplateDefaults(UriTemplate template)
			{
				this.defaults = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				if (template.variables != null && template.variables.DefaultValues != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in template.variables.DefaultValues)
					{
						this.defaults.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
				if (template.additionalDefaults != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair2 in template.additionalDefaults)
					{
						this.defaults.Add(keyValuePair2.Key.ToUpperInvariant(), keyValuePair2.Value);
					}
				}
				this.keys = new ReadOnlyCollection<string>(new List<string>(this.defaults.Keys));
				this.values = new ReadOnlyCollection<string>(new List<string>(this.defaults.Values));
			}

			// Token: 0x170019A0 RID: 6560
			// (get) Token: 0x06006DD2 RID: 28114 RVA: 0x0019A280 File Offset: 0x00198480
			public int Count
			{
				get
				{
					return this.defaults.Count;
				}
			}

			// Token: 0x170019A1 RID: 6561
			// (get) Token: 0x06006DD3 RID: 28115 RVA: 0x0019A28D File Offset: 0x0019848D
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170019A2 RID: 6562
			// (get) Token: 0x06006DD4 RID: 28116 RVA: 0x0019A290 File Offset: 0x00198490
			public ICollection<string> Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x170019A3 RID: 6563
			// (get) Token: 0x06006DD5 RID: 28117 RVA: 0x0019A298 File Offset: 0x00198498
			public ICollection<string> Values
			{
				get
				{
					return this.values;
				}
			}

			// Token: 0x170019A4 RID: 6564
			public string this[string key]
			{
				get
				{
					return this.defaults[key];
				}
				set
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UTDefaultValuesAreImmutable")));
				}
			}

			// Token: 0x06006DD8 RID: 28120 RVA: 0x0019A2C9 File Offset: 0x001984C9
			public void Add(string key, string value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UTDefaultValuesAreImmutable")));
			}

			// Token: 0x06006DD9 RID: 28121 RVA: 0x0019A2E4 File Offset: 0x001984E4
			public void Add(KeyValuePair<string, string> item)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UTDefaultValuesAreImmutable")));
			}

			// Token: 0x06006DDA RID: 28122 RVA: 0x0019A2FF File Offset: 0x001984FF
			public void Clear()
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UTDefaultValuesAreImmutable")));
			}

			// Token: 0x06006DDB RID: 28123 RVA: 0x0019A31A File Offset: 0x0019851A
			public bool Contains(KeyValuePair<string, string> item)
			{
				return ((ICollection<KeyValuePair<string, string>>)this.defaults).Contains(item);
			}

			// Token: 0x06006DDC RID: 28124 RVA: 0x0019A328 File Offset: 0x00198528
			public bool ContainsKey(string key)
			{
				return this.defaults.ContainsKey(key);
			}

			// Token: 0x06006DDD RID: 28125 RVA: 0x0019A336 File Offset: 0x00198536
			public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
			{
				((ICollection<KeyValuePair<string, string>>)this.defaults).CopyTo(array, arrayIndex);
			}

			// Token: 0x06006DDE RID: 28126 RVA: 0x0019A345 File Offset: 0x00198545
			public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
			{
				return this.defaults.GetEnumerator();
			}

			// Token: 0x06006DDF RID: 28127 RVA: 0x0019A357 File Offset: 0x00198557
			public bool Remove(string key)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UTDefaultValuesAreImmutable")));
			}

			// Token: 0x06006DE0 RID: 28128 RVA: 0x0019A372 File Offset: 0x00198572
			public bool Remove(KeyValuePair<string, string> item)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UTDefaultValuesAreImmutable")));
			}

			// Token: 0x06006DE1 RID: 28129 RVA: 0x0019A38D File Offset: 0x0019858D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.defaults.GetEnumerator();
			}

			// Token: 0x06006DE2 RID: 28130 RVA: 0x0019A39F File Offset: 0x0019859F
			public bool TryGetValue(string key, out string value)
			{
				return this.defaults.TryGetValue(key, out value);
			}

			// Token: 0x04003ED7 RID: 16087
			private Dictionary<string, string> defaults;

			// Token: 0x04003ED8 RID: 16088
			private ReadOnlyCollection<string> keys;

			// Token: 0x04003ED9 RID: 16089
			private ReadOnlyCollection<string> values;
		}

		// Token: 0x02000AB5 RID: 2741
		private class VariablesCollection
		{
			// Token: 0x06006DE3 RID: 28131 RVA: 0x0019A3AE File Offset: 0x001985AE
			public VariablesCollection(UriTemplate owner)
			{
				this.owner = owner;
				this.pathSegmentVariableNames = new List<string>();
				this.pathSegmentVariableNature = new List<UriTemplatePartType>();
				this.queryValueVariableNames = new List<string>();
				this.firstNullablePathVariable = -1;
			}

			// Token: 0x170019A5 RID: 6565
			// (get) Token: 0x06006DE4 RID: 28132 RVA: 0x0019A3E5 File Offset: 0x001985E5
			public static ReadOnlyCollection<string> EmptyCollection
			{
				get
				{
					if (UriTemplate.VariablesCollection.emptyStringCollection == null)
					{
						UriTemplate.VariablesCollection.emptyStringCollection = new ReadOnlyCollection<string>(new List<string>());
					}
					return UriTemplate.VariablesCollection.emptyStringCollection;
				}
			}

			// Token: 0x170019A6 RID: 6566
			// (get) Token: 0x06006DE5 RID: 28133 RVA: 0x0019A402 File Offset: 0x00198602
			public Dictionary<string, string> DefaultValues
			{
				get
				{
					return this.defaultValues;
				}
			}

			// Token: 0x170019A7 RID: 6567
			// (get) Token: 0x06006DE6 RID: 28134 RVA: 0x0019A40A File Offset: 0x0019860A
			public ReadOnlyCollection<string> PathSegmentVariableNames
			{
				get
				{
					if (this.pathSegmentVariableNamesSnapshot == null)
					{
						Interlocked.CompareExchange<ReadOnlyCollection<string>>(ref this.pathSegmentVariableNamesSnapshot, new ReadOnlyCollection<string>(this.pathSegmentVariableNames), null);
					}
					return this.pathSegmentVariableNamesSnapshot;
				}
			}

			// Token: 0x170019A8 RID: 6568
			// (get) Token: 0x06006DE7 RID: 28135 RVA: 0x0019A432 File Offset: 0x00198632
			public ReadOnlyCollection<string> QueryValueVariableNames
			{
				get
				{
					if (this.queryValueVariableNamesSnapshot == null)
					{
						Interlocked.CompareExchange<ReadOnlyCollection<string>>(ref this.queryValueVariableNamesSnapshot, new ReadOnlyCollection<string>(this.queryValueVariableNames), null);
					}
					return this.queryValueVariableNamesSnapshot;
				}
			}

			// Token: 0x06006DE8 RID: 28136 RVA: 0x0019A45C File Offset: 0x0019865C
			public void AddDefaultValue(string varName, string value)
			{
				int num = this.pathSegmentVariableNames.IndexOf(varName);
				if (this.owner.wildcard != null && this.owner.wildcard.HasVariable && num == this.pathSegmentVariableNames.Count - 1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTStarVariableWithDefaultsFromAdditionalDefaults", new object[]
					{
						this.owner.originalTemplate,
						varName
					})));
				}
				if (this.pathSegmentVariableNature[num] != UriTemplatePartType.Variable)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTDefaultValueToCompoundSegmentVarFromAdditionalDefaults", new object[]
					{
						this.owner.originalTemplate,
						varName
					})));
				}
				if (string.IsNullOrEmpty(value) || string.Compare(value, "null", StringComparison.OrdinalIgnoreCase) == 0)
				{
					value = null;
				}
				if (this.defaultValues == null)
				{
					this.defaultValues = new Dictionary<string, string>();
				}
				this.defaultValues.Add(varName, value);
			}

			// Token: 0x06006DE9 RID: 28137 RVA: 0x0019A554 File Offset: 0x00198754
			public string AddPathVariable(UriTemplatePartType sourceNature, string varDeclaration, out bool hasDefaultValue)
			{
				string text;
				string text2;
				this.ParseVariableDeclaration(varDeclaration, out text, out text2);
				hasDefaultValue = (text2 != null);
				if (text.IndexOf("*", StringComparison.Ordinal) != -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidWildcardInVariableOrLiteral", new object[]
					{
						this.owner.originalTemplate,
						"*"
					})));
				}
				string text3 = text.ToUpperInvariant();
				if (this.pathSegmentVariableNames.Contains(text3) || this.queryValueVariableNames.Contains(text3))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTVarNamesMustBeUnique", new object[]
					{
						this.owner.originalTemplate,
						text
					})));
				}
				this.pathSegmentVariableNames.Add(text3);
				this.pathSegmentVariableNature.Add(sourceNature);
				if (hasDefaultValue)
				{
					if (text2 == string.Empty)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTInvalidDefaultPathValue", new object[]
						{
							this.owner.originalTemplate,
							varDeclaration,
							text
						})));
					}
					if (string.Compare(text2, "null", StringComparison.OrdinalIgnoreCase) == 0)
					{
						text2 = null;
					}
					if (this.defaultValues == null)
					{
						this.defaultValues = new Dictionary<string, string>();
					}
					this.defaultValues.Add(text3, text2);
				}
				return text3;
			}

			// Token: 0x06006DEA RID: 28138 RVA: 0x0019A69C File Offset: 0x0019889C
			public string AddQueryVariable(string varDeclaration)
			{
				string text;
				string text2;
				this.ParseVariableDeclaration(varDeclaration, out text, out text2);
				if (text.IndexOf("*", StringComparison.Ordinal) != -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidWildcardInVariableOrLiteral", new object[]
					{
						this.owner.originalTemplate,
						"*"
					})));
				}
				if (text2 != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTDefaultValueToQueryVar", new object[]
					{
						this.owner.originalTemplate,
						varDeclaration,
						text
					})));
				}
				string text3 = text.ToUpperInvariant();
				if (this.pathSegmentVariableNames.Contains(text3) || this.queryValueVariableNames.Contains(text3))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTVarNamesMustBeUnique", new object[]
					{
						this.owner.originalTemplate,
						text
					})));
				}
				this.queryValueVariableNames.Add(text3);
				return text3;
			}

			// Token: 0x06006DEB RID: 28139 RVA: 0x0019A794 File Offset: 0x00198994
			public void LookupDefault(string varName, NameValueCollection boundParameters)
			{
				boundParameters.Add(varName, this.owner.UnescapeDefaultValue(this.defaultValues[varName]));
			}

			// Token: 0x06006DEC RID: 28140 RVA: 0x0019A7B4 File Offset: 0x001989B4
			public UriTemplate.BindInformation PrepareBindInformation(IDictionary<string, string> parameters, bool omitDefaults)
			{
				if (parameters == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
				}
				string[] normalizedParameters = this.PrepareNormalizedParameters();
				IDictionary<string, string> extraParameters = null;
				foreach (string text in parameters.Keys)
				{
					this.ProcessBindParameter(text, parameters[text], normalizedParameters, ref extraParameters);
				}
				UriTemplate.BindInformation result;
				this.ProcessDefaultsAndCreateBindInfo(omitDefaults, normalizedParameters, extraParameters, out result);
				return result;
			}

			// Token: 0x06006DED RID: 28141 RVA: 0x0019A838 File Offset: 0x00198A38
			public UriTemplate.BindInformation PrepareBindInformation(NameValueCollection parameters, bool omitDefaults)
			{
				if (parameters == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("parameters");
				}
				string[] normalizedParameters = this.PrepareNormalizedParameters();
				IDictionary<string, string> extraParameters = null;
				foreach (string name in parameters.AllKeys)
				{
					this.ProcessBindParameter(name, parameters[name], normalizedParameters, ref extraParameters);
				}
				UriTemplate.BindInformation result;
				this.ProcessDefaultsAndCreateBindInfo(omitDefaults, normalizedParameters, extraParameters, out result);
				return result;
			}

			// Token: 0x06006DEE RID: 28142 RVA: 0x0019A8A0 File Offset: 0x00198AA0
			public UriTemplate.BindInformation PrepareBindInformation(params string[] parameters)
			{
				if (parameters == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("values");
				}
				if (parameters.Length < this.pathSegmentVariableNames.Count || parameters.Length > this.pathSegmentVariableNames.Count + this.queryValueVariableNames.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTBindByPositionWrongCount", new object[]
					{
						this.owner.originalTemplate,
						this.pathSegmentVariableNames.Count,
						this.queryValueVariableNames.Count,
						parameters.Length
					})));
				}
				string[] array;
				if (parameters.Length == this.pathSegmentVariableNames.Count + this.queryValueVariableNames.Count)
				{
					array = parameters;
				}
				else
				{
					array = new string[this.pathSegmentVariableNames.Count + this.queryValueVariableNames.Count];
					parameters.CopyTo(array, 0);
					for (int i = parameters.Length; i < array.Length; i++)
					{
						array[i] = null;
					}
				}
				int lastNonDefaultPathParameter;
				int lastNonNullablePathParameter;
				this.LoadDefaultsAndValidate(array, out lastNonDefaultPathParameter, out lastNonNullablePathParameter);
				return new UriTemplate.BindInformation(array, lastNonDefaultPathParameter, lastNonNullablePathParameter, this.owner.additionalDefaults);
			}

			// Token: 0x06006DEF RID: 28143 RVA: 0x0019A9C4 File Offset: 0x00198BC4
			public void ValidateDefaults(out int firstOptionalSegment)
			{
				int num = this.pathSegmentVariableNames.Count - 1;
				while (num >= 0 && this.firstNullablePathVariable == -1)
				{
					string key = this.pathSegmentVariableNames[num];
					string text;
					if (!this.defaultValues.TryGetValue(key, out text))
					{
						this.firstNullablePathVariable = num + 1;
					}
					else if (text != null)
					{
						this.firstNullablePathVariable = num + 1;
					}
					num--;
				}
				if (this.firstNullablePathVariable == -1)
				{
					this.firstNullablePathVariable = 0;
				}
				if (this.firstNullablePathVariable > 1)
				{
					for (int i = this.firstNullablePathVariable - 2; i >= 0; i--)
					{
						string text2 = this.pathSegmentVariableNames[i];
						string text3;
						if (this.defaultValues.TryGetValue(text2, out text3) && text3 == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTNullableDefaultMustBeFollowedWithNullables", new object[]
							{
								this.owner.originalTemplate,
								text2,
								this.pathSegmentVariableNames[i + 1]
							})));
						}
					}
				}
				if (this.firstNullablePathVariable < this.pathSegmentVariableNames.Count)
				{
					if (this.owner.HasWildcard)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTNullableDefaultMustNotBeFollowedWithWildcard", new object[]
						{
							this.owner.originalTemplate,
							this.pathSegmentVariableNames[this.firstNullablePathVariable]
						})));
					}
					for (int j = this.pathSegmentVariableNames.Count - 1; j >= this.firstNullablePathVariable; j--)
					{
						int index = this.owner.segments.Count - (this.pathSegmentVariableNames.Count - j);
						if (this.owner.segments[index].Nature != UriTemplatePartType.Variable)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTNullableDefaultMustNotBeFollowedWithLiteral", new object[]
							{
								this.owner.originalTemplate,
								this.pathSegmentVariableNames[this.firstNullablePathVariable],
								this.owner.segments[index].OriginalSegment
							})));
						}
					}
				}
				int num2 = this.pathSegmentVariableNames.Count - this.firstNullablePathVariable;
				firstOptionalSegment = this.owner.segments.Count - num2;
				if (!this.owner.HasWildcard)
				{
					while (firstOptionalSegment > 0)
					{
						UriTemplatePathSegment uriTemplatePathSegment = this.owner.segments[firstOptionalSegment - 1];
						if (uriTemplatePathSegment.Nature != UriTemplatePartType.Variable)
						{
							break;
						}
						UriTemplateVariablePathSegment uriTemplateVariablePathSegment = uriTemplatePathSegment as UriTemplateVariablePathSegment;
						if (!this.defaultValues.ContainsKey(uriTemplateVariablePathSegment.VarName))
						{
							break;
						}
						firstOptionalSegment--;
					}
				}
			}

			// Token: 0x06006DF0 RID: 28144 RVA: 0x0019AC64 File Offset: 0x00198E64
			private void AddAdditionalDefaults(ref IDictionary<string, string> extraParameters)
			{
				if (extraParameters == null)
				{
					extraParameters = this.owner.additionalDefaults;
					return;
				}
				foreach (KeyValuePair<string, string> keyValuePair in this.owner.additionalDefaults)
				{
					if (!extraParameters.ContainsKey(keyValuePair.Key))
					{
						extraParameters.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}

			// Token: 0x06006DF1 RID: 28145 RVA: 0x0019ACEC File Offset: 0x00198EEC
			private void LoadDefaultsAndValidate(string[] normalizedParameters, out int lastNonDefaultPathParameter, out int lastNonNullablePathParameter)
			{
				for (int i = 0; i < this.pathSegmentVariableNames.Count; i++)
				{
					if (string.IsNullOrEmpty(normalizedParameters[i]) && this.defaultValues != null)
					{
						this.defaultValues.TryGetValue(this.pathSegmentVariableNames[i], out normalizedParameters[i]);
					}
				}
				lastNonDefaultPathParameter = this.pathSegmentVariableNames.Count - 1;
				if (this.defaultValues != null && this.owner.segments[this.owner.segments.Count - 1].Nature != UriTemplatePartType.Literal)
				{
					bool flag = false;
					while (!flag && lastNonDefaultPathParameter >= 0)
					{
						string strB;
						if (this.defaultValues.TryGetValue(this.pathSegmentVariableNames[lastNonDefaultPathParameter], out strB))
						{
							if (string.Compare(normalizedParameters[lastNonDefaultPathParameter], strB, StringComparison.Ordinal) != 0)
							{
								flag = true;
							}
							else
							{
								lastNonDefaultPathParameter--;
							}
						}
						else
						{
							flag = true;
						}
					}
				}
				if (this.firstNullablePathVariable > lastNonDefaultPathParameter)
				{
					lastNonNullablePathParameter = this.firstNullablePathVariable - 1;
				}
				else
				{
					lastNonNullablePathParameter = lastNonDefaultPathParameter;
				}
				for (int j = 0; j <= lastNonNullablePathParameter; j++)
				{
					if ((!this.owner.HasWildcard || !this.owner.wildcard.HasVariable || j != this.pathSegmentVariableNames.Count - 1) && string.IsNullOrEmpty(normalizedParameters[j]))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("parameters", SR.GetString("BindUriTemplateToNullOrEmptyPathParam", new object[]
						{
							this.pathSegmentVariableNames[j]
						}));
					}
				}
			}

			// Token: 0x06006DF2 RID: 28146 RVA: 0x0019AE54 File Offset: 0x00199054
			private void ParseVariableDeclaration(string varDeclaration, out string varName, out string defaultValue)
			{
				if (varDeclaration.IndexOf('{') != -1 || varDeclaration.IndexOf('}') != -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidVarDeclaration", new object[]
					{
						this.owner.originalTemplate,
						varDeclaration
					})));
				}
				int num = varDeclaration.IndexOf('=');
				if (num == -1)
				{
					varName = varDeclaration;
					defaultValue = null;
					return;
				}
				if (num == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidVarDeclaration", new object[]
					{
						this.owner.originalTemplate,
						varDeclaration
					})));
				}
				varName = varDeclaration.Substring(0, num);
				defaultValue = varDeclaration.Substring(num + 1);
				if (defaultValue.IndexOf('=') != -1)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("UTInvalidVarDeclaration", new object[]
					{
						this.owner.originalTemplate,
						varDeclaration
					})));
				}
			}

			// Token: 0x06006DF3 RID: 28147 RVA: 0x0019AF48 File Offset: 0x00199148
			private string[] PrepareNormalizedParameters()
			{
				string[] array = new string[this.pathSegmentVariableNames.Count + this.queryValueVariableNames.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = null;
				}
				return array;
			}

			// Token: 0x06006DF4 RID: 28148 RVA: 0x0019AF88 File Offset: 0x00199188
			private void ProcessBindParameter(string name, string value, string[] normalizedParameters, ref IDictionary<string, string> extraParameters)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("parameters", SR.GetString("UTBindByNameCalledWithEmptyKey"));
				}
				string item = name.ToUpperInvariant();
				int num = this.pathSegmentVariableNames.IndexOf(item);
				if (num != -1)
				{
					normalizedParameters[num] = (string.IsNullOrEmpty(value) ? string.Empty : value);
					return;
				}
				int num2 = this.queryValueVariableNames.IndexOf(item);
				if (num2 != -1)
				{
					normalizedParameters[this.pathSegmentVariableNames.Count + num2] = (string.IsNullOrEmpty(value) ? string.Empty : value);
					return;
				}
				if (extraParameters == null)
				{
					extraParameters = new Dictionary<string, string>(UriTemplateHelpers.GetQueryKeyComparer());
				}
				extraParameters.Add(name, value);
			}

			// Token: 0x06006DF5 RID: 28149 RVA: 0x0019B030 File Offset: 0x00199230
			private void ProcessDefaultsAndCreateBindInfo(bool omitDefaults, string[] normalizedParameters, IDictionary<string, string> extraParameters, out UriTemplate.BindInformation bindInfo)
			{
				int lastNonDefaultPathParameter;
				int lastNonNullablePathParameter;
				this.LoadDefaultsAndValidate(normalizedParameters, out lastNonDefaultPathParameter, out lastNonNullablePathParameter);
				if (this.owner.additionalDefaults != null)
				{
					if (omitDefaults)
					{
						this.RemoveAdditionalDefaults(ref extraParameters);
					}
					else
					{
						this.AddAdditionalDefaults(ref extraParameters);
					}
				}
				bindInfo = new UriTemplate.BindInformation(normalizedParameters, lastNonDefaultPathParameter, lastNonNullablePathParameter, extraParameters);
			}

			// Token: 0x06006DF6 RID: 28150 RVA: 0x0019B07C File Offset: 0x0019927C
			private void RemoveAdditionalDefaults(ref IDictionary<string, string> extraParameters)
			{
				if (extraParameters == null)
				{
					return;
				}
				foreach (KeyValuePair<string, string> keyValuePair in this.owner.additionalDefaults)
				{
					string strA;
					if (extraParameters.TryGetValue(keyValuePair.Key, out strA) && string.Compare(strA, keyValuePair.Value, StringComparison.Ordinal) == 0)
					{
						extraParameters.Remove(keyValuePair.Key);
					}
				}
				if (extraParameters.Count == 0)
				{
					extraParameters = null;
				}
			}

			// Token: 0x04003EDA RID: 16090
			private readonly UriTemplate owner;

			// Token: 0x04003EDB RID: 16091
			private static ReadOnlyCollection<string> emptyStringCollection;

			// Token: 0x04003EDC RID: 16092
			private Dictionary<string, string> defaultValues;

			// Token: 0x04003EDD RID: 16093
			private int firstNullablePathVariable;

			// Token: 0x04003EDE RID: 16094
			private List<string> pathSegmentVariableNames;

			// Token: 0x04003EDF RID: 16095
			private ReadOnlyCollection<string> pathSegmentVariableNamesSnapshot;

			// Token: 0x04003EE0 RID: 16096
			private List<UriTemplatePartType> pathSegmentVariableNature;

			// Token: 0x04003EE1 RID: 16097
			private List<string> queryValueVariableNames;

			// Token: 0x04003EE2 RID: 16098
			private ReadOnlyCollection<string> queryValueVariableNamesSnapshot;
		}

		// Token: 0x02000AB6 RID: 2742
		private class WildcardInfo
		{
			// Token: 0x06006DF7 RID: 28151 RVA: 0x0019B110 File Offset: 0x00199310
			public WildcardInfo(UriTemplate owner)
			{
				this.varName = null;
				this.owner = owner;
			}

			// Token: 0x06006DF8 RID: 28152 RVA: 0x0019B128 File Offset: 0x00199328
			public WildcardInfo(UriTemplate owner, string segment)
			{
				bool flag;
				this.varName = owner.AddPathVariable(UriTemplatePartType.Variable, segment.Substring(1 + "*".Length, segment.Length - 2 - "*".Length), out flag);
				if (flag)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UTStarVariableWithDefaults", new object[]
					{
						owner.originalTemplate,
						segment,
						this.varName
					})));
				}
				this.owner = owner;
			}

			// Token: 0x170019A9 RID: 6569
			// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x0019B1AF File Offset: 0x001993AF
			internal bool HasVariable
			{
				get
				{
					return !string.IsNullOrEmpty(this.varName);
				}
			}

			// Token: 0x06006DFA RID: 28154 RVA: 0x0019B1C0 File Offset: 0x001993C0
			public void Bind(string[] values, ref int valueIndex, StringBuilder path)
			{
				if (this.HasVariable)
				{
					if (string.IsNullOrEmpty(values[valueIndex]))
					{
						valueIndex++;
						return;
					}
					int num = valueIndex;
					valueIndex = num + 1;
					path.Append(values[num]);
				}
			}

			// Token: 0x06006DFB RID: 28155 RVA: 0x0019B1FC File Offset: 0x001993FC
			public void Lookup(int numMatchedSegments, Collection<string> relativePathSegments, NameValueCollection boundParameters)
			{
				if (this.HasVariable)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = numMatchedSegments; i < relativePathSegments.Count; i++)
					{
						if (i < relativePathSegments.Count - 1)
						{
							stringBuilder.AppendFormat("{0}/", relativePathSegments[i]);
						}
						else
						{
							stringBuilder.Append(relativePathSegments[i]);
						}
					}
					boundParameters.Add(this.varName, stringBuilder.ToString());
				}
			}

			// Token: 0x04003EE3 RID: 16099
			private readonly UriTemplate owner;

			// Token: 0x04003EE4 RID: 16100
			private readonly string varName;
		}
	}
}
