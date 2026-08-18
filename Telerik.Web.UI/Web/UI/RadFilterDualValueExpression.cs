using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001881 RID: 6273
	public abstract class RadFilterDualValueExpression<T> : RadFilterNonGroupExpression, IRadFilterValueExpression
	{
		// Token: 0x1700493E RID: 18750
		// (get) Token: 0x0600F2FB RID: 62203 RVA: 0x00375620 File Offset: 0x00373820
		// (set) Token: 0x0600F2FC RID: 62204 RVA: 0x00375651 File Offset: 0x00373851
		public T LeftValue
		{
			get
			{
				object obj = base.ViewState["LeftValue"];
				if (obj != null)
				{
					return (T)((object)obj);
				}
				return default(T);
			}
			set
			{
				base.ViewState["LeftValue"] = value;
			}
		}

		// Token: 0x1700493F RID: 18751
		// (get) Token: 0x0600F2FD RID: 62205 RVA: 0x0037566C File Offset: 0x0037386C
		// (set) Token: 0x0600F2FE RID: 62206 RVA: 0x0037569D File Offset: 0x0037389D
		public T RightValue
		{
			get
			{
				object obj = base.ViewState["RightValue"];
				if (obj != null)
				{
					return (T)((object)obj);
				}
				return default(T);
			}
			set
			{
				base.ViewState["RightValue"] = value;
			}
		}

		// Token: 0x17004940 RID: 18752
		// (get) Token: 0x0600F2FF RID: 62207 RVA: 0x003756B5 File Offset: 0x003738B5
		public override Type FieldType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17004941 RID: 18753
		// (get) Token: 0x0600F300 RID: 62208 RVA: 0x003756C4 File Offset: 0x003738C4
		ArrayList IRadFilterValueExpression.Values
		{
			get
			{
				return new ArrayList
				{
					this.LeftValue,
					this.RightValue
				};
			}
		}

		// Token: 0x0600F301 RID: 62209 RVA: 0x003756FC File Offset: 0x003738FC
		void IRadFilterValueExpression.SetValues(ArrayList values)
		{
			if (values == null || values.Count < 2)
			{
				return;
			}
			this.LeftValue = ((values[0] == null) ? default(T) : base.ParseValue<T>(values[0]));
			this.RightValue = ((values[1] == null) ? default(T) : base.ParseValue<T>(values[1]));
		}
	}
}
