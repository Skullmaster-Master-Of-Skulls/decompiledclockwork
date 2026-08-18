using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A5 RID: 933
	internal class DomainConstraint<T_Variable, T_Element>
	{
		// Token: 0x06003366 RID: 13158 RVA: 0x000C8139 File Offset: 0x000C6339
		internal DomainConstraint(DomainVariable<T_Variable, T_Element> variable, Set<T_Element> range)
		{
			this._variable = variable;
			this._range = range.AsReadOnly();
			this._hashCode = (this._variable.GetHashCode() ^ this._range.GetElementsHashCode());
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000C8171 File Offset: 0x000C6371
		internal DomainConstraint(DomainVariable<T_Variable, T_Element> variable, T_Element element) : this(variable, new Set<T_Element>(new T_Element[]
		{
			element
		}).MakeReadOnly())
		{
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000C8192 File Offset: 0x000C6392
		internal DomainVariable<T_Variable, T_Element> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000C819A File Offset: 0x000C639A
		internal Set<T_Element> Range
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000C81A2 File Offset: 0x000C63A2
		internal DomainConstraint<T_Variable, T_Element> InvertDomainConstraint()
		{
			return new DomainConstraint<T_Variable, T_Element>(this._variable, this._variable.Domain.Difference(this._range).AsReadOnly());
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000C81CC File Offset: 0x000C63CC
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			DomainConstraint<T_Variable, T_Element> domainConstraint = obj as DomainConstraint<T_Variable, T_Element>;
			return domainConstraint != null && this._hashCode == domainConstraint._hashCode && this._range.SetEquals(domainConstraint._range) && this._variable.Equals(domainConstraint._variable);
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000C8221 File Offset: 0x000C6421
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000C8229 File Offset: 0x000C6429
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0} in [{1}]", new object[]
			{
				this._variable,
				this._range
			});
		}

		// Token: 0x04001685 RID: 5765
		private readonly DomainVariable<T_Variable, T_Element> _variable;

		// Token: 0x04001686 RID: 5766
		private readonly Set<T_Element> _range;

		// Token: 0x04001687 RID: 5767
		private readonly int _hashCode;
	}
}
