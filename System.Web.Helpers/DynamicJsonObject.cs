using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web.Helpers.Resources;

namespace System.Web.Helpers
{
	// Token: 0x0200000F RID: 15
	public class DynamicJsonObject : DynamicObject
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00003BC8 File Offset: 0x00001DC8
		public DynamicJsonObject(IDictionary<string, object> values)
		{
			this._values = values.ToDictionary((KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => Json.WrapObject(p.Value), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003C28 File Offset: 0x00001E28
		public override bool TryConvert(ConvertBinder binder, out object result)
		{
			result = null;
			if (binder.Type.IsAssignableFrom(this._values.GetType()))
			{
				result = this._values;
				return true;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, HelpersResources.Json_UnableToConvertType, new object[]
			{
				binder.Type
			}));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003C81 File Offset: 0x00001E81
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = this.GetValue(binder.Name);
			return true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003C92 File Offset: 0x00001E92
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			this._values[binder.Name] = Json.WrapObject(value);
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003CAC File Offset: 0x00001EAC
		public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			string key = DynamicJsonObject.GetKey(indexes);
			if (!string.IsNullOrEmpty(key))
			{
				this._values[key] = Json.WrapObject(value);
			}
			return true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003CDC File Offset: 0x00001EDC
		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			string key = DynamicJsonObject.GetKey(indexes);
			result = null;
			if (!string.IsNullOrEmpty(key))
			{
				result = this.GetValue(key);
			}
			return true;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003D05 File Offset: 0x00001F05
		private static string GetKey(object[] indexes)
		{
			if (indexes.Length == 1)
			{
				return (string)indexes[0];
			}
			return null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003D17 File Offset: 0x00001F17
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return this._values.Keys;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003D24 File Offset: 0x00001F24
		private object GetValue(string name)
		{
			object result;
			if (this._values.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0400002F RID: 47
		private readonly IDictionary<string, object> _values;
	}
}
