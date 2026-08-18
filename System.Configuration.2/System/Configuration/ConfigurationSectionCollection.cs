using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public sealed class ConfigurationSectionCollection : NameObjectCollectionBase
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00011868 File Offset: 0x0000FA68
		internal ConfigurationSectionCollection(MgmtConfigurationRecord configRecord, ConfigurationSectionGroup configSectionGroup) : base(StringComparer.Ordinal)
		{
			this._configRecord = configRecord;
			this._configSectionGroup = configSectionGroup;
			foreach (object obj in this._configRecord.SectionFactories)
			{
				FactoryId factoryId = (FactoryId)((DictionaryEntry)obj).Value;
				if (factoryId.Group == this._configSectionGroup.SectionGroupName)
				{
					base.BaseAdd(factoryId.Name, factoryId.Name);
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00011910 File Offset: 0x0000FB10
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0001191A File Offset: 0x0000FB1A
		internal void DetachFromConfigurationRecord()
		{
			this._configRecord = null;
			base.BaseClear();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00011929 File Offset: 0x0000FB29
		private void VerifyIsAttachedToConfigRecord()
		{
			if (this._configRecord == null)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsectiongroup_when_not_attached"));
			}
		}

		// Token: 0x170000B7 RID: 183
		public ConfigurationSection this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x170000B8 RID: 184
		public ConfigurationSection this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00011955 File Offset: 0x0000FB55
		public void Add(string name, ConfigurationSection section)
		{
			this.VerifyIsAttachedToConfigRecord();
			this._configRecord.AddConfigurationSection(this._configSectionGroup.SectionGroupName, name, section);
			base.BaseAdd(name, name);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00011980 File Offset: 0x0000FB80
		public void Clear()
		{
			this.VerifyIsAttachedToConfigRecord();
			string[] array = base.BaseGetAllKeys();
			foreach (string name in array)
			{
				this.Remove(name);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x000119B5 File Offset: 0x0000FBB5
		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000119C0 File Offset: 0x0000FBC0
		public void CopyTo(ConfigurationSection[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int count = this.Count;
			if (array.Length < count + index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int i = 0;
			int num = index;
			while (i < count)
			{
				array[num] = this.Get(i);
				i++;
				num++;
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00011A11 File Offset: 0x0000FC11
		public ConfigurationSection Get(int index)
		{
			return this.Get(this.GetKey(index));
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00011A20 File Offset: 0x0000FC20
		public ConfigurationSection Get(string name)
		{
			this.VerifyIsAttachedToConfigRecord();
			if (string.IsNullOrEmpty(name))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("name");
			}
			if (name.IndexOf('/') >= 0)
			{
				return null;
			}
			string configKey = BaseConfigurationRecord.CombineConfigKey(this._configSectionGroup.SectionGroupName, name);
			return (ConfigurationSection)this._configRecord.GetSection(configKey);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00011A76 File Offset: 0x0000FC76
		public override IEnumerator GetEnumerator()
		{
			int c = this.Count;
			int num;
			for (int i = 0; i < c; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00011A85 File Offset: 0x0000FC85
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00011A8E File Offset: 0x0000FC8E
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00011A98 File Offset: 0x0000FC98
		public void Remove(string name)
		{
			this.VerifyIsAttachedToConfigRecord();
			this._configRecord.RemoveConfigurationSection(this._configSectionGroup.SectionGroupName, name);
			string key = BaseConfigurationRecord.CombineConfigKey(this._configSectionGroup.SectionGroupName, name);
			if (!this._configRecord.SectionFactories.Contains(key))
			{
				base.BaseRemove(name);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00011AEE File Offset: 0x0000FCEE
		public void RemoveAt(int index)
		{
			this.VerifyIsAttachedToConfigRecord();
			this.Remove(this.GetKey(index));
		}

		// Token: 0x04000209 RID: 521
		private MgmtConfigurationRecord _configRecord;

		// Token: 0x0400020A RID: 522
		private ConfigurationSectionGroup _configSectionGroup;
	}
}
