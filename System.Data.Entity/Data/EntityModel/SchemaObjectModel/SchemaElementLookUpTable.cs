using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200030B RID: 779
	internal sealed class SchemaElementLookUpTable<T> : IEnumerable<!0>, IEnumerable, ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000AF870 File Offset: 0x000ADA70
		public int Count
		{
			get
			{
				return this.KeyToType.Count;
			}
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000AF87D File Offset: 0x000ADA7D
		public bool ContainsKey(string key)
		{
			return this.KeyToType.ContainsKey(SchemaElementLookUpTable<T>.KeyFromName(key));
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000AF890 File Offset: 0x000ADA90
		public T LookUpEquivalentKey(string key)
		{
			key = SchemaElementLookUpTable<T>.KeyFromName(key);
			T result;
			if (this.KeyToType.TryGetValue(key, out result))
			{
				return result;
			}
			return default(T);
		}

		// Token: 0x17000917 RID: 2327
		public T this[string key]
		{
			get
			{
				return this.KeyToType[SchemaElementLookUpTable<T>.KeyFromName(key)];
			}
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000AF8D3 File Offset: 0x000ADAD3
		public T GetElementAt(int index)
		{
			return this.KeyToType[this._keysInDefOrder[index]];
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000AF8EC File Offset: 0x000ADAEC
		public IEnumerator<T> GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000AF8EC File Offset: 0x000ADAEC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000AF8FF File Offset: 0x000ADAFF
		public IEnumerator<S> GetFilteredEnumerator<S>() where S : T
		{
			return new SchemaElementLookUpTableEnumerator<S, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000AF914 File Offset: 0x000ADB14
		public AddErrorKind TryAdd(T type)
		{
			if (string.IsNullOrEmpty(type.Identity))
			{
				return AddErrorKind.MissingNameError;
			}
			string text = SchemaElementLookUpTable<T>.KeyFromElement(type);
			T t;
			if (this.KeyToType.TryGetValue(text, out t))
			{
				return AddErrorKind.DuplicateNameError;
			}
			this.KeyToType.Add(text, type);
			this._keysInDefOrder.Add(text);
			return AddErrorKind.Succeeded;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000AF968 File Offset: 0x000ADB68
		public void Add(T type, bool doNotAddErrorForEmptyName, Func<object, string> duplicateKeyErrorFormat)
		{
			AddErrorKind addErrorKind = this.TryAdd(type);
			if (addErrorKind == AddErrorKind.MissingNameError)
			{
				if (!doNotAddErrorForEmptyName)
				{
					type.AddError(ErrorCode.InvalidName, EdmSchemaErrorSeverity.Error, Strings.MissingName);
				}
				return;
			}
			if (addErrorKind == AddErrorKind.DuplicateNameError)
			{
				type.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, duplicateKeyErrorFormat(type.FQName));
			}
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000AF9BB File Offset: 0x000ADBBB
		private static string KeyFromElement(T type)
		{
			return SchemaElementLookUpTable<T>.KeyFromName(type.Identity);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x00048AC0 File Offset: 0x00046CC0
		private static string KeyFromName(string unnormalizedKey)
		{
			return unnormalizedKey;
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000AF9CD File Offset: 0x000ADBCD
		private Dictionary<string, T> KeyToType
		{
			get
			{
				if (this._keyToType == null)
				{
					this._keyToType = new Dictionary<string, T>(StringComparer.Ordinal);
				}
				return this._keyToType;
			}
		}

		// Token: 0x0400141E RID: 5150
		private Dictionary<string, T> _keyToType;

		// Token: 0x0400141F RID: 5151
		private List<string> _keysInDefOrder = new List<string>();
	}
}
