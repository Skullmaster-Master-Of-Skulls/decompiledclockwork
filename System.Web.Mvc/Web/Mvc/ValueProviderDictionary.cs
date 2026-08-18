using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x02000184 RID: 388
	[Obsolete("The recommended alternative is to use one of the specific ValueProvider types, such as FormValueProvider.")]
	public class ValueProviderDictionary : IDictionary<string, ValueProviderResult>, ICollection<KeyValuePair<string, ValueProviderResult>>, IEnumerable<KeyValuePair<string, ValueProviderResult>>, IEnumerable, IValueProvider
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0001D0AD File Offset: 0x0001B2AD
		public ValueProviderDictionary(ControllerContext controllerContext)
		{
			this.ControllerContext = controllerContext;
			if (controllerContext != null)
			{
				this.PopulateDictionary();
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0001D0D5 File Offset: 0x0001B2D5
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x0001D0DD File Offset: 0x0001B2DD
		public ControllerContext ControllerContext { get; private set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0001D0E6 File Offset: 0x0001B2E6
		public int Count
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).Count;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		internal Dictionary<string, ValueProviderResult> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x0001D0FB File Offset: 0x0001B2FB
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).IsReadOnly;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001D108 File Offset: 0x0001B308
		public ICollection<string> Keys
		{
			get
			{
				return this.Dictionary.Keys;
			}
		}

		// Token: 0x17000274 RID: 628
		public ValueProviderResult this[string key]
		{
			get
			{
				ValueProviderResult result;
				this.Dictionary.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this.Dictionary[key] = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001D144 File Offset: 0x0001B344
		public ICollection<ValueProviderResult> Values
		{
			get
			{
				return this.Dictionary.Values;
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0001D151 File Offset: 0x0001B351
		public void Add(KeyValuePair<string, ValueProviderResult> item)
		{
			((ICollection<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).Add(item);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0001D160 File Offset: 0x0001B360
		public void Add(string key, object value)
		{
			string attemptedValue = Convert.ToString(value, CultureInfo.InvariantCulture);
			ValueProviderResult value2 = new ValueProviderResult(value, attemptedValue, CultureInfo.InvariantCulture);
			this.Add(key, value2);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0001D18E File Offset: 0x0001B38E
		public void Add(string key, ValueProviderResult value)
		{
			this.Dictionary.Add(key, value);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0001D19D File Offset: 0x0001B39D
		private void AddToDictionaryIfNotPresent(string key, ValueProviderResult result)
		{
			if (!string.IsNullOrEmpty(key) && !this.Dictionary.ContainsKey(key))
			{
				this.Dictionary.Add(key, result);
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001D1C2 File Offset: 0x0001B3C2
		public void Clear()
		{
			((ICollection<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).Clear();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0001D1CF File Offset: 0x0001B3CF
		public bool Contains(KeyValuePair<string, ValueProviderResult> item)
		{
			return ((ICollection<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).Contains(item);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001D1DD File Offset: 0x0001B3DD
		public bool ContainsKey(string key)
		{
			return this.Dictionary.ContainsKey(key);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001D1EB File Offset: 0x0001B3EB
		public void CopyTo(KeyValuePair<string, ValueProviderResult>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001D1FA File Offset: 0x0001B3FA
		public IEnumerator<KeyValuePair<string, ValueProviderResult>> GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).GetEnumerator();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0001D208 File Offset: 0x0001B408
		private void PopulateDictionary()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			NameValueCollection form = this.ControllerContext.HttpContext.Request.Form;
			if (form != null)
			{
				string[] allKeys = form.AllKeys;
				foreach (string text in allKeys)
				{
					string[] values = form.GetValues(text);
					string attemptedValue = form[text];
					ValueProviderResult result = new ValueProviderResult(values, attemptedValue, currentCulture);
					this.AddToDictionaryIfNotPresent(text, result);
				}
			}
			RouteValueDictionary values2 = this.ControllerContext.RouteData.Values;
			if (values2 != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in values2)
				{
					string key = keyValuePair.Key;
					object value = keyValuePair.Value;
					string attemptedValue2 = Convert.ToString(value, invariantCulture);
					ValueProviderResult result2 = new ValueProviderResult(value, attemptedValue2, invariantCulture);
					this.AddToDictionaryIfNotPresent(key, result2);
				}
			}
			NameValueCollection queryString = this.ControllerContext.HttpContext.Request.QueryString;
			if (queryString != null)
			{
				string[] allKeys2 = queryString.AllKeys;
				foreach (string text2 in allKeys2)
				{
					string[] values3 = queryString.GetValues(text2);
					string attemptedValue3 = queryString[text2];
					ValueProviderResult result3 = new ValueProviderResult(values3, attemptedValue3, invariantCulture);
					this.AddToDictionaryIfNotPresent(text2, result3);
				}
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0001D37C File Offset: 0x0001B57C
		public bool Remove(KeyValuePair<string, ValueProviderResult> item)
		{
			return ((ICollection<KeyValuePair<string, ValueProviderResult>>)this.Dictionary).Remove(item);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0001D38A File Offset: 0x0001B58A
		public bool Remove(string key)
		{
			return this.Dictionary.Remove(key);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0001D398 File Offset: 0x0001B598
		public bool TryGetValue(string key, out ValueProviderResult value)
		{
			return this.Dictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0001D3A7 File Offset: 0x0001B5A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.Dictionary).GetEnumerator();
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
		bool IValueProvider.ContainsPrefix(string prefix)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			return ValueProviderUtil.CollectionContainsPrefix(this.Keys, prefix);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		ValueProviderResult IValueProvider.GetValue(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ValueProviderResult result;
			this.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x040002D7 RID: 727
		private readonly Dictionary<string, ValueProviderResult> _dictionary = new Dictionary<string, ValueProviderResult>(StringComparer.OrdinalIgnoreCase);
	}
}
