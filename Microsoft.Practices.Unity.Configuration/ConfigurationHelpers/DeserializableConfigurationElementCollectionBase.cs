using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000004 RID: 4
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is a base class, name is reasonable")]
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Deserializable", Justification = "It is spelled correctly")]
	public abstract class DeserializableConfigurationElementCollectionBase<TElement> : ConfigurationElementCollection, IEnumerable<TElement>, IEnumerable where TElement : DeserializableConfigurationElement
	{
		// Token: 0x17000003 RID: 3
		public TElement this[int index]
		{
			get
			{
				return this.GetElement(index);
			}
			set
			{
				if (this.GetElement(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021AE File Offset: 0x000003AE
		protected virtual TElement GetElement(int index)
		{
			return (TElement)((object)base.BaseGet(index));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021BC File Offset: 0x000003BC
		protected virtual TElement GetElement(object key)
		{
			return (TElement)((object)base.BaseGet(key));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021CA File Offset: 0x000003CA
		public virtual void Deserialize(XmlReader reader)
		{
			this.DeserializeElement(reader, false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002280 File Offset: 0x00000480
		public new IEnumerator<TElement> GetEnumerator()
		{
			for (int index = 0; index < base.Count; index++)
			{
				yield return this[index];
			}
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000229C File Offset: 0x0000049C
		public void Add(TElement element)
		{
			this.BaseAdd(element);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022AA File Offset: 0x000004AA
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022B3 File Offset: 0x000004B3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022BC File Offset: 0x000004BC
		public void SerializeElementContents(XmlWriter writer, string elementName)
		{
			foreach (TElement telement in this)
			{
				writer.WriteElement(elementName, new Action<XmlWriter>(telement.SerializeContent));
			}
		}
	}
}
