using System;
using System.Collections;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000163 RID: 355
	internal class EnumModel : TypeModel
	{
		// Token: 0x0600181B RID: 6171 RVA: 0x000692E2 File Offset: 0x000674E2
		internal EnumModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x000692F0 File Offset: 0x000674F0
		internal ConstantModel[] Constants
		{
			get
			{
				if (this.constants == null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (FieldInfo fieldInfo in base.Type.GetFields())
					{
						ConstantModel constantModel = this.GetConstantModel(fieldInfo);
						if (constantModel != null)
						{
							arrayList.Add(constantModel);
						}
					}
					this.constants = (ConstantModel[])arrayList.ToArray(typeof(ConstantModel));
				}
				return this.constants;
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00069360 File Offset: 0x00067560
		private ConstantModel GetConstantModel(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsSpecialName)
			{
				return null;
			}
			return new ConstantModel(fieldInfo, ((IConvertible)fieldInfo.GetValue(null)).ToInt64(null));
		}

		// Token: 0x04000B2C RID: 2860
		private ConstantModel[] constants;
	}
}
