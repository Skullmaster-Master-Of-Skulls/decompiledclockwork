using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x02000031 RID: 49
	[__DynamicallyInvokable]
	public abstract class HttpHeaders : IEnumerable<KeyValuePair<string, IEnumerable<string>>>, IEnumerable
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000A571 File Offset: 0x00008771
		[__DynamicallyInvokable]
		protected HttpHeaders()
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A57C File Offset: 0x0000877C
		[__DynamicallyInvokable]
		public void Add(string name, string value)
		{
			this.CheckHeaderName(name);
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo;
			bool flag;
			this.PrepareHeaderInfoForAdd(name, out headerStoreItemInfo, out flag);
			this.ParseAndAddValue(name, headerStoreItemInfo, value);
			if (flag && headerStoreItemInfo.ParsedValue != null)
			{
				this.AddHeaderToStore(name, headerStoreItemInfo);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A5B8 File Offset: 0x000087B8
		[__DynamicallyInvokable]
		public void Add(string name, IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			this.CheckHeaderName(name);
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo;
			bool flag;
			this.PrepareHeaderInfoForAdd(name, out headerStoreItemInfo, out flag);
			try
			{
				foreach (string value in values)
				{
					this.ParseAndAddValue(name, headerStoreItemInfo, value);
				}
			}
			finally
			{
				if (flag && headerStoreItemInfo.ParsedValue != null)
				{
					this.AddHeaderToStore(name, headerStoreItemInfo);
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A644 File Offset: 0x00008844
		[__DynamicallyInvokable]
		public bool TryAddWithoutValidation(string name, string value)
		{
			if (!this.TryCheckHeaderName(name))
			{
				return false;
			}
			if (value == null)
			{
				value = string.Empty;
			}
			HttpHeaders.HeaderStoreItemInfo orCreateHeaderInfo = this.GetOrCreateHeaderInfo(name, false);
			HttpHeaders.AddValue(orCreateHeaderInfo, value, HttpHeaders.StoreLocation.Raw);
			return true;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A678 File Offset: 0x00008878
		[__DynamicallyInvokable]
		public bool TryAddWithoutValidation(string name, IEnumerable<string> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (!this.TryCheckHeaderName(name))
			{
				return false;
			}
			HttpHeaders.HeaderStoreItemInfo orCreateHeaderInfo = this.GetOrCreateHeaderInfo(name, false);
			foreach (string text in values)
			{
				HttpHeaders.AddValue(orCreateHeaderInfo, text ?? string.Empty, HttpHeaders.StoreLocation.Raw);
			}
			return true;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A6F0 File Offset: 0x000088F0
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this.headerStore != null)
			{
				this.headerStore.Clear();
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A705 File Offset: 0x00008905
		[__DynamicallyInvokable]
		public bool Remove(string name)
		{
			this.CheckHeaderName(name);
			return this.headerStore != null && this.headerStore.Remove(name);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A724 File Offset: 0x00008924
		[__DynamicallyInvokable]
		public IEnumerable<string> GetValues(string name)
		{
			this.CheckHeaderName(name);
			IEnumerable<string> result;
			if (!this.TryGetValues(name, out result))
			{
				throw new InvalidOperationException(SR.net_http_headers_not_found);
			}
			return result;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A750 File Offset: 0x00008950
		[__DynamicallyInvokable]
		public bool TryGetValues(string name, out IEnumerable<string> values)
		{
			if (!this.TryCheckHeaderName(name))
			{
				values = null;
				return false;
			}
			if (this.headerStore == null)
			{
				values = null;
				return false;
			}
			HttpHeaders.HeaderStoreItemInfo info = null;
			if (this.TryGetAndParseHeaderInfo(name, out info))
			{
				values = HttpHeaders.GetValuesAsStrings(info);
				return true;
			}
			values = null;
			return false;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A794 File Offset: 0x00008994
		[__DynamicallyInvokable]
		public bool Contains(string name)
		{
			this.CheckHeaderName(name);
			if (this.headerStore == null)
			{
				return false;
			}
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo = null;
			return this.TryGetAndParseHeaderInfo(name, out headerStoreItemInfo);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A7C0 File Offset: 0x000089C0
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in this)
			{
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append(": ");
				stringBuilder.Append(this.GetHeaderString(keyValuePair.Key));
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A848 File Offset: 0x00008A48
		internal IEnumerable<KeyValuePair<string, string>> GetHeaderStrings()
		{
			if (this.headerStore == null)
			{
				yield break;
			}
			foreach (KeyValuePair<string, HttpHeaders.HeaderStoreItemInfo> keyValuePair in this.headerStore)
			{
				HttpHeaders.HeaderStoreItemInfo value = keyValuePair.Value;
				string headerString = this.GetHeaderString(value);
				yield return new KeyValuePair<string, string>(keyValuePair.Key, headerString);
			}
			Dictionary<string, HttpHeaders.HeaderStoreItemInfo>.Enumerator enumerator = default(Dictionary<string, HttpHeaders.HeaderStoreItemInfo>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A858 File Offset: 0x00008A58
		internal string GetHeaderString(string headerName)
		{
			return this.GetHeaderString(headerName, null);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A864 File Offset: 0x00008A64
		internal string GetHeaderString(string headerName, object exclude)
		{
			HttpHeaders.HeaderStoreItemInfo info;
			if (!this.TryGetHeaderInfo(headerName, out info))
			{
				return string.Empty;
			}
			return this.GetHeaderString(info, exclude);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A88A File Offset: 0x00008A8A
		private string GetHeaderString(HttpHeaders.HeaderStoreItemInfo info)
		{
			return this.GetHeaderString(info, null);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A894 File Offset: 0x00008A94
		private string GetHeaderString(HttpHeaders.HeaderStoreItemInfo info, object exclude)
		{
			string result = string.Empty;
			string[] valuesAsStrings = HttpHeaders.GetValuesAsStrings(info, exclude);
			if (valuesAsStrings.Length == 1)
			{
				result = valuesAsStrings[0];
			}
			else
			{
				string separator = ", ";
				if (info.Parser != null && info.Parser.SupportsMultipleValues)
				{
					separator = info.Parser.Separator;
				}
				result = string.Join(separator, valuesAsStrings);
			}
			return result;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A8EB File Offset: 0x00008AEB
		[__DynamicallyInvokable]
		public IEnumerator<KeyValuePair<string, IEnumerable<string>>> GetEnumerator()
		{
			if (this.headerStore == null)
			{
				yield break;
			}
			List<string> invalidHeaders = null;
			foreach (KeyValuePair<string, HttpHeaders.HeaderStoreItemInfo> keyValuePair in this.headerStore)
			{
				HttpHeaders.HeaderStoreItemInfo value = keyValuePair.Value;
				if (!this.ParseRawHeaderValues(keyValuePair.Key, value, false))
				{
					if (invalidHeaders == null)
					{
						invalidHeaders = new List<string>();
					}
					invalidHeaders.Add(keyValuePair.Key);
				}
				else
				{
					string[] valuesAsStrings = HttpHeaders.GetValuesAsStrings(value);
					yield return new KeyValuePair<string, IEnumerable<string>>(keyValuePair.Key, valuesAsStrings);
				}
			}
			Dictionary<string, HttpHeaders.HeaderStoreItemInfo>.Enumerator enumerator = default(Dictionary<string, HttpHeaders.HeaderStoreItemInfo>.Enumerator);
			if (invalidHeaders != null)
			{
				using (List<string>.Enumerator enumerator2 = invalidHeaders.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string key = enumerator2.Current;
						this.headerStore.Remove(key);
					}
					yield break;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000A8FA File Offset: 0x00008AFA
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000A902 File Offset: 0x00008B02
		internal void SetConfiguration(Dictionary<string, HttpHeaderParser> parserStore, HashSet<string> invalidHeaders)
		{
			this.parserStore = parserStore;
			this.invalidHeaders = invalidHeaders;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A914 File Offset: 0x00008B14
		internal void AddParsedValue(string name, object value)
		{
			HttpHeaders.HeaderStoreItemInfo orCreateHeaderInfo = this.GetOrCreateHeaderInfo(name, true);
			HttpHeaders.AddValue(orCreateHeaderInfo, value, HttpHeaders.StoreLocation.Parsed);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A934 File Offset: 0x00008B34
		internal void SetParsedValue(string name, object value)
		{
			HttpHeaders.HeaderStoreItemInfo orCreateHeaderInfo = this.GetOrCreateHeaderInfo(name, true);
			orCreateHeaderInfo.InvalidValue = null;
			orCreateHeaderInfo.ParsedValue = null;
			orCreateHeaderInfo.RawValue = null;
			HttpHeaders.AddValue(orCreateHeaderInfo, value, HttpHeaders.StoreLocation.Parsed);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A967 File Offset: 0x00008B67
		internal void SetOrRemoveParsedValue(string name, object value)
		{
			if (value == null)
			{
				this.Remove(name);
				return;
			}
			this.SetParsedValue(name, value);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A980 File Offset: 0x00008B80
		internal bool RemoveParsedValue(string name, object value)
		{
			if (this.headerStore == null)
			{
				return false;
			}
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo = null;
			if (!this.TryGetAndParseHeaderInfo(name, out headerStoreItemInfo))
			{
				return false;
			}
			bool result = false;
			if (headerStoreItemInfo.ParsedValue == null)
			{
				return false;
			}
			IEqualityComparer comparer = headerStoreItemInfo.Parser.Comparer;
			List<object> list = headerStoreItemInfo.ParsedValue as List<object>;
			if (list == null)
			{
				if (this.AreEqual(value, headerStoreItemInfo.ParsedValue, comparer))
				{
					headerStoreItemInfo.ParsedValue = null;
					result = true;
				}
			}
			else
			{
				foreach (object obj in list)
				{
					if (this.AreEqual(value, obj, comparer))
					{
						result = list.Remove(obj);
						break;
					}
				}
				if (list.Count == 0)
				{
					headerStoreItemInfo.ParsedValue = null;
				}
			}
			if (headerStoreItemInfo.IsEmpty)
			{
				bool flag = this.Remove(name);
			}
			return result;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000AA60 File Offset: 0x00008C60
		internal bool ContainsParsedValue(string name, object value)
		{
			if (this.headerStore == null)
			{
				return false;
			}
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo = null;
			if (!this.TryGetAndParseHeaderInfo(name, out headerStoreItemInfo))
			{
				return false;
			}
			if (headerStoreItemInfo.ParsedValue == null)
			{
				return false;
			}
			List<object> list = headerStoreItemInfo.ParsedValue as List<object>;
			IEqualityComparer comparer = headerStoreItemInfo.Parser.Comparer;
			if (list == null)
			{
				return this.AreEqual(value, headerStoreItemInfo.ParsedValue, comparer);
			}
			foreach (object storeValue in list)
			{
				if (this.AreEqual(value, storeValue, comparer))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000AB0C File Offset: 0x00008D0C
		internal virtual void AddHeaders(HttpHeaders sourceHeaders)
		{
			if (sourceHeaders.headerStore == null)
			{
				return;
			}
			List<string> list = null;
			foreach (KeyValuePair<string, HttpHeaders.HeaderStoreItemInfo> keyValuePair in sourceHeaders.headerStore)
			{
				if (this.headerStore == null || !this.headerStore.ContainsKey(keyValuePair.Key))
				{
					HttpHeaders.HeaderStoreItemInfo value = keyValuePair.Value;
					if (!sourceHeaders.ParseRawHeaderValues(keyValuePair.Key, value, false))
					{
						if (list == null)
						{
							list = new List<string>();
						}
						list.Add(keyValuePair.Key);
					}
					else
					{
						this.AddHeaderInfo(keyValuePair.Key, value);
					}
				}
			}
			if (list != null)
			{
				foreach (string key in list)
				{
					sourceHeaders.headerStore.Remove(key);
				}
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000AC08 File Offset: 0x00008E08
		private void AddHeaderInfo(string headerName, HttpHeaders.HeaderStoreItemInfo sourceInfo)
		{
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo = this.CreateAndAddHeaderToStore(headerName);
			if (headerStoreItemInfo.Parser == null)
			{
				headerStoreItemInfo.ParsedValue = HttpHeaders.CloneStringHeaderInfoValues(sourceInfo.ParsedValue);
				return;
			}
			headerStoreItemInfo.InvalidValue = HttpHeaders.CloneStringHeaderInfoValues(sourceInfo.InvalidValue);
			if (sourceInfo.ParsedValue != null)
			{
				List<object> list = sourceInfo.ParsedValue as List<object>;
				if (list == null)
				{
					HttpHeaders.CloneAndAddValue(headerStoreItemInfo, sourceInfo.ParsedValue);
					return;
				}
				foreach (object source in list)
				{
					HttpHeaders.CloneAndAddValue(headerStoreItemInfo, source);
				}
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		private static void CloneAndAddValue(HttpHeaders.HeaderStoreItemInfo destinationInfo, object source)
		{
			ICloneable cloneable = source as ICloneable;
			if (cloneable != null)
			{
				HttpHeaders.AddValue(destinationInfo, cloneable.Clone(), HttpHeaders.StoreLocation.Parsed);
				return;
			}
			HttpHeaders.AddValue(destinationInfo, source, HttpHeaders.StoreLocation.Parsed);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		private static object CloneStringHeaderInfoValues(object source)
		{
			if (source == null)
			{
				return null;
			}
			List<object> list = source as List<object>;
			if (list == null)
			{
				return source;
			}
			return new List<object>(list);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000AD04 File Offset: 0x00008F04
		private HttpHeaders.HeaderStoreItemInfo GetOrCreateHeaderInfo(string name, bool parseRawValues)
		{
			HttpHeaders.HeaderStoreItemInfo result = null;
			bool flag;
			if (parseRawValues)
			{
				flag = this.TryGetAndParseHeaderInfo(name, out result);
			}
			else
			{
				flag = this.TryGetHeaderInfo(name, out result);
			}
			if (!flag)
			{
				result = this.CreateAndAddHeaderToStore(name);
			}
			return result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AD3C File Offset: 0x00008F3C
		private HttpHeaders.HeaderStoreItemInfo CreateAndAddHeaderToStore(string name)
		{
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo = new HttpHeaders.HeaderStoreItemInfo(this.GetParser(name));
			this.AddHeaderToStore(name, headerStoreItemInfo);
			return headerStoreItemInfo;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000AD5F File Offset: 0x00008F5F
		private void AddHeaderToStore(string name, HttpHeaders.HeaderStoreItemInfo info)
		{
			if (this.headerStore == null)
			{
				this.headerStore = new Dictionary<string, HttpHeaders.HeaderStoreItemInfo>(StringComparer.OrdinalIgnoreCase);
			}
			this.headerStore.Add(name, info);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000AD86 File Offset: 0x00008F86
		private bool TryGetHeaderInfo(string name, out HttpHeaders.HeaderStoreItemInfo info)
		{
			if (this.headerStore == null)
			{
				info = null;
				return false;
			}
			return this.headerStore.TryGetValue(name, out info);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000ADA2 File Offset: 0x00008FA2
		private bool TryGetAndParseHeaderInfo(string name, out HttpHeaders.HeaderStoreItemInfo info)
		{
			return this.TryGetHeaderInfo(name, out info) && this.ParseRawHeaderValues(name, info, true);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000ADBC File Offset: 0x00008FBC
		private bool ParseRawHeaderValues(string name, HttpHeaders.HeaderStoreItemInfo info, bool removeEmptyHeader)
		{
			lock (info)
			{
				if (info.RawValue != null)
				{
					List<string> list = info.RawValue as List<string>;
					if (list == null)
					{
						HttpHeaders.ParseSingleRawHeaderValue(name, info);
					}
					else
					{
						HttpHeaders.ParseMultipleRawHeaderValues(name, info, list);
					}
					info.RawValue = null;
					if (info.InvalidValue == null && info.ParsedValue == null)
					{
						if (removeEmptyHeader)
						{
							this.headerStore.Remove(name);
						}
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AE48 File Offset: 0x00009048
		private static void ParseMultipleRawHeaderValues(string name, HttpHeaders.HeaderStoreItemInfo info, List<string> rawValues)
		{
			if (info.Parser == null)
			{
				using (List<string>.Enumerator enumerator = rawValues.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string value = enumerator.Current;
						if (!HttpHeaders.ContainsInvalidNewLine(value, name))
						{
							HttpHeaders.AddValue(info, value, HttpHeaders.StoreLocation.Parsed);
						}
					}
					return;
				}
			}
			foreach (string text in rawValues)
			{
				if (!HttpHeaders.TryParseAndAddRawHeaderValue(name, info, text, true) && Logging.On)
				{
					Logging.PrintWarning(Logging.Http, string.Format(CultureInfo.InvariantCulture, SR.net_http_log_headers_invalid_value, new object[]
					{
						name,
						text
					}));
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AF18 File Offset: 0x00009118
		private static void ParseSingleRawHeaderValue(string name, HttpHeaders.HeaderStoreItemInfo info)
		{
			string text = info.RawValue as string;
			if (info.Parser == null)
			{
				if (!HttpHeaders.ContainsInvalidNewLine(text, name))
				{
					info.ParsedValue = info.RawValue;
					return;
				}
			}
			else if (!HttpHeaders.TryParseAndAddRawHeaderValue(name, info, text, true) && Logging.On)
			{
				Logging.PrintWarning(Logging.Http, string.Format(CultureInfo.InvariantCulture, SR.net_http_log_headers_invalid_value, new object[]
				{
					name,
					text
				}));
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000AF88 File Offset: 0x00009188
		internal bool TryParseAndAddValue(string name, string value)
		{
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo;
			bool flag;
			this.PrepareHeaderInfoForAdd(name, out headerStoreItemInfo, out flag);
			bool flag2 = HttpHeaders.TryParseAndAddRawHeaderValue(name, headerStoreItemInfo, value, false);
			if (flag2 && flag && headerStoreItemInfo.ParsedValue != null)
			{
				this.AddHeaderToStore(name, headerStoreItemInfo);
			}
			return flag2;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000AFC0 File Offset: 0x000091C0
		private static bool TryParseAndAddRawHeaderValue(string name, HttpHeaders.HeaderStoreItemInfo info, string value, bool addWhenInvalid)
		{
			if (!info.CanAddValue)
			{
				if (addWhenInvalid)
				{
					HttpHeaders.AddValue(info, value ?? string.Empty, HttpHeaders.StoreLocation.Invalid);
				}
				return false;
			}
			int i = 0;
			object obj = null;
			if (!info.Parser.TryParseValue(value, info.ParsedValue, ref i, out obj))
			{
				if (!HttpHeaders.ContainsInvalidNewLine(value, name) && addWhenInvalid)
				{
					HttpHeaders.AddValue(info, value ?? string.Empty, HttpHeaders.StoreLocation.Invalid);
				}
				return false;
			}
			if (value == null || i == value.Length)
			{
				if (obj != null)
				{
					HttpHeaders.AddValue(info, obj, HttpHeaders.StoreLocation.Parsed);
				}
				return true;
			}
			List<object> list = new List<object>();
			if (obj != null)
			{
				list.Add(obj);
			}
			while (i < value.Length)
			{
				if (!info.Parser.TryParseValue(value, info.ParsedValue, ref i, out obj))
				{
					if (!HttpHeaders.ContainsInvalidNewLine(value, name) && addWhenInvalid)
					{
						HttpHeaders.AddValue(info, value, HttpHeaders.StoreLocation.Invalid);
					}
					return false;
				}
				if (obj != null)
				{
					list.Add(obj);
				}
			}
			foreach (object value2 in list)
			{
				HttpHeaders.AddValue(info, value2, HttpHeaders.StoreLocation.Parsed);
			}
			return true;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B0E0 File Offset: 0x000092E0
		private static void AddValue(HttpHeaders.HeaderStoreItemInfo info, object value, HttpHeaders.StoreLocation location)
		{
			object obj = null;
			switch (location)
			{
			case HttpHeaders.StoreLocation.Raw:
				obj = info.RawValue;
				HttpHeaders.AddValueToStoreValue<string>(info, value, ref obj);
				info.RawValue = obj;
				return;
			case HttpHeaders.StoreLocation.Invalid:
				obj = info.InvalidValue;
				HttpHeaders.AddValueToStoreValue<string>(info, value, ref obj);
				info.InvalidValue = obj;
				return;
			case HttpHeaders.StoreLocation.Parsed:
				obj = info.ParsedValue;
				HttpHeaders.AddValueToStoreValue<object>(info, value, ref obj);
				info.ParsedValue = obj;
				return;
			default:
				return;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B14C File Offset: 0x0000934C
		private static void AddValueToStoreValue<T>(HttpHeaders.HeaderStoreItemInfo info, object value, ref object currentStoreValue) where T : class
		{
			if (currentStoreValue == null)
			{
				currentStoreValue = value;
				return;
			}
			List<T> list = currentStoreValue as List<T>;
			if (list == null)
			{
				list = new List<T>(2);
				list.Add(currentStoreValue as T);
				currentStoreValue = list;
			}
			list.Add(value as T);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B19C File Offset: 0x0000939C
		internal object GetParsedValues(string name)
		{
			HttpHeaders.HeaderStoreItemInfo headerStoreItemInfo = null;
			if (!this.TryGetAndParseHeaderInfo(name, out headerStoreItemInfo))
			{
				return null;
			}
			return headerStoreItemInfo.ParsedValue;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000B1BE File Offset: 0x000093BE
		private void PrepareHeaderInfoForAdd(string name, out HttpHeaders.HeaderStoreItemInfo info, out bool addToStore)
		{
			info = null;
			addToStore = false;
			if (!this.TryGetAndParseHeaderInfo(name, out info))
			{
				info = new HttpHeaders.HeaderStoreItemInfo(this.GetParser(name));
				addToStore = true;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000B1E4 File Offset: 0x000093E4
		private void ParseAndAddValue(string name, HttpHeaders.HeaderStoreItemInfo info, string value)
		{
			if (info.Parser == null)
			{
				HttpHeaders.CheckInvalidNewLine(value);
				HttpHeaders.AddValue(info, value ?? string.Empty, HttpHeaders.StoreLocation.Parsed);
				return;
			}
			if (!info.CanAddValue)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, SR.net_http_headers_single_value_header, new object[]
				{
					name
				}));
			}
			int i = 0;
			object obj = info.Parser.ParseValue(value, info.ParsedValue, ref i);
			if (value == null || i == value.Length)
			{
				if (obj != null)
				{
					HttpHeaders.AddValue(info, obj, HttpHeaders.StoreLocation.Parsed);
				}
				return;
			}
			List<object> list = new List<object>();
			if (obj != null)
			{
				list.Add(obj);
			}
			while (i < value.Length)
			{
				obj = info.Parser.ParseValue(value, info.ParsedValue, ref i);
				if (obj != null)
				{
					list.Add(obj);
				}
			}
			foreach (object value2 in list)
			{
				HttpHeaders.AddValue(info, value2, HttpHeaders.StoreLocation.Parsed);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B2E8 File Offset: 0x000094E8
		private HttpHeaderParser GetParser(string name)
		{
			if (this.parserStore == null)
			{
				return null;
			}
			HttpHeaderParser result = null;
			if (this.parserStore.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B314 File Offset: 0x00009514
		private void CheckHeaderName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "name");
			}
			if (HttpRuleParser.GetTokenLength(name, 0) != name.Length)
			{
				throw new FormatException(SR.net_http_headers_invalid_header_name);
			}
			if (this.invalidHeaders != null && this.invalidHeaders.Contains(name))
			{
				throw new InvalidOperationException(SR.net_http_headers_not_allowed_header_name);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B374 File Offset: 0x00009574
		private bool TryCheckHeaderName(string name)
		{
			return !string.IsNullOrEmpty(name) && HttpRuleParser.GetTokenLength(name, 0) == name.Length && (this.invalidHeaders == null || !this.invalidHeaders.Contains(name));
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B3AA File Offset: 0x000095AA
		private static void CheckInvalidNewLine(string value)
		{
			if (value == null)
			{
				return;
			}
			if (HttpRuleParser.ContainsInvalidNewLine(value))
			{
				throw new FormatException(SR.net_http_headers_no_newlines);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B3C3 File Offset: 0x000095C3
		private static bool ContainsInvalidNewLine(string value, string name)
		{
			if (HttpRuleParser.ContainsInvalidNewLine(value))
			{
				if (Logging.On)
				{
					Logging.PrintError(Logging.Http, string.Format(CultureInfo.InvariantCulture, SR.net_http_log_headers_no_newlines, new object[]
					{
						name,
						value
					}));
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000B3FE File Offset: 0x000095FE
		private static string[] GetValuesAsStrings(HttpHeaders.HeaderStoreItemInfo info)
		{
			return HttpHeaders.GetValuesAsStrings(info, null);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000B408 File Offset: 0x00009608
		private static string[] GetValuesAsStrings(HttpHeaders.HeaderStoreItemInfo info, object exclude)
		{
			int valueCount = HttpHeaders.GetValueCount(info);
			string[] array = new string[valueCount];
			if (valueCount > 0)
			{
				int num = 0;
				HttpHeaders.ReadStoreValues<string>(array, info.RawValue, null, null, ref num);
				HttpHeaders.ReadStoreValues<object>(array, info.ParsedValue, info.Parser, exclude, ref num);
				HttpHeaders.ReadStoreValues<string>(array, info.InvalidValue, null, null, ref num);
				if (num < valueCount)
				{
					string[] array2 = new string[num];
					Array.Copy(array, array2, num);
					array = array2;
				}
			}
			return array;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000B474 File Offset: 0x00009674
		private static int GetValueCount(HttpHeaders.HeaderStoreItemInfo info)
		{
			int result = 0;
			HttpHeaders.UpdateValueCount<string>(info.RawValue, ref result);
			HttpHeaders.UpdateValueCount<string>(info.InvalidValue, ref result);
			HttpHeaders.UpdateValueCount<object>(info.ParsedValue, ref result);
			return result;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000B4AC File Offset: 0x000096AC
		private static void UpdateValueCount<T>(object valueStore, ref int valueCount)
		{
			if (valueStore == null)
			{
				return;
			}
			List<T> list = valueStore as List<T>;
			if (list != null)
			{
				valueCount += list.Count;
				return;
			}
			valueCount++;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B4DC File Offset: 0x000096DC
		private static void ReadStoreValues<T>(string[] values, object storeValue, HttpHeaderParser parser, T exclude, ref int currentIndex)
		{
			if (storeValue != null)
			{
				List<T> list = storeValue as List<T>;
				if (list == null)
				{
					if (HttpHeaders.ShouldAdd<T>(storeValue, parser, exclude))
					{
						values[currentIndex] = ((parser == null) ? storeValue.ToString() : parser.ToString(storeValue));
						currentIndex++;
						return;
					}
				}
				else
				{
					foreach (T t in list)
					{
						object obj = t;
						if (HttpHeaders.ShouldAdd<T>(obj, parser, exclude))
						{
							values[currentIndex] = ((parser == null) ? obj.ToString() : parser.ToString(obj));
							currentIndex++;
						}
					}
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B58C File Offset: 0x0000978C
		private static bool ShouldAdd<T>(object storeValue, HttpHeaderParser parser, T exclude)
		{
			bool result = true;
			if (parser != null && exclude != null)
			{
				if (parser.Comparer != null)
				{
					result = !parser.Comparer.Equals(exclude, storeValue);
				}
				else
				{
					result = !exclude.Equals(storeValue);
				}
			}
			return result;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B5D9 File Offset: 0x000097D9
		private bool AreEqual(object value, object storeValue, IEqualityComparer comparer)
		{
			if (comparer != null)
			{
				return comparer.Equals(value, storeValue);
			}
			return value.Equals(storeValue);
		}

		// Token: 0x0400013C RID: 316
		private Dictionary<string, HttpHeaders.HeaderStoreItemInfo> headerStore;

		// Token: 0x0400013D RID: 317
		private Dictionary<string, HttpHeaderParser> parserStore;

		// Token: 0x0400013E RID: 318
		private HashSet<string> invalidHeaders;

		// Token: 0x02000063 RID: 99
		private enum StoreLocation
		{
			// Token: 0x040001C1 RID: 449
			Raw,
			// Token: 0x040001C2 RID: 450
			Invalid,
			// Token: 0x040001C3 RID: 451
			Parsed
		}

		// Token: 0x02000064 RID: 100
		private class HeaderStoreItemInfo
		{
			// Token: 0x1700010D RID: 269
			// (get) Token: 0x0600045B RID: 1115 RVA: 0x00010104 File Offset: 0x0000E304
			// (set) Token: 0x0600045C RID: 1116 RVA: 0x0001010C File Offset: 0x0000E30C
			internal object RawValue
			{
				get
				{
					return this.rawValue;
				}
				set
				{
					this.rawValue = value;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x0600045D RID: 1117 RVA: 0x00010115 File Offset: 0x0000E315
			// (set) Token: 0x0600045E RID: 1118 RVA: 0x0001011D File Offset: 0x0000E31D
			internal object InvalidValue
			{
				get
				{
					return this.invalidValue;
				}
				set
				{
					this.invalidValue = value;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600045F RID: 1119 RVA: 0x00010126 File Offset: 0x0000E326
			// (set) Token: 0x06000460 RID: 1120 RVA: 0x0001012E File Offset: 0x0000E32E
			internal object ParsedValue
			{
				get
				{
					return this.parsedValue;
				}
				set
				{
					this.parsedValue = value;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000461 RID: 1121 RVA: 0x00010137 File Offset: 0x0000E337
			internal HttpHeaderParser Parser
			{
				get
				{
					return this.parser;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001013F File Offset: 0x0000E33F
			internal bool CanAddValue
			{
				get
				{
					return this.parser.SupportsMultipleValues || (this.invalidValue == null && this.parsedValue == null);
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x00010163 File Offset: 0x0000E363
			internal bool IsEmpty
			{
				get
				{
					return this.rawValue == null && this.invalidValue == null && this.parsedValue == null;
				}
			}

			// Token: 0x06000464 RID: 1124 RVA: 0x00010180 File Offset: 0x0000E380
			internal HeaderStoreItemInfo(HttpHeaderParser parser)
			{
				this.parser = parser;
			}

			// Token: 0x040001C4 RID: 452
			private object rawValue;

			// Token: 0x040001C5 RID: 453
			private object invalidValue;

			// Token: 0x040001C6 RID: 454
			private object parsedValue;

			// Token: 0x040001C7 RID: 455
			private HttpHeaderParser parser;
		}
	}
}
