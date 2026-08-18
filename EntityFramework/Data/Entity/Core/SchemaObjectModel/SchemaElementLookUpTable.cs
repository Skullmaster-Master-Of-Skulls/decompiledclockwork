using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000389 RID: 905
	internal sealed class SchemaElementLookUpTable<T> : IEnumerable<!0>, IEnumerable, ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0009A66D File Offset: 0x0009886D
		public int Count
		{
			get
			{
				return this.KeyToType.Count;
			}
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0009A67A File Offset: 0x0009887A
		public bool ContainsKey(string key)
		{
			return this.KeyToType.ContainsKey(SchemaElementLookUpTable<T>.KeyFromName(key));
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0009A690 File Offset: 0x00098890
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

		// Token: 0x17000428 RID: 1064
		public T this[string key]
		{
			get
			{
				return this.KeyToType[SchemaElementLookUpTable<T>.KeyFromName(key)];
			}
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x0009A6D3 File Offset: 0x000988D3
		public T GetElementAt(int index)
		{
			return this.KeyToType[this._keysInDefOrder[index]];
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0009A6EC File Offset: 0x000988EC
		public IEnumerator<T> GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0009A6FF File Offset: 0x000988FF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SchemaElementLookUpTableEnumerator<T, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x0009A712 File Offset: 0x00098912
		public IEnumerator<S> GetFilteredEnumerator<S>() where S : T
		{
			return new SchemaElementLookUpTableEnumerator<S, T>(this.KeyToType, this._keysInDefOrder);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0009A728 File Offset: 0x00098928
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

		// Token: 0x060020C8 RID: 8392 RVA: 0x0009A780 File Offset: 0x00098980
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

		// Token: 0x060020C9 RID: 8393 RVA: 0x0009A7D9 File Offset: 0x000989D9
		private static string KeyFromElement(T type)
		{
			return SchemaElementLookUpTable<T>.KeyFromName(type.Identity);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0009A7ED File Offset: 0x000989ED
		private static string KeyFromName(string unnormalizedKey)
		{
			return unnormalizedKey;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0009A7F0 File Offset: 0x000989F0
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

		// Token: 0x04000B99 RID: 2969
		private Dictionary<string, T> _keyToType;

		// Token: 0x04000B9A RID: 2970
		private readonly List<string> _keysInDefOrder = new List<string>();
	}
}
