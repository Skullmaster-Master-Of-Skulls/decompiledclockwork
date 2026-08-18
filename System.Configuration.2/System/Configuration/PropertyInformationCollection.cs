using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	public sealed class PropertyInformationCollection : NameObjectCollectionBase
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0001925C File Offset: 0x0001745C
		internal PropertyInformationCollection(ConfigurationElement thisElement) : base(StringComparer.Ordinal)
		{
			this.ThisElement = thisElement;
			foreach (object obj in this.ThisElement.Properties)
			{
				ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
				if (configurationProperty.Name != this.ThisElement.ElementTagName)
				{
					base.BaseAdd(configurationProperty.Name, new PropertyInformation(thisElement, configurationProperty.Name));
				}
			}
			base.IsReadOnly = true;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00011910 File Offset: 0x0000FB10
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x17000160 RID: 352
		public PropertyInformation this[string propertyName]
		{
			get
			{
				PropertyInformation propertyInformation = (PropertyInformation)base.BaseGet(propertyName);
				if (propertyInformation == null)
				{
					PropertyInformation propertyInformation2 = (PropertyInformation)base.BaseGet(ConfigurationProperty.DefaultCollectionPropertyName);
					if (propertyInformation2 != null && propertyInformation2.ProvidedName == propertyName)
					{
						propertyInformation = propertyInformation2;
					}
				}
				return propertyInformation;
			}
		}

		// Token: 0x17000161 RID: 353
		internal PropertyInformation this[int index]
		{
			get
			{
				return (PropertyInformation)base.BaseGet(base.BaseGetKey(index));
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00019354 File Offset: 0x00017554
		public void CopyTo(PropertyInformation[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length < this.Count + index)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			foreach (object obj in this)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				array[index++] = propertyInformation;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000193D0 File Offset: 0x000175D0
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

		// Token: 0x040002C7 RID: 711
		private ConfigurationElement ThisElement;
	}
}
