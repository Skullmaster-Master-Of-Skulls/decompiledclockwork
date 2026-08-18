using System;

namespace System.Configuration
{
	// Token: 0x02000714 RID: 1812
	public class SettingsProperty
	{
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000EB5B5 File Offset: 0x000EA5B5
		// (set) Token: 0x06003772 RID: 14194 RVA: 0x000EB5BD File Offset: 0x000EA5BD
		public virtual string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000EB5C6 File Offset: 0x000EA5C6
		// (set) Token: 0x06003774 RID: 14196 RVA: 0x000EB5CE File Offset: 0x000EA5CE
		public virtual bool IsReadOnly
		{
			get
			{
				return this._IsReadOnly;
			}
			set
			{
				this._IsReadOnly = value;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000EB5D7 File Offset: 0x000EA5D7
		// (set) Token: 0x06003776 RID: 14198 RVA: 0x000EB5DF File Offset: 0x000EA5DF
		public virtual object DefaultValue
		{
			get
			{
				return this._DefaultValue;
			}
			set
			{
				this._DefaultValue = value;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000EB5E8 File Offset: 0x000EA5E8
		// (set) Token: 0x06003778 RID: 14200 RVA: 0x000EB5F0 File Offset: 0x000EA5F0
		public virtual Type PropertyType
		{
			get
			{
				return this._PropertyType;
			}
			set
			{
				this._PropertyType = value;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000EB5F9 File Offset: 0x000EA5F9
		// (set) Token: 0x0600377A RID: 14202 RVA: 0x000EB601 File Offset: 0x000EA601
		public virtual SettingsSerializeAs SerializeAs
		{
			get
			{
				return this._SerializeAs;
			}
			set
			{
				this._SerializeAs = value;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000EB60A File Offset: 0x000EA60A
		// (set) Token: 0x0600377C RID: 14204 RVA: 0x000EB612 File Offset: 0x000EA612
		public virtual SettingsProvider Provider
		{
			get
			{
				return this._Provider;
			}
			set
			{
				this._Provider = value;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000EB61B File Offset: 0x000EA61B
		public virtual SettingsAttributeDictionary Attributes
		{
			get
			{
				return this._Attributes;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x000EB623 File Offset: 0x000EA623
		// (set) Token: 0x0600377F RID: 14207 RVA: 0x000EB62B File Offset: 0x000EA62B
		public bool ThrowOnErrorDeserializing
		{
			get
			{
				return this._ThrowOnErrorDeserializing;
			}
			set
			{
				this._ThrowOnErrorDeserializing = value;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x000EB634 File Offset: 0x000EA634
		// (set) Token: 0x06003781 RID: 14209 RVA: 0x000EB63C File Offset: 0x000EA63C
		public bool ThrowOnErrorSerializing
		{
			get
			{
				return this._ThrowOnErrorSerializing;
			}
			set
			{
				this._ThrowOnErrorSerializing = value;
			}
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000EB645 File Offset: 0x000EA645
		public SettingsProperty(string name)
		{
			this._Name = name;
			this._Attributes = new SettingsAttributeDictionary();
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x000EB660 File Offset: 0x000EA660
		public SettingsProperty(string name, Type propertyType, SettingsProvider provider, bool isReadOnly, object defaultValue, SettingsSerializeAs serializeAs, SettingsAttributeDictionary attributes, bool throwOnErrorDeserializing, bool throwOnErrorSerializing)
		{
			this._Name = name;
			this._PropertyType = propertyType;
			this._Provider = provider;
			this._IsReadOnly = isReadOnly;
			this._DefaultValue = defaultValue;
			this._SerializeAs = serializeAs;
			this._Attributes = attributes;
			this._ThrowOnErrorDeserializing = throwOnErrorDeserializing;
			this._ThrowOnErrorSerializing = throwOnErrorSerializing;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x000EB6B8 File Offset: 0x000EA6B8
		public SettingsProperty(SettingsProperty propertyToCopy)
		{
			this._Name = propertyToCopy.Name;
			this._IsReadOnly = propertyToCopy.IsReadOnly;
			this._DefaultValue = propertyToCopy.DefaultValue;
			this._SerializeAs = propertyToCopy.SerializeAs;
			this._Provider = propertyToCopy.Provider;
			this._PropertyType = propertyToCopy.PropertyType;
			this._ThrowOnErrorDeserializing = propertyToCopy.ThrowOnErrorDeserializing;
			this._ThrowOnErrorSerializing = propertyToCopy.ThrowOnErrorSerializing;
			this._Attributes = new SettingsAttributeDictionary(propertyToCopy.Attributes);
		}

		// Token: 0x040031D7 RID: 12759
		private string _Name;

		// Token: 0x040031D8 RID: 12760
		private bool _IsReadOnly;

		// Token: 0x040031D9 RID: 12761
		private object _DefaultValue;

		// Token: 0x040031DA RID: 12762
		private SettingsSerializeAs _SerializeAs;

		// Token: 0x040031DB RID: 12763
		private SettingsProvider _Provider;

		// Token: 0x040031DC RID: 12764
		private SettingsAttributeDictionary _Attributes;

		// Token: 0x040031DD RID: 12765
		private Type _PropertyType;

		// Token: 0x040031DE RID: 12766
		private bool _ThrowOnErrorDeserializing;

		// Token: 0x040031DF RID: 12767
		private bool _ThrowOnErrorSerializing;
	}
}
