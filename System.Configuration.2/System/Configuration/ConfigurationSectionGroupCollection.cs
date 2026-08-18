using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	public sealed class ConfigurationSectionGroupCollection : NameObjectCollectionBase
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		internal ConfigurationSectionGroupCollection(MgmtConfigurationRecord configRecord, ConfigurationSectionGroup configSectionGroup) : base(StringComparer.Ordinal)
		{
			this._configRecord = configRecord;
			this._configSectionGroup = configSectionGroup;
			foreach (object obj in this._configRecord.SectionGroupFactories)
			{
				FactoryId factoryId = (FactoryId)((DictionaryEntry)obj).Value;
				if (factoryId.Group == this._configSectionGroup.SectionGroupName)
				{
					base.BaseAdd(factoryId.Name, factoryId.Name);
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00011910 File Offset: 0x0000FB10
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00011E9C File Offset: 0x0001009C
		internal void DetachFromConfigurationRecord()
		{
			this._configRecord = null;
			base.BaseClear();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011EAB File Offset: 0x000100AB
		private void VerifyIsAttachedToConfigRecord()
		{
			if (this._configRecord == null)
			{
				throw new InvalidOperationException(SR.GetString("Config_cannot_edit_configurationsectiongroup_when_not_attached"));
			}
		}

		// Token: 0x170000C4 RID: 196
		public ConfigurationSectionGroup this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x170000C5 RID: 197
		public ConfigurationSectionGroup this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00011ED7 File Offset: 0x000100D7
		public void Add(string name, ConfigurationSectionGroup sectionGroup)
		{
			this.VerifyIsAttachedToConfigRecord();
			this._configRecord.AddConfigurationSectionGroup(this._configSectionGroup.SectionGroupName, name, sectionGroup);
			base.BaseAdd(name, name);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011F00 File Offset: 0x00010100
		public void Clear()
		{
			this.VerifyIsAttachedToConfigRecord();
			if (this._configSectionGroup.IsRoot)
			{
				this._configRecord.RemoveLocationWriteRequirement();
			}
			string[] array = base.BaseGetAllKeys();
			foreach (string name in array)
			{
				this.Remove(name);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000119B5 File Offset: 0x0000FBB5
		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00011F50 File Offset: 0x00010150
		public void CopyTo(ConfigurationSectionGroup[] array, int index)
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

		// Token: 0x060002CD RID: 717 RVA: 0x00011FA1 File Offset: 0x000101A1
		public ConfigurationSectionGroup Get(int index)
		{
			return this.Get(this.GetKey(index));
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00011FB0 File Offset: 0x000101B0
		public ConfigurationSectionGroup Get(string name)
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
			return this._configRecord.GetSectionGroup(configKey);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00012001 File Offset: 0x00010201
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

		// Token: 0x060002D0 RID: 720 RVA: 0x00011A85 File Offset: 0x0000FC85
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00011A8E File Offset: 0x0000FC8E
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00012010 File Offset: 0x00010210
		public void Remove(string name)
		{
			this.VerifyIsAttachedToConfigRecord();
			this._configRecord.RemoveConfigurationSectionGroup(this._configSectionGroup.SectionGroupName, name);
			string key = BaseConfigurationRecord.CombineConfigKey(this._configSectionGroup.SectionGroupName, name);
			if (!this._configRecord.SectionFactories.Contains(key))
			{
				base.BaseRemove(name);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00012066 File Offset: 0x00010266
		public void RemoveAt(int index)
		{
			this.VerifyIsAttachedToConfigRecord();
			this.Remove(this.GetKey(index));
		}

		// Token: 0x04000215 RID: 533
		private MgmtConfigurationRecord _configRecord;

		// Token: 0x04000216 RID: 534
		private ConfigurationSectionGroup _configSectionGroup;
	}
}
