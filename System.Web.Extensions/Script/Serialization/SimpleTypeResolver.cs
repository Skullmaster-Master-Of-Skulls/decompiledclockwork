using System;

namespace System.Web.Script.Serialization
{
	// Token: 0x02000105 RID: 261
	public class SimpleTypeResolver : JavaScriptTypeResolver
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00031069 File Offset: 0x0002F269
		public override Type ResolveType(string id)
		{
			return Type.GetType(id);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00031045 File Offset: 0x0002F245
		public override string ResolveTypeId(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.AssemblyQualifiedName;
		}
	}
}
