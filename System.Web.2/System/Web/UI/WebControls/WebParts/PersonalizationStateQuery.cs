using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000561 RID: 1377
	[Serializable]
	public sealed class PersonalizationStateQuery
	{
		// Token: 0x060045E9 RID: 17897 RVA: 0x000E66C8 File Offset: 0x000E48C8
		static PersonalizationStateQuery()
		{
			PersonalizationStateQuery._knownPropertyTypeMappings["PathToMatch"] = typeof(string);
			PersonalizationStateQuery._knownPropertyTypeMappings["UserInactiveSinceDate"] = typeof(DateTime);
			PersonalizationStateQuery._knownPropertyTypeMappings["UsernameToMatch"] = typeof(string);
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x000E672F File Offset: 0x000E492F
		public PersonalizationStateQuery()
		{
			this._data = new HybridDictionary(true);
			this._data["UserInactiveSinceDate"] = PersonalizationAdministration.DefaultInactiveSinceDate;
		}

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060045EB RID: 17899 RVA: 0x000E675D File Offset: 0x000E495D
		// (set) Token: 0x060045EC RID: 17900 RVA: 0x000E676F File Offset: 0x000E496F
		public string PathToMatch
		{
			get
			{
				return (string)this["PathToMatch"];
			}
			set
			{
				if (value != null)
				{
					value = value.Trim();
				}
				this._data["PathToMatch"] = value;
			}
		}

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060045ED RID: 17901 RVA: 0x000E6790 File Offset: 0x000E4990
		// (set) Token: 0x060045EE RID: 17902 RVA: 0x000E67AF File Offset: 0x000E49AF
		public DateTime UserInactiveSinceDate
		{
			get
			{
				object obj = this["UserInactiveSinceDate"];
				return (DateTime)obj;
			}
			set
			{
				this._data["UserInactiveSinceDate"] = value;
			}
		}

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x000E67C7 File Offset: 0x000E49C7
		// (set) Token: 0x060045F0 RID: 17904 RVA: 0x000E67D9 File Offset: 0x000E49D9
		public string UsernameToMatch
		{
			get
			{
				return (string)this["UsernameToMatch"];
			}
			set
			{
				if (value != null)
				{
					value = value.Trim();
				}
				this._data["UsernameToMatch"] = value;
			}
		}

		// Token: 0x1700149D RID: 5277
		public object this[string queryKey]
		{
			get
			{
				queryKey = StringUtil.CheckAndTrimString(queryKey, "queryKey");
				return this._data[queryKey];
			}
			set
			{
				queryKey = StringUtil.CheckAndTrimString(queryKey, "queryKey");
				if (PersonalizationStateQuery._knownPropertyTypeMappings.ContainsKey(queryKey))
				{
					Type type = PersonalizationStateQuery._knownPropertyTypeMappings[queryKey];
					if ((value == null && type.IsValueType) || (value != null && !type.IsAssignableFrom(value.GetType())))
					{
						throw new ArgumentException(SR.GetString("PersonalizationStateQuery_IncorrectValueType", new object[]
						{
							queryKey,
							type.FullName
						}));
					}
				}
				this._data[queryKey] = value;
			}
		}

		// Token: 0x04002686 RID: 9862
		private static readonly Dictionary<string, Type> _knownPropertyTypeMappings = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002687 RID: 9863
		private HybridDictionary _data;
	}
}
