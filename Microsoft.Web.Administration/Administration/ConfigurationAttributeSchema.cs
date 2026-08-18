using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200001D RID: 29
	[DebuggerDisplay("Name = {Name}")]
	public sealed class ConfigurationAttributeSchema
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000592B File Offset: 0x0000492B
		internal ConfigurationAttributeSchema(IAppHostPropertySchema schema)
		{
			this._schema = schema;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000593A File Offset: 0x0000493A
		public bool AllowInfinite
		{
			get
			{
				return this._schema.DoesAllowInfinite;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005947 File Offset: 0x00004947
		public object DefaultValue
		{
			get
			{
				return this._schema.DefaultValue;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005954 File Offset: 0x00004954
		public bool IsCaseSensitive
		{
			get
			{
				return this._schema.IsCaseSensitive;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005961 File Offset: 0x00004961
		public bool IsCombinedKey
		{
			get
			{
				return this._schema.IsCombinedKey;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000596E File Offset: 0x0000496E
		public bool IsEncrypted
		{
			get
			{
				return this._schema.IsEncrypted;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000597B File Offset: 0x0000497B
		public bool IsExpanded
		{
			get
			{
				return this._schema.IsExpanded;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005988 File Offset: 0x00004988
		public bool IsRequired
		{
			get
			{
				return this._schema.IsRequired;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005995 File Offset: 0x00004995
		public bool IsUniqueKey
		{
			get
			{
				return this._schema.IsUniqueKey;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000059A2 File Offset: 0x000049A2
		public string Name
		{
			get
			{
				return this._schema.Name;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000059AF File Offset: 0x000049AF
		public string TimeSpanFormat
		{
			get
			{
				return this._schema.TimeSpanFormat;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000059BC File Offset: 0x000049BC
		public string Type
		{
			get
			{
				return this._schema.Type;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000059C9 File Offset: 0x000049C9
		public string ValidationParameter
		{
			get
			{
				return this._schema.ValidationParameter;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000059D6 File Offset: 0x000049D6
		public string ValidationType
		{
			get
			{
				return this._schema.ValidationType;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000059E4 File Offset: 0x000049E4
		public ConfigurationEnumValueCollection GetEnumValues()
		{
			IAppHostConstantValueCollection possibleValues = this._schema.PossibleValues;
			if (possibleValues == null)
			{
				return null;
			}
			return new ConfigurationEnumValueCollection(possibleValues);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005A08 File Offset: 0x00004A08
		public object GetMetadata(string metadataType)
		{
			return this._schema.GetMetadata(metadataType);
		}

		// Token: 0x0400005A RID: 90
		private IAppHostPropertySchema _schema;
	}
}
