using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A4 RID: 932
	internal class DomainVariable<T_Variable, T_Element>
	{
		// Token: 0x0600335F RID: 13151 RVA: 0x000C8020 File Offset: 0x000C6220
		internal DomainVariable(T_Variable identifier, Set<T_Element> domain, IEqualityComparer<T_Variable> identifierComparer)
		{
			this._identifier = identifier;
			this._domain = domain.AsReadOnly();
			this._identifierComparer = (identifierComparer ?? EqualityComparer<T_Variable>.Default);
			int elementsHashCode = this._domain.GetElementsHashCode();
			int hashCode = this._identifierComparer.GetHashCode(this._identifier);
			this._hashCode = (elementsHashCode ^ hashCode);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000C807D File Offset: 0x000C627D
		internal DomainVariable(T_Variable identifier, Set<T_Element> domain) : this(identifier, domain, null)
		{
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000C8088 File Offset: 0x000C6288
		internal T_Variable Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000C8090 File Offset: 0x000C6290
		internal Set<T_Element> Domain
		{
			get
			{
				return this._domain;
			}
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000C8098 File Offset: 0x000C6298
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000C80A0 File Offset: 0x000C62A0
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			DomainVariable<T_Variable, T_Element> domainVariable = obj as DomainVariable<T_Variable, T_Element>;
			return domainVariable != null && this._hashCode == domainVariable._hashCode && this._identifierComparer.Equals(this._identifier, domainVariable._identifier) && this._domain.SetEquals(domainVariable._domain);
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000C80FC File Offset: 0x000C62FC
		public override string ToString()
		{
			string format = "{0}{{{1}}}";
			object[] array = new object[2];
			int num = 0;
			T_Variable identifier = this._identifier;
			array[num] = identifier.ToString();
			array[1] = this._domain;
			return StringUtil.FormatInvariant(format, array);
		}

		// Token: 0x04001681 RID: 5761
		private readonly T_Variable _identifier;

		// Token: 0x04001682 RID: 5762
		private readonly Set<T_Element> _domain;

		// Token: 0x04001683 RID: 5763
		private readonly int _hashCode;

		// Token: 0x04001684 RID: 5764
		private readonly IEqualityComparer<T_Variable> _identifierComparer;
	}
}
