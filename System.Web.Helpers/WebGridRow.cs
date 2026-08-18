using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Helpers.Resources;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x02000022 RID: 34
	public class WebGridRow : DynamicObject, IEnumerable<object>, IEnumerable
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00008638 File Offset: 0x00006838
		public WebGridRow(WebGrid webGrid, object value, int rowIndex)
		{
			this._grid = webGrid;
			this._value = value;
			this._rowIndex = rowIndex;
			this._dynamic = (value as IDynamicMetaObjectProvider);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008661 File Offset: 0x00006861
		[Dynamic]
		public dynamic Value
		{
			[return: Dynamic]
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00008669 File Offset: 0x00006869
		public WebGrid WebGrid
		{
			get
			{
				return this._grid;
			}
		}

		// Token: 0x17000073 RID: 115
		public object this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
				}
				object result = null;
				if (!this.TryGetMember(name, out result))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, HelpersResources.WebGrid_ColumnNotFound, new object[]
					{
						name
					}));
				}
				return result;
			}
		}

		// Token: 0x17000074 RID: 116
		public object this[int index]
		{
			get
			{
				if (index < 0 || index >= this._grid.ColumnNames.Count<string>())
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.Skip(index).First<object>();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000086F8 File Offset: 0x000068F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000870C File Offset: 0x0000690C
		public IEnumerator<object> GetEnumerator()
		{
			if (this._values == null)
			{
				this._values = from c in this._grid.ColumnNames
				select WebGrid.GetMember(this, c);
			}
			return this._values.GetEnumerator();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008755 File Offset: 0x00006955
		public IHtmlString GetSelectLink(string text = null)
		{
			if (string.IsNullOrEmpty(text))
			{
				text = HelpersResources.WebGrid_SelectLinkText;
			}
			return WebGridRenderer.GridLink(this._grid, this.GetSelectUrl(), text);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008778 File Offset: 0x00006978
		public string GetSelectUrl()
		{
			NameValueCollection nameValueCollection = new NameValueCollection(1);
			nameValueCollection[this.WebGrid.SelectionFieldName] = ((long)this._rowIndex + 1L).ToString(CultureInfo.CurrentCulture);
			return this.WebGrid.GetPath(nameValueCollection, new string[0]);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000087C8 File Offset: 0x000069C8
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			return this.TryGetRowIndex(binder.Name, out result) || (this._dynamic != null && DynamicHelper.TryGetMemberValue(this._dynamic, binder, out result)) || WebGridRow.TryGetComplexMember(this._value, binder.Name, out result);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008814 File Offset: 0x00006A14
		internal bool TryGetMember(string memberName, out object result)
		{
			result = null;
			return this.TryGetRowIndex(memberName, out result) || (this._dynamic != null && DynamicHelper.TryGetMemberValue(this._dynamic, memberName, out result)) || WebGridRow.TryGetComplexMember(this._value, memberName, out result);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000884B File Offset: 0x00006A4B
		public override string ToString()
		{
			return this._value.ToString();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008858 File Offset: 0x00006A58
		private bool TryGetRowIndex(string memberName, out object result)
		{
			result = null;
			if (string.IsNullOrEmpty(memberName))
			{
				return false;
			}
			if (memberName == "ROW")
			{
				result = this._rowIndex;
				return true;
			}
			return false;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008884 File Offset: 0x00006A84
		private static bool TryGetComplexMember(object obj, string name, out object result)
		{
			result = null;
			string[] array = name.Split(new char[]
			{
				'.'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (obj == null || !WebGridRow.TryGetMember(obj, array[i], out result))
				{
					result = null;
					return false;
				}
				obj = result;
			}
			return true;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000088D0 File Offset: 0x00006AD0
		private static bool TryGetMember(object obj, string name, out object result)
		{
			PropertyInfo property = obj.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			if (property != null && property.GetIndexParameters().Length == 0)
			{
				result = property.GetValue(obj, null);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x04000087 RID: 135
		private const string RowIndexMemberName = "ROW";

		// Token: 0x04000088 RID: 136
		private const BindingFlags BindFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x04000089 RID: 137
		private WebGrid _grid;

		// Token: 0x0400008A RID: 138
		private IDynamicMetaObjectProvider _dynamic;

		// Token: 0x0400008B RID: 139
		private int _rowIndex;

		// Token: 0x0400008C RID: 140
		private object _value;

		// Token: 0x0400008D RID: 141
		[Dynamic(new bool[]
		{
			false,
			true
		})]
		private IEnumerable<dynamic> _values;
	}
}
