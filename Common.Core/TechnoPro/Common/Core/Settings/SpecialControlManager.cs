using System;
using TechnoPro.Common.DAO.Impl.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Settings
{
	// Token: 0x02000047 RID: 71
	public class SpecialControlManager : ISpecialControlManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002EC RID: 748 RVA: 0x000111AE File Offset: 0x0000F3AE
		public SpecialControlManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000111C0 File Offset: 0x0000F3C0
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000111C8 File Offset: 0x0000F3C8
		public OperationContext OpContext { get; set; }

		// Token: 0x060002EF RID: 751 RVA: 0x000111D4 File Offset: 0x0000F3D4
		public int GetSpecialControlId(eSpecialControlType SpecialControlType)
		{
			SpecialControlDAO specialControlDAO = new SpecialControlDAO(this.OpContext);
			return specialControlDAO.GetSpecialControlId(SpecialControlType);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000111FC File Offset: 0x0000F3FC
		public T? GetSpecialControlValue<T>(int PersonId, eSpecialControlType SpecialControlType) where T : struct
		{
			SpecialControlDAO specialControlDAO = new SpecialControlDAO(this.OpContext);
			Type typeFromHandle = typeof(T);
			object obj = null;
			bool flag = typeFromHandle == typeof(int);
			if (flag)
			{
				obj = specialControlDAO.GetSpecialControlValueInt(PersonId, SpecialControlType);
			}
			else
			{
				bool flag2 = typeFromHandle == typeof(bool);
				if (flag2)
				{
					obj = specialControlDAO.GetSpecialControlValueBool(PersonId, SpecialControlType);
				}
				else
				{
					bool flag3 = typeFromHandle == typeof(DateTime);
					if (flag3)
					{
						obj = specialControlDAO.GetSpecialControlValueDateTime(PersonId, SpecialControlType);
					}
					else
					{
						bool flag4 = typeFromHandle == typeof(string);
						if (flag4)
						{
							obj = specialControlDAO.GetSpecialControlValueString(PersonId, SpecialControlType);
						}
						else
						{
							bool flag5 = typeFromHandle == typeof(double);
							if (flag5)
							{
								string specialControlValueString = specialControlDAO.GetSpecialControlValueString(PersonId, SpecialControlType);
								double num;
								bool flag6 = double.TryParse(specialControlValueString, out num);
								if (flag6)
								{
									obj = num;
								}
							}
						}
					}
				}
			}
			return (T?)obj;
		}
	}
}
