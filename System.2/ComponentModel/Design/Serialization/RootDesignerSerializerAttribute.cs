using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000614 RID: 1556
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	[Obsolete("This attribute has been deprecated. Use DesignerSerializerAttribute instead.  For example, to specify a root designer for CodeDom, use DesignerSerializerAttribute(...,typeof(TypeCodeDomSerializer)).  http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class RootDesignerSerializerAttribute : Attribute
	{
		// Token: 0x060038F4 RID: 14580 RVA: 0x000F2539 File Offset: 0x000F0739
		public RootDesignerSerializerAttribute(Type serializerType, Type baseSerializerType, bool reloadable)
		{
			this.serializerTypeName = serializerType.AssemblyQualifiedName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.reloadable = reloadable;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000F2560 File Offset: 0x000F0760
		public RootDesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType, bool reloadable)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.reloadable = reloadable;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000F2582 File Offset: 0x000F0782
		public RootDesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName, bool reloadable)
		{
			this.serializerTypeName = serializerTypeName;
			this.serializerBaseTypeName = baseSerializerTypeName;
			this.reloadable = reloadable;
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000F259F File Offset: 0x000F079F
		public bool Reloadable
		{
			get
			{
				return this.reloadable;
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000F25A7 File Offset: 0x000F07A7
		public string SerializerTypeName
		{
			get
			{
				return this.serializerTypeName;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x060038F9 RID: 14585 RVA: 0x000F25AF File Offset: 0x000F07AF
		public string SerializerBaseTypeName
		{
			get
			{
				return this.serializerBaseTypeName;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000F25B8 File Offset: 0x000F07B8
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.serializerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		// Token: 0x04002B85 RID: 11141
		private bool reloadable;

		// Token: 0x04002B86 RID: 11142
		private string serializerTypeName;

		// Token: 0x04002B87 RID: 11143
		private string serializerBaseTypeName;

		// Token: 0x04002B88 RID: 11144
		private string typeId;
	}
}
