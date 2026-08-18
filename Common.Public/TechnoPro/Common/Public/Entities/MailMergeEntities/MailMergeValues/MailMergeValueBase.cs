using System;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D0 RID: 720
	public class MailMergeValueBase : ICloneable<MailMergeValueBase>, ICloneable
	{
		// Token: 0x060015C7 RID: 5575 RVA: 0x0000D55A File Offset: 0x0000B75A
		public MailMergeValueBase()
		{
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0001B2CC File Offset: 0x000194CC
		public virtual void SetValue(object obj)
		{
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public virtual object GetValue()
		{
			return null;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x0001B2E4 File Offset: 0x000194E4
		public T GetValue<T>(object obj, T defaultValue)
		{
			bool flag = obj == null;
			T result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				Type typeFromHandle = typeof(T);
				bool flag2 = !TypeAdapter.TypeEquals(obj.GetType(), typeFromHandle);
				if (flag2)
				{
					throw new InvalidParameterException("Expected type " + typeFromHandle.ToString());
				}
				result = (T)((object)obj);
			}
			return result;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0001B33C File Offset: 0x0001953C
		public MailMergeValueBase Clone()
		{
			return new MailMergeValueBase(this);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0001B354 File Offset: 0x00019554
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0001B36C File Offset: 0x0001956C
		public MailMergeValueBase(MailMergeValueBase item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.SetValue(item.GetValue());
			}
		}
	}
}
