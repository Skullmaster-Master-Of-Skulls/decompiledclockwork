using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200068C RID: 1676
	public class RequiredField
	{
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000C478A File Offset: 0x000C298A
		internal RequiredField()
		{
		}

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000C4792 File Offset: 0x000C2992
		// (set) Token: 0x06003CE6 RID: 15590 RVA: 0x000C479A File Offset: 0x000C299A
		internal string Name { get; set; }

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x000C47A3 File Offset: 0x000C29A3
		// (set) Token: 0x06003CE8 RID: 15592 RVA: 0x000C47AB File Offset: 0x000C29AB
		internal bool IsCalculated { get; set; }

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x000C47B4 File Offset: 0x000C29B4
		// (set) Token: 0x06003CEA RID: 15594 RVA: 0x000C47BC File Offset: 0x000C29BC
		internal object AggregateFunction { get; private set; }

		// Token: 0x06003CEB RID: 15595 RVA: 0x000C47C8 File Offset: 0x000C29C8
		public static RequiredField ForCalculatedField(string calculatedFieldName)
		{
			return new RequiredField
			{
				Name = calculatedFieldName,
				IsCalculated = true
			};
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000C47EC File Offset: 0x000C29EC
		public static RequiredField ForProperty(string propertyName)
		{
			return new RequiredField
			{
				Name = propertyName
			};
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000C4808 File Offset: 0x000C2A08
		internal static RequiredField ForProperty(string propertyName, object aggregateFunction)
		{
			return new RequiredField
			{
				Name = propertyName,
				AggregateFunction = aggregateFunction
			};
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000C482C File Offset: 0x000C2A2C
		public override bool Equals(object obj)
		{
			RequiredField requiredField = obj as RequiredField;
			return requiredField != null && (object.Equals(this.Name, requiredField.Name) && object.Equals(this.IsCalculated, requiredField.IsCalculated)) && object.Equals(this.AggregateFunction, requiredField.AggregateFunction);
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000C4888 File Offset: 0x000C2A88
		public override int GetHashCode()
		{
			return ((this.Name == null) ? 0 : (this.Name.GetHashCode() << 6)) ^ this.IsCalculated.GetHashCode() ^ ((this.AggregateFunction == null) ? 0 : (this.AggregateFunction.GetHashCode() << 1));
		}
	}
}
