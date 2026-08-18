using System;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200026F RID: 623
	public sealed class DataBinding
	{
		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006062F File Offset: 0x0005E82F
		public DataBinding(string propertyName, Type propertyType, string expression)
		{
			this.propertyName = propertyName;
			this.propertyType = propertyType;
			this.expression = expression;
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0006064C File Offset: 0x0005E84C
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x00060654 File Offset: 0x0005E854
		public string Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0006065D File Offset: 0x0005E85D
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00060665 File Offset: 0x0005E865
		public Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0006066D File Offset: 0x0005E86D
		public override int GetHashCode()
		{
			return this.propertyName.ToLower(CultureInfo.InvariantCulture).GetHashCode();
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00060684 File Offset: 0x0005E884
		public override bool Equals(object obj)
		{
			if (obj != null && obj is DataBinding)
			{
				DataBinding dataBinding = (DataBinding)obj;
				return StringUtil.EqualsIgnoreCase(this.propertyName, dataBinding.PropertyName);
			}
			return false;
		}

		// Token: 0x04001963 RID: 6499
		private string propertyName;

		// Token: 0x04001964 RID: 6500
		private Type propertyType;

		// Token: 0x04001965 RID: 6501
		private string expression;
	}
}
