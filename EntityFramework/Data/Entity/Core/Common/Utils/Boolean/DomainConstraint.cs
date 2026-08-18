using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000309 RID: 777
	internal class DomainConstraint<T_Variable, T_Element>
	{
		// Token: 0x06001B12 RID: 6930 RVA: 0x00086CD8 File Offset: 0x00084ED8
		internal DomainConstraint(DomainVariable<T_Variable, T_Element> variable, Set<T_Element> range)
		{
			this._variable = variable;
			this._range = range.AsReadOnly();
			this._hashCode = (this._variable.GetHashCode() ^ this._range.GetElementsHashCode());
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00086D10 File Offset: 0x00084F10
		internal DomainConstraint(DomainVariable<T_Variable, T_Element> variable, T_Element element) : this(variable, new Set<T_Element>(new T_Element[]
		{
			element
		}).MakeReadOnly())
		{
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00086D3E File Offset: 0x00084F3E
		internal DomainVariable<T_Variable, T_Element> Variable
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x00086D46 File Offset: 0x00084F46
		internal Set<T_Element> Range
		{
			get
			{
				return this._range;
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00086D4E File Offset: 0x00084F4E
		internal DomainConstraint<T_Variable, T_Element> InvertDomainConstraint()
		{
			return new DomainConstraint<T_Variable, T_Element>(this._variable, this._variable.Domain.Difference(this._range).AsReadOnly());
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00086D78 File Offset: 0x00084F78
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			DomainConstraint<T_Variable, T_Element> domainConstraint = obj as DomainConstraint<T_Variable, T_Element>;
			return domainConstraint != null && this._hashCode == domainConstraint._hashCode && this._range.SetEquals(domainConstraint._range) && this._variable.Equals(domainConstraint._variable);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00086DD2 File Offset: 0x00084FD2
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00086DDC File Offset: 0x00084FDC
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0} in [{1}]", new object[]
			{
				this._variable,
				this._range
			});
		}

		// Token: 0x04000981 RID: 2433
		private readonly DomainVariable<T_Variable, T_Element> _variable;

		// Token: 0x04000982 RID: 2434
		private readonly Set<T_Element> _range;

		// Token: 0x04000983 RID: 2435
		private readonly int _hashCode;
	}
}
