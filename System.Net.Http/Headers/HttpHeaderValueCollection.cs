using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x02000032 RID: 50
	[__DynamicallyInvokable]
	public sealed class HttpHeaderValueCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable where T : class
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B5EE File Offset: 0x000097EE
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetCount();
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B5F6 File Offset: 0x000097F6
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B5F9 File Offset: 0x000097F9
		internal bool IsSpecialValueSet
		{
			get
			{
				return this.specialValue != null && this.store.ContainsParsedValue(this.headerName, this.specialValue);
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B628 File Offset: 0x00009828
		internal HttpHeaderValueCollection(string headerName, HttpHeaders store) : this(headerName, store, default(T), null)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B648 File Offset: 0x00009848
		internal HttpHeaderValueCollection(string headerName, HttpHeaders store, Action<HttpHeaderValueCollection<T>, T> validator) : this(headerName, store, default(T), validator)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B667 File Offset: 0x00009867
		internal HttpHeaderValueCollection(string headerName, HttpHeaders store, T specialValue) : this(headerName, store, specialValue, null)
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B673 File Offset: 0x00009873
		internal HttpHeaderValueCollection(string headerName, HttpHeaders store, T specialValue, Action<HttpHeaderValueCollection<T>, T> validator)
		{
			this.store = store;
			this.headerName = headerName;
			this.specialValue = specialValue;
			this.validator = validator;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B698 File Offset: 0x00009898
		[__DynamicallyInvokable]
		public void Add(T item)
		{
			this.CheckValue(item);
			this.store.AddParsedValue(this.headerName, item);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B6B8 File Offset: 0x000098B8
		[__DynamicallyInvokable]
		public void ParseAdd(string input)
		{
			this.store.Add(this.headerName, input);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B6CC File Offset: 0x000098CC
		[__DynamicallyInvokable]
		public bool TryParseAdd(string input)
		{
			return this.store.TryParseAndAddValue(this.headerName, input);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B6E0 File Offset: 0x000098E0
		[__DynamicallyInvokable]
		public void Clear()
		{
			this.store.Remove(this.headerName);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B6F4 File Offset: 0x000098F4
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			this.CheckValue(item);
			return this.store.ContainsParsedValue(this.headerName, item);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B714 File Offset: 0x00009914
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			object parsedValues = this.store.GetParsedValues(this.headerName);
			if (parsedValues == null)
			{
				return;
			}
			List<object> list = parsedValues as List<object>;
			if (list != null)
			{
				list.CopyTo(array, arrayIndex);
				return;
			}
			if (arrayIndex == array.Length)
			{
				throw new ArgumentException(SR.net_http_copyto_array_too_small);
			}
			array[arrayIndex] = (parsedValues as T);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B792 File Offset: 0x00009992
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			this.CheckValue(item);
			return this.store.RemoveParsedValue(this.headerName, item);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B7B2 File Offset: 0x000099B2
		[__DynamicallyInvokable]
		public IEnumerator<T> GetEnumerator()
		{
			object parsedValues = this.store.GetParsedValues(this.headerName);
			if (parsedValues == null)
			{
				yield break;
			}
			List<object> list = parsedValues as List<object>;
			if (list == null)
			{
				yield return parsedValues as T;
			}
			else
			{
				foreach (object obj in list)
				{
					yield return obj as T;
				}
				List<object>.Enumerator enumerator = default(List<object>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B7C1 File Offset: 0x000099C1
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000B7C9 File Offset: 0x000099C9
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.store.GetHeaderString(this.headerName);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B7DC File Offset: 0x000099DC
		internal string GetHeaderStringWithoutSpecial()
		{
			if (!this.IsSpecialValueSet)
			{
				return this.ToString();
			}
			return this.store.GetHeaderString(this.headerName, this.specialValue);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B809 File Offset: 0x00009A09
		internal void SetSpecialValue()
		{
			if (!this.store.ContainsParsedValue(this.headerName, this.specialValue))
			{
				this.store.AddParsedValue(this.headerName, this.specialValue);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B845 File Offset: 0x00009A45
		internal void RemoveSpecialValue()
		{
			this.store.RemoveParsedValue(this.headerName, this.specialValue);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B864 File Offset: 0x00009A64
		private void CheckValue(T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.validator != null)
			{
				this.validator(this, item);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B890 File Offset: 0x00009A90
		private int GetCount()
		{
			object parsedValues = this.store.GetParsedValues(this.headerName);
			if (parsedValues == null)
			{
				return 0;
			}
			List<object> list = parsedValues as List<object>;
			if (list == null)
			{
				return 1;
			}
			return list.Count;
		}

		// Token: 0x0400013F RID: 319
		private string headerName;

		// Token: 0x04000140 RID: 320
		private HttpHeaders store;

		// Token: 0x04000141 RID: 321
		private T specialValue;

		// Token: 0x04000142 RID: 322
		private Action<HttpHeaderValueCollection<T>, T> validator;
	}
}
