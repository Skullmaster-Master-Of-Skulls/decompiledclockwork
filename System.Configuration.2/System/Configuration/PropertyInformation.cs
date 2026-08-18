using System;
using System.ComponentModel;

namespace System.Configuration
{
	// Token: 0x02000077 RID: 119
	public sealed class PropertyInformation
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00018FEA File Offset: 0x000171EA
		private ConfigurationProperty Prop
		{
			get
			{
				if (this._Prop == null)
				{
					this._Prop = this.ThisElement.Properties[this.PropertyName];
				}
				return this._Prop;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00019016 File Offset: 0x00017216
		internal PropertyInformation(ConfigurationElement thisElement, string propertyName)
		{
			this.PropertyName = propertyName;
			this.ThisElement = thisElement;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0001902C File Offset: 0x0001722C
		public string Name
		{
			get
			{
				return this.PropertyName;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00019034 File Offset: 0x00017234
		internal string ProvidedName
		{
			get
			{
				return this.Prop.ProvidedName;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x00019041 File Offset: 0x00017241
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x00019054 File Offset: 0x00017254
		public object Value
		{
			get
			{
				return this.ThisElement[this.PropertyName];
			}
			set
			{
				this.ThisElement[this.PropertyName] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x00019068 File Offset: 0x00017268
		public object DefaultValue
		{
			get
			{
				return this.Prop.DefaultValue;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00019075 File Offset: 0x00017275
		public PropertyValueOrigin ValueOrigin
		{
			get
			{
				if (this.ThisElement.Values[this.PropertyName] == null)
				{
					return PropertyValueOrigin.Default;
				}
				if (this.ThisElement.Values.IsInherited(this.PropertyName))
				{
					return PropertyValueOrigin.Inherited;
				}
				return PropertyValueOrigin.SetHere;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000190AC File Offset: 0x000172AC
		public bool IsModified
		{
			get
			{
				return this.ThisElement.Values[this.PropertyName] != null && this.ThisElement.Values.IsModified(this.PropertyName);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x000190E3 File Offset: 0x000172E3
		public bool IsKey
		{
			get
			{
				return this.Prop.IsKey;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x000190F0 File Offset: 0x000172F0
		public bool IsRequired
		{
			get
			{
				return this.Prop.IsRequired;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00019100 File Offset: 0x00017300
		public bool IsLocked
		{
			get
			{
				return (this.ThisElement.LockedAllExceptAttributesList != null && !this.ThisElement.LockedAllExceptAttributesList.DefinedInParent(this.PropertyName)) || (this.ThisElement.LockedAttributesList != null && (this.ThisElement.LockedAttributesList.DefinedInParent(this.PropertyName) || this.ThisElement.LockedAttributesList.DefinedInParent("*"))) || ((this.ThisElement.ItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default && (this.ThisElement.ItemLocked & ConfigurationValueFlags.Inherited) > ConfigurationValueFlags.Default);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00019194 File Offset: 0x00017394
		public string Source
		{
			get
			{
				PropertySourceInfo sourceInfo = this.ThisElement.Values.GetSourceInfo(this.PropertyName);
				if (sourceInfo == null)
				{
					sourceInfo = this.ThisElement.Values.GetSourceInfo(string.Empty);
				}
				if (sourceInfo == null)
				{
					return string.Empty;
				}
				return sourceInfo.FileName;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000191E0 File Offset: 0x000173E0
		public int LineNumber
		{
			get
			{
				PropertySourceInfo sourceInfo = this.ThisElement.Values.GetSourceInfo(this.PropertyName);
				if (sourceInfo == null)
				{
					sourceInfo = this.ThisElement.Values.GetSourceInfo(string.Empty);
				}
				if (sourceInfo == null)
				{
					return 0;
				}
				return sourceInfo.LineNumber;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00019228 File Offset: 0x00017428
		public Type Type
		{
			get
			{
				return this.Prop.Type;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00019235 File Offset: 0x00017435
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this.Prop.Validator;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00019242 File Offset: 0x00017442
		public TypeConverter Converter
		{
			get
			{
				return this.Prop.Converter;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001924F File Offset: 0x0001744F
		public string Description
		{
			get
			{
				return this.Prop.Description;
			}
		}

		// Token: 0x040002C3 RID: 707
		private ConfigurationElement ThisElement;

		// Token: 0x040002C4 RID: 708
		private string PropertyName;

		// Token: 0x040002C5 RID: 709
		private ConfigurationProperty _Prop;

		// Token: 0x040002C6 RID: 710
		private const string LockAll = "*";
	}
}
