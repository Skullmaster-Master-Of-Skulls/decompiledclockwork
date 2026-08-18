using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x02000056 RID: 86
	public sealed class ElementInformation
	{
		// Token: 0x06000365 RID: 869 RVA: 0x000131C7 File Offset: 0x000113C7
		internal ElementInformation(ConfigurationElement thisElement)
		{
			this._thisElement = thisElement;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000131D6 File Offset: 0x000113D6
		public PropertyInformationCollection Properties
		{
			get
			{
				if (this._internalProperties == null)
				{
					this._internalProperties = new PropertyInformationCollection(this._thisElement);
				}
				return this._internalProperties;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000131F7 File Offset: 0x000113F7
		public bool IsPresent
		{
			get
			{
				return this._thisElement.ElementPresent;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00013204 File Offset: 0x00011404
		public bool IsLocked
		{
			get
			{
				return (this._thisElement.ItemLocked & ConfigurationValueFlags.Locked) != ConfigurationValueFlags.Default && (this._thisElement.ItemLocked & ConfigurationValueFlags.Inherited) > ConfigurationValueFlags.Default;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00013228 File Offset: 0x00011428
		public bool IsCollection
		{
			get
			{
				ConfigurationElementCollection configurationElementCollection = this._thisElement as ConfigurationElementCollection;
				if (configurationElementCollection == null && this._thisElement.Properties.DefaultCollectionProperty != null)
				{
					configurationElementCollection = (this._thisElement[this._thisElement.Properties.DefaultCollectionProperty] as ConfigurationElementCollection);
				}
				return configurationElementCollection != null;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001327B File Offset: 0x0001147B
		internal PropertySourceInfo PropertyInfoInternal()
		{
			return this._thisElement.PropertyInfoInternal(this._thisElement.ElementTagName);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00013293 File Offset: 0x00011493
		internal void ChangeSourceAndLineNumber(PropertySourceInfo sourceInformation)
		{
			this._thisElement.Values.ChangeSourceInfo(this._thisElement.ElementTagName, sourceInformation);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000132B4 File Offset: 0x000114B4
		public string Source
		{
			get
			{
				PropertySourceInfo sourceInfo = this._thisElement.Values.GetSourceInfo(this._thisElement.ElementTagName);
				if (sourceInfo == null)
				{
					return null;
				}
				return sourceInfo.FileName;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000132E8 File Offset: 0x000114E8
		public int LineNumber
		{
			get
			{
				PropertySourceInfo sourceInfo = this._thisElement.Values.GetSourceInfo(this._thisElement.ElementTagName);
				if (sourceInfo == null)
				{
					return 0;
				}
				return sourceInfo.LineNumber;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0001331C File Offset: 0x0001151C
		public Type Type
		{
			get
			{
				return this._thisElement.GetType();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00013329 File Offset: 0x00011529
		public ConfigurationValidatorBase Validator
		{
			get
			{
				return this._thisElement.ElementProperty.Validator;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001333C File Offset: 0x0001153C
		private ConfigurationException[] GetReadOnlyErrorsList()
		{
			ArrayList errorsList = this._thisElement.GetErrorsList();
			int count = errorsList.Count;
			ConfigurationException[] array = new ConfigurationException[errorsList.Count];
			if (count != 0)
			{
				errorsList.CopyTo(array, 0);
			}
			return array;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00013374 File Offset: 0x00011574
		public ICollection Errors
		{
			get
			{
				if (this._errors == null)
				{
					this._errors = this.GetReadOnlyErrorsList();
				}
				return this._errors;
			}
		}

		// Token: 0x04000259 RID: 601
		private ConfigurationElement _thisElement;

		// Token: 0x0400025A RID: 602
		private PropertyInformationCollection _internalProperties;

		// Token: 0x0400025B RID: 603
		private ConfigurationException[] _errors;
	}
}
