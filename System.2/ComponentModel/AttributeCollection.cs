using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000515 RID: 1301
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class AttributeCollection : ICollection, IEnumerable
	{
		// Token: 0x06003142 RID: 12610 RVA: 0x000DF138 File Offset: 0x000DD338
		public AttributeCollection(params Attribute[] attributes)
		{
			if (attributes == null)
			{
				attributes = new Attribute[0];
			}
			this._attributes = attributes;
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i] == null)
				{
					throw new ArgumentNullException("attributes");
				}
			}
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000DF17B File Offset: 0x000DD37B
		protected AttributeCollection()
		{
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x000DF184 File Offset: 0x000DD384
		public static AttributeCollection FromExisting(AttributeCollection existing, params Attribute[] newAttributes)
		{
			if (existing == null)
			{
				throw new ArgumentNullException("existing");
			}
			if (newAttributes == null)
			{
				newAttributes = new Attribute[0];
			}
			Attribute[] array = new Attribute[existing.Count + newAttributes.Length];
			int count = existing.Count;
			existing.CopyTo(array, 0);
			for (int i = 0; i < newAttributes.Length; i++)
			{
				if (newAttributes[i] == null)
				{
					throw new ArgumentNullException("newAttributes");
				}
				bool flag = false;
				for (int j = 0; j < existing.Count; j++)
				{
					if (array[j].TypeId.Equals(newAttributes[i].TypeId))
					{
						flag = true;
						array[j] = newAttributes[i];
						break;
					}
				}
				if (!flag)
				{
					array[count++] = newAttributes[i];
				}
			}
			Attribute[] array2;
			if (count < array.Length)
			{
				array2 = new Attribute[count];
				Array.Copy(array, 0, array2, 0, count);
			}
			else
			{
				array2 = array;
			}
			return new AttributeCollection(array2);
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000DF254 File Offset: 0x000DD454
		protected virtual Attribute[] Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000DF25C File Offset: 0x000DD45C
		public int Count
		{
			get
			{
				return this.Attributes.Length;
			}
		}

		// Token: 0x17000C0E RID: 3086
		public virtual Attribute this[int index]
		{
			get
			{
				return this.Attributes[index];
			}
		}

		// Token: 0x17000C0F RID: 3087
		public virtual Attribute this[Type attributeType]
		{
			get
			{
				object obj = AttributeCollection.internalSyncObject;
				Attribute defaultAttribute;
				lock (obj)
				{
					if (this._foundAttributeTypes == null)
					{
						this._foundAttributeTypes = new AttributeCollection.AttributeEntry[5];
					}
					int i = 0;
					while (i < 5)
					{
						if (this._foundAttributeTypes[i].type == attributeType)
						{
							int index = this._foundAttributeTypes[i].index;
							if (index != -1)
							{
								return this.Attributes[index];
							}
							return this.GetDefaultAttribute(attributeType);
						}
						else
						{
							if (this._foundAttributeTypes[i].type == null)
							{
								break;
							}
							i++;
						}
					}
					int index2 = this._index;
					this._index = index2 + 1;
					i = index2;
					if (this._index >= 5)
					{
						this._index = 0;
					}
					this._foundAttributeTypes[i].type = attributeType;
					int num = this.Attributes.Length;
					for (int j = 0; j < num; j++)
					{
						Attribute attribute = this.Attributes[j];
						Type type = attribute.GetType();
						if (type == attributeType)
						{
							this._foundAttributeTypes[i].index = j;
							return attribute;
						}
					}
					for (int k = 0; k < num; k++)
					{
						Attribute attribute2 = this.Attributes[k];
						Type type2 = attribute2.GetType();
						if (attributeType.IsAssignableFrom(type2))
						{
							this._foundAttributeTypes[i].index = k;
							return attribute2;
						}
					}
					this._foundAttributeTypes[i].index = -1;
					defaultAttribute = this.GetDefaultAttribute(attributeType);
				}
				return defaultAttribute;
			}
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000DF428 File Offset: 0x000DD628
		public bool Contains(Attribute attribute)
		{
			Attribute attribute2 = this[attribute.GetType()];
			return attribute2 != null && attribute2.Equals(attribute);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000DF454 File Offset: 0x000DD654
		public bool Contains(Attribute[] attributes)
		{
			if (attributes == null)
			{
				return true;
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Contains(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000DF484 File Offset: 0x000DD684
		protected Attribute GetDefaultAttribute(Type attributeType)
		{
			object obj = AttributeCollection.internalSyncObject;
			Attribute result;
			lock (obj)
			{
				if (AttributeCollection._defaultAttributes == null)
				{
					AttributeCollection._defaultAttributes = new Hashtable();
				}
				if (AttributeCollection._defaultAttributes.ContainsKey(attributeType))
				{
					result = (Attribute)AttributeCollection._defaultAttributes[attributeType];
				}
				else
				{
					Attribute attribute = null;
					Type reflectionType = TypeDescriptor.GetReflectionType(attributeType);
					FieldInfo field = reflectionType.GetField("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					if (field != null && field.IsStatic)
					{
						attribute = (Attribute)field.GetValue(null);
					}
					else
					{
						ConstructorInfo constructor = reflectionType.UnderlyingSystemType.GetConstructor(new Type[0]);
						if (constructor != null)
						{
							attribute = (Attribute)constructor.Invoke(new object[0]);
							if (!attribute.IsDefaultAttribute())
							{
								attribute = null;
							}
						}
					}
					AttributeCollection._defaultAttributes[attributeType] = attribute;
					result = attribute;
				}
			}
			return result;
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000DF57C File Offset: 0x000DD77C
		public IEnumerator GetEnumerator()
		{
			return this.Attributes.GetEnumerator();
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000DF58C File Offset: 0x000DD78C
		public bool Matches(Attribute attribute)
		{
			for (int i = 0; i < this.Attributes.Length; i++)
			{
				if (this.Attributes[i].Match(attribute))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000DF5C0 File Offset: 0x000DD7C0
		public bool Matches(Attribute[] attributes)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Matches(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000DF5E9 File Offset: 0x000DD7E9
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000DF5F1 File Offset: 0x000DD7F1
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000DF5F4 File Offset: 0x000DD7F4
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000DF5F7 File Offset: 0x000DD7F7
		public void CopyTo(Array array, int index)
		{
			Array.Copy(this.Attributes, 0, array, index, this.Attributes.Length);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000DF60F File Offset: 0x000DD80F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002913 RID: 10515
		public static readonly AttributeCollection Empty = new AttributeCollection(null);

		// Token: 0x04002914 RID: 10516
		private static Hashtable _defaultAttributes;

		// Token: 0x04002915 RID: 10517
		private Attribute[] _attributes;

		// Token: 0x04002916 RID: 10518
		private static object internalSyncObject = new object();

		// Token: 0x04002917 RID: 10519
		private const int FOUND_TYPES_LIMIT = 5;

		// Token: 0x04002918 RID: 10520
		private AttributeCollection.AttributeEntry[] _foundAttributeTypes;

		// Token: 0x04002919 RID: 10521
		private int _index;

		// Token: 0x0200088F RID: 2191
		private struct AttributeEntry
		{
			// Token: 0x040037C6 RID: 14278
			public Type type;

			// Token: 0x040037C7 RID: 14279
			public int index;
		}
	}
}
