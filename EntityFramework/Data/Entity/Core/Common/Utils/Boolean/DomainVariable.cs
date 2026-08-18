using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200030B RID: 779
	internal class DomainVariable<T_Variable, T_Element>
	{
		// Token: 0x06001B20 RID: 6944 RVA: 0x00087290 File Offset: 0x00085490
		internal DomainVariable(T_Variable identifier, Set<T_Element> domain, IEqualityComparer<T_Variable> identifierComparer)
		{
			this._identifier = identifier;
			this._domain = domain.AsReadOnly();
			this._identifierComparer = (identifierComparer ?? EqualityComparer<T_Variable>.Default);
			int elementsHashCode = this._domain.GetElementsHashCode();
			int hashCode = this._identifierComparer.GetHashCode(this._identifier);
			this._hashCode = (elementsHashCode ^ hashCode);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000872ED File Offset: 0x000854ED
		internal DomainVariable(T_Variable identifier, Set<T_Element> domain) : this(identifier, domain, null)
		{
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x000872F8 File Offset: 0x000854F8
		internal T_Variable Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00087300 File Offset: 0x00085500
		internal Set<T_Element> Domain
		{
			get
			{
				return this._domain;
			}
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00087308 File Offset: 0x00085508
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00087310 File Offset: 0x00085510
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			DomainVariable<T_Variable, T_Element> domainVariable = obj as DomainVariable<T_Variable, T_Element>;
			return domainVariable != null && this._hashCode == domainVariable._hashCode && this._identifierComparer.Equals(this._identifier, domainVariable._identifier) && this._domain.SetEquals(domainVariable._domain);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00087370 File Offset: 0x00085570
		public override string ToString()
		{
			string format = "{0}{{{1}}}";
			object[] array = new object[2];
			object[] array2 = array;
			int num = 0;
			T_Variable identifier = this._identifier;
			array2[num] = identifier.ToString();
			array[1] = this._domain;
			return StringUtil.FormatInvariant(format, array);
		}

		// Token: 0x04000988 RID: 2440
		private readonly T_Variable _identifier;

		// Token: 0x04000989 RID: 2441
		private readonly Set<T_Element> _domain;

		// Token: 0x0400098A RID: 2442
		private readonly int _hashCode;

		// Token: 0x0400098B RID: 2443
		private readonly IEqualityComparer<T_Variable> _identifierComparer;
	}
}
