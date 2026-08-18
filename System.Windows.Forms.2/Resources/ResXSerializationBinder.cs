using System;
using System.ComponentModel.Design;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x020000ED RID: 237
	internal class ResXSerializationBinder : SerializationBinder
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0000A099 File Offset: 0x00008299
		internal ResXSerializationBinder(ITypeResolutionService typeResolver)
		{
			this.typeResolver = typeResolver;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000A0A8 File Offset: 0x000082A8
		internal ResXSerializationBinder(Func<Type, string> typeNameConverter)
		{
			this.typeNameConverter = typeNameConverter;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public override Type BindToType(string assemblyName, string typeName)
		{
			if (this.typeResolver == null)
			{
				return null;
			}
			typeName = typeName + ", " + assemblyName;
			Type type = this.typeResolver.GetType(typeName);
			if (type == null)
			{
				string[] array = typeName.Split(new char[]
				{
					','
				});
				if (array != null && array.Length > 2)
				{
					string text = array[0].Trim();
					for (int i = 1; i < array.Length; i++)
					{
						string text2 = array[i].Trim();
						if (!text2.StartsWith("Version=") && !text2.StartsWith("version="))
						{
							text = text + ", " + text2;
						}
					}
					type = this.typeResolver.GetType(text);
					if (type == null)
					{
						type = this.typeResolver.GetType(array[0].Trim());
					}
				}
			}
			return type;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000A188 File Offset: 0x00008388
		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			typeName = null;
			if (this.typeNameConverter != null)
			{
				string assemblyQualifiedName = MultitargetUtil.GetAssemblyQualifiedName(serializedType, this.typeNameConverter);
				if (!string.IsNullOrEmpty(assemblyQualifiedName))
				{
					int num = assemblyQualifiedName.IndexOf(',');
					if (num > 0 && num < assemblyQualifiedName.Length - 1)
					{
						assemblyName = assemblyQualifiedName.Substring(num + 1).TrimStart(new char[0]);
						string text = assemblyQualifiedName.Substring(0, num);
						if (!string.Equals(text, serializedType.FullName, StringComparison.InvariantCulture))
						{
							typeName = text;
						}
						return;
					}
				}
			}
			base.BindToName(serializedType, out assemblyName, out typeName);
		}

		// Token: 0x040003CC RID: 972
		private ITypeResolutionService typeResolver;

		// Token: 0x040003CD RID: 973
		private Func<Type, string> typeNameConverter;
	}
}
