using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000554 RID: 1364
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PersonalizableAttribute : Attribute
	{
		// Token: 0x06004551 RID: 17745 RVA: 0x000E4B9D File Offset: 0x000E2D9D
		public PersonalizableAttribute() : this(true, PersonalizationScope.User, false)
		{
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x000E4BA8 File Offset: 0x000E2DA8
		public PersonalizableAttribute(bool isPersonalizable) : this(isPersonalizable, PersonalizationScope.User, false)
		{
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000E4BB3 File Offset: 0x000E2DB3
		public PersonalizableAttribute(PersonalizationScope scope) : this(true, scope, false)
		{
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x000E4BBE File Offset: 0x000E2DBE
		public PersonalizableAttribute(PersonalizationScope scope, bool isSensitive) : this(true, scope, isSensitive)
		{
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x000E4BC9 File Offset: 0x000E2DC9
		private PersonalizableAttribute(bool isPersonalizable, PersonalizationScope scope, bool isSensitive)
		{
			this._isPersonalizable = isPersonalizable;
			this._isSensitive = isSensitive;
			if (this._isPersonalizable)
			{
				this._scope = scope;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x000E4BEE File Offset: 0x000E2DEE
		public bool IsPersonalizable
		{
			get
			{
				return this._isPersonalizable;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x000E4BF6 File Offset: 0x000E2DF6
		public bool IsSensitive
		{
			get
			{
				return this._isSensitive;
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06004558 RID: 17752 RVA: 0x000E4BFE File Offset: 0x000E2DFE
		public PersonalizationScope Scope
		{
			get
			{
				return this._scope;
			}
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x000E4C08 File Offset: 0x000E2E08
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			PersonalizableAttribute personalizableAttribute = obj as PersonalizableAttribute;
			return personalizableAttribute != null && (personalizableAttribute.IsPersonalizable == this.IsPersonalizable && personalizableAttribute.Scope == this.Scope) && personalizableAttribute.IsSensitive == this.IsSensitive;
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x000E4C53 File Offset: 0x000E2E53
		public override int GetHashCode()
		{
			return HashCodeCombiner.CombineHashCodes(this._isPersonalizable.GetHashCode(), this._scope.GetHashCode(), this._isSensitive.GetHashCode());
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x000E4C84 File Offset: 0x000E2E84
		public static ICollection GetPersonalizableProperties(Type type)
		{
			PersonalizableTypeEntry personalizableTypeEntry = (PersonalizableTypeEntry)PersonalizableAttribute.PersonalizableTypeTable[type];
			if (personalizableTypeEntry == null)
			{
				personalizableTypeEntry = new PersonalizableTypeEntry(type);
				PersonalizableAttribute.PersonalizableTypeTable[type] = personalizableTypeEntry;
			}
			return personalizableTypeEntry.PropertyInfos;
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x000E4CC0 File Offset: 0x000E2EC0
		internal static IDictionary GetPersonalizablePropertyEntries(Type type)
		{
			PersonalizableTypeEntry personalizableTypeEntry = (PersonalizableTypeEntry)PersonalizableAttribute.PersonalizableTypeTable[type];
			if (personalizableTypeEntry == null)
			{
				personalizableTypeEntry = new PersonalizableTypeEntry(type);
				PersonalizableAttribute.PersonalizableTypeTable[type] = personalizableTypeEntry;
			}
			return personalizableTypeEntry.PropertyEntries;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x000E4CFC File Offset: 0x000E2EFC
		internal static IDictionary GetPersonalizablePropertyValues(Control control, PersonalizationScope scope, bool excludeSensitive)
		{
			IDictionary dictionary = null;
			IDictionary personalizablePropertyEntries = PersonalizableAttribute.GetPersonalizablePropertyEntries(control.GetType());
			if (personalizablePropertyEntries.Count != 0)
			{
				foreach (object obj in personalizablePropertyEntries)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = (string)dictionaryEntry.Key;
					PersonalizablePropertyEntry personalizablePropertyEntry = (PersonalizablePropertyEntry)dictionaryEntry.Value;
					if ((!excludeSensitive || !personalizablePropertyEntry.IsSensitive) && (scope != PersonalizationScope.User || personalizablePropertyEntry.Scope != PersonalizationScope.Shared))
					{
						if (dictionary == null)
						{
							dictionary = new HybridDictionary(personalizablePropertyEntries.Count, false);
						}
						object property = FastPropertyAccessor.GetProperty(control, text, control.DesignMode);
						dictionary[text] = new Pair(personalizablePropertyEntry.PropertyInfo, property);
					}
				}
			}
			if (dictionary == null)
			{
				dictionary = new HybridDictionary(false);
			}
			return dictionary;
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000E4DDC File Offset: 0x000E2FDC
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PersonalizableAttribute.Default);
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000E4DEC File Offset: 0x000E2FEC
		public override bool Match(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			PersonalizableAttribute personalizableAttribute = obj as PersonalizableAttribute;
			return personalizableAttribute != null && personalizableAttribute.IsPersonalizable == this.IsPersonalizable;
		}

		// Token: 0x0400265C RID: 9820
		internal static readonly Type PersonalizableAttributeType = typeof(PersonalizableAttribute);

		// Token: 0x0400265D RID: 9821
		private static readonly IDictionary PersonalizableTypeTable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x0400265E RID: 9822
		public static readonly PersonalizableAttribute NotPersonalizable = new PersonalizableAttribute(false);

		// Token: 0x0400265F RID: 9823
		public static readonly PersonalizableAttribute Personalizable = new PersonalizableAttribute(true);

		// Token: 0x04002660 RID: 9824
		public static readonly PersonalizableAttribute UserPersonalizable = new PersonalizableAttribute(PersonalizationScope.User);

		// Token: 0x04002661 RID: 9825
		public static readonly PersonalizableAttribute SharedPersonalizable = new PersonalizableAttribute(PersonalizationScope.Shared);

		// Token: 0x04002662 RID: 9826
		public static readonly PersonalizableAttribute Default = PersonalizableAttribute.NotPersonalizable;

		// Token: 0x04002663 RID: 9827
		private bool _isPersonalizable;

		// Token: 0x04002664 RID: 9828
		private bool _isSensitive;

		// Token: 0x04002665 RID: 9829
		private PersonalizationScope _scope;
	}
}
