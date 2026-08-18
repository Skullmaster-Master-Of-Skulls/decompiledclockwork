using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001883 RID: 6275
	public abstract class RadFilterSingleValueExpression<T> : RadFilterNonGroupExpression, IRadFilterValueExpression
	{
		// Token: 0x17004943 RID: 18755
		// (get) Token: 0x0600F306 RID: 62214 RVA: 0x00375788 File Offset: 0x00373988
		// (set) Token: 0x0600F307 RID: 62215 RVA: 0x003757B9 File Offset: 0x003739B9
		public T Value
		{
			get
			{
				object obj = base.ViewState["Value"];
				if (obj != null)
				{
					return (T)((object)obj);
				}
				return default(T);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17004944 RID: 18756
		// (get) Token: 0x0600F308 RID: 62216 RVA: 0x003757D1 File Offset: 0x003739D1
		public override Type FieldType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17004945 RID: 18757
		// (get) Token: 0x0600F309 RID: 62217 RVA: 0x003757E0 File Offset: 0x003739E0
		ArrayList IRadFilterValueExpression.Values
		{
			get
			{
				return new ArrayList
				{
					this.Value
				};
			}
		}

		// Token: 0x0600F30A RID: 62218 RVA: 0x00375808 File Offset: 0x00373A08
		void IRadFilterValueExpression.SetValues(ArrayList values)
		{
			if (values == null || values.Count < 1)
			{
				return;
			}
			this.Value = ((values[0] == null) ? default(T) : base.ParseValue<T>(values[0]));
		}
	}
}
