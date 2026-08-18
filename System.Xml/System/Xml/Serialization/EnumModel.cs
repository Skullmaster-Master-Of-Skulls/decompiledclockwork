using System;
using System.Collections;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002DE RID: 734
	internal class EnumModel : TypeModel
	{
		// Token: 0x06002266 RID: 8806 RVA: 0x000A0F1F File Offset: 0x0009FF1F
		internal EnumModel(Type type, TypeDesc typeDesc, ModelScope scope) : base(type, typeDesc, scope)
		{
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x000A0F2C File Offset: 0x0009FF2C
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

		// Token: 0x06002268 RID: 8808 RVA: 0x000A0F9C File Offset: 0x0009FF9C
		private ConstantModel GetConstantModel(FieldInfo fieldInfo)
		{
			if (fieldInfo.IsSpecialName)
			{
				return null;
			}
			return new ConstantModel(fieldInfo, ((IConvertible)fieldInfo.GetValue(null)).ToInt64(null));
		}

		// Token: 0x040014C0 RID: 5312
		private ConstantModel[] constants;
	}
}
