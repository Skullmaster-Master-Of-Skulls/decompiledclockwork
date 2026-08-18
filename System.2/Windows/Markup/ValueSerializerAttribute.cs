using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Markup
{
	// Token: 0x020003A3 RID: 931
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	[TypeForwardedFrom("WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public sealed class ValueSerializerAttribute : Attribute
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x000A52D0 File Offset: 0x000A34D0
		public ValueSerializerAttribute(Type valueSerializerType)
		{
			this._valueSerializerType = valueSerializerType;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000A52DF File Offset: 0x000A34DF
		public ValueSerializerAttribute(string valueSerializerTypeName)
		{
			this._valueSerializerTypeName = valueSerializerTypeName;
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000A52EE File Offset: 0x000A34EE
		public Type ValueSerializerType
		{
			get
			{
				if (this._valueSerializerType == null && this._valueSerializerTypeName != null)
				{
					this._valueSerializerType = Type.GetType(this._valueSerializerTypeName);
				}
				return this._valueSerializerType;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000A531D File Offset: 0x000A351D
		public string ValueSerializerTypeName
		{
			get
			{
				if (this._valueSerializerType != null)
				{
					return this._valueSerializerType.AssemblyQualifiedName;
				}
				return this._valueSerializerTypeName;
			}
		}

		// Token: 0x04001FA0 RID: 8096
		private Type _valueSerializerType;

		// Token: 0x04001FA1 RID: 8097
		private string _valueSerializerTypeName;
	}
}
