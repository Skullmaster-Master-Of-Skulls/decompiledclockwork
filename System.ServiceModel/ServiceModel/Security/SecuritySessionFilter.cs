using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200031C RID: 796
	internal sealed class SecuritySessionFilter : HeaderFilter
	{
		// Token: 0x06001B95 RID: 7061 RVA: 0x00067528 File Offset: 0x00065728
		public SecuritySessionFilter(UniqueId securityContextTokenId, SecurityStandardsManager standardsManager, bool isStrictMode, params string[] excludedActions)
		{
			if (securityContextTokenId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("securityContextTokenId"));
			}
			this.excludedActions = excludedActions;
			this.securityContextTokenId = securityContextTokenId;
			this.standardsManager = standardsManager;
			this.isStrictMode = isStrictMode;
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x00067576 File Offset: 0x00065776
		public UniqueId SecurityContextTokenId
		{
			get
			{
				return this.securityContextTokenId;
			}
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00067580 File Offset: 0x00065780
		private static bool ShouldExcludeMessage(Message message, string[] excludedActions)
		{
			string action = message.Headers.Action;
			if (excludedActions == null || action == null)
			{
				return false;
			}
			for (int i = 0; i < excludedActions.Length; i++)
			{
				if (string.Equals(action, excludedActions[i], StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000675BE File Offset: 0x000657BE
		internal static bool CanHandleException(Exception e)
		{
			return e is XmlException || e is FormatException || e is SecurityTokenException || e is MessageSecurityException || e is ProtocolException || e is InvalidOperationException || e is ArgumentException;
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000675FC File Offset: 0x000657FC
		public override bool Match(Message message)
		{
			if (SecuritySessionFilter.ShouldExcludeMessage(message, this.excludedActions))
			{
				return false;
			}
			object obj;
			List<UniqueId> list;
			if (!message.Properties.TryGetValue(SecuritySessionFilter.SessionContextIdsProperty, out obj))
			{
				list = new List<UniqueId>(1);
				try
				{
					if (!this.standardsManager.TryGetSecurityContextIds(message, message.Version.Envelope.UltimateDestinationActorValues, this.isStrictMode, list))
					{
						return false;
					}
				}
				catch (Exception e)
				{
					if (!SecuritySessionFilter.CanHandleException(e))
					{
						throw;
					}
					return false;
				}
				message.Properties.Add(SecuritySessionFilter.SessionContextIdsProperty, list);
			}
			else
			{
				list = (obj as List<UniqueId>);
				if (list == null)
				{
					return false;
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] == this.securityContextTokenId)
				{
					message.Properties.Remove(SecuritySessionFilter.SessionContextIdsProperty);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000676E0 File Offset: 0x000658E0
		public override bool Match(MessageBuffer buffer)
		{
			bool result;
			using (Message message = buffer.CreateMessage())
			{
				result = this.Match(message);
			}
			return result;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0006771C File Offset: 0x0006591C
		protected internal override IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
		{
			return new SecuritySessionFilter.SecuritySessionFilterTable<FilterData>(this.standardsManager, this.isStrictMode, this.excludedActions);
		}

		// Token: 0x04001D8B RID: 7563
		private static readonly string SessionContextIdsProperty = string.Format(CultureInfo.InvariantCulture, "{0}/SecuritySessionContextIds", new object[]
		{
			"http://schemas.microsoft.com/ws/2006/05/security"
		});

		// Token: 0x04001D8C RID: 7564
		private UniqueId securityContextTokenId;

		// Token: 0x04001D8D RID: 7565
		private SecurityStandardsManager standardsManager;

		// Token: 0x04001D8E RID: 7566
		private string[] excludedActions;

		// Token: 0x04001D8F RID: 7567
		private bool isStrictMode;

		// Token: 0x02000B6F RID: 2927
		private class SecuritySessionFilterTable<FilterData> : IMessageFilterTable<FilterData>, IDictionary<MessageFilter, FilterData>, ICollection<KeyValuePair<MessageFilter, !0>>, IEnumerable<KeyValuePair<MessageFilter, !0>>, IEnumerable
		{
			// Token: 0x0600726B RID: 29291 RVA: 0x001AB33C File Offset: 0x001A953C
			public SecuritySessionFilterTable(SecurityStandardsManager standardsManager, bool isStrictMode, string[] excludedActions)
			{
				if (standardsManager == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("standardsManager");
				}
				if (excludedActions == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("excludedActions");
				}
				this.standardsManager = standardsManager;
				this.excludedActions = new string[excludedActions.Length];
				excludedActions.CopyTo(this.excludedActions, 0);
				this.isStrictMode = isStrictMode;
				this.contextMappings = new Dictionary<UniqueId, KeyValuePair<MessageFilter, FilterData>>();
				this.filterMappings = new Dictionary<MessageFilter, FilterData>();
			}

			// Token: 0x17001A8D RID: 6797
			// (get) Token: 0x0600726C RID: 29292 RVA: 0x001AB3B4 File Offset: 0x001A95B4
			public ICollection<MessageFilter> Keys
			{
				get
				{
					return this.filterMappings.Keys;
				}
			}

			// Token: 0x17001A8E RID: 6798
			// (get) Token: 0x0600726D RID: 29293 RVA: 0x001AB3C1 File Offset: 0x001A95C1
			public ICollection<FilterData> Values
			{
				get
				{
					return this.filterMappings.Values;
				}
			}

			// Token: 0x17001A8F RID: 6799
			public FilterData this[MessageFilter filter]
			{
				get
				{
					return this.filterMappings[filter];
				}
				set
				{
					if (this.filterMappings.ContainsKey(filter))
					{
						this.Remove(filter);
					}
					this.Add(filter, value);
				}
			}

			// Token: 0x17001A90 RID: 6800
			// (get) Token: 0x06007270 RID: 29296 RVA: 0x001AB3FC File Offset: 0x001A95FC
			public int Count
			{
				get
				{
					return this.filterMappings.Count;
				}
			}

			// Token: 0x17001A91 RID: 6801
			// (get) Token: 0x06007271 RID: 29297 RVA: 0x001AB409 File Offset: 0x001A9609
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007272 RID: 29298 RVA: 0x001AB40C File Offset: 0x001A960C
			public void Add(KeyValuePair<MessageFilter, FilterData> item)
			{
				this.Add(item.Key, item.Value);
			}

			// Token: 0x06007273 RID: 29299 RVA: 0x001AB422 File Offset: 0x001A9622
			public void Clear()
			{
				this.filterMappings.Clear();
				this.contextMappings.Clear();
			}

			// Token: 0x06007274 RID: 29300 RVA: 0x001AB43A File Offset: 0x001A963A
			public bool Contains(KeyValuePair<MessageFilter, FilterData> item)
			{
				return this.ContainsKey(item.Key);
			}

			// Token: 0x06007275 RID: 29301 RVA: 0x001AB44C File Offset: 0x001A964C
			public void CopyTo(KeyValuePair<MessageFilter, FilterData>[] array, int arrayIndex)
			{
				int num = arrayIndex;
				foreach (KeyValuePair<MessageFilter, FilterData> keyValuePair in this.contextMappings.Values)
				{
					array[num] = keyValuePair;
					num++;
				}
			}

			// Token: 0x06007276 RID: 29302 RVA: 0x001AB4AC File Offset: 0x001A96AC
			public bool Remove(KeyValuePair<MessageFilter, FilterData> item)
			{
				return this.Remove(item.Key);
			}

			// Token: 0x06007277 RID: 29303 RVA: 0x001AB4BB File Offset: 0x001A96BB
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06007278 RID: 29304 RVA: 0x001AB4C3 File Offset: 0x001A96C3
			public IEnumerator<KeyValuePair<MessageFilter, FilterData>> GetEnumerator()
			{
				return ((IEnumerable<KeyValuePair<MessageFilter, FilterData>>)this.contextMappings.Values).GetEnumerator();
			}

			// Token: 0x06007279 RID: 29305 RVA: 0x001AB4D8 File Offset: 0x001A96D8
			public void Add(MessageFilter filter, FilterData data)
			{
				SecuritySessionFilter securitySessionFilter = filter as SecuritySessionFilter;
				if (securitySessionFilter == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnknownFilterType", new object[]
					{
						filter.GetType()
					})));
				}
				if (securitySessionFilter.standardsManager != this.standardsManager)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("StandardsManagerDoesNotMatch")));
				}
				if (securitySessionFilter.isStrictMode != this.isStrictMode)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("FilterStrictModeDifferent")));
				}
				if (this.contextMappings.ContainsKey(securitySessionFilter.SecurityContextTokenId))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SecuritySessionIdAlreadyPresentInFilterTable", new object[]
					{
						securitySessionFilter.SecurityContextTokenId
					})));
				}
				this.filterMappings.Add(filter, data);
				this.contextMappings.Add(securitySessionFilter.SecurityContextTokenId, new KeyValuePair<MessageFilter, FilterData>(filter, data));
			}

			// Token: 0x0600727A RID: 29306 RVA: 0x001AB5C9 File Offset: 0x001A97C9
			public bool ContainsKey(MessageFilter filter)
			{
				return this.filterMappings.ContainsKey(filter);
			}

			// Token: 0x0600727B RID: 29307 RVA: 0x001AB5D8 File Offset: 0x001A97D8
			public bool Remove(MessageFilter filter)
			{
				SecuritySessionFilter securitySessionFilter = filter as SecuritySessionFilter;
				if (securitySessionFilter == null)
				{
					return false;
				}
				bool flag = this.filterMappings.Remove(filter);
				if (flag)
				{
					this.contextMappings.Remove(securitySessionFilter.SecurityContextTokenId);
				}
				return flag;
			}

			// Token: 0x0600727C RID: 29308 RVA: 0x001AB614 File Offset: 0x001A9814
			public bool TryGetValue(MessageFilter filter, out FilterData data)
			{
				return this.filterMappings.TryGetValue(filter, out data);
			}

			// Token: 0x0600727D RID: 29309 RVA: 0x001AB624 File Offset: 0x001A9824
			private bool TryGetContextIds(Message message, out List<UniqueId> contextIds)
			{
				object obj;
				if (!message.Properties.TryGetValue(SecuritySessionFilter.SessionContextIdsProperty, out obj))
				{
					contextIds = new List<UniqueId>(1);
					return this.standardsManager.TryGetSecurityContextIds(message, message.Version.Envelope.UltimateDestinationActorValues, this.isStrictMode, contextIds);
				}
				contextIds = (obj as List<UniqueId>);
				return contextIds != null;
			}

			// Token: 0x0600727E RID: 29310 RVA: 0x001AB680 File Offset: 0x001A9880
			private bool TryMatchCore(Message message, out KeyValuePair<MessageFilter, FilterData> match)
			{
				match = default(KeyValuePair<MessageFilter, FilterData>);
				if (SecuritySessionFilter.ShouldExcludeMessage(message, this.excludedActions))
				{
					return false;
				}
				List<UniqueId> list;
				try
				{
					if (!this.TryGetContextIds(message, out list))
					{
						return false;
					}
				}
				catch (Exception e)
				{
					if (!SecuritySessionFilter.CanHandleException(e))
					{
						throw;
					}
					return false;
				}
				for (int i = 0; i < list.Count; i++)
				{
					if (this.contextMappings.TryGetValue(list[i], out match))
					{
						message.Properties.Remove(SecuritySessionFilter.SessionContextIdsProperty);
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600727F RID: 29311 RVA: 0x001AB714 File Offset: 0x001A9914
			public bool GetMatchingValue(Message message, out FilterData data)
			{
				KeyValuePair<MessageFilter, FilterData> keyValuePair;
				if (!this.TryMatchCore(message, out keyValuePair))
				{
					data = default(FilterData);
					return false;
				}
				data = keyValuePair.Value;
				return true;
			}

			// Token: 0x06007280 RID: 29312 RVA: 0x001AB744 File Offset: 0x001A9944
			public bool GetMatchingValue(MessageBuffer buffer, out FilterData data)
			{
				bool matchingValue;
				using (Message message = buffer.CreateMessage())
				{
					matchingValue = this.GetMatchingValue(message, out data);
				}
				return matchingValue;
			}

			// Token: 0x06007281 RID: 29313 RVA: 0x001AB780 File Offset: 0x001A9980
			public bool GetMatchingValues(Message message, ICollection<FilterData> results)
			{
				FilterData item;
				if (!this.GetMatchingValue(message, out item))
				{
					return false;
				}
				results.Add(item);
				return true;
			}

			// Token: 0x06007282 RID: 29314 RVA: 0x001AB7A4 File Offset: 0x001A99A4
			public bool GetMatchingValues(MessageBuffer buffer, ICollection<FilterData> results)
			{
				bool matchingValues;
				using (Message message = buffer.CreateMessage())
				{
					matchingValues = this.GetMatchingValues(message, results);
				}
				return matchingValues;
			}

			// Token: 0x06007283 RID: 29315 RVA: 0x001AB7E0 File Offset: 0x001A99E0
			public bool GetMatchingFilter(Message message, out MessageFilter filter)
			{
				KeyValuePair<MessageFilter, FilterData> keyValuePair;
				if (!this.TryMatchCore(message, out keyValuePair))
				{
					filter = null;
					return false;
				}
				filter = keyValuePair.Key;
				return true;
			}

			// Token: 0x06007284 RID: 29316 RVA: 0x001AB808 File Offset: 0x001A9A08
			public bool GetMatchingFilter(MessageBuffer buffer, out MessageFilter filter)
			{
				bool matchingFilter;
				using (Message message = buffer.CreateMessage())
				{
					matchingFilter = this.GetMatchingFilter(message, out filter);
				}
				return matchingFilter;
			}

			// Token: 0x06007285 RID: 29317 RVA: 0x001AB844 File Offset: 0x001A9A44
			public bool GetMatchingFilters(Message message, ICollection<MessageFilter> results)
			{
				MessageFilter item;
				if (this.GetMatchingFilter(message, out item))
				{
					results.Add(item);
					return true;
				}
				return false;
			}

			// Token: 0x06007286 RID: 29318 RVA: 0x001AB868 File Offset: 0x001A9A68
			public bool GetMatchingFilters(MessageBuffer buffer, ICollection<MessageFilter> results)
			{
				bool matchingFilters;
				using (Message message = buffer.CreateMessage())
				{
					matchingFilters = this.GetMatchingFilters(message, results);
				}
				return matchingFilters;
			}

			// Token: 0x040040CD RID: 16589
			private Dictionary<UniqueId, KeyValuePair<MessageFilter, FilterData>> contextMappings;

			// Token: 0x040040CE RID: 16590
			private Dictionary<MessageFilter, FilterData> filterMappings;

			// Token: 0x040040CF RID: 16591
			private SecurityStandardsManager standardsManager;

			// Token: 0x040040D0 RID: 16592
			private string[] excludedActions;

			// Token: 0x040040D1 RID: 16593
			private bool isStrictMode;
		}
	}
}
